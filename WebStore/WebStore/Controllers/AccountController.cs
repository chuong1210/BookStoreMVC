using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Models;
using WedStore.Repositories;

namespace WedStore.Controllers
{

    public class AccountController : Controller
    {
        private List<string> role = new List<string>();

        public ActionResult Index()
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.GetAllType();
            var account = NguoiDungDB.GetAll();
            dy.account = account;
            return View(dy);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login1(NguoiDungDTO account, string returnUrl) 
        {
            if (account.UserName == null)
            {
                ViewBag.ErrorMessage = "Vui lòng nhập tên tài khoản";
                return View(account);
            }
            else if(account.Password == null)
            {
                ViewBag.ErrorMessage = "Vui lòng nhập password";
                return View(account);
            }

            var result = NguoiDungDB.CheckLogin(account.UserName, account.Password);
            if(result != null)
            {
                if (result.Authority == 0)
                {
                    role.Add("Customer");            
                }
                else if (result.Authority == 1)
                {
                    role.Add("Customer");
                    role.Add("Admin");
                }
                else if (result.Authority == 2)
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
                    userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.UserName));
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
                if (result.UserRole == "customer")
                {
                    role.Add("Customer");
                }
                else if (result.UserRole == "staff")
                {
                    role.Add("Customer");
                    role.Add("Admin");
                }
                else if (result.UserRole == "admin")
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(NguoiDungDTO account,IFormCollection collection)
        {
            try
            {
                if(account.UserName == null)
                {
                    ViewBag.ErrorMessage = "vui lòng nhập tên tài khoản";
                    return View();
                }
                else if (account.Password == null)
                {
                    ViewBag.ErrorMessage = "vui lòng nhập mật khẩu";
                    return View();
                }
                else if (account.Password.Length <6)
                {
                    ViewBag.ErrorMessage = "mật khẩu ít nhất 6 ký tự";
                    return View();
                }
                else if (account.FullName == null)
                {
                    ViewBag.ErrorMessage = "vui lòng nhập họ tên";
                    return View();
                }
                else if (account.Age == 0)
                {
                    ViewBag.ErrorMessage = "vui lòng nhập tuổi hoặc tuổi không được nhỏ hơn 0";
                    return View();
                }
                else if (account.Phone == null)
                {
                    ViewBag.ErrorMessage = "vui lòng nhập số điện thoại";
                    return View();
                }
                else if (account.Address == null)
                {
                    ViewBag.ErrorMessage = "vui lòng nhập địa chỉ";
                    return View();
                }
                else if (account.Email == null)
                {
                    ViewBag.ErrorMessage = "vui lòng nhập email";
                    return View();
                }
                NguoiDungDTO acc = NguoiDungDB.GetAccountWithUser(account.UserName);
                if(acc.UserName != null)// username tồn tại
                {
                    ViewBag.ErrorMessage = "tài khoản này đã tồn tại";
                    return View();
                }
                acc = NguoiDungDB.GetAccountWithEmail(account.Email);
                if (acc.Email != null)// email tồn tại
                {
                    ViewBag.ErrorMessage = "email này đã tồn tại";
                    return View();
                }
                //hash password
                string passhash = BCrypt.Net.BCrypt.HashPassword(account.Password);
                account.Password = passhash;
                NguoiDungDB.Account_Create(account);
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }
    }
}
