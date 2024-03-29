using Core.Repository;
using Core.Service;
using DataLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using WebShipPort.Factory;
using WebShipPort.HostedServices;

namespace WebApplication123
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            services.AddMvc();
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

            services.AddDbContext<Database>();
            //services.AddScoped<Database>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IWarehouseClerkRepository, WarehouseClerkRepository>();
            services.AddScoped<ICrewRepository, CrewRepository>();
            services.AddScoped<IShipCaptainRepository, ShipCaptainRepository>();
            services.AddScoped<ITransportRepository, TransportRepository>();
            services.AddScoped<IShipPortRepository, ShipPortRepository>();
            services.AddScoped<IShipRepository, ShipRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<WarehouseService>();
            services.AddTransient<WarehouseClerkService>();
            services.AddTransient<CrewService>();
            services.AddTransient<ShipCaptainService>();
            services.AddTransient<TransportService>();
            services.AddTransient<ShipPortService>();
            services.AddTransient<ShipService>();
            services.AddTransient<CargoService>();
            services.AddTransient<UserService>();

            services.AddTransient<TransportFactory>();
            services.AddTransient<ShipFactory>();
            services.AddTransient<ShipPortFactory>();
            services.AddTransient<WarehouseFactory>();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}").RequireCors(MyAllowSpecificOrigins);
            });

        }
    }
}
