using CAIT.SQLHelper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebStore.Models;
using WedStore.Const;

namespace WedStore.Repositories
{
    public class NguoiDungDB
    {
        private readonly static string connectionString = ConnectStringValue.ConnectStringMyDB;


        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

  
        public static  NguoiDungDTO LoginUser(string UserName, string Password)
        {
           string userRole = ""; 
           NguoiDungDTO nd= new NguoiDungDTO();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_LOGIN", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", UserName);
                        command.Parameters.AddWithValue("@Password", Password);

                        SqlParameter userRoleParam = new SqlParameter("@UserRole", SqlDbType.VarChar, 10);
                        userRoleParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(userRoleParam);

                        SqlParameter fullNameParam = new SqlParameter("@FullName", SqlDbType.NVarChar, 255);
                        fullNameParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(fullNameParam);


						SqlParameter idUserParam = new SqlParameter("@UserId", SqlDbType.VarChar, 50);
						idUserParam.Direction = ParameterDirection.Output;
						command.Parameters.Add(idUserParam);

						command.ExecuteNonQuery();

                        if (userRoleParam.Value == DBNull.Value)
                        {
                            return nd;
                        }

                        userRole = userRoleParam.Value.ToString();
                        string fullName= fullNameParam.Value.ToString();
                        string idUser = idUserParam.Value.ToString();
                        nd.idND= idUser;
                        nd.UserRole = userRole;
                        nd.UserName = UserName;
                        nd.Password=Password;
                        nd.FullName= fullName;
                        return nd;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the error (very important)
                Console.WriteLine($"Error during login: {ex.Message}");
                return nd;
               
            }
            catch (Exception ex)
            {
                //Log the error
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return nd;


            }
        }

    

    




        public static List<NguoiDungDTO> LayTatCaNguoiDung()
        {
            var list = new List<NguoiDungDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Câu truy vấn SQL với JOIN để lấy thông tin từ bảng KhachHang hoặc NhanVien
                string query = @"
          

   SELECT 
    nd.Id, 
    nd.Username, 
    nd.Password, 
    r.role_name AS Role,  -- Thêm role_name từ bảng Roles
    nd.GioiTinh, 
    nd.NgaySinh,
    CASE 
        WHEN r.role_name = 'Customer' THEN kh.Ten 
        WHEN r.role_name IN ('Staff', 'Admin') THEN nv.Ten 
        ELSE NULL 
    END AS Ten,
    CASE 
        WHEN r.role_name = 'Customer' THEN kh.Email 
        WHEN r.role_name IN ('Staff', 'Admin') THEN nv.Email 
        ELSE NULL 
    END AS Email
FROM 
    NguoiDung nd
JOIN 
    User_Roles ur ON nd.Id = ur.user_id
JOIN 
    Roles r ON ur.role_id = r.id
LEFT JOIN 
    KhachHang kh ON nd.Id = kh.Id_NguoiDung AND r.role_name = 'Customer'
LEFT JOIN 
    NhanVien nv ON nd.Id = nv.Id_NguoiDung AND r.role_name IN ('Staff', 'Admin');
";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            string loaiTaiKhoan = role == "customer" ? "Khách hàng" : "Nhân viên";

                            list.Add(new NguoiDungDTO
                            {
                                idND = reader["Id"].ToString(),
                                UserName = reader["Username"].ToString(),
                                UserRole = role,
                                TypeRole = loaiTaiKhoan,
                                Password = reader["Password"].ToString(),
                                Gender = int.Parse(reader["GioiTinh"].ToString()),
                                NgaySinh = reader["NgaySinh"] != DBNull.Value ? (DateTime?)reader["NgaySinh"] : null,
                                FullName = reader["Ten"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }



        public static NguoiDungDTO LayChiTietNguoiDungTheoId(string id)
        {
            var nguoiDung = new NguoiDungDTO();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Lấy thông tin cơ bản từ bảng NguoiDung
                using (SqlCommand cmd = new SqlCommand("" +
                    "   SELECT     nd.Username,    nd.Password,     r.role_name AS Role, r.id as role_id ,  nd.GioiTinh,     nd.NgaySinh," +
                    "   CASE         WHEN r.role_name = 'Customer' THEN kh.Ten       WHEN r.role_name IN ('Staff', 'Admin') THEN nv.Ten " +
                    "        ELSE NULL " +
                    "   END AS Ten,    CASE " +
                    "     WHEN r.role_name = 'Customer' THEN kh.Email         WHEN r.role_name IN ('Staff', 'Admin') THEN nv.Email" +
                    "      ELSE NULL     END AS Email FROM     NguoiDung nd JOIN " +
                    "   User_Roles ur ON nd.Id = ur.user_id JOIN    Roles r ON ur.role_id = r.id LEFT JOIN " +
                    "  KhachHang kh ON nd.Id = kh.Id_NguoiDung AND r.role_name = 'Customer'\r\nLEFT JOIN \r\n    NhanVien nv ON nd.Id = nv.Id_NguoiDung AND r.role_name IN ('Staff', 'Admin') WHERE     nd.Id = @Id;", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                       

                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            string loaiTaiKhoan = role == "customer" ? "Khách hàng" : "Nhân viên";
                            nguoiDung.idND = id;
                            nguoiDung.UserName = reader["Username"].ToString();
                            nguoiDung.RoleId = reader["role_id"].ToString();
                            nguoiDung.UserRole = reader["Role"].ToString();
                            nguoiDung.TypeRole = loaiTaiKhoan;
                            nguoiDung.Gender=int.Parse(reader["GioiTinh"].ToString());
                            nguoiDung.Password= reader["Password"].ToString();
                            nguoiDung.NgaySinh = reader["NgaySinh"] != DBNull.Value ? (DateTime?)reader["NgaySinh"] : null;




                        }
                    }
                }

                // Kiểm tra vai trò và lấy thông tin chi tiết từ bảng KhachHang hoặc NhanVien
                if (nguoiDung.UserRole == "customer")
                {
                    // Lấy thông tin từ bảng KhachHang nếu role là 'customer'
                    using (SqlCommand cmd = new SqlCommand("SELECT ten, diachi, sodienthoai, email FROM KhachHang WHERE id_NguoiDung = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nguoiDung.FullName = reader["ten"].ToString();
                                nguoiDung.Address = reader["diachi"].ToString();
                                nguoiDung.Phone = reader["sodienthoai"].ToString();
                                nguoiDung.Email = reader["email"].ToString();
                            }
                        }
                    }
                }
                else if (nguoiDung.UserRole == "staff" || nguoiDung.UserRole == "admin")
                {
                    // Lấy thông tin từ bảng NhanVien nếu role là 'staff' hoặc 'admin'
                    using (SqlCommand cmd = new SqlCommand("SELECT ten, chucVu, sodienthoai, email FROM NhanVien WHERE id_NguoiDung = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nguoiDung.FullName = reader["ten"].ToString();
                                nguoiDung.ChucVu = reader["chucVu"].ToString();
                                nguoiDung.Phone = reader["sodienthoai"].ToString();
                                nguoiDung.Email = reader["email"].ToString();
                            }
                        }
                    }
                }
            }

            return nguoiDung;
        }
        public static bool ThemMoiNguoiDung(NguoiDungDTO nguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    string idNguoiDung = Guid.NewGuid().ToString(); // Tạo Id ngẫu nhiên cho người dùng mới

                    try
                    {
                        // Insert vào bảng NguoiDung
                        string insertNguoiDungQuery = @"
                    INSERT INTO NguoiDung (Id, Username, Password, GioiTinh, NgaySinh) 
                    VALUES (@Id, @Username, @Password, @GioiTinh, @NgaySinh)";
                        using (SqlCommand cmd = new SqlCommand(insertNguoiDungQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", idNguoiDung);
                            cmd.Parameters.AddWithValue("@Username", nguoiDung.UserName);
                            cmd.Parameters.AddWithValue("@Password", nguoiDung.Password); // Lưu mật khẩu đã mã hóa
                            cmd.Parameters.AddWithValue("@GioiTinh", nguoiDung.Gender);
                            cmd.Parameters.AddWithValue("@NgaySinh", nguoiDung.NgaySinh);

                            cmd.ExecuteNonQuery();
                        }

                        // Gán vai trò cho người dùng
                        string insertUserRoleQuery = @"
                    INSERT INTO User_Roles (user_id, role_id) 
                    VALUES (@UserId, @RoleId)";
                        using (SqlCommand cmd = new SqlCommand(insertUserRoleQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", idNguoiDung);
                            cmd.Parameters.AddWithValue("@RoleId", nguoiDung.RoleId); // RoleId cần được ánh xạ từ giao diện

                            cmd.ExecuteNonQuery();
                        }

                        // Insert vào bảng KhachHang nếu vai trò là "customer"
                        if (nguoiDung.UserRole == "customer")
                        {
                            string insertKhachHangQuery = @"
                        INSERT INTO KhachHang (Id, Ten, DiaChi, SoDienThoai, Email, Id_NguoiDung) 
                        VALUES (@Id, @Ten, @DiaChi, @SoDienThoai, @Email, @Id_NguoiDung)";
                            using (SqlCommand cmd = new SqlCommand(insertKhachHangQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                                cmd.Parameters.AddWithValue("@Ten", nguoiDung.FullName);
                                cmd.Parameters.AddWithValue("@DiaChi", nguoiDung.Address);
                                cmd.Parameters.AddWithValue("@SoDienThoai", nguoiDung.Phone);
                                cmd.Parameters.AddWithValue("@Email", nguoiDung.Email);
                                cmd.Parameters.AddWithValue("@Id_NguoiDung", idNguoiDung);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Insert vào bảng NhanVien nếu vai trò là "staff" hoặc "admin"
                        if (nguoiDung.UserRole == "staff" || nguoiDung.UserRole == "admin")
                        {
                            string insertNhanVienQuery = @"
                        INSERT INTO NhanVien (Id, Ten, ChucVu, SoDienThoai, Email, Id_NguoiDung) 
                        VALUES (@Id, @Ten, @ChucVu, @SoDienThoai, @Email, @Id_NguoiDung)";
                            using (SqlCommand cmd = new SqlCommand(insertNhanVienQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                                cmd.Parameters.AddWithValue("@Ten", nguoiDung.FullName);
                                cmd.Parameters.AddWithValue("@ChucVu", nguoiDung.ChucVu); // Chức vụ của nhân viên
                                cmd.Parameters.AddWithValue("@SoDienThoai", nguoiDung.Phone);
                                cmd.Parameters.AddWithValue("@Email", nguoiDung.Email);
                                cmd.Parameters.AddWithValue("@Id_NguoiDung", idNguoiDung);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction nếu không có lỗi
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction nếu có lỗi
                        transaction.Rollback();
                        Console.WriteLine($"Error: {ex.Message}");
                        return false;
                    }
                }
            }
        }


        public static bool CapNhatNguoiDung(NguoiDungDTO nguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Cập nhật thông tin bảng NguoiDung
                        string updateNguoiDungQuery = "UPDATE NguoiDung SET     Username = @Username, \r\n    Password = @Password, " +
                            "    GioiTinh = @GioiTinh,    NgaySinh = @NgaySinh WHERE \r\n    Id = @Id; " +
                            "UPDATE User_Roles SET    role_id = @RoleId WHERE \r\n    user_id = @user_Id;";
                        using (SqlCommand cmd = new SqlCommand(updateNguoiDungQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", nguoiDung.idND);
                            cmd.Parameters.AddWithValue("@user_Id", nguoiDung.idND);

                            cmd.Parameters.AddWithValue("@GioiTinh", nguoiDung.Gender);
                            cmd.Parameters.AddWithValue("@Username", nguoiDung.UserName);
                            cmd.Parameters.AddWithValue("@Password", nguoiDung.Password);  // Lưu mật khẩu đã mã hóa
                            cmd.Parameters.AddWithValue("@RoleId", nguoiDung.RoleId);
                            cmd.Parameters.AddWithValue("@NgaySinh", nguoiDung.NgaySinh ?? (object)DBNull.Value);  // Kiểm tra null trước khi cập nhật


                            cmd.ExecuteNonQuery();
                        }

                        // Cập nhật thông tin bảng KhachHang nếu vai trò là customer
                        if (nguoiDung.UserRole == "customer")
                        {
                            string updateKhachHangQuery = "UPDATE KhachHang SET Ten = @Ten, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE Id_NguoiDung = @Id_NguoiDung";
                            using (SqlCommand cmd = new SqlCommand(updateKhachHangQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Ten", nguoiDung.FullName);
                                cmd.Parameters.AddWithValue("@DiaChi", nguoiDung.Address);
                                cmd.Parameters.AddWithValue("@SoDienThoai", nguoiDung.Phone);
                                cmd.Parameters.AddWithValue("@Email", nguoiDung.Email);
                                cmd.Parameters.AddWithValue("@Id_NguoiDung", nguoiDung.idND);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        // Cập nhật thông tin bảng NhanVien nếu vai trò là staff hoặc admin
                        else if (nguoiDung.UserRole == "staff" || nguoiDung.UserRole == "admin")
                        {
                            string updateNhanVienQuery = "UPDATE NhanVien SET Ten = @Ten, ChucVu = @ChucVu, SoDienThoai = @SoDienThoai, Email = @Email WHERE Id_NguoiDung = @Id_NguoiDung";
                            using (SqlCommand cmd = new SqlCommand(updateNhanVienQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Ten", nguoiDung.FullName);
                                cmd.Parameters.AddWithValue("@ChucVu", nguoiDung.ChucVu);  // Cập nhật chức vụ
                                cmd.Parameters.AddWithValue("@SoDienThoai", nguoiDung.Phone);
                                cmd.Parameters.AddWithValue("@Email", nguoiDung.Email);
                                cmd.Parameters.AddWithValue("@Id_NguoiDung", nguoiDung.idND);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction nếu không có lỗi
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        // Nếu có lỗi, rollback transaction
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public static bool XoaNguoiDung(string idNguoiDung)
        {
            try
            {
                object[] value = { idNguoiDung };
                SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
                DataTable result = connection.Select("SP_XoaNguoiDungVaLienQuan", value);

                if (connection.errorCode == 0 && connection.errorMessage == "")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        
        }

    

    public static List<KhachHangDTO> LayTatCaKhachHang()
        {
            var list = new List<KhachHangDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Ten, Diachi, Sodienthoai, Email, Id_NguoiDung FROM Khachhang", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new KhachHangDTO
                            {
                                Id = reader["Id"].ToString(),
                                Ten = reader["Ten"].ToString(),
                                Diachi = reader["Diachi"].ToString(),
                                Sodienthoai = reader["Sodienthoai"].ToString(),
                                Email = reader["Email"].ToString(),
                                IdNguoiDung = reader["Id_NguoiDung"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public static KhachHangDTO LayChiTietKhachHangTheoId(string id)
        {
            KhachHangDTO khachhang = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Ten, Diachi, Sodienthoai, Email, Id_NguoiDung FROM Khachhang WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            khachhang = new KhachHangDTO
                            {
                                Id = reader["Id"].ToString(),
                                Ten = reader["Ten"].ToString(),
                                Diachi = reader["Diachi"].ToString(),
                                Sodienthoai = reader["Sodienthoai"].ToString(),
                                Email = reader["Email"].ToString(),
                                IdNguoiDung = reader["Id_NguoiDung"].ToString()
                            };
                        }
                    }
                }
            }
            return khachhang;
        }


        public static bool ThemMoiKhachHang(KhachHangDTO khachhang)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Khachhang (Id, Ten, Diachi, Sodienthoai, Email, Id_NguoiDung) VALUES (@Id, @Ten, @Diachi, @Sodienthoai, @Email, @Id_NguoiDung)", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", khachhang.Id);
                    cmd.Parameters.AddWithValue("@Ten", khachhang.Ten);
                    cmd.Parameters.AddWithValue("@Diachi", khachhang.Diachi);
                    cmd.Parameters.AddWithValue("@Sodienthoai", khachhang.Sodienthoai);
                    cmd.Parameters.AddWithValue("@Email", khachhang.Email);
                    cmd.Parameters.AddWithValue("@Id_NguoiDung", khachhang.IdNguoiDung);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public static bool CapNhatKhachHang(KhachHangDTO khachhang)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Khachhang SET Ten = @Ten, Diachi = @Diachi, Sodienthoai = @Sodienthoai, Email = @Email, Id_NguoiDung = @Id_NguoiDung WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", khachhang.Id);
                    cmd.Parameters.AddWithValue("@Ten", khachhang.Ten);
                    cmd.Parameters.AddWithValue("@Diachi", khachhang.Diachi);
                    cmd.Parameters.AddWithValue("@Sodienthoai", khachhang.Sodienthoai);
                    cmd.Parameters.AddWithValue("@Email", khachhang.Email);
                    cmd.Parameters.AddWithValue("@Id_NguoiDung", khachhang.IdNguoiDung);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool XoaKhachHang(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Khachhang WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }



        public static List<NhanVienDTO> LayTatCaNhanVien()
        {
            var list = new List<NhanVienDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Ten, ChucVu, Sodienthoai, Email, Id_NguoiDung FROM NhanVien", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NhanVienDTO
                            {
                                Id = reader["Id"].ToString(),
                                Ten = reader["Ten"].ToString(),
                                ChucVu = reader["ChucVu"].ToString(),
                                Sodienthoai = reader["Sodienthoai"].ToString(),
                                Email = reader["Email"].ToString(),
                                IdNguoiDung = reader["Id_NguoiDung"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public static NhanVienDTO LayChiTietNhanVienTheoId(string id)
        {
            NhanVienDTO nhanVien = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Ten, ChucVu, Sodienthoai, Email, Id_NguoiDung FROM NhanVien WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nhanVien = new NhanVienDTO
                            {
                                Id = reader["Id"].ToString(),
                                Ten = reader["Ten"].ToString(),
                                ChucVu = reader["ChucVu"].ToString(),
                                Sodienthoai = reader["Sodienthoai"].ToString(),
                                Email = reader["Email"].ToString(),
                                IdNguoiDung = reader["Id_NguoiDung"].ToString()
                            };
                        }
                    }
                }
            }
            return nhanVien;
        }


        public static bool ThemMoiNhanVien(NhanVienDTO nhanVien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NhanVien (Id, Ten, ChucVu, Sodienthoai, Email, Id_NguoiDung) VALUES (@Id, @Ten, @ChucVu, @Sodienthoai, @Email, @Id_NguoiDung)", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", nhanVien.Id);
                    cmd.Parameters.AddWithValue("@Ten", nhanVien.Ten);
                    cmd.Parameters.AddWithValue("@ChucVu", nhanVien.ChucVu);
                    cmd.Parameters.AddWithValue("@Sodienthoai", nhanVien.Sodienthoai);
                    cmd.Parameters.AddWithValue("@Email", nhanVien.Email);
                    cmd.Parameters.AddWithValue("@Id_NguoiDung", nhanVien.IdNguoiDung);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool CapNhatNhanVien(NhanVienDTO nhanVien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET Ten = @Ten, ChucVu = @ChucVu, Sodienthoai = @Sodienthoai, Email = @Email, Id_NguoiDung = @Id_NguoiDung WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", nhanVien.Id);
                    cmd.Parameters.AddWithValue("@Ten", nhanVien.Ten);
                    cmd.Parameters.AddWithValue("@ChucVu", nhanVien.ChucVu);
                    cmd.Parameters.AddWithValue("@Sodienthoai", nhanVien.Sodienthoai);
                    cmd.Parameters.AddWithValue("@Email", nhanVien.Email);
                    cmd.Parameters.AddWithValue("@Id_NguoiDung", nhanVien.IdNguoiDung);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }



        }
        public static bool XoaNhanVien(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM NhanVien WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
