﻿namespace Demo.Infrastructure.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Core;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    using Repositories.Entities;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.BearAggregate;
    using Zoo.Domain.GiraffeAggregate;
    using Zoo.Domain.LionAggregate;

    internal interface IRestrainedAnimalBaseSpecification<TRestrainedAnimal> : ISpecification<Animal>
    {
    }

    internal class RestrainedAnimalBaseSpecification<TRestrainedAnimal> : Specification<Animal>, IRestrainedAnimalBaseSpecification<TRestrainedAnimal>
    {
        private readonly int[] families;

        private readonly Dictionary<Type, int[]> familyByType = new Dictionary<Type, int[]>
                                                                    {
                                                                        {
                                                                            typeof(RestrainedBear),
                                                                            new[] { FamilyCode.Bear }
                                                                        },
                                                                        {
                                                                            typeof(RestrainedGiraffe),
                                                                            new[] { FamilyCode.Giraffe }
                                                                        },
                                                                        {
                                                                            typeof(RestrainedLion),
                                                                            new[] { FamilyCode.Lion }
                                                                        },
                                                                        { typeof(RestrainedAnimal), FamilyCode.All }
                                                                    };

        public RestrainedAnimalBaseSpecification()
        {
            this.families = this.familyByType[typeof(TRestrainedAnimal)];
        }

        public override Expression<Func<Animal, bool>> ToExpression() =>
            animal => this.families.Any(family => family == animal.FamilyId);

        protected override void OnAddRelation(AddRelationship<Animal> addRelationship)
        {
            base.OnAddRelation(addRelationship);
            var include = new Relationship<Animal>(animal => animal.AnimalEats);
            include.Then<AnimalEat>(animalEat => animalEat.Food);
            addRelationship(include);
        }
    }
}