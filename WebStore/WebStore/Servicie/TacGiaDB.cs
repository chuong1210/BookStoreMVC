using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using WedStore.Const;
using WedStore.Models;
using System;

namespace WedStore.Repositories
{
    public class TacGiaDB
    {
        private static readonly string _connectionString = ConnectStringValue.ConnectStringMyDB;



        public static List<TacGiaDTO> ListTacGias()
        {
            List<TacGiaDTO> tacGias = new List<TacGiaDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_DanhSachTacGia", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Kiểm tra nếu giá trị là NULL trước khi đọc
                            string idTG = reader.IsDBNull(reader.GetOrdinal("id")) ? null : reader.GetString(reader.GetOrdinal("id"));
                            string tentacGia = reader.IsDBNull(reader.GetOrdinal("ten")) ? null : reader.GetString(reader.GetOrdinal("ten"));
                            string ngaySinhStr; // Crucial: Declare as string

                            if (reader.IsDBNull(reader.GetOrdinal("NgaySinh")))
                            {
                                ngaySinhStr = "Không rõ ngày sinh"; // Handle null
                            }
                            else
                            {
                                // Format the DateTime to string.  Important!
                                ngaySinhStr = reader.GetDateTime(reader.GetOrdinal("NgaySinh")).ToString("yyyy-MM-dd"); // Format as desired
                            }
                            string quocTich = reader.IsDBNull(reader.GetOrdinal("QuocTich")) ? null : reader.GetString(reader.GetOrdinal("QuocTich"));

                            //Kiểm tra xem id không null
                            if (idTG == null)
                            {
                                throw new Exception("Id tác giả không được phép null");
                            }


                            TacGiaDTO tg = new TacGiaDTO
                            {
                                IdTG = idTG,
                                TenTacGia = tentacGia,
                                NgaySinh = ngaySinhStr,
                                QuocTich = quocTich
                            };
                            tacGias.Add(tg);
                        }
                    }
                }
            }

            return tacGias;
        }
    }
}
