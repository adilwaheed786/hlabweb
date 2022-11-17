using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestPaymentRepository : Interface_hlab_test_payments
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestPaymentRepository> _logger;

        public HlabTestPaymentRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabTestPaymentRepository> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public bool AddPayment(hlab_test_payments payment)
        {
            try
            {
                if (payment != null)
                {
                    _hlab_Db_Context.hlab_test_payments.Add(payment);
                    _hlab_Db_Context.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError("MODEL HlabTestPaymentRepository > Add: payment class object is null.");
                    return false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        public bool DeleteBulkPayment(hlab_test_payments payment)
        {
            try
            {
                _hlab_Db_Context.hlab_test_payments.RemoveRange(_hlab_Db_Context.hlab_test_payments.Where(x => x.order_id == payment.order_id));
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        public bool DeleteOnePayment(hlab_test_payments payment)
        {
            try
            {
                _hlab_Db_Context.hlab_test_payments.RemoveRange(_hlab_Db_Context.hlab_test_payments.Where(x => x.payment_id == payment.payment_id));
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        public IEnumerable<hlab_test_payment_options> GetAllPaymentOptions()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_payment_options.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        public IEnumerable<orderpaymentsview> GetAllPayments(orderpaymentsview orderpayment)
        {
            try
            {
                if (orderpayment.order_id == 0) return _hlab_Db_Context.orderpaymentsview.ToList();
                return _hlab_Db_Context.orderpaymentsview.Where(x => x.order_id == orderpayment.order_id).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        public IEnumerable<hlab_test_payment_types> GetAllPaymentTypes()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_payment_types.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        public orderpaymentsview GetPaymentInfo(orderpaymentsview orderpayment)
        {
            try
            {
                return _hlab_Db_Context.orderpaymentsview.Where(x => x.payment_id == orderpayment.payment_id).FirstOrDefault();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        public bool UpdatePayment(hlab_test_payments payment)
        {
            try
            {
                _hlab_Db_Context.hlab_test_payments.Update(payment);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }
    }
}
