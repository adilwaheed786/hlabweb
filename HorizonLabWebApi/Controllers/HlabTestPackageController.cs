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
    [Route("hlab_test_package_controller")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabTestPackageController : ControllerBase
    {
        private Interface_test_package _hlabTestPackages;
        private readonly ILogger<HlabTestPackageController> _logger;

        public HlabTestPackageController(
            Interface_test_package hlabTestPackages,
            ILogger<HlabTestPackageController> logger)
        {
            _hlabTestPackages = hlabTestPackages;
            _logger = logger;
        }

        [HttpGet("gettestpackagecategories")]
        public List<hlab_test_pkgs_category> gettestpackagecategories()
        {
            try
            {
                return _hlabTestPackages.GetPackageCategories().ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getalltestpackageforms")]
        public List<hlab_test_pkgs_forms> getalltestpackageforms()
        {
            try
            {
                return _hlabTestPackages.GetTestPackageForms().ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getalltestpackages")]
        public List<hlab_test_pkgs> getalltestpackages()
        {
            try
            {
                return _hlabTestPackages.GetAllTestPackages().ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("gettestpackages")]
        public List<hlab_test_pkgs> GetTestpackages(int classid)
        {
            try
            {
                List<hlab_test_pkgs> test_package_list = _hlabTestPackages.GetTestPackages(classid).ToList();
                return test_package_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("gettestpackagedetails")]
        public hlab_test_pkgs GetTestpackageDetails(int pkgid)
        {
            try
            {
                hlab_test_pkgs test_package = _hlabTestPackages.GetTestPackageDetails(pkgid);
                return test_package;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("addtestpackage")]
        public ActionResult AddTestPackage(hlab_test_pkgs package)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("AddTestPackage : Not a valid model");
                bool result = _hlabTestPackages.AddTestPackage(package);
                if (result) return Ok();
                return BadRequest("HlabTestPackageController > AddTestPackage Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("HlabTestPackageController >  AddTestPackage Exception Error: " + xc);
            }
        }

        [HttpPost("updatetestpackage")]
        public ActionResult UpdateTestpackage(hlab_test_pkgs package)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("UpdateTestpackage : Not a valid model");
                bool result = _hlabTestPackages.UpdateTestPackage(package);
                if (result) return Ok();
                return BadRequest("HlabTestPackageController > UpdateTestpackage Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("HlabTestPackageController > UpdateTestpackage Exception Error: " + xc);
            }
        }

        [HttpPost("addtestpackagecategory")]
        public ActionResult AddTestpackageCategory(hlab_test_pkgs_category category)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("AddTestpackageCategory : Not a valid model");
                bool result = _hlabTestPackages.AddTestPackageCategory(category);
                if (result) return Ok();
                return BadRequest("HlabTestPackageController > AddTestpackageCategory Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("HlabTestPackageController > AddTestpackageCategory Exception Error: " + xc);
            }
        }

        [HttpPost("updatetestpackagecategory")]
        public ActionResult UpdateTestpackageCategory(hlab_test_pkgs_category category)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("UpdateTestpackageCategory : Not a valid model");
                bool result = _hlabTestPackages.UpdateTestPackageCategory(category);
                if (result) return Ok();
                return BadRequest("HlabTestPackageController > UpdateTestpackageCategory Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("HlabTestPackageController > UpdateTestpackageCategory Exception Error: " + xc);
            }
        }

        [HttpGet("gettestpackagesbycategory")]
        public List<sp_gettestpackagesbycategory> GetTestpackagesByCategory(int categoryid)
        {
            try
            {
                List<sp_gettestpackagesbycategory> test_package_list = _hlabTestPackages.GetAllPackagesByCategory(categoryid).ToList();
                return test_package_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("addtestpackagecategorization")]
        public ActionResult AddTestPackageCategorization(hlab_test_default_parameter_category input)
        {
            try
            {
                if (input.pkg_id == 0 && input.category_id == 0) return BadRequest("AddTestPackageCategorization : Package Id or Category Id is 0");
                bool result = _hlabTestPackages.AddTestPackageCategorization(input.pkg_id, input.category_id);
                if (result) return Ok();
                return BadRequest("HlabTestPackageController > AddTestPackageCategorization Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("HlabTestPackageController > AddTestPackageCategorization Exception Error: " + xc);
            }
        }

        [HttpPost("deletetestpackagecategorization")]
        public ActionResult DeleteTestPackageCategorization(hlab_test_default_parameter_category input)
        {
            try
            {
                if (input.pkg_id == 0 && input.category_id == 0) return BadRequest("DeleteTestPackageCategorization : Package Id or Category Id is 0");
                bool result = _hlabTestPackages.DeleteTestPackageCategorization(input.pkg_id, input.category_id);
                if (result) return Ok();
                return BadRequest("HlabTestPackageController > DeleteTestPackageCategorization Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("HlabTestPackageController > DeleteTestPackageCategorization Exception Error: " + xc);
            }
        }
    }
}