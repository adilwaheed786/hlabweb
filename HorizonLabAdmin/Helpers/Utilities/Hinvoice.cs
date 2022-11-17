using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class Hinvoice: Iinvoice
    {
        private readonly IUtility _utility;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly ILogger<Hinvoice> _logger;
        private readonly Interface_hlab_invoice _hlabInvoice;

        public Hinvoice(IHttpContextAccessor httpContextAccessor, IHorizonLabSession sessionHelper, IUtility utility, ILogger<Hinvoice> logger, Interface_hlab_invoice hlabInvoice)
        {
            _sessionHelper = sessionHelper;
            _utility = utility;
            _logger = logger;
            _hlabInvoice = hlabInvoice;
        }

        public List<sp_gethorizonlabtransactioninvoices> GetInvoiceFromDb(int transactionid)
        {
            try
            {
                return _hlabInvoice.GetTransactionInvoice(new sp_gethorizonlabtransactioninvoices { trans_id = transactionid }).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateTestTransactionParameterFromSession() : {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}
