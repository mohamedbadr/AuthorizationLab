using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationLab.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            const string issuer = "https://contoso.com";
            var userIdentity = new ClaimsIdentity("SuperSecureLogin");

            var claims = new List<Claim>
            {
                // adds usern ame claim
                new Claim(ClaimTypes.Name, "Mohamed", ClaimValueTypes.String, issuer),
                // adds administrator role claim
                new Claim(ClaimTypes.Role,"Administrator", ClaimValueTypes.String, issuer),
                // adds employeeId claim with id 123
                new Claim("EmployeeId","123", ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.DateOfBirth,"1985-05-15", ClaimValueTypes.Date),
                //new Claim("BadgeNumber","123456",ClaimValueTypes.String,issuer)
                new Claim("TemporaryBadgeExpiry",DateTime.Now.AddDays(-1).ToString(),ClaimValueTypes.String,issuer)
            };

            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });

            return RedirectToLocal(returnUrl);
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}