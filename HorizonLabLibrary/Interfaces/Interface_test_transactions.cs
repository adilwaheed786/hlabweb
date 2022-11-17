using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_test_transactions
    {
        IEnumerable<testtransactionsview> GetAllTransactions(test_transaction htt);
        IEnumerable<sp_getsemipublicreport> GetSemiPublicTransactions(test_transaction htt);
        IEnumerable<sp_getmonthlysubsidyreport> GetMonthlySubsidyReport(test_transaction htt);
        List<testrequestsupplyview> GetTestRequestSupplyList(int requestid, int formid);
        IEnumerable<hlab_test_transaction_types> GetTestTransactionTypes();
        IEnumerable<hlab_transaction_supplies> GetTransactionSuppliesIds(hlab_transaction_supplies parameter);
        sp_gethorizonlabtransactiondetails GetTransactionDetails(int trans_id);
        IEnumerable<sp_getdefaultpackageparameters> GetDefaultParameters(int packageid);
        bool IfWaterSampleExists(hlab_test_transactions htt);
        bool AddSubsidyFormImage(hlab_water_subsidy_form_images subsidyimage);
        bool UpdateSubsidyFormImage(hlab_water_subsidy_form_images subsidyimage);
        bool DeleteSubsidyFormImage(int id);
        int AddTransactionDetails(hlab_test_transactions htt);
        bool AddTransactionSupplies(transaction_supply_param supply_param);
        bool DeleteTransactionSupplies(int transactionid);
        bool UpdateTransactionDetails(hlab_test_transactions htt);
        bool PublishTestTransaction(int transaction_id);
        bool DeleteTransactionDetails(int transaction_id);
    }
}
