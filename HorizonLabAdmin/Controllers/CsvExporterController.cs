using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace HorizonLabAdmin.Controllers
{
    public class CsvExporterController : Controller
    {
        private readonly ITransaction _transactionHelper;
        private readonly IUtility _utility;

        public CsvExporterController(IUtility utility, ITransaction transactionHelper)
        {
            _transactionHelper = transactionHelper;
            _utility = utility;
        }

        public IActionResult SemiPublic(string start = null, string end = null)
        {
            DateTime? rcv_date_start = null, rcv_date_end = null;
            int water_bacteria_class_id = 1, total_coliform_id = 9, ecoli_id = 2, prev_trans_id = 0;
            bool transaction_status = true;
            List<sp_getsemipublicreport> semipublic_transaction_list = new List<sp_getsemipublicreport>();
            ResultDataParameter result_data = new ResultDataParameter();
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            var stream = new MemoryStream();

            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                rcv_date_start = _utility.GetFirstDateInWeek(DateTime.Now);
                rcv_date_end = _utility.GetLastDateInWeek(DateTime.Now);
            }
            else if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                rcv_date_start = _utility.FormatStringToDateTime(start);
                rcv_date_end = _utility.FormatStringToDateTime(end);
            }

            if (rcv_date_start != null && rcv_date_end != null)
            {
                semipublic_transaction_list = _transactionHelper.GetSemiPublicTransactionsList(new test_transaction
                {
                    rcv_date_start = rcv_date_start,
                    rcv_date_end = rcv_date_end,
                    class_id = water_bacteria_class_id,
                    status = transaction_status
                });

                if (semipublic_transaction_list != null && semipublic_transaction_list.Count > 0)
                {
                    using (var sw = new StreamWriter(stream: stream, encoding: new UTF8Encoding(true)))
                    {
                        using (var cw = new CsvWriter(sw, cc))
                        {
                            //header
                            cw.WriteField("Customer ID");
                            cw.WriteField("Test Date");
                            cw.WriteField("Last Name");
                            cw.WriteField("Environment District");
                            cw.WriteField("DW Officer");
                            cw.WriteField("DW Phone");
                            cw.WriteField("Com Code");
                            cw.WriteField("Collection Date");
                            cw.WriteField("Location");
                            cw.WriteField("Sample Type");
                            cw.WriteField("Sample ID");
                            cw.WriteField("Collected By");
                            cw.WriteField("Loginnum");
                            cw.WriteField("Sample Number");
                            cw.WriteField("(MPN/100ml)");
                            cw.WriteField("EC (MPN/100ml)");

                            foreach (var item in semipublic_transaction_list)
                            {
                                if (prev_trans_id != item.trans_id)
                                {
                                    cw.NextRecord();
                                    cw.WriteField($"{item.customer_id}");
                                    cw.WriteField($"{item.test_date.Value.ToString("yyyy-MM-dd")}");
                                    cw.WriteField($"{item.last_name}");
                                    cw.WriteField($"{item.env_distr}");
                                    cw.WriteField($"{item.dw_officer}");
                                    cw.WriteField($"{item.dw_phone}");
                                    cw.WriteField($"{item.com_code}");
                                    cw.WriteField($"{item.collect_datetime.Value.ToString("yyyy-MM-dd")}");
                                    cw.WriteField($"{item.sample_legal_loc}");
                                    cw.WriteField($"{item.sample_type}");
                                    cw.WriteField($"{item.idnty_location}");
                                    cw.WriteField($"{item.submtd_by}");
                                    cw.WriteField("");
                                    cw.WriteField($"{item.trans_id}");

                                    result_data = ProcessResultsFromSemiPublic(semipublic_transaction_list, item.trans_id);
                                    cw.WriteField($"{result_data.coliform_result}");
                                    cw.WriteField($"{result_data.ecoli_result}");

                                    List<sp_getsemipublicreport> results = semipublic_transaction_list.Where(x => x.trans_id == item.trans_id).ToList();
                                    if(results!=null && results.Count > 0)
                                    {
                                        results.Where(x => x.param_id == total_coliform_id).FirstOrDefault().result = "**processed**";
                                        results.Where(x => x.param_id == ecoli_id).FirstOrDefault().result = "**processed**";
                                    }

                                    foreach (var others in semipublic_transaction_list.Where(x => x.trans_id == item.trans_id && x.result != "**processed**"))
                                    {
                                        cw.WriteField($"{others.param_name}:{others.result}");
                                    }
                                }
                                prev_trans_id = item.trans_id;
                            }
                        }

                    }
                }
            }
            string csvName = $"SemiPublicWeeklyReport-{DateTime.UtcNow.Ticks}.csv";
            return File(stream.ToArray(), "text/csv", csvName);
        }

        public IActionResult MonthlySubsidy(string start = null, string end = null)
        {
            List<sp_getmonthlysubsidyreport> monthly_subsidy_transaction_list = new List<sp_getmonthlysubsidyreport>();
            ResultDataParameter result_data = new ResultDataParameter();
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            var stream = new MemoryStream();
            DateTime? rcv_date_start = null, rcv_date_end = null;
            int water_bacteria_class_id = 1, prev_trans_id = 0;
            bool transaction_status = true;

            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                rcv_date_start = _utility.GetFirstDateOfMonth(DateTime.Now);
                rcv_date_end = _utility.GetLastDateOfMonth(DateTime.Now);
            }
            else if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                rcv_date_start = _utility.FormatStringToDateTime(start);
                rcv_date_end = _utility.FormatStringToDateTime(end);
            }

            if (rcv_date_start != null && rcv_date_end != null)
            {
                monthly_subsidy_transaction_list = _transactionHelper.GetMonthlySubsidyTransactionsList(new test_transaction
                {
                    rcv_date_start = rcv_date_start,
                    rcv_date_end = rcv_date_end,
                    class_id = water_bacteria_class_id,
                    status = transaction_status
                });

                if (monthly_subsidy_transaction_list != null && monthly_subsidy_transaction_list.Count > 0)
                {
                    using (var sw = new StreamWriter(stream: stream, encoding: new UTF8Encoding(true)))
                    {
                        using (var cw = new CsvWriter(sw, cc))
                        {
                            //header
                            cw.WriteField("Horizon ID");
                            cw.WriteField("Project");
                            cw.WriteField("First Name");
                            cw.WriteField("Last Name");
                            cw.WriteField("Mailing Address");
                            cw.WriteField("City");
                            cw.WriteField("Province");
                            cw.WriteField("Postal Code");
                            cw.WriteField("Daytime Phone");
                            cw.WriteField("Evening Phone");
                            cw.WriteField("Email");
                            cw.WriteField("Fax");
                            cw.WriteField("Sample ID(Client name)");
                            cw.WriteField("Sample ID(location)");
                            cw.WriteField("Sample Type");
                            cw.WriteField("Legal Location");
                            cw.WriteField("Town");
                            cw.WriteField("RM/LGD");
                            cw.WriteField("Sample Latitude");
                            cw.WriteField("Sample Longitude");
                            cw.WriteField("UTM X");
                            cw.WriteField("UTM Y");
                            cw.WriteField("UTM Zone");
                            cw.WriteField("Coliforms");
                            cw.WriteField("Ecoli");
                            cw.WriteField("Units");
                            cw.WriteField("Submitted Date");
                            cw.WriteField("Collect Date");
                            cw.WriteField("Collect Time");
                            cw.WriteField("Received Date");
                            cw.WriteField("Test Date");
                            cw.WriteField("Report Date");
                            cw.WriteField("Coupon ID");
                            cw.WriteField("Paid By Coupon");

                            foreach (var item in monthly_subsidy_transaction_list)
                            {
                                if (prev_trans_id != item.trans_id)
                                {
                                    cw.NextRecord();
                                    cw.WriteField($"{item.customer_id}");
                                    cw.WriteField($"{item.project}");                                    
                                    cw.WriteField($"{item.first_name}");
                                    cw.WriteField($"{item.last_name}");
                                    cw.WriteField($"{item.street}");
                                    cw.WriteField($"{item.city}");
                                    cw.WriteField($"{item.province}");
                                    cw.WriteField($"{item.postal_code}");
                                    cw.WriteField($"{item.phone}");
                                    cw.WriteField("");
                                    cw.WriteField($"{item.email}");
                                    cw.WriteField("");
                                    cw.WriteField("");
                                    cw.WriteField($"{item.idnty_location}");
                                    cw.WriteField($"{item.sample_type}");
                                    cw.WriteField($"{item.sample_legal_loc}");
                                    cw.WriteField($"{item.town}");
                                    cw.WriteField($"{item.municipality}");
                                    cw.WriteField($"{item.latitude}");
                                    cw.WriteField($"{item.longitude}");
                                    cw.WriteField($"{item.utm_x}");
                                    cw.WriteField($"{item.utm_y}");
                                    cw.WriteField($"{item.zone}");

                                    result_data = ProcessResultsFromMonthltSubsidy(monthly_subsidy_transaction_list, item.trans_id);

                                    cw.WriteField($"{result_data.coliform_result}");
                                    cw.WriteField($"{result_data.ecoli_result}");
                                    cw.WriteField($"{item.unit_of_measurement}");
                                    cw.WriteField($"{item.submtd_by}");
                                    
                                    result_data = ProcessDateMonthlySubsidy(item);

                                    cw.WriteField($"{result_data.collect_date}");
                                    cw.WriteField($"{result_data.collect_time}");                                                                      
                                    cw.WriteField($"{result_data.received_date}");                                                                      
                                    cw.WriteField($"{result_data.test_date}");                                                                      
                                    cw.WriteField($"{result_data.report_date}");                                                                      
                                    cw.WriteField($"{item.gen_coupon}");                                                                      
                                    cw.WriteField($"{item.assigned_coupon}");                                                                      

                                    //foreach (var others in monthly_subsidy_transaction_list.Where(x => x.trans_id == item.trans_id && x.result != "**processed**"))
                                    //{
                                    //    cw.WriteField($"{others.param_name}:{others.result}");
                                    //}
                                }
                                prev_trans_id = item.trans_id;
                            }
                        }
                    }
                }
            }

            string csvName = $"MonthlySubsidyReport-{DateTime.UtcNow.Ticks}.csv";
            return File(stream.ToArray(), "text/csv", csvName);
        }

        private ResultDataParameter ProcessResultsFromMonthltSubsidy(List<sp_getmonthlysubsidyreport> data, int transid)
        {
            ResultDataParameter report_data = new ResultDataParameter();
            sp_getmonthlysubsidyreport report_total_coliform = new sp_getmonthlysubsidyreport();
            sp_getmonthlysubsidyreport report_ecoli = new sp_getmonthlysubsidyreport();
            int total_coliform_id = 9, ecoli_id = 2;
            List<sp_getmonthlysubsidyreport> results = data.Where(x => x.trans_id == transid).ToList();
            report_data.coliform_result = "";
            report_data.ecoli_result = "";

            if (results != null && results.Count > 0)
            {

                report_total_coliform =  results.Where(x => x.param_id == total_coliform_id).FirstOrDefault();
                report_ecoli =  results.Where(x => x.param_id == total_coliform_id).FirstOrDefault();
                if (report_total_coliform != null && !string.IsNullOrEmpty(report_total_coliform.result))
                    report_data.coliform_result = results.Where(x => x.param_id == total_coliform_id).FirstOrDefault().result;

                if (report_ecoli != null && !string.IsNullOrEmpty(report_ecoli.result))
                    report_data.ecoli_result = results.Where(x => x.param_id == ecoli_id).FirstOrDefault().result;
            }

            return report_data;
        }

        private ResultDataParameter ProcessResultsFromSemiPublic(List<sp_getsemipublicreport> data, int transid)
        {
            ResultDataParameter report_data = new ResultDataParameter();
            int total_coliform_id = 9, ecoli_id = 2;
            List<sp_getsemipublicreport> results = data.Where(x => x.trans_id == transid).ToList();
            report_data.coliform_result = "";
            report_data.ecoli_result = "";

            if (results != null && results.Count > 0)
            {
                if (!string.IsNullOrEmpty(results.Where(x => x.param_id == total_coliform_id).FirstOrDefault().result))
                    report_data.coliform_result = results.Where(x => x.param_id == total_coliform_id).FirstOrDefault().result;

                if (!string.IsNullOrEmpty(results.Where(x => x.param_id == ecoli_id).FirstOrDefault().result))
                    report_data.ecoli_result = results.Where(x => x.param_id == ecoli_id).FirstOrDefault().result;
            }

            return report_data;
        }

        private ResultDataParameter ProcessDateMonthlySubsidy(sp_getmonthlysubsidyreport report)
        {
            ResultDataParameter report_data = new ResultDataParameter();
            report_data.collect_date = "";
            report_data.collect_time = "";
            report_data.received_date = "";
            report_data.test_date = "";
            report_data.report_date = "";

            if (report.collect_datetime != null)
            {
                report_data.collect_date = report.collect_datetime.Value.ToString("yyyy-MM-dd");
                report_data.collect_time = report.collect_datetime.Value.ToString("HH:mm");
            }

            if (report.rcv_date != null)
            {
                report_data.received_date = report.rcv_date.Value.ToString("yyyy-MM-dd");
            }

            if (report.test_date != null)
            {
                report_data.test_date = report.test_date.Value.ToString("yyyy-MM-dd");
            }

            if (report.work_date != null)
            {
                report_data.report_date = report.work_date.Value.ToString("yyyy-MM-dd");
            }

            return report_data;
        }
    }
}