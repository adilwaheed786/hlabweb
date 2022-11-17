using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ITestProjectForm
    {
        int AddNewTestProjecrFormToDb(hlab_test_project_forms form);
        bool UpdateNewTestProjecrFormToDb(hlab_test_project_forms form);
        projectrequestsformview GetProjectRequestFormInfo(int proj_form_id);
        List<projectrequestsformview> ListProjectRequestFormInfo(projectrequestsformview param);
    }
}
