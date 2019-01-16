namespace Demo.Application.Commands
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Validators.Core;

    using Zoo.Domain;
    using Zoo.Domain.BearAggregate;

    public interface ICreateBearCommand
    {
        Task<BearDetails> CreateAsync(CreateBear createBear);
    }

    internal class CreateBearCommand : ICreateBearCommand
    {
        private readonly IValidator<CreateBear> validator;

        private readonly IBearDetailsAdapter bearDetailsAdapter;

        public CreateBearCommand(IValidator<CreateBear> validator, IBearDetailsAdapter bearDetailsAdapter)
        {
            this.validator = validator;
            this.bearDetailsAdapter = bearDetailsAdapter;
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