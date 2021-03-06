﻿namespace Demo.Infrastructure.Specifications.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public sealed class Relationship<TEntity>
    {
        public Relationship(Expression<Func<TEntity, object>> root)
        {
            this.Root = root;
        }

        public static Relationship<TEntity> Create(Expression<Func<TEntity, object>> root)
        {
            var relationship = new Relationship<TEntity>(root);
            return relationship;
        }

        public Expression<Func<TEntity, object>> Root { get; }

        public List<Expression<Func<object, object>>> Children { get; } = new List<Expression<Func<object, object>>>();

        public Relationship<TEntity> Then<TProperty>(Expression<Func<TProperty, object>> navigationPropertyPath)
        {
            var expression = Convert(navigationPropertyPath);
            this.Children.Add(expression);
            return this;
        }

        private static Expression<Func<object, object>> Convert<TProperty>(Expression<Func<TProperty, object>> include)
        {
            var parameter = Expression.Parameter(typeof(object));
            var expression = Expression.Lambda<Func<object, object>>(
                Expression.Invoke(include, Expression.Convert(parameter, typeof(TProperty))),
                parameter);
            return expression;
        }
    }
}