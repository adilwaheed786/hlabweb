using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ICertificate
    {
        WaterCertificateListWithCustomerId GetCustomerCertificateWithId(int id);
        List<watercertificatesummaryview> BatchCertificateRecordList(List<watercertificatesummaryview> request_records, int rec_start = 0, int rec_end = 0);
        List<watercertificatesummaryview> SortCertificatetRecordList(List<watercertificatesummaryview> requestlist);
        TestResultPageViewModel GenerateB1NSCertificate(TestResultPageViewModel page_model, List<int> request_ids);
        TestTransactionSearchParameters PrepareWaterTestCertificatePageData(string filter="all");
        string GenerateB1NSCertificateRequestURL(string request_scheme, string request_host, int requestid, int test_pkg_id);
        string GenerateCouponCertificateRequestURL(string request_scheme, string request_host, int coiponid);
        string GenerateSubsidyImageRequestURL(string request_scheme, string request_host, string filename);
    }
}
