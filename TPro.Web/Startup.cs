using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System;
using TPro.Business.IServiceProvider;
using TPro.Business.ServiceProvider;
using TPro.Business.Admin.IServiceProvider;
using TPro.Business.Admin.ServiceProvider;
using Mapster;
using MapsterMapper;
using TPro.Models.MapsterConfig;
using Microsoft.Extensions.FileProviders;

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

            #region ����

            services.AddCors(options =>
            {
                #region ���Կ���--�������п���

                options.AddPolicy("CorsSample", p => p.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());

                #endregion ���Կ���--�������п���
            });

            #endregion ����

            services.AddSingleton(new MapperService());

            #region ����ע��
            //Admin
            services.AddTransient<IDbService, DbService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ISysManageService, SysManageService>();
            //default
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();

            #endregion ����ע��


            #region ע��Swagger����

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("API", new OpenApiInfo { Version = "V1", Title = "API", Description = "ASP.NET Core Web API", });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion ע��Swagger����

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);//Ĭ������
                c.DefaultModelExpandDepth(-1);
            });

            #endregion SwaggerUI

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider=new PhysicalFileProvider("F:\\staticdir"),
                RequestPath="/staticdir"
            });
            app.UseRouting();
            app.UseCors("CorsSample");//���ÿ���
            app.UseAuthentication();
            app.UseAuthorization();

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
            });
        }
    }
}