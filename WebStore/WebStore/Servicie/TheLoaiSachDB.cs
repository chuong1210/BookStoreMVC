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
    public class TheLoaiSachDB
    {
        public static List<BookType> GetAllType()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_GetAllType", value);
            List<BookType> lstResult = new List<BookType>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    BookType bookType = new BookType();
                    bookType.BookTypeID = dr["BookTypeID"].ToString();
                    bookType.BookTypeName = dr["BookTypeName"].ToString();

                    lstResult.Add(bookType);
                }
            }
            return lstResult;
        }
		public static List<BookType> ListTheLoai()
		{
			object[] value = { };
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_Book_GetAllType", value);
			List<BookType> lstResult = new List<BookType>();
			if (connection.errorCode == 0 && result.Rows.Count > 0)
			{
				foreach (DataRow dr in result.Rows)
				{
					BookType bookType = new BookType();
					bookType.BookTypeID = dr["id"].ToString();
					bookType.BookTypeName = dr["ten"].ToString();

					lstResult.Add(bookType);
				}
			}
			return lstResult;
		}
		public static BookType BookTypeWithID(string ID)
        {
            object[] value = { ID};
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Book_BookTypeWithID", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    BookType bookType = new BookType();
                    bookType.BookTypeID = dr["BookTypeID"].ToString();
                    bookType.BookTypeName = dr["BookTypeName"].ToString();
                    return bookType;
                }
            }
            return null;
        }	public static BookType LayThongTinTheLoai(string ID)
        {
            object[] value = { ID};
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
            DataTable result = connection.Select("SP_LayThongTinTheLoai", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    BookType bookType = new BookType();
                    bookType.BookTypeID = ID;
                    bookType.BookTypeName = dr["ten"].ToString();
                    return bookType;
                }
            }
            return null;
        }

    }
}
