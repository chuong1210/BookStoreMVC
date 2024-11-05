using CAIT.SQLHelper;
using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
