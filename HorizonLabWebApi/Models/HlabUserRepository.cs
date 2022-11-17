using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Models
{
    public class HlabUserRepository : Interface_hlab_users
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabUserRepository> _logger;

        public HlabUserRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabUserRepository> logger)
        {
           _hlab_Db_Context = hlab_db_context;
           _logger = logger;
        }

        public hlab_users GetUserAuthentication(string username, string password)
        {
            hlab_users user = new hlab_users();
            try
            {
                if (string.IsNullOrEmpty(password)) //for windows authentication
                {
                    user = _hlab_Db_Context.hlab_users.FirstOrDefault(e => e.username == username);
                }
                else //for login & password authentication
                {
                    user = _hlab_Db_Context.hlab_users.FirstOrDefault(e => e.username == username && e.password == MD5Hash(password));
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }            
            return user;
        }

        public IEnumerable<hlab_users> GetAllActiveAccounts()
        {
            return _hlab_Db_Context.hlab_users.Where(x => x.status == true).ToList();
        }

        public IEnumerable<hlab_users> GetAllInActiveAccounts()
        {
            return _hlab_Db_Context.hlab_users.Where(x => x.status == false).ToList();
        }

        public hlab_users GetUserDetails(int UserId)
        {
            return _hlab_Db_Context.hlab_users.FirstOrDefault(x => x.user_id == UserId);
        }

        private static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public bool UpdateUserInformation(hlab_users user)
        {
            if (user == null) return false;
            try
            {
                _hlab_Db_Context.hlab_users.Update(user);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.Message);
                return false;
            }
        }

        public bool AddNewUserAccount(hlab_users user)
        {
            if (user == null) return false;
            try
            {
                _hlab_Db_Context.hlab_users.Add(user);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch(Exception xc)
            {
                _logger.LogError(xc.Message);
                return false;
            }
        }

        public List<hlab_user_access> GetUserAccessList()
        {
            return _hlab_Db_Context.hlab_user_access.ToList();
        }

        public IEnumerable<hlab_users> SearchUserRecords(string searchString, string searchBy, bool accountStatus)
        {
            throw new NotImplementedException();
        }
    }
}
