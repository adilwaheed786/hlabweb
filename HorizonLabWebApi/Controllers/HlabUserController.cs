using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_user")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabUserController : ControllerBase
    {
        private Interface_hlab_users _hlabUserRepo;

        public HlabUserController(Interface_hlab_users hlabServiceRepo)
        {
            _hlabUserRepo = hlabServiceRepo;
        }

        [HttpPost("addnewuser")]
        public ActionResult AddNewUser(hlab_users user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (string.IsNullOrEmpty(user.fname) || string.IsNullOrEmpty(user.lname) || string.IsNullOrEmpty(user.username)) return BadRequest("Can't add new user with incomplete information (First / Last name and Username)");
                bool result = _hlabUserRepo.AddNewUserAccount(user);
                if (result) return Ok();
                return BadRequest("AddNewUser Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("AddNewUser Exception Error: " + xc);
            }
        }

        [HttpPost("updateuser")]
        public ActionResult UpdateUser(hlab_users user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (string.IsNullOrEmpty(user.fname) || string.IsNullOrEmpty(user.lname) || string.IsNullOrEmpty(user.username)) return BadRequest("Can't save changes on a user with incomplete information (First / Last name and Username)");
                bool result = _hlabUserRepo.UpdateUserInformation(user);
                if (result) return Ok();
                return BadRequest("UpdateUser Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateUser Exception Error: " + xc);
            }
        }

        [HttpGet("getuserinfo")]
        public hlab_users GetUserInfo(string userid)
        {
            hlab_users user = _hlabUserRepo.GetUserDetails(Convert.ToInt32(userid));
            return user;
        }

        [HttpGet("getallacvtivehlabuseraccounts")]
        public List<hlab_users> GetAllAcvtiveHlabUserAccounts()
        {
            List<hlab_users> userList = _hlabUserRepo.GetAllActiveAccounts().ToList();
            return userList;
        }

        [HttpGet("getallinacacvtivehlabuseraccounts")]
        public List<hlab_users> GetAllInAcvtiveHlabUserAccounts()
        {
            List<hlab_users> userList = _hlabUserRepo.GetAllInActiveAccounts().ToList();
            return userList;
        }
        
        [HttpGet("searchhlabuseraccountsbyfirstname")]
        public List<hlab_users> SearchHlabUserAccountsByFirstName(string firstName, string status="true")
        {
            List<hlab_users> userList = new List<hlab_users>();
            if (status == "true")
            {
                userList = _hlabUserRepo.GetAllActiveAccounts().Where(x => x.fname.Contains(firstName)).ToList();
            }
            else
            {
                userList = _hlabUserRepo.GetAllInActiveAccounts().Where(x => x.fname.Contains(firstName)).ToList();
            }
            return userList;
        }

        [HttpGet("searchhlabuseraccountsbylastname")]
        public List<hlab_users> SearchHlabUserAccountsByLastName(string lastName, string status= "true")
        {
            List<hlab_users> userList = new List<hlab_users>();
            if (status == "true")
            {
                userList = _hlabUserRepo.GetAllActiveAccounts().Where(x => x.lname.Contains(lastName)).ToList();
            }
            else
            {
                userList = _hlabUserRepo.GetAllInActiveAccounts().Where(x => x.lname.Contains(lastName)).ToList();
            }
            return userList;
        }

        [HttpGet("searchhlabuseraccountsbyusername")]
        public List<hlab_users> SearchHlabUserAccountsByUserName(string userName, string status = "true")
        {
            List<hlab_users> userList = new List<hlab_users>();
            if (status == "true")
            {
                userList = _hlabUserRepo.GetAllActiveAccounts().Where(x => x.username.Contains(userName)).ToList();
            }
            else
            {
                userList = _hlabUserRepo.GetAllInActiveAccounts().Where(x => x.username.Contains(userName)).ToList();
            }
            return userList;
        }

        [HttpGet("searchhlabuseraccountsbyemail")]
        public List<hlab_users> SearchHlabUserAccountsByEmail(string email, string status = "true")
        {
            List<hlab_users> userList = new List<hlab_users>();
            if (status == "true")
            {
                userList = _hlabUserRepo.GetAllActiveAccounts().Where(x => x.email.Contains(email)).ToList();
            }
            else
            {
                userList = _hlabUserRepo.GetAllInActiveAccounts().Where(x => x.email.Contains(email)).ToList();
            }
            return userList;
        }

        [HttpGet("getuseraccesslist")]
        public List<hlab_user_access> GetUserAccessList()
        {
            List<hlab_user_access> user_Accesses = _hlabUserRepo.GetUserAccessList();
            return user_Accesses;
        }
    }
}