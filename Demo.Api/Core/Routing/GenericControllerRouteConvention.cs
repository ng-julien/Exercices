﻿namespace Demo.Api.Core.Routing
{
    using System.Linq;

    using Humanizer;

    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType)
            {
                return;
            }

            var genericType = controller.ControllerType.GenericTypeArguments[0];
            var className = genericType.ShortDisplayName().Humanize();
            var controllerName = string.Concat(className.TakeWhile(c => c != ' '))
                                       .Transform(To.LowerCase)
                                       .Pluralize();

            controller.ControllerName = controllerName;
            controller.RouteValues.Add("family", controllerName);
            controller.RouteValues.Add("familyType", controllerName);
        }
    }
}