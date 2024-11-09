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
        public static List<NguoiDungDTO> GetAll()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_GetAll", value);
            List<NguoiDungDTO> lstResult = new List<NguoiDungDTO>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    NguoiDungDTO account = new NguoiDungDTO();
                    account.UserName = dr["UserName"].ToString();
                    account.Password = dr["Password"].ToString();
                    account.FullName = dr["FullName"].ToString();
                    account.Age = string.IsNullOrEmpty(dr["Age"].ToString()) ? 0 : int.Parse(dr["Age"].ToString());
                    account.Gender = string.IsNullOrEmpty(dr["Gender"].ToString()) ? 0 : int.Parse(dr["Gender"].ToString());
                    account.Address = dr["Address"].ToString();
                    account.Email = dr["Email"].ToString();
                    account.Phone = "0" + dr["Phone"].ToString();
                    account.Authority = string.IsNullOrEmpty(dr["Authority"].ToString()) ? 0 : int.Parse(dr["Authority"].ToString());


                    lstResult.Add(account);
                }
            }
            return lstResult;
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


						SqlParameter idUserParam = new SqlParameter("@idND", SqlDbType.VarChar, 50);
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
        public static NguoiDungDTO GetAccountWithUser(string UserName)
        {
            object[] value = { UserName };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_GetWithUser", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var acc = new NguoiDungDTO();
                acc.UserName = result.Rows[0]["UserName"].ToString();
                acc.FullName = result.Rows[0]["FullName"].ToString();
                acc.Age = string.IsNullOrEmpty(result.Rows[0]["Age"].ToString()) ? 0 : int.Parse(result.Rows[0]["Age"].ToString());
                acc.Gender = string.IsNullOrEmpty(result.Rows[0]["Gender"].ToString()) ? 0 : int.Parse(result.Rows[0]["Gender"].ToString());
                acc.Email = result.Rows[0]["Email"].ToString();
                acc.Address = result.Rows[0]["Address"].ToString();
                int phone = string.IsNullOrEmpty(result.Rows[0]["Phone"].ToString()) ? 0 : int.Parse(result.Rows[0]["Phone"].ToString());
                acc.Phone = "0"+phone.ToString();
                acc.Authority = string.IsNullOrEmpty(result.Rows[0]["Authority"].ToString()) ? 0 : int.Parse(result.Rows[0]["Authority"].ToString());

                return acc;
            }
            return new NguoiDungDTO();
        }
        public static NguoiDungDTO GetAccountWithEmail(string Email)
        {
            object[] value = { Email };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_GetWithEmail", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var acc = new NguoiDungDTO();
                acc.FullName = result.Rows[0]["FullName"].ToString();
                acc.Email = result.Rows[0]["Email"].ToString();
                acc.Address = result.Rows[0]["Address"].ToString();
                int phone = string.IsNullOrEmpty(result.Rows[0]["Phone"].ToString()) ? 0 : int.Parse(result.Rows[0]["Phone"].ToString());
                acc.Phone = "0" + phone.ToString();
                return acc;
            }
            return new NguoiDungDTO();
        }
        public static bool Account_Create(NguoiDungDTO account)
        {
            object[] value = { account.UserName,account.Password,account.FullName,account.Age, 
                account.Gender,account.Address,account.Email,account.Phone,account.Authority };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_Create", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static bool Account_Update(NguoiDungDTO account)
        {
            object[] value = { account.UserName,account.FullName,account.Age,
                account.Gender,account.Address,account.Email,account.Phone,account.Authority };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_Update", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static bool Account_Delete(string userName)
        {
            object[] value = { userName };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_Delete", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }




        public static List<NguoiDungDTO> LayTatCaNguoiDung()
        {
            var list = new List<NguoiDungDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Username,Password, Role,GioiTinh,NgaySinh FROM NguoiDung", conn))
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
                                Gender = int.Parse(reader["GioiTinh"].ToString())

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
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Username,Password, Role,GioiTinh,NgaySinh FROM NguoiDung WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                       

                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            string loaiTaiKhoan = role == "customer" ? "Khách hàng" : "Nhân viên";
                            nguoiDung.idND = reader["Id"].ToString();
                            nguoiDung.UserName = reader["Username"].ToString();
                            nguoiDung.UserRole = reader["Role"].ToString();
                            nguoiDung.TypeRole = loaiTaiKhoan;
                            nguoiDung.Gender=int.Parse(reader["GioiTinh"].ToString());
                            nguoiDung.Password= reader["Password"].ToString();

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
            using(SqlConnection conn = new SqlConnection(connectionString))
        {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    string idNguoiDung = Guid.NewGuid().ToString(); // Tạo Id ngẫu nhiên cho người dùng mới

                    try
                    {
                        // Insert vào bảng NguoiDung
                        string insertNguoiDungQuery = "INSERT INTO NguoiDung (Id, Username, Password, Role,GioiTinh,NgaySinh) VALUES (@Id, @Username, @Password, @Role,@GioiTinh,@NgaySinh)";
                        using (SqlCommand cmd = new SqlCommand(insertNguoiDungQuery, conn, transaction))
                        {

                            cmd.Parameters.AddWithValue("@Id", idNguoiDung);
                            cmd.Parameters.AddWithValue("@Username", nguoiDung.UserName);
                            cmd.Parameters.AddWithValue("@Password", nguoiDung.Password);  // Lưu mật khẩu đã mã hóa
                            cmd.Parameters.AddWithValue("@Role", nguoiDung.UserRole);
                            cmd.Parameters.AddWithValue("@GioiTinh", nguoiDung.Gender);
                            cmd.Parameters.AddWithValue("@NgaySinh", nguoiDung.NgaySinh);

                            cmd.ExecuteNonQuery();
                        }

                        // Insert vào bảng KhachHang hoặc NhanVien tùy theo role
                        if (nguoiDung.UserRole == "customer")
                        {
                            string insertKhachHangQuery = "INSERT INTO KhachHang (Id, Ten, DiaChi, SoDienThoai, Email, Id_NguoiDung) VALUES (@Id, @Ten, @DiaChi, @SoDienThoai, @Email, @Id_NguoiDung)";
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
                        else if (nguoiDung.UserRole == "staff" || nguoiDung.UserRole == "admin")
                        {
                            string insertNhanVienQuery = "INSERT INTO NhanVien (Id, Ten, ChucVu, SoDienThoai, Email, Id_NguoiDung) VALUES (@Id, @Ten, @ChucVu, @SoDienThoai, @Email, @Id_NguoiDung)";
                            using (SqlCommand cmd = new SqlCommand(insertNhanVienQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                                cmd.Parameters.AddWithValue("@Ten", nguoiDung.FullName);
                                cmd.Parameters.AddWithValue("@ChucVu", nguoiDung.ChucVu);  // Chức vụ của nhân viên
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
                    catch (Exception)
                    {
                        // Nếu có lỗi, rollback transaction
                        transaction.Rollback();
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
                        string updateNguoiDungQuery = "UPDATE NguoiDung SET Username = @Username, Password = @Password,GioiTinh=@GioiTinh, Role = @Role, NgaySinh=@NgaySinh WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(updateNguoiDungQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", nguoiDung.idND);

                            cmd.Parameters.AddWithValue("@GioiTinh", nguoiDung.Gender);
                            cmd.Parameters.AddWithValue("@Username", nguoiDung.UserName);
                            cmd.Parameters.AddWithValue("@Password", nguoiDung.Password);  // Lưu mật khẩu đã mã hóa
                            cmd.Parameters.AddWithValue("@Role", nguoiDung.UserRole);
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Xóa thông tin trong bảng NhanVien nếu người dùng là nhân viên (staff hoặc admin)
                        string deleteNhanVienQuery = "DELETE FROM NhanVien WHERE Id_NguoiDung = @Id_NguoiDung";
                        using (SqlCommand cmd = new SqlCommand(deleteNhanVienQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id_NguoiDung", idNguoiDung);
                            cmd.ExecuteNonQuery();
                        }

                        // Xóa thông tin trong bảng KhachHang nếu người dùng là khách hàng (customer)
                        string deleteKhachHangQuery = "DELETE FROM KhachHang WHERE Id_NguoiDung = @Id_NguoiDung";
                        using (SqlCommand cmd = new SqlCommand(deleteKhachHangQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id_NguoiDung", idNguoiDung);
                            cmd.ExecuteNonQuery();
                        }

                        // Xóa người dùng trong bảng NguoiDung
                        string deleteNguoiDungQuery = "DELETE FROM NguoiDung WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(deleteNguoiDungQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", idNguoiDung);
                            cmd.ExecuteNonQuery();
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
