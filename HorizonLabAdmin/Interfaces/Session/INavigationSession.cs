using HorizonLabAdmin.Helpers.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces.Session
{
    public interface INavigationSession
    {
        void SetStartRecordSession(int record_start);//for water bacteria
        void SetEndRecordSession(int record_end);//for water bacteria
        void SetStartCertificateRecordSession(int record_start);
        void SetEndCertificateRecordSession(int record_end);
        void SetStartCustomerRecordSession(int record_start);
        void SetEndCustomerRecordSession(int record_end);
        void SetSortByString(string sort_string);
        void SetSortByOption(string sort_option);
        void SetSubmittedStartDate(DateTime date_parameter);
        void SetSubmittedEndDate(DateTime date_parameter);
        void SetReceivedStartDate(DateTime date_parameter);
        void SetReceivedEndDate(DateTime date_parameter);
        void SetTestStartDate(DateTime date_parameter);
        void SetTestdEndDate(DateTime date_parameter);
        void SetProjectStartDate(DateTime date_parameter);
        void SetProjectEndDate(DateTime date_parameter);
        bool IsStartRecordHasValue();
        bool IsEndRecordHasValue();
        bool IsCertStartRecordHasValue();
        bool IsCertEndRecordHasValue();
        bool IsSearchSortByHasValue();
        bool IsSearchSortByOptionHasValue();
        bool IsSearchSubmitStartDateHasValue();
        bool IsSearchSubmitEndDateHasValue();
        bool IsReceivedStartDateHasValue();
        bool IsReceivedEndDateHasValue();
        bool IsTestStartDateHasValue();
        bool IsTestdEndDatHasValue();
        bool IsProjectStartDateHasValue();
        bool IsProjectEndDateHasValue();
        int GetSessionRecordStart();//for water bacteria
        int GetSessionRecordEnd();//for water bacteria
        int GetStartCertificateRecordSession();
        int GetEndCertificateRecordSession();
        int GetStartCustomerRecordSession();
        int GetEndCustomerRecordSession();

        DateTime? GetSessionSubmitStart();
        DateTime? GetSessionSubmitEnd();
        DateTime? GetSessionReceivedStartDate();
        DateTime? GetSessionReceviedEndDate();
        DateTime? GetSessionTestStartDate();
        DateTime? GetSessionTestEndDate();
        DateTime? GetSessionProjStartDate();
        DateTime? GetSessionProjEndDate();
        string GetSortByValue();
        string GetSortByOptionValue();
    }
}
