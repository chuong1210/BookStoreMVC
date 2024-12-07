using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebStore.Servicie
{
    public class ThongKeService
    {
        private readonly string _connectionString;

        public ThongKeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ThongKeDoanhThuResult GetDoanhThuByMonth(int thang, int nam)
        {
            ThongKeDoanhThuResult result = new ThongKeDoanhThuResult();
            List<HoaDonDTO> hoaDons = new List<HoaDonDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_ThongKeDoanhThuTheoThang", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@thang", thang);
                    command.Parameters.AddWithValue("@nam", nam);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Lấy thông tin tổng doanh thu và số hóa đơn
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("TongDoanhThu")))
                            {
                                result.TongDoanhThu = reader.GetDecimal(reader.GetOrdinal("TongDoanhThu"));
                            }
                            else
                            {
                                result.TongDoanhThu = 0;
                            }

                            result.SoHoaDon = reader.GetInt32(reader.GetOrdinal("SoHoaDon"));
                            result.Thang = reader.GetInt32(reader.GetOrdinal("Thang"));
                            result.Nam = reader.GetInt32(reader.GetOrdinal("Nam"));
                        }

                        // Lấy danh sách hóa đơn
                        if (reader.NextResult())  // Chuyển sang kết quả tiếp theo (hoa don)
                        {
                            while (reader.Read())
                            {
                                HoaDonDTO hoaDon = new HoaDonDTO
                                {
                                    Id = reader.GetString(reader.GetOrdinal("Id")),
                                    DonHangId = reader.GetString(reader.GetOrdinal("donhang_id")),
                                    NgayLap = reader.GetDateTime(reader.GetOrdinal("NgayLap")),
                                    TongTien = reader.GetDecimal(reader.GetOrdinal("TongTien")),
                                    PhuongThucTT = reader.GetString(reader.GetOrdinal("PhuongThucTT")),
                                    TrangThaiTT = reader.GetString(reader.GetOrdinal("TrangThaiTT")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    SoDienThoai = reader.GetString(reader.GetOrdinal("SoDienThoai")),
                                    DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                                    TenNguoiDatHang = reader.GetString(reader.GetOrdinal("TenNguoiDatHang"))
                                };
                                hoaDons.Add(hoaDon);
                            }
                        }
                    }
                }
            }

            result.HoaDons = hoaDons;
            return result;
        }


        //private readonly string _connectionString;

        //public ThongKeService(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        //public ThongKeDoanhThuResult GetThongKeDoanhThu(int thang, int nam)
        //{
        //    var result = new ThongKeDoanhThuResult();
        //    result.HoaDons = new List<HoaDonDTO>();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        var command = new SqlCommand("EXEC ThongKeDoanhThuTheoThang @Thang, @Nam", connection);
        //        command.Parameters.AddWithValue("@Thang", thang);
        //        command.Parameters.AddWithValue("@Nam", nam);

        //        var reader = command.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                var hoaDon = new HoaDonDTO
        //                {
        //                    Id = reader["Id"].ToString(),
        //                    TenNguoiDatHang = reader["TenNguoiDatHang"].ToString(),
        //                    NgayLap = Convert.ToDateTime(reader["NgayLap"]),
        //                    TongTien = Convert.ToDecimal(reader["TongTien"]),
        //                    PhuongThucTT = reader["PhuongThucTT"].ToString(),
        //                    TrangThaiTT = reader["TrangThaiTT"].ToString(),
        //                };
        //                result.HoaDons.Add(hoaDon);
        //            }

        //            // Lấy tổng doanh thu và số lượng hóa đơn
        //            result.TongDoanhThu = result.HoaDons.Sum(hd => hd.TongTien);
        //            result.SoHoaDon = result.HoaDons.Count();
        //            result.Thang = thang;
        //            result.Nam = nam;
        //        }
        //    }

        //    return result;
        //}
    }
}
