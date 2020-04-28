namespace EmailCountsV2
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Maps;
    using Microsoft.Extensions.DependencyInjection;
    using NHibernate;

    public static class NHibernateExtensions
    {
        private static ISessionFactory _sessionFactory;

        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DbEmailMap>()
                .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                .ExposeConfiguration(cfg => cfg.SetProperty("adonet.batch", "1"))
                .BuildSessionFactory();

            services.AddSingleton(_sessionFactory);
            services.AddScoped(factory => _sessionFactory.OpenSession());

            return services;
        }

    }
}
