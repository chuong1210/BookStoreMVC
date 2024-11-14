using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using WebStore.Models;
using WedStore.Const;
using System.Data;

namespace WedStore.Servicie
{
    public static class HoaDonDB
    {
        private readonly static string connectionString = ConnectStringValue.ConnectStringMyDB;

        public static List<HoaDonDTO> LayTatCaHoaDon()
        {
            List<HoaDonDTO> danhSachHoaDon = new List<HoaDonDTO>();

            string query = "SELECT * FROM HoaDon";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HoaDonDTO hoaDon = new HoaDonDTO
                            {
                                Id = reader["id"].ToString(),
                                DonHangId = reader["donhang_id"].ToString(),
                                NgayLap = Convert.ToDateTime(reader["ngayLap"]),
                                TongTien = Convert.ToDecimal(reader["tongTien"]),
                                PhuongThucTT = reader["phuongThucTT"].ToString(),
                                TrangThaiTT = reader["trangthaiTT"].ToString(),
                                Email = reader["email"].ToString(),
                                SoDienThoai = reader["sodienthoai"].ToString(),
                                DiaChi = reader["diachi"].ToString(),
                                TenNguoiDatHang = reader["tenNguoiDatHang"].ToString()
                            };
                            danhSachHoaDon.Add(hoaDon);
                        }
                    }
                }
            }
            return danhSachHoaDon;
        }



        public static List<HoaDonDTO> LayHoaDonTheoUserId(string userId)
        {
            List<HoaDonDTO> hoaDons = new List<HoaDonDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT hd.id, hd.donhang_id, hd.ngayLap, hd.tongTien, hd.phuongThucTT, 
                               hd.trangthaiTT, hd.email, hd.sodienthoai, hd.diachi, hd.tenNguoiDatHang
                        FROM HoaDon hd
                        INNER JOIN DonHang dh ON hd.donhang_id = dh.id
                        WHERE dh.nguoidung_id = @UserId"; // Lọc theo ID người dùng trong bảng DonHang

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);  // Truyền tham số người dùng

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HoaDonDTO hoaDon = new HoaDonDTO
                                {
                                    Id = reader["id"].ToString(),
                                    DonHangId = reader["donhang_id"].ToString(),
                                    NgayLap = Convert.ToDateTime(reader["ngayLap"]),
                                    TongTien = Convert.ToDecimal(reader["tongTien"]),
                                    PhuongThucTT = reader["phuongThucTT"].ToString(),
                                    TrangThaiTT = reader["trangthaiTT"].ToString(),
                                    Email = reader["email"].ToString(),
                                    SoDienThoai = reader["sodienthoai"].ToString(),
                                    DiaChi = reader["diachi"].ToString(),
                                    TenNguoiDatHang = reader["tenNguoiDatHang"].ToString()
                                };
                                hoaDons.Add(hoaDon);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có (ví dụ: logging)
                    Console.WriteLine(ex.Message);
                }
            }

            return hoaDons;
        }

        public static HoaDonDTO LayHoaDonTheoId(string id)
        {
            HoaDonDTO hoaDon = null;

            string query = "SELECT * FROM HoaDon WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hoaDon = new HoaDonDTO
                            {
                                Id = reader["id"].ToString(),
                                DonHangId = reader["donhang_id"].ToString(),
                                NgayLap = Convert.ToDateTime(reader["ngayLap"]),
                                TongTien = Convert.ToDecimal(reader["tongTien"]),
                                PhuongThucTT = reader["phuongThucTT"].ToString(),
                                TrangThaiTT = reader["trangthaiTT"].ToString(),
                                Email = reader["email"].ToString(),
                                SoDienThoai = reader["sodienthoai"].ToString(),
                                DiaChi = reader["diachi"].ToString(),
                                TenNguoiDatHang = reader["tenNguoiDatHang"].ToString()
                            };
                        }
                    }
                }
            }
            return hoaDon;
        }
        public static bool XoaHoaDonVaCapNhatDonHang(string hoaDonId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteHoaDonVaCapNhatDonHang", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@hoaDonId", hoaDonId);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu cần
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }





        public static bool CapNhatHoaDon(HoaDonDTO hoaDon)
        {
            string query = @"
        UPDATE HoaDon 
        SET donhang_id = @DonHangId,
            ngayLap = @NgayLap,
            tongTien = @TongTien,
            phuongThucTT = @PhuongThucTT,
            trangthaiTT = @TrangThaiTT,
            email = @Email,
            sodienthoai = @SoDienThoai,
            diachi = @DiaChi,
            tenNguoiDatHang = @TenNguoiDatHang
        WHERE id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", hoaDon.Id);
                    command.Parameters.AddWithValue("@DonHangId", hoaDon.DonHangId);
                    command.Parameters.AddWithValue("@NgayLap", DateTime.Now);
                    command.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                    command.Parameters.AddWithValue("@PhuongThucTT", hoaDon.PhuongThucTT);
                    command.Parameters.AddWithValue("@TrangThaiTT", hoaDon.TrangThaiTT);
                    command.Parameters.AddWithValue("@Email", hoaDon.Email);
                    command.Parameters.AddWithValue("@SoDienThoai", hoaDon.SoDienThoai);
                    command.Parameters.AddWithValue("@DiaChi", hoaDon.DiaChi);
                    command.Parameters.AddWithValue("@TenNguoiDatHang", hoaDon.TenNguoiDatHang);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public static bool ThanhToanDonHang(HoaDonDTO hoaDonDto)
          {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_ThanhToanDonHang", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DonHangId", hoaDonDto.DonHangId);

                        // Thêm các tham số vào command từ đối tượng hoaDonDto
                        command.Parameters.AddWithValue("@PhuongThucTT", hoaDonDto.PhuongThucTT.ToString());

                        command.Parameters.AddWithValue("@Email", hoaDonDto.Email);
                        command.Parameters.AddWithValue("@SoDienThoai", hoaDonDto.SoDienThoai);
                        command.Parameters.AddWithValue("@DiaChi", hoaDonDto.DiaChi);
                        command.Parameters.AddWithValue("@TenNguoiDatHang", hoaDonDto.TenNguoiDatHang);
                        command.Parameters.AddWithValue("@TongTien", hoaDonDto.TongTien);

             

                        // Mở kết nối và thực thi stored procedure
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Lấy giá trị tổng tiền sau khi thanh toán thành công
                        bool rs = HoaDonDB.CapNhatSoLuongTonKhiDatHangThanhCong(hoaDonDto.DonHangId);
                        if(rs)

                        return true; // Trả về true nếu thanh toán thành công
                        return false;


                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ghi log hoặc thông báo)
                Console.WriteLine(ex.Message);
                return false; // Trả về false nếu thanh toán thất bại
            }
        }

     

        public static bool CapNhatSoLuongTonKhiDatHangThanhCong(string donhangid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_CapNhatDonHang", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@donhang_id", donhangid);

                        // Thêm tham số đầu ra cho tổng tiền
                     
                        connection.Open();
                        command.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }    public static bool CapNhatTrangThaiHoaDon(string hoaDonId, string status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("Update HoaDon Set TrangThaiTT=@TrangThaiTT Where Id=@HoaDonId", connection))
                    {

                        command.Parameters.AddWithValue("@HoaDonId", hoaDonId);
                        command.Parameters.AddWithValue("@TrangThaiTT",status);

                        // Thêm tham số đầu ra cho tổng tiền
                     
                        connection.Open();
                        command.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }




}
