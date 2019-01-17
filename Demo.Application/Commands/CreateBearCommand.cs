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
        private readonly CreateBearValidator validator;

        private readonly BearDetailsAdapter bearDetailsAdapter;

        public CreateBearCommand()
        {
            this.validator = new CreateBearValidator();
            this.bearDetailsAdapter = new BearDetailsAdapter();
        }

        public async Task<BearDetails> CreateAsync(CreateBear createBear)
        {
            var errors = new List<Error>();
            var valid = this.validator.Validate(createBear, errors.Add);
            if (!valid)
            {
                return new CreateBearError(createBear, errors);
            }

            var createdBear = await this.bearDetailsAdapter.CreateAsync(createBear);
            return createdBear;
        }
    }
}