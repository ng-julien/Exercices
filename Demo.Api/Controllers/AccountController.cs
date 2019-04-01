namespace Demo.Api.Controllers
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : Controller
    {
        public IActionResult Login()    
        {
            if (!this.HttpContext.User.Identity.IsAuthenticated)
            {
                return this.Challenge(OpenIdConnectDefaults.AuthenticationScheme);
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return new SignOutResult(
                new[]
                    {
                        OpenIdConnectDefaults.AuthenticationScheme,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    });
        }

        [HttpGet("signout/callback")]
        public IActionResult Signout()
        {
            return this.RedirectToAction("Index", "Home");
        }
    }
}