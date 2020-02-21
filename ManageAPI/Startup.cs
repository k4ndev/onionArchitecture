using AutoMapper;
using Core;
using Core.Services.Data;
using Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using ManageAPI.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Services.Data;
using System.Text;

namespace ManageAPI
{
    public class Startup
    {
        public  Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=KAMRAN;Database=MusicApp;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddCors();
            services.AddControllers();


            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<MusicAppContext>(options =>
                  options.UseSqlServer(connection,
                  x => x.MigrationsAssembly("Data")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<IUserService, UserService>();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value));
            services.AddAuthentication(x=> {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddMvc(setup =>
            {
                //setup.EnableEndpointRouting = false;
                
            }).AddFluentValidation();

            services.AddTransient<IValidator<ArtistCreateDto>, ArtistCreateDtoValidator>();
           
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
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMvc();
        }
    }
}
