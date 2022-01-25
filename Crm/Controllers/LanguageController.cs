using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Crm.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index(string language)
        {
           if(!string.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }

            var cookie = new HttpCookie("Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            Session["language"] = language;
            return RedirectToAction("Index", "Home", new { language = language });

        }
    }
}