#pragma checksum "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Shared\Components\ProductCategoriesComponent\ProductCategories.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7f417c037ae67fdfc13dd490208560cb12bad241"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_ProductCategoriesComponent_ProductCategories), @"mvc.1.0.view", @"/Views/Shared/Components/ProductCategoriesComponent/ProductCategories.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\_ViewImports.cshtml"
using EndPoint.Site;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\_ViewImports.cshtml"
using EndPoint.Site.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7f417c037ae67fdfc13dd490208560cb12bad241", @"/Views/Shared/Components/ProductCategoriesComponent/ProductCategories.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87685c89e84078b3e134b89a928accf3d0f04a39", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_ProductCategoriesComponent_ProductCategories : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<HotelAbshar.Domain.Entities.Product.Category>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<li class=""dropdown"">
    <a class=""dropdown-toggle"" href=""/Products"" data-toggle=""dropdown"" role=""button"" aria-haspopup=""true"" aria-expanded=""false"">(فروشگاه سایت) دسته بندی ها</a>
    <div class=""dropdown-menu"">
        <div class=""row"">
            <div class=""col-md-12"">
                <ul class=""dropdown-meganav-list-items"">
                    <li><a href=""/Products"">همه محصولات</a></li>
");
#nullable restore
#line 9 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Shared\Components\ProductCategoriesComponent\ProductCategories.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <li>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 597, "\"", 651, 2);
            WriteAttributeValue("", 604, "/Products/Index?parentCategory=", 604, 31, true);
#nullable restore
#line 12 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Shared\Components\ProductCategoriesComponent\ProductCategories.cshtml"
WriteAttributeValue("", 635, item.CategoryID, 635, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 12 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Shared\Components\ProductCategoriesComponent\ProductCategories.cshtml"
                                                                                 Write(item.CategoryName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </a>\r\n                        </li>\r\n");
#nullable restore
#line 14 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Shared\Components\ProductCategoriesComponent\ProductCategories.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</li>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<HotelAbshar.Domain.Entities.Product.Category>> Html { get; private set; }
    }
}
#pragma warning restore 1591