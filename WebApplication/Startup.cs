using Core.Repository;
using Core.Service;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebShipPort.Factory;

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

            services.AddScoped<Database>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IWarehouseClerkRepository, WarehouseClerkRepository>();
            services.AddScoped<ICrewRepository, CrewRepository>();
            services.AddScoped<IShipCaptainRepository, ShipCaptainRepository>();
            services.AddScoped<ITransportRepository, TransportRepository>();
            services.AddScoped<IShipPortRepository, ShipPortRepository>();
            services.AddScoped<IShipRepository, ShipRepository>();

            services.AddScoped<WarehouseService>();
            services.AddScoped<WarehouseClerkService>();
            services.AddScoped<CrewService>();
            services.AddScoped<ShipCaptainService>();
            services.AddScoped<TransportService>();
            services.AddScoped<ShipPortService>();
            services.AddScoped<ShipService>();

            services.AddScoped<TransportFactory>();
            services.AddScoped<ShipFactory>();
            services.AddScoped<ShipPortFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    //endpoints.MapGet("/", async context =>
            //    //{
            //    //    await context.Response.WriteAsync("Hello World!");
            //    //});
            //    endpoints.MapControllers();
            //});

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
