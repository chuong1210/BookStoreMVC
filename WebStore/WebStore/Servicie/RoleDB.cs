using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CAIT.SQLHelper;
using WebStore.Models;
using WedStore.Const;

namespace WebStore.Servicie
{
    public static class RoleDB
    {
        public static bool HasPermission(string userId, string requiredPermission)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            string connectionString = ConnectStringValue.ConnectStringMyDB;

            // Truy vấn kiểm tra quyền
            string query = @"
            SELECT COUNT(*) 
            FROM Permissions p
            INNER JOIN Role_Permissions rp ON p.id = rp.permission_id
            INNER JOIN User_Roles ur ON rp.role_id = ur.role_id
            WHERE ur.user_id = @UserId AND p.permission_name = @Permission";

            try
            {
                // Kết nối đến cơ sở dữ liệu
                using (var connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo lệnh SQL
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số @UserId
                        command.Parameters.AddWithValue("@UserId", userId);
                        // Thêm tham số @Permission
                        command.Parameters.AddWithValue("@Permission", requiredPermission);

                        // Thực thi lệnh và nhận kết quả
                        int count = (int)command.ExecuteScalar();

                        // Kiểm tra nếu count > 0 thì người dùng có quyền
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý lỗi
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public static List<RoleDTO> ListRole()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            string connectionString = ConnectStringValue.ConnectStringMyDB;

            // Truy vấn SQL để lấy danh sách vai trò
            string query = "SELECT id, role_name FROM Roles";

            // Danh sách vai trò sẽ trả về
            List<RoleDTO> lstResult = new List<RoleDTO>();

            try
            {
                // Tạo kết nối đến cơ sở dữ liệu
                using (var connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo lệnh SQL
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Thực thi lệnh và đọc dữ liệu
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RoleDTO role = new RoleDTO
                                {
                                    id = reader["id"].ToString(),
                                    roleName = reader["role_name"].ToString()
                                };

                                lstResult.Add(role);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý lỗi
                Console.WriteLine("Error: " + ex.Message);
            }

            return lstResult;
        }

    }
}
