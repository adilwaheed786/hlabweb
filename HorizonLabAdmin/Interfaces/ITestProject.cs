using HorizonLabAdmin.Helpers.Containers;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ITestProject
    {
        ProjectRequestPageObject PrepareProjectRequestData(ProjectRequestPageObject project);
        bool ProcessBulkRequests(ProjectRequestPageObject project);
        bool InsertBulkProjectRequestToDb(ProjectRequestPageObject project);
        bool DeleteTemporaryRequests(ProjectRequestPageObject project);
        bool UpdateTemporaryRequests(ProjectRequestPageObject project);
        bool DeleteAddProjectSupplies(project_form_supply_param param);        
        hlab_test_project_forms ConvertTestProjectToDbObject(ProjectRequestPageObject page_object);
        List<temporaryprojectrequestsview> ListProjectRequestRecords(ProjectRequestPageObject request);
        List<hlab_temp_requests> ListProjectRequestRecords(hlab_temp_requests request);
        List<projectrequestsupplyview> GetProjectRequestSuppliesFromDb(int proj_form_id);
        List<hlab_temp_requests> RemoveRequestFromList(List<hlab_temp_requests> request_list, int request_id);
    }
}
