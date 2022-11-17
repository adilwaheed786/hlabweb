using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabInvoiceRepository : Interface_hlab_invoice
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabInvoiceRepository> _logger;

        public HlabInvoiceRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabInvoiceRepository> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public bool AddNewInvoice(hlab_invoice invoice)
        {
            try
            {
                _hlab_Db_Context.hlab_invoice.Add(invoice);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public IEnumerable<sp_gethorizonlabtransactioninvoices> GetTransactionInvoice(sp_gethorizonlabtransactioninvoices invoice)
        {
            try
            {
                return _hlab_Db_Context.transactioninvoicelist.FromSql("sp_GetHorizonLabTransactionInvoices " + invoice.trans_id).ToList();
            }
            catch(Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool UpdateInvoice(hlab_invoice invoice)
        {
            try
            {
                _hlab_Db_Context.hlab_invoice.Update(invoice);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }
    }
}
