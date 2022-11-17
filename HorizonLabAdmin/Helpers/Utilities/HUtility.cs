using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HUtility: IUtility
    {
        private readonly ILogger<HUtility> _logger;

        public HUtility(ILogger<HUtility> logger)
        {
            _logger = logger;
        }

        public string GetSelectedTextFromSelectList(List<SelectListItem> selectlistitem, string selecteditem)
        {
            return selectlistitem.Where(x => x.Value == selecteditem).First().Text;
        }

        public List<SelectListItem> GenerateSelectListItem(IEnumerable item_list, string str_text, string str_value)
        {
            return new SelectList(item_list, str_text, str_value).ToList();
        }

        public List<SelectListItem> SetSelectedItemFromList(List<SelectListItem> selectlistitem, string selecteditem)
        {
            selectlistitem.Where(x => x.Value.ToLower() == selecteditem.ToLower()).First().Selected = true;
            return selectlistitem;
        }

        public string FormatIntIDToString(int id)
        {
            return string.Format("{0:00000000}", id);
        }

        public string FormatDateTimeToMMDDYYString(DateTime? datetime)
        {
            try
            {
                if (datetime != null) return datetime != null ? datetime.Value.ToString("dd/MM/yyyy") : "";
                return "";
            }
            catch (Exception exc)
            {
                throw exc.InnerException;
            }
        }

        public string FormatDecimalToMoneyString(decimal amount)
        {
            try
            {
                return string.Format("{0:C}", amount);
            }
            catch (Exception exc)
            {
                throw exc.InnerException;
            }
        }

        public List<SelectListItem> GenerateTrueFalseSelectList()
        {
            return new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = "True", Value = "true"},
                    new SelectListItem { Selected = false, Text = "False", Value = "false"}
                };
        }

        public List<SelectListItem> GenerateYesNoSelectList()
        {
            return new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = "Yes", Value = "True"},
                    new SelectListItem { Selected = false, Text = "No", Value = "False"}
                };
        }

        public string SetDefaultStringTimeIfNullOrEmpty(string input_time)
        {
            string str_time = "";
            try
            {
                str_time = FunctionLibrary.IntToMilitaryDate(Convert.ToInt32(input_time));
            }
            catch (Exception exc)
            {
                str_time = "00:00";
            }
            return str_time;
        }

        public void UploadFile(FileUploadParameter file_parameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(file_parameter.save_path) &&  file_parameter.file != null)
                {
                    string image_name = Path.GetFileName(file_parameter.file.FileName).ToString();
                    string file_extension = Path.GetExtension(file_parameter.file.FileName).ToString().ToLower();

                    using (var stream = new FileStream(file_parameter.save_path, FileMode.Create))
                    {
                        file_parameter.file.CopyTo(stream);
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.LogError($"HUtility > UploadFile(): {exc.Message} {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool IsFileOnRequiredFormat(IFormFile file, string required_file_format)
        {
            string image_name = Path.GetFileName(file.FileName).ToString();
            string file_extension = Path.GetExtension(file.FileName).ToString().ToLower();
            if (required_file_format.ToLower() != file_extension)
            {
                return false;
            }
            return true;
        }

        public bool IsDateTimeNullOrEmpty(DateTime? date_time_parameter)
        {
            try
            {
                if(date_time_parameter != null || date_time_parameter != DateTime.MinValue)
                {
                    return false;
                }
                return true;
            }
            catch(Exception exc)
            {
                throw exc.InnerException;
            }
        }

        public string ConvertAsciiCodeToString(int unicode)
        {
            try
            {
                char character = (char)unicode;
                return character.ToString();
            }
            catch(Exception exc)
            {
                throw exc.InnerException;
            }
        }

        public DateTime AddDayToNullableDateTime(DateTime? NullableDateTime, int AdditionalDays = 0)
        {
            DateTime temp_date = DateTime.Now;
            if (NullableDateTime != null && NullableDateTime != DateTime.MinValue)
            {
                temp_date = NullableDateTime ?? DateTime.Now;
                return temp_date.AddDays(1);
            }
            return DateTime.MinValue;
        }

        public string GetFileNameFromFormFile(IFormFile form_file)
        {
            try
            {
                return Path.GetFileName(form_file.FileName).ToString();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HUtility > GetFileNameFromFormFile(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public string GetFileExensionNameFromFormFile(IFormFile form_file)
        {
            try
            {
                return Path.GetExtension(form_file.FileName).ToString();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HUtility > GetFileExensionNameFromFormFile(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public string AddSuffixToID(int input_id)
        {
            try
            {
                return string.Format("{0:00000000}", input_id);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HUtility > AddSuffixToID(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool IsNullableIntNotEmpty(int? input_integer)
        {
            try
            {
                if (input_integer != null && input_integer != 0) {
                    return true;
                }
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HUtility > IsNullableIntNotEmpty(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<SelectListItem> GenerateSortByListSelectList()
        {
            List<SelectListItem> sortByList = new List<SelectListItem>();
            sortByList.Add(new SelectListItem { Text = "", Value = "" });
            sortByList.Add(new SelectListItem { Text = "Transaction ID", Value = "id" });
            sortByList.Add(new SelectListItem { Text = "Customer Name", Value = "first_name" });
            sortByList.Add(new SelectListItem { Text = "Submitted Date", Value = "submtd_datetime" });
            sortByList.Add(new SelectListItem { Text = "Received Date", Value = "rcv_date" });
            sortByList.Add(new SelectListItem { Text = "Test Date", Value = "test_date" });
            return sortByList;
        }

        public List<SelectListItem> GenerateSortByOptionSelectList()
        {
            List<SelectListItem> sortOptionList = new List<SelectListItem>();
            sortOptionList.Add(new SelectListItem { Text = "", Value = "" });
            sortOptionList.Add(new SelectListItem { Text = "Ascending", Value = "asc" });
            sortOptionList.Add(new SelectListItem { Text = "Descending", Value = "desc" });
            return sortOptionList;
        }

        public string SetDefaultStatusIfInvalid(string input)
        {
            if (string.IsNullOrEmpty(input)) return "true";
            if (input.ToLower() != "true" && input.ToLower() != "false")
            {
                input = "true";
            }
            return input;
        }

        public DateTime? FormatStringToDateTime(string datetime)
        {
            try
            {
                DateTime? str_date = null;
                if (string.IsNullOrEmpty(datetime)) return null;
                str_date = DateTime.Parse(datetime);
                return str_date;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HUtility > FormatStringToDateTime(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public DateTime? FormatDateFrom_ddMMyyyy(string datetime)
        {
            try
            {
                if (string.IsNullOrEmpty(datetime)) return null;
                string Month = "";
                string Day = "";
                string Year = "";

                var str_details = datetime.Split('/');

                Day = str_details[0];
                Month = str_details[1];
                Year = str_details[2];

                datetime = $"{Day}/{Month}/{Year}";

                return FormatStringToDateTime(datetime);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HUtility > FormatStringToddMMyyyy(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public DateTime? FormatDateFrom_MMddyyyy(string datetime)
        {
            try
            {
                if (string.IsNullOrEmpty(datetime)) return null;
                string Month = "";
                string Day = "";
                string Year = "";

                var str_details = datetime.Split('/');
                Day = str_details[0];
                Month = str_details[1];
                Year = str_details[2];

                datetime = $"{Month}/{Day}/{Year}";

                return FormatStringToDateTime(datetime);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HUtility > FormatStringToddMMyyyy(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public SplitDateTimeOutput SplitDateTime(DateTime? input)
        {
            SplitDateTimeOutput output = new SplitDateTimeOutput();
            if (input == null) return output;
            output.strDate = input.Value.ToString("yyyy-MM-dd");
            output.strTime = $"{input.Value.Hour}:{input.Value.Minute}";
            return output;
        }

        public DateTime GetFirstDateInWeek(DateTime datetime)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - datetime.DayOfWeek;
            DateTime fdowDate = datetime.AddDays(offset);
            return fdowDate;
        }

        public DateTime GetLastDateInWeek(DateTime datetime)
        {
            DateTime ldowDate = GetFirstDateInWeek(datetime).AddDays(6);
            return ldowDate;
        }

        public DateTime GetFirstDateOfMonth(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            return firstDayOfMonth;
        }

        public DateTime GetLastDateOfMonth(DateTime date)
        {
            DateTime lastDayOfMonth = GetFirstDateOfMonth(date).AddMonths(1).AddDays(-1);
            return lastDayOfMonth;
        }
    }
}
