namespace Demo.Infrastructure.Specifications
{
    using System.Linq;

    using Zoo.Domain.Common;

    internal static class FamilyCode
    {
        public const int Bear = (int)FamilyType.Bears;

        public const int Giraffe = (int)FamilyType.Giraffes;

        public const int Lion = (int)FamilyType.Lions;

        public static int[] All => typeof(FamilyCode).GetFields().Select(x => (int)x.GetValue(null)).ToArray();
    }
}