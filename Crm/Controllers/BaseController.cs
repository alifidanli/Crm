using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Crm.Controllers
{
    public class BaseController : Controller
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["CrmConnectionString"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureOnCookie = GetCultureOnCookie(filterContext.HttpContext.Request);

            string cultureOnUrl = filterContext.RouteData.Values.ContainsKey("language")
                ? filterContext.RouteData.Values["language"].ToString()
                : cultureOnCookie;

            string culture = (cultureOnCookie == string.Empty)
                ? (filterContext.RouteData.Values["language"].ToString())
                : cultureOnCookie;

            if (cultureOnUrl != culture)
            {
                filterContext.HttpContext.Response.RedirectToRoute("Default",
                    new
                    {
                        language = culture,
                        controller = filterContext.RouteData.Values["controller"],
                        action = filterContext.RouteData.Values["action"]
                    });
                return;
            }

            SetCurrentCultureOnThread(culture);

            //if (culture != MultiLanguageViewEngine.CurrentCulture)
            //{
            //    (ViewEngines.Engines[0] as MultiLanguageViewEngine).SetCurrentCulture(culture);
            //}

            base.OnActionExecuting(filterContext);

        }

        private static void SetCurrentCultureOnThread(string lang)
        {
            if (string.IsNullOrEmpty(lang)){
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                GlobalizationSection section = (GlobalizationSection)config.GetSection("system.web/globalizitaon");
                lang = section.UICulture;
            }

            var cultureInfo = new System.Globalization.CultureInfo(lang);
              System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
              System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        public static String GetCultureOnCookie(HttpRequestBase request)
        {
            var cookie = request.Cookies["Languages"];
            string culture = string.Empty;
            if (cookie != null)
            {
                culture = cookie.Value;
            }
            return culture;
        }


    }
}