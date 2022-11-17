using HorizonLabAdmin.Helpers.Containers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HController : Controller
    {
        public IActionResult GoToMainPage()
        {
            return RedirectToAction("Index", "Login");
        }

        public IActionResult GoToServiceHomePage()
        {
            return RedirectToAction("Index", "Services");
        }

        public IActionResult GoToSupplyMainPage(int sid = 0)
        {
            return RedirectToAction("MainPage", "Supply", new { sid = sid });
        }

        public IActionResult GoToWaterTestCertificatePage(string filter="all")
        {
            return RedirectToActionPermanent("WaterTestCertificatePage", "Certificate", new { filter=filter});
        }

        public IActionResult GoToRequestMainPage()
        {
            return RedirectToActionPermanent("TestRequestPage", "Order");
        }

        public IActionResult GoToEditWaterChemPage(int transactionid)
        {
            return RedirectToAction("EditWaterChemPage", "WaterChemicalTransaction", new { transId = transactionid });
        }

        public IActionResult GoToEditWaterBacteriaPage(int transactionid, int refresh = 0)
        {
            return RedirectToAction("EditTestTransactionPage", "WaterBacteriaTransaction", new { transId = transactionid , refresh = refresh});
        }

        public IActionResult GoToEditTestTransactionFrame(int transactionid)
        {
            return RedirectToAction("EditTestTransactionFrame", "WaterBacteriaTransaction", new { transId = transactionid });
        }

        public IActionResult GoToCustomerListPage(string input_status = "true")
        {
            return RedirectToAction("ViewCustomerListPage", "Customer", new { status = input_status });
        }

        public IActionResult GoToRequestRecordsPage(OrderListPageParameter param)
        {
            if (param == null) return RedirectToAction("OrderListPage", "Order");
            return RedirectToAction("OrderListPage", "Order", new { req_id = param.request_id, trans_id = param.transaction_id, pkgid = param.package_id });
        }

        public IActionResult GoToRequestPage()
        {
            return RedirectToAction("OrderPage", "Order");
        }

        public IActionResult GoToUserAccountMainPage()
        {
            return RedirectToAction("Index", "UserAccount");
        }

        public IActionResult GoToActiveUserAccountPage()
        {
            return RedirectToAction("ActiveAccountsPage", "UserAccount");
        }

        public IActionResult GoToWaterBacteriaTransactionViewPage()
        {
            return RedirectToAction("ViewTransactions", "WaterBacteriaTransaction");
        }

        public IActionResult GoToBulkUploadWaterBacteriaPage(bool result = true)
        {
            return RedirectToAction("BulkUploadWaterBacteria", "WaterBacteriaTransaction", new { result = result });
        }

        public IActionResult GoToEditCustomerPage(int customerid)
        {
            return RedirectToAction("EditCustomerForm", "Customer", new { cid = customerid });
        }

        public IActionResult GoToNewCustomerPage()
        {
            return RedirectToAction("NewCustomerForm", "Customer");
        }

        public IActionResult GoToNewWaterSampleForm(int requestid = 0, int requestitemid = 0, string frame = "yes")
        {
            return RedirectToAction("NewWaterSampleForm", "WaterBacteriaTransaction", new {oid = requestid , oiid = requestitemid , frame = frame});
        }

        public IActionResult GoToWaterBacteriaProjectFormsPage(ProjectRequestPageObject param)
        {
            return RedirectToAction("WaterBacteriaProjectsForms", "Certificate", new{view_data = param});
        }

        public IActionResult GoToProjectRequestPage(ProjectRequestPageObject param)
        {
            return RedirectToAction("RecordPage", "ProjectRequests", new {
                row_count = param.row_count,
                payment_id = param.selected_payment_type_id,
                test_pkg_id = param.selected_testpkg_id,
                project_id = param.selected_project_id,
                date = param.RequestDate,
                filter = param.SelectedFilter
            });
        }

        public IActionResult GoToF054PForm(F054P_paramter param)
        {
            return RedirectToAction("F054P", "Forms", new
            {
                pid = param.pid,
                fid = param.fid,
                transid = param.transid,
                rush = param.rush,
                condition = param.condition
            });
        }

        public IActionResult GoToF125PForm(F054P_paramter param)
        {
            return RedirectToAction("F125P", "Forms", new
            {
                pid = param.pid,
                fid = param.fid,
                transid = param.transid,
                rush = param.rush,
                condition = param.condition
            });
        }
    }
}
