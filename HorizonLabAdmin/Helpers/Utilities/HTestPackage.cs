using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
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
    public class HTestPackage : ITestPackage
    {
        private readonly IUtility _utility;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly Interface_test_package _hlabPkgCtgry;
        private readonly Interface_test_class _hlabTestClass;
        private readonly ILogger<HTestPackage> _logger;
        private readonly Interface_test_transactions _hlabTestTransRepo;
        private readonly Interface_test_results _hlabTestResult;
        public string InsertResult { get; set; }

        public HTestPackage(
            IHorizonLabSession sessionHelper, 
            IUtility utility, 
            Interface_test_package hlabPkgCtgry, 
            Interface_test_class hlabTestClass, 
            IUtility utilityHelper, ILogger<HTestPackage> logger,
            Interface_test_results hlabTestResult,
            Interface_test_transactions hlabTestTransRepo)
        {
            _sessionHelper = sessionHelper;
            _utility = utility;
            _hlabPkgCtgry = hlabPkgCtgry;
            _hlabTestClass = hlabTestClass;
            _logger = logger;
            _hlabTestTransRepo = hlabTestTransRepo;
            _hlabTestResult = hlabTestResult;
        }

        public hlab_test_pkgs GetWaterBacteriaTestPackage(int test_pkg_id)
        {
            try
            {
                return _hlabPkgCtgry.GetTestPackages(1).Where(x => x.id == test_pkg_id).FirstOrDefault();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetWaterBacteriaTestPackage(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        public List<SelectListItem> GeneratePackageCategorySelectListItem()
        {
            try
            {
                return _utility.GenerateSelectListItem(
                        _hlabPkgCtgry.GetPackageCategories().Where(x => x.status = true).ToList(),
                        "category_id",
                        "package_ctgry");
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GeneratePackageCategorySelectListItem(): {exc.InnerException}");
                throw exc.InnerException;
            }

        }

        public List<hlab_test_pkgs_class> GetNonHiddenTestClasses()
        {
            try{
                return _hlabTestClass.GetTestClasses().Where(x => x.class_id != 3).OrderBy(x => x.pkg_class).ToList(); //get all pkg class that is not hidden(id=3)
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetNonHiddenTestClasses(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_test_pkgs_category> GetActiveTestPackageCategories()
        {
            try
            {
                return _hlabPkgCtgry.GetPackageCategories().Where(x => x.status = true).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetActiveTestPackages(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<SelectListItem> SetWaterBacteriaTestClassItem(List<SelectListItem> selectClassList)
        {
            try
            {
                return _utility.SetSelectedItemFromList(selectClassList, "1");//select first item in the list
            }
            catch(Exception exc)
            {
                _logger.LogError($"HTestPackage > SetWaterBacteriaTestClassItem(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<SelectListItem> SetDebitPaymentItem(List<SelectListItem> selectPaymentTypeList)
        {
            try
            {
                return _utility.SetSelectedItemFromList(selectPaymentTypeList, "6");
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > SetDebitPaymentItem(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool InsertDefaultTestParameters(int test_package_id = 0, int transactionid = 0)
        {
            List<sp_getdefaultpackageparameters> default_params = new List<sp_getdefaultpackageparameters>();
            try
            {
                if (!_utility.IsNullableIntNotEmpty(test_package_id)) return true;

                default_params = _hlabTestTransRepo.GetDefaultParameters(test_package_id).ToList();
                if (default_params.Count > 0) //if test package has default parameters, insert test results
                {
                    foreach (var param in default_params)
                    {
                        _hlabTestResult.AddTestResults(new hlab_test_results
                        {
                            param_id = param.param_id,
                            result = "",
                            unit_id = param.unit_measurement_id,
                            is_failed = false,
                            trans_id = transactionid,
                            test_note = ""
                        });
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > InsertDefaultTestParameters(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public TestPackageObject GetLabPakcageObject(string lab_code)
        {            
            TestPackageObject test_package = new TestPackageObject();

            try
            {
                test_package.lab_package = _hlabPkgCtgry.GetTestPackages(0).Where(x => x.lab_code.ToLower() == lab_code.ToLower()).FirstOrDefault();
                test_package.lab_package_id = test_package.lab_package.id;
                test_package.lab_package_fee = test_package.lab_package.lab_fee;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetLabPakcage() : {exc.Message}");
                test_package.lab_package = null;
                test_package.lab_package_id = 0;
                test_package.lab_package_fee = 0;
            }
                
            return test_package;
        }

        public List<hlab_test_pkgs> GetAllTestPackageList()
        {
            try
            {
                int all_classes = 0;
                return _hlabPkgCtgry.GetTestPackages(all_classes).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetAllTestPackageList(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_test_pkgs> GetAllWaterBacteriaTestPackageList()
        {
            try
            {
                int water_bacteria = 3;
                return _hlabPkgCtgry.GetTestPackages(water_bacteria).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetAllWaterBacteriaTestPackageList(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool IsTestPackageSet(hlab_order_items request_item_obj)
        {
            if (request_item_obj.test_pkg_id != null && request_item_obj.test_pkg_id != 0) return true;
            return false;
        }

        public List<sp_getdefaultpackageparameters> GetDefaultTestParametersFromDb(int test_pkg_id)
        {
            try
            {
                return _hlabTestTransRepo.GetDefaultParameters(test_pkg_id).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetDefaultTestParametersFromDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_test_pkgs> GetWaterBacteriaTestPackageForSelectList()
        {
            try
            {
                List<hlab_test_pkgs> pkgCategoryList = new List<hlab_test_pkgs>();
                pkgCategoryList = GetAllWaterBacteriaTestPackageList();
                pkgCategoryList.Add(new hlab_test_pkgs { id = 0, lab_code = "" });
                pkgCategoryList = pkgCategoryList.OrderBy(x => x.id).ToList();
                return pkgCategoryList;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetWaterBacteriaTestPackageForSelectList(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_test_pkgs> GetTestPackageListByClass(int classid)
        {
            try
            {
                List<hlab_test_pkgs> pkgCategoryList = new List<hlab_test_pkgs>();
                pkgCategoryList = _hlabPkgCtgry.GetTestPackages(classid).ToList();
                return pkgCategoryList;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetTestPackageListByClass(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<sp_gettestpackagesbycategory> GetTestPackageListByCategory(int categoryid)
        {
            try
            {
                List<sp_gettestpackagesbycategory> pkgCategoryList = new List<sp_gettestpackagesbycategory>();
                pkgCategoryList = _hlabPkgCtgry.GetAllPackagesByCategory(categoryid).Where(x => x.status = true).ToList();
                return pkgCategoryList;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetTestPackageListByCategory(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public hlab_test_pkgs GetTestPackageInfo(int packageid)
        {
            try
            {
                hlab_test_pkgs testpackage = new hlab_test_pkgs();
                testpackage = _hlabPkgCtgry.GetTestPackageDetails(packageid);
                return testpackage;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetTestPackageInfo(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_test_pkgs_forms> GetAllTestPackageForms()
        {
            try
            {
                return _hlabPkgCtgry.GetTestPackageForms().ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestPackage > GetAllTestPackageForms(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}
