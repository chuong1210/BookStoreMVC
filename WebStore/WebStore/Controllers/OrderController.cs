using Microsoft.AspNetCore.Authorization;
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
using WedStore.Servicie;

namespace WedStore.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private string userName;
        private string idND;
        public OrderController(IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userName = userId;
			idND= userId;

		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(ChiTietVatPhamDTO orderItem)
        {
            bool ab=DonHangDB.AddToCart(idND, orderItem.BookID, orderItem.Quantity);
            if (ab)
            return RedirectToAction("Cart", "Order");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Cart()
        {
            dynamic dy = new ExpandoObject();
            // dy.booktypeNAV = TheLoaiSachDB.GetAllType();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();

			//bool result1 = DonHangDB.checkOrders(userName);

			//   DonHang orders =DonHangDB.GetOrdersUserOnStatus(userName, 1);
			  DonHang orders =DonHangDB.LayOrderTheoTrangThai(userName, 0);
            if (orders == null)
            {
                // Nếu không có đơn hàng nào có trạng thái = 0, truyền thông báo cho View
                dy.emptyCart = true;
                return View(dy);
            }
            dy.emptyCart = false;

            //	List<ChiTietVatPhamDTO> lstOrderItem = ChiTietHoaDonDB.GetOrderItemsWithOrderID(orders.OrderID);
            List<ChiTietVatPhamDTO> lstOrderItem = ChiTietHoaDonDB.LayChiTietDonHangTheoDonHang(orders.OrderID);
            dy.orderItem = lstOrderItem;
            decimal totalPrice = 0; 
            List<SachDTO> lstBook = new List<SachDTO>();//danh sách thông tin sách từ Item
            if(lstOrderItem.Count !=0)
            {
                foreach (var item in lstOrderItem)
                {
                    //cộng giá Item vào DonHang
                    totalPrice += item.TotalPrice;
                    // cập nhật tổng giá thì giỏ hàng
                    orders.OrderPrice = totalPrice;


				//	DonHangDB.Orders_Update(orders);
					DonHangDB.Capnhat_DH(orders);
                    // danh sách thông tin sách
                  //  lstBook.Add(SachDB.BookWithID(item.BookID));
                    lstBook.Add(SachDB.SachTheoId(item.BookID));


                }

				decimal tongtien = ChiTietDonHangDB.TongTienDH(orders.OrderID);
				DonHangDB.Capnhat_DH(orders);


			}
			else
            {
                // cập nhật tổng giá thì giỏ hàng
                orders.OrderPrice = totalPrice;
               // DonHangDB.Orders_Update(orders);
                DonHangDB.Capnhat_DH(orders);
                // danh sách thông tin sách
            }
            dy.lstBook = lstBook;
            dy.orders = orders;
            return View(dy);
        }
       
        public ActionResult Checkout()
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
            dy.account = NguoiDungDB.LayChiTietNguoiDungTheoId(idND);
            //tìm giỏ hàng của user trạng thái =0
            DonHang orders = DonHangDB.LayOrderTheoTrangThai(idND, 0);
            if(orders == null)
            {
                return Redirect("/");
            }
            if(orders.OrderPrice == 0)
            {
                return RedirectToAction(nameof(Cart));
            }
            dy.orders = orders;
            dy.totalPrice = orders.OrderPrice + 20000;
            return View(dy);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(HoaDonDTO hd)
        {
            // Lấy phương thức thanh toán từ form
            var paymentMethod = Request.Form["PaymentMethod"];
            DonHang orders = DonHangDB.LayOrderTheoTrangThai(idND, 0);
            hd.NgayLap = DateTime.Now;
            hd.PhuongThucTT = paymentMethod.ToString();
            hd.DonHangId = orders.OrderID;
            hd.TongTien = orders.OrderPrice + 20000;

            bool rs = HoaDonDB.ThanhToanDonHang(hd);


            return Redirect("OrdersList");
        }

        public ActionResult OrdersList()// danh sách đơn hàng
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
            //lấy danh sách các giỏ hàng của user bằng email
            //List<DonHang> lstOrder = DonHangDB.LayDanhSachOrderTheoTrangThai(idND,1);
            List<HoaDonDTO> lstOrder = HoaDonDB.LayHoaDonTheoUserId(idND);
            if(lstOrder==null)
            {
                ViewBag.msgErr = true;
                return View();
            }
          
            ViewBag.msgErr = false;

            dy.lstOrder = lstOrder;
            return View(dy);
        }
        public ActionResult InfoOrderDetail(string id)
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();

            var infoOrder = HoaDonDB.LayHoaDonTheoId(id);
            
            //lấy danh sách item trong giỏ hàng bằng OrderID
            List<ChiTietVatPhamDTO> lstOrderItem = ChiTietHoaDonDB.LayChiTietDonHangTheoDonHang(infoOrder.DonHangId);
            dy.orderItem = lstOrderItem;

            //tìm thông tin sách từ List ChiTietVatPhamDTO trong giỏ hàng
            List<SachDTO> lstBook = new List<SachDTO>();
            foreach (var item in lstOrderItem)
            {
                lstBook.Add(SachDB.SachTheoId(item.BookID));
            }
            dy.lstBook = lstBook;
            dy.infoOrder = infoOrder;
            return View(dy);
        }
        public ActionResult DeleteItem(string id)
        {
            //xóa item trong giỏ hàng
            //ChiTietHoaDonDB.deleteOrderItem(id);//ItemID
            return RedirectToAction("Cart");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(ChiTietVatPhamDTO orderItem,string add, string sub)
        {
            //lấy thông tin item bằng ItemID
           // ChiTietVatPhamDTO orderItem1 = ChiTietHoaDonDB.GetOrderItemWithID(orderItem.ItemID);
            ChiTietVatPhamDTO orderItem1 = ChiTietHoaDonDB.LayChiTietDonHangTheoIdCTDH(orderItem.ItemID);
            //qiá sản phẩm
            decimal price = orderItem1.TotalPrice / orderItem1.Quantity;
            //nếu nhấm button "add" thì tăng sản phẩm lên 1 ngược lại "sub"  giảm 1
            if (add != null)
            {
                orderItem1.Quantity ++;
                //orderItem1.TotalPrice = price * orderItem1.Quantity;
				//ChiTietHoaDonDB.updateOrderItem(orderItem1);
				ChiTietHoaDonDB.capnhatChiTietDonHang(orderItem1);

			}
			else if(sub != null)
            {
                orderItem1.Quantity--;
                if(orderItem1.Quantity != 0)
                {
                   // orderItem1.TotalPrice = price * orderItem1.Quantity;
                    //ChiTietHoaDonDB.updateOrderItem(orderItem1);
                    ChiTietHoaDonDB.capnhatChiTietDonHang(orderItem1);
                }
                else
                {
                   // ChiTietHoaDonDB.deleteOrderItem(orderItem1.ItemID);
                    ChiTietHoaDonDB.xoaChiTietDonHang(orderItem1.ItemID);
                }
            }
            return RedirectToAction("Cart");
        }
    }
}
