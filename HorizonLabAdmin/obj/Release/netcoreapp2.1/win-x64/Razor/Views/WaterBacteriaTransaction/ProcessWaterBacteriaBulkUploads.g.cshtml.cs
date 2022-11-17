#pragma checksum "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "342c6fb752c5f2fd4b0532b14fd4fd71e06a4fa2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_WaterBacteriaTransaction_ProcessWaterBacteriaBulkUploads), @"mvc.1.0.view", @"/Views/WaterBacteriaTransaction/ProcessWaterBacteriaBulkUploads.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/WaterBacteriaTransaction/ProcessWaterBacteriaBulkUploads.cshtml", typeof(AspNetCore.Views_WaterBacteriaTransaction_ProcessWaterBacteriaBulkUploads))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"342c6fb752c5f2fd4b0532b14fd4fd71e06a4fa2", @"/Views/WaterBacteriaTransaction/ProcessWaterBacteriaBulkUploads.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19004a3953a47e7099f28de08d084a43d97f8c9b", @"/Views/_ViewImports.cshtml")]
    public class Views_WaterBacteriaTransaction_ProcessWaterBacteriaBulkUploads : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HorizonLabAdmin.Models.Forms.WaterBacteriaBulkUploadObject>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
  
    ViewData["Title"] = "ProcessWaterBacteriaBulkUploads";
    Layout = "~/Views/Shared/_HlabWebAdminLayout.cshtml";

