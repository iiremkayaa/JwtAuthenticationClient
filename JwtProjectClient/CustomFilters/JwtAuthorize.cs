﻿using JwtProjectClient.Builders.Abstract;
using JwtProjectClient.Builders.Concrete;
using JwtProjectClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtProjectClient.CustomFilters
{
    public class JwtAuthorize :ActionFilterAttribute
    {
        public string Roles { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("token");
            if(JwtAuthorizeHelper.CheckToken(context,out token))
            {


                var responseMessage = JwtAuthorizeHelper.GetActiveUserResponseMessage(token);
                
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {

                    JwtAuthorizeHelper.CheckUserRole(JwtAuthorizeHelper.GetActiveUser(responseMessage), Roles, context);
                }
                else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    context.HttpContext.Session.Remove("token");
                    context.Result = new RedirectToActionResult("SignIn", "Account", null);

                }
                else
                {
                    var statusCode = responseMessage.StatusCode.ToString();
                    context.HttpContext.Session.Remove("token");
                    context.Result = new RedirectToActionResult("ApiError", "Account", new
                    {
                        code = statusCode
                    });
                }
            }
        }

    }
}
