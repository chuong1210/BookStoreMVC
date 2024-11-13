using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using WebStore.Models;
using WedStore.Const;

namespace WedStore.Servicie
{
    public class HoaDonDB
    {
        private readonly static string connectionString = ConnectStringValue.ConnectStringMyDB;

        public List<HoaDonDTO> LayTatCaHoaDon()
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
        public HoaDonDTO LayHoaDonTheoId(string id)
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
        public bool XoaHoaDon(string id)
        {
            string query = "DELETE FROM HoaDon WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool CapNhatHoaDon(HoaDonDTO hoaDon)
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
                    command.Parameters.AddWithValue("@NgayLap", hoaDon.NgayLap);
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

    }
}
