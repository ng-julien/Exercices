namespace Demo.Application.Validators.Core
{
    using System;
    using System.Linq;

    using FluentValidation;

    using Zoo.Domain;

    internal interface IValidator<in T>
    {
        bool Validate(T value, Action<Error> addError);
    }

    internal abstract class Validator<T> : AbstractValidator<T>, IValidator<T>
    {
        protected Validator()
        {
            this.DefineRules();
        }

        public bool Validate(T value, Action<Error> addError)
        {
            var validationResult = base.Validate(value);
            validationResult.Errors.ToList().ForEach(
                failure =>
                    {
                        var error = new Error(failure.PropertyName, failure.ErrorMessage);
                        addError(error);
                    });

            return validationResult.IsValid;
        }

        protected abstract void OnDefineRules();

        private void DefineRules()
        {
            this.OnDefineRules();
        }
    }
}