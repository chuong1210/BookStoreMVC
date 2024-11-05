using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class NguoiDungDTO
    {
        public string UserName { get; set; }

		public string idND { get; set; }


		public string Password { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Authority { get; set; }
        public string UserRole { get; set; }



    }
}
