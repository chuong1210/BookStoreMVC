#pragma checksum "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4c9323eb68eb6bfb67dbcfaccd0f9af1818cd34ae9863fd9c1a314d5eeb14b34"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Book_BookType), @"mvc.1.0.view", @"/Views/Book/BookType.cshtml")]
namespace AspNetCore
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\_ViewImports.cshtml"
using WebStore

#nullable disable
    ;
#nullable restore
#line 1 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
 using WebStore.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"4c9323eb68eb6bfb67dbcfaccd0f9af1818cd34ae9863fd9c1a314d5eeb14b34", @"/Views/Book/BookType.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"0ca74d9d3d8f0fda11e57151523a5ee9557a88a1c5aba4211246523de85a92d1", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Book_BookType : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
  
    ViewData["Title"] = "BookType";

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n<div id=\"main\">\r\n    <div class=\"inner\">\r\n\r\n        <div class=\"image main\">\r\n            <img src=\"/images/banner-image-6-1920x500.jpg\" class=\"img-fluid\"");
            BeginWriteAttribute("alt", " alt=\"", 242, "\"", 248, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n        </div>\r\n        <h1>");
            Write(
#nullable restore
#line 14 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
             Model.booktype.BookTypeName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</h1>\r\n\r\n        <hr />\r\n        <!-- Products -->\r\n        <section class=\"tiles\">\r\n");
#nullable restore
#line 19 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
             if (Model.booktypelist.Count <= 0)
            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                <span class=\"mt-2\">\r\n                    Hiện cửa hàng ko có sách có thể loại ");
            Write(
#nullable restore
#line 22 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                                          Model.booktype.BookTypeName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n\r\n                </span>\r\n");
#nullable restore
#line 25 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
            }

#line default
#line hidden
#nullable disable

#nullable restore
#line 26 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
             foreach (var item in Model.booktypelist)
            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                <article class=\"style1\">\r\n                    <span class=\"image\">\r\n                        <img");
            BeginWriteAttribute("src", " src=\"", 807, "\"", 832, 2);
            WriteAttributeValue("", 813, "/images/", 813, 8, true);
            WriteAttributeValue("", 821, 
#nullable restore
#line 30 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                           item.Image

#line default
#line hidden
#nullable disable
            , 821, 11, false);
            EndWriteAttribute();
            WriteLiteral(" width=\"300\" height=\"325\"");
            BeginWriteAttribute("alt", " alt=\"", 858, "\"", 864, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    </span>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 921, "\"", 984, 1);
            WriteAttributeValue("", 928, 
#nullable restore
#line 32 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                              Url.Action("Details", "Book", new {id = @item.BookID })

#line default
#line hidden
#nullable disable
            , 928, 56, false);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <h2>\r\n                            ");
            Write(
#nullable restore
#line 34 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                             item.BookName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                        </h2>\r\n\r\n                        <p><del>");
            Write(
#nullable restore
#line 37 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                 item.Price

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" đ</del> <strong>");
            Write(
#nullable restore
#line 37 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                                             item.Price

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" đ</strong></p>\r\n                    </a>\r\n                </article>\r\n");
#nullable restore
#line 40 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
            }

#line default
#line hidden
#nullable disable

            WriteLiteral("        </section>\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-6\">\r\n                <div class=\"pagination\">\r\n");
#nullable restore
#line 45 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                     for (int i = 1; i <= Model.maxpage; i++)
                    {
                        if (i == Model.currentpage)
                        {

#line default
#line hidden
#nullable disable

            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\"", 1573, "\"", 1624, 4);
            WriteAttributeValue("", 1580, "/Book/BookType/", 1580, 15, true);
            WriteAttributeValue("", 1595, 
#nullable restore
#line 49 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                                     Model.booktype.BookTypeID

#line default
#line hidden
#nullable disable
            , 1595, 26, false);
            WriteAttributeValue("", 1621, "/", 1621, 1, true);
            WriteAttributeValue("", 1622, 
#nullable restore
#line 49 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                                                                i

#line default
#line hidden
#nullable disable
            , 1622, 2, false);
            EndWriteAttribute();
            WriteLiteral(" class=\"active\">");
            Write(
#nullable restore
#line 49 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                                                                                   i

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</a>\r\n");
#nullable restore
#line 50 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable

            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\"", 1763, "\"", 1814, 4);
            WriteAttributeValue("", 1770, "/Book/BookType/", 1770, 15, true);
            WriteAttributeValue("", 1785, 
#nullable restore
#line 53 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                                     Model.booktype.BookTypeID

#line default
#line hidden
#nullable disable
            , 1785, 26, false);
            WriteAttributeValue("", 1811, "/", 1811, 1, true);
            WriteAttributeValue("", 1812, 
#nullable restore
#line 53 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                                                                i

#line default
#line hidden
#nullable disable
            , 1812, 2, false);
            EndWriteAttribute();
            WriteLiteral(">");
            Write(
#nullable restore
#line 53 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                                                                                    i

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</a>\r\n");
#nullable restore
#line 54 "C:\Users\chuon\Downloads\BookStore\BookStore\WebStore\WebStore\Views\Book\BookType.cshtml"
                        }
                    }

#line default
#line hidden
#nullable disable

            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
