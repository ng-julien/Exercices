namespace Demo.Infrastructure.Repositories.Entities
{
    using System;

    public partial class DemoContextFactory
    {
        private static readonly Lazy<DemoContextFactory> lazyInstance =
            new Lazy<DemoContextFactory>(() => new DemoContextFactory());

        private DemoContextFactory()
        {
        }

        public static string ConnectionString { get; set; }

        public static DemoContextFactory Instance => lazyInstance.Value;
    }
}