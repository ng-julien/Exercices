namespace Demo.Infrastructure.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Zoo.Domain.AnimalAggregate;
    using Zoo.Domain.BearAggregate;
    using Zoo.Domain.Common;
    using Zoo.Domain.GiraffeAggregate;
    using Zoo.Domain.LionAggregate;

    internal static class FamilyCode
    {
        public const int Bear = (int)FamilyType.Bears;

        public const int Giraffe = (int)FamilyType.Giraffes;

        public const int Lion = (int)FamilyType.Lions;
        
        public static int[] All => typeof(FamilyCode).GetFields().Select(x => (int)x.GetValue(null)).ToArray();

        public static int[] Get<TAnimal>() where TAnimal : class
        {
            var type = typeof(TAnimal);
            var result = new List<int>();
            if (type == typeof(BearRestrained) || type == typeof(BearDetails) || type == typeof(BearCreating))
            {
                result.Add(Bear);
            }
            else if (type == typeof(GiraffeRestrained) || type == typeof(GiraffeDetails))
            {
                result.Add(Giraffe);
            }
            else if (type == typeof(LionRestrained) || type == typeof(LionDetails))
            {
                result.Add(Lion);
            }
            else
            {
                result.AddRange(All);
            }

            return result.ToArray();
        }
    }
}