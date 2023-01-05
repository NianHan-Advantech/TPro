using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using TPro.Business.Admin.IServiceProvider;
using TPro.Business.Admin.ServiceProvider;
using TPro.Business.IServiceProvider;
using TPro.Business.ServiceProvider;
using TPro.Common.Cache;
using TPro.Common.CustomLog;
using TPro.Models.AutoMapperConfig;
using TPro.Web.Configs;

namespace TPro.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddControllers();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });


            #region Hangfire

            services.AddHangfire(CustomConfigs.HangfireConfig);

            #endregion Hangfire

            #region øÁ”Ú

            services.AddCors(options =>
            {
                #region ≤‚ ‘øÁ”Ú--‘ –ÌÀ˘”–øÁ”Ú

                options.AddPolicy(CustomConfigs.CorsSampleCorsPolicy, p => p.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());

                #endregion ≤‚ ‘øÁ”Ú--‘ –ÌÀ˘”–øÁ”Ú
            });

            #endregion øÁ”Ú

            #region “¿¿µ◊¢»Î

            //Admin
            services.AddTransient<IDbService, DbService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ISysManageService, SysManageService>();
            //default
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddScoped<MemoryCacheHelper>();

            #endregion “¿¿µ◊¢»Î

            #region ◊¢≤·Swagger∑˛ŒÒ

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("API", new OpenApiInfo { Version = "V1", Title = "API", Description = "ASP.NET Core Web API", });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion ◊¢≤·Swagger∑˛ŒÒ

            #region Cookie

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
                {
                    config.Cookie.Name = "CookieName";
                    config.LoginPath = "/TPro/Login";
                });
            services.AddAuthorization(config =>
            {
                var builder = new AuthorizationPolicyBuilder();
                var defaultPolicy = builder.RequireAuthenticatedUser().Build();
                config.DefaultPolicy = defaultPolicy;
            });

            #endregion Cookie

            #region AutoMapper

            services.AddAutoMapper(config => config.AddProfile<MapperConfig>());

            #endregion AutoMapper
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #region SwaggerUI

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/API/swagger.json", "API");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);//ƒ¨»œ ’∆
                c.DefaultModelExpandDepth(-1);
            });

            #endregion SwaggerUI
            
            loggerFactory.AddProvider(CustomConfigs.logProvider);
            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider("F:\\staticdir"),
                RequestPath = "/staticdir"
            });
 
            app.UseRouting();
            app.UseCors(CustomConfigs.CorsSampleCorsPolicy);//∆Ù”√øÁ”Ú
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                //Authorization = new[] { new MyHangfireFilter() }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    name: "Api",
                    areaName: "ApiControllers",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapHangfireDashboard();
            });
        }
    }
}