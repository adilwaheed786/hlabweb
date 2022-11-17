using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_ref_tables")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabRefTablesController : ControllerBase
    {
        private Interface_test_package _hlabTestPkgCtgryRepo;
        private Interface_test_sample_types _hlabTestSampleTypes;
        private Interface_receivers _hlabReceivers;
        private Interface_hlab_cities _hlabCities;
        private Interface_rural_municipality _Municipality;
        private Interface_test_projects _TestPorjects;
        private Interface_test_parameter _TestParameter;
        private Interface_test_report_types _ReporTypes;
        private Interface_unit_of_measurement _UnitOfMeasurement;
        private Interface_test_transactions _testTransaction;
        private Interface_hlab_provinces _province;
        private Interface_hlab_test_payments _payments;
        private Interface_test_description _testDescription;
        private Interface_test_class _testClasses;
        private readonly ILogger<HlabTestTransactionsController> _logger;

        public HlabRefTablesController(
            Interface_test_package hlabTestPkgCtgryRepo, 
            Interface_receivers hlabReceivers,
            Interface_hlab_cities hlabCities,
            Interface_test_projects TestPorjects,
            Interface_rural_municipality Municipality,
            Interface_test_sample_types hlabTestSampleTypes,
            Interface_test_parameter TestParameter,
            Interface_test_report_types ReporTypes,
            Interface_unit_of_measurement UnitOfMeasurement,
            Interface_hlab_provinces province,
            Interface_hlab_test_payments payments,
            Interface_test_transactions testTransaction,
            Interface_test_class testClasses,
            Interface_test_description testDescription,
            ILogger<HlabTestTransactionsController> logger)
        {
            _hlabTestPkgCtgryRepo = hlabTestPkgCtgryRepo;
            _hlabReceivers = hlabReceivers;
            _hlabCities = hlabCities;
            _Municipality = Municipality;
            _TestPorjects = TestPorjects;
            _hlabTestSampleTypes = hlabTestSampleTypes;
            _ReporTypes = ReporTypes;
            _TestParameter = TestParameter;
            _province = province;
            _payments = payments;
            _testDescription = testDescription;
            _testTransaction = testTransaction;
            _UnitOfMeasurement = UnitOfMeasurement;
            _testClasses = testClasses;
            _logger = logger;
        }



        [HttpGet("getallruralmunicipalities")]
        public List<hlab_rural_municipalities> getallruralmunicipalities()
        {
            try
            {
                List<hlab_rural_municipalities> rurals = _Municipality.GetRuralMunicipalities().ToList();
                return rurals;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }        

        [HttpGet("getallreceivers")]
        public List<hlab_receivers> GetAllReceivers()
        {
            try
            {
                List<hlab_receivers> receiver_list = _hlabReceivers.GetAllReceivers().ToList();
                return receiver_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getalltestprojects")]
        public List<hlab_test_projects> getalltestprojects()
        {
            try
            {
                List<hlab_test_projects> projectlist = _TestPorjects.GetAllTestProjects().ToList();
                return projectlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getalltestsampletypes")]
        public List<hlab_test_sample_types> getalltestsampletypes()
        {
            try
            {
                List<hlab_test_sample_types> sampletypelist = _hlabTestSampleTypes.GetAllTestSampleTypes().ToList();
                return sampletypelist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getalltestparameters")]
        public List<hlab_test_params> getalltestparameters()
        {
            try
            {
                List<hlab_test_params> paramlist = _TestParameter.GetAllTestParameters().ToList();
                return paramlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getallreporttypes")]
        public List<hlab_test_report_types> getallreporttypes()
        {
            try
            {
                List<hlab_test_report_types> reporttypes = _ReporTypes.GetAllReportTypes().ToList();
                return reporttypes;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getallunitmeasurements")]
        public List<hlab_test_measurement_units> getallunitmeasurements()
        {
            try
            {
                List<hlab_test_measurement_units> unitmeasurements = _UnitOfMeasurement.GetAllUnitMeasurements().ToList();
                return unitmeasurements;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getcities")]
        public List<hlab_cities> getcities(int provinceid = 3)
        {
            try
            {
                List<hlab_cities> cities = _hlabCities.GetAllCities(Convert.ToInt32(provinceid)).ToList();
                return cities;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getprovinces")]
        public List<hlab_provinces> getprovinces()
        {
            try
            {
                List<hlab_provinces> provinces = _province.GetAllProvinces().ToList();
                return provinces;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getpaymentoptionlist")]
        public List<hlab_test_payment_options> getpaymentoptionlist()
        {
            try
            {
                List<hlab_test_payment_options> options = _payments.GetAllPaymentOptions().ToList();
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
                List<hlab_test_payment_types> types = _payments.GetAllPaymentTypes().ToList();
                return types;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("gettestpkgclasses")]
        public List<hlab_test_pkgs_class> gettestpkgclasses()
        {
            try
            {
                List<hlab_test_pkgs_class> classlist = _testClasses.GetTestClasses().ToList();
                return classlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("gettesttranstypes")]
        public List<hlab_test_transaction_types> gettesttranstypes()
        {
            try
            {
                List<hlab_test_transaction_types> trantypes = _testTransaction.GetTestTransactionTypes().ToList();
                return trantypes;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("gettestdescriptions")]
        public List<hlab_test_descriptions> gettestdescriptions()
        {
            try
            {
                List<hlab_test_descriptions> descriptions = _testDescription.GetAllTestDescriptions().ToList();
                return descriptions;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("addnewcity")]
        public int? addnewcity(hlab_cities newcity)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                return _hlabCities.AddNewCity(newcity);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return 0;
            }
        }

        [HttpPost("addnewmuniciaplity")]
        public int? addnewmuniciaplity(hlab_rural_municipalities newmuniciaplity)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                return _Municipality.AddRuralMunicipality(newmuniciaplity);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return 0;
            }
        }

        [HttpPost("addnewunitofmeasurement")]
        public int? addnewunitofmeasurement(hlab_test_measurement_units unit)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                return _UnitOfMeasurement.AddNewUnitofMeasurement(unit);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return 0;
            }
        }
    }
}