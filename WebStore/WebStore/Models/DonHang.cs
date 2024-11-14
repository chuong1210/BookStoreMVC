using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class DonHang
    {
        public string OrderID { get; set; }
        public string UserName { get; set; }
		public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }

		public decimal OrderPrice { get; set; }
        public int OrderStatus { get; set; }

    }
}
