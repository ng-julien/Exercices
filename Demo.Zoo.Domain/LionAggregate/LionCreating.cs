namespace Demo.Zoo.Domain.LionAggregate
{
    using Common;

    public class LionCreating : AnimalCreating
    {
        public override IModelError ToError()
        {
            return new LionCreatingError(this);
        }
    }
}