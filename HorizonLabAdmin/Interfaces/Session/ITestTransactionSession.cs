using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces.Session
{
    public interface ITestTransactionSession
    {
        void SetTestTransactionDisplayOptionSession(string option);
        void SetIntSearchWaterBacteriaTransactionId(int transactionid);
        void SetIntSearchPackakgeClassId(int package_class_id);
        void SetIntSearchPackakgeId(int packageid);
        void SetIntProjectId(int Projectid);

        bool IsTestTransactionDisplayOptionHasValue();
        int GetIntSearchWaterBacteriaTransactionId();        
        int GetSearchPackakgeClassId();        
        int GetSearchPackakgeId();
        int GetProjectId();
    }
}
