namespace Demo.Api.Core
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;

    using Okta.Sdk;

    internal class GroupsToRolesTransformation : IClaimsTransformation
    {
        private readonly IOktaClient oktaClient;

        public GroupsToRolesTransformation(IOktaClient oktaClient)
        {
            this.oktaClient = oktaClient;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var idClaim = principal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null)
            {
                return principal;
            }

            var user = await this.oktaClient.Users.GetUserAsync(idClaim.Value);

            if (user == null)
            {
                return principal;
            }

            var claims = user.Groups.ToEnumerable()
                             .Select(group => new Claim(ClaimTypes.Role, group.Profile.Name)).ToList();
            principal.AddIdentity(new ClaimsIdentity(claims));

            return principal;
        }
    }
}