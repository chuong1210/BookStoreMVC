using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Models;
using WedStore.Const;
using WedStore.Repositories;

namespace WedStore.Controllers
{

    public class AccountController : Controller
    {
        private List<string> role = new List<string>();

        public ActionResult Index()
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
            var account = NguoiDungDB.LayTatCaNguoiDung();
            dy.account = account;
            return View(dy);
        }
        public ActionResult Login()
        {
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(NguoiDungDTO account, string returnUrl)
        {
            if (account.UserName == null)
            {
                ViewBag.ErrorMessage = "Vui lòng nhập tên tài khoản";
                return View(account);
            }
            else if (account.Password == null)
            {
                ViewBag.ErrorMessage = "Vui lòng nhập password";
                return View(account);
            }

            var result = NguoiDungDB.LoginUser(account.UserName, account.Password);
            if (result != null)
            {
                if (result.UserRole == "Customer")
                {
                    role.Add("Customer");
                }
                else if (result.UserRole == "Staff")
                {
                    role.Add("Customer");
                    role.Add("Admin");
                }
                else if (result.UserRole == "Admin")
                {
                    role.Add("Customer");
                    role.Add("Admin");
                    role.Add("SuperAdmin");
                }
                if (!string.IsNullOrEmpty(result.UserName))
                {
                    ClaimsIdentity userIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                    userIdentity.AddClaim(new Claim(ClaimTypes.Name, result.FullName));
                    foreach (var r in role)
                    {
                        userIdentity.AddClaim(new Claim(ClaimTypes.Role, r));
                    }
                    userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.idND));
                    //userIdentity.AddClaim(new Claim(ClaimTypes.Authentication, result.Authority));

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    if (returnUrl == "/Order/AddToCart")
                    {
                        return Redirect("/Book");
                    }

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect(" / ");
                    }
                }
            }
            ViewBag.ErrorMessage = "Tài khoản hoặc password không đúng";
            return View(account);
        }
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Account/Login");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public IActionResult SignUp(NguoiDungDTO model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra mật khẩu và xác nhận mật khẩu
                if (model.Password != model.RetypePassword)
                {
                    ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp.");
                    return View(model);
                }

                // Tạo ID ngẫu nhiên cho Người dùng và Khách hàng
                string idNguoiDung = $"user{Guid.NewGuid().ToString().Substring(0, 8)}";
                string idKhachHang = $"KH{Guid.NewGuid().ToString().Substring(0, 8)}";

                using (SqlConnection connection = new SqlConnection(ConnectStringValue.ConnectStringMyDB))
                {
                    connection.Open();

                    // Thực hiện việc lưu thông tin người dùng và khách hàng vào cơ sở dữ liệu
                    string execQuery = $@"
                EXEC SP_ThemNguoiDungVaKhachHang
                    @idNguoiDung = '{idNguoiDung}',
                    @username = '{model.UserName}',
                    @password = '{model.Password}',
                    @role = 'customer', 
                    @gioitinh = {model.Gender},
                    @NgaySinh = '{model.NgaySinh?.ToString("yyyy-MM-dd")}',
                    @idKhachHang = '{idKhachHang}',
                    @ten = N'{model.FullName}',
                    @diachi = N'{model.Address}',
                    @sodienthoai = '{model.Phone}',
                    @email = '{model.Email}'
            ";

                    using (SqlCommand command = new SqlCommand(execQuery, connection))
                    {
                        try
                        {
                            command.ExecuteNonQuery();

                            // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                            TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                            return RedirectToAction("Login", "Account");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                        }
                    }
                }
            }

            return View(model);
        }


    }
}
