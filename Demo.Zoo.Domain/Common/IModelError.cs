namespace Demo.Zoo.Domain.Common
{
    using System;

    public interface IModelError
    {
        void Add(Error error);
    }
}