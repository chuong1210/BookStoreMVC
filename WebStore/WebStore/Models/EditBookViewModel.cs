using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.Models;

namespace WebStore.Models
{
    public class EditBookViewModel
    {
        //[DisplayName("Sách")]
        public SachDTO Book { get; set; }
        public SelectList BookTypes { get; set; }
        public SelectList Authors { get; set; }
        //public List<SelectListItem> Authors { get; set; }
        public SelectList Publishers { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> AuthorIds { get; set; } // crucial



    }

}
