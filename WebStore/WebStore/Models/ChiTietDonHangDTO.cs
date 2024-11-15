using System;

namespace WebStore.Models
{
    public class ChiTietDonHangDTO
    {
        public string OrderDetailId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public string StatusString { get; set; } = "Pending";



        public string OrderID { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
    
    }


}
