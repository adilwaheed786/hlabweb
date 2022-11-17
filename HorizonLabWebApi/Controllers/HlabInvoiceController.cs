using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_invoice")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabInvoiceController : ControllerBase
    {
        private Interface_hlab_invoice _hlabInvoice;

        public HlabInvoiceController(Interface_hlab_invoice hlabInvoice)
        {
            _hlabInvoice = hlabInvoice;
        }

        [HttpPost("gettransactioninvoice")]
        public List<sp_gethorizonlabtransactioninvoices> GetTransactionInvoice(sp_gethorizonlabtransactioninvoices invoice)
        {
            var invoice_list = _hlabInvoice.GetTransactionInvoice(invoice).ToList(); ;
            return invoice_list;
        }

        [HttpPost("updateinvoice")]
        public ActionResult UpdateInvoice(hlab_invoice invoice)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (invoice.invoice_id == 0) return BadRequest("Can't save changes with no invoice id value");
                if (invoice.trans_id == 0) return BadRequest("Can't save changes with no transaction id value");
                bool result = _hlabInvoice.UpdateInvoice(invoice);
                if (result) return Ok();
                return BadRequest("UpdateInvoice Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateInvoice Exception Error: " + xc);
            }
        }

        [HttpPost("insertnewinvoice")]
        public ActionResult InsertNewInvoice(hlab_invoice invoice)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");                
                if (invoice.trans_id == 0) return BadRequest("Can't save invoice with no transaction id value");
                bool result = _hlabInvoice.AddNewInvoice(invoice);
                if (result) return Ok();
                return BadRequest("InsertNewInvoice Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("InsertNewInvoice Exception Error: " + xc);
            }
        }
    }
}