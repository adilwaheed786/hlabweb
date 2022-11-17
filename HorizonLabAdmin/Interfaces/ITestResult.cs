using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ITestResult
    {
        TestResultPageViewModel GetTestResultListFromRequestItems(List<orderdetailsview> request_item_list, TestResultPageViewModel page_model);
        List<testresultsview> GetTestResultFromDb(int transactionid);
        bool AddNewTestResultToDb(hlab_test_results test_result_obj);
        bool UpdateTestResultDb(hlab_test_results test_result_obj);
        bool DeleteTestResultDb(int result_id);
    }
}
