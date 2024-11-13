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
        public static bool InfoOrder_create(OrderDetails infoOrder)// tạo đơn hàng mới
        {
            object[] value = { infoOrder.OrderDetailId, infoOrder.OrderID, infoOrder.Name,
                               infoOrder.Email, infoOrder.Phone,infoOrder. Address,
                               infoOrder.TotalPrice, infoOrder.Status };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("InfoOrder_Create", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static OrderDetails InfoOrder_GetInfoOrdersWithID(string id)
        {
            object[] value = { id };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("InfoOrder_GetInfoOrdersWithID", value);
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderDetails infoOrder = new OrderDetails();
                    infoOrder.OrderDetailId = dr["InfoOrderID"].ToString();
                    infoOrder.OrderID = dr["OrderID"].ToString();
                    infoOrder.Name = dr["Name"].ToString();
                    infoOrder.Email = dr["Email"].ToString();
                    infoOrder.Phone = dr["Phone"].ToString();
                    infoOrder.Address = dr["Address"].ToString();

                    infoOrder.TotalPrice = string.IsNullOrEmpty(dr["TotalPrice"].ToString()) ? 0 : Decimal.Parse(dr["TotalPrice"].ToString());
                    infoOrder.Status = string.IsNullOrEmpty(dr["Status"].ToString()) ? 0 : int.Parse(dr["Status"].ToString());
                    return infoOrder;
                }
            }
            return null;
        }
        public static OrderDetails InfoOrder_GetInfoOrdersWithOrderID(string id)
        {
            object[] value = { id };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("InfoOrder_GetInfoOrderWithOrderID", value);
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderDetails infoOrder = new OrderDetails();
                    infoOrder.OrderDetailId = dr["InfoOrderID"].ToString();
                    infoOrder.OrderID = dr["OrderID"].ToString();
                    infoOrder.Name = dr["Name"].ToString();
                    infoOrder.Email = dr["Email"].ToString();
                    infoOrder.Phone = dr["Phone"].ToString();
                    infoOrder.Address = dr["Address"].ToString();

                    infoOrder.TotalPrice = string.IsNullOrEmpty(dr["TotalPrice"].ToString()) ? 0 : Decimal.Parse(dr["TotalPrice"].ToString());
                    infoOrder.Status = string.IsNullOrEmpty(dr["Status"].ToString()) ? 0 : int.Parse(dr["Status"].ToString());
                    return infoOrder;
                }
            }
            return null;
        }

        public static List<OrderDetails> InfoOrder_GetInfoOrderWithEmail(string email)
        {
            object[] value = { email };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("InfoOrder_GetInfoOrderWithEmail", value);
            List<OrderDetails> lstInfoOrder = new List<OrderDetails>();
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderDetails infoOrder = new OrderDetails();
                    infoOrder.OrderDetailId = dr["InfoOrderID"].ToString();
                    infoOrder.OrderID = dr["OrderID"].ToString();
                    infoOrder.Name = dr["Name"].ToString();
                    infoOrder.Email = dr["Email"].ToString();
                    infoOrder.Phone = dr["Phone"].ToString();
                    infoOrder.Address = dr["Address"].ToString();

                    infoOrder.TotalPrice = string.IsNullOrEmpty(dr["TotalPrice"].ToString()) ? 0 : Decimal.Parse(dr["TotalPrice"].ToString());
                    infoOrder.Status = string.IsNullOrEmpty(dr["Status"].ToString()) ? 0 : int.Parse(dr["Status"].ToString());

                    lstInfoOrder.Add(infoOrder);
                }
            }
            return lstInfoOrder;
        }
        public static List<OrderDetails> InfoOrder_GetAll() {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("InfoOrder_GetAll", value);
            List<OrderDetails> lstInfoOrder = new List<OrderDetails>();
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderDetails infoOrder = new OrderDetails();
                    infoOrder.OrderDetailId = dr["InfoOrderID"].ToString();
                    infoOrder.OrderID = dr["OrderID"].ToString();
                    infoOrder.Name = dr["Name"].ToString();
                    infoOrder.Email = dr["Email"].ToString();
                    infoOrder.Phone = dr["Phone"].ToString();
                    infoOrder.Address = dr["Address"].ToString();

                    infoOrder.TotalPrice = string.IsNullOrEmpty(dr["TotalPrice"].ToString()) ? 0 : decimal.Parse(dr["TotalPrice"].ToString());
                    infoOrder.Status = string.IsNullOrEmpty(dr["Status"].ToString()) ? 0 : int.Parse(dr["Status"].ToString());

                    lstInfoOrder.Add(infoOrder);
                }
            }
            return lstInfoOrder;
        }
        public static bool InfoOrder_Delete(string InfoOrderID)
        {
            object[] value = { InfoOrderID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("InfoOrder_Delete", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }

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

        public static List<OrderDetail> LayChiTietDonHang(string orderId)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

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
                            OrderDetail detail = new OrderDetail
                            {
                                DetailID = reader["DetailID"].ToString(),
                                BookId = reader["sach_id"].ToString(),
                                BookTitle = reader["BookTitle"].ToString(),
                                SoLuong = Convert.ToInt32(reader["soLuong"]),
                                BookDescription = reader["BookDescription"].ToString(),
                                BookImage = reader["BookImage"].ToString(),
                                GiaDonVi = Convert.ToDecimal(reader["giaDonVi"]),
                                TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                            };

                            orderDetails.Add(detail);
                        }
                    }
                }
            }

            return orderDetails;
        }

        public static bool InfoOrder_Update(OrderDetails infoOrder)
        {
            object[] value = { infoOrder.OrderDetailId, infoOrder.OrderID, infoOrder.Name,
                               infoOrder.Email, infoOrder.Phone,infoOrder. Address,
                               infoOrder.TotalPrice, infoOrder.Status };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("InfoOrder_Update", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
    }
}
