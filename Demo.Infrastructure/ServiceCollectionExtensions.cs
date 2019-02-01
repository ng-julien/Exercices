namespace Demo.Infrastructure
{
    using System;
    using System.Linq;

    using Adapters;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Repositories;
    using Repositories.Entities;

    using Specifications;

    using Transforms;
    using Transforms.Core;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.BearAggregate;

    using AnimalEntity = Repositories.Entities.Animal;
    using Food = Repositories.Entities.Food;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IDemoDbContext>(
                _ =>
                    {
                        var demoDbContext = new DemoDbContext(configuration.GetConnectionString(nameof(DemoDbContext)));
                        demoDbContext.Database.Log = Console.WriteLine;
                        return demoDbContext;
                    });

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
                    .AddTransient<ITranform<Food, Zoo.Domain.BearAggregate.Food>, FoodTransform>()
                    .AddTransient<IFoodsBearAdapter, FoodsBearAdapter>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                      .Where(assembly => assembly.GetName().Name.StartsWith("Demo."));
            var enumsOnApplication = assemblies.SelectMany(assembly => assembly.ExportedTypes.Where(t => t.IsEnum))
                                               .ToList();
            services.AddSingleton(enumsOnApplication.ToDictionary(type => type.Name, type => type));

            return services;
        }
    }
}