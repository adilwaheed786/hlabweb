using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_customer")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabCustomerController : ControllerBase
    {
        private Interface_hlab_customers _hlabCustomers;
        private Interface_EmailSender _hlabEmailSender;
        private readonly ILogger<HlabTestTransactionsController> _logger;

        public HlabCustomerController(Interface_hlab_customers hlabCustomers, ILogger<HlabTestTransactionsController> logger, Interface_EmailSender hlabEmailSender)
        {
            _hlabCustomers = hlabCustomers;
            _logger = logger;
            _hlabEmailSender = hlabEmailSender;
        }

        [HttpPost("getcustomerlist")]
        public List<horizonlabcustomerview> GetCustomerList(horizonlabcustomerview hc)
        {
            try
            {
                List<horizonlabcustomerview> customer_list = new List<horizonlabcustomerview>();
                customer_list = _hlabCustomers.GetAllCustomers(hc).ToList();
                return customer_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("getcustomerdetailsbyobject")]
        public List<horizonlabcustomerview> GetCustomerDetailsByObject(hlab_customers hc)
        {
            try
            {
                List<horizonlabcustomerview> customer_list = new List<horizonlabcustomerview>();
                customer_list = _hlabCustomers.ListCustomersDetails(hc).ToList();
                return customer_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("getcustomerdetails")]
        public horizonlabcustomerview GetCustomerDetails(hlab_customers input_customer)
        {
            try
            {
                horizonlabcustomerview customer = _hlabCustomers.GetCustomersDetails(input_customer);
                return customer;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("getcustomerphone")]
        public List<hlab_customer_phone> GetCustomerPhone(hlab_customers input_customer)
        {
            try
            {
                List<hlab_customer_phone>  phone_list = _hlabCustomers.GetCustomerPhone(input_customer).ToList();
                return phone_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("getcustomeremail")]
        public List<hlab_customer_email> GetCustomerEmail(hlab_customers input_customer)
        {
            try
            {
                List<hlab_customer_email> email_list = _hlabCustomers.GetCustomerEmail(input_customer).ToList();
                return email_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("deactivatecustomer")]
        public bool DeativateCustomer(hlab_customers input_customer)
        {
            try
            {
                return _hlabCustomers.DeleteCustomer(input_customer);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        [HttpPost("addnewcustomer")]
        public int AddNewCustomer(customerparameters customer)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                int customerid = _hlabCustomers.AddCustomer(customer);                
                return customerid;
            }
            catch (Exception xc)
            {
                return 0;
            }
        }

        [HttpPost("updatecustomer")]
        public ActionResult UpdateCustomer(customerparameters customer)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("UpdateCustomer : Not a valid model");
                bool result = _hlabCustomers.UpdateCustomer(customer);
                if (result) return Ok();
                return BadRequest("UpdateCustomer : UpdateCustomer Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateCustomer Exception Error: " + xc);
            }
        }

        [HttpPost("isemailassigned")]
        public string IsEmailAssigned(customerparameters customer)
        {
            try
            {
                if (_hlabCustomers.CheckIfEmailAssigned(customer)) return "true";
                return "false";
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return "Exception Error Logged";
            }
        }

        [HttpPost("emaildocs")]
        public bool EmailDocs(emaildetails emaildetails)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabEmailSender.SendEMailByTestTransaction(emaildetails);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }
    }
}