#pragma checksum "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "f7d577dbfea1c0ac31a2874b2c1f82c9624a5c6c9736bca59a4ded9fa2c72171"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_QuanLy_DeleteOrder), @"mvc.1.0.view", @"/Views/QuanLy/DeleteOrder.cshtml")]
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
#line 1 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
 using WebStore.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"f7d577dbfea1c0ac31a2874b2c1f82c9624a5c6c9736bca59a4ded9fa2c72171", @"/Views/QuanLy/DeleteOrder.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"0ca74d9d3d8f0fda11e57151523a5ee9557a88a1c5aba4211246523de85a92d1", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_QuanLy_DeleteOrder : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteOrder", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ManagerOrder", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
  
    ViewData["Title"] = "DeleteOrder";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
#nullable disable

            WriteLiteral(@"
<h1>Xóa đơn hàng</h1>

<h3>Bạn có muốn xóa đơn hàng này không?</h3>
<div>
    <div class=""row"">
        <div class=""col-sm-8"">
            <div class=""row"">
                <table class=""table"">
                    <thead>
                        <tr>
                            <th class=""col-md-2""></th>
                            <th class=""col-md-6"">
                                Tên sách
                            </th>
                            <th class=""col-md-4"">
                                Số lượng
                            </th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>

");
#nullable restore
#line 31 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                         foreach (var item in @Model.orderItem)
                        {

#line default
#line hidden
#nullable disable

            WriteLiteral("                            <tr>\r\n");
#nullable restore
#line 34 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                                 foreach (var book in Model.lstBook)
                                {
                                    if (item.BookID == book.BookID)
                                    {

#line default
#line hidden
#nullable disable

            WriteLiteral("                                        <td>\r\n                                            <span class=\"image\">\r\n                                                <img");
            BeginWriteAttribute("src", " src=\"", 1353, "\"", 1378, 2);
            WriteAttributeValue("", 1359, "/images/", 1359, 8, true);
            WriteAttributeValue("", 1367, 
#nullable restore
#line 40 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                                                                   book.Image

#line default
#line hidden
#nullable disable
            , 1367, 11, false);
            EndWriteAttribute();
            WriteLiteral(" height=\"120\" width=\"110\"");
            BeginWriteAttribute("alt", " alt=\"", 1404, "\"", 1410, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                            </span>\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
            Write(
#nullable restore
#line 44 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                                             book.BookName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                                        </td>\r\n");
#nullable restore
#line 46 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                                    }
                                }

#line default
#line hidden
#nullable disable

            WriteLiteral("                                <td>\r\n                                    ");
            Write(
#nullable restore
#line 49 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                                     item.Quantity

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 52 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                        }

#line default
#line hidden
#nullable disable

            WriteLiteral(@"                    </tbody>
                </table>
            </div>
        </div>
        <div class=""col-sm-4"">
            <div class=""row"">
                <label class=""col-sm-3"">
                    InfoOrderID
                </label>
                <div class=""col-sm-9"">
                    ");
            Write(
#nullable restore
#line 63 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                     Model.infoOrder.InfoOrderID

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </div>\r\n                <label class=\"col-sm-3\">\r\n                    Name\r\n                </label>\r\n                <div class=\"col-sm-9\">\r\n                    ");
            Write(
#nullable restore
#line 69 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                     Model.infoOrder.Name

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </div>\r\n                <label class=\"col-sm-3\">\r\n                    Email\r\n                </label>\r\n                <div class=\"col-sm-9\">\r\n                    ");
            Write(
#nullable restore
#line 75 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                     Model.infoOrder.Email

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </div>\r\n                <label class=\"col-sm-3\">\r\n                    Phone\r\n                </label>\r\n                <div class=\"col-sm-9\">\r\n                    ");
            Write(
#nullable restore
#line 81 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                     Model.infoOrder.Phone

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </div>\r\n                <label class=\"col-sm-3\">\r\n                    Address\r\n                </label>\r\n                <div class=\"col-sm-9\">\r\n                    ");
            Write(
#nullable restore
#line 87 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                     Model.infoOrder.Address

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </div>\r\n                <label class=\"col-sm-3\">\r\n                    TotalPrice\r\n                </label>\r\n                <div class=\"col-sm-9\">\r\n                    ");
            Write(
#nullable restore
#line 93 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                     Model.infoOrder.TotalPrice

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </div>\r\n                <label class=\"col-sm-3\">\r\n                    Status\r\n                </label>\r\n                <div class=\"col-sm-9\">\r\n                    ");
            Write(
#nullable restore
#line 99 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                     Model.infoOrder.Status

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7d577dbfea1c0ac31a2874b2c1f82c9624a5c6c9736bca59a4ded9fa2c7217112222", async() => {
                WriteLiteral("\r\n                <input hidden name=\"InfoOrderID\"");
                BeginWriteAttribute("value", " value=\"", 3649, "\"", 3685, 1);
                WriteAttributeValue("", 3657, 
#nullable restore
#line 103 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\QuanLy\DeleteOrder.cshtml"
                                                         Model.infoOrder.InfoOrderID

#line default
#line hidden
#nullable disable
                , 3657, 28, false);
                EndWriteAttribute();
                WriteLiteral(" />\r\n                <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" />\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7d577dbfea1c0ac31a2874b2c1f82c9624a5c6c9736bca59a4ded9fa2c7217114370", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n</div>\r\n");
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