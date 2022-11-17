using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_test_projects
    {
        IEnumerable<hlab_test_projects> GetAllTestProjects();
        IEnumerable<projectrequestsupplyview> GetAllProjectRequestSupplies(int proj_form_id);        
        IEnumerable<temporaryprojectrequestsview> GetTemporaryProjectRequestsView(temporaryprojectrequestsview param);
        IEnumerable<hlab_temp_requests> GetTemporaryProjectRequests(hlab_temp_requests param);
        bool AddNewTestPorject(hlab_test_projects new_project);
        bool DeleteAddTestProjectSupplies(project_form_supply_param parameter);
        bool DeleteTemporaryRequests(BulkRequestInsertParameter param);
        bool BulkCreateTemporaryRequest(bulkrequest_params param);
        bool InsertBulkRequestToDb(BulkRequestInsertParameter param);
        bool UpdateBulkRequest(BulkRequestInsertParameter param);
    }
}
