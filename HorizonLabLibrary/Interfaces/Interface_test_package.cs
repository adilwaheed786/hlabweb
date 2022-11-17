using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_test_package
    {
        IEnumerable<hlab_test_pkgs> GetAllTestPackages();

        IEnumerable<sp_gettestpackagesbycategory> GetAllPackagesByCategory(int categoryid);
        IEnumerable<hlab_test_pkgs> GetTestPackages(int classid);
        hlab_test_pkgs GetTestPackageDetails(int pkgid);
        bool AddTestPackage(hlab_test_pkgs package);
        bool UpdateTestPackage(hlab_test_pkgs package);

        IEnumerable<hlab_test_pkgs_category> GetPackageCategories();
        IEnumerable<hlab_test_pkgs_forms> GetTestPackageForms();
        bool AddTestPackageCategory(hlab_test_pkgs_category category);
        bool UpdateTestPackageCategory(hlab_test_pkgs_category category);
        bool AddTestPackageCategorization(int pkgid, int ctgryid);
        bool DeleteTestPackageCategorization(int pkgid, int ctgryid);
    }
}
