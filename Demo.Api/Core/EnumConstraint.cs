namespace Demo.Api.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class EnumConstraint : IRouteConstraint
    {
        private readonly Type enumType;

        public EnumConstraint(Dictionary<string, Type> enumTypeByName, string enumFamily)
        {
            this.enumType = enumTypeByName[enumFamily];
        }

        public bool Match(
            HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var response = Enum.GetNames(this.enumType)
                               .Any(s => s.ToLowerInvariant() == values[routeKey].ToString().ToLowerInvariant());
            return response;
        }
    }
}