using Core.Repository;
using Core.Service;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebShipPort.Factory;
using WebShipPort.HostedServices;

namespace WebApplication123
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder.WithOrigins("https://localhost:44326")
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod();
            //            //builder.WithOrigins("www.shipportme.com")
            //            //                    .AllowAnyHeader()
            //            //                    .AllowAnyMethod();
            //        });
            //});
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:18093", "http://localhost:3000", "https://localhost:44326")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });

            services.AddTransient<Database>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<IWarehouseClerkRepository, WarehouseClerkRepository>();
            services.AddTransient<ICrewRepository, CrewRepository>();
            services.AddTransient<IShipCaptainRepository, ShipCaptainRepository>();
            services.AddTransient<ITransportRepository, TransportRepository>();
            services.AddTransient<IShipPortRepository, ShipPortRepository>();
            services.AddTransient<IShipRepository, ShipRepository>();

            services.AddTransient<WarehouseService>();
            services.AddTransient<WarehouseClerkService>();
            services.AddTransient<CrewService>();
            services.AddTransient<ShipCaptainService>();
            services.AddTransient<TransportService>();
            services.AddTransient<ShipPortService>();
            services.AddTransient<ShipService>();

            services.AddTransient<TransportFactory>();
            services.AddTransient<ShipFactory>();
            services.AddTransient<ShipPortFactory>();

            services.AddHostedService<TimedHostedService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}").RequireCors(MyAllowSpecificOrigins);
            });
        }
    }
}
