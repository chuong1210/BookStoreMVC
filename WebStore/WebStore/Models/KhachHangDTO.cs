using System.ComponentModel;

namespace WebStore.Models
{
    public class KhachHangDTO
    {
        public string Id { get; set; }  // Id khách hàng
        [DisplayName("Tên khách hàng")]

        public string Ten { get; set; }  // Tên khách hàng
        [DisplayName("Địa chỉ khách hàng")]

        public string Diachi { get; set; }  // Địa chỉ
        [DisplayName("Số điện thoại")]

        public string Sodienthoai { get; set; }  // Số điện thoại

        public string Email { get; set; }  // Email
        public string IdNguoiDung { get; set; }
    }
}
