using CAIT.SQLHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WedStore.Const;

namespace WedStore.Repositories
{
    public class DonHangDB
    {
        private readonly static string connectionString = ConnectStringValue.ConnectStringMyDB;
        public static bool createOrdersOld(Orders orders)
        {
            object[] value = { orders.OrderID, orders.UserName, orders.OrderPrice, orders.OrderStatus };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Orders_Create", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }

        public static bool createOrders(Orders orders)
        {
            object[] value = { orders.OrderID, orders.UserName, orders.OrderPrice, orders.OrderStatus };
            SQLCommand connection = new SQLCommand(connectionString);
            DataTable result = connection.Select("SP_TaoDonHang", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static List<Orders> GetOrdersUser(string userName) 
        {
            object[] value = { userName };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Orders_GetOrdersUser", value);
            List<Orders> lstResult = new List<Orders>();
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    Orders orders = new Orders();
                    orders.OrderID = dr["OrderID"].ToString();
                    orders.UserName = dr["UserName"].ToString();
                    orders.OrderPrice = string.IsNullOrEmpty(dr["OrderPrice"].ToString()) ? 0 : int.Parse(dr["OrderPrice"].ToString());
                    orders.OrderStatus = string.IsNullOrEmpty(dr["OrderStatus"].ToString()) ? 0 : int.Parse(dr["OrderStatus"].ToString());
                    lstResult.Add(orders);
                }
            }
            return lstResult;
        }
        public static Orders GetOrdersUserOnStatus(string userName,int status)
        {
            object[] value = { userName, status };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Orders_GetOrdersUserOnStatus", value);
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    Orders orders = new Orders();
                    orders.OrderID = dr["OrderID"].ToString();
                    orders.UserName = dr["UserName"].ToString();
                    orders.OrderPrice = string.IsNullOrEmpty(dr["OrderPrice"].ToString()) ? 0 : int.Parse(dr["OrderPrice"].ToString());
                    orders.OrderStatus = string.IsNullOrEmpty(dr["OrderStatus"].ToString()) ? 0 : int.Parse(dr["OrderStatus"].ToString());
                    return orders;
                }
            }
            return null;
        }

