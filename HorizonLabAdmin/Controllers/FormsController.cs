using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    public class FormsController : HController
    {
        private readonly IRequest _requestHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly ITransaction _transactionHelper;
        private readonly ITestProject _projectRequestHelper;
        private readonly ILogger<FormsController> _logger;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly IConfiguration _appConfig;
        private readonly ITestProjectForm _testProjForm;

        public FormsController(
            IConfiguration appConfig,
            IRequest requestHelper,
            IRequestItem requestItemHelper,
            ITransaction transactionHelper,
            IHorizonLabSession sessionHelper,
            ILogger<FormsController> logger,
            ITestProject projectRequestHelper,
            ITestProjectForm testProjForm
            )
        {
            _appConfig = appConfig;
            _logger = logger;
            _sessionHelper = sessionHelper;
            _requestHelper = requestHelper;
            _transactionHelper = transactionHelper;
            _requestItemHelper = requestItemHelper;
            _projectRequestHelper = projectRequestHelper;
            _testProjForm = testProjForm;
        }

        public IActionResult F054(int rid = 0, int fid = 0, int transid = 0, int rush = 0, int condition = 0)
        {
            F054Data viewdata = new F054Data();
            viewdata.is_condition_met = false;
            viewdata.is_rush = false;
            if (rush != 0) viewdata.is_rush = true;
            if (condition != 0) viewdata.is_condition_met = true;

            List<testrequestsupplyview> AllRequestSupplyList = _transactionHelper.GetTestRequestSupplies(rid, fid);
            viewdata.RequestDetailList = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = rid }).Where(x => x.form_id == fid).ToList();
            viewdata.TestTransaction = _transactionHelper.GetTransactionInformationFromSp(transid);
            viewdata.RequestInformation = _requestHelper.GetRequestInformationDetails(rid);
            viewdata.RequestSupplyList = AllRequestSupplyList.GroupBy(c => c.supply_id, (key, c) => c.FirstOrDefault()).ToList();
            viewdata.ColilertSupply = viewdata.RequestSupplyList.Where(x => x.name.ToLower().Contains("colilert")).FirstOrDefault();
            return View(viewdata);
        }

        public IActionResult F125(int rid = 0, int fid = 0, int transid = 0, int rush = 0, int condition = 0)
        {
            F054Data viewdata = new F054Data();
            viewdata.is_condition_met = false;
            viewdata.is_rush = false;
            if (rush != 0) viewdata.is_rush = true;
            if (condition != 0) viewdata.is_condition_met = true;

            List<testrequestsupplyview> AllRequestSupplyList = _transactionHelper.GetTestRequestSupplies(rid, fid);
            viewdata.RequestDetailList = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = rid }).Where(x => x.form_id == fid).ToList();
            viewdata.TestTransaction = _transactionHelper.GetTransactionInformationFromSp(transid);
            viewdata.RequestInformation = _requestHelper.GetRequestInformationDetails(rid);
            viewdata.RequestSupplyList = AllRequestSupplyList.GroupBy(c => c.supply_id, (key, c) => c.FirstOrDefault()).ToList();
            viewdata.ColilertSupply = viewdata.RequestSupplyList.Where(x => x.name.ToLower().Contains("colilert")).FirstOrDefault();
            return View(viewdata);
        }

        public IActionResult F125P(int pid = 0, int fid = 0, int transid = 0, int rush = 0, int condition = 0)
        {
            F054Data viewdata = new F054Data();
            viewdata.is_condition_met = false;
            viewdata.is_rush = false;
            int F125_form_id = 3;
            if (rush != 0) viewdata.is_rush = true;
            if (condition != 0) viewdata.is_condition_met = true;
            DateTime? receive_date_time = null;
            DateTime? collect_date_time = null;

            List<projectrequestsupplyview> AllRequestSupplyList = _projectRequestHelper.GetProjectRequestSuppliesFromDb(pid);
            projectrequestsformview form_details = new projectrequestsformview();
            form_details = _testProjForm.GetProjectRequestFormInfo(pid);
            List<temporaryprojectrequestsview> temp_request_list = _projectRequestHelper.ListProjectRequestRecords(new ProjectRequestPageObject
            {
                proj_form_id = pid,
                selected_project_id = 0,
                selected_payment_type_id = 0,
                selected_testpkg_id = 0,
                RequestDate = null
            });

            viewdata.RequestDetailList = new List<orderdetailsview>();
            foreach (var req in temp_request_list)
            {
                viewdata.RequestDetailList.Add(new orderdetailsview
                {
                    first_name = $"{form_details.project}",
                    last_name = "",
                    idnty_location = $"{req.first_name}{req.last_name}-{req.sample_desc}",
                    hl_code = req.hl_code,
                    order_item_id = req.id
                });
                receive_date_time = req.date_insert;
                collect_date_time = req.collect_datetime;
            }
            if (viewdata.RequestDetailList != null && viewdata.RequestDetailList.Count > 0) viewdata.RequestDetailList = viewdata.RequestDetailList.OrderBy(x => x.hl_code).ToList();

            viewdata.TestTransaction = new sp_gethorizonlabtransactiondetails { rcv_date = receive_date_time, collect_datetime = collect_date_time };

            viewdata.RequestInformation = new ordersummaryview
            {
                incubation_date_time_in = form_details.incubation_date_time_in,
                incubation_date_time_out = form_details.incubation_date_time_out,
                incubation_temp = form_details.incubation_temp,
                is_condition_met = form_details.is_condition_met,
                is_rush = form_details.is_rush
            };

            AllRequestSupplyList = AllRequestSupplyList.GroupBy(c => c.supply_id, (key, c) => c.FirstOrDefault()).ToList();

            viewdata.RequestSupplyList = new List<testrequestsupplyview>();
            foreach (var supply in AllRequestSupplyList)
            {
                viewdata.RequestSupplyList.Add(new testrequestsupplyview
                {
                    transaction_id = 0,
                    supply_id = supply.supply_id,
                    name = supply.name,
                    lot = supply.lot,
                    expiry_date = supply.expiry_date,
                    form_id = F125_form_id,
                    order_id = 0,
                    hours_tolerance_start = supply.hours_tolerance_start,
                    hours_tolerance_end = supply.hours_tolerance_end
                });
            }

            viewdata.ColilertSupply = viewdata.RequestSupplyList.Where(x => x.name.ToLower().Contains("colilert")).FirstOrDefault();
            return View(viewdata);
        }

        public IActionResult F054P(int pid = 0, int fid = 0, int transid = 0, int rush = 0, int condition = 0)
        {
            F054Data viewdata = new F054Data();
            int F054_form_id = 1;
            viewdata.is_condition_met = false;
            viewdata.is_rush = false;
            if (rush != 0) viewdata.is_rush = true;
            if (condition != 0) viewdata.is_condition_met = true;
            DateTime? receive_date_time = null;
            DateTime? collect_date_time = null;

            List<projectrequestsupplyview> AllRequestSupplyList = _projectRequestHelper.GetProjectRequestSuppliesFromDb(pid);
            projectrequestsformview form_details = new projectrequestsformview();

            form_details = _testProjForm.GetProjectRequestFormInfo(pid);
            List<temporaryprojectrequestsview> temp_request_list = _projectRequestHelper.ListProjectRequestRecords(new ProjectRequestPageObject {
                proj_form_id = pid,
                selected_project_id = 0,
                selected_payment_type_id = 0,
                selected_testpkg_id = 0,
                RequestDate = null
            });
            viewdata.RequestDetailList = new List<orderdetailsview>();
            foreach (var req in temp_request_list)
            {
                viewdata.RequestDetailList.Add(new orderdetailsview
                {
                    first_name = $"{form_details.project}",
                    last_name = "",
                    idnty_location = $"{req.first_name}{req.last_name}-{req.sample_desc}",
                    hl_code = req.hl_code,
                    order_item_id = req.id
                });
                receive_date_time = req.rcv_date;
                collect_date_time = req.collect_datetime;
            }
            if (viewdata.RequestDetailList != null && viewdata.RequestDetailList.Count > 0) viewdata.RequestDetailList = viewdata.RequestDetailList.OrderBy(x => x.order_item_id).ToList();


            viewdata.TestTransaction = new sp_gethorizonlabtransactiondetails { rcv_date = receive_date_time, collect_datetime = collect_date_time };

            viewdata.RequestInformation = new ordersummaryview {
                incubation_date_time_in = form_details.incubation_date_time_in,
                incubation_date_time_out = form_details.incubation_date_time_out,
                incubation_temp = form_details.incubation_temp,
                is_condition_met = form_details.is_condition_met,
                is_rush = form_details.is_rush
            };

            AllRequestSupplyList = AllRequestSupplyList.GroupBy(c => c.supply_id, (key, c) => c.FirstOrDefault()).ToList();

            viewdata.RequestSupplyList = new List<testrequestsupplyview>();
            foreach (var supply in AllRequestSupplyList)
            {
                viewdata.RequestSupplyList.Add(new testrequestsupplyview
                {
                    transaction_id = 0,
                    supply_id = supply.supply_id,
                    name = supply.name,
                    lot = supply.lot,
                    expiry_date = supply.expiry_date,
                    form_id = F054_form_id,
                    order_id = 0,
                    hours_tolerance_start = supply.hours_tolerance_start,
                    hours_tolerance_end = supply.hours_tolerance_end
                });
            }

            viewdata.ColilertSupply = viewdata.RequestSupplyList.Where(x => x.name.ToLower().Contains("colilert")).FirstOrDefault();
            return View(viewdata);
        }

        public IActionResult F110(int rid = 0, int fid = 0, int transid = 0, int rush = 0, int condition = 0)
        {
            F110Data viewdata = new F110Data();
            viewdata.is_condition_met = false;
            viewdata.is_rush = false;

            if (rush != 0) viewdata.is_rush = true;
            if (condition != 0) viewdata.is_condition_met = true;

            List<testrequestsupplyview> AllRequestSupplyList = _transactionHelper.GetTestRequestSupplies(rid, fid);
            viewdata.RequestDetailList = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = rid })
                .Where(x => x.form_id == fid)
                .Where(x => x.trans_id == transid)
                .ToList();
            viewdata.hl_code = "";

            if ((viewdata.RequestDetailList != null || viewdata.RequestDetailList.Count > 0) && transid!=0) {
                viewdata.hl_code = viewdata.RequestDetailList.Where(x => x.trans_id == transid).FirstOrDefault().hl_code;
            }

            viewdata.TestTransaction = _transactionHelper.GetTransactionInformationFromSp(transid);
            viewdata.RequestInformation = _requestHelper.GetRequestInformationDetails(rid);
            viewdata.RequestSupplyList = AllRequestSupplyList.GroupBy(c => c.supply_id, (key, c) => c.FirstOrDefault()).ToList();
            viewdata.ColilertSupply = viewdata.RequestSupplyList.Where(x => x.name.ToLower().Contains("colilert")).FirstOrDefault();
            viewdata.ComparatorSupply = viewdata.RequestSupplyList.Where(x => x.name.ToLower().Contains("comparator")).FirstOrDefault();
            viewdata.Simplate = viewdata.RequestSupplyList.Where(x => x.name.ToLower().Contains("simplate")).FirstOrDefault();
            return View(viewdata);
        }
    }
}