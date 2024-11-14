using System;
using WebStore.Constraint;

namespace WebStore.Models
{
    public class HoaDonDTO
    {
      
            public string Id { get; set; }
            public string DonHangId { get; set; }
            public DateTime NgayLap { get; set; }
            public decimal TongTien { get; set; }
             public string PhuongThucTT { get; set; } // Đổi từ string sang enum
             public string TrangThaiTT { get; set; }
            public string Email { get; set; }
            public string SoDienThoai { get; set; }
            public string DiaChi { get; set; }
            public string TenNguoiDatHang { get; set; }

    }
}
