using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Data;
using WebApi.Extensions;
using WebApi.Helpers;
using WebApi.Interface;

namespace WebApi
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


            services.AddDbContext<HousingDbContext>
                            (option=>option.UseSqlServer
                            (Configuration.GetConnectionString("Default")));
                         
            services.AddControllers();
            services.AddAutoMapper(typeof(AutomapperProfile).Assembly);
            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            var secretKey=Configuration.GetSection("AppSettings:Key").Value;

            var Key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt=>{
                        opt.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuerSigningKey=true,
                            ValidateIssuer=false,
                            ValidateAudience=false,
                            IssuerSigningKey= Key
                        };
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.ConfigureExceptionHandler(env);
            app.UseRouting();

            app.UseCors(m=>m.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod());
            app.UseAuthentication();                

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
