#pragma checksum "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6174307c76aa9ac7a69b8192b742f362ba55a6e30d92c6a9cb305e9526bde1c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Book_Details), @"mvc.1.0.view", @"/Views/Book/Details.cshtml")]
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
#line 1 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\_ViewImports.cshtml"
using WebStore

#nullable disable
    ;
#nullable restore
#line 1 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
 using WebStore.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"6174307c76aa9ac7a69b8192b742f362ba55a6e30d92c6a9cb305e9526bde1c2", @"/Views/Book/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"0ca74d9d3d8f0fda11e57151523a5ee9557a88a1c5aba4211246523de85a92d1", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Book_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddToCart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("add_to_cart"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n<div id=\"main\">\r\n    <div class=\"inner\">\r\n        <h1>");
            Write(
#nullable restore
#line 10 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
             Model.bookdetail.BookName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" <span class=\"pull-right\"><del></del>");
            Write(
#nullable restore
#line 10 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                                                            Model.bookdetail.Price

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" đ</span></h1>\r\n\r\n        <div class=\"container-fluid\">\r\n            <div class=\"row\">\r\n                <div class=\"col-md-5\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 379, "\"", 416, 2);
            WriteAttributeValue("", 385, "/images/", 385, 8, true);
            WriteAttributeValue("", 393, 
#nullable restore
#line 15 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                       Model.bookdetail.Image

#line default
#line hidden
#nullable disable
            , 393, 23, false);
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid\"");
            BeginWriteAttribute("alt", " alt=\"", 435, "\"", 441, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                </div>\r\n\r\n                <div class=\"col-md-7\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-sm-3\">Thể loại:</div>\r\n");
#nullable restore
#line 21 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                         foreach (var type in Model.booktypeNAV)
                        {
                            if (type.BookTypeID == Model.bookdetail.BookTypeID)
                            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                                <div class=\"col-sm-9\">");
            Write(
#nullable restore
#line 25 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                                       type.BookTypeName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</div>\r\n");
#nullable restore
#line 26 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"

                            }
                        }

#line default
#line hidden
#nullable disable

            WriteLiteral("                        <div class=\"col-sm-3\">Tác giả:</div>\r\n                        <div class=\"col-sm-9\">");
            Write(
#nullable restore
#line 30 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                               Model.bookdetail.Author

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</div>\r\n                        <div class=\"col-sm-3\">Nhà xuất bản:</div>\r\n                        <div class=\"col-sm-9\">");
            Write(
#nullable restore
#line 32 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                               Model.bookdetail.Nxb

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</div>\r\n                    </div>\r\n\r\n                    <div class=\"row\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6174307c76aa9ac7a69b8192b742f362ba55a6e30d92c6a9cb305e9526bde1c28295", async() => {
                WriteLiteral("\r\n                            <div class=\"col-sm-8\">\r\n                                <input hidden type=\"text\" name=\"BookID\"");
                BeginWriteAttribute("value", " value=\"", 1527, "\"", 1559, 1);
                WriteAttributeValue("", 1535, 
#nullable restore
#line 38 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                                                                Model.bookdetail.BookID

#line default
#line hidden
#nullable disable
                , 1535, 24, false);
                EndWriteAttribute();
                WriteLiteral(@"/>
                                <label class=""control-label"">Quantity</label>
                                <div class=""row"">
                                    <div class=""col-sm-6"">
                                        <div class=""form-group"">
                                            <input type=""text"" name=""quantity"" id=""quantity"">
                                            <span class=""text-danger field-validation-error"" id=""add_to_cart_alert"">vui lòng nhập số lượng</span>

                                        </div>
                                    </div>
                                    <div class=""col-sm-6"">
                                        <button type=""submit"" class=""primary"">Add to Cart</button>
                                    </div>
                                </div>
                            </div>
                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-sm-12\">Mô tả:</div>\r\n                        <div class=\"col-sm-1\"></div>\r\n                        <div class=\"col-sm-11\">");
            Write(
#nullable restore
#line 59 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                                Model.bookdetail.Description

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(@"</div>
                    </div>
                </div>
            </div>


        </div>
        <br>
        <br>

        <div class=""container-fluid"">
            <h2>Other Products</h2>
            <!-- Products -->
            <section class=""tiles"">
");
#nullable restore
#line 73 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                 foreach (var item in Model.lstBook)
                {

#line default
#line hidden
#nullable disable

            WriteLiteral("                    <article class=\"style1\">\r\n                        <span class=\"image\">\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 3200, "\"", 3225, 2);
            WriteAttributeValue("", 3206, "/images/", 3206, 8, true);
            WriteAttributeValue("", 3214, 
#nullable restore
#line 77 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                               item.Image

#line default
#line hidden
#nullable disable
            , 3214, 11, false);
            EndWriteAttribute();
            WriteLiteral(" width=\"300\" height=\"325\"");
            BeginWriteAttribute("alt", " alt=\"", 3251, "\"", 3257, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                        </span>\r\n                        <a");
            BeginWriteAttribute("href", " href=\"", 3322, "\"", 3385, 1);
            WriteAttributeValue("", 3329, 
#nullable restore
#line 79 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                  Url.Action("Details", "Book", new {id = @item.BookID })

#line default
#line hidden
#nullable disable
            , 3329, 56, false);
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <h2>\r\n                                ");
            Write(
#nullable restore
#line 81 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                 item.BookName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                            </h2>\r\n                            <p><del>");
            Write(
#nullable restore
#line 83 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                     item.Price

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" đ</del> <strong>");
            Write(
#nullable restore
#line 83 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                                                                 item.Price

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" đ</strong></p>\r\n                        </a>\r\n                    </article>\r\n");
#nullable restore
#line 86 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Book\Details.cshtml"
                }

#line default
#line hidden
#nullable disable

            WriteLiteral("            </section>\r\n        </div>\r\n    </div>\r\n</div>");
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