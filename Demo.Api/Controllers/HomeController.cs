namespace Demo.Api.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Models;

    using Okta.Sdk;
    using Okta.Sdk.Configuration;

    public class HomeController : Controller
    {
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy([FromServices] IOktaClient oktaClient)
        {
            var name = this.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
            var user = await oktaClient.Users.GetUserAsync(name.Value);
            var groups = user.Groups.ToEnumerable();

            foreach (var group in groups)
            {
            }

            return this.View(this.User.Claims);
        }
    }
}