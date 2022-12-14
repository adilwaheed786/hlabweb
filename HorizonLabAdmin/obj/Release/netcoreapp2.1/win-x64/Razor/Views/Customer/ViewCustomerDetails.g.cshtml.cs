#pragma checksum "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "df801700cce6cbccf4ad7b2b4459f5ae9a1af6f6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_ViewCustomerDetails), @"mvc.1.0.view", @"/Views/Customer/ViewCustomerDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Customer/ViewCustomerDetails.cshtml", typeof(AspNetCore.Views_Customer_ViewCustomerDetails))]
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
#line 1 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\_ViewImports.cshtml"
using HorizonLabAdmin;

#line default
#line hidden
#line 2 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\_ViewImports.cshtml"
using HorizonLabAdmin.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df801700cce6cbccf4ad7b2b4459f5ae9a1af6f6", @"/Views/Customer/ViewCustomerDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19004a3953a47e7099f28de08d084a43d97f8c9b", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_ViewCustomerDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HorizonLabLibrary.Entities.horizonlabcustomerview>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/customer_events.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
  
    ViewData["Title"] = "Customer: " + Model.first_name + " " + Model.last_name;
    Layout = "~/Views/Shared/_HlabWebAdminLayout.cshtml";

#line default
#line hidden
            BeginContext(206, 47, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ba9c3d2fb55a4a819544802a87ac850a", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(253, 63, true);
            WriteLiteral("\r\n<p style=\"font-size:0.85rem;color:#4A6ABF;font-weight:bold;\">");
            EndContext();
            BeginContext(317, 15, false);
#line 7 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                                        Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(332, 388, true);
            WriteLiteral(@"</p>
<div class=""row"" style=""font-size:1.3rem;"">
    <div class=""col-lg-1""></div>
    <div class=""col-lg-10"">
        <div class=""panel panel-success"">
            <div class=""panel-heading"">Customer Information</div>
            <div class=""panel-body"">
                <div class=""row"">
                    <div class=""col-sm-4"">
                        <p><b>Customer ID:</b> ");
            EndContext();
            BeginContext(721, 48, false);
#line 16 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                          Write(String.Format("{0:00000000}", Model.customer_id));

#line default
#line hidden
            EndContext();
            BeginContext(769, 56, true);
            WriteLiteral("</p>\r\n                        <p><b>Customer Name: </b> ");
            EndContext();
            BeginContext(826, 16, false);
#line 17 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                             Write(Model.first_name);

#line default
#line hidden
            EndContext();
            BeginContext(842, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(844, 15, false);
#line 17 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                                               Write(Model.last_name);

#line default
#line hidden
            EndContext();
            BeginContext(859, 55, true);
            WriteLiteral("</p>\r\n                        <p><b>Company Name: </b> ");
            EndContext();
            BeginContext(915, 18, false);
#line 18 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                            Write(Model.company_name);

#line default
#line hidden
            EndContext();
            BeginContext(933, 58, true);
            WriteLiteral("</p>\r\n                        <p><b>Producer Number: </b> ");
            EndContext();
            BeginContext(992, 21, false);
#line 19 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                               Write(Model.producer_number);

#line default
#line hidden
            EndContext();
            BeginContext(1013, 189, true);
            WriteLiteral("</p>\r\n                    </div>\r\n                    <div class=\"col-sm-4\">\r\n                        <p>\r\n                            <b>Address</b>\r\n                            <br />\r\n\r\n");
            EndContext();
#line 26 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                               string postal_code = "";

#line default
#line hidden
            BeginContext(1260, 28, true);
            WriteLiteral("                            ");
            EndContext();
#line 27 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                             if (!string.IsNullOrEmpty(Model.postal_code))
                            {
                                postal_code = Model.postal_code.ToUpper();

                                if(postal_code.Length > 3)
                                {
                                    char mid = postal_code[3];
                                    if(mid!=' ')
                                    {
                                        postal_code = postal_code.Insert(3, " ");
                                    }
                                }
                            }

#line default
#line hidden
            BeginContext(1881, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(1912, 12, false);
#line 41 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                       Write(Model.street);

#line default
#line hidden
            EndContext();
            BeginContext(1924, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(1926, 10, false);
#line 41 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                     Write(Model.city);

#line default
#line hidden
            EndContext();
            BeginContext(1936, 37, true);
            WriteLiteral(" <br />\r\n                            ");
            EndContext();
            BeginContext(1974, 14, false);
#line 42 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                       Write(Model.province);

#line default
#line hidden
            EndContext();
            BeginContext(1988, 3, true);
            WriteLiteral(" , ");
            EndContext();
            BeginContext(1992, 17, false);
#line 42 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                         Write(Model.postal_code);

#line default
#line hidden
            EndContext();
            BeginContext(2009, 142, true);
            WriteLiteral("\r\n                        </p>\r\n                        <p><b>Email Address: </b></p>\r\n                        <ul style=\"list-style:none;\">\r\n");
            EndContext();
#line 46 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                             foreach (var email in ViewBag.emaillist)
                            {

#line default
#line hidden
            BeginContext(2253, 38, true);
            WriteLiteral("                                <li>\r\n");
            EndContext();
#line 49 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                       
                                        if (email.is_primary)
                                        {

#line default
#line hidden
            BeginContext(2438, 93, true);
            WriteLiteral("                                            <span style=\"color:green;\">Primary&nbsp;</span>\r\n");
            EndContext();
#line 53 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                        }
                                     

#line default
#line hidden
            BeginContext(2614, 36, true);
            WriteLiteral("                                    ");
            EndContext();
            BeginContext(2651, 11, false);
#line 55 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                               Write(email.email);

#line default
#line hidden
            EndContext();
            BeginContext(2662, 41, true);
            WriteLiteral("\r\n                                </li>\r\n");
            EndContext();
#line 57 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                            }

#line default
#line hidden
            BeginContext(2734, 217, true);
            WriteLiteral("                        </ul>\r\n                    </div>\r\n                    <div class=\"col-sm-4\">\r\n                        <p><b>Contact Number(s): </b></p>\r\n                        <ul style=\"list-style:none;\">\r\n");
            EndContext();
#line 63 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                             foreach (var phone in ViewBag.phonelist)
                            {

#line default
#line hidden
            BeginContext(3053, 36, true);
            WriteLiteral("                                <li>");
            EndContext();
            BeginContext(3090, 11, false);
#line 65 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                               Write(phone.phone);

#line default
#line hidden
            EndContext();
            BeginContext(3101, 7, true);
            WriteLiteral("</li>\r\n");
            EndContext();
#line 66 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                            }

#line default
#line hidden
            BeginContext(3139, 71, true);
            WriteLiteral("                        </ul>\r\n                        <p><b>Fax: </b> ");
            EndContext();
            BeginContext(3211, 9, false);
#line 68 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                   Write(Model.fax);

#line default
#line hidden
            EndContext();
            BeginContext(3220, 57, true);
            WriteLiteral("</p>\r\n                        <p><b>Date Registered: </b>");
            EndContext();
            BeginContext(3278, 45, false);
#line 69 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                              Write(Model.date_registered?.ToString("dd/MM/yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(3323, 221, true);
            WriteLiteral("</p>\r\n                    </div>\r\n                </div>\r\n                <hr />\r\n\r\n                <div class=\"row\">\r\n                    <div class=\"col-sm-4\">\r\n                        <p><b>Approved for Financing: </b>");
            EndContext();
            BeginContext(3545, 26, false);
#line 76 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                                     Write(Model.is_approve_financing);

#line default
#line hidden
            EndContext();
            BeginContext(3571, 48, true);
            WriteLiteral("</p>\r\n                        <p><b>Public: </b>");
            EndContext();
            BeginContext(3620, 15, false);
#line 77 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                     Write(Model.is_public);

#line default
#line hidden
            EndContext();
            BeginContext(3635, 53, true);
            WriteLiteral("</p>\r\n                        <p><b>Semi Public: </b>");
            EndContext();
            BeginContext(3689, 20, false);
#line 78 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                          Write(Model.is_semi_public);

#line default
#line hidden
            EndContext();
            BeginContext(3709, 48, true);
            WriteLiteral("</p>\r\n                        <p><b>Coupon: </b>");
            EndContext();
            BeginContext(3758, 12, false);
#line 79 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                     Write(Model.coupon);

#line default
#line hidden
            EndContext();
            BeginContext(3770, 60, true);
            WriteLiteral("</p>\r\n                        <p><b>Coupon Date Issued: </b>");
            EndContext();
            BeginContext(3831, 24, false);
#line 80 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                                 Write(Model.coupon_issued_date);

#line default
#line hidden
            EndContext();
            BeginContext(3855, 122, true);
            WriteLiteral("</p>\r\n                    </div>\r\n                    <div class=\"col-sm-4\">\r\n                        <p><b>Location:</b> ");
            EndContext();
            BeginContext(3978, 48, false);
#line 83 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                       Write(String.Format("{0:00000000}", Model.customer_id));

#line default
#line hidden
            EndContext();
            BeginContext(4026, 63, true);
            WriteLiteral("</p>\r\n                        <p><b>Environment District: </b> ");
            EndContext();
            BeginContext(4090, 15, false);
#line 84 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                                    Write(Model.env_distr);

#line default
#line hidden
            EndContext();
            BeginContext(4105, 53, true);
            WriteLiteral("</p>\r\n                        <p><b>DW Officer: </b> ");
            EndContext();
            BeginContext(4159, 16, false);
#line 85 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                          Write(Model.dw_officer);

#line default
#line hidden
            EndContext();
            BeginContext(4175, 51, true);
            WriteLiteral("</p>\r\n                        <p><b>DW Phone: </b> ");
            EndContext();
            BeginContext(4227, 14, false);
#line 86 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                        Write(Model.dw_phone);

#line default
#line hidden
            EndContext();
            BeginContext(4241, 51, true);
            WriteLiteral("</p>\r\n                        <p><b>Com Code: </b> ");
            EndContext();
            BeginContext(4293, 14, false);
#line 87 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                        Write(Model.com_code);

#line default
#line hidden
            EndContext();
            BeginContext(4307, 318, true);
            WriteLiteral(@"</p>
                    </div>
                </div>
            </div>
        </div>
        <button type=""button"" class=""btn btn-success"" onclick=""window.location='/Customer/ViewCustomerListPage'"">CUSTOMER LIST PAGE</button>
        <!--<button type=""button"" class=""btn btn-success goto_placeorderform"" id=""");
            EndContext();
            BeginContext(4626, 17, false);
#line 93 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
                                                                             Write(Model.customer_id);

#line default
#line hidden
            EndContext();
            BeginContext(4643, 94, true);
            WriteLiteral("\">ADD NEW WATER TEST SAMPLE</button>-->\r\n        <button type=\"button\" class=\"btn btn-success\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 4737, "\"", 4814, 3);
            WriteAttributeValue("", 4747, "window.location=\'/Customer/EditCustomerForm?cid=", 4747, 48, true);
#line 94 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\Customer\ViewCustomerDetails.cshtml"
WriteAttributeValue("", 4795, Model.customer_id, 4795, 18, false);

#line default
#line hidden
            WriteAttributeValue("", 4813, "\'", 4813, 1, true);
            EndWriteAttribute();
            BeginContext(4815, 79, true);
            WriteLiteral(">EDIT CUSTOMER</button>\r\n    </div>\r\n    <div class=\"col-lg-1\"></div>\r\n</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HorizonLabLibrary.Entities.horizonlabcustomerview> Html { get; private set; }
    }
}
#pragma warning restore 1591
