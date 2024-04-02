using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Postie.Api.Data;
using Postie.Api.Mappers;
using Postie.Api.Models.Db;
using Postie.Api.Models.Options;
using Postie.Api.Repositories.Interfaces;
using Postie.Api.Repositories;
using Postie.Api.Services;
using System.IO;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace Postie.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationBuilder();
            
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddMvc();
            services.AddOptions();

            services.AddCors(options => {
                options.AddPolicy("AllowedOrigins", builder =>
                {
                    builder.WithOrigins("https://localhost:8081")
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .AllowAnyMethod();
                });
            });

            services
                .AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Postie",
                    Version = "v1"
                });

                // Source: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IUrlService, UrlService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IPostVotesRepository, PostVotesRepository>();

            // Mappers
            services.AddTransient<PostResponseMapper>();
            services.AddSingleton<BoardResponseMapper>();
            services.AddSingleton<CommentResponseMapper>();
            services.AddSingleton<UserResponseMapper>();

            services.AddSingleton(o => Configuration.GetSection("SeedData").Get<SeedDataOptions>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.SeedDatabase(env);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCors("AllowedOrigins");

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints
                    .MapGroup("/account")
                    .MapIdentityApi<User>();
            });

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
