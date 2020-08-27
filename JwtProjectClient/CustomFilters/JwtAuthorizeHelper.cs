using JwtProjectClient.Builders.Concrete;
using JwtProjectClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace JwtProjectClient.CustomFilters
{
    public class JwtAuthorizeHelper
    {
        public static void CheckUserRole(AppUser activeUser,string roles,ActionExecutingContext context)
        {
            if (!string.IsNullOrWhiteSpace(roles))
            {
                Status status = null;
                if (roles.Contains(","))
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new MultiRoleStatusBuilder());
                    status=director.GenerateStatus(activeUser, roles);
                }
                else
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new SingleRoleStatusBuilder());
                    status=director.GenerateStatus(activeUser, roles);
                }

                CheckStatus(status,context);

            }

        }
        private static void CheckStatus(Status status,ActionExecutingContext context)
        {
            if (!status.AccessStatus)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);

            }

        }
        public static AppUser GetActiveUser(HttpResponseMessage responseMessage)
        {
            var activeUser = JsonConvert.DeserializeObject<AppUser>(responseMessage.Content.ReadAsStringAsync().Result);
            return activeUser;

        }
        public static bool CheckToken(ActionExecutingContext context,out string token)
        {
            token = context.HttpContext.Session.GetString("token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                return true;
            }

            context.Result = new RedirectToActionResult("SignIn", "Account", null);

            return false;

        }
        public static HttpResponseMessage GetActiveUserResponseMessage(string token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = httpClient.GetAsync("http://localhost:59334/api/Auth/ActiveUser").Result;
            return responseMessage;

        }
    }
}
