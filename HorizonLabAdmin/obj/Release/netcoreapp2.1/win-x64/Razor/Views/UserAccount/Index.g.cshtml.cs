#pragma checksum "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "86266bbf18088e18e114ca89cc436ecc14798f0f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserAccount_Index), @"mvc.1.0.view", @"/Views/UserAccount/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/UserAccount/Index.cshtml", typeof(AspNetCore.Views_UserAccount_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"86266bbf18088e18e114ca89cc436ecc14798f0f", @"/Views/UserAccount/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19004a3953a47e7099f28de08d084a43d97f8c9b", @"/Views/_ViewImports.cshtml")]
    public class Views_UserAccount_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "searchBy", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SearchUserAccounts", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "UserAccount", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/user_account_events.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
  
    Layout = "~/Views/Shared/_HlabWebAdminLayout.cshtml";
    ViewData["PageTitle"] = "User Accounts Panel";

#line default
#line hidden
#line 5 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
  string msg_color = "green", page_message = "";

#line default
#line hidden
#line 6 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
 if (!string.IsNullOrEmpty(ViewBag.PageMessage))
{
    var message = ViewBag.PageMessage.Split(":");
    if (message[0] == "Error")
    {
        msg_color = "red";
    }
    page_message = message[1];
}

#line default
#line hidden
            BeginContext(382, 248, true);
            WriteLiteral("<div class=\"row\">\r\n    <div class=\"col-lg-4\">\r\n            <button type=\"button\" class=\"btn btn-success goto_newuseraccountform\"><span class=\"glyphicon glyphicon-plus\"></span>&nbsp;Add New Account</button>\r\n    </div>\r\n    <div class=\"col-lg-4\"><b>");
            EndContext();
            BeginContext(631, 12, false);
#line 19 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                        Write(ViewBag.Page);

#line default
#line hidden
            EndContext();
            BeginContext(643, 48, true);
            WriteLiteral("</b></div>\r\n    <div class=\"col-lg-4\">\r\n        ");
            EndContext();
            BeginContext(691, 533, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bed59962b1114044b66ca8133fa66aa3", async() => {
                BeginContext(784, 61, true);
                WriteLiteral("\r\n            <div class=\"form-group mb-2\">\r\n                ");
                EndContext();
                BeginContext(845, 88, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80329676ba3b40a0a16b1969a18604b0", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#line 23 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.searchOption;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(933, 95, true);
                WriteLiteral("\r\n                <input type=\"text\" class=\"form-control\" name=\"searchString\" id=\"searchString\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1028, "\"", 1054, 1);
#line 24 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
WriteAttributeValue(" ", 1036, ViewBag.searched, 1037, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1055, 162, true);
                WriteLiteral(" />\r\n                <button type=\"submit\" class=\"btn btn-outline-success\"><span class=\"glyphicon glyphicon-search\"></span></button>\r\n            </div>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1224, 45, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n<p align=\"left\"><span");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 1269, "\"", 1309, 3);
            WriteAttributeValue("", 1277, "font-size:15px;color:", 1277, 21, true);
#line 31 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
WriteAttributeValue("", 1298, msg_color, 1298, 10, false);

#line default
#line hidden
            WriteAttributeValue("", 1308, ";", 1308, 1, true);
            EndWriteAttribute();
            BeginContext(1310, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1312, 12, false);
#line 31 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                                                          Write(page_message);

#line default
#line hidden
            EndContext();
            BeginContext(1324, 457, true);
            WriteLiteral(@"</span></p>
<div style=""background-color:white;"">    
    <table class=""table table-striped"" style=""font-size:1.3rem;"">
        <thead class=""data_table_header"">
            <tr>
                <th scope=""col"">ID</th>
                <th scope=""col"">Name</th>
                <th scope=""col"">Email</th>
                <th scope=""col"">UserName</th>
                <th scope=""col"">&nbsp</th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 44 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
             foreach (var user in ViewBag.userList)
            {
                var name = user.fname + " " + user.lname;

#line default
#line hidden
            BeginContext(1908, 58, true);
            WriteLiteral("                <tr>\r\n                    <th scope=\"row\">");
            EndContext();
            BeginContext(1967, 12, false);
#line 48 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                               Write(user.user_id);

#line default
#line hidden
            EndContext();
            BeginContext(1979, 31, true);
            WriteLiteral("</th>\r\n                    <td>");
            EndContext();
            BeginContext(2011, 4, false);
#line 49 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                   Write(name);

#line default
#line hidden
            EndContext();
            BeginContext(2015, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(2047, 10, false);
#line 50 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                   Write(user.email);

#line default
#line hidden
            EndContext();
            BeginContext(2057, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(2089, 13, false);
#line 51 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                   Write(user.username);

#line default
#line hidden
            EndContext();
            BeginContext(2102, 107, true);
            WriteLiteral("</td>\r\n                    <td>\r\n                        <button type=\"button\" class=\"btn btn-outline-info\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2209, "\"", 2291, 3);
            WriteAttributeValue("", 2219, "window.location.href=\'/UserAccount/ViewUserAccount?userId=", 2219, 58, true);
#line 53 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
WriteAttributeValue("", 2277, user.user_id, 2277, 13, false);

#line default
#line hidden
            WriteAttributeValue("", 2290, "\'", 2290, 1, true);
            EndWriteAttribute();
            BeginContext(2292, 140, true);
            WriteLiteral("><span class=\"glyphicon glyphicon-info-sign\"></span></button>\r\n                        <button type=\"button\" class=\"btn btn-outline-success\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2432, "\"", 2514, 3);
            WriteAttributeValue("", 2442, "window.location.href=\'/UserAccount/UserAccountForm?userId=", 2442, 58, true);
#line 54 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
WriteAttributeValue("", 2500, user.user_id, 2500, 13, false);

#line default
#line hidden
            WriteAttributeValue("", 2513, "\'", 2513, 1, true);
            EndWriteAttribute();
            BeginContext(2515, 114, true);
            WriteLiteral("><span class=\"glyphicon glyphicon-pencil\"></span></button>\r\n                        <!--For active users only-->\r\n");
            EndContext();
#line 56 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                         if (ViewBag.status)
                        {

#line default
#line hidden
            BeginContext(2702, 92, true);
            WriteLiteral("                            <button type=\"button\" class=\"btn btn-outline-danger delete_user\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 2794, "\"", 2815, 1);
#line 58 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
WriteAttributeValue("", 2802, user.user_id, 2802, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2816, 60, true);
            WriteLiteral("><span class=\"glyphicon glyphicon-remove\"></span></button>\r\n");
            EndContext();
#line 59 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(2960, 95, true);
            WriteLiteral("                            <button type=\"button\" class=\"btn btn-outline-success activate_user\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 3055, "\"", 3076, 1);
#line 62 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
WriteAttributeValue("", 3063, user.user_id, 3063, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3077, 56, true);
            WriteLiteral("><span class=\"glyphicon glyphicon-ok\"></span></button>\r\n");
            EndContext();
#line 63 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
                        }

#line default
#line hidden
            BeginContext(3160, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 67 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\UserAccount\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(3227, 42, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n");
            EndContext();
            BeginContext(3269, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ddfde8b82c47468fb5e5ee4c40fe5bf1", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
