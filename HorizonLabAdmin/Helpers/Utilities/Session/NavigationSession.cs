using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities.Session
{
    public class NavigationSession : CustomerSession, INavigationSession
    {
        private readonly ILogger<NavigationSession> _logger;

        private string key_TestStartDate = "search_TestDateStart";
        private string key_TestEndDate = "search_TestDateEnd";
        private string key_ProjStartDate = "search_ProjDateStart";
        private string key_ProjEndDate = "search_ProjDateEnd";
        private string key_ReceviedStartDate = "search_ReceivedDateStart";
        private string key_ReceviedEndDate = "search_ReceivedDateEnd";
        private string key_SubmittedStartDate = "search_SubmtdDateStart";
        private string key_SubmittedEndDate = "search_SubmtdDateEnd";
        private string key_waterbacteria_record_start = "record_start";
        private string key_waterbacteria_record_end = "record_end";
        private string key_sort_by_string = "sortByString";
        private string key_sort_by_option = "sortByOption";
        private string key_certification_record_start = "cert_record_start";
        private string key_certification_record_end = "cert_record_end";
        private string key_customerpage_rec_start = "customerpage_rec_start";
        private string key_customerpage_rec_end = "customerpage_rec_end";
       
        public NavigationSession(IHttpContextAccessor _httpContextAccessor, ILogger<NavigationSession> logger) : base(_httpContextAccessor, logger)
        {
            _logger = logger;
        }

        public void SetStartRecordSession(int record_start)
        {
            SetIntSession(new IntSessionParameter { Key = key_waterbacteria_record_start, Value = record_start });
        }

        public void SetEndRecordSession(int record_end)
        {
            SetIntSession(new IntSessionParameter { Key = key_waterbacteria_record_end, Value = record_end });
        }

        public void SetSortByString(string sort_string)
        {
            SetStringSessionWithNullValidation(new StringSessionParameter { Key = key_sort_by_string, Value = sort_string });
        }

        public void SetSortByOption(string sort_option)
        {
            SetStringSessionWithNullValidation(new StringSessionParameter { Key = key_sort_by_option, Value = sort_option });
        }

        public bool IsStartRecordHasValue()
        {
            return IsSessionStringHasValue(key_waterbacteria_record_start);
        }

        public bool IsEndRecordHasValue()
        {
            return IsSessionStringHasValue(key_waterbacteria_record_end);
        }

        public int GetSessionRecordStart()
        {
            return GetSessionIntValue(key_waterbacteria_record_start);
        }

        public int GetSessionRecordEnd()
        {
            return GetSessionIntValue(key_waterbacteria_record_end);
        }

        public bool IsSearchSortByHasValue()
        {
            return IsSessionStringHasValue(key_sort_by_string);
        }

        public bool IsSearchSortByOptionHasValue()
        {
            return IsSessionStringHasValue(key_sort_by_option);
        }

        public string GetSortByValue()
        {
            return GetSessionStringValue(key_sort_by_string);
        }

        public string GetSortByOptionValue()
        {
            return GetSessionStringValue(key_sort_by_option);
        }

        public void SetSubmittedStartDate(DateTime date_parameter)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_SubmittedStartDate, Value = date_parameter });
        }

        public void SetSubmittedEndDate(DateTime date_parameter)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_SubmittedEndDate, Value = date_parameter });
        }

        public void SetReceivedStartDate(DateTime date_parameter)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_ReceviedStartDate, Value = date_parameter });
        }

        public void SetReceivedEndDate(DateTime date_parameter)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_ReceviedEndDate, Value = date_parameter });
        }

        public void SetTestStartDate(DateTime date_parameter)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_TestStartDate, Value = date_parameter });
        }

        public void SetTestdEndDate(DateTime date_parameter)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_TestEndDate, Value = date_parameter });
        }
        public void SetProjectStartDate(DateTime date_parameter)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_ProjStartDate, Value = date_parameter });
        }
        public void SetProjectEndDate(DateTime date_parameter)
        {
            SetDateTimeSession(new DateTimeSessionParameter { Key = key_ProjEndDate, Value = date_parameter });
        }
        
        public bool IsReceivedStartDateHasValue()
        {
            return IsSessionStringHasValue(key_ReceviedStartDate);
        }

        public bool IsReceivedEndDateHasValue()
        {
            return IsSessionStringHasValue(key_ReceviedEndDate);
        }

        public bool IsTestStartDateHasValue()
        {
            return IsSessionStringHasValue(key_TestStartDate);
        }

        public bool IsTestdEndDatHasValue()
        {
            return IsSessionStringHasValue(key_TestEndDate);
        }
        public bool IsProjectStartDateHasValue()
        {
            return IsSessionStringHasValue(key_ProjStartDate);
        }
  
        public bool IsProjectEndDateHasValue()
        {
            return IsSessionStringHasValue(key_ProjEndDate);
        }
        public bool IsSearchSubmitStartDateHasValue()
        {
            return IsSessionStringHasValue(key_SubmittedStartDate);
        }

        public bool IsSearchSubmitEndDateHasValue()
        {
            return IsSessionStringHasValue(key_SubmittedEndDate);
        }

        public DateTime? GetSessionReceivedStartDate()
        {
            try
            {
                return DateTime.Parse(GetSessionStringValue(key_ReceviedStartDate));
            }
            catch (Exception exc)
            {
                _logger.LogError($"NavigationSession > GetSessionReceivedStartDate(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public DateTime? GetSessionReceviedEndDate()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_ReceviedEndDate));
            }
            catch (Exception exc)
            {
                _logger.LogError($"NavigationSession > GetSessionReceviedEndDate(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public DateTime? GetSessionTestStartDate()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_TestStartDate));
            }
            catch (Exception exc)
            {
                _logger.LogError($"NavigationSession > GetSessionTestStartDate(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public DateTime? GetSessionTestEndDate()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_TestEndDate));
            }
            catch (Exception exc)
            {
                _logger.LogError($"NavigationSession > GetSessionTestEndDate(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
        public DateTime? GetSessionProjStartDate()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_ProjStartDate));
            }
            catch (Exception exc)
            {
                _logger.LogError($"NavigationSession > GetSessionProjStartDate(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public DateTime? GetSessionProjEndDate()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_ProjEndDate));
            }
            catch (Exception exc)
            {
                _logger.LogError($"NavigationSession > GetSessionProjEndDate(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
        public DateTime? GetSessionSubmitStart()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_SubmittedStartDate));
            }
            catch (Exception exc)
            {
                _logger.LogError($"NavigationSession > GetSessionSubmitStart(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public DateTime? GetSessionSubmitEnd()
        {
            try
            {
                return FormatStringToDateTime(GetSessionStringValue(key_SubmittedEndDate));
            }
            catch (Exception exc)
            {
                _logger.LogError($"NavigationSession > GetSessionSubmitEnd(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public void SetStartCertificateRecordSession(int record_start)
        {
            SetIntSession(new IntSessionParameter { Key = key_certification_record_start, Value = record_start });
        }

        public void SetEndCertificateRecordSession(int record_end)
        {
            SetIntSession(new IntSessionParameter { Key = key_certification_record_end, Value = record_end });
        }

        public void SetStartCustomerRecordSession(int record_start)
        {
            SetIntSession(new IntSessionParameter { Key = key_customerpage_rec_start, Value = record_start });
        }

        public void SetEndCustomerRecordSession(int record_end)
        {
            SetIntSession(new IntSessionParameter { Key = key_customerpage_rec_end, Value = record_end });
        }

        public int GetStartCertificateRecordSession()
        {
            return GetSessionIntValue(key_certification_record_start);
        }

        public int GetEndCertificateRecordSession()
        {
            return GetSessionIntValue(key_certification_record_end);
        }

        public int GetStartCustomerRecordSession()
        {
            return GetSessionIntValue(key_customerpage_rec_start);
        }

        public int GetEndCustomerRecordSession()
        {
            return GetSessionIntValue(key_customerpage_rec_end);
        }

        public bool IsCertStartRecordHasValue()
        {
            return IsSessionStringHasValue(key_certification_record_start);
        }

        public bool IsCertEndRecordHasValue()
        {
            return IsSessionStringHasValue(key_certification_record_end);
        }
    }
}
