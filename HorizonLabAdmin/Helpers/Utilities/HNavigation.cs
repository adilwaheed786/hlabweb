using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HNavigation:INavigation
    {
        private readonly IUtility _utility;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly ILogger<HNavigation> _logger;

        public HNavigation(IHttpContextAccessor httpContextAccessor, IHorizonLabSession sessionHelper, IUtility utility, ILogger<HNavigation> logger)
        {
            _sessionHelper = sessionHelper;
            _utility = utility;
            _logger = logger;
        }

        public List<SelectListItem> ListSortByItems()
        {
            List<SelectListItem> sortByList = new List<SelectListItem>();
            sortByList.Add(new SelectListItem { Text = "", Value = "" });
            sortByList.Add(new SelectListItem { Text = "Request ID", Value = "reqid" });
            sortByList.Add(new SelectListItem { Text = "Customer ID", Value = "custid" });
            sortByList.Add(new SelectListItem { Text = "Customer Name", Value = "first_name" });
            sortByList.Add(new SelectListItem { Text = "Request Date", Value = "request_datetime" });
            return sortByList;
        }

        public List<SelectListItem> ListSortOptionItems()
        {
            List<SelectListItem> sortOptionList = new List<SelectListItem>();
            sortOptionList.Add(new SelectListItem { Text = "", Value = "" });
            sortOptionList.Add(new SelectListItem { Text = "Ascending", Value = "asc" });
            sortOptionList.Add(new SelectListItem { Text = "Descending", Value = "desc" });
            return sortOptionList;
        }

        public List<SelectListItem> SetSelectedItemFromSortByStringList(List<SelectListItem> selectlistitem)
        {
            selectlistitem = _utility.SetSelectedItemFromList(selectlistitem, _sessionHelper.GetSortByValue());
            return selectlistitem;
        }

        public string GetSelectedItemFromSortByStringList(List<SelectListItem> selectlistitem)
        {
            return _utility.GetSelectedTextFromSelectList(selectlistitem, _sessionHelper.GetSortByValue());
        }

        public List<SelectListItem> SetSelectedItemFromSortByOptionList(List<SelectListItem> selectlistitem)
        {
            selectlistitem = _utility.SetSelectedItemFromList(selectlistitem, _sessionHelper.GetSortByOptionValue());
            return selectlistitem;
        }

        public string GetSelectedItemFromSortByOptionList(List<SelectListItem> selectlistitem)
        {
            return _utility.GetSelectedTextFromSelectList(selectlistitem, _sessionHelper.GetSortByOptionValue());
        }

        public int SetRecordEnd(int record_count, int record_per_batch)
        {
            return record_count > record_per_batch ? record_per_batch - 1 : record_count;
        }

        public List<testtransactionsview> SortTransactionList(TestTransactionSearchParameters parameter)
        {
            try
            {
                if (_sessionHelper.IsSearchSortByHasValue() && _sessionHelper.IsSearchSortByOptionHasValue())
                {
                    if (_sessionHelper.GetSortByOptionValue().ToLower() == "asc")
                    {
                        parameter.transList = parameter.transList.OrderBy(x => x.GetType().GetProperty(_sessionHelper.GetSortByValue()).GetValue(x, null)).ToList();
                    }
                    else
                    {
                        parameter.transList = parameter.transList.OrderByDescending(x => x.GetType().GetProperty(_sessionHelper.GetSortByValue()).GetValue(x, null)).ToList();
                    }
                }
                return parameter.transList;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateTestTransactionParameterFromSession() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public TestTransactionSearchParameters SetSortByFields(TestTransactionSearchParameters parameter)
        {
            try
            {
                if (_sessionHelper.IsSearchSortByHasValue() && _sessionHelper.IsSearchSortByOptionHasValue())
                {
                    if (_sessionHelper.IsSearchSortByHasValue())
                    {
                        parameter.sortByList = _utility.SetSelectedItemFromList(parameter.sortByList, _sessionHelper.GetSortByValue());
                        parameter.str_sortBy = _sessionHelper.GetSortByValue();
                    }

                    if (_sessionHelper.IsSearchSortByOptionHasValue())
                    {
                        parameter.sortOptionList = _utility.SetSelectedItemFromList(parameter.sortOptionList, _sessionHelper.GetSortByOptionValue());
                        parameter.str_sortOption = _sessionHelper.GetSortByOptionValue();
                    }                   
                }
                return parameter;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > SetSortByOptionFields() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public TestTransactionSearchParameters SetSearchDateFieldValues(TestTransactionSearchParameters search_param, test_transaction parameter)
        {
            try
            {
                search_param.str_searchSubmtDateStart = "";
                search_param.str_searchSubmtDateEnd = "";
                search_param.str_searchRcvdDateStart = "";
                search_param.str_searchRcvdDateEnd = "";
                search_param.str_searchTestDateStart = "";
                search_param.str_searchTestDateEnd = "";
                search_param.str_searchProjectDateStart = "";
                search_param.str_searchProjectDateEnd = "";
                if (parameter.submtd_datetime_start != null && parameter.submtd_datetime_start != DateTime.MinValue) search_param.str_searchSubmtDateStart = parameter.submtd_datetime_start?.ToString("dd/MM/yyyy");

                if (parameter.submtd_datetime_end != null && parameter.submtd_datetime_end != DateTime.MinValue) search_param.str_searchSubmtDateEnd = parameter.submtd_datetime_end?.ToString("dd/MM/yyyy");

                if (parameter.rcv_date_start != null && parameter.rcv_date_start != DateTime.MinValue) search_param.str_searchRcvdDateStart = parameter.rcv_date_start?.ToString("dd/MM/yyyy");

                if (parameter.rcv_date_end != null && parameter.rcv_date_end != DateTime.MinValue) search_param.str_searchRcvdDateEnd = parameter.rcv_date_end?.ToString("dd/MM/yyyy");

                if (parameter.test_date_start != null && parameter.test_date_start != DateTime.MinValue) search_param.str_searchTestDateStart = parameter.test_date_start?.ToString("dd/MM/yyyy");

                if (parameter.test_date_end != null && parameter.test_date_end != DateTime.MinValue) search_param.str_searchTestDateEnd = parameter.test_date_end?.ToString("dd/MM/yyyy");
                
                if (parameter.project_date_start != null && parameter.project_date_start != DateTime.MinValue) search_param.str_searchProjectDateStart = parameter.project_date_start?.ToString("dd/MM/yyyy");
                
                if (parameter.project_date_end != null && parameter.project_date_end != DateTime.MinValue) search_param.str_searchProjectDateEnd = parameter.project_date_end?.ToString("dd/MM/yyyy");

                return search_param;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > SetSortByOptionFields() : {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}
