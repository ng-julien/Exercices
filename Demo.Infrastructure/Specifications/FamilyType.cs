namespace Demo.Infrastructure.Specifications
{
    using System.Linq;

    public enum FamilyType
    {
        All = 0,

        Giraffes = FamilyCode.Giraffe,

        Lions = FamilyCode.Lion,

        Bears = FamilyCode.Bear
    }

    internal static class FamilyCode
    {
        public const int Bear = 3;

        public const int Giraffe = 1;

        public const int Lion = 2;

        public static int[] All => typeof(FamilyCode).GetFields().Select(x => (int)x.GetValue(null)).ToArray();
    }
}