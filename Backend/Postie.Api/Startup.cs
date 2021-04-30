using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Postie.Api.Data;
using Postie.Api.Mappers;
using Postie.Api.Repositories;
using Postie.Api.Services;

namespace Postie.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddMvc();

            services.AddCors(options => {
                options.AddPolicy("AllowedOrigins", builder => {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.Authority = "https://dev-q8zwjq1z.us.auth0.com/";
                options.Audience = "postie";
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Postie",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Source: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<IFetchPostService, FetchPostService>();
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IUrlService, UrlService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<PostResponseMapper>();
            services.AddSingleton<BoardResponseMapper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("AllowedOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Postie");
            });

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
