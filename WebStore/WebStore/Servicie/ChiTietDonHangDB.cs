using CAIT.SQLHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WedStore.Const;
using WebStore.Models;
namespace WedStore.Repositories
{
    public class ChiTietDonHangDB
    {
    
   
		public static decimal TongTienDH(string idOrder)
		{


			using (SqlConnection connection = new SqlConnection(ConnectStringValue.ConnectStringMyDB))
			{
				connection.Open();

				// Tạo câu lệnh truy vấn gọi hàm SQL
				string query = "SELECT dbo.fn_TongTienDonHang(@donhang_id)";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					// Thêm tham số cho hàm
					command.Parameters.AddWithValue("@donhang_id", idOrder);

					// Thực thi câu lệnh và lấy kết quả
					object result = command.ExecuteScalar();

					if (result != null)
					{
						decimal totalAmount = Convert.ToDecimal(result);
						return totalAmount;
					}
					else
					{
						return -1;
					}
				}
			}
		}

        public static List<ChiTietVatPhamDTO> LayChiTietDonHang(string orderId)
        {
            List<ChiTietVatPhamDTO> orderDetails = new List<ChiTietVatPhamDTO>();

            // Mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(ConnectStringValue.ConnectStringMyDB))
            {
                connection.Open();

                string query = @"
        SELECT 
            ctdh.id AS DetailID,
            ctdh.sach_id,
            s.tieude AS BookTitle,
                s.moTa AS BookDescription,
                s.HinhAnh AS BookImage,  -- Lấy ảnh sách từ cột 'anh'
            ctdh.soLuong,
            ctdh.giaDonVi,
            (ctdh.soLuong * ctdh.giaDonVi) AS TotalPrice
        FROM ChiTietDonHang ctdh
        JOIN Sach s ON ctdh.sach_id = s.id
        WHERE ctdh.donhang_id = @OrderID;
        ";

                // Thực thi truy vấn và lấy dữ liệu
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Thêm tham số OrderID vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChiTietVatPhamDTO detail = new ChiTietVatPhamDTO
                            {
                                ItemID = reader["DetailID"].ToString(),
                                BookID = reader["sach_id"].ToString(),
                                BookTitle = reader["BookTitle"].ToString(),
                                Quantity = Convert.ToInt32(reader["soLuong"]),
                                BookDescription = reader["BookDescription"].ToString(),
                                BookImage = reader["BookImage"].ToString(),
                                Price = Convert.ToDecimal(reader["giaDonVi"]),
                                TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                            };

                            orderDetails.Add(detail);
                        }
                    }
                }
            }

            return orderDetails;
        }


    }
}
