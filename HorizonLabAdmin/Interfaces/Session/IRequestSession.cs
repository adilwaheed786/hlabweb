using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces.Session
{
    public interface IRequestSession
    {
        void SetTestRequestSessionInfo(RequestSessionParameter request);
        void SetTestRequestPageFilter(ordersearch order_search);
        void SetSearchOrder(string search_order);
        void SetSearchRequestId(int requestid);
        void SetSearchRequestItemId(int requesitemtid);
        void SetSearchRequestStartDate(DateTime? start_date);
        void SetSearchRequestEndDate(DateTime? end_date);
        void SetSearchFilter(string search_filter_value);
        void SetSearchValue(string search_value);

        int GetSearchRequestId();
        int GetSelectedProjectId();
        int GetSearchRequestItemId();

        DateTime? GetSearchRequestStartDate();
        DateTime? GetSearchRequestEndDate();

        bool IsSearchRequestStartDateHasValue();
        bool IsSearchRequestEndDateHasValue();

        string GetSearchOrderSession();
        string GetRequestSearchValueSession();
        string GetRequestSearchFilterSession();
    }
}