		public static Orders LayDanhSachOrderTheoTrangThai(string idKH, int status)
		{
			object[] value = { status, idKH };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_LayTrangThaiDonHang", value);
			if (connection.errorCode == 0 && connection.errorMessage == "")
			{
				foreach (DataRow dr in result.Rows)
				{
					Orders orders = new Orders();
					orders.OrderID = dr["id"].ToString();
					orders.UserId = dr["nguoidung_id"].ToString();
					orders.OrderPrice = string.IsNullOrEmpty(dr["TongTien"].ToString()) ? 0 : decimal.Parse(dr["TongTien"].ToString());
					orders.OrderStatus = string.IsNullOrEmpty(dr["trangthaiDH"].ToString()) ? 0 : int.Parse(dr["trangthaiDH"].ToString());
					return orders;
				}
			}
			return null;
		}
		public static Orders GetOrdersWithID(string id) 
        {
            object[] value = { id };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Orders_GetOrdersWithID", value);
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    Orders orders = new Orders();
                    orders.OrderID = dr["OrderID"].ToString();
                    orders.UserName = dr["UserName"].ToString();
                    orders.OrderPrice = string.IsNullOrEmpty(dr["OrderPrice"].ToString()) ? 0 : int.Parse(dr["OrderPrice"].ToString());
                    orders.OrderStatus = string.IsNullOrEmpty(dr["OrderStatus"].ToString()) ? 0 : int.Parse(dr["OrderStatus"].ToString());
                    return orders;
                }
            }
            return null;
        }

        public static OrderDetails LayThongTinDonHangTheoId(string orderId)
        {
            // Tạo đối tượng kết nối SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Câu lệnh SQL để lấy thông tin đơn hàng và thông tin khách hàng
                string query = @"
            SELECT dh.id AS OrderId, 
                   -- Lựa chọn tên và email từ Khách hàng hoặc Nhân viên
                   CASE 
                       WHEN kh.id_NguoiDung IS NOT NULL THEN kh.ten  -- Nếu là khách hàng, lấy tên khách hàng
                       ELSE nv.ten  -- Nếu là nhân viên, lấy tên nhân viên
                   END AS Name,
                   CASE 
                       WHEN kh.id_NguoiDung IS NOT NULL THEN kh.email  -- Nếu là khách hàng, lấy email khách hàng
                       ELSE nv.email  -- Nếu là nhân viên, lấy email nhân viên
                   END AS Email,
                   -- Số điện thoại và địa chỉ có thể tùy chỉnh
                   CASE 
                       WHEN kh.id_NguoiDung IS NOT NULL THEN kh.sodienthoai  -- Nếu là khách hàng, lấy số điện thoại khách hàng
                       ELSE nv.sodienthoai  -- Nếu là nhân viên, lấy số điện thoại nhân viên
                   END AS Phone,
                   CASE 
                       WHEN kh.id_NguoiDung IS NOT NULL THEN kh.diachi  -- Nếu là khách hàng, lấy địa chỉ khách hàng
                       ELSE null  -- Nếu là nhân viên, lấy địa chỉ nhân viên (nếu có)
                   END AS Address,
                   dh.tongTien AS TotalPrice, 
                   CASE 
                       WHEN dh.trangthaiDH = 0 THEN 'Pending'
                       WHEN dh.trangthaiDH = 1 THEN 'Completed'
                       ELSE 'Unknown'
                   END AS StatusString
            FROM DonHang dh
            LEFT JOIN Khachhang kh ON dh.nguoidung_id = kh.id_NguoiDung
            LEFT JOIN NhanVien nv ON dh.nguoidung_id = nv.id_NguoiDung
            WHERE dh.id = @orderId";

                // Sử dụng SqlCommand để thực thi câu truy vấn
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@orderId", orderId);

                // Mở kết nối
                connection.Open();

                // Thực thi truy vấn và lấy kết quả
                SqlDataReader reader = cmd.ExecuteReader();

                // Kiểm tra kết quả truy vấn
                if (reader.Read())
                {
                    return new OrderDetails
                    {
                        OrderID = reader["OrderId"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                        StatusString = reader["StatusString"].ToString()
                    };
                }
            }

            return null;  // Trả về null nếu không tìm thấy đơn hàng
        }

        public static bool checkOrders(string userName)// kiểm tra khách hàng có đơn hàng sẵn sàng chưa nếu chưa thì tạo mới
        {
            /*khách hàng chưa có đơn hàng nào
            kiểm tra khách hàng đã có đơn hàng nào chưa
            */
            /*status = 1: đã có đơn hàng sẵn sàng
            =>kiểm tra khách hàng có đơn hàng sẵn sàng không*/
            Orders orders = GetOrdersUserOnStatus(userName, 1);
            if(orders == null)
            {
                Orders orders1 = new Orders();

                Random rnd = new Random();
                int id = rnd.Next(100000,999999);
                while (GetOrdersWithID(id.ToString()) != null)
                {
                    id = rnd.Next(100000, 999999);
                }
                orders1.OrderID = id.ToString();
                orders1.UserName = userName;
                orders1.OrderPrice = 0;
                orders1.OrderStatus = 1;//1: đơn hàng sẵn sáng
                createOrders(orders1);
                return true;
            }

            return true;
        }
        public static bool Orders_Update(Orders orders)
        {
            object[] value = { orders.OrderID, orders.UserName,orders.OrderPrice,orders.OrderStatus};
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Orders_Update", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }

		public static bool Capnhat_DH(Orders orders)
		{
			object[] value = { orders.OrderID, orders.UserId,  orders.OrderStatus, orders.OrderPrice };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
			DataTable result = connection.Select("SP_UpdateDonHang", value);

			if (connection.errorCode == 0 && connection.errorMessage == "")
			{
				return true;
			}
			return false;
		}
		public static bool Orders_Delete(string OrderID)
        {
            object[] value = { OrderID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Orders_Delete", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }






            public static bool AddToCart(string nguoiDungId, string sachId, int soLuong)
            {
                // Kiểm tra đầu vào hợp lệ
                if (string.IsNullOrEmpty(nguoiDungId) || string.IsNullOrEmpty(sachId) || soLuong <= 0)
                {
                    return false;
                }

                SQLCommand connection = new SQLCommand(connectionString);
                DataTable result;
                Orders existingOrder = LayDanhSachOrderTheoTrangThai(nguoiDungId, 0); // Tìm đơn hàng có trạng thái 0

                if (existingOrder == null)
                {
                    // Nếu chưa có đơn hàng trạng thái = 0, tạo mới đơn hàng
                    Orders newOrder = new Orders
                    {
                        OrderID = Guid.NewGuid().ToString(),
                        UserId = nguoiDungId,
                        OrderPrice = 0,
                        OrderStatus = 0 // Đơn hàng chưa thanh toán
                    };

                    // Tạo đơn hàng mới
                    if (!createOrders(newOrder))
                    {
                        return false;
                    }

                    existingOrder = newOrder;
                }

                // Kiểm tra chi tiết đơn hàng đã tồn tại với sách này chưa
                object[] checkDetailParams = { existingOrder.OrderID, sachId };

            result = connection.Select("SP_LayChiTietDonHangTheoSachId", checkDetailParams);

                if (connection.errorCode == 0 && connection.errorMessage == "")
                {
                    if (result.Rows.Count > 0)
                    {
                        // Đã tồn tại chi tiết đơn hàng, cập nhật số lượng
                        DataRow row = result.Rows[0];
                        string chiTietId = row["Id"].ToString();
                        string donHangId = row["donhang_id"].ToString();
                        string sachid = row["sach_id"].ToString();
                        string soluong = row["soluong"].ToString();
                        string giadonvi = row["giadonvi"].ToString();
                        int existingSoLuong = Convert.ToInt32(row["soLuong"]);

                        // Tăng số lượng
                        object[] updateDetailParams = { chiTietId,donHangId,sachId, existingSoLuong + soLuong, giadonvi };
                        connection.ExecuteData("SP_UpdateChiTietDonHang", updateDetailParams);
                    }
                    else
                    {
                        // Thêm chi tiết đơn hàng mới nếu chưa tồn tại
                        object[] insertDetailParams = { existingOrder.OrderID, sachId, soLuong, GetBookPrice(sachId) };
                        connection.ExecuteData("SP_TaoChiTietDonHang", insertDetailParams);
                    }
                }

                // Cập nhật tổng tiền của đơn hàng
                UpdateOrderTotalPrice(existingOrder.OrderID);

                return true;
            }

            // Hàm lấy giá sách từ bảng Sach
            private static decimal GetBookPrice(string sachId)
            {
                SQLCommand connection = new SQLCommand(connectionString);
                object[] value = { sachId };
                DataTable result = connection.Select("SELECT gia FROM Sach WHERE id = @SachId", value);
                if (connection.errorCode == 0 && connection.errorMessage == "" && result.Rows.Count > 0)
                {
                    return Convert.ToDecimal(result.Rows[0]["gia"]);
                }
                return 0;
            }

        // Hàm cập nhật tổng tiền của đơn hàng
        private static void UpdateOrderTotalPrice(string orderId)
        {
            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Truy vấn tổng tiền của đơn hàng từ ChiTietDonHang
                string query = @"
        SELECT SUM(cd.soLuong * cd.giaDonVi) AS TotalPrice
        FROM ChiTietDonHang cd
        WHERE cd.donhang_id = @OrderID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    // Sử dụng DataSet để lấy kết quả
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            // Lấy tổng tiền từ kết quả
                            decimal totalPrice = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["TotalPrice"]);

                            // Cập nhật tổng tiền cho đơn hàng
                            string updateQuery = @"
                    UPDATE DonHang
                    SET tongTien = @TotalPrice
                    WHERE Id = @OrderID";

                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                            {
                                // Thêm tham số vào câu lệnh UPDATE
                                updateCmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                                updateCmd.Parameters.AddWithValue("@OrderID", orderId);

                                // Thực thi câu lệnh UPDATE
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        public static List<OrderDetails> LayDanhSachDonHangVoiThongTinND()
        {
            List<OrderDetails> orders = new List<OrderDetails>();

            // Mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"

  SELECT 
      dh.id AS OrderID,
      dh.nguoidung_id,
      dh.trangthaiDH,
      dh.ngayDatHang,
      dh.tongTien,
      CASE
          WHEN k.id IS NOT NULL THEN k.ten -- Nếu là khách hàng
          WHEN nv.id IS NOT NULL THEN nv.ten -- Nếu là nhân viên
          ELSE 'Unknown' -- Nếu không phải khách hàng hoặc nhân viên
      END AS CustomerOrStaffName,
      CASE
          WHEN k.email IS NOT NULL THEN k.email -- Nếu là khách hàng
          WHEN nv.email IS NOT NULL THEN nv.email -- Nếu là nhân viên
          ELSE 'No Email' -- Nếu không có email
      END AS CustomerOrStaffEmail
  FROM DonHang dh
  LEFT JOIN Khachhang k ON dh.nguoidung_id = k.id_NguoiDung
  LEFT JOIN NhanVien nv ON dh.nguoidung_id = nv.id_NguoiDung
  ORDER BY dh.ngayDatHang DESC;
        ";

                // Thực thi truy vấn và lấy dữ liệu
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderDetails order = new OrderDetails
                            {
                                OrderID = reader["OrderID"].ToString(),
                                UserId = reader["nguoidung_id"].ToString(),
                                Status = Convert.ToInt32(reader["trangthaiDH"]),
                                OrderDate = Convert.ToDateTime(reader["ngayDatHang"]),
                                TotalPrice = Convert.ToDecimal(reader["tongTien"]),
                                Name = reader["CustomerOrStaffName"].ToString(),
                                Email = reader["CustomerOrStaffEmail"].ToString()
                            };

                            orders.Add(order);
                        }
                    }
                }
            }

            return orders;
        }


        public static bool CapNhatTrangThaiDonHang(string orderId, int status)
        {
            // Kiểm tra giá trị status có hợp lệ không
            if (status != 0 && status != 1)
            {
                throw new ArgumentException("Trạng thái chỉ có thể là 0 (Pending) hoặc 1 (Completed).");
            }

            // Câu lệnh SQL để cập nhật trạng thái đơn hàng
            string query = "UPDATE DonHang SET trangthaiDH = @status WHERE id = @orderId";

            // Tạo đối tượng kết nối và câu lệnh SQL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số vào câu lệnh
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@orderId", orderId);

                    // Mở kết nối
                    connection.Open();

                    // Thực thi câu lệnh cập nhật và kiểm tra số dòng bị ảnh hưởng
                    int rowsAffected = command.ExecuteNonQuery();

                    // Đóng kết nối
                    connection.Close();

                    // Trả về true nếu cập nhật thành công
                    return rowsAffected > 0;
                }
            }
        }

    }


}



