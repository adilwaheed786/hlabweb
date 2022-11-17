using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface Iinvoice
    {
        List<sp_gethorizonlabtransactioninvoices> GetInvoiceFromDb(int transactionid);
    }
}
