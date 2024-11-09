using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class NhaXuatBanDTO
    {
        [Required]
        public string IdNXB { get; set; }
        [DisplayName("Tên nhà xuất bản")]
        public string Ten { get; set; }
        [DisplayName("Địa chỉ")]

        public string DiaChi { get; set; }
        [DisplayName("Số điện thoại")]

        public string SoDienThoai { get; set; }
        [DisplayName("Email")]

        public string Email { get; set; }

    }
}
