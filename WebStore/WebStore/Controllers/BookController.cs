using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WedStore.Repositories;

namespace WedStore.Controllers
{

    
    public class BookController : Controller
    {
		// GET: BookController
		public ActionResult Index(int? page, string tenSach)
		{
			List<SachDTO> books;
			int pageSize = 6;
			int currentPage = page ?? 1; // Nếu page là null thì gán page = 1
            dynamic dy = new ExpandoObject();

            // Lấy danh sách tất cả sách từ cơ sở dữ liệu
            var lstBook = SachDB.LayTatCaSach();
			var lstBookType = TheLoaiSachDB.ListTheLoai();

			// Nếu tìm kiếm theo tên sách
		
		
				// Phân trang
				books = lstBook.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

				// Tính tổng số trang
				int totalBooks = lstBook.Count;
				int maxPage = (int)Math.Ceiling((double)totalBooks / pageSize); // Số trang tối đa

				// Nếu currentPage lớn hơn số trang tối đa, gán lại currentPage là 1
				if (currentPage > maxPage)
				{
					currentPage = 1;
				}

				// Tạo đối tượng dynamic để truyền vào view
            if (!string.IsNullOrEmpty(tenSach))
            {
                // Tìm kiếm sách theo tên
                books = SachDB.TimKiemSachTheoTen(tenSach);
                dy.book = books;

            }

            else
            {
                dy.book = books;

            }
            dy.booktypeNAV = lstBookType;
				dy.maxpage = maxPage;
				dy.currentpage = currentPage;

				return View(dy); // Trả về đối tượng dynamic
			
		}

		public ActionResult BookType(string ID,int? page)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }
            //

            //var lstBookTypeList = TheLoaiSachDB.GetAllType();
            var lstBookTypeList = TheLoaiSachDB.ListTheLoai();
           // var lstBook = SachDB.GetTypeList(ID);//danh sách sản phẩm theo book type
            var lstBook = SachDB.LaySachTheoTheLoai(ID);//danh sách sản phẩm theo book type
            // lấy 6 sản phẩm trong danh sách
            List<SachDTO> books = new List<SachDTO>();
            int i;
            int bookPerPage = 6;
            for (i = (int)((page-1)*bookPerPage); i < page*bookPerPage; i++)
            {
                if(lstBook.Count == i)
                {
                    break;
                }
                books.Add(lstBook[i]);
            }
            BookType BookType= TheLoaiSachDB.LayThongTinTheLoai(ID);
            if (books.Count>0)
            {
                 BookType = new BookType { BookTypeID = ID, BookTypeName = books[0].BookTypeName };

            }
          //  var BookType = TheLoaiSachDB.BookTypeWithID(ID);

            //chia so luong
            int MaxPage = lstBook.Count / bookPerPage ;
            int tmp = lstBook.Count % bookPerPage;// so du
            if (tmp >= 1) MaxPage += 1;

            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = lstBookTypeList;
            dy.booktypelist = books;
            dy.booktype = BookType;
            dy.maxpage = MaxPage;
            dy.currentpage = page;
            return View(dy);
        }
        // GET: BookController/Details/id
        public ActionResult Details(string id)
        {

           // var book = SachDB.BookWithID(id);
            var book = SachDB.SachTheoId(id);
            dynamic dy = new ExpandoObject();
         //   dy.booktypeNAV = TheLoaiSachDB.GetAllType();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
            dy.bookdetail = book;
           // var lstBookWithType = SachDB.GetAll();
            var lstBookWithType = SachDB.LayTatCaSach();
            List<SachDTO> lstBook = new List<SachDTO>();
            int tr = 0;
            var it = lstBookWithType.Single(r => r.BookID == book.BookID);
            if (lstBookWithType.Remove(it)) { 
                tr = 1;
            }

            Random rnd = new Random();
            for (int i = 1; i <= 3; i++) 
            {
                int random = rnd.Next(lstBookWithType.Count);
                int j = 0;
                foreach (var item in lstBookWithType)
                {
                    if(j == random)
                    {
                        lstBook.Add(item);
                        lstBookWithType.Remove(item);
                        break;
                    }
                    j++;
                }
            }
            dy.lstBook = lstBook;

            return View(dy);
        }


		public IActionResult Search(string theloai_id, int giaMin = 0, int giaMax = 1000000)
		{
			// Gọi phương thức tìm kiếm từ SachDB
			var books = SachDB.TimKiemSachTheoTheLoaiVaGia(theloai_id, giaMin, giaMax);
			return View(books); // Trả về danh sách sách tìm được
		}


	}
}
