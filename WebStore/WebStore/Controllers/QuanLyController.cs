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
using System.Security.Policy;
using System.Threading.Tasks;
using WebStore.Constraint;
using WebStore.Models;
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
          //  var lstAcc = NguoiDungDB.GetAll();
            var model = new EditAccountViewModel
            {
                NguoiDungs = NguoiDungDB.LayTatCaNguoiDung(),
                KhachHangs = NguoiDungDB.LayTatCaKhachHang(),
                NhanViens = NguoiDungDB.LayTatCaNhanVien()
            };
            return View(model);
           // return View(lstAcc);
        }
        //Detail Account
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DetailAccount(string id)// username
        {
            var acc = NguoiDungDB.LayChiTietNguoiDungTheoId(id);
            return View(acc);
        }
        // Edit Account
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult EditAccount(string id)//username
        {
            var acc = NguoiDungDB.LayChiTietNguoiDungTheoId(id);
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
            else if (account.NgaySinh == null)
            {
                ViewBag.ErrorMessage = "vui lòng nhập ngày sinh phù hợp";
                return View(account);
            }
            else if (account.Phone == null)
            {
                ViewBag.ErrorMessage = "vui lòng nhập số điện thoại";
                return View(account);
            }
    
            else if (account.Email == null)
            {
                ViewBag.ErrorMessage = "vui lòng nhập email";
                return View(account);
            }
            //kiểm tra email tồn tại
            var acc = NguoiDungDB.LayChiTietNguoiDungTheoId(account.idND);
            var lstAccount = NguoiDungDB.LayTatCaNguoiDung();
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
            NguoiDungDB.CapNhatNguoiDung(account);
            return RedirectToAction(nameof(ManagerAccount));
        }
        //Delete Account
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DeleteAccount(string id)//username
        {
            var acc = NguoiDungDB.LayChiTietNguoiDungTheoId(id);
            return View(acc);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount(string id, IFormCollection collection)
        {
            // xóa dữ liệu của account
            //var lstOrders = DonHangDB.GetOrdersUser(id);
            //foreach (var i in lstOrders)
            //{
            //    //đơn hàng
            //    InfoOrder infoOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithOrderID(i.OrderID);
            //    ChiTietDonHangDB.InfoOrder_Delete(infoOrder.OrderDetailId);
            //    //item trong giỏ hàng
            //    var listOrderItem = ChiTietHoaDonDB.GetOrderItemsWithOrderID(i.OrderID);
            //    foreach (var item in listOrderItem)
            //    {
            //        ChiTietHoaDonDB.deleteOrderItem(item.ItemID);
            //    }
            //    //giỏ hàng
            //    DonHangDB.Orders_Delete(i.OrderID);
            //}
           bool ab= NguoiDungDB.XoaNguoiDung(id);
            if(ab)
            return RedirectToAction(nameof(ManagerAccount));
            else
            {
                var acc = NguoiDungDB.LayChiTietNguoiDungTheoId(id);
                ViewBag.msgErr = "Xóa thất bại";
                return View(acc);


            }
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
     
      

        [HttpGet]
        public ActionResult EditBook(string id)
        {
           var book = SachDB.SachTheoId(id);
            var authors = TacGiaDB.ListTacGias();
            var publishers = NhaXuatBanDB.ListNhaXuatBans();
            var bookTypes = TheLoaiSachDB.ListTheLoai();
            var viewModel = new EditBookViewModel
            {
                Book = book,
                Authors = new SelectList(authors, "IdTG", "TenTacGia", book.AuthorIds),
                Publishers = new SelectList(publishers, "IdNXB", "Ten", book.NxbId),
                BookTypes = new SelectList(bookTypes, "BookTypeID", "BookTypeName", book.BookTypeID),

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(string id, SachDTO book, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                return ReloadEditView(id, book);
            }

            if (string.IsNullOrEmpty(book.BookName))
            {
                ModelState.AddModelError(nameof(book.BookName), "Vui lòng không để trống tên sách");
                return ReloadEditView(id, book);
            }

            if (string.IsNullOrEmpty(book.BookTypeID))
            {
                ModelState.AddModelError(nameof(book.BookTypeID), "Vui lòng chọn loại sách");
                return ReloadEditView(id, book);
            }

            if (book.AuthorIds.Count<=0)
            {
                ModelState.AddModelError(nameof(book.AuthorId), "Vui lòng không để trống tên tác giả");
                return ReloadEditView(id, book);
            }

            if (string.IsNullOrEmpty(book.NxbId))
            {
                ModelState.AddModelError(nameof(book.NxbId), "Vui lòng không để trống tên nhà xuất bản");
                return ReloadEditView(id, book);
            }

            if (string.IsNullOrEmpty(book.Description))
            {
                ModelState.AddModelError(nameof(book.Description), "Vui lòng không để trống mô tả sách");
                return ReloadEditView(id, book);
            }

            if (Image == null)
            {
                book.Image = SachDB.SachTheoId(id).Image;
            }
            else
            {
                var fileName = Path.Combine(he.WebRootPath + "/images", Path.GetFileName(Image.FileName));
                Image.CopyTo(new FileStream(fileName, FileMode.Create));
                book.Image = Image.FileName;
            }

            book.BookID = id;
         //   book.AuthorId=string.Join(",",book.AuthorIds);
            SachDB.CapNhat_Sach(book);

            return RedirectToAction(nameof(ManagerBook));
        }

        private ActionResult ReloadEditView(string id, SachDTO book)
        {
             book = SachDB.SachTheoId(id);
            var authors = TacGiaDB.ListTacGias();
            var publishers = NhaXuatBanDB.ListNhaXuatBans();
            var bookTypes = TheLoaiSachDB.ListTheLoai();

           var viewModel = new EditBookViewModel
            {
               Book = book,
               Authors = new SelectList(authors, "IdTG", "TenTacGia", book.AuthorId),
               Publishers = new SelectList(publishers, "IdNXB", "Ten", book.NxbId),
               BookTypes = new SelectList(bookTypes, "BookTypeID", "BookTypeName", book.BookTypeID)
           };
            return View("EditBook", viewModel);
        }




        //Create Book
        public ActionResult CreateBook()
        {
			var authors = TacGiaDB.ListTacGias();
			var publishers = NhaXuatBanDB.ListNhaXuatBans();
			var bookTypes = TheLoaiSachDB.ListTheLoai();
			var viewModel = new EditBookViewModel
			{
				nhaXuatBanDTOs=publishers,
                theloaiDTOs=bookTypes,
                tacGiaDTOs=authors,

			};
   //         		var viewModel = new EditBookViewModel
			//{
			//	Authors = new SelectList(authors, "IdTG", "TenTacGia"),
			//	Publishers = new SelectList(publishers, "IdNXB", "Ten"),
			//	BookTypes = new SelectList(bookTypes, "BookTypeID", "BookTypeName"),

			//};

			return View(viewModel);

		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(SachDTO book,IFormFile Image)
        {
            try
            {
                var authors = TacGiaDB.ListTacGias();
                var publishers = NhaXuatBanDB.ListNhaXuatBans();
                var bookTypes = TheLoaiSachDB.ListTheLoai();
                var viewModel = new EditBookViewModel
                {
                    nhaXuatBanDTOs = publishers,
                    theloaiDTOs = bookTypes,
                    tacGiaDTOs = authors,

                };
                //ViewData["MessageError"] = "";
                if (book.BookName == null)
                {
                    //ModelState.AddModelError(nameof(book.BookName), "ID not found");
                    ViewBag.ErrorMessage = "Vui lòng nhập tên";
                    return View(viewModel);
                }
                else if(book.BookTypeID == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng chọn loại sách";

                    return View(viewModel);
                }
                else if(book.AuthorId == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập tên tác giả";

                    return View(viewModel);
                }
                else if (book.NxbId == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập tên nhà xuất bản";

                    return View(viewModel);
                }
                else if (book.Description == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập mô tả sách";
                    return View(viewModel);


                }
                else if (book.Price == null)
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập giá bán";

                    return View(viewModel);
                }

                //string fileName = Path.GetFileNameWithoutExtension(book.Image)

                // Kiểm tra nếu ảnh có được chọn
                if (Image != null && Image.Length > 0)
                {
                    // Lưu ảnh vào thư mục images
                    var filePath = Path.Combine(he.WebRootPath, "images", Path.GetFileName(Image.FileName));

                    // Đảm bảo file không bị chiếm dụng bởi một process khác
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(fileStream);
                    }

                    // Cập nhật ảnh vào thông tin sách
                    book.Image = Path.GetFileName(Image.FileName);
                }

                SachDB.ThemSach(book);
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
            var book = SachDB.SachTheoId(id);
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
                SachDB.XoaSach(id);
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
         //  var lstInfoOrder = ChiTietDonHangDB.InfoOrder_GetAll();
          //var lstInfoOrder = DonHangDB.LayDanhSachDonHangVoiThongTinND();
          var lstInfoOrder = HoaDonDB.LayTatCaHoaDon();
            return View(lstInfoOrder);
        }    
        public ActionResult ManagerPublisher()
        {
            var lstInfoOrder = NhaXuatBanDB.ListNhaXuatBans();
            return View(lstInfoOrder);
        }
                public ActionResult ManagerAuthor()
        {
            var lstInfoAuthor = TacGiaDB.ListTacGias();
            return View(lstInfoAuthor);
        }


        // delete InfoOrder
        public ActionResult DeleteOrder(string id)
        {
            dynamic dy = new ExpandoObject();
            var infoOrder =  HoaDonDB.LayHoaDonTheoId(id);
            dy.infoOrder = infoOrder;
            var orderItem = ChiTietHoaDonDB.LayChiTietDonHangTheoDonHang(infoOrder.DonHangId);
            dy.orderItem = orderItem;
            List<SachDTO> lstBook = new List<SachDTO>();
            foreach(var i in orderItem)
            {
                lstBook.Add(SachDB.SachTheoId(i.BookID));
            }
            dy.lstBook = lstBook;
            return View(dy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrder(string id, IFormCollection collection)//OrderDetailId
        {
            bool isDeleted = HoaDonDB.XoaHoaDonVaCapNhatDonHang(id);

            if (isDeleted)
            {
                TempData["Message"] = "Hóa đơn đã được xóa thành công và trạng thái đơn hàng đã được cập nhật.";
            }
            else
            {
                TempData["Error"] = "Lỗi khi xóa hóa đơn.";
            }

            return RedirectToAction(nameof(ManagerOrder));
        }
        
        public ActionResult DetailOrder(string id)//OrderDetailId
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index"); // Hoặc trả về một lỗi nếu id không hợp lệ
            }
            HoaDonDTO  orderInfo = HoaDonDB.LayHoaDonTheoId(id);
            List<ChiTietVatPhamDTO> orderDetails = ChiTietHoaDonDB.LayChiTietDonHangTheoDonHang(orderInfo.DonHangId);
         //   var bookdetail = SachDB.LaySachTheoOrderId(orderInfo.DonHangId);

     
            var model = new OrderDetailViewModel
            {
                InfoOrder = orderInfo,
                OrderItem = orderDetails,
                LstBook = SachDB.LaySachTheoOrderId(id)
            };

            if (orderDetails == null || orderDetails.Count == 0)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy chi tiết đơn hàng
            }

            // Trả về View và truyền dữ liệu chi tiết đơn hàng
            return View(model); // Trả về View với model đã được cấu trúc đúng
        }
   
        
        
        public ActionResult EditOrder(string id)////OrderDetailId
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index"); // Hoặc trả về một lỗi nếu id không hợp lệ
            }
            HoaDonDTO orderInfo = HoaDonDB.LayHoaDonTheoId(id);

            List<ChiTietVatPhamDTO> orderDetails = ChiTietHoaDonDB.LayChiTietDonHangTheoDonHang(orderInfo.DonHangId); // Giả sử phương thức này lấy chi tiết đơn hàng

            var model = new OrderDetailViewModel
            {
                InfoOrder = orderInfo,
                OrderItem = orderDetails,
                LstBook = SachDB.LaySachTheoOrderId(orderInfo.DonHangId)
            };
            model.PhuongThucTT = (PhuongThucThanhToan)Enum.Parse(typeof(PhuongThucThanhToan), model.InfoOrder.PhuongThucTT);


            if (orderDetails == null || orderDetails.Count == 0)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy chi tiết đơn hàng
            }

            // Trả về View và truyền dữ liệu chi tiết đơn hàng
            return View(model); // Trả về View với model đã được cấu trúc đúng
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(HoaDonDTO infoOrder)
        {
          //  ChiTietDonHangDB.InfoOrder_Update(infoOrder);

            HoaDonDB.CapNhatHoaDon(infoOrder);
            return RedirectToAction(nameof(ManagerOrder));
        }
    
        public ActionResult InfoOrderComplete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index"); // Hoặc xử lý lỗi nếu orderId không hợp lệ
            }

            // Cập nhật trạng thái đơn hàng
            bool isUpdated = HoaDonDB.CapNhatTrangThaiHoaDon(id,"Đã thanh toán");

            if (isUpdated)
            {
                // Chuyển hướng đến trang quản lý đơn hàng hoặc thông báo thành công
                return RedirectToAction("ManagerOrder");
            }
            else
            {
                // Xử lý lỗi nếu cập nhật không thành công
                ViewBag.ErrorMessage = "Cập nhật trạng thái đơn hàng không thành công.";
                return View("Error"); // Hoặc chuyển đến trang thông báo lỗi
            }
        }
          
        public ActionResult InfoOrderIncomplete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index"); // Hoặc xử lý lỗi nếu orderId không hợp lệ
            }

            // Cập nhật trạng thái đơn hàng
            bool isUpdated = HoaDonDB.CapNhatTrangThaiHoaDon(id, "Chưa thanh toán");

            if (isUpdated)
            {
                // Chuyển hướng đến trang quản lý đơn hàng hoặc thông báo thành công
                return RedirectToAction(nameof(ManagerOrder));

            }
            else
            {
                // Xử lý lỗi nếu cập nhật không thành công
                ViewBag.ErrorMessage = "Cập nhật trạng thái đơn hàng không thành công.";
                return View("Error"); // Hoặc chuyển đến trang thông báo lỗi
            }
        }
    }
}
