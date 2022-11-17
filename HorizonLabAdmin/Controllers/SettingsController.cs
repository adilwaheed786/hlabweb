using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    public class SettingsController : Controller
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private hlab_email_file_attachments _efa = new hlab_email_file_attachments();
        private Interface_hlab_test_default_pkg_params _hllDefaultParams;
        private Interface_test_package _hllTestPackage;
        private Interface_test_transactions _hlltestTrans;
        private Interface_EmailSender _Email;
        private Interface_test_parameter _hlabTestParameters;
        private Interface_test_class _hlabTestClass;
        private readonly ILogger<OrderController> _logger;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;
        private readonly IUtility _utilityHelper;

        public SettingsController(
            IConfiguration appConfig,
            ILogger<OrderController> logger,
            IHostingEnvironment env,            
            Interface_test_package hllTestPackage,
            Interface_hlab_test_default_pkg_params hllDefaultParams,
            Interface_test_parameter hlabTestParameters,
            Interface_test_class hlabTestClass,
            Interface_test_transactions hlltestTrans,
            Interface_EmailSender Email,
            IUtility utilityHelper
            )
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;
            _Email = Email;
            _hllTestPackage = hllTestPackage;
            _hllDefaultParams = hllDefaultParams;
            _hlltestTrans = hlltestTrans;
            _hlabTestClass = hlabTestClass;
            _hlabTestParameters = hlabTestParameters;
            _utilityHelper = utilityHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCategory(string str_search)
        {
            return View();
        }

        public IActionResult TestPackageCategories()
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "settings");
            string Message = "";
            TestPackage testpackage_model = new TestPackage();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                testpackage_model.test_categories = _hllTestPackage.GetPackageCategories().ToList();
            }
            catch (Exception exc)
            {
                testpackage_model.test_categories = null;
                _logger.LogError(exc.Message);
            }
            if (TempData["TestPackageCategoriesMessage"] != null) Message = TempData["TestPackageCategoriesMessage"].ToString();
            TempData["TestPackageCategoriesMessage"] = null;
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            return View(testpackage_model);
        }

        public IActionResult EmailLogs(string sortdate="desc")
        {
            List<emaillogview> email_logs = new List<emaillogview>();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "settings");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if (sortdate == "desc")
                {
                    email_logs = _Email.GetAllEmailLogs().OrderByDescending(x => x.date_sent).ToList();
                }
                else
                {
                    email_logs = _Email.GetAllEmailLogs().OrderBy(x => x.date_sent).ToList();
                }
                
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            ViewBag.menu = _hlabMenu;
            ViewBag.email_logs = email_logs;
            ViewBag.sortdate = sortdate;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");

            return View();
        }

        public IActionResult TestPackageSettings()
        {
            string Message = "";
            TestPackage testpackage_model = new TestPackage();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "settings");
            List<hlab_test_pkgs_class> pkg_class_list = new List<hlab_test_pkgs_class>();
            List<SelectListItem> selectClassList = new SelectList(pkg_class_list, "class_id", "pkg_class").ToList();
            List<hlab_test_pkgs_forms> test_pkg_form_list = new List<hlab_test_pkgs_forms>();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {                
                testpackage_model.test_packages = _hllTestPackage.GetTestPackages(0).ToList();

                test_pkg_form_list = _hllTestPackage.GetTestPackageForms().ToList();
                testpackage_model.selectTestPackageForm = _utilityHelper.GenerateSelectListItem(test_pkg_form_list,"id","form_name");
                testpackage_model.selectTestPackageForm.Add(new SelectListItem { Text="", Value="0"});

                pkg_class_list = _hlabTestClass.GetTestClasses().ToList();
                selectClassList = new SelectList(pkg_class_list, "class_id", "pkg_class").ToList();
            }
            catch (Exception exc)
            {
                testpackage_model.test_categories = null;
                _logger.LogError(exc.Message);
            }

            if (TempData["TestPackagesMessage"] != null) Message = TempData["TestPackagesMessage"].ToString();
            TempData["TestPackagesMessage"] = null;
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;
            ViewBag.selectClassList = selectClassList;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            return View(testpackage_model);
        }

        public IActionResult TestPackageDefaultParameters(int packageid=0)
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "settings");
            List<sp_getdefaultpackageparameters> defaults = new List<sp_getdefaultpackageparameters>();
            List<hlab_test_params> all_parameters = new List<hlab_test_params>();
            string Message = "";
            string package_name = "";
            List<SelectListItem> selectAllParameters = new List<SelectListItem>();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if(packageid!=0) defaults = _hlltestTrans.GetDefaultParameters(packageid).ToList();
                all_parameters = _hlabTestParameters.GetAllTestParameters().ToList();
                selectAllParameters = new SelectList(all_parameters, "param_id", "param_name").ToList();
                //package_name = _hllTestPackage.GetAllTestPackages().Where(x => x.id == packageid).FirstOrDefault().lab_code;
                package_name = _hllTestPackage.GetTestPackageDetails(packageid).lab_code;
            }
            catch (Exception exc)
            {
                defaults = null;
                _logger.LogError(exc.Message);
            }

            if (TempData["TestPackagesMessage"] != null) Message = TempData["TestPackagesMessage"].ToString();
            TempData["TestPackagesMessage"] = null;
            ViewBag.Message = Message;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["PageTitle"] = "Default Parameters";
            ViewBag.menu = _hlabMenu;
            ViewBag.defaults = defaults;
            ViewBag.all_parameters = selectAllParameters;
            ViewBag.package_id = packageid;
            ViewBag.package_name = package_name;
            return View();
        }

        public IActionResult EmailTemplateSettings(int templateid=0)
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "settings");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page      
            EmailTemplate emailtemplate = new EmailTemplate();
            emailtemplate.hlab_email_template_list = _Email.GetAllEmailTemplates().ToList();
            emailtemplate.hlab_email_template = new hlab_email_templates();
            emailtemplate.new_hlab_email_template = new hlab_email_templates();
            emailtemplate.file_attachment_list = new List<hlab_email_file_attachments>();
            if (templateid != 0)
            {
                _efa.email_template_id = templateid;
                emailtemplate.hlab_email_template = emailtemplate.hlab_email_template_list.Where(x => x.id == templateid).FirstOrDefault();
                emailtemplate.file_attachment_list = _Email.GetEmailFileAttachments(_efa).ToList();
            }

            ViewBag.menu = _hlabMenu;
            ViewBag.templateid = templateid;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            return View(emailtemplate);
        }

        [HttpPost]
        public IActionResult AddDefaultParameter(List<int> SelectedParameters, int package_id=0)
        {
            if(SelectedParameters.Count > 0 && package_id != 0)
            {
                foreach (var param in SelectedParameters)
                {
                    _hllDefaultParams.AssignDefaultParam(new hlab_test_default_pkg_params { param_id = param, pkg_id = package_id });
                }
            }
            return RedirectToAction("TestPackageDefaultParameters", "Settings", new { packageid = package_id }); 
        }

        public IActionResult DeleteDefaultParameter(int defaultparamid = 0, int packageid=0)
        {
            if (packageid != 0)
            {
                _hllDefaultParams.DeleteDefaultParam(new hlab_test_default_pkg_params {  id = defaultparamid });
            }
            return RedirectToAction("TestPackageDefaultParameters", "Settings", new { packageid = packageid });
        }

        public IActionResult TestPackageCategorization(int categoryid = 1, int classid=1)
        {
            string Message = "", selectedCategoryName="";
            int selectedCategoryId = categoryid;
            TestPackage testpackage_model = new TestPackage();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "settings");
            testpackage_model.test_packages = new List<hlab_test_pkgs>();
            testpackage_model.packagelist_bycategory = new List<sp_gettestpackagesbycategory>();
            List<hlab_test_pkgs_category> pkg_category_list = new List<hlab_test_pkgs_category>();
            List<hlab_test_pkgs_class> pkg_class_list = new List<hlab_test_pkgs_class>();
            List<SelectListItem> selectCategoryList = new SelectList(pkg_category_list, "category_id", "package_ctgry").ToList();
            List<SelectListItem> selectPackageList = new SelectList(testpackage_model.test_packages, "id", "lab_code").ToList();
            List<SelectListItem> selectPackageListByCategory = new SelectList(testpackage_model.packagelist_bycategory, "id", "lab_code").ToList();
            List<SelectListItem> selectPackageClass = new SelectList(pkg_class_list, "class_id", "pkg_class").ToList();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                //testpackage_model.test_packages = _hllTableReference.GetTestPackages(classid).Where(x => x.pkg_class_id!=2 && x.pkg_class_id!=3).ToList();
                testpackage_model.test_packages = _hllTestPackage.GetTestPackages(classid).ToList();
                selectPackageList = new SelectList(testpackage_model.test_packages, "id", "lab_code").ToList();
                pkg_category_list = _hllTestPackage.GetPackageCategories().ToList();
                //pkg_class_list = _hllTableReference.GetTestClasses().Where(x => x.class_id != 2 && x.class_id != 3).ToList();
                pkg_class_list = _hlabTestClass.GetTestClasses().ToList();

                selectCategoryList = new SelectList(pkg_category_list, "category_id", "package_ctgry").ToList();
                selectCategoryList.Where(x => x.Value == categoryid.ToString()).FirstOrDefault().Selected = true;
                selectPackageClass = new SelectList(pkg_class_list, "class_id", "pkg_class").ToList();
                selectPackageClass.Where(x => x.Value == classid.ToString()).First().Selected = true;

                if (categoryid != 0)
                {
                    testpackage_model.packagelist_bycategory = _hllTestPackage.GetAllPackagesByCategory(categoryid).ToList();
                    selectPackageListByCategory = new SelectList(testpackage_model.packagelist_bycategory, "id", "lab_code").ToList();
                    selectedCategoryName = pkg_category_list.Where(x => x.category_id == categoryid).FirstOrDefault().package_ctgry;
                }                                                
            }
            catch (Exception exc)
            {
                testpackage_model.test_categories = null;
                _logger.LogError(exc.Message);
            }

            if (TempData["CategorizationMessage"] != null) Message = TempData["CategorizationMessage"].ToString();
            TempData["CategorizationMessage"] = null;
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;
            ViewBag.selectCategoryList = selectCategoryList;
            ViewBag.selectPackageClass = selectPackageClass;
            ViewBag.selectedCategoryId = selectedCategoryId;
            ViewBag.selectedCategoryName = selectedCategoryName;
            ViewBag.selectPackageList = selectPackageList;
            ViewBag.selectPackageListByCategory = selectPackageListByCategory;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            return View(testpackage_model);
        }

        [HttpPost]
        public IActionResult UpdateTestPackageCategory(TestPackage pagemodel)
        {
            string Message = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if(pagemodel.test_categories.Count > 0)
                {
                    foreach (var category in pagemodel.test_categories)
                    {
                        _hllTestPackage.UpdateTestPackageCategory(category);
                    }
                    Message = "success: Horizon Lab Test package categories successfully updated";
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                Message = "error: Horizon Lab Test package categories update failed, please contact administrator.";
            }
            TempData["TestPackageCategoriesMessage"] = Message;
            return RedirectToAction("TestPackageCategories", "Settings");
        }

        [HttpPost]
        public IActionResult UpdateTestPackage(TestPackage pagemodel)
        {
            string Message = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if (pagemodel.test_packages.Count > 0)
                {
                    foreach (var package in pagemodel.test_packages)
                    {
                        _hllTestPackage.UpdateTestPackage(package);
                    }
                    Message = "success: Horizon Lab Test packages successfully updated";
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                Message = "error: Horizon Lab Test packages update failed, please contact administrator.";
            }
            TempData["TestPackagesMessage"] = Message;
            return RedirectToAction("TestPackageSettings", "Settings");
        }

        [HttpPost]
        public IActionResult AddTestPackageCategory(TestPackage pagemodel)
        {
            string Message = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if (!string.IsNullOrEmpty(pagemodel.test_category.package_ctgry))
                {
                    _hllTestPackage.AddTestPackageCategory(pagemodel.test_category);
                    Message = "success: New Horizon Lab Test package category was successfully added";
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                Message = "error: Adding new Horizon Lab Test package category failed, please contact administrator.";
            }
            TempData["TestPackageCategoriesMessage"] = Message;
            return RedirectToAction("TestPackageCategories", "Settings");
        }

        [HttpPost]
        public IActionResult AddTestPackage(TestPackage pagemodel)
        {
            string Message = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if (!string.IsNullOrEmpty(pagemodel.test_package.lab_code))
                {
                    _hllTestPackage.AddTestPackage(pagemodel.test_package);
                    Message = "success: New Horizon Lab Test package was successfully added";
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                Message = "error: Adding new Horizon Lab Test package failed, please contact administrator.";
            }
            TempData["TestPackagesMessage"] = Message;
            return RedirectToAction("TestPackageSettings", "Settings");
        }

        [HttpPost]
        public IActionResult AddTestPackageCategorization(int add_package_id, int add_category_id)
        {
            string Message = "";
            List<sp_gettestpackagesbycategory> packagelist = new List<sp_gettestpackagesbycategory>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if (add_package_id!=0 && add_category_id!=0)
                {
                    packagelist = _hllTestPackage.GetAllPackagesByCategory(add_category_id).ToList();
                    packagelist = packagelist.Where(x => x.id == add_package_id).ToList();
                    Message = "error: Adding a test package to a category failed, It is already existing on the selected category.";
                    if (packagelist.Count == 0)
                    {
                        _hllTestPackage.AddTestPackageCategorization(add_package_id, add_category_id);
                        Message = "success: New Horizon Lab Test package was successfully categorized";
                    }                    
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                Message = "error: Adding a test package to a category failed, please contact administrator.";
            }
            TempData["CategorizationMessage"] = Message;
            return RedirectToAction("TestPackageCategorization", "Settings");
        }

        [HttpPost]
        public IActionResult DeleteTestPackageCategorization(int delete_package_id, int delete_category_id)
        {
            string Message = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if (delete_package_id != 0 && delete_category_id != 0)
                {
                    _hllTestPackage.DeleteTestPackageCategorization(delete_package_id, delete_category_id);
                    Message = "success: Horizon Lab Test package was successfully deleted from a category";
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                Message = "error: Deleting a test package from a category failed, please contact administrator.";
            }
            TempData["CategorizationMessage"] = Message;
            return RedirectToAction("TestPackageCategorization", "Settings");
        }

        [HttpPost]
        public IActionResult AddNewEmailTemplate(EmailTemplate pagemodel, IFormFile[] attachments)
        {
            string Message = "", filename="";
            int templateid = 0;
            string savepath = _env.WebRootPath + "\\email_file_attachments";
            hlab_email_file_attachments file_attachment = new hlab_email_file_attachments();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if (string.IsNullOrEmpty(pagemodel.new_hlab_email_template.template_name)) pagemodel.new_hlab_email_template.template_name = "Not Assigned";
                templateid = _Email.InsertNewEmailTemplate(pagemodel.new_hlab_email_template);
                if(templateid==0) Message = "error: Creating new email template failed, please contact administrator.";
                if (attachments.ToList().Count > 0 && templateid > 0)
                {
                    //_Email.DeleteAllFileAttachments(templateid);
                    foreach(var file in attachments)
                    {
                        savepath = _env.WebRootPath + "\\email_file_attachments";
                        filename = Path.GetFileName(file.FileName).ToString();
                        file_attachment = new hlab_email_file_attachments {
                            email_template_id = templateid,
                            file_name = filename                        
                        };
                        savepath = savepath + "\\" + filename;
                        using (var stream = new FileStream(savepath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        _Email.InsertFileAttachment(file_attachment);
                    }                    
                    Message = "success: New Email Templated was created.";
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                Message = "error: Creating new email template failed, please contact administrator.";
            }
            TempData["EmailTemplateMessage"] = Message;
            return RedirectToAction("EmailTemplateSettings", "Settings");
        }

        [HttpPost]
        public IActionResult UpdateNewEmailTemplate(EmailTemplate pagemodel, IFormFile[] attachments, string[] attached_files)
        {
            string Message = "", filename = "";
            string savepath = _env.WebRootPath + "\\email_file_attachments";
            hlab_email_file_attachments file_attachment = new hlab_email_file_attachments();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            try
            {
                if (string.IsNullOrEmpty(pagemodel.hlab_email_template.template_name)) pagemodel.hlab_email_template.template_name = "Not Assigned";
                var result = _Email.UpdateEmailTemplate(pagemodel.hlab_email_template);
                _efa.email_template_id = pagemodel.hlab_email_template.id;
                _Email.DeleteAllFileAttachments(_efa);

                foreach (var file in attachments)
                {
                    savepath = _env.WebRootPath + "\\email_file_attachments";
                    filename = Path.GetFileName(file.FileName).ToString();
                    file_attachment = new hlab_email_file_attachments
                    {
                        email_template_id = pagemodel.hlab_email_template.id,
                        file_name = filename
                    };
                    savepath = savepath + "\\" + filename;
                    using (var stream = new FileStream(savepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    _Email.InsertFileAttachment(file_attachment);
                }                    
                
                foreach (var file in attached_files)
                {
                    file_attachment = new hlab_email_file_attachments
                    {
                        email_template_id = pagemodel.hlab_email_template.id,
                        file_name = file
                    };
                    _Email.InsertFileAttachment(file_attachment);
                }
                Message = "success: New Email Templated was created.";
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                Message = "error: Creating new email template failed, please contact administrator.";
            }
            TempData["EmailTemplateMessage"] = Message;
            return RedirectToAction("EmailTemplateSettings", "Settings", new { templateid=pagemodel.hlab_email_template.id });
        }
    }
}