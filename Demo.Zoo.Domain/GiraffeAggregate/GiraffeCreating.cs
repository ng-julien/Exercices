namespace Demo.Zoo.Domain.GiraffeAggregate
{
    using Common;

    public class GiraffeCreating : AnimalCreating
    {
        public override IModelError ToError()
        {
            return new GiraffeCreatingError(this);
        }
    }
}