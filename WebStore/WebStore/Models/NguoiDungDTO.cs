using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public string RetypePassword { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; } = "Tân Phú";
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



		[PasswordMatch(ErrorMessage = "Mật khẩu và mật khẩu xác nhận phải giống nhau.")]
		public bool IsPasswordMatching { get; set; } 
	}


   
		public class PasswordMatchAttribute : ValidationAttribute
		{
			// Kiểm tra nếu Password và RetypePassword giống nhau
			public override bool IsValid(object value)
			{
				var obj = value as NguoiDungDTO;
				if (obj != null)
				{
					if (obj.Password != obj.RetypePassword)
					{
						// Nếu không giống nhau, trả về lỗi
						return false;
					}
				}
				return true;
			}

			public override string FormatErrorMessage(string name)
			{
				// Định nghĩa thông báo lỗi khi Password và RetypePassword không giống nhau
				return "Mật khẩu và mật khẩu xác nhận phải giống nhau.";
			}
		}
	

}
