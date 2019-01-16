namespace Demo.Application.Commands
{
    using System.Threading.Tasks;

    using Validators.Core;

    using Zoo.Domain.BearAggregate;

    public interface ICreateBearCommand
    {
        Task<BearDetails> CreateAsync(CreateBear createBear);
    }

    internal class CreateBearCommand : ICreateBearCommand
    {
        private readonly IBearDetailsAdapter bearDetailsAdapter;

        private readonly IValidator<CreateBear> validator;

        public CreateBearCommand(IValidator<CreateBear> validator, IBearDetailsAdapter bearDetailsAdapter)
        {
            this.validator = validator;
            this.bearDetailsAdapter = bearDetailsAdapter;
        }

        public async Task<BearDetails> CreateAsync(CreateBear createBear)
        {
            this.validator.Validate(createBear);
            var createdBear = await this.bearDetailsAdapter.CreateAsync(createBear);
            return createdBear;
        }
    }
}