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
    public class HlabTableReferenceRepository : 
        Interface_rural_municipality, 
        Interface_hlab_cities,
        Interface_test_sample_types,
        Interface_test_parameter,
        Interface_test_report_types,
        Interface_test_class,
        Interface_test_description,
        Interface_unit_of_measurement,
        Interface_hlab_provinces,
        Interface_receivers
    {
        private HorizonLabTableReferenceApiLibrary _hllTableReference = new HorizonLabTableReferenceApiLibrary();
        private HorizonLabTestPackagesApiLibrary _hllTestPackageApi = new HorizonLabTestPackagesApiLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabTableReferenceRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public IEnumerable<hlab_rural_municipalities> GetRuralMunicipalities()
        {
            var json_data = _hllTableReference.GetAllRuralMunicipalities(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var municipalities = JsonConvert.DeserializeObject<List<hlab_rural_municipalities>>(json_data);
            return municipalities;
        }

        public IEnumerable<hlab_test_report_types> GetAllReportTypes()
        {
            var json_data = _hllTableReference.GetAllReportTypes(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var reporttypes = JsonConvert.DeserializeObject<List<hlab_test_report_types>>(json_data);
            return reporttypes;
        }

        public IEnumerable<hlab_test_sample_types> GetAllTestSampleTypes()
        {
            var json_data = _hllTableReference.GetAllSampleTypes(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var sampletypes = JsonConvert.DeserializeObject<List<hlab_test_sample_types>>(json_data);
            return sampletypes;
        }

        public IEnumerable<hlab_test_params> GetAllTestParameters()
        {
            var json_data = _hllTableReference.GetAllTestParameters(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var parameterlist = JsonConvert.DeserializeObject<List<hlab_test_params>>(json_data);
            return parameterlist;
        }

        public IEnumerable<hlab_test_measurement_units> GetAllUnitMeasurements()
        {
            var json_data = _hllTableReference.GetAllUnitMeasurements(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var unitmeasurementlist = JsonConvert.DeserializeObject<List<hlab_test_measurement_units>>(json_data);
            return unitmeasurementlist;
        }

        public IEnumerable<hlab_cities> GetAllCities(int provinceid)
        {
            var json_data = _hllTableReference.GetCities(provinceid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var cities = JsonConvert.DeserializeObject<List<hlab_cities>>(json_data);
            return cities;
        }

        public IEnumerable<hlab_provinces> GetAllProvinces()
        {
            var json_data = _hllTableReference.GetProvinces(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var provinces = JsonConvert.DeserializeObject<List<hlab_provinces>>(json_data);
            return provinces;
        }

        public IEnumerable<hlab_test_payment_options> GetAllPaymentOptions()
        {
            var json_data = _hllTableReference.GetTestPaymentOptions(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var options = JsonConvert.DeserializeObject<List<hlab_test_payment_options>>(json_data);
            return options;
        }

        public IEnumerable<hlab_test_payment_types> GetAllPaymentTypes()
        {
            var json_data = _hllTableReference.GetTestPaymentTypes(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var types = JsonConvert.DeserializeObject<List<hlab_test_payment_types>>(json_data);
            return types;
        }

        public IEnumerable<hlab_test_pkgs_class> GetTestClasses()
        {
            var json_data = _hllTableReference.GetPackageClasses(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var classlist = JsonConvert.DeserializeObject<List<hlab_test_pkgs_class>>(json_data);
            return classlist;
        }

        public IEnumerable<hlab_receivers> GetAllReceivers()
        {
            var json_data = _hllTableReference.GetAllReceivers(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var receiverlist = JsonConvert.DeserializeObject<List<hlab_receivers>>(json_data);
            return receiverlist;
        }

        public IEnumerable<hlab_test_transaction_types> GetTestTransactionTypes()
        {
            var json_data = _hllTableReference.GetAllTranTypes(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var trantypelist = JsonConvert.DeserializeObject<List<hlab_test_transaction_types>>(json_data);
            return trantypelist;
        }

        public IEnumerable<hlab_test_descriptions> GetAllTestDescriptions()
        {
            var json_data = _hllTableReference.GetAllTestDescriptions(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var descriptions = JsonConvert.DeserializeObject<List<hlab_test_descriptions>>(json_data);
            return descriptions;
        }

        public IEnumerable<sp_gettestpackagesbycategory> GetTestPackagesByCategory(int category_id)
        {
            var json_data = _hllTableReference.GetTestPackagesByCategory(category_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var packagelist = JsonConvert.DeserializeObject<List<sp_gettestpackagesbycategory>>(json_data);
            return packagelist;
        }

        public int AddNewCity(hlab_cities newcity)
        {
            var result = _hllTableReference.AddNewCity(newcity, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (!string.IsNullOrEmpty(result))
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
            return 0;
        }

        public int AddNewUnitofMeasurement(hlab_test_measurement_units unit)
        {
            var result = _hllTableReference.AddNewUnitofMeasurement(unit, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (!string.IsNullOrEmpty(result))
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
            return 0;
        }

        public int AddRuralMunicipality(hlab_rural_municipalities municipality)
        {
            var result = _hllTableReference.AddNewMunicapality(municipality, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (!string.IsNullOrEmpty(result))
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
            return 0;
        }
    }
}
