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
    public class TestTransactionSession: RequestSession, ITestTransactionSession
    {
        private readonly ILogger<TestTransactionSession> _logger;
        private readonly string key_testtransaction_display_option = "testtransaction_display_option";
        private readonly string key_waterbacteria_transaction_id = "waterbacteria_transactionid";
        private readonly string key_search_pkgclass_id = "search_pkgclassid";
        private readonly string key_search_search_package = "search_searchPackage";
        private readonly string key_search_project_id = "project_id";
        public TestTransactionSession(IHttpContextAccessor _httpContextAccessor, ILogger<TestTransactionSession> logger) : base(_httpContextAccessor, logger)
        {
            _logger = logger;
        }

        public void SetTestTransactionDisplayOptionSession(string option)
        {
            SetStringSessionWithNullValidation(new StringSessionParameter { Key = key_testtransaction_display_option, Value = option });
        }

        public bool IsTestTransactionDisplayOptionHasValue()
        {
            return IsSessionStringHasValue(key_testtransaction_display_option);
        }

        public int GetIntSearchWaterBacteriaTransactionId()
        {
            return GetSessionIntValue(key_waterbacteria_transaction_id);
        }

        public void SetIntSearchWaterBacteriaTransactionId(int transactionid)
        {
            SetIntSession(new IntSessionParameter { Key = key_waterbacteria_transaction_id, Value = transactionid });
        }

        public void SetIntSearchPackakgeClassId(int package_class_id)
        {
            SetIntSession(new IntSessionParameter {Key = key_search_pkgclass_id, Value = package_class_id });
        }

        public void SetIntSearchPackakgeId(int packageid)
        {
            SetIntSession(new IntSessionParameter { Key = key_search_search_package, Value = packageid });
        }
        
        public void SetIntProjectId(int projectid)
        {
            SetIntSession(new IntSessionParameter { Key = key_search_project_id, Value = projectid });
        }
        public int GetSearchPackakgeId()
        {
            return GetSessionIntValue(key_search_search_package);
        }
        public int GetSearchPackakgeClassId()
        {
            return GetSessionIntValue(key_search_pkgclass_id);
        }

        public int GetProjectId()
        {
            return GetSessionIntValue(key_search_project_id);
        }
    }
}
