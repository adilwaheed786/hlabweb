using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IPayment
    {
        bool proceed_csv_process { get; set; }
        string InsertResult { get; set; }

        void UpdateCustomerRequestPayment(OrderPageForm request_view_model);
        sp_gethorizonlabtransactioninvoices CreateBlankInvoiceItem();
        bool InsertWaterBacteriaPaymentsCsvToDatabase(WaterBacteriaObject water_bacteria_csv_row);
        List<orderpaymentsview> ListRequestPayments(int request_id);
        List<hlab_test_payment_types> ListAllDbPaymentTypes();
    }
}
