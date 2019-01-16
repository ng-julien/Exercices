namespace Demo.Application.Validators
{
    using System.Collections.Generic;
    using System.Linq;

    using Core;

    using FluentValidation;

    using Zoo.Domain.BearAggregate;

    internal class CreateBearValidator : Validator<CreateBear>
    {
        private readonly IFoodsBearAdapter foodsBearAdapter;

        public CreateBearValidator(IFoodsBearAdapter foodsBearAdapter)
        {
            this.foodsBearAdapter = foodsBearAdapter;
        }
        protected override void OnDefineRules()
        {
            this.RuleFor(bear => bear.Name).NotEmpty();
            this.RuleFor(bear => bear.Foods).Must(this.BePartOfHisDiet);
            this.RuleFor(bear => bear.Legs).Must(legs => legs >= 3);
        }

        private bool BePartOfHisDiet(ICollection<int> foods)
        {
            var canBeEatFoods = this.foodsBearAdapter.CanBeEat().Select(food => food.Code);
            var authorizedFoods = canBeEatFoods.Intersect(foods);
            return authorizedFoods.Count() == foods.Count;
        }
    }
}