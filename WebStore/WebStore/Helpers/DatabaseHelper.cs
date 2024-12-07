using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WedStore.Const;
using System.Data;
using System.Data.SqlClient;

namespace WebStore.Helpers
{
    public class DatabaseHelper
    {
        private readonly string _connectionString = ConnectStringValue.ConnectStringMyDB;
        // Kiểm tra username đã tồn tại
        public async Task<bool> IsUsernameExists(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT COUNT(*) FROM NguoiDung WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = (int)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }
        // Thêm người dùng mới và trả về ID
        public async Task<int> InsertNguoiDung(string username, string password, string role, string gioiTinh, DateTime ngaySinh)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
                INSERT INTO NguoiDung (Username, Password, Role, GioiTinh, NgaySinh)
                OUTPUT INSERTED.ID
                VALUES (@Username, @Password, @Role, @GioiTinh, @NgaySinh)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // Nên hash mật khẩu
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);

                    int userId = (int)await cmd.ExecuteScalarAsync();
                    return userId;
                }
            }
        }
        // Thêm khách hàng mới
        public async Task InsertKhachHang(string ten, string diaChi, string soDienThoai, string email, int idNguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
                INSERT INTO KhachHang (Ten, DiaChi, SoDienThoai, Email, Id_NguoiDung)
                VALUES (@Ten, @DiaChi, @SoDienThoai, @Email, @IdNguoiDung)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten", ten);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@IdNguoiDung", idNguoiDung);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
