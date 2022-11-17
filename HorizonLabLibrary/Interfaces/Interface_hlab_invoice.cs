using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_invoice
    {
        IEnumerable<sp_gethorizonlabtransactioninvoices> GetTransactionInvoice(sp_gethorizonlabtransactioninvoices invoice);
        bool UpdateInvoice(hlab_invoice invoice);
        bool AddNewInvoice(hlab_invoice invoice);
    }
}
