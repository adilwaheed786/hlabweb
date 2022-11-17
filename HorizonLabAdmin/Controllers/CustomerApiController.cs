using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    [Route("hlab_customer_api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly Interface_test_transactions _hlabTestTransRepo;
        private readonly Interface_hlab_customers _hlabCustomer;
        private readonly ILogger<CustomerApiController> _logger;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;
        private readonly Interface_hlab_test_coupon_logs _hlabCouponLog;
        private readonly Interface_hlab_cities _City;
        private readonly Interface_rural_municipality _Municipality;
        private hlab_customers _customer = new hlab_customers();

        public CustomerApiController(
            IConfiguration appConfig,
            ILogger<CustomerApiController> logger,
            IHostingEnvironment env,
            Interface_hlab_customers hlabCustomer,
            Interface_hlab_test_coupon_logs hlabCouponLog,
            Interface_test_transactions hlabTestTransRepo,
            Interface_hlab_cities City,
            Interface_rural_municipality Municipality
            )
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;
            _hlabCustomer = hlabCustomer;
            _City = City;
            _Municipality = Municipality;
            _hlabCouponLog = hlabCouponLog;
            _hlabTestTransRepo = hlabTestTransRepo;
        }

        [HttpGet("addnewcity")]
        public hlab_cities addnewcity(string newcity)
        {
            int manitoba = 3;
            try
            {
                if (!ModelState.IsValid) return null;
                hlab_cities new_city_object = new hlab_cities();
                new_city_object.city = newcity;
                new_city_object.province_id = manitoba; //default to Maniotba:3
                _City.AddNewCity(new_city_object);
                return new_city_object;
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return null;
            }
        }

        [HttpGet("addnewmuncipality")]
        public hlab_rural_municipalities addnewmuncipality(string newmuncipality)
        {
            int manitoba = 3;
            try
            {
                if (!ModelState.IsValid) return null;
                hlab_rural_municipalities new_object = new hlab_rural_municipalities();
                new_object.rural_municipality = newmuncipality;
                new_object.province_id = manitoba; //default to Maniotba:3
                new_object.id = _Municipality.AddRuralMunicipality(new_object);

                return new_object;
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return null;
            }
        }

        [HttpGet("generatecoupon")]
        public string generatecoupon(int customerid=0, int transid=0)
        {
            couponrecord couponrecord = new couponrecord();
            List<sp_getcustomercouponrecords> coupondbrecordlist = new List<sp_getcustomercouponrecords>();
            hlab_test_coupon_logs couponlog = new hlab_test_coupon_logs();
            int coupon_number;
            try
            {
                coupon_number = _hlabCouponLog.GenerateCoupon() ?? 0;
                couponrecord.customerid = customerid;
                coupondbrecordlist = _hlabCouponLog.GetCustomerCouponRecord(couponrecord).Where(x => x.coupon_issued_date.Value.AddMonths(3) >= DateTime.Now).ToList();
                if (coupondbrecordlist.Count == 0)//check if has valid coupon
                {
                    //log coupon for the customerid
                    couponlog.customer_id = customerid;
                    couponlog.coupon = coupon_number;
                    couponlog.coupon_issued_date = DateTime.Now;
                    couponlog.trans_id = transid;
                    _hlabCouponLog.LogCoupon(couponlog);
                    return coupon_number.ToString();
                }
                return "The customer might have an existing valid coupon. Do you wish to proceed?";
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return "Generating coupon failed because of Exception error. Please contact administrator!";
            }
        }

        [HttpGet("forced_generatecoupon")]
        public string forced_generatecoupon(int customerid = 0, int transid = 0)
        {
            couponrecord couponrecord = new couponrecord();
            List<sp_getcustomercouponrecords> coupondbrecordlist = new List<sp_getcustomercouponrecords>();
            hlab_test_coupon_logs couponlog = new hlab_test_coupon_logs();
            int coupon_number;
            try
            {
                coupon_number = _hlabCouponLog.GenerateCoupon() ?? 0;
                couponrecord.customerid = customerid;
                //log coupon for the customerid
                couponlog.customer_id = customerid;
                couponlog.coupon = coupon_number;
                couponlog.coupon_issued_date = DateTime.Now;
                couponlog.trans_id = transid;
                _hlabCouponLog.LogCoupon(couponlog);
                return coupon_number.ToString();
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return "Generating coupon failed because of Exception error. Please contact administrator!";
            }
        }

        [HttpGet("deletecoupon")]
        public string deletecoupon(int customerid = 0, int coupon = 0)
        {
            try
            {
                List<testtransactionsview> transactionrecords = new List<testtransactionsview>();
                test_transaction parameter = new test_transaction();
                parameter.assigned_coupon = coupon;
                transactionrecords = _hlabTestTransRepo.GetAllTransactions(parameter).ToList(); //check if coupon was assigned to other records
                if (transactionrecords.Count > 0) return $"Can not remove coupon {coupon}. It is currently assigned to a water sample record";
                if (_hlabCouponLog.RemoveCouponLog(customerid, coupon))
                {
                    return "0";
                }
                else
                {
                    return "Removing coupon failed!";
                }
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return "Removing coupon failed because of Exception error. Please contact administrator!";
            }
        }

        [HttpGet("validatecoupon")]
        public bool validatecoupon(int coupon, int customerid)
        {
            try
            {
                couponrecord couponrecord = new couponrecord();
                sp_getcustomercouponrecords coupondbrecords = new sp_getcustomercouponrecords();
                List<sp_getcustomercouponrecords> coupondbrecordlist = new List<sp_getcustomercouponrecords>();
                List<testtransactionsview> transactionrecords = new List<testtransactionsview>();
                test_transaction parameter = new test_transaction();
                parameter.assigned_coupon = coupon;
                couponrecord.customerid = customerid;
                //couponrecord.coupon = coupon;
                coupondbrecordlist = _hlabCouponLog.GetCustomerCouponRecord(couponrecord).Where(x => x.coupon == coupon).ToList(); //check if coupon exists
                coupondbrecordlist = coupondbrecordlist.Where(x => x.coupon_issued_date.Value.AddMonths(3) >= DateTime.Now).ToList(); //check if coupon is not expired
                transactionrecords = _hlabTestTransRepo.GetAllTransactions(parameter).ToList(); //check if coupon was assigned to other records

                if (transactionrecords.Count > 0) return false;
                if (coupondbrecordlist.Count == 0) return false;
                return true;
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("logcoupon")]
        public bool logcoupon(hlab_test_coupon_logs log)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabCouponLog.LogCoupon(log);
            }
            catch (Exception xc)
            {
                return false;
            }
        }

        [HttpPost("updatecouponlog")]
        public bool updatecouponlog(hlab_test_coupon_logs log)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabCouponLog.UpdateCouponLog(log);
            }
            catch (Exception xc)
            {
                return false;
            }
        }

        [HttpGet("getcustomerinfo")]
        public horizonlabcustomerview getcustomerinfo(int customerid)
        {
            try
            {
                if (customerid != 0)
                {                    
                    _customer.customer_id = customerid;
                    return _hlabCustomer.GetCustomersDetails(_customer);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return null;
            }
        }

        [HttpGet("getcustomernameduplicates")]
        public List<horizonlabcustomerview> getcustomernameduplicates(string firstname, string lastname)
        {
            List<horizonlabcustomerview> customerlist = new List<horizonlabcustomerview>();
            try
            {
                customerlist = _hlabCustomer.ListCustomersDetails(new hlab_customers { first_name = firstname, last_name=lastname});
                return customerlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpPost("addnewcustomer")]
        public hlab_customers addnewcustomer(customerparameters customer)
        {
            try
            {
                if (!ModelState.IsValid) return null;
                customer.hlab_customers.customer_id = _hlabCustomer.AddCustomer(customer);
                return customer.hlab_customers;
            }
            catch (Exception xc)
            {
                return null;
            }
        }
    }
}