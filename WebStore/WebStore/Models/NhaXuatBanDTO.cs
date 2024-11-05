using System.ComponentModel.DataAnnotations;

namespace WedStore.Models
{
    public class NhaXuatBanDTO
    {
        [Required]
        public string IdNXB { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

    }
}
