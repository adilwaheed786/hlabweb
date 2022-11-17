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
    public interface IWaterBacteria
    {
        bool proceed_csv_process { get; set; }
        string InsertResult { get; set; }

        WaterBacteriaCsvFile PopulateCsvFileFromExistingCustomerDb(WaterBacteriaCsvFile item);
        WaterBacteriaCsvFile PopulateCsvFileFromNewCustomerDb(WaterBacteriaCsvFile item);
        WaterBacteriaCsvFile PopulateCsvFileFromPreviousCsvItem(WaterBacteriaCsvFile item);
        WaterBacteriaBulkUploadObject ParseWaterBacteriaFile(IFormFile waterbacteriacsv);
        hlab_test_transactions GenerateWaterBacteriaDbObject(WaterBacteriaObject water_bacteria);
        WaterBacteriaCsvFile AssignCouponToWaterTestTransaction(WaterBacteriaCsvFile water_bateria_csv);
        TestResultPageViewModel PopulateWaterBacteriaEditPageSelectListFields(string transid);
        TestResultPageViewModel PopulateWaterBacteriaEditPageListObjects(TestResultPageViewModel page_model, string transid);
        TestResultPageViewModel PopulateNewWaterBacteriaPageObject(TestResultPageViewModel page_model);
        DateTime? ProcessCsvDateTime(WaterBacteriaObject water_bacteria_obj);
        int GetDbProjectId(WaterBacteriaCsvFile water_bateria_csv);
        int GetDbReceivedById(WaterBacteriaCsvFile water_bateria_csv);
        int GetDbSampleTypeId(WaterBacteriaCsvFile water_bateria_csv);
        int GetDbMunicipalityId(WaterBacteriaCsvFile water_bateria_csv);
        hlab_test_transactions InitializeTestTransactionDbObject(WaterBacteriaCsvFile water_bateria_csv);
        test_transaction PopulateTestTransactionParameterObjFromSession();
        bool InsertCsvWaterBacteriaRowToDB(WaterBacteriaObject water_bacteria_object);
        bool IsOkToProcessCsv();
        bool IsWaterBacteriaCsvNotNullOrEmpty(hlab_test_transactions water_bacteria_csv);
    }
}
