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
    public class HlabTestPackage : Interface_test_package
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private HorizonLabLibrary.HorizonLabTableReferenceApiLibrary _hllTableReferenceApi = new HorizonLabLibrary.HorizonLabTableReferenceApiLibrary();
        private HorizonLabLibrary.HorizonLabTestPackagesApiLibrary _hllTestPackageApi = new HorizonLabLibrary.HorizonLabTestPackagesApiLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabTestPackage(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public IEnumerable<hlab_test_pkgs> GetAllTestPackages()
        {
            try
            {
                var json_data = _hllTestPackageApi.GetAllTestPackages(_webApibaseUrl, _hlabApiKey, _ApiHeader);
                var packages = JsonConvert.DeserializeObject<List<hlab_test_pkgs>>(json_data);
                return packages;

            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public bool AddTestPackage(hlab_test_pkgs package)
        {
            try
            {
                _hllTestPackageApi.InsertTestPackage(package, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool UpdateTestPackage(hlab_test_pkgs package)
        {
            try
            {
                _hllTestPackageApi.UpdateTestPackage(package, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public IEnumerable<sp_gettestpackagesbycategory> GetAllPackagesByCategory(int categoryid)
        {
            try
            {
                var json_data = _hllTestPackageApi.GetAllTestPackagesByCategory(categoryid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                var projects = JsonConvert.DeserializeObject<List<sp_gettestpackagesbycategory>>(json_data);
                return projects;
            }
            catch (Exception exc)
            {               
                return null;
            }
        }

        public IEnumerable<hlab_test_pkgs_category> GetPackageCategories()
        {
            var jsonList = _hllTestPackageApi.GetAllTestPackagesCategories(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var pckgList = JsonConvert.DeserializeObject<List<hlab_test_pkgs_category>>(jsonList);
            return pckgList;
        }

        public bool AddTestPackageCategory(hlab_test_pkgs_category category)
        {
            try
            {
                _hllTestPackageApi.InsertTestPackageCategory(category, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool UpdateTestPackageCategory(hlab_test_pkgs_category category)
        {
            try
            {
                _hllTestPackageApi.UpdateTestPackageCategory(category, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool AddTestPackageCategorization(int pkgid, int ctgryid)
        {
            try
            {
                _hllTestPackageApi.AddTestPackageCategorization(
                    new hlab_test_default_parameter_category()
                    {
                        pkg_id = pkgid,
                        category_id = ctgryid
                    },
                    _webApibaseUrl,
                    _hlabApiKey,
                    _ApiHeader);
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool DeleteTestPackageCategorization(int pkgid, int ctgryid)
        {
            try
            {
                _hllTestPackageApi.DeleteTestPackageCategorization(
                    new hlab_test_default_parameter_category()
                    {
                        pkg_id = pkgid,
                        category_id = ctgryid
                    },
                    _webApibaseUrl,
                    _hlabApiKey,
                    _ApiHeader);
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public IEnumerable<hlab_test_pkgs> GetTestPackages(int classid)
        {
            try
            {
                var jsonList = _hllTestPackageApi.GetTestPackagesByClassId(classid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                var pckgList = JsonConvert.DeserializeObject<List<hlab_test_pkgs>>(jsonList);
                return pckgList;
            }
            catch(Exception exc)
            {
                return null;
            }
        }

        public hlab_test_pkgs GetTestPackageDetails(int pkgid)
        {
            try
            {
                var json_data = _hllTestPackageApi.GetTestPackagesDetails(pkgid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                var package = JsonConvert.DeserializeObject<hlab_test_pkgs>(json_data);
                return package;
            }
            catch(Exception exc)
            {
                return null;
            }
        }

        public IEnumerable<hlab_test_pkgs_forms> GetTestPackageForms()
        {
            try
            {
                var json_data = _hllTestPackageApi.GetAllTestPackagesForms(_webApibaseUrl, _hlabApiKey, _ApiHeader);
                var forms = JsonConvert.DeserializeObject<List<hlab_test_pkgs_forms>>(json_data);
                return forms;
            }
            catch (Exception exc)
            {
                return null;
            }
        }
    }
}
