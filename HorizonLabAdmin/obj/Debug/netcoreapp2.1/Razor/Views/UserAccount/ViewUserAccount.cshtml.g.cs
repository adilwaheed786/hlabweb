#pragma checksum "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e1a9bce4e7323c822109cdc8fbe179ba51065ba0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserAccount_ViewUserAccount), @"mvc.1.0.view", @"/Views/UserAccount/ViewUserAccount.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/UserAccount/ViewUserAccount.cshtml", typeof(AspNetCore.Views_UserAccount_ViewUserAccount))]
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
#line 1 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\_ViewImports.cshtml"
using HorizonLabAdmin;

#line default
#line hidden
#line 2 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\_ViewImports.cshtml"
using HorizonLabAdmin.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e1a9bce4e7323c822109cdc8fbe179ba51065ba0", @"/Views/UserAccount/ViewUserAccount.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19004a3953a47e7099f28de08d084a43d97f8c9b", @"/Views/_ViewImports.cshtml")]
    public class Views_UserAccount_ViewUserAccount : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml"
  
    ViewData["PageTitle"] = "View User Account";
    Layout = "~/Views/Shared/_HlabWebAdminLayout.cshtml";

#line default
#line hidden
            BeginContext(118, 172, true);
            WriteLiteral("\r\n<div class=\"\">\r\n    <div class=\"col-lg-8 main_content\">\r\n        <div style=\"padding:40px 30px 40px 30px;\">\r\n            <div class=\"\">\r\n                <h3>Name: </h3>\r\n");
            EndContext();
#line 12 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml"
                  var name = ViewBag.user.fname.ToUpper() + " " + @ViewBag.user.lname.ToUpper();

#line default
#line hidden
            BeginContext(389, 45, true);
            WriteLiteral("                <span style=\"color:#6C6E70;\">");
            EndContext();
            BeginContext(435, 4, false);
#line 13 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml"
                                        Write(name);

#line default
#line hidden
            EndContext();
            BeginContext(439, 138, true);
            WriteLiteral("</span>\r\n            </div>\r\n\r\n            <div class=\"\">\r\n                <h3>Email: </h3>\r\n                <span style=\"color:#6C6E70;\">");
            EndContext();
            BeginContext(578, 18, false);
#line 18 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml"
                                        Write(ViewBag.user.email);

#line default
#line hidden
            EndContext();
            BeginContext(596, 166, true);
            WriteLiteral("</span><br />\r\n            </div>            \r\n\r\n            <div class=\"\">\r\n                <h3>Date Registered: </h3>\r\n                <span style=\"color:#6C6E70;\">");
            EndContext();
            BeginContext(763, 21, false);
#line 23 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml"
                                        Write(ViewBag.user.date_reg);

#line default
#line hidden
            EndContext();
            BeginContext(784, 147, true);
            WriteLiteral("</span><br />\r\n            </div>\r\n\r\n            <div class=\"\">\r\n                <h3>username: </h3>\r\n                <span style=\"color:#6C6E70;\">");
            EndContext();
            BeginContext(932, 21, false);
#line 28 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml"
                                        Write(ViewBag.user.username);

#line default
#line hidden
            EndContext();
            BeginContext(953, 145, true);
            WriteLiteral("</span><br />\r\n            </div>\r\n\r\n            <div class=\"\">\r\n                <h3>Access: </h3>\r\n                <span style=\"color:#6C6E70;\">");
            EndContext();
            BeginContext(1099, 14, false);
#line 33 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml"
                                        Write(ViewBag.access);

#line default
#line hidden
            EndContext();
            BeginContext(1113, 388, true);
            WriteLiteral(@"</span><br />
            </div>

            <div class="""" style=""display: block; text-align: right;"">
                <div class=""btn-group"" role=""group"" aria-label="""">
                    <button type=""button"" class=""btn btn-primary"" onclick =""window.location.href='/UserAccount/UserAccountForm'"">Add New</button>
                    <button type=""button"" class=""btn btn-primary""");
            EndContext();
            BeginWriteAttribute("onclick", " onclick =\"", 1501, "\"", 1592, 3);
            WriteAttributeValue("", 1512, "window.location.href=\'/UserAccount/UserAccountForm?userId=", 1512, 58, true);
#line 39 "C:\Users\Mutahir\Desktop\Fiverr\HorizonLab-Software\HorizonLabFiles - Water Program\HorizonLabAdmin\Views\UserAccount\ViewUserAccount.cshtml"
WriteAttributeValue("", 1570, ViewBag.user.user_id, 1570, 21, false);

#line default
#line hidden
            WriteAttributeValue("", 1591, "\'", 1591, 1, true);
            EndWriteAttribute();
            BeginContext(1593, 454, true);
            WriteLiteral(@">Edit</button>
                    <button type=""button"" class=""btn btn-primary"" onclick =""window.location.href='/UserAccount/ActiveAccountsPage'"">Active Accounts</button>
                    <button type=""button"" class=""btn btn-primary"" onclick =""window.location.href='/UserAccount/InactiveAccountsPage'"">InActive Accounts</button>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-lg-4""></div>

</div>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
