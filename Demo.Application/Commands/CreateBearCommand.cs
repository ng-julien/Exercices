namespace Demo.Application.Commands
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Infrastructure.Adapters;

    using Validators;

    using Zoo.Domain;
    using Zoo.Domain.BearAggregate;

    public class CreateBearCommand
    {
        public async Task<BearDetails> CreateAsync(CreateBear createBear)
        {
            var validator = new CreateBearValidator();
            var bearDetailsAdapter = new BearDetailsAdapter();
            var errors = new List<Error>();
            var valid = validator.Validate(createBear, errors.Add);
            if (!valid)
            {
                return new CreateBearError(createBear, errors);
            }

            var createdBear = await bearDetailsAdapter.CreateAsync(createBear);
            return createdBear;
        }
    }
}