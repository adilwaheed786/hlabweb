using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ILogin
    {
        hlab_users GenerateLoginWindowsAuthenticationUser();
    }
}
