namespace Demo.Zoo.Domain.Referentials
{
    using System.Collections.Generic;

    public interface IFamilyAdapter
    {
        IReadOnlyList<Referential> Get();
    }
}