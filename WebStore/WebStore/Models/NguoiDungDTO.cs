using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class NguoiDungDTO
    {
        [DisplayName("Tên tài khoản")]
        public string UserName { get; set; }
        [DisplayName("Id tài khoản")]


        public string idND { get; set; }

        [DisplayName("Mật khẩu")]

        public string Password { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Authority { get; set; }
        public string UserRole { get; set; }
        [DisplayName("Loại tài khoản")]

        public string TypeRole { get; set; }
        [DisplayName("Chức vụ")]

        public string ChucVu { get; set; }
        [DisplayName("Ngày sinh")]

        public DateTime? NgaySinh { get; set; }  // Thêm thuộc tính NgaySinh




    }
}
