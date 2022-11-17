using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_test_payments
    {
        IEnumerable<orderpaymentsview> GetAllPayments(orderpaymentsview orderpayment);
        orderpaymentsview GetPaymentInfo(orderpaymentsview orderpayment);
        IEnumerable<hlab_test_payment_options> GetAllPaymentOptions();
        IEnumerable<hlab_test_payment_types> GetAllPaymentTypes();
        bool AddPayment(hlab_test_payments payment);
        bool UpdatePayment(hlab_test_payments payment);
        bool DeleteOnePayment(hlab_test_payments paymentid);
        bool DeleteBulkPayment(hlab_test_payments orderid);
    }
}
