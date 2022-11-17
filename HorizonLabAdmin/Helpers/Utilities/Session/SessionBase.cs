using HorizonLabAdmin.Helpers.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities.Session
{
    public class SessionBase
    {
        public IHttpContextAccessor _httpContextAccessor { get;set;}
        private readonly ILogger<SessionBase> _logger;

        public SessionBase(IHttpContextAccessor httpContextAccessor, ILogger<SessionBase> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        protected bool IsSessionInputNotNull(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        protected void SetStringSession(StringSessionParameter parameter)
        {
            _httpContextAccessor.HttpContext.Session.SetString(parameter.Key, parameter.Value);
        }

        protected void SetStringSessionWithNullValidation(StringSessionParameter parameter) // set Blank value if Value is NULL
        {
            _httpContextAccessor.HttpContext.Session.SetString(parameter.Key, parameter.Value != null ? parameter.Value : "");
        }

        protected void SetBooleanSessionWithNullValidation(BooleanSessionParameter parameter) // set Blank value if Value is NULL
        {
            if (!parameter.Value.HasValue) parameter.Value = false;
            _httpContextAccessor.HttpContext.Session.SetString(parameter.Key, parameter.Value.ToString());
        }

        protected void SetDateTimeSession(DateTimeSessionParameter datetime_parameter) //set Blank value if Value is NULL
        {
            try
            {
                _httpContextAccessor.HttpContext.Session.SetString(datetime_parameter.Key, datetime_parameter.Value != null ? string.Format("{0:d}", datetime_parameter.Value) : "");
            }
            catch (Exception exc)
            {
                throw exc.InnerException;
            }
            
        }

        protected void SetIntSession(IntSessionParameter parameter)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32(parameter.Key, parameter.Value);
        }

        protected bool IsSessionStringHasValue(string SessionKey)
        {
            try
            {
                return !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString(SessionKey));
            }
            catch(Exception exc)
            {
                _logger.LogError($"ERROR: SessionBase > IsSessionStringHasValue():{exc.InnerException}");
                throw exc.InnerException;
            }
        }

        protected int GetSessionIntValue(string SessionKey)
        {
            int? session_int_value = _httpContextAccessor.HttpContext.Session.GetInt32(SessionKey);
            return session_int_value != 0 ? session_int_value ?? default(int) : 0;
        }

        public string GetSessionStringValue(string SessionKey)
        {
            return _httpContextAccessor.HttpContext.Session.GetString(SessionKey);
        }

        public bool GetSessionBooleanValue(string SessionKey)
        {
            return Convert.ToBoolean(_httpContextAccessor.HttpContext.Session.GetString(SessionKey));
        }

        public DateTime? FormatStringToDateTime(string datetime)
        {
            try
            {
                DateTime? str_date = null;
                if (string.IsNullOrEmpty(datetime)) return null;
                str_date = DateTime.Parse(datetime);
                return str_date;
            }
            catch (Exception exc)
            {
                _logger.LogError($"SessionBase > FormatStringToDateTime(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}
