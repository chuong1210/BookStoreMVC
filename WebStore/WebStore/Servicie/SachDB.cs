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
        public static List<SachDTO> LaySachTheoSoLuongTon()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectStringMyDB);
            DataTable result = connection.Select("SP_SapXepSachTheoSoLuongTon", value);
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


        public static List<SachDTO> LaySachTheoOrderId(string orderId)
        {
            List<SachDTO> books = new List<SachDTO>();

            using (SqlConnection connection = new SqlConnection(ConnectStringValue.ConnectStringMyDB))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_LaySachTheoIdDonHang", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.VarChar)).Value = orderId;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SachDTO bookDetails = new SachDTO
                            {
                                BookID = reader["SachId"].ToString(),
                                BookName = reader["TenSach"].ToString(),
                                Image = reader["HinhAnh"].ToString(),
                                Price = Convert.ToDecimal(reader["Gia"]),
                                OrderedQuantity = Convert.ToInt32(reader["SoLuongDat"]),
                                NamXuatBan = reader["NamXuatBan"].ToString(),
                                Description = reader["MoTa"].ToString(),
                                BookTypeName = reader["TenTheLoai"].ToString(),
                                Author = reader["TenTacGias"].ToString(),
                                Nxb = reader["TenNhaXuatBan"].ToString()
                            };
                            books.Add(bookDetails);
                        }
                    }
                }
            }
            return books;
        }
    
     
         public static bool ThemSach(SachDTO book)
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

        public static bool XoaSach(string ID)
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
