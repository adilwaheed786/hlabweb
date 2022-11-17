using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities.Session
{
    public class RequestSession: NavigationSession, IRequestSession
    {
        private readonly ILogger<RequestSession> _logger;
        private readonly string key_search_request_id = "search_requestId";
        private readonly string key_search_transaction_id = "search_transaction_id";
        private readonly string key_search_received_date_start = "search_RequestDateStart";
        private readonly string key_search_received_date_end = "search_RequestDateEnd";
        private readonly string key_search_request_item_id = "search_request_item_id";
        private readonly string key_search_order = "search_order";
        private readonly string key_search_filter = "searchfilter";
        private readonly string key_search_value = "searchvalue";
        private readonly string firstname = "fn";
        private readonly string lastname = "ln";
        private readonly string customerid= "ci";
        private readonly string requestitemid= "oid";
        private readonly string key_search_project_id = "project_id";
        public RequestSession(IHttpContextAccessor _httpContextAccessor, ILogger<RequestSession> logger) : base(_httpContextAccessor, logger)
        {
            _logger = logger;
        }

        public void SetTestRequestSessionInfo(RequestSessionParameter request)
        {            
            SetIntSession(new IntSessionParameter { Key = key_search_request_id, Value = request.request_id});
            SetIntSession(new IntSessionParameter { Key = key_search_project_id, Value = request.project_id });
            SetIntSession(new IntSessionParameter { Key = key_search_transaction_id, Value = request.transaction_id});
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_search_received_date_start, Value = request.request_date_start });
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_search_received_date_end, Value = request.request_date_end });
        
        }

        public int GetSearchRequestId()
        {
            return GetSessionIntValue(key_search_request_id);
        }
        public int GetSelectedProjectId()
        {
            return GetSessionIntValue(key_search_project_id);
        }

        public DateTime? GetSearchRequestStartDate()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_search_received_date_start));
            }
            catch(Exception exc)
            {
                _logger.LogError($"RequestSession > GetSearchRequestStartDate(): {exc.InnerException}");
                throw exc.InnerException;
            }
            
        }

        public DateTime? GetSearchRequestEndDate()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_search_received_date_end));
            }
            catch (Exception exc)
            {
                _logger.LogError($"RequestSession > GetSearchRequestEndDate(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        public bool IsSearchRequestStartDateHasValue()
        {
            return IsSessionStringHasValue(key_search_received_date_start);
        }

        public bool IsSearchRequestEndDateHasValue()
        {
            return IsSessionStringHasValue(key_search_received_date_end);
        }

        public int GetSearchRequestItemId()
        {
            return GetSessionIntValue(key_search_request_item_id);
        }

        public void SetTestRequestPageFilter(ordersearch order_search)
        {
            if (order_search.searchfilter == firstname)
            {
                SetSearchCustomerSessionInfo(new hlab_customers
                {
                    first_name = order_search.searchvalue != null ? order_search.searchvalue : ""
                });
            }

            if (order_search.searchfilter == lastname)
            {
                SetSearchCustomerSessionInfo(new hlab_customers
                {
                    last_name = order_search.searchvalue != null ? order_search.searchvalue : ""
                });
            }

            if (order_search.searchfilter == customerid)
            {
                SetSearchCustomerSessionInfo(new hlab_customers
                {
                    customer_id = Convert.ToInt32(order_search.searchvalue),
                });
            }

            if (order_search.searchfilter == requestitemid)
            {
                SetTestRequestSessionInfo(new RequestSessionParameter()
                {
                    search_order = "true",
                    request_id = Convert.ToInt32(order_search.searchvalue)
                });
            }
        }

        public string GetSearchOrderSession()
        {
            return GetSessionStringValue(key_search_order);
        }

        public void SetSearchOrder(string search_order)
        {
            SetStringSession(new StringSessionParameter { Key = key_search_order, Value = search_order });
        }

        public void SetSearchRequestId(int requestid)
        {
            SetIntSession(new IntSessionParameter { Key = key_search_request_id, Value = requestid });
        }

        public void SetSearchRequestItemId(int requesitemtid)
        {
            SetIntSession(new IntSessionParameter { Key = key_search_request_item_id, Value = requesitemtid });
        }

        public void SetSearchRequestStartDate(DateTime? start_date)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_search_received_date_start, Value = start_date });            
        }

        public void SetSearchRequestEndDate(DateTime? end_date)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_search_received_date_end, Value = end_date });
        }

        public void SetSearchFilter(string search_filter)
        {
            SetStringSession(new StringSessionParameter { Key = key_search_filter, Value= search_filter  });
        }

        public void SetSearchValue(string search_value)
        {
            SetStringSession(new StringSessionParameter { Key = key_search_value, Value = search_value });
        }

        public string GetRequestSearchValueSession()
        {
            try
            {
                return GetSessionStringValue(key_search_value);
            }
            catch (Exception exc)
            {
                _logger.LogError($"RequestSession > GetRequestSearchValueSession(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        public string GetRequestSearchFilterSession()
        {
            try
            {
                return GetSessionStringValue(key_search_filter);
            }
            catch (Exception exc)
            {
                _logger.LogError($"RequestSession > GetRequestSearchFilterSession(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}
