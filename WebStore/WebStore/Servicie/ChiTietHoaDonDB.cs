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
 

		public static bool capnhatChiTietDonHang(ChiTietVatPhamDTO orderItem)
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




		public static ChiTietVatPhamDTO LayChiTietDonHangTheoIdCTDH(string ID)// tạo đơn hàng mới
		{
			object[] value = { ID };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_LayChiTietDonHangTheoID", value);

			if (connection.errorCode == 0 && connection.errorMessage == "")
			{
				foreach (DataRow dr in result.Rows)
				{
					ChiTietVatPhamDTO orderItem = new ChiTietVatPhamDTO();
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
	
     

		public static List<ChiTietVatPhamDTO> LayChiTietDonHangTheoDonHang(string OrderID)
		{
			object[] value = { OrderID };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_LayChiTietDonHangTheoDH", value);
			List<ChiTietVatPhamDTO> lstResult = new List<ChiTietVatPhamDTO>();
			if (connection.errorCode == 0 && connection.errorMessage == "")
			{
				foreach (DataRow dr in result.Rows)
				{
					ChiTietVatPhamDTO orderItem = new ChiTietVatPhamDTO();
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
