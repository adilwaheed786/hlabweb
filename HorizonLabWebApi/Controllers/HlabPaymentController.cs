using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabWebApi.ApiFilter;
using HorizonLabWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_payment")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabPaymentController : ControllerBase
    {
        private Interface_hlab_test_payments _hlabPayment;
        private readonly ILogger<HlabOrderController> _logger;

        public HlabPaymentController(Interface_hlab_test_payments hlabPayment, ILogger<HlabOrderController> logger)
        {
            _hlabPayment = hlabPayment;
            _logger = logger;
        }

        [HttpPost("addpayment")]
        public bool AddPayment(hlab_test_payments payment)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabPayment.AddPayment(payment);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("updatepayment")]
        public bool UpdatePayment(hlab_test_payments payment)
        {
            try
            {
                return _hlabPayment.UpdatePayment(payment);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("deletebulkpayment")]
        public bool DeleteBulkPayment(hlab_test_payments payment)
        {
            try
            {
                return _hlabPayment.DeleteBulkPayment(payment);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("deleteonepayment")]
        public bool DeleteOnePayment(hlab_test_payments payment)
        {
            try
            {
                return _hlabPayment.DeleteOnePayment(payment);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("getallpayments")]
        public IEnumerable<orderpaymentsview> GetAllPayments(orderpaymentsview orderpayment)
        {
            try
            {
                return _hlabPayment.GetAllPayments(orderpayment);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return null;
            }
        }

        [HttpPost("getpaymentinfo")]
        public orderpaymentsview GetPaymentInfo(orderpaymentsview orderpayment)
        {
            try
            {
                return _hlabPayment.GetPaymentInfo(orderpayment);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return null;
            }
        }

        [HttpGet("getpaymentoptionlist")]
        public List<hlab_test_payment_options> getpaymentoptionlist()
        {
            try
            {
                List<hlab_test_payment_options> options = _hlabPayment.GetAllPaymentOptions().ToList();
                return options;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getpaymenttypelist")]
        public List<hlab_test_payment_types> getpaymenttypelist()
        {
            try
            {
                List<hlab_test_payment_types> types = _hlabPayment.GetAllPaymentTypes().ToList();
                return types;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }
    }
}