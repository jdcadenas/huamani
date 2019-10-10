
using sb_admin.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace sb_admin.web.Filters
{
    public class AuthActionFilter : ActionFilterAttribute, IAuthenticationFilter
    {

        public void OnAuthentication(AuthenticationContext filterContext)
        {
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string userName = null;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                userName = filterContext.HttpContext.User.Identity.Name;
            }

            try
            {
                if (!Access(filterContext.RouteData, userName))
                    filterContext.Result = new HttpUnauthorizedResult();

                base.OnActionExecuting(filterContext);
            }
            catch
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

        }

        private bool Access(RouteData routeData, string userName)
        {
            var controllerName = routeData.Values["controller"].ToString();
            var actionName = routeData.Values["action"].ToString();

            GAHEContext data = new GAHEContext();
           var items = data.Navbar;
           var rolesNav = data.Roles;
           var usersNav = data.Users;

           var getAccess = (from nav in items
                            join rol in rolesNav on nav.id equals rol.id
                            join Users  in usersNav on rol.id equals Users.Id
                            where Users.user == userName && nav.controller == controllerName && nav.action == actionName
                            select Users.Id).Single();

           var context = new ActionExecutingContext();

           if (getAccess != 0)
               return true;
           else
               return false;
        }
    }
}