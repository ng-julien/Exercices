namespace Microsoft.Extensions.DependencyInjection
{
    using Demo.Application.Commands;
    using Demo.Application.Queries;
    using Demo.Application.Validators;
    using Demo.Application.Validators.Core;
    using Demo.Zoo.Domain.BearAggregate;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            services.AddTransient<IGetAnimalsByFamilyQuery, GetAnimalsByFamilyQuery>()
                    .AddTransient<IGetBearDetailsQuery, GetBearDetailsQuery>()
                    .AddTransient<IValidator<CreateBear>, CreateBearValidator>()
                    .AddTransient<ICreateBearCommand, CreateBearCommand>()
                    .AddTransient<IGetFamiliesQuery, GetFamiliesQuery>()
                    .AddTransient<IGetAnimalDetailsById, GetAnimalDetailsById>();
            return services;
        }
    }
}