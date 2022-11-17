using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabServicesRepository : Interface_hlab_services
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;

        public HlabServicesRepository(HorizonLabDbContext hlab_db_context)
        {
            _hlab_Db_Context = hlab_db_context;
        }

        public int AddNewService(hlab_services inputService)
        {
            int return_service_id = 0;
            try
            {
                _hlab_Db_Context.hlab_services.Add(inputService);
                _hlab_Db_Context.SaveChanges();
                return_service_id = inputService.id;
                return return_service_id;
            }
            catch(Exception exc)
            {
                return return_service_id;
            }
        }

        public bool DeleteService(hlab_services service)
        {
            return this.UpdateService(service);
        }

        public IEnumerable<hlab_services> GetActiveServices()
        {
            return _hlab_Db_Context.hlab_services.ToList();
        }

        public List<hlab_web_services_intro> GetServiceHeader()
        {
            return _hlab_Db_Context.hlab_web_services_intro.ToList();
        }

        public hlab_services GetServiceInfo(int id)
        {
            return _hlab_Db_Context.hlab_services.FirstOrDefault(x => x.id == id);
        }

        public bool UpdateService(hlab_services service_info)
        {
            try
            {
                if (service_info != null)
                {
                    _hlab_Db_Context.hlab_services.Update(service_info);
                    _hlab_Db_Context.SaveChanges();
                }
                return true;
            }
            catch(Exception xc)
            {
                return false;
            }
        }

        public bool UpdateServiceHeader(IEnumerable<hlab_web_services_intro> introList)
        {
            try
            {
                foreach(hlab_web_services_intro intro in introList)
                {
                    _hlab_Db_Context.hlab_web_services_intro.Update(intro);
                    _hlab_Db_Context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
 
    }
}
