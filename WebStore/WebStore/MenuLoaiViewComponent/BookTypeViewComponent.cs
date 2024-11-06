using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WedStore.Repositories;

namespace WebStore.MenuLoaiViewComponent
{
    public class TheLoaiViewComponent : ViewComponent
    {

        public TheLoaiViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bookTypes =  TheLoaiSachDB.ListTheLoai(); // Replace with actual service method
            return View(bookTypes); // Pass the data to the view
        }
    }
}
