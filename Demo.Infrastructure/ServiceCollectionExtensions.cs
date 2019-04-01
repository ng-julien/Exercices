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
                    .AddTransient<IBearInformationSpecification, BearInformationSpecification>()
                    .AddTransient(
                        typeof(IRestrainedAnimalBaseSpecification<>),
                        typeof(RestrainedAnimalBaseSpecification<>))
                    .AddTransient<IWhatBearCanEatSpecification, WhatBearCanEatCanEatSpecification>()
                    .AddTransient<ITranform<AnimalEntity, BearDetails>, BearDetailsTransform>()
                    .AddTransient<IBearDetailsAdapter, BearDetailsAdapter>()
                    .AddTransient(typeof(IRestrainedAnimalBaseTransform<>), typeof(RestrainedAnimalBaseTransform<>))
                    .AddTransient<IRestrainedAnimalBaseTransform<RestrainedAnimal>, AnimalRestrainedTransform>()
                    .AddTransient(typeof(IRestrainedAnimalAdapter<>), typeof(RestrainedAnimalAdapter<>))
                    .AddTransient<ITranform<Food, Referential>, FoodTransform>()
                    .AddTransient<IFoodsBearAdapter, FoodsBearAdapter>()
                    .AddTransient<ITranform<Family, Referential>, FamilyTransform>()
                    .AddTransient<IFamilyAdapter, FamilyAdapter>()
                    .AddTransient<IAnimalDetailsAdapter, AnimalDetailsAdapter>()
                    .AddTransient<ITranform<AnimalEntity, AnimalDetails>, AnimalDetailsTransform>();

            var enumsOnApplication = AppDomain.CurrentDomain.GetAssemblies()
                                              .Where(assembly => assembly.GetName().Name.StartsWith("Demo."))
                                              .SelectMany(assembly => assembly.ExportedTypes.Where(t => t.IsEnum))
                                              .ToList();
            services.AddSingleton(enumsOnApplication.ToDictionary(type => type.Name, type => type));

            return services;
        }
    }
}