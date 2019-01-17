namespace Demo.Api.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class EnumConstraint : IRouteConstraint
    {
        private static readonly Dictionary<string, Type> EnumTypeByName;

        private readonly Type enumType;

        static EnumConstraint()
        {
            var enumsOnApplication = AppDomain.CurrentDomain.GetAssemblies()
                                              .Where(assembly => assembly.GetName().Name.StartsWith("Demo."))
                                              .SelectMany(assembly => assembly.GetTypes().Where(t => t.IsEnum))
                                              .ToList();

            EnumTypeByName = enumsOnApplication.ToDictionary(type => type.Name, type => type);
        }

        public EnumConstraint(string enumFamily)
        {
            this.enumType = EnumTypeByName[enumFamily];
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