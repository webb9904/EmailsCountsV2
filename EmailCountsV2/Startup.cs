namespace EmailCountsV2
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using CanonicalModels;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddNHibernate(connectionString);

            services.AddScoped<ICsvEmailTransformer, CsvEmailTransformer>();
            services.AddScoped<ICsvReaderHandler, CsvReaderHandler>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<DbEmail>, Repository<DbEmail>>();
            services.AddScoped<IRepository<Recipient>, Repository<Recipient>>();
            services.AddScoped<IRepository<Exclusion>, Repository<Exclusion>>();
            services.AddScoped<IRepository<Department>, Repository<Department>>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
