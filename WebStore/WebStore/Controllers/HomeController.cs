using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WedStore.Repositories;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
          //  var lstBook = SachDB.GetBookWithSelling();
            var lstBook = SachDB.LaySachTheoSoLuongTon();
          //  var lstBookType = TheLoaiSachDB.GetAllType();
            var lstBookType = TheLoaiSachDB.ListTheLoai();
            List<SachDTO> books = new List<SachDTO>();
            for (int i = 0; i < 6; i++)
            {
                books.Add(lstBook[i]);
            }
            dynamic dy = new ExpandoObject();
            dy.book = books;
            dy.booktypeNAV = lstBookType;
            return View(dy);
        }
        
        public IActionResult Privacy()
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();

            return View(dy);
        }
        public IActionResult About()
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
            return View(dy);
        }
        public IActionResult Contact()
        {
            dynamic dy = new ExpandoObject();
            dy.booktypeNAV = TheLoaiSachDB.ListTheLoai();
            return View(dy);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
