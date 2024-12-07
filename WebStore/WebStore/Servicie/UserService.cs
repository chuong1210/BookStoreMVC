using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using WebStore.Data;

namespace WebStore.Servicie
{
    public class UserService
    {
        private readonly DatabaseHelper _dbHelper;

        public UserService()
        {
            _dbHelper = new DatabaseHelper();
        }

        public void RegisterUser(string username, string password)
        {
            // Tạo ID ngẫu nhiên cho người dùng
            string userId = "USER" + new Random().Next(10000000, 99999999).ToString();  // Tạo số ngẫu nhiên dài 8 chữ số

            // Thêm người dùng vào bảng NguoiDung
            string addUserQuery = "INSERT INTO NguoiDung (id, username, password, role, gioitinh, NgaySinh) " +
                                  "VALUES (@UserId, @Username, @Password, 'User', 0, GETDATE())";
            var userParameters = new[]
            {
        new SqlParameter("@UserId", userId),
        new SqlParameter("@Username", username),
        new SqlParameter("@Password", password)
    };
            _dbHelper.ExecuteNonQuery(addUserQuery, userParameters);

            // Tạo ID tự động cho khách hàng
            string customerIdQuery = "SELECT 'KH' + RIGHT('0000' + CAST(COALESCE(MAX(CAST(SUBSTRING(id, 3, LEN(id) - 2) AS INT)), 0) + 1 AS VARCHAR), 4) FROM KhachHang";
            string customerId = _dbHelper.ExecuteScalar(customerIdQuery)?.ToString();

            // Thêm khách hàng vào bảng KhachHang
            string addCustomerQuery = "INSERT INTO KhachHang (id, ten, diachi, sodienthoai, email, id_NguoiDung) " +
                                      "VALUES (@CustomerId, @Ten, @DiaChi, @SoDienThoai, @Email, @UserId)";
            var customerParameters = new[]
            {
        new SqlParameter("@CustomerId", customerId),
        new SqlParameter("@UserId", userId),
        new SqlParameter("@Ten", " "),  // giá trị mặc định
        new SqlParameter("@DiaChi", " "), // giá trị mặc định
        new SqlParameter("@SoDienThoai", " "), // giá trị mặc định
        new SqlParameter("@Email", " ")  // giá trị mặc định
    };
            _dbHelper.ExecuteNonQuery(addCustomerQuery, customerParameters);
        }

    }
}
