namespace Demo.Api.Core.Constraints
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Humanizer;

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
            var family = values[routeKey].ToString().Pluralize().Transform(To.LowerCase);
            var isKnown = Enum.GetNames(this.enumType)
                               .Any(s => s.Transform(To.LowerCase) == family);
            if (isKnown)
            {
                values[routeKey] = family;
            }

            return isKnown;
        }
    }
}