namespace Demo.Application.Validators.Core
{
    using FluentValidation;

    internal interface IValidator<in T>
    {
        void Validate(T value);
    }

    internal abstract class Validator<T> : AbstractValidator<T>, IValidator<T>
    {
        protected Validator()
        {
            this.DefineRules();
        }

        void IValidator<T>.Validate(T value)
        {
            var validationResult = this.Validate(value);
            if (validationResult.IsValid)
            {
                return;
            }

            throw new ValidationException($"L'animal {typeof(T).Name} n'est pas valide", validationResult.Errors);
        }

        protected abstract void OnDefineRules();

        private void DefineRules()
        {
            this.OnDefineRules();
        }
    }
}