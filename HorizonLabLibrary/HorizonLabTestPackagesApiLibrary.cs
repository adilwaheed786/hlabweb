using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabTestPackagesApiLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_test_package_controller";

        public string InsertTestPackage(hlab_test_pkgs package, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(package);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addtestpackage/", ApiKey, ApiHeader);
        }

        public string UpdateTestPackage(hlab_test_pkgs package, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(package);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatetestpackage/", ApiKey, ApiHeader);
        }

        public string InsertTestPackageCategory(hlab_test_pkgs_category category, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(category);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addtestpackagecategory/", ApiKey, ApiHeader);
        }

        public string UpdateTestPackageCategory(hlab_test_pkgs_category category, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(category);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatetestpackagecategory/", ApiKey, ApiHeader);
        }

        public string AddTestPackageCategorization(hlab_test_default_parameter_category input, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(input);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addtestpackagecategorization/", ApiKey, ApiHeader);
        }

        public string DeleteTestPackageCategorization(hlab_test_default_parameter_category input, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(input);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/deletetestpackagecategorization/", ApiKey, ApiHeader);
        }

        public string GetAllTestPackagesByCategory(int categoryid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettestpackagesbycategory?categoryid=" + categoryid, ApiKey, ApiHeader);
        }

        public string GetAllTestPackagesForms(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getalltestpackageforms", ApiKey, ApiHeader);
        }

        public string GetAllTestPackagesCategories(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettestpackagecategories", ApiKey, ApiHeader);
        }

        public string GetAllTestPackages(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getalltestpackages", ApiKey, ApiHeader);
        }

        public string GetTestPackagesByClassId(int classid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettestpackages?classid=" + classid, ApiKey, ApiHeader);
        }

        public string GetTestPackagesDetails(int packageid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettestpackagedetails?pkgid=" + packageid, ApiKey, ApiHeader);
        }
    }
}
