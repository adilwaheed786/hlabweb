using HorizonLabAdmin.Helpers.Containers;
using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces.Session
{
    public interface IHorizonLabSession: IUserSession, ITestTransactionSession, INavigationSession, ICustomerSession, IRequestSession
    {

    }
}
