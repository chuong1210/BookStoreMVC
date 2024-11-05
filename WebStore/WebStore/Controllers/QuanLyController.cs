using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Models;
using WedStore.Models;
using WedStore.Repositories;
using WedStore.Servicie;

namespace WedStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyController : Controller
    {
        // GET: ManagerController
        private readonly IHostingEnvironment he;
        private string userName;
        public QuanLyController(IHostingEnvironment e, IHttpContextAccessor httpContextAccessor)
        {
            he = e;
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userName = userId;
        }
        public ActionResult Index()
        {
            return View();
        }
        /////
        /////
        /////
        //ManagerAccount
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ManagerAccount()
        {
            var lstAcc = NguoiDungDB.GetAll();
            return View(lstAcc);
        }
        //Detail Account
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DetailAccount(string id)// username
        {
            var acc = NguoiDungDB.GetAccountWithUser(id);
            return View(acc);
        }
        // Edit Account
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult EditAccount(string id)//username
        {
            var acc = NguoiDungDB.GetAccountWithUser(id);
            return View(acc);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(NguoiDungDTO account)
        {
            if (account.UserName == null)
            {
                ViewBag.ErrorMessage = "vui lòng nhập tên tài khoản";
                return View(account);
            }
            else if (account.FullName == null)
            {
                ViewBag.ErrorMessage = "vui lòng nhập họ tên";
                return View(account);
            }
            else if (account.Age == 0)
            {
                ViewBag.ErrorMessage = "vui lòng nhập tuổi hoặc tuổi không được nhỏ hơn 0";
                return View(account);
            }
            else if (account.Phone == null)
            {
                ViewBag.ErrorMessage = "vui lòng nhập số điện thoại";
                return View(account);
            }
            else if (account.Address == null)
            {
                ViewBag.ErrorMessage = "vui lòng nhập địa chỉ";
                return View(account);
            }
            else if (account.Email == null)
            {
                ViewBag.ErrorMessage = "vui lòng nhập email";
                return View(account);
            }
            //kiểm tra email tồn tại
            var acc = NguoiDungDB.GetAccountWithUser(account.UserName);
            var lstAccount = NguoiDungDB.GetAll();
            var itemToRemove = lstAccount.Single(r => r.Email == acc.Email);
            lstAccount.Remove(itemToRemove);

            foreach(var i in lstAccount)
            {
                if(i.Email == account.Email)
                {
                    ViewBag.ErrorMessage = "email này đã tồn tại";
                    return View(account);
                }
            }
            NguoiDungDB.Account_Update(account);
            return RedirectToAction(nameof(ManagerAccount));
        }
        //Delete Account
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DeleteAccount(string id)//username
        {
            var acc = NguoiDungDB.GetAccountWithUser(id);
            return View(acc);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount(string id, IFormCollection collection)
        {
            // xóa dữ liệu của account
            var lstOrders = DonHangDB.GetOrdersUser(id);
            foreach (var i in lstOrders)
            {
                //đơn hàng
                InfoOrder infoOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithOrderID(i.OrderID);
                ChiTietDonHangDB.InfoOrder_Delete(infoOrder.InfoOrderID);
                //item trong giỏ hàng
                var listOrderItem = OrderItemRes.GetOrderItemsWithOrderID(i.OrderID);
                foreach (var item in listOrderItem)
                {
                    OrderItemRes.deleteOrderItem(item.ItemID);
                }
                //giỏ hàng
                DonHangDB.Orders_Delete(i.OrderID);
            }
            NguoiDungDB.Account_Delete(id);
            return RedirectToAction(nameof(ManagerAccount));
        }
        /////
        /////
        /////
        //ManagerBook
        public ActionResult ManagerBook()
        {
          //  var lstBook = SachDB.GetAll();
            var lstBook = SachDB.LayTatCaSach();
            return View(lstBook);
        }
        //Detail Book
        public ActionResult DetailsBook(string id)
        {
         //   var book = SachDB.BookWithID(id);
            var book = SachDB.SachTheoId(id);
            dynamic dy = new ExpandoObject();
            dy.book = book;
      //      dy.bookType = TheLoaiSachDB.BookTypeWithID(book.BookTypeID);
            dy.bookType = TheLoaiSachDB.LayThongTinTheLoai(book.BookTypeID);
            return View(dy);
        }
        //Edit Book
        public ActionResult EditBook(string id)
        {
          //  var book = SachDB.BookWithID(id);
            var book = SachDB.SachTheoId(id);

            dynamic dy = new ExpandoObject();
         //   dy.booktypeNAV = TheLoaiSachDB.GetAllType();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
            dy.NXBs=NhaXuatBanDB.ListNhaXuatBans();
            dy.book = book;
             List <TacGiaDTO> authors=TacGiaDB.ListTacGias();
            List<SelectListItem>  authorList = authors.Select(a => new SelectListItem
            {
                Value = a.IdTG.ToString(), // Crucial: Convert Id to string
                Text = a.TenTacGia.ToString()
            }).ToList();
            dy.TGs = authors;

            return View(dy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(string id,SachDTO book, IFormCollection collection, IFormFile Image)
        {
           
                if (ModelState.IsValid)
                {

                    List<TacGiaDTO> authors = TacGiaDB.ListTacGias();

                    //Book bookInData = BookRes.BookWithID(id);
                    if (book.BookName == null)
                    {
                        //ModelState.AddModelError(nameof(book.BookName), "ID not found");
                        ViewBag.ErrorMessage = "Vui lòng không để trống tên";
                        dynamic dy = new ExpandoObject();
                        //dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                        dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
                        //    dy.book = SachDB.BookWithID(book.BookID);
                        dy.book = SachDB.SachTheoId(book.BookID);
                        dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();
                        dy.TGs = authors;

                        return View(dy);
                    }
                    else if (book.BookTypeID == null)
                    {
                        ViewBag.ErrorMessage = "Vui lòng chọn loại sách";
                        dynamic dy = new ExpandoObject();
                        //dy.book = SachDB.BookWithID(book.BookID);
                        dy.book = SachDB.SachTheoId(book.BookID);
                        //  dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                        dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
                        dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();
                        dy.TGs = authors;

                        return View(dy);
                    }
                    else if (book.AuthorId == null)
                    {
                        ViewBag.ErrorMessage = "Vui lòng không để trống tên tác giả";
                        dynamic dy = new ExpandoObject();
                        //  dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                        dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();

                        //	dy.book = SachDB.BookWithID(book.BookID);
                        dy.book = SachDB.SachTheoId(book.BookID);
                        dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();
                        dy.TGs = authors;



                        return View(dy);
                    }
                    else if (book.NxbId == null)
                    {
                        ViewBag.ErrorMessage = "Vui lòng không để trống tên nhà xuất bản";
                        dynamic dy = new ExpandoObject();
                        // dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                        dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();

                        //dy.book = SachDB.BookWithID(book.BookID);
                        dy.book = SachDB.SachTheoId(book.BookID);
                        dy.TGs = authors;

                        dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();

                        return View(dy);
                    }
                    else if (book.Description == null)
                    {
                        ViewBag.ErrorMessage = "Vui lòng không để trống mô tả sách";
                        dynamic dy = new ExpandoObject();
                        //dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                        //dy.book = SachDB.BookWithID(book.BookID);

                        dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
                        dy.book = SachDB.SachTheoId(book.BookID);
                        dy.TGs = authors;

                        dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();

                        return View(dy);
                    }
                    else if (book.Price == null)
                    {
                        ViewBag.ErrorMessage = "Vui lòng không để trống giá bán";
                        dynamic dy = new ExpandoObject();
                        dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                        dy.book = SachDB.BookWithID(book.BookID);
                        dy.TGs = authors;

                        dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();

                        return View(dy);
                    }
                    else if (book.Quantity == null)
                    {
                        ViewBag.ErrorMessage = "Vui lòng không để trống số lượng";
                        dynamic dy = new ExpandoObject();
                        dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
                        dy.book = SachDB.SachTheoId(book.BookID);
                        dy.TGs = authors;

                        dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();

                        return View(dy);
                    }
                    else if (book.OrderedQuantity == null)
                    {
                        ViewBag.ErrorMessage = "Vui lòng không để trống lượng đặt hàng";
                        dynamic dy = new ExpandoObject();
                        dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
                        dy.book = SachDB.SachTheoId(book.BookID);
                        dy.TGs = authors;

                        dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();

                        return View(dy);
                    }
                    book.BookID = id;

                    if (Image == null)
                    {
                        book.Image = SachDB.SachTheoId(id).Image;

                      //  SachDB.Book_Update(book);
                        SachDB.CapNhat_Sach(book);
                    }
                    else
                    {
                        var fileName = Path.Combine(he.WebRootPath + "/images", Path.GetFileName(Image.FileName));
                        Image.CopyTo(new FileStream(fileName, FileMode.Create));
                        book.Image = Image.FileName;

                        // SachDB.Book_Update(book);
                        SachDB.CapNhat_Sach(book);
                    }
                    return RedirectToAction(nameof(ManagerBook));
                }
         
        else
        {
                 book = SachDB.SachTheoId(id);

                dynamic dy = new ExpandoObject();
                //   dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
                dy.NXBs = NhaXuatBanDB.ListNhaXuatBans();
                dy.book = book;
                List<TacGiaDTO> authors = TacGiaDB.ListTacGias();
           
                dy.TGs = authors;

                return View(dy);

    }
}
        //Create Book
        public ActionResult CreateBook()
        {
            dynamic dy = new ExpandoObject();
            dy.book = new SachDTO();
            dy.booktypeNAV = TheLoaiSachDB.GetAllType();
            return View(dy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(SachDTO book,IFormFile Image)
        {
            try
            {
                //ViewData["MessageError"] = "";
                if (book.BookName == null)
                {
                    //ModelState.AddModelError(nameof(book.BookName), "ID not found");
                    ViewBag.ErrorMessage = "Vui lòng nhập tên";
                    dynamic dy = new ExpandoObject();
                    dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                    return View(dy);
                }
                else if(book.BookTypeID == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng chọn loại sách";
                    dynamic dy = new ExpandoObject();
                    dy.book = new SachDTO();
                    dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                    return View(dy);
                }
                else if(book.Author == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập tên tác giả";
                    dynamic dy = new ExpandoObject();
                    dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                    return View(dy);
                }
                else if (book.Nxb == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập tên nhà xuất bản";
                    dynamic dy = new ExpandoObject();
                    dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                    return View(dy);
                }
                else if (book.Description == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập mô tả sách";
                    dynamic dy = new ExpandoObject();
                    dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                    return View(dy);
                }
                else if (book.Price == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập giá bán";
                    dynamic dy = new ExpandoObject();
                    dy.booktypeNAV = TheLoaiSachDB.GetAllType();
                    return View(dy);
                }

                //string fileName = Path.GetFileNameWithoutExtension(book.Image)
                Random rnd = new Random();
                int id = rnd.Next(10000, 99999);
                // kiem tra id co ton tai trong database ko
                while (SachDB.BookWithID(id.ToString()) != null)
                {
                    id = rnd.Next(10000, 99999);
                }

                var fileName = Path.Combine(he.WebRootPath+"/images", Path.GetFileName(Image.FileName));
                Image.CopyTo(new FileStream(fileName, FileMode.Create));
                book.BookID = id.ToString();
                book.Image = Image.FileName;
                SachDB.Book_CreateBook(book);
                return RedirectToAction(nameof(ManagerBook));
            }
            catch
            {
                string name = book.BookName;

                return View();
            }
        }
        //Delete Book
        public ActionResult DeleteBook(string id)
        {
            var book = SachDB.BookWithID(id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(string id, IFormCollection collection, string cancelButton)
        {
            try
            {
                if (cancelButton != null)
                {
                    return RedirectToAction(nameof(ManagerBook));
                }
                SachDB.Book_Delete(id);
                return RedirectToAction(nameof(ManagerBook));
            }
            catch
            {
                return View();
            }
        }
        /////
        /////
        /////
        //ManagerOrder
        public ActionResult ManagerOrder()
        {
            var lstInfoOrder = ChiTietDonHangDB.InfoOrder_GetAll();
            return View(lstInfoOrder);
        }
        // delete InfoOrder
        public ActionResult DeleteOrder(string id)//InfoOrderID
        {
            dynamic dy = new ExpandoObject();
            //lấy thông tin đơn hàng
            var infoOrder =  ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithID(id);
            dy.infoOrder = infoOrder;
            //lấy danh sách  OrderItem 
            var orderItem = OrderItemRes.GetOrderItemsWithOrderID(infoOrder.OrderID);
            dy.orderItem = orderItem;
            //lấy danh sách Book
            List<SachDTO> lstBook = new List<SachDTO>();
            foreach(var i in orderItem)
            {
                lstBook.Add(SachDB.BookWithID(i.BookID));
            }
            dy.lstBook = lstBook;
            return View(dy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrder(string id, IFormCollection collection)//InfoOrderID
        {
            var infoOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithID(id);
            var orderItem = OrderItemRes.GetOrderItemsWithOrderID(infoOrder.OrderID);
            //delete all Order Item
            foreach(var i in orderItem)
            {
                OrderItemRes.deleteOrderItem(i.ItemID);
            }
            //delete InfoOrder
            ChiTietDonHangDB.InfoOrder_Delete(id);
            //delete Order
            DonHangDB.Orders_Delete(infoOrder.OrderID);

            return RedirectToAction(nameof(ManagerOrder));
        }
        //detail InfoOrder
        public ActionResult DetailOrder(string id)//InfoOrderID
        {
            dynamic dy = new ExpandoObject();
            //lấy thông tin đơn hàng
            var infoOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithID(id);
            dy.infoOrder = infoOrder;
            //lấy danh sách  OrderItem 
            var orderItem = OrderItemRes.GetOrderItemsWithOrderID(infoOrder.OrderID);
            dy.orderItem = orderItem;
            //lấy danh sách Book
            List<SachDTO> lstBook = new List<SachDTO>();
            foreach (var i in orderItem)
            {
                lstBook.Add(SachDB.BookWithID(i.BookID));
            }
            dy.lstBook = lstBook;
            return View(dy);
        }
        // edit InfoOrder
        public ActionResult EditOrder(string id)////InfoOrderID
        {
            dynamic dy = new ExpandoObject();
            //lấy thông tin đơn hàng
            var infoOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithID(id);
            dy.infoOrder = infoOrder;
            //lấy danh sách  OrderItem 
            var orderItem = OrderItemRes.GetOrderItemsWithOrderID(infoOrder.OrderID);
            dy.orderItem = orderItem;
            //lấy danh sách Book
            List<SachDTO> lstBook = new List<SachDTO>();
            foreach (var i in orderItem)
            {
                lstBook.Add(SachDB.BookWithID(i.BookID));
            }
            dy.lstBook = lstBook;
            return View(dy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(InfoOrder infoOrder)
        {
            ChiTietDonHangDB.InfoOrder_Update(infoOrder);
            return RedirectToAction(nameof(ManagerOrder));
        }
        //update status InfoOrder
        public ActionResult InfoOrderComplete(string id)
        {
            var infoOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithID(id);
            infoOrder.Status = 1;
            ChiTietDonHangDB.InfoOrder_Update(infoOrder);
            return RedirectToAction(nameof(ManagerOrder));
        }
        public ActionResult InfoOrderIncomplete(string id)
        {
            var infoOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithID(id);
            infoOrder.Status = 0;
            ChiTietDonHangDB.InfoOrder_Update(infoOrder);
            return RedirectToAction(nameof(ManagerOrder));
        }
    }
}
