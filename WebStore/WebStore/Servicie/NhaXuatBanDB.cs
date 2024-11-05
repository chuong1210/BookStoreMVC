using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using WedStore.Const;
using WedStore.Models;
namespace WedStore.Servicie
{
    public static class NhaXuatBanDB
    {
        private static readonly string _connectionString=ConnectStringValue.ConnectStringMyDB;



        public static List<NhaXuatBanDTO> ListNhaXuatBans()
        {
            List<NhaXuatBanDTO> nhaXuatBans = new List<NhaXuatBanDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_DanhSachNXB", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhaXuatBanDTO nhaXuatBan = new NhaXuatBanDTO
                            {
                                IdNXB = reader.GetString(reader.GetOrdinal("id")),
                                Ten = reader.GetString(reader.GetOrdinal("ten")),
                                DiaChi = reader.GetString(reader.GetOrdinal("diachi")),
                                SoDienThoai = reader.GetString(reader.GetOrdinal("sodienthoai")),
                                Email = reader.GetString(reader.GetOrdinal("email"))
                            };
                            nhaXuatBans.Add(nhaXuatBan);
                        }
                    }
                }
            }

            return nhaXuatBans;
        }
    }
}
