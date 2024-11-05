using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WedStore.Models
{
    public class InfoOrder
    {
        public string InfoOrderID { get; set; }
        public string OrderID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
    }
}
