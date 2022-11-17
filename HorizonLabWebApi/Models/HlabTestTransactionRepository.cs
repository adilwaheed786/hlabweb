using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Models
{
    public class HlabTestTransactionRepository : Interface_test_transactions
    {

        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestTransactionRepository> _logger;

        public HlabTestTransactionRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabTestTransactionRepository> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public int AddTransactionDetails(hlab_test_transactions htt)
        {
            int ReportPrivateId = 5;
            int B2TestPackagId = 2;
            try
            {
                if (htt.test_pkg_id == B2TestPackagId) htt.report_type_id = ReportPrivateId;

                htt.status = true;
                _hlab_Db_Context.hlab_test_transactions.Add(htt);
                _hlab_Db_Context.SaveChanges();
                return htt.trans_id;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return 0;
            }                
        }

        public IEnumerable<hlab_test_transaction_types> GetTestTransactionTypes()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_transaction_types.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool DeleteTransactionDetails(int transaction_id)
        {
            try
            {
                hlab_test_transactions htt = _hlab_Db_Context.hlab_test_transactions.ToList().Where(x => x.trans_id == transaction_id).FirstOrDefault();
                if (htt != null)
                {
                    _hlab_Db_Context.hlab_test_transactions.Remove(htt);
                    _hlab_Db_Context.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError("MODEL DeleteTransactionDetails: Unable to delete transaction id - " + transaction_id + ", it doesn't exists on horizon lab database.");
                    return false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        public IEnumerable<testtransactionsview> GetAllTransactions(test_transaction htt)
        {
            List<testtransactionsview> transaction_lists = new List<testtransactionsview>();
            try
            {                
                //transaction id
                if (htt.trans_id != 0) transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => x.trans_id == htt.trans_id
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                //customer id
                else if (htt.customer_id != 0) transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => x.customer_id == htt.customer_id
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                //date entered 
                else if (htt.date_entered_start != null && htt.date_entered_end != null) transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => x.date_entered >= htt.date_entered_start
                    && x.date_entered < AddDayToNullableDateTime(htt.date_entered_end, 1)
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();

                //submitted date 
                else if (htt.submtd_datetime_end != DateTime.MinValue && htt.submtd_datetime_start != DateTime.MinValue && htt.submtd_datetime_end != null && htt.submtd_datetime_start != null)
                    transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => x.submtd_datetime >= htt.submtd_datetime_start
                    && x.submtd_datetime < AddDayToNullableDateTime(htt.submtd_datetime_end, 1)
                    && x.status == htt.status
                    && x.class_id == htt.class_id).ToList();
                //received date 
                else if (htt.rcv_date_start != DateTime.MinValue && htt.rcv_date_end != DateTime.MinValue && htt.rcv_date_start != null && htt.rcv_date_end != null)
                    transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => x.rcv_date >= htt.rcv_date_start
                    && x.rcv_date < AddDayToNullableDateTime(htt.rcv_date_end, 1)
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                //test date 
                else if (htt.test_date_start != DateTime.MinValue && htt.test_date_end != DateTime.MinValue && htt.test_date_start != null && htt.test_date_end != null)
                    transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => x.test_date >= htt.test_date_start
                    && x.rcv_date < AddDayToNullableDateTime(htt.test_date_end, 1)
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                //identification name
                else if (!string.IsNullOrEmpty(htt.idnty_name))
                    transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => x.idnty_name.ToLower().StartsWith(htt.idnty_name.ToLower())
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                //test package
                else if (htt.test_pkg_id != 0)
                    transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => x.test_pkg_id == htt.test_pkg_id
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                //customer first name != null/empty; last name = null/empty
                else if (!string.IsNullOrEmpty(htt.first_name) && string.IsNullOrEmpty(htt.last_name)) transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => !string.IsNullOrEmpty(x.first_name)
                    && x.first_name.ToLower().Contains(htt.first_name.ToLower())
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                //customer fisrt name = null/empty; last name != null/empty
                else if (string.IsNullOrEmpty(htt.first_name) && !string.IsNullOrEmpty(htt.last_name)) transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => !string.IsNullOrEmpty(x.last_name)
                    && x.last_name.ToLower().Contains(htt.last_name.ToLower())
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                //customer first and last name
                else if (!string.IsNullOrEmpty(htt.first_name) && !string.IsNullOrEmpty(htt.last_name))
                    transaction_lists = _hlab_Db_Context.testtransactionsview.Where(
                    x => !string.IsNullOrEmpty(x.last_name)
                    && x.last_name.ToLower().Contains(htt.last_name.ToLower())
                    && x.first_name.ToLower().Contains(htt.first_name.ToLower())
                    && x.status == htt.status
                    && x.class_id == htt.class_id
                    ).ToList();
                else
                    transaction_lists = _hlab_Db_Context.testtransactionsview.ToList();

                //if coupon
                if (htt.assigned_coupon != 0 && transaction_lists.Count > 0) transaction_lists = transaction_lists.Where(x => x.assigned_coupon == htt.assigned_coupon).ToList();

                if(!string.IsNullOrEmpty(htt.filter) && htt.filter != "all" && transaction_lists!=null && transaction_lists.Count > 0)
                {
                    if (htt.filter == "unpr") transaction_lists = transaction_lists.Where(x => x.work_date == null).ToList();
                    if (htt.filter == "proc") transaction_lists = transaction_lists.Where(x => x.work_date != null).ToList();
                }
            }
            catch(Exception exc){
                _logger.LogError($"HlabTestTransactionRepository > GetAllTransactions(): {exc.ToString()}");
                return null;
            }
            return transaction_lists;
        }

        public IEnumerable<sp_getdefaultpackageparameters> GetDefaultParameters(int package_id)
        {
            try
            {
                return _hlab_Db_Context.default_package_parameters.FromSql("sp_GetDefaultPackageParameters " + package_id).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public sp_gethorizonlabtransactiondetails GetTransactionDetails(int trans_id)
        {
            return _hlab_Db_Context.test_transaction_details.FromSql("sp_GetHorizonLabTransactionDetails " + Convert.ToInt32(trans_id)).FirstOrDefault();
        }

        public bool UpdateTransactionDetails(hlab_test_transactions htt)
        {
            try
            {
                if (htt != null)
                {
                    int? request_id = 0;
                    _hlab_Db_Context.hlab_test_transactions.Update(htt);
                    if (htt.test_pkg_id != 0)
                    {
                        hlab_order_items item = _hlab_Db_Context.hlab_order_items.Where(x => x.trans_id == htt.trans_id).FirstOrDefault();
                        item.test_pkg_id = htt.test_pkg_id;
                        request_id = item.order_id;
                        _hlab_Db_Context.hlab_order_items.Update(item);                        
                    }

                    if (request_id != 0 && request_id!=null)
                    {
                        hlab_order_logs request = new hlab_order_logs { order_id = (int)request_id, customer_id = htt.customer_id };
                        _hlab_Db_Context.Attach(request);
                        _hlab_Db_Context.Entry(request).Property(r => r.customer_id).IsModified = true;
                    }

                    _hlab_Db_Context.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError("MODEL UpdateTransactionDetails : Unable to update transaction records, hlab_test_transactions object is null");
                    return false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        private static DateTime AddDayToNullableDateTime(DateTime? NullableDateTime, int AdditionalDays = 0) {
            DateTime temp_date = DateTime.Now;
            if (NullableDateTime != null && NullableDateTime != DateTime.MinValue)
            {
                temp_date = NullableDateTime ?? DateTime.Now;
                return temp_date.AddDays(1);
            }
            return DateTime.MinValue;
        }

        public bool AddSubsidyFormImage(hlab_water_subsidy_form_images subsidyimage)
        {
            try
            {
                if (subsidyimage != null)
                {
                    _hlab_Db_Context.hlab_water_subsidy_form_images.Add(subsidyimage);
                    _hlab_Db_Context.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError("MODEL AddSubsidyFormImage : Unable to add subsidy image, object is null");
                    return false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        public bool UpdateSubsidyFormImage(hlab_water_subsidy_form_images subsidyimage)
        {
            try
            {
                if (subsidyimage != null)
                {
                    _hlab_Db_Context.hlab_water_subsidy_form_images.Update(subsidyimage);
                    _hlab_Db_Context.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError("MODEL UpdateSubsidyFormImage : Unable to add subsidy image, object is null");
                    return false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        public bool IfWaterSampleExists(hlab_test_transactions htt)
        {
            try
            {
                var record = _hlab_Db_Context.hlab_test_transactions.Where(
                    x => x.project_id == htt.project_id
                    && x.idnty_location == htt.idnty_location
                    && x.customer_id == htt.customer_id
                    && x.temp == htt.temp
                    && x.sample_legal_loc == htt.sample_legal_loc
                    && x.town == htt.town
                    && x.collect_datetime == htt.collect_datetime
                    && x.submtd_datetime == htt.submtd_datetime
                    && x.rcv_date == htt.rcv_date
                    && x.sample_type_id == htt.sample_type_id
                    && x.rm_id == htt.rm_id
                    ).ToList();
                if (record.Count > 0) return true;
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        public IEnumerable<hlab_transaction_supplies> GetTransactionSuppliesIds(hlab_transaction_supplies parameter)
        {
            try
            {
                List<hlab_transaction_supplies> list_hlab_transaction_supplies = new List<hlab_transaction_supplies>();
                if(parameter.supply_id!=0)
                {
                    list_hlab_transaction_supplies = _hlab_Db_Context.hlab_transaction_supplies.Where(x => x.supply_id == parameter.supply_id).ToList();
                    return list_hlab_transaction_supplies;
                }

                if (parameter.transaction_id != 0)
                {
                    list_hlab_transaction_supplies = _hlab_Db_Context.hlab_transaction_supplies.Where(x => x.transaction_id == parameter.transaction_id).ToList();
                    return list_hlab_transaction_supplies;
                }
                return list_hlab_transaction_supplies;
            }
            catch (Exception exc)
            {
                _logger.LogError($"ERROR: HlabTestTransactionRepository > GetTransactionSuppliesIds() : {exc.Message}" );
                return null;
            }
        }

        public bool AddTransactionSupplies(transaction_supply_param param)
        {
            try
            {
                foreach (var id in param.supply_ids)
                {
                    _hlab_Db_Context.hlab_transaction_supplies.Add(new hlab_transaction_supplies {
                        supply_id = id,
                        transaction_id = param.transactionid
                    });
                    _hlab_Db_Context.SaveChanges();
                }
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"AddTransactionSupplies(): {exc.Message}");
                return false;
            }
        }

        public bool DeleteTransactionSupplies(int transactionid)
        {
            try
            {
                _hlab_Db_Context.RemoveRange(
                    _hlab_Db_Context.hlab_transaction_supplies.Where(
                        x => x.transaction_id == transactionid));
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"DeleteTransactionSupplies(): {exc.Message}");
                return false;
            }
        }

        public List<testrequestsupplyview> GetTestRequestSupplyList(int requestid, int formid)
        {
            try
            {
                List<testrequestsupplyview> request_supply_list = new List<testrequestsupplyview>();
                request_supply_list = _hlab_Db_Context.testrequestsupplyview.Where(x => x.order_id == requestid && x.form_id == formid).ToList();
                return request_supply_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"ERROR: HlabTestTransactionRepository > GetTransactionSuppliesIds() : {exc.Message}");
                return null;
            }
        }

        public bool PublishTestTransaction(int transaction_id)
        {
            try
            {
                var transaction = new hlab_test_transactions { trans_id = transaction_id, publish = true };
                _hlab_Db_Context.Attach(transaction);
                _hlab_Db_Context.Entry(transaction).Property(r => r.publish).IsModified = true;
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"Error HlabTestTransactionRepository > PublishTestTransaction : {exc.Message}");
                return false;
            }
        }

        public IEnumerable<sp_getsemipublicreport> GetSemiPublicTransactions(test_transaction htt)
        {
            List<sp_getsemipublicreport> transaction_lists = new List<sp_getsemipublicreport>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@datestart",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = htt.rcv_date_start
                        },
                        new SqlParameter() {
                            ParameterName = "@dateend",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = htt.rcv_date_end
                        }};
                transaction_lists = _hlab_Db_Context.sp_getsemipublicreport.FromSql("sp_GetSemiPublicReport @datestart, @dateend", param).ToList();
                return transaction_lists;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestTransactionRepository > GetSemiPublicTransactions(): {exc.ToString()}");
                return null;
            }            
        }

        public IEnumerable<sp_getmonthlysubsidyreport> GetMonthlySubsidyReport(test_transaction htt)
        {
            List<sp_getmonthlysubsidyreport> transaction_lists = new List<sp_getmonthlysubsidyreport>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@datestart",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = htt.rcv_date_start
                        },
                        new SqlParameter() {
                            ParameterName = "@dateend",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = htt.rcv_date_end
                        }};
                transaction_lists = _hlab_Db_Context.sp_getmonthlysubsidyreport.FromSql("sp_GetMonthlySubsidyReport @datestart, @dateend", param).ToList();
                return transaction_lists;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestTransactionRepository > GetMonthlySubsidyReport(): {exc.ToString()}");
                return null;
            }
        }

        public bool DeleteSubsidyFormImage(int id = 0)
        {
            try
            {
                hlab_water_subsidy_form_images subsidyimage = new hlab_water_subsidy_form_images();
                subsidyimage = _hlab_Db_Context.hlab_water_subsidy_form_images.Where(x => x.id == id).FirstOrDefault();
                _hlab_Db_Context.Remove(subsidyimage);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }
    }
}
