using HorizonLabAdmin.Helpers.Containers;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ITestPackage
    {
        string InsertResult { get; set; }
        
        List<hlab_test_pkgs> GetAllTestPackageList();
        List<hlab_test_pkgs> GetAllWaterBacteriaTestPackageList();
        List<hlab_test_pkgs> GetWaterBacteriaTestPackageForSelectList();
        List<hlab_test_pkgs> GetTestPackageListByClass(int classid);
        List<sp_gettestpackagesbycategory> GetTestPackageListByCategory(int categoryid);
        List<SelectListItem> GeneratePackageCategorySelectListItem();
        List<SelectListItem> SetWaterBacteriaTestClassItem(List<SelectListItem> selectClassList);
        List<SelectListItem> SetDebitPaymentItem(List<SelectListItem> selectPaymentTypeList);

        hlab_test_pkgs GetWaterBacteriaTestPackage(int test_pkg_id);
        List<hlab_test_pkgs_class> GetNonHiddenTestClasses();
        List<hlab_test_pkgs_category> GetActiveTestPackageCategories();
        List<hlab_test_pkgs_forms> GetAllTestPackageForms();
        List<sp_getdefaultpackageparameters> GetDefaultTestParametersFromDb(int test_pkg_id);
        TestPackageObject GetLabPakcageObject(string lab_code);
        hlab_test_pkgs GetTestPackageInfo(int packageid);

        bool InsertDefaultTestParameters(int test_package_id = 0, int transactionid = 0);
        bool IsTestPackageSet(hlab_order_items request_item_obj);        
    }
}
