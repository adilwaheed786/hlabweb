using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_test_results
    {
        IEnumerable<testresultsview> GetAllTestResults(testresultsview htr);
        IEnumerable<sp_gettestresults> GetTestResults(int transid);
        hlab_test_results GetTransactionDetails(int result_id);
        bool AddTestResults(hlab_test_results htt);
        bool UpdateTestResults(hlab_test_results htt);
        bool DeleteTestResults(int resultid);
        bool DeleteTestResultsByTransId(int transid);
    }
}
