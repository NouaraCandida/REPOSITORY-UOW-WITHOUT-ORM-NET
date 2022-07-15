using DIP.Core.Repository;
using DIP.Core.Validate;
using DIP.Sensor.Domain._Util;
using DIP.Sensor.Domain.Repositorys;
using DIP.Sensor.Infra.Postgres;
using DIP.Sensor.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DIP.Sensor.API
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

            //Log
            Serilog.Core.Logger serilog = new LoggerConfiguration()
              .ReadFrom.Configuration(Configuration)
              .CreateLogger();

            var loggerFactory = new LoggerFactory()
                .AddSerilog(serilog);

            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("SensorLogger");
            services.AddSingleton(logger);

            /////Register DIP/////
            //Util
            services.AddSingleton<IStringLocalizer, StringProvider>();
            services.AddSingleton<IValidate, Validate>();
            services.AddScoped<IOptionsSearch, OptionsSearch>();
            //DIP Operations
            services.AddScoped<DbSession>();
            services.AddScoped<IUoWApplication, UoWApplication>();

           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
