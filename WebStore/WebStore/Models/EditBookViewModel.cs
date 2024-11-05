using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.Models;

namespace WedStore.Models
{
    public class EditBookViewModel
    {
        [DisplayName("Sách")]
        public SachDTO book { get; set; }
        public IEnumerable<SelectListItem> BookTypes { get; set; }
        public IEnumerable<SelectListItem> NXBs { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
        public string ErrorMessage { get; set; }


    }

}
