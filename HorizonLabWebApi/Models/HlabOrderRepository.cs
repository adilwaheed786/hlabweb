using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabOrderRepository : Interface_hlab_orders
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabOrderRepository> _logger;

        public HlabOrderRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabOrderRepository> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public int? AddNewOrder(hlab_order_logs log)
        {
            try
            {
                log.record_status = true;
                log.is_condition_met = true;
                _hlab_Db_Context.hlab_order_logs.Add(log);
                _hlab_Db_Context.SaveChanges();
                return log.order_id;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return 0;
            }
        }

        public bool AddNewOrderItem(hlab_order_items item)
        {
            try
            {
                _hlab_Db_Context.hlab_order_items.Add(item);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteOrder(int orderid)
        {
            try
            {
                var order = new hlab_order_logs { order_id = orderid, record_status = false };
                var request_items = _hlab_Db_Context.hlab_order_items.Where(x => x.order_id == orderid).ToList();
                bool request_result = false;
                bool request_item_result = false;
                bool transaction_result = false;

                request_result = DeactivateRequest(orderid);
                if (request_result) request_item_result = LoopDeactivateRequestItems(request_items);
                if (request_item_result) transaction_result = LoopDeactivateTransactions(request_items);
                if (request_result) _hlab_Db_Context.SaveChanges();

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteOrderItems(int order_id)
        {
            try
            {
                var request_items = _hlab_Db_Context.hlab_order_items.Where(x => x.order_id == order_id).ToList();
                bool request_item_result = false;
                bool transaction_result = false;
                request_item_result = LoopDeactivateRequestItems(request_items);
                if (request_item_result) transaction_result = LoopDeactivateTransactions(request_items);
                if(request_item_result)_hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }
        
        private bool DeactivateRequest(int requestid)
        {
            try
            {
                _hlab_Db_Context.hlab_order_logs.Where(x => x.order_id == requestid).FirstOrDefault().record_status = false;
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        private bool LoopDeactivateRequestItems(List<hlab_order_items> request_items)
        {
            try
            {
                if (request_items.Count > 0)
                {
                    request_items.ForEach(a => a.status = false);
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        private bool LoopDeactivateTransactions(List<hlab_order_items> request_items)
        {
            try
            {
                if (request_items.Count > 0)
                {
                    foreach (var item in request_items)
                    {
                        if (item.trans_id == null || item.trans_id == 0) break;
                        var transaction = _hlab_Db_Context.hlab_test_transactions.Where(x => x.trans_id == item.trans_id).FirstOrDefault();
                        if (transaction == null) break;
                        transaction.status = false;
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        //returns active orders only
        public IEnumerable<ordersummaryview> GetAllOrders(ordersearch log)
        {
            try
            {
                List<ordersummaryview> orderlist = new List<ordersummaryview>();
                if (log.customer_id != 0) orderlist = _hlab_Db_Context.ordersummaryview.Where(x => x.customer_id == log.customer_id && x.record_status == true).ToList();
                if (log.order_id != 0) orderlist = _hlab_Db_Context.ordersummaryview.Where(x => x.order_id == log.order_id && x.record_status == true).ToList();
                //if (!string.IsNullOrEmpty(log.received_by)) orderlist = _hlab_Db_Context.ordersummaryview.Where(x => x.received_by.ToLower() == log.received_by.ToLower()).ToList();
                if (!string.IsNullOrEmpty(log.first_name)) orderlist = _hlab_Db_Context.ordersummaryview.Where(x => x.first_name.ToLower().Contains(log.first_name.ToLower()) && x.record_status == true).ToList();
                if (!string.IsNullOrEmpty(log.last_name)) orderlist = _hlab_Db_Context.ordersummaryview.Where(x => x.last_name.ToLower().Contains(log.last_name.ToLower()) && x.record_status == true).ToList();
                if (log.start_order_date != null 
                    && log.end_order_date != null 
                    && log.start_order_date != DateTime.MinValue 
                    && log.end_order_date != DateTime.MinValue)
                        orderlist = _hlab_Db_Context.ordersummaryview.Where(
                        x => x.order_date >= log.start_order_date
                        //&& x.order_date <= AddDayToNullableDateTime(log.end_order_date,1) 
                        && x.order_date <= log.end_order_date
                        && x.record_status == true).ToList();
                return orderlist;
            }
            catch (Exception exc)
            {
                _logger.LogError($"GetAllOrders() : {exc.Message} | {log.customer_id} | {log.order_id} | ");
                return null;
            }
        }

        public ordersummaryview GetOrderInfo(int order_id)
        {
            try
            {
                return _hlab_Db_Context.ordersummaryview.Where(x => x.order_id == order_id).FirstOrDefault();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public IEnumerable<orderdetailsview> GetOrderItems(orderdetailsview log)
        {
            try
            {
                List<orderdetailsview> orderitemview = new List<orderdetailsview>();

                if (log.order_id != 0)
                {
                    orderitemview = _hlab_Db_Context.orderdetailsview.Where(x => x.order_id == log.order_id).ToList();
                    return orderitemview;
                }

                else if (log.trans_id != 0 && log.trans_id != null)
                {
                    orderitemview = _hlab_Db_Context.orderdetailsview.Where(x => x.trans_id == log.trans_id).ToList();
                    return orderitemview;
                }

                else if (log.customer_id != 0 && !string.IsNullOrEmpty(log.hl_code_prefix) && log.order_date != null)
                {
                    orderitemview = _hlab_Db_Context.orderdetailsview.Where(
                        x => x.customer_id == log.customer_id
                        && x.hl_code_prefix == log.hl_code_prefix
                        && x.order_date >= log.order_date
                        && x.order_date <= log.order_date
                    ).ToList();
                    return orderitemview;
                }

                else if (log.order_date != null && !string.IsNullOrEmpty(log.hl_code_prefix))
                {
                    orderitemview = _hlab_Db_Context.orderdetailsview.Where(
                        x => x.hl_code_prefix == log.hl_code_prefix
                        && x.order_date >= log.order_date
                        && x.order_date <= log.order_date
                    ).ToList();
                    return orderitemview;
                }

                else if (log.customer_id != 0)
                {
                    orderitemview = _hlab_Db_Context.orderdetailsview.Where(x => x.customer_id == log.customer_id).ToList();
                    return orderitemview;
                }

                else if (log.order_date != null)
                {
                    orderitemview = _hlab_Db_Context.orderdetailsview.Where(
                        x => x.order_date.Value >= log.order_date
                        && x.order_date <= log.order_date
                    ).ToList();
                    return orderitemview;
                }

                return orderitemview;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool UpdateOrder(hlab_order_logs log)
        {
            try
            {
                log.record_status = true;
                _hlab_Db_Context.hlab_order_logs.Update(log);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UpdateOrder(): {exc.Message}");
                return false;
            }
        }

        private static DateTime AddDayToNullableDateTime(DateTime? NullableDateTime, int AdditionalDays = 0)
        {
            DateTime temp_date = DateTime.Now;
            if (NullableDateTime != null && NullableDateTime != DateTime.MinValue)
            {
                temp_date = NullableDateTime ?? DateTime.Now;
                return temp_date.AddDays(1);
            }
            return DateTime.MinValue;
        }

        public bool UpdateOrderItem(hlab_order_items item)
        {
            try
            {
                _hlab_Db_Context.hlab_order_items.Update(item);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public IEnumerable<watercertificatesummaryview> GetAllWaterCertificates(ordersearch log)
        {
            try
            {
                List<watercertificatesummaryview> certlist = new List<watercertificatesummaryview>();
                if (log.customer_id != 0) certlist = _hlab_Db_Context.watercertificatesummaryview.Where(x => x.customer_id == log.customer_id && x.record_status == true).ToList();
                if (log.order_id != 0) certlist = _hlab_Db_Context.watercertificatesummaryview.Where(x => x.order_id == log.order_id && x.record_status == true).ToList();
                
                if (log.Project_id != 0) certlist = _hlab_Db_Context.watercertificatesummaryview.Where(x => x.project_id == log.Project_id && x.record_status == true).ToList();

                //if (!string.IsNullOrEmpty(log.received_by)) orderlist = _hlab_Db_Context.ordersummaryview.Where(x => x.received_by.ToLower() == log.received_by.ToLower()).ToList();
                if (!string.IsNullOrEmpty(log.first_name)) certlist = _hlab_Db_Context.watercertificatesummaryview.Where(x => x.first_name.ToLower().Contains(log.first_name.ToLower()) && x.record_status == true).ToList();
                if (!string.IsNullOrEmpty(log.last_name)) certlist = _hlab_Db_Context.watercertificatesummaryview.Where(x => x.last_name.ToLower().Contains(log.last_name.ToLower()) && x.record_status == true).ToList();

                //order date
                if (log.start_order_date != null
                    && log.end_order_date != null
                    && log.start_order_date != DateTime.MinValue
                    && log.end_order_date != DateTime.MinValue)
                    certlist = _hlab_Db_Context.watercertificatesummaryview.Where(
                    x => x.order_date.Date >= log.start_order_date.Value.Date
                    && x.order_date.Date < AddDayToNullableDateTime(log.end_order_date.Value.Date, 1)
                    && x.record_status == true).ToList();
                //submitted
                if (log.submtd_datetime_end != DateTime.MinValue && log.submtd_datetime_start != DateTime.MinValue && log.submtd_datetime_end != null && log.submtd_datetime_start != null)
                    certlist = _hlab_Db_Context.watercertificatesummaryview.Where(
                    x => x.submtd_datetime >= log.submtd_datetime_start
                    && x.submtd_datetime < AddDayToNullableDateTime(log.submtd_datetime_end, 1)
                    && x.record_status == true).ToList();
                //received date 
                if (log.rcv_date_start != DateTime.MinValue && log.rcv_date_end != DateTime.MinValue && log.rcv_date_start != null && log.rcv_date_end != null)
                    certlist = _hlab_Db_Context.watercertificatesummaryview.Where(
                    x => x.rcv_date >= log.rcv_date_start
                    && x.rcv_date < AddDayToNullableDateTime(log.rcv_date_end, 1)
                    && x.record_status == true).ToList();
                //test date 
                if (log.test_date_start != DateTime.MinValue && log.test_date_end != DateTime.MinValue && log.test_date_start != null && log.test_date_end != null)
                    certlist = _hlab_Db_Context.watercertificatesummaryview.Where(
                    x => x.test_date >= log.test_date_start
                    && x.test_date < AddDayToNullableDateTime(log.test_date_end, 1)
                    && x.record_status).ToList();
                //Project date
                if (log.proj_date_start != DateTime.MinValue && log.proj_date_end != DateTime.MinValue && log.proj_date_start != null && log.proj_date_end != null)
                    certlist = _hlab_Db_Context.watercertificatesummaryview.Where(
                    x => x.date_insert >= log.proj_date_start
                    && x.date_insert < AddDayToNullableDateTime(log.proj_date_end, 1)
                    && x.record_status).ToList();

                return certlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool UpdateOrderItemSentEmail(hlab_order_items orderitem)
        {
            try
            {
                if (orderitem.date_email_sent== null) orderitem.date_email_sent = DateTime.Now;
                _hlab_Db_Context.Attach(orderitem);
                _hlab_Db_Context.Entry(orderitem).Property(r => r.date_email_sent).IsModified = true;
                _hlab_Db_Context.Entry(orderitem).Property(r => r.email_tpl_id).IsModified = true;
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"Error HlabOrderRepository > UpdateOrderItemSentEmail : {exc.Message}");
                return false;
            }
        }

        public bool UpdateOrderItemUID(hlab_order_items item)
        {
            try
            {
                _hlab_Db_Context.hlab_order_items.Attach(item).Property(x => x.hl_code).IsModified = true;
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UpdateOrderItemUID(): {exc.Message}");
                return false;
            }
        }

        public int? CountTodaysRequests(DateTime date_request, string hl_code_prefix)
        {
            try
            {
                int count = 0;
                sp_CountTodaysRequests result = new sp_CountTodaysRequests();
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@p0",
                            SqlDbType =  System.Data.SqlDbType.Date,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = date_request
                        },
                        new SqlParameter() {
                            ParameterName = "@p1",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = hl_code_prefix
                        },
                        new SqlParameter() {
                            ParameterName = "@p3",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Output
                        },
                };
                int affectedrows=  _hlab_Db_Context.Database.ExecuteSqlCommand("sp_CountTodaysRequests @p0, @p1, @p3 output", param);
                count = Convert.ToInt32(param[2].Value);
                return count;
            }
            catch (Exception exc)
            {
                _logger.LogError($"CountTodaysRequests(): {exc.Message}");
                return 0;
            }
        }
    }
}