#line default
#line hidden
            BeginContext(193, 3495, true);
            WriteLiteral(@"
<h2>Processed Water Bacteria Bulk Upload</h2>

<div class=""row"">
    <div class=""col-sm-12"">
        <div class=""panel panel-success"">
            <div class=""panel-heading"">&nbsp;</div>
            <div class=""panel-body"" style=""overflow:auto;height:650px;"">
                <div class=""row"">
                        <table class=""table table-striped"" style=""font-size:1.15rem;"">
                            <thead>
                                <tr>
                                    <th><div style=""width:200px;"">Insert Result</div></th>
                                    <th><div style=""width:100px;"">LabPackage</div></th>
                                    <th><div style=""width:70px;"">CustomerID</div></th>
                                    <th><div style=""width:150px;"">FirstName</div></th>
                                    <th><div style=""width:150px;"">LastName</div></th>
                                    <th><div style=""width:220px;"">PrimaryEmail</div></th>
                      ");
            WriteLiteral(@"              <th><div style=""width:120px;"">PrimaryPhone</div></th>
                                    <th><div style=""width:100px;"">TotalRequestAmount</div></th>
                                    <th><div style=""width:90px;"">Payment1</div></th>
                                    <th><div style=""width:70px;"">Amount1</div></th>
                                    <th><div style=""width:90px;"">Payment2</div></th>
                                    <th><div style=""width:70px;"">Amount2</div></th>
                                    <th><div style=""width:90px;"">Payment3</div></th>
                                    <th><div style=""width:70px;"">Amount3</div></th>
                                    <th><div style=""width:100px;"">Coupon</div></th>
                                    <th><div style=""width:175px;"">Project</div></th>
                                    <th><div style=""width:120px;"">Postal</div></th>
                                    <th><div style=""width:190px;"">SampleID</div></th>
  ");
            WriteLiteral(@"                                  <th><div style=""width:100px;"">SampleType</div></th>
                                    <th><div style=""width:190px;"">LegalLocation</div></th>
                                    <th><div style=""width:150px;"">Town</div></th>
                                    <th><div style=""width:170px;"">Municipality</div></th>
                                    <th><div style=""width:120px;"">CollectDate</div></th>
                                    <th><div style=""width:80px;"">CollectTime</div></th>
                                    <th><div style=""width:150px;"">SubmittedBy</div></th>
                                    <th><div style=""width:120px;"">SubmittedDate</div></th>
                                    <th><div style=""width:80px;"">SubmittedTime</div></th>
                                    <th><div style=""width:80px;"">ReceivedBy</div></th>
                                    <th><div style=""width:120px;"">ReceivedDate</div></th>
                                    <th>");
            WriteLiteral(@"<div style=""width:80px;"">ReceivedTime</div></th>
                                    <th><div style=""width:120px;"">TestDate</div></th>
                                    <th><div style=""width:100px;"">Temperature</div></th>
                                    <th><div style=""width:60px;"">Tax%</div></th>
                                </tr>
                            </thead>
                            <tbody>
");
            EndContext();
#line 54 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                 for (int i = 0; i < Model.wbf_list.Count; i++)
                                {

#line default
#line hidden
            BeginContext(3804, 78, true);
            WriteLiteral("                                <tr>\r\n                                    <td>");
            EndContext();
            BeginContext(3883, 30, false);
#line 57 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].InsertResult);

#line default
#line hidden
            EndContext();
            BeginContext(3913, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(3961, 28, false);
#line 58 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].LabPackage);

#line default
#line hidden
            EndContext();
            BeginContext(3989, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4037, 28, false);
#line 59 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].CustomerID);

#line default
#line hidden
            EndContext();
            BeginContext(4065, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4113, 27, false);
#line 60 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(4140, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4188, 26, false);
#line 61 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].LastName);

#line default
#line hidden
            EndContext();
            BeginContext(4214, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4262, 30, false);
#line 62 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].PrimaryEmail);

#line default
#line hidden
            EndContext();
            BeginContext(4292, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4340, 30, false);
#line 63 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].PrimaryPhone);

#line default
#line hidden
            EndContext();
            BeginContext(4370, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4418, 36, false);
#line 64 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].TotalRequestAmount);

#line default
#line hidden
            EndContext();
            BeginContext(4454, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4502, 26, false);
#line 65 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Payment1);

#line default
#line hidden
            EndContext();
            BeginContext(4528, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4576, 25, false);
#line 66 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Amount1);

#line default
#line hidden
            EndContext();
            BeginContext(4601, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4649, 26, false);
#line 67 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Payment2);

#line default
#line hidden
            EndContext();
            BeginContext(4675, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4723, 25, false);
#line 68 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Amount2);

#line default
#line hidden
            EndContext();
            BeginContext(4748, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4796, 26, false);
#line 69 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Payment3);

#line default
#line hidden
            EndContext();
            BeginContext(4822, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4870, 25, false);
#line 70 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Amount3);

#line default
#line hidden
            EndContext();
            BeginContext(4895, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(4943, 24, false);
#line 71 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Coupon);

#line default
#line hidden
            EndContext();
            BeginContext(4967, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5015, 25, false);
#line 72 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Project);

#line default
#line hidden
            EndContext();
            BeginContext(5040, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5088, 24, false);
#line 73 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Postal);

#line default
#line hidden
            EndContext();
            BeginContext(5112, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5160, 26, false);
#line 74 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].SampleID);

#line default
#line hidden
            EndContext();
            BeginContext(5186, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5234, 28, false);
#line 75 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].SampleType);

#line default
#line hidden
            EndContext();
            BeginContext(5262, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5310, 31, false);
#line 76 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].LegalLocation);

#line default
#line hidden
            EndContext();
            BeginContext(5341, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5389, 22, false);
#line 77 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Town);

#line default
#line hidden
            EndContext();
            BeginContext(5411, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5459, 30, false);
#line 78 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Municipality);

#line default
#line hidden
            EndContext();
            BeginContext(5489, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5537, 29, false);
#line 79 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].CollectDate);

#line default
#line hidden
            EndContext();
            BeginContext(5566, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5614, 29, false);
#line 80 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].CollectTime);

#line default
#line hidden
            EndContext();
            BeginContext(5643, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5691, 29, false);
#line 81 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].SubmittedBy);

#line default
#line hidden
            EndContext();
            BeginContext(5720, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5768, 31, false);
#line 82 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].SubmittedDate);

#line default
#line hidden
            EndContext();
            BeginContext(5799, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5847, 31, false);
#line 83 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].SubmittedTime);

#line default
#line hidden
            EndContext();
            BeginContext(5878, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(5926, 28, false);
#line 84 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].ReceivedBy);

#line default
#line hidden
            EndContext();
            BeginContext(5954, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(6002, 30, false);
#line 85 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].ReceivedDate);

#line default
#line hidden
            EndContext();
            BeginContext(6032, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(6080, 30, false);
#line 86 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].ReceivedTime);

#line default
#line hidden
            EndContext();
            BeginContext(6110, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(6158, 26, false);
#line 87 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].TestDate);

#line default
#line hidden
            EndContext();
            BeginContext(6184, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(6232, 29, false);
#line 88 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Temperature);

#line default
#line hidden
            EndContext();
            BeginContext(6261, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(6309, 21, false);
#line 89 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                   Write(Model.wbf_list[i].Tax);

#line default
#line hidden
            EndContext();
            BeginContext(6330, 46, true);
            WriteLiteral("</td>\r\n                                </tr>\r\n");
            EndContext();
#line 91 "C:\CODES\HorizonLab\12102021\HorizonLabAdmin\Views\WaterBacteriaTransaction\ProcessWaterBacteriaBulkUploads.cshtml"
                                }

#line default
#line hidden
            BeginContext(6411, 148, true);
            WriteLiteral("                            </tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HorizonLabAdmin.Models.Forms.WaterBacteriaBulkUploadObject> Html { get; private set; }
    }
}
#pragma warning restore 1591
