using HorizonLabLibrary;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class HlabInvoiceRepository : Interface_hlab_invoice
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private HorizonLabInvoiceLibrary _hlabInvoiceLib = new HorizonLabInvoiceLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabInvoiceRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public bool AddNewInvoice(hlab_invoice invoice)
        {
            var result = _hlabInvoiceLib.AddNewInvoice(invoice, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public IEnumerable<sp_gethorizonlabtransactioninvoices> GetTransactionInvoice(sp_gethorizonlabtransactioninvoices invoice)
        {
            var jsonList = _hlabInvoiceLib.GetTransactionInvoice(invoice, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var invoice_list = JsonConvert.DeserializeObject<List<sp_gethorizonlabtransactioninvoices>>(jsonList);
            return invoice_list;
        }

        public bool UpdateInvoice(hlab_invoice invoice)
        {
            var result = _hlabInvoiceLib.UpdateInvoiceChages(invoice, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
