#pragma checksum "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "64ba7203e0cbbe8acd7e5aa7dda07ab75e70e32b85e2293811315b041274ab0b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__HeaderComponent), @"mvc.1.0.view", @"/Views/Shared/_HeaderComponent.cshtml")]
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
#line 2 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\_ViewImports.cshtml"
using WebStore.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"64ba7203e0cbbe8acd7e5aa7dda07ab75e70e32b85e2293811315b041274ab0b", @"/Views/Shared/_HeaderComponent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"0ca74d9d3d8f0fda11e57151523a5ee9557a88a1c5aba4211246523de85a92d1", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__HeaderComponent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Logout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<header id=\"header\">\r\n\t<div class=\"inner\">\r\n\t\t<!-- Logo -->\r\n\t\t<a");
            BeginWriteAttribute("href", " href=\"", 67, "\"", 102, 1);
            WriteAttributeValue("", 74, 
#nullable restore
#line 5 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
            Url.Action("Index", "Home")

#line default
#line hidden
#nullable disable
            , 74, 28, false);
            EndWriteAttribute();
            WriteLiteral(@" class=""logo"">
			<span class=""fa fa-book""></span> <span class=""title"">Book Online Store Website</span>
		</a>
		<!-- Nav -->
		<nav>
			<ul>
				<li><a href=""#menu"">Menu</a></li>
			</ul>
		</nav>
	</div>
</header>
<nav id=""menu"">
	<h2>Menu</h2>

	<h4>");
            Write(
#nullable restore
#line 19 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
      User.Identity.Name

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</h4>\r\n\t<ul>\r\n\t\t<li><a");
            BeginWriteAttribute("href", " href=\"", 411, "\"", 446, 1);
            WriteAttributeValue("", 418, 
#nullable restore
#line 21 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
                Url.Action("Index", "Home")

#line default
#line hidden
#nullable disable
            , 418, 28, false);
            EndWriteAttribute();
            WriteLiteral(" class=\"active\">Home</a></li>\r\n");
#nullable restore
#line 22 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
   if (this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin"))
		{

#line default
#line hidden
#nullable disable

            WriteLiteral("\t\t\t<li><a");
            BeginWriteAttribute("href", " href=\"", 565, "\"", 602, 1);
            WriteAttributeValue("", 572, 
#nullable restore
#line 24 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
                 Url.Action("Index", "QuanLy")

#line default
#line hidden
#nullable disable
            , 572, 30, false);
            EndWriteAttribute();
            WriteLiteral(">Manager</a></li>\r\n");
#nullable restore
#line 25 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
		}

#line default
#line hidden
#nullable disable

#nullable restore
#line 26 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
   if (User.Identity.IsAuthenticated)
		{

#line default
#line hidden
#nullable disable

            WriteLiteral("\t\t\t<li><a");
            BeginWriteAttribute("href", " href=\"", 680, "\"", 715, 1);
            WriteAttributeValue("", 687, 
#nullable restore
#line 28 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
                 Url.Action("Cart", "Order")

#line default
#line hidden
#nullable disable
            , 687, 28, false);
            EndWriteAttribute();
            WriteLiteral(">Giỏ hàng</a></li>\r\n\t\t\t<li><a");
            BeginWriteAttribute("href", " href=\"", 745, "\"", 786, 1);
            WriteAttributeValue("", 752, 
#nullable restore
#line 29 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
                 Url.Action("OrdersList", "Order")

#line default
#line hidden
#nullable disable
            , 752, 34, false);
            EndWriteAttribute();
            WriteLiteral(">Đơn hàng</a></li>\r\n\t\t\t<li>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "64ba7203e0cbbe8acd7e5aa7dda07ab75e70e32b85e2293811315b041274ab0b7589", async() => {
                WriteLiteral("Logout");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n");
#nullable restore
#line 31 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"

		}
		else
		{

#line default
#line hidden
#nullable disable

            WriteLiteral("\t\t\t<li><a");
            BeginWriteAttribute("href", " href=\"", 908, "\"", 946, 1);
            WriteAttributeValue("", 915, 
#nullable restore
#line 35 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
                 Url.Action("Login", "Account")

#line default
#line hidden
#nullable disable
            , 915, 31, false);
            EndWriteAttribute();
            WriteLiteral(">Đăng nhập</a></li>\r\n");
#nullable restore
#line 36 "C:\Users\chuon\Downloads\QuanLyBanSach\WebStore\WebStore\Views\Shared\_HeaderComponent.cshtml"
		}

#line default
#line hidden
#nullable disable

            WriteLiteral("\t</ul>\r\n</nav>");
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