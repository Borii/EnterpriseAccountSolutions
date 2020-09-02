using AccountApi.Core;
using AccountApi.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AccountApi.Core.Services;

namespace AccountApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient<ICustomerClient, CustomerClient>(
                configureClient => configureClient.BaseAddress = new Uri(Configuration.GetSection("Services:CustomerAPI").Value));
            services.AddHttpClient<ITransactionClient, TransactionClient>(
                configureClient => configureClient.BaseAddress = new Uri(Configuration.GetSection("Services:TransactionAPI").Value));

            services.AddDbContext<AccountContext>(options => options.UseSqlite(Configuration.GetSection("ConnectionStrings:SqliteConnection").Value));

            // call seeder

            services.AddTransient<IAccountDataAccess, AccountDataAccess>();
            services.AddTransient<IAccountCore, AccountCore>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.WithOrigins(Configuration.GetSection("Cors:Url").Value)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
