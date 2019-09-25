namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Linq;

    using Configuration;

    using Demo.Infrastructure.Adapters;
    using Demo.Infrastructure.Repositories;
    using Demo.Infrastructure.Repositories.Entities;
    using Demo.Infrastructure.Specifications;
    using Demo.Infrastructure.Transforms;
    using Demo.Infrastructure.Transforms.Core;
    using Demo.Zoo.Domain.AnimalAggregate;
    using Demo.Zoo.Domain.BearAggregate;
    using Demo.Zoo.Domain.Common;
    using Demo.Zoo.Domain.GiraffeAggregate;
    using Demo.Zoo.Domain.LionAggregate;
    using Demo.Zoo.Domain.Referentials;

    using EntityFrameworkCore;

    using AnimalEntity = Demo.Infrastructure.Repositories.Entities.Animal;
    using Food = Demo.Infrastructure.Repositories.Entities.Food;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<IPatternContext, PatternContext>(
                options =>
                    options.UseSqlServer(configuration.GetConnectionString(nameof(PatternContext))));

            services.AddTransient(typeof(IReader<>), typeof(Reader<>))
                    .AddTransient(typeof(IWriter<>), typeof(Writer<>))
                    .AddTransient(typeof(IAnimalDetailsSpecification<>), typeof(AnimalDetailsSpecification<>))
                    .AddTransient(typeof(IRestrainedAnimalSpecification<>), typeof(RestrainedAnimalSpecification<>))
                    .AddTransient<IWhatBearCanEatSpecification, WhatBearCanEatCanEatSpecification>()
                    .AddTransient<IAnimalDetailsAdapter<BearDetails>, AnimalDetailsAdapter<BearDetails, BearDetailsNotFound>>()
                    .AddTransient<IAnimalDetailsAdapter<GiraffeDetails>, AnimalDetailsAdapter<GiraffeDetails, GiraffeDetailsNotFound>>()
                    .AddTransient<IAnimalDetailsAdapter<LionDetails>, AnimalDetailsAdapter<LionDetails, LionDetailsNotFound>>()
                    .AddTransient(typeof(IRestrainedAnimalAdapter<>), typeof(RestrainedAnimalAdapter<>))
                    .AddTransient<IFoodsBearAdapter, FoodsBearAdapter>()
                    .AddTransient<IFamilyAdapter, FamilyAdapter>()
                    .AddTransient(typeof(IAnimalDetailsTransform<>), typeof(AnimalDetailsTransform<>))
                    .AddTransient(typeof(IRestrainedAnimalTransform<>), typeof(RestrainedAnimalTransform<>))
                    .AddTransient<ITransform<Food, Referential>, FoodTransform>()
                    .AddTransient<ITransform<Family, Referential>, FamilyTransform>();

            var enumsOnApplication = AppDomain.CurrentDomain.GetAssemblies()
                                              .Where(assembly => assembly.GetName().Name.StartsWith("Demo."))
                                              .SelectMany(assembly => assembly.ExportedTypes.Where(t => t.IsEnum))
                                              .ToList();
            services.AddSingleton(enumsOnApplication.ToDictionary(type => type.Name, type => type));

            return services;
        }
    }
}