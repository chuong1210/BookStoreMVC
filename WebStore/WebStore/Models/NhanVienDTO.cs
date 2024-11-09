using System.ComponentModel;

namespace WebStore.Models
{
    public class NhanVienDTO
    {
        public string Id { get; set; }  // Id nhân viên
        [DisplayName("Tên nhân viên ")]

        public string Ten { get; set; }  // Tên nhân viên
        [DisplayName("Chức vụ ")]

        public string ChucVu { get; set; }  // Chức vụ
        [DisplayName("Số điện thoại ")]

        public string Sodienthoai { get; set; }  // Số điện thoại

        public string Email { get; set; }  // Email
        public string IdNguoiDung { get; set; }  // Id của tài khoản người dùng (foreign key)
    }
}
