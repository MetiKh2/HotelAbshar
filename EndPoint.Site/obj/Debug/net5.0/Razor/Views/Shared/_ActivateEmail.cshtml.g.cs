#pragma checksum "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Shared\_ActivateEmail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ccef47be15f9d55c0cf1e02949a9de39fcd028bd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ActivateEmail), @"mvc.1.0.view", @"/Views/Shared/_ActivateEmail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccef47be15f9d55c0cf1e02949a9de39fcd028bd", @"/Views/Shared/_ActivateEmail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87685c89e84078b3e134b89a928accf3d0f04a39", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__ActivateEmail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HotelAbshar.Domain.Entities.User.User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n<div style=\"direction: rtl; padding: 20px\">\r\n    <h2>");
#nullable restore
#line 6 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Shared\_ActivateEmail.cshtml"
   Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" عزیز !</h2>\r\n    <p>با تشکر از ثبت نام شما در  هتل سرای آبشار ، جهت ادامه کار میبایست حساب کاربری خود را فعال کنید</p>\r\n    <p>\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 260, "\"", 331, 2);
            WriteAttributeValue("", 267, "https://localhost:44358/Account/ActiveUser/", 267, 43, true);
#nullable restore
#line 9 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Shared\_ActivateEmail.cshtml"
WriteAttributeValue("", 310, Model.ActivationCode, 310, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">فعالسازی حساب کاربری</a>\r\n    </p>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HotelAbshar.Domain.Entities.User.User> Html { get; private set; }
    }
}
#pragma warning restore 1591
