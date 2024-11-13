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
        public ActionResult AddToCart1(OrderItem orderItem)
        {
            bool result1 = DonHangDB.checkOrders(userName);// kiểm tra có giỏ hàng chưa

            Orders orders = DonHangDB.GetOrdersUserOnStatus(userName, 1);

            OrderItem item = OrderItemRes.GetOrderItemWithOrderIDBookID(orders.OrderID, orderItem.BookID);
            if(item != null)
            {
                item.Quantity += orderItem.Quantity;
                item.TotalPrice = item.Quantity * SachDB.BookWithID(orderItem.BookID).Price;
                OrderItemRes.updateOrderItem(item);
            }
            else
            {
                Random rnd = new Random();
                int id = rnd.Next(100000, 999999);
                while (OrderItemRes.GetOrderItemWithID(id.ToString()) != null)
                {
                    id = rnd.Next(100000, 999999);
                }
                orderItem.ItemID = id.ToString();
                orderItem.OrderID = orders.OrderID;
                orderItem.TotalPrice = orderItem.Quantity * SachDB.BookWithID(orderItem.BookID).Price;
                orderItem.Discount = 0;
                OrderItemRes.createOrderItem(orderItem);
            }
            return RedirectToAction("Cart", "Order");
        } [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(OrderItem orderItem)
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

			//   Orders orders =DonHangDB.GetOrdersUserOnStatus(userName, 1);
			  Orders orders =DonHangDB.LayDanhSachOrderTheoTrangThai(userName, 0);


		//	List<OrderItem> lstOrderItem = OrderItemRes.GetOrderItemsWithOrderID(orders.OrderID);
			List<OrderItem> lstOrderItem = OrderItemRes.LayChiTietDonHangTheoDonHang(orders.OrderID);
            dy.orderItem = lstOrderItem;
            decimal totalPrice = 0; 
            List<SachDTO> lstBook = new List<SachDTO>();//danh sách thông tin sách từ Item
            if(lstOrderItem.Count !=0)
            {
                foreach (var item in lstOrderItem)
                {
                    //cộng giá Item vào Orders
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
            dy.booktypeNAV = TheLoaiSachDB.GetAllType();
            dy.account = NguoiDungDB.GetAccountWithUser(userName);
            //tìm giỏ hàng của user trạng thái =1
            Orders orders = DonHangDB.GetOrdersUserOnStatus(userName, 1);
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
        public ActionResult Checkout(OrderDetails infoOrder)
        {
            Orders orders = DonHangDB.GetOrdersUserOnStatus(userName, 1);

            infoOrder.OrderID = orders.OrderID;
            infoOrder.TotalPrice = orders.OrderPrice + 20000;
            //random id cho thông tin đơn hàng
            Random rnd = new Random();
            int id = rnd.Next(100000, 999999);
            while (ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithID(id.ToString()) != null)
            {
                id = rnd.Next(100000, 999999);
            }
            infoOrder.OrderDetailId = id.ToString();
            //tạo đơn hàng
            ChiTietDonHangDB.InfoOrder_create(infoOrder);
            //cập nhật trạng thái giỏ hàng = 2
            orders.OrderStatus = 2;
            DonHangDB.Orders_Update(orders);
            return Redirect("OrdersList");
        }

        public ActionResult OrdersList()// danh sách đơn hàng
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.GetAllType();
            //lấy danh sách các giỏ hàng của user bằng email
            List<OrderDetails> lstOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrderWithEmail(
                                        NguoiDungDB.GetAccountWithUser(userName).Email);
            dy.lstOrder = lstOrder;
            return View(dy);
        }
        public ActionResult InfoOrderDetail(string id)
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.GetAllType();

            var infoOrder = ChiTietDonHangDB.InfoOrder_GetInfoOrdersWithID(id);
            
            //lấy danh sách item trong giỏ hàng bằng OrderID
            List<OrderItem> lstOrderItem = OrderItemRes.GetOrderItemsWithOrderID(infoOrder.OrderID);
            dy.orderItem = lstOrderItem;

            //tìm thông tin sách từ List OrderItem trong giỏ hàng
            List<SachDTO> lstBook = new List<SachDTO>();
            foreach (var item in lstOrderItem)
            {
                lstBook.Add(SachDB.BookWithID(item.BookID));
            }
            dy.lstBook = lstBook;
            dy.infoOrder = infoOrder;
            return View(dy);
        }
        public ActionResult DeleteItem(string id)
        {
            //xóa item trong giỏ hàng
            OrderItemRes.deleteOrderItem(id);//ItemID
            return RedirectToAction("Cart");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(OrderItem orderItem,string add, string sub)
        {
            //lấy thông tin item bằng ItemID
           // OrderItem orderItem1 = OrderItemRes.GetOrderItemWithID(orderItem.ItemID);
            OrderItem orderItem1 = OrderItemRes.LayChiTietDonHangTheoIdCTDH(orderItem.ItemID);
            //qiá sản phẩm
            decimal price = orderItem1.TotalPrice / orderItem1.Quantity;
            //nếu nhấm button "add" thì tăng sản phẩm lên 1 ngược lại "sub"  giảm 1
            if (add != null)
            {
                orderItem1.Quantity ++;
                //orderItem1.TotalPrice = price * orderItem1.Quantity;
				//OrderItemRes.updateOrderItem(orderItem1);
				OrderItemRes.capnhatChiTietDonHang(orderItem1);

			}
			else if(sub != null)
            {
                orderItem1.Quantity--;
                if(orderItem1.Quantity != 0)
                {
                   // orderItem1.TotalPrice = price * orderItem1.Quantity;
                    //OrderItemRes.updateOrderItem(orderItem1);
                    OrderItemRes.capnhatChiTietDonHang(orderItem1);
                }
                else
                {
                   // OrderItemRes.deleteOrderItem(orderItem1.ItemID);
                    OrderItemRes.xoaChiTietDonHang(orderItem1.ItemID);
                }
            }
            return RedirectToAction("Cart");
        }
    }
}
