using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Tp5.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language/Change/?lang={lang}&returnurl={returnurl}
        public IActionResult Change(string lang, string returnurl)
        {
            if (!string.IsNullOrWhiteSpace(lang))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            if (!string.IsNullOrWhiteSpace(returnurl) && Url.IsLocalUrl(returnurl))
            {
                return LocalRedirect(returnurl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
