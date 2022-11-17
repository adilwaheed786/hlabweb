using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ITransaction
    {
        string InsertResult { get; set; }

        bool IsTransactionIDNotEmpty(string transid);
        bool IsTransactionIDNotEmpty(int transid);
        bool IsTransactionPaymentOptionSet(List<sp_gethorizonlabtransactioninvoices> payment_list);
        bool IsTransactionPaymentTypeSet(List<sp_gethorizonlabtransactioninvoices> payment_list);
        bool IsTransactionHasPayments(List<sp_gethorizonlabtransactioninvoices> payment_list);
        bool IsTransactionHasNoGeneratedCoupon(sp_gethorizonlabtransactiondetails transaction);
        bool IsTestTrsansactionExistsInDb(hlab_test_transactions transaction_obj);
        bool UpdateTestTransactionDb(hlab_test_transactions transaction_obj);
        bool UpdateTransactionSupplies(List<int> supply_ids, int transactionid = 0);

        int InsertNewTestTransactionToDb(hlab_test_transactions transaction_obj);        
        int InsertCsvWaterBacteriaTestTransactionToDatabase(WaterBacteriaObject water_bacteria_object);        
        int GetEmailTemplateIdFromTestTransactionsViewPageData(TestTransactionSearchParameters test_transaction, int trnsaction_id);
        List<orderdetailsview> GetB1RequestItems(List<orderdetailsview> request_item_list);

        transaction_email InitTransactionParameter(sp_gethorizonlabtransactiondetails transactiondetails, hlab_email_templates email_template);
        TestResultPageViewModel InititalizeSelectListForWaterBacteria(TestResultPageViewModel page_model);
        TestResultPageViewModel SetSelectedItemsFromWaterBacteriaSelectItemList(TestResultPageViewModel page_model);
        ScannedSubsidyFileParameter ProcessScannedSubsidyImagePdfFile(ScannedSubsidyFileParameter file_parameter);
        hlab_test_transactions ConvertTransactionObjToDbObject(sp_gethorizonlabtransactiondetails test_transaction, bool record_status);
        sp_gethorizonlabtransactiondetails GetTransactionInformationFromSp(int transactionid);
        List<testtransactionsview> GetAllTestTransactionsView(test_transaction transaction_obj);
        List<sp_getsemipublicreport> GetSemiPublicTransactionsList(test_transaction transaction_obj);
        List<sp_getmonthlysubsidyreport> GetMonthlySubsidyTransactionsList(test_transaction transaction_obj);
        List<hlab_test_transaction_types> GetAllTestTransactionsTypes();
        List<hlab_test_descriptions> GetAllTestDescriptionDb();
        List<testrequestsupplyview> GetTestRequestSupplies(int reqid, int formid);
        void SetSearchTransactionParameters(TestTransactionSearchParameters search_parameter);
    }
}
