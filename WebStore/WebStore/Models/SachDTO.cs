using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class SachDTO
    {
        public string BookID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sách")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại sách")]
        public string BookTypeID { get; set; }
        public string BookTypeName { get; set; }

        public string Author { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]

        public List<String> AuthorIds { get; set; }

        public string AuthorId
        {
            get
            {
                return AuthorIds != null && AuthorIds.Any() ? string.Join(",", AuthorIds) : string.Empty;
            }
        }
        [RegularExpression(@"^\d+$", ErrorMessage = "Năm xuất bản phải là số.")]

        //[Range(1000, 9999, ErrorMessage = "Năm xuất bản không hợp lệ.")]
        public string NamXuatBan { get; set; }

        public string Nxb { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn nhà xuất bản")]

        public string NxbId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng đã đặt")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng đã đặt phải lớn hơn 0")]
        public int OrderedQuantity { get; set; }


    }
}
