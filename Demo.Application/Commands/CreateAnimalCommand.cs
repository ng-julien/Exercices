namespace Demo.Application.Commands
{
    using System.Threading.Tasks;

    using Common;

    using Validators.Core;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.Common;
    
    public interface ICreateAnimalCommand<in TCreateAnimal, out TCreatedAnimal>
        where TCreateAnimal : AnimalCreating where TCreatedAnimal : AnimalDetails
    {
        Task CreateAsync(
            TCreateAnimal createAnimal,
            CreatedCallback<AnimalDetails> created,
            NotCreatedCallback<IModelError> notCreated);
    }
    
    internal class CreateAnimalCommand<TCreateAnimal, TCreatedAnimal> : ICreateAnimalCommand<TCreateAnimal, TCreatedAnimal>
        where TCreateAnimal : AnimalCreating
        where TCreatedAnimal : AnimalDetails
    {
        private readonly IAnimalDetailsAdapter<TCreatedAnimal> animalDetailsAdapter;

        private readonly IValidator<TCreateAnimal> validator;

        public CreateAnimalCommand(
            IValidator<TCreateAnimal> validator,
            IAnimalDetailsAdapter<TCreatedAnimal> animalDetailsAdapter)
        {
            this.validator = validator;
            this.animalDetailsAdapter = animalDetailsAdapter;
        }

        public async Task CreateAsync(
            TCreateAnimal createAnimal,
            CreatedCallback<AnimalDetails> created,
            NotCreatedCallback<IModelError> notCreated)
        {
            var createError = createAnimal.ToError();
            var valid = this.validator.Validate(createAnimal, createError.Add);
            if (!valid)
            {
                notCreated(createError);
                return;
            }

            var createdAnimal = await this.animalDetailsAdapter.CreateAsync(createAnimal);
            created(createdAnimal);
        }
    }
}