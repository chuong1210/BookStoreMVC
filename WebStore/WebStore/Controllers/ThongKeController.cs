using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using System.Data;
using System.Data.SqlClient;
using WebStore.Servicie;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using WedStore.Const;

namespace WebStore.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly ThongKeService _thongKeService = new ThongKeService(ConnectStringValue.ConnectStringMyDB);


      
        public IActionResult ThongKe(int thang, int nam)
        {
            // Gọi phương thức GetDoanhThuByMonth từ ThongKeService để lấy dữ liệu thống kê

            // Nếu không có thang và nam, mặc định lấy tháng và năm hiện tại
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            // Gọi service để lấy thống kê doanh thu
            var result = _thongKeService.GetDoanhThuByMonth(thang, nam);


            return View(result);






        }



        //private readonly string _connectionString = "Your_Connection_String";

        //// GET: ThongKe
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //// GET: ThongKe/DoanhThu
        //public IActionResult DoanhThu(int thang, int nam)
        //{
        //    var result = GetDoanhThuByMonth(thang, nam);
        //    return View(result);
        //}

        //public IActionResult ThongKe(int thang, int nam)
        //{
        //    var result = ThongKeService.GetDoanhThuByMonth(thang, nam);
        //    return View(result);
        //}


        //// Hàm gọi stored procedure và trả kết quả
        //public ThongKeDoanhThuResult GetDoanhThuByMonth(int thang, int nam)
        //{
        //    ThongKeDoanhThuResult result = new ThongKeDoanhThuResult();
        //    List<HoaDonDTO> hoaDons = new List<HoaDonDTO>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand("ThongKeDoanhThuTheoThang", connection))
        //        {
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@thang", thang);
        //            command.Parameters.AddWithValue("@nam", nam);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                // Lấy thông tin tổng doanh thu và số hóa đơn
        //                if (reader.Read())
        //                {
        //                    // Kiểm tra nếu cột không phải NULL
        //                    if (!reader.IsDBNull(reader.GetOrdinal("TongDoanhThu")))
        //                    {
        //                        result.TongDoanhThu = reader.GetDecimal(reader.GetOrdinal("TongDoanhThu"));
        //                    }
        //                    else
        //                    {
        //                        result.TongDoanhThu = 0;
        //                    }

        //                    result.SoHoaDon = reader.GetInt32(reader.GetOrdinal("SoHoaDon"));
        //                    result.Thang = reader.GetInt32(reader.GetOrdinal("Thang"));
        //                    result.Nam = reader.GetInt32(reader.GetOrdinal("Nam"));
        //                }

        //                // Lấy danh sách hóa đơn
        //                if (reader.NextResult())  // Chuyển sang kết quả tiếp theo (hoa don)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        HoaDonDTO hoaDon = new HoaDonDTO
        //                        {
        //                            Id = reader.GetString(reader.GetOrdinal("Id")),
        //                            DonHangId = reader.GetString(reader.GetOrdinal("DonHangId")),
        //                            NgayLap = reader.GetDateTime(reader.GetOrdinal("NgayLap")),
        //                            TongTien = reader.GetDecimal(reader.GetOrdinal("TongTien")),
        //                            PhuongThucTT = reader.GetString(reader.GetOrdinal("PhuongThucTT")),
        //                            TrangThaiTT = reader.GetString(reader.GetOrdinal("TrangThaiTT")),
        //                            Email = reader.GetString(reader.GetOrdinal("Email")),
        //                            SoDienThoai = reader.GetString(reader.GetOrdinal("SoDienThoai")),
        //                            DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
        //                            TenNguoiDatHang = reader.GetString(reader.GetOrdinal("TenNguoiDatHang"))
        //                        };
        //                        hoaDons.Add(hoaDon);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    result.HoaDons = hoaDons;
        //    return result;
        //}
    }
}
