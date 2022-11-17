using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_test_project_form
    {
        int AddNewTestPorjectForm(hlab_test_project_forms new_form);
        bool UpdateTestProjForm(hlab_test_project_forms param);
        projectrequestsformview GetProjectRequestFormInfo(int proj_form_id);
        List<projectrequestsformview> ListProjectRequestFormInfo(projectrequestsformview param);
    }
}
