using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HPayment : IPayment
    {
        private readonly IUtility _utility;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly Interface_hlab_test_payments _hlabPayment;
        private readonly ILogger<HPayment> _logger;
        public string InsertResult { get; set; }
        public bool proceed_csv_process { get; set; }

        public HPayment(IHorizonLabSession sessionHelper, IUtility utility, Interface_hlab_test_payments hlabPayment, ILogger<HPayment> logger)
        {
            _sessionHelper = sessionHelper;
            _utility = utility;
            _hlabPayment = hlabPayment;
            _logger = logger;
        }

        public sp_gethorizonlabtransactioninvoices CreateBlankInvoiceItem()
        {
            return new sp_gethorizonlabtransactioninvoices
            {
                invoice_id = 0,
                invoice_date = DateTime.Now,
                paid_by = "",
                payment_option_id = 0,
                payment_option = "",
                payment_type_id = 0,
                payment = "",
                trans_id = 0,
                paid_amount = 0,
                payment_date = null
            };
        }

        public bool InsertWaterBacteriaPaymentsCsvToDatabase(WaterBacteriaObject csv)
        {
            hlab_test_payment_types payment_option = new hlab_test_payment_types();
            List<hlab_test_payment_types> payment_option_list = new List<hlab_test_payment_types>();
            WaterBacteriaCsvFile csv_row = csv.current_csv_row;
            try
            {
                if (csv.request_id != 0)
                {
                    payment_option_list = _hlabPayment.GetAllPaymentTypes().ToList();
                    if (!string.IsNullOrEmpty(csv.payment))
                    {
                        try
                        {
                            payment_option = payment_option_list.Where(x => x.payment.ToLower() == csv.payment.ToLower()).FirstOrDefault();
                        }
                        catch (Exception exc)
                        {
                            payment_option = null;
                        }

                        if (payment_option != null)
                        {
                            try
                            {
                                if (csv.proceed_csv_process)
                                {
                                    if (!string.IsNullOrEmpty(csv.amount))
                                        _hlabPayment.AddPayment(new hlab_test_payments
                                        {
                                            order_id = csv.request_id,
                                            payment_date = DateTime.Now,
                                            paid_amount = Convert.ToDecimal(csv.amount),
                                            payment_type_id = payment_option.id
                                        });
                                    InsertResult = $"{csv_row.InsertResult} Insert {csv.payment_item_label} Successful.";
                                }
                                else
                                {
                                    InsertResult = $"{csv_row.InsertResult} Inserting {csv.payment_item_label} cancelled.";
                                }
                            }
                            catch (Exception exc)
                            {
                                InsertResult = $"{csv_row.InsertResult} Failed:Unable to Insert {csv.payment_item_label}.";
                            }
                        }
                        else
                        {
                            InsertResult = $"{csv_row.InsertResult} Insert failed:Unable to Identify {csv.payment_item_label}.";
                        }
                    }
                    proceed_csv_process = true;
                    return true;
                }
                else
                {
                    proceed_csv_process = false;
                    InsertResult = $"{csv_row.InsertResult} Inserting Lab Request Payments cancelled (REQUEST ID is 0).";
                    return false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError($"HPayment > InsertWaterBacteriaPaymentsCsvToDatabase(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public void UpdateCustomerRequestPayment(OrderPageForm request_view_model)
        {
            try
            {               
                //remove all payments 
                _hlabPayment.DeleteBulkPayment(new hlab_test_payments {
                    order_id = request_view_model.hlab_order_log.order_id
                } );

                //verify if all payment items were removed
                var paymentcount = _hlabPayment.GetAllPayments(new orderpaymentsview {
                    order_id = request_view_model.hlab_order_log.order_id
                }).ToList().Count;

                if (paymentcount == 0)
                {
                    //then add payment items
                    foreach (var item in request_view_model.hlab_test_payments)
                    {
                        if (item.paid_amount != 0 && item.paid_amount != null)
                        {
                            item.order_id = request_view_model.hlab_order_log.order_id;
                            _hlabPayment.AddPayment(item);
                        }
                    }
                }

            }
            catch (Exception exc)
            {
                _logger.LogError($"HPayment > UpdateCustomerRequestPayment{exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<orderpaymentsview> ListRequestPayments(int request_id)
        {
            try
            {
                List<orderpaymentsview> payment_list = new List<orderpaymentsview>();
                if (request_id != 0) payment_list = _hlabPayment.GetAllPayments(new orderpaymentsview { order_id = request_id }).ToList();
                return payment_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HPayment > ListRequestPayments(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<hlab_test_payment_types> ListAllDbPaymentTypes()
        {
            try
            {
                return _hlabPayment.GetAllPaymentTypes().ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HPayment > ListAllDbPaymentTypes(): {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}
