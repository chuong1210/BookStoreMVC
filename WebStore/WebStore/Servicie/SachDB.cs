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
                    book.BookID = dr["SachId"].ToString();
                    book.BookName = dr["TenSach"].ToString();
                    book.BookTypeName = dr["TenTheLoai"].ToString();
                    book.Author = dr["TenTacGias"].ToString();
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
                    book.BookID = dr["SachId"].ToString();
                    book.BookName = dr["TenSach"].ToString();
                    book.BookTypeName = dr["TenTheLoai"].ToString();
                    book.Author = dr["TenTacGias"].ToString();
                    book.BookTypeID = ID;

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
        public static SachDTO SachTheoId(string ID)
        {
            SachDTO book = null;
            using (SqlConnection connection = new SqlConnection(ConnectStringValue.ConnectStringMyDB))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_ThongTinSachDuocDatCuaKhTheoId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SachId", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read(); // Move to the first row
                                book = new SachDTO
                                {
                                    BookID = ID,
                                    BookName = reader["TenSach"].ToString(),
                                    BookTypeName = reader["TenTheLoai"].ToString(),
                                    BookTypeID = reader["theloai_id"].ToString(),
                                    Nxb = reader["TenNhaXuatBan"].ToString(),
                                    NamXuatBan = reader["NamXuatBan"].ToString(),
                                    NxbId = reader["NhaXuatBanId"].ToString(), // Use the correct column name
                                    Description = reader["MoTa"].ToString(),
                                    Image = reader["HinhAnh"].ToString(),
                                    Price = Convert.ToDecimal(reader["Gia"]),  // Correct conversion
                                    Quantity = Convert.ToInt32(reader["SoLuongTon"])
                                };


                                //Handles multiple authors.
                                if (reader.GetDataTypeName("TenTacGias") != null)
                                {
                                    string authorString = reader["TenTacGias"].ToString();
                                    string[] authors = authorString.Split(", "); // Split by comma and space
                                    book.Author = authorString;
                                }

                                if (reader.GetDataTypeName("TacGiaIds") != null)
                                {
                                    string authorIdsString = reader["TacGiaIds"].ToString();
                                    string[] authorIds = authorIdsString.Split(", "); // Split by comma and space
                                  //  book.AuthorId = authorIdsString;
                                    book.AuthorIds=authorIds.ToList();

                                }



                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions appropriately (logging, error handling)
                    Console.WriteLine($"Error: {ex.Message}");
                    // throw; // Consider re-throwing the exception if necessary
                }
            }
            return book;
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
        } public static bool ThemSach(SachDTO book)
        {
            object[] value = {  book.BookName, book.BookTypeID, book.Price, book.Quantity, book.Image, book.NamXuatBan, book.Description, book.NxbId, book.AuthorId };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
            DataTable result = connection.Select("SP_ThemSach", value);

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
			object[] value = { book.BookID, book.BookName, book.BookTypeID, book.Price, book.Quantity, book.Image,book.NamXuatBan,   book.Description, book.NxbId, book.AuthorId};
			SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
			DataTable result = connection.Select("SP_UpdateThongTinSach", value);

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
        }		public static bool XoaSach(string ID)
        {
            object[] value = { ID };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
            DataTable result = connection.Select("SP_XoaSach", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
    }
}
