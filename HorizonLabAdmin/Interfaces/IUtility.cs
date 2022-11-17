using HorizonLabAdmin.Helpers.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IUtility
    {
        List<SelectListItem> SetSelectedItemFromList(List<SelectListItem> selectlistitem, string selecteditem);
        List<SelectListItem> GenerateSelectListItem(IEnumerable item_list, string str_label, string str_value);
        List<SelectListItem> GenerateTrueFalseSelectList();
        List<SelectListItem> GenerateYesNoSelectList();
        List<SelectListItem> GenerateSortByListSelectList();
        List<SelectListItem> GenerateSortByOptionSelectList();

        string GetSelectedTextFromSelectList(List<SelectListItem> selectlistitem, string selecteditem);
        string FormatIntIDToString(int id);
        string FormatDateTimeToMMDDYYString(DateTime? datetime);
        DateTime? FormatStringToDateTime(string datetime);
        DateTime? FormatDateFrom_MMddyyyy(string datetime);
        DateTime? FormatDateFrom_ddMMyyyy(string datetime);
        DateTime GetFirstDateInWeek(DateTime datetime);
        DateTime GetFirstDateOfMonth(DateTime datetime);
        DateTime GetLastDateInWeek(DateTime datetime);
        DateTime GetLastDateOfMonth(DateTime datetime);
        string FormatDecimalToMoneyString(decimal amount);
        string SetDefaultStatusIfInvalid(string input);
        string SetDefaultStringTimeIfNullOrEmpty(string input_time);
        string ConvertAsciiCodeToString(int unicode);
        string GetFileNameFromFormFile(IFormFile form_file);
        string GetFileExensionNameFromFormFile(IFormFile form_file);
        string AddSuffixToID(int input_id);
        SplitDateTimeOutput SplitDateTime(DateTime? input);
        void UploadFile(FileUploadParameter file_parameter);        

        bool IsFileOnRequiredFormat(IFormFile file, string required_file_format);
        bool IsDateTimeNullOrEmpty(DateTime? date_time_parameter);
        bool IsNullableIntNotEmpty(int? input_integer);

        DateTime AddDayToNullableDateTime(DateTime? NullableDateTime, int AdditionalDays = 0);
    }
}
