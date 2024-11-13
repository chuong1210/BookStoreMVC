using System;

namespace WebStore.Models
{
    public class OrderDetails
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

    public class OrderDetail
    {
        public string DetailID { get; set; }
        public string BookTitle { get; set; }
        public string BookId { get; set; }
        public string BookDescription { get; set; }
        public string BookImage { get; set; }  // Đường dẫn đến ảnh sách
        public int SoLuong { get; set; }
        public decimal GiaDonVi { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
