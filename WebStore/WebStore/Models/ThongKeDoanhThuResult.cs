using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class ThongKeDoanhThuResult
    {
        public decimal TongDoanhThu { get; set; }  // Tổng doanh thu của tháng
        public int SoHoaDon { get; set; }  // Số lượng hóa đơn trong tháng
        public int Thang { get; set; }  // Tháng mà doanh thu được tính
        public int Nam { get; set; }  // Năm mà doanh thu được tính
        public List<HoaDonDTO> HoaDons { get; set; }  // Danh sách hóa đơn của tháng đó
    }
}
