#pragma checksum "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "266fd9718d1be031519dd2396de825fdcd6df882"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Products_ShowProduct), @"mvc.1.0.view", @"/Views/Products/ShowProduct.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"266fd9718d1be031519dd2396de825fdcd6df882", @"/Views/Products/ShowProduct.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87685c89e84078b3e134b89a928accf3d0f04a39", @"/Views/_ViewImports.cshtml")]
    public class Views_Products_ShowProduct : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HotelAbshar.Domain.Entities.Product.Product>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("mySlides"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:100%"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("formAddProductComment"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Products", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddComment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-method", new global::Microsoft.AspNetCore.Html.HtmlString("post"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-mode", new global::Microsoft.AspNetCore.Html.HtmlString("replace"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-update", new global::Microsoft.AspNetCore.Html.HtmlString("#listComment"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-success", new global::Microsoft.AspNetCore.Html.HtmlString("Success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/ajax/jquery.unobtrusive-ajax.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
  
    ViewData["Title"] = "ShowProduct";
    Layout = "~/Views/Shared/_Layout.cshtml";
    List<HotelAbshar.Domain.Entities.Product.ProductImage> Images = ViewBag.Images;
    List<HotelAbshar.Domain.Entities.Product.ProductFeature> Features = ViewBag.Features;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""theme-hero-area"">
    <div class=""theme-hero-area-bg-wrap"">
        <div class=""theme-hero-area-bg-pattern theme-hero-area-bg-pattern-ultra-light"" style=""background-image:url(../../wwwroot/Hotels/SliderHotelImages/6c537c21a0fb4bb18e1313987a761947.jpg);""></div>
        <div class=""theme-hero-area-grad-mask""></div>
    </div>
    <div class=""theme-hero-area-body"">
        <div class=""container"">
            <div class=""theme-item-page-header _pb-50 theme-item-page-header-white"">
                <div class=""theme-item-page-header-body"">
                    <ul class=""theme-item-page-header-stars"">
                        <li>
                            <i class=""fa fa-star""></i>
                        </li>
                        <li>
                            <i class=""fa fa-star""></i>
                        </li>
                        <li>
                            <i class=""fa fa-star""></i>
                        </li>
                        <li>
               ");
            WriteLiteral(@"             <i class=""fa fa-star""></i>
                        </li>
                        <li>
                            <i class=""fa fa-star""></i>
                        </li>
                    </ul>
                    <h2 class=""theme-item-page-header-title"">");
#nullable restore
#line 36 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                                        Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h2>\r\n                    <div class=\"theme-item-page-header-price\">\r\n                        <div class=\"theme-item-page-header-price-body\">\r\n                            <p class=\"_fs-sm\"> قیمت   </p>\r\n                            <p class=\"_fs-xl\">");
#nullable restore
#line 40 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                         Write(Model.Price.ToString("n0"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>




<div class=""theme-page-section theme-page-section-gray"">
    <div class=""container"">
        <div class=""row row-col-static"" id=""sticky-parent"">
            <div class=""col-md-9 "">

                <div class=""w3-content w3-section"" style=""max-width:500px"">
");
#nullable restore
#line 59 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                     foreach (var item in Images)
                    {




#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "266fd9718d1be031519dd2396de825fdcd6df88210516", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2463, "~/Products/ShowSiteImage/", 2463, 25, true);
#nullable restore
#line 64 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
AddHtmlAttributeValue("", 2488, item.Src, 2488, 9, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 65 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"


                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                </div>
                <div class=""theme-item-page-tabs _mb-30"">
                    <div class=""tabbable"">
                        <ul class=""nav nav-tabs nav-default nav-no-br nav-sqr nav-mob-inline"" role=""tablist"">
                            <li class=""active"" role=""presentation"">
                                <a class=""_ph-30"" aria-controls=""HotelPageTabs-1"" role=""tab"" data-toggle=""tab"" href=""#HotelPageTabs-1"">درباره کالا</a>
                            </li>
                            <li role=""presentation"">
                                <a class=""_ph-30"" aria-controls=""HotelPageTabs-3"" role=""tab"" data-toggle=""tab"" href=""#HotelPageTabs-2"">نظرات کاربران</a>
                            </li>
                            <li role=""presentation"">
                                <a class=""_ph-30"" aria-controls=""HotelPageTabs-4"" role=""tab"" data-toggle=""tab"" href=""#HotelPageTabs-3"">ویژگی ها</a>
                            </li>
                        </ul>
                  ");
            WriteLiteral(@"      <div class=""tab-content _p-30 _bg-w"">
                            <div class=""tab-pane active"" id=""HotelPageTabs-1"" role=""tab-panel"">
                                <div class=""theme-item-page-desc"">
                                    <p style=""text-align:right"">");
#nullable restore
#line 85 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                                           Write(Html.Raw(Model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                </div>
                            </div>
                            <div class=""tab-pane"" id=""HotelPageTabs-2"" role=""tab-panel"">
                                <div class=""theme-reviews"">
                                    <div class=""theme-reviews-score theme-reviews-score-hor"">
");
            WriteLiteral(@"                                    </div>
                                    <div id=""listComment"" class=""theme-reviews-list"">
                                       
                                    </div>
                                </div>
                            </div>
                            <div class=""tab-pane"" id=""HotelPageTabs-3"" role=""tab-panel"">
                                <div class=""theme-item-page-facilities"">
                                    <div class=""row"">
");
#nullable restore
#line 154 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                         for (int i = 0; i < Features.Count + 1; i++)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <div class=\"col-md-3 \">\r\n\r\n\r\n");
#nullable restore
#line 159 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                                 foreach (var item in Features.Skip(i * 3).Take(3))
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                    <div class=""theme-item-page-facilities-item"">
                                                        <i class=""fa fa-empire theme-item-page-facilities-item-icon""></i>
                                                        <h5 class=""theme-item-page-facilities-item-title"">");
#nullable restore
#line 163 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                                                                                     Write(item.DisPlayName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 163 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                                                                                                         Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                                    </div>\r\n");
#nullable restore
#line 165 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            </div>\r\n");
#nullable restore
#line 168 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <h2 style=""color:blue;text-shadow:2px 2px 5px red"">نظر دهید</h2>
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "266fd9718d1be031519dd2396de825fdcd6df88217540", async() => {
                WriteLiteral("\r\n                    <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 11051, "\"", 11075, 1);
#nullable restore
#line 184 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
WriteAttributeValue("", 11059, Model.ProductID, 11059, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"productId\" />\r\n                    <textarea id=\"TextComment\" name=\"text\" rows=\"10\" cols=\"75\" placeholder=\"متن نظر ...\"></textarea>\r\n\r\n                    <input type=\"submit\" value=\"ثبت نظر\" class=\"btn btn-success\" />\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n            </div>\r\n\r\n\r\n            <div class=\"col-md-3 \">\r\n                <div class=\"sticky-col\"");
            BeginWriteAttribute("style", " style=\"", 11430, "\"", 11438, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    <div class=""theme-sidebar-section _mb-10 _mob-h"">
                        <h5 class=""theme-sidebar-section-title"">جزییات کالا</h5>
                        <ul class=""theme-sidebar-section-summary-list"">
                            <li>برند : <span>");
#nullable restore
#line 198 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                        Write(Model.Brand);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></li>\r\n                            <li>موجودی : <span>");
#nullable restore
#line 199 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                          Write(Model.Invertory);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></li>\r\n                            <li>دسته بندی : <span>");
#nullable restore
#line 200 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                             Write(ViewBag.FullCategory);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></li>\r\n                            <li class=\"btn btn-warning\">\r\n                                <a");
            BeginWriteAttribute("href", " href=\"", 11993, "\"", 12033, 2);
            WriteAttributeValue("", 12000, "/Order/AddToCart/", 12000, 17, true);
#nullable restore
#line 202 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
WriteAttributeValue("", 12017, Model.ProductID, 12017, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-warning""><p class=""text text-success"">افزودن به سبد خرید</p></a>
                            </li>
                            <li></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<script>
    const formAddProductComment = document.querySelector('#formAddProductComment');
    formAddProductComment.addEventListener('submit', event => {

        let TextComment = document.getElementById('TextComment').value;


        if (TextComment == """") {
            alert(""متن نظر را وارد کنید"");
            event.preventDefault();
        }



        // actual logic, e.g. validate the form
        console.log('Form submission cancelled.');
    });

</script>


<script>

    var myIndex = 0;
    carousel();

    function carousel() {
        var i;
        var x = document.getElementsByClassName(""mySlides"");
        for (i = 0; i < x.length; i++) {
            x[i].style.disp");
            WriteLiteral("lay = \"none\";\r\n        }\r\n        myIndex++;\r\n        if (myIndex > x.length) { myIndex = 1 }\r\n        x[myIndex - 1].style.display = \"block\";\r\n        setTimeout(carousel, 2000); // Change image every 2 seconds\r\n    }\r\n\r\n</script>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "266fd9718d1be031519dd2396de825fdcd6df88223763", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    <script>\r\n        function Success() {\r\n\r\n\r\n\r\n\r\n            $(\"#TextComment\").val(\"\");\r\n          \r\n        }\r\n\r\n        $(function () {\r\n            $(\"#listComment\").load(\"/Products/ShowComments/\" +");
#nullable restore
#line 267 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                                         Write(Model.ProductID);

#line default
#line hidden
#nullable disable
                WriteLiteral(");\r\n\r\n        });\r\n\r\n        function pageComment(pageid) {\r\n            $(\"#listComment\").load(\"/Products/ShowComments/\" + ");
#nullable restore
#line 272 "D:\Abshar\HotelAbshar\EndPoint.Site\Views\Products\ShowProduct.cshtml"
                                                          Write(Model.ProductID);

#line default
#line hidden
#nullable disable
                WriteLiteral("+\"?pageId=\" + pageid);\r\n        }\r\n\r\n\r\n    </script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HotelAbshar.Domain.Entities.Product.Product> Html { get; private set; }
    }
}
#pragma warning restore 1591