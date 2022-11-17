using HorizonLabLibrary.Interfaces;
using HorizonLabWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.ApiFilter
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class APIKeyHandlers : Attribute, IAsyncActionFilter
    {
        private Interface_hlab_api_keys _apiRepo;

        public APIKeyHandlers(Interface_hlab_api_keys apiRepo)
        {
            _apiRepo = apiRepo;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("HlabApiKey", out var ApiHeaderValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }         

            if (_apiRepo.GetApi(ApiHeaderValue)==null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            await next();
        }
    }
}
