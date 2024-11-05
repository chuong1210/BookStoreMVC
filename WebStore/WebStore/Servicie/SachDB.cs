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
    public class SachDB
    {
        public static List<SachDTO> GetAll()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_GetAll", value);
            List<SachDTO> lstResult = new List<SachDTO>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    SachDTO book = new SachDTO();
                    book.BookID = dr["BookID"].ToString();
                    book.BookName = dr["BookName"].ToString();
                    book.BookTypeID = dr["BookTypeID"].ToString();
                    book.Author = dr["Author"].ToString();
                    book.Nxb = dr["Nxb"].ToString();
                    book.Description = dr["Description"].ToString();
                    book.Image = dr["Image"].ToString();

                    book.Price = string.IsNullOrEmpty(dr["Price"].ToString()) ? 0 : int.Parse(dr["Price"].ToString());
                    book.Quantity = string.IsNullOrEmpty(dr["Quantity"].ToString()) ? 0 : int.Parse(dr["Quantity"].ToString());
                    book.OrderedQuantity = string.IsNullOrEmpty(dr["OrderedQuantity"].ToString()) ? 0 : int.Parse(dr["OrderedQuantity"].ToString());

                    lstResult.Add(book);
                }
            }
            return lstResult;
        }

        public static List<SachDTO> LayTatCaSach()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
            DataTable result = connection.Select("SP_DanhSachSach", value);
            List<SachDTO> lstResult = new List<SachDTO>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    SachDTO book = new SachDTO();
                    book.BookID = dr["id"].ToString();
                    book.BookName = dr["TenSach"].ToString();
                    book.BookTypeName = dr["TenTheLoai"].ToString();
                    book.Author = dr["TenTacGia"].ToString();
                    book.BookTypeID = dr["theloai_id"].ToString();

                    book.Nxb = dr["TenNhaXuatBan"].ToString();
                    book.Description = dr["MoTa"].ToString();
                    book.Image = dr["HinhAnh"].ToString();

                    book.Price = string.IsNullOrEmpty(dr["Gia"].ToString()) ? 0 : Decimal.Parse(dr["Gia"].ToString());
                    book.Quantity = string.IsNullOrEmpty(dr["SoLuongTon"].ToString()) ? 0 : int.Parse(dr["SoLuongTon"].ToString());
                    lstResult.Add(book);
                }
            }
            return lstResult;
        }
        public static List<SachDTO> GetBookWithSelling()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_BookWithSelling", value);
            List<SachDTO> lstResult = new List<SachDTO>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    SachDTO book = new SachDTO();
                    book.BookID = dr["BookID"].ToString();
                    book.BookName = dr["BookName"].ToString();
                    book.BookTypeID = dr["BookTypeID"].ToString();
                    book.Author = dr["Author"].ToString();
                    book.Nxb = dr["Nxb"].ToString();
                    book.Description = dr["Description"].ToString();
                    book.Image = dr["Image"].ToString();

                    book.Price = string.IsNullOrEmpty(dr["Price"].ToString()) ? 0 : int.Parse(dr["Price"].ToString());
                    book.Quantity = string.IsNullOrEmpty(dr["Quantity"].ToString()) ? 0 : int.Parse(dr["Quantity"].ToString());
                    book.OrderedQuantity = string.IsNullOrEmpty(dr["OrderedQuantity"].ToString()) ? 0 : int.Parse(dr["OrderedQuantity"].ToString());

                    lstResult.Add(book);
                }
            }
            return lstResult;
        }

        public static List<SachDTO> GetTypeList(string ID)//type ID
        {
            object[] value = { ID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_GetTypeList", value);
            List<SachDTO> lstResult = new List<SachDTO>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    SachDTO book = new SachDTO();
                    book.BookID = dr["BookID"].ToString();
                    book.BookName = dr["BookName"].ToString();
                    book.BookTypeID = dr["BookTypeID"].ToString();
                    book.Author = dr["Author"].ToString();
                    book.Nxb = dr["Nxb"].ToString();
                    book.Description = dr["Description"].ToString();
                    book.Image = dr["Image"].ToString();

                    book.Price = string.IsNullOrEmpty(dr["Price"].ToString()) ? 0 : int.Parse(dr["Price"].ToString());
                    book.Quantity = string.IsNullOrEmpty(dr["Quantity"].ToString()) ? 0 : int.Parse(dr["Quantity"].ToString());
                    book.OrderedQuantity = string.IsNullOrEmpty(dr["OrderedQuantity"].ToString()) ? 0 : int.Parse(dr["OrderedQuantity"].ToString());

                    lstResult.Add(book);
                }
            }
            return lstResult;
        }



        public static List<SachDTO> LaySachTheoTheLoai(string ID)//type ID
        {
            object[] value = { ID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
            DataTable result = connection.Select("SP_DanhSachSachTheoTheLoai", value);
            List<SachDTO> lstResult = new List<SachDTO>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    SachDTO book = new SachDTO();
                    book.BookID = ID;
                    book.BookName = dr["TenSach"].ToString();
                    book.BookTypeName = dr["TenTheLoai"].ToString();
                    book.Author = dr["TenTacGia"].ToString();
                    book.BookTypeID = dr["theloai_id"].ToString();

                    book.Nxb = dr["TenNhaXuatBan"].ToString();
                    book.Description = dr["MoTa"].ToString();
                    book.Image = dr["HinhAnh"].ToString();

                    book.Price = string.IsNullOrEmpty(dr["Gia"].ToString()) ? 0 : Decimal.Parse(dr["Gia"].ToString());
                    book.Quantity = string.IsNullOrEmpty(dr["SoLuongTon"].ToString()) ? 0 : int.Parse(dr["SoLuongTon"].ToString());
                    lstResult.Add( book);
                }
            }
            return lstResult;
        }

        public static SachDTO BookWithID(string ID)//type ID
        {
            object[] value = { ID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_BookWithID", value);
            //List<Book> lstResult = new List<Book>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    SachDTO book = new SachDTO();
                    book.BookID = dr["BookID"].ToString();
                    book.BookName = dr["BookName"].ToString();
                    book.BookTypeID = dr["BookTypeID"].ToString();
                    book.Author = dr["Author"].ToString();
                    book.Nxb = dr["Nxb"].ToString();
                    book.Description = dr["Description"].ToString();
                    book.Image = dr["Image"].ToString();

                    book.Price = string.IsNullOrEmpty(dr["Price"].ToString()) ? 0 : int.Parse(dr["Price"].ToString());
                    book.Quantity = string.IsNullOrEmpty(dr["Quantity"].ToString()) ? 0 : int.Parse(dr["Quantity"].ToString());
                    book.OrderedQuantity = string.IsNullOrEmpty(dr["OrderedQuantity"].ToString()) ? 0 : int.Parse(dr["OrderedQuantity"].ToString());
                    return book;
                    //lstResult.Add(book);
                }
            }
            return null;
        }

		public static SachDTO SachTheoId(string ID)//type ID
		{
			object[] value = { ID };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_ThongTinSachDuocDatCuaKhTheoId", value);
			//List<Book> lstResult = new List<Book>();
			if (connection.errorCode == 0 && result.Rows.Count > 0)
			{
				foreach (DataRow dr in result.Rows)
				{
					SachDTO book = new SachDTO();
					book.BookID = ID;
					book.BookName = dr["TenSach"].ToString();
					book.BookTypeName = dr["TenTheLoai"].ToString();
					book.Author = dr["TenTacGia"].ToString();
					book.Nxb = dr["TenNhaXuatBan"].ToString();
					book.Description = dr["MoTa"].ToString();
					book.Image = dr["HinhAnh"].ToString();

					book.Price = string.IsNullOrEmpty(dr["Gia"].ToString()) ? 0 : Decimal.Parse(dr["Gia"].ToString());
					book.Quantity = string.IsNullOrEmpty(dr["SoLuongTon"].ToString()) ? 0 : int.Parse(dr["SoLuongTon"].ToString());
					return book;
					//lstResult.Add(book);
				}
			}
			return null;
		}
		public static int Book_Count()
        {
            return GetAll().Count;
        }
        public static bool Book_CreateBook(SachDTO book)
        {
            object[] value = { book.BookID,book.BookName, book.BookTypeID, book.Author, book.Nxb, book.Description, book.Image, book.Price, book.Quantity, book.OrderedQuantity };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_CreateBook", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static bool Book_Update(SachDTO book)
        {
            object[] value = { book.BookID, book.BookName, book.BookTypeID, book.Author, book.Nxb, book.Description, book.Image, book.Price, book.Quantity, book.OrderedQuantity };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_Update", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
		public static bool CapNhat_Sach(SachDTO book)
		{
			object[] value = { book.BookID, book.BookName, book.BookTypeID, book.Author, book.Nxb, book.Description, book.Image, book.Price, book.Quantity, book.OrderedQuantity };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_UpdateSach", value);

			if (connection.errorCode == 0 && connection.errorMessage == "")
			{
				return true;
			}
			return false;
		}
		public static bool Book_Delete(string ID)
        {
            object[] value = { ID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_Delete", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
    }
}
