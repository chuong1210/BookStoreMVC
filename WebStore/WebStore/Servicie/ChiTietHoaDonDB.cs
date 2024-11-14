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
    public class ChiTietHoaDonDB
    {
        public static bool createOrderItem(OrderItem orderItem)// tạo đơn hàng mới
        {
            object[] value = { orderItem.ItemID, orderItem.OrderID, orderItem.BookID, 
                               orderItem.Quantity, orderItem.TotalPrice, orderItem.Discount };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("OrderItem_Create", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static bool updateOrderItem(OrderItem orderItem)
        {
            object[] value = { orderItem.ItemID, orderItem.OrderID, orderItem.BookID,
                               orderItem.Quantity, orderItem.TotalPrice, orderItem.Discount };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("OrderItem_Update", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }

		public static bool capnhatChiTietDonHang(OrderItem orderItem)
		{
			object[] value = { orderItem.ItemID, orderItem.OrderID, orderItem.BookID,
							   orderItem.Quantity, orderItem.Price };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_UpdateChiTietDonHang", value);

			if (connection.errorCode == 0 && connection.errorMessage == "")
			{
				return true;
			}
			return false;
		}
		public static bool deleteOrderItem(string ID)
        {
            object[] value = { ID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("OrderItem_Delete", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        	public static bool xoaChiTietDonHang(string ID)
        {
            object[] value = { ID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
            DataTable result = connection.Select("SP_DeleteChiTietDonHang", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }

        public static OrderItem GetOrderItemWithID(string ID)// tạo đơn hàng mới
        {
            object[] value = { ID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("OrderItem_GetOrderItemWithID", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ItemID = dr["ItemID"].ToString();
                    orderItem.OrderID = dr["OrderID"].ToString();
                    orderItem.BookID = dr["BookID"].ToString();
                    orderItem.Quantity = string.IsNullOrEmpty(dr["Quantity"].ToString()) ? 0 : int.Parse(dr["Quantity"].ToString());
                    orderItem.TotalPrice = string.IsNullOrEmpty(dr["TotalPrice"].ToString()) ? 0 : int.Parse(dr["TotalPrice"].ToString());
                    orderItem.Discount = string.IsNullOrEmpty(dr["Discount"].ToString()) ? 0 : int.Parse(dr["Discount"].ToString());
                    return orderItem;
                }
            }
            return null;
        }



		public static OrderItem LayChiTietDonHangTheoIdCTDH(string ID)// tạo đơn hàng mới
		{
			object[] value = { ID };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_LayChiTietDonHangTheoID", value);

			if (connection.errorCode == 0 && connection.errorMessage == "")
			{
				foreach (DataRow dr in result.Rows)
				{
					OrderItem orderItem = new OrderItem();
					orderItem.ItemID = dr["id"].ToString();
					orderItem.OrderID = dr["donhang_id"].ToString();
					orderItem.BookID = dr["sach_id"].ToString();
					orderItem.Quantity = string.IsNullOrEmpty(dr["soluong"].ToString()) ? 0 : int.Parse(dr["soluong"].ToString());
					orderItem.Price = string.IsNullOrEmpty(dr["GiaDonVi"].ToString()) ? 0 : Decimal.Parse(dr["GiaDonVi"].ToString());
					return orderItem;
				}
			}
			return null;
		}
		public static OrderItem GetOrderItemWithOrderIDBookID(string OrderID, string BookID)// tạo đơn hàng mới
        {
            object[] value = { OrderID, BookID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("OrderItem_GetOrderItemWithOrderIDBookID", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ItemID = dr["ItemID"].ToString();
                    orderItem.OrderID = dr["OrderID"].ToString();
                    orderItem.BookID = dr["BookID"].ToString();
                    orderItem.Quantity = string.IsNullOrEmpty(dr["Quantity"].ToString()) ? 0 : int.Parse(dr["Quantity"].ToString());
                    orderItem.TotalPrice = string.IsNullOrEmpty(dr["TotalPrice"].ToString()) ? 0 : int.Parse(dr["TotalPrice"].ToString());
                    orderItem.Discount = string.IsNullOrEmpty(dr["Discount"].ToString()) ? 0 : int.Parse(dr["Discount"].ToString());
                    return orderItem;
                }
            }
            return null;
        }
        public static List<OrderItem> GetOrderItemsWithOrderID(string OrderID)
        {
            object[] value = { OrderID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("OrderItem_GetOrderItemWithOrderID", value);
            List<OrderItem> lstResult = new List<OrderItem>();
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ItemID = dr["ItemID"].ToString();
                    orderItem.OrderID = dr["OrderID"].ToString();
                    orderItem.BookID = dr["BookID"].ToString();
                    orderItem.Quantity = string.IsNullOrEmpty(dr["Quantity"].ToString()) ? 0 : int.Parse(dr["Quantity"].ToString());
                    orderItem.TotalPrice = string.IsNullOrEmpty(dr["TotalPrice"].ToString()) ? 0 : int.Parse(dr["TotalPrice"].ToString());
                    orderItem.Discount = string.IsNullOrEmpty(dr["Discount"].ToString()) ? 0 : int.Parse(dr["Discount"].ToString());
                    lstResult.Add(orderItem);
                }
            }
            return lstResult;
        }


		public static List<OrderItem> LayChiTietDonHangTheoDonHang(string OrderID)
		{
			object[] value = { OrderID };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_LayChiTietDonHangTheoDH", value);
			List<OrderItem> lstResult = new List<OrderItem>();
			if (connection.errorCode == 0 && connection.errorMessage == "")
			{
				foreach (DataRow dr in result.Rows)
				{
					OrderItem orderItem = new OrderItem();
					orderItem.ItemID = dr["id"].ToString();
					orderItem.OrderID = dr["donhang_id"].ToString();
					orderItem.BookID = dr["sach_id"].ToString();
					orderItem.Quantity = string.IsNullOrEmpty(dr["soluong"].ToString()) ? 0 : int.Parse(dr["soluong"].ToString());
					orderItem.Price = string.IsNullOrEmpty(dr["GiaDonVi"].ToString()) ? 0 : Decimal.Parse(dr["GiaDonVi"].ToString());
                    //	orderItem.Discount = string.IsNullOrEmpty(dr["Discount"].ToString()) ? 0 : int.Parse(dr["Discount"].ToString());
                    SachDTO s = new SachDTO();
                    s= SachDB.SachTheoId(orderItem.BookID);
                    orderItem.BookTitle=s.BookName;
                    orderItem.BookImage=s.Image;
                    orderItem.BookDescription = s.Description;

                    lstResult.Add(orderItem);
				}
			}
			return lstResult;
		}
	}
}
