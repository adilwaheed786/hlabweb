using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabApiKeyRepository : Interface_hlab_api_keys
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;

        public HlabApiKeyRepository(HorizonLabDbContext hlab_Db_Context)
        {
            _hlab_Db_Context = hlab_Db_Context;
        }

        public hlab_api_keys GetApi(string ApiHeaderValue)
        {
            return _hlab_Db_Context.hlab_api_keys.FirstOrDefault(x => x.id.ToString() == ApiHeaderValue);
        }
    }
}
