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
            services.AddTransient(typeof(IGetAnimalDetailQuery<>), typeof(GetAnimalDetailsQuery<>))
                    .AddTransient(typeof(IGetRestrainedAnimalsQuery<>), typeof(GetRestrainedAnimalsQuery<>))
                    .AddTransient(typeof(ICreateAnimalCommand<,>), typeof(CreateAnimalCommand<,>))
                    .AddTransient<IValidator<BearCreating>, CreateBearValidator>()
                    .AddTransient<IGetFamiliesQuery, GetFamiliesQuery>();
            return services;
        }
    }
}