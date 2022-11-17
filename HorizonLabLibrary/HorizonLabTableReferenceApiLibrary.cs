using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabTableReferenceApiLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_service_controller_name = "/hlab_ref_tables";

        public string GetAllRuralMunicipalities(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getallruralmunicipalities", ApiKey, ApiHeader);
        }
        
        public string GetAllTestProjects(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getalltestprojects", ApiKey, ApiHeader);
        }

        public string GetAllSampleTypes(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getalltestsampletypes", ApiKey, ApiHeader);
        }

        public string GetAllReportTypes(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getallreporttypes", ApiKey, ApiHeader);
        }

        public string GetAllTestParameters(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getalltestparameters", ApiKey, ApiHeader);
        }

        public string GetAllUnitMeasurements(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getallunitmeasurements", ApiKey, ApiHeader);
        }

        public string GetCities(int provinceid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getcities?provinceid=" + provinceid, ApiKey, ApiHeader);
        }

        public string GetProvinces(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getprovinces", ApiKey, ApiHeader);
        }

        public string GetTestPaymentOptions(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getpaymentoptionlist", ApiKey, ApiHeader);
        }

        public string GetTestPaymentTypes(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getpaymenttypelist", ApiKey, ApiHeader);
        }

        public string GetPackageClasses(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/gettestpkgclasses", ApiKey, ApiHeader);
        }

        public string GetAllReceivers(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getallreceivers", ApiKey, ApiHeader);
        }

        public string GetAllTranTypes (string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/gettesttranstypes", ApiKey, ApiHeader);
        }

        public string GetAllTestDescriptions(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/gettestdescriptions", ApiKey, ApiHeader);
        }

        public string GetTestPackagesByCategory(int categoryid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/gettestpackagesbycategory?categoryid=" + categoryid, ApiKey, ApiHeader);
        }

        public string AddNewCity(hlab_cities new_city, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(new_city);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_service_controller_name + "/addnewcity/", ApiKey, ApiHeader);
        }

        public string AddNewMunicapality(hlab_rural_municipalities new_municipality, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(new_municipality);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_service_controller_name + "/addnewmuniciaplity/", ApiKey, ApiHeader);
        }

        public string AddNewUnitofMeasurement(hlab_test_measurement_units unit, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(unit);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_service_controller_name + "/addnewunitofmeasurement/", ApiKey, ApiHeader);
        }     
    }
}
