using HorizonLabWebApi.Models;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabServiceDetailRepository : Interface_hlab_service_details
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;

        public HlabServiceDetailRepository (HorizonLabDbContext hlab_db_context)
        {
            _hlab_Db_Context = hlab_db_context;
        }

        public IEnumerable<hlab_service_details> GetAllServiceDetails(int service_id)
        {
            return _hlab_Db_Context.hlab_service_details.Where(x => x.service_id == service_id).ToList();
        }

        public hlab_service_details GetServiceDetailInfo(int id)
        {
            return _hlab_Db_Context.hlab_service_details.FirstOrDefault(x => x.id == id);
        }

        public bool AddNewServiceDetail(hlab_service_details service_info)
        {
            try
            {
                _hlab_Db_Context.hlab_service_details.Add(new hlab_service_details()
                {
                    service_detail = service_info.service_detail,
                    service_id = service_info.service_id,
                    description = service_info.description
                });
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool UpdateServiceDetail(hlab_service_details service_info)
        {
            try
            {
                if (service_info != null)
                {
                    _hlab_Db_Context.hlab_service_details.Update(service_info);
                    _hlab_Db_Context.SaveChanges();
                }
                return true;
            }
            catch (Exception xc)
            {
                return false;
            }
        }

        public bool DeleteServiceDetail(hlab_service_details service_info)
        {
            try
            {
                if (service_info != null)
                {
                    _hlab_Db_Context.hlab_service_details.Remove(service_info);
                    _hlab_Db_Context.SaveChanges();
                }
                return true;
            }
            catch (Exception xc)
            {
                return false;
            }
        }
    }
}
