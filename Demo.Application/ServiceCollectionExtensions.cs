namespace Demo.Application
{
    using Commands;

    using Microsoft.Extensions.DependencyInjection;

    using Queries;

    using Validators;
    using Validators.Core;

    using Zoo.Domain.BearAggregate;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            services.AddTransient<IGetAnimalsByFamilyQuery, GetAnimalsByFamilyQuery>()
                    .AddTransient<IGetBearDetailsQuery, GetBearDetailsQuery>()
                    .AddTransient<IValidator<CreateBear>, CreateBearValidator>()
                    .AddTransient<ICreateBearCommand, CreateBearCommand>();
            return services;
        }
    }
}