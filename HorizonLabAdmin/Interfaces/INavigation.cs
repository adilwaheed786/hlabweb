using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface INavigation
    {
        List<SelectListItem> ListSortByItems();
        List<SelectListItem> ListSortOptionItems();
        List<SelectListItem> SetSelectedItemFromSortByStringList(List<SelectListItem> selectlistitem);
        List<SelectListItem> SetSelectedItemFromSortByOptionList(List<SelectListItem> selectlistitem);
        List<testtransactionsview> SortTransactionList(TestTransactionSearchParameters parameter);
        TestTransactionSearchParameters SetSortByFields (TestTransactionSearchParameters parameter);
        TestTransactionSearchParameters SetSearchDateFieldValues (TestTransactionSearchParameters search_parameter, test_transaction test_transaction_parameter);
        string GetSelectedItemFromSortByStringList(List<SelectListItem> selectlistitem);
        string GetSelectedItemFromSortByOptionList(List<SelectListItem> selectlistitem);
        int SetRecordEnd(int record_count, int record_per_batch);
    }
}
