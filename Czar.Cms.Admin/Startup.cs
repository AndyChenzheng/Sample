using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexinea.Autofac.Extensions.DependencyInjection;
using Autofac;
using AutoMapper;
using Czar.Cms.Admin.Filter;
using Czar.Cms.Admin.Validation;
using Czar.Cms.Core.Options;
using Czar.Cms.IRepository;
using Czar.Cms.IServices;
using Czar.Cms.Repository.SqlServer;
using Czar.Cms.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Czar.Cms.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<DbOption>("CzarCms", Configuration.GetSection("DbOption"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Index";
                    options.LogoutPath = "/Account/Logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                //如果上面的cookiePolicyOptions中CheckConsentNeeded为true时，不重要的cookie会不发送，所以要加下面这句
                options.Cookie.IsEssential = true;

            });

            services.AddAntiforgery(options =>
            {
                
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "X-CSRF-TOKEN-andy";
                options.SuppressXFrameOptionsHeader = false;
            });

           // services.AddScoped<IMenuRepository, MenuRepository>();
           // services.AddScoped<IMenuService, MenuService>();


           services.AddMvc(option => { option.Filters.Add(new GlobalExceptionFilter()); })
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddControllersAsServices()
               .AddFluentValidation(fv =>
               {
                   //程序集方式引入
                   fv.RegisterValidatorsFromAssemblyContaining<LoginModelValidation>();
                   //去年其他的验证，只使用FluentValidation的验证规则
                   fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;

               });

            //DI了AutoMapper中需要用到的服务，其中包括AutoMapper的配置类Profile
            services.AddAutoMapper();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyTypes(typeof(ManagerRoleRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ManagerRoleService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            /*app.UseStatusCodePages(async ctx =>
            {
                ctx.HttpContext.Response.ContentType = "text/plain; charset=utf-8";
                await ctx.HttpContext.Response.WriteAsync($"状态代码页，错误代码是：{ctx.HttpContext.Response.StatusCode}");
            });*/

            //app.UseStatusCodePagesWithRedirects("/Home");
            app.UseStatusCodePagesWithReExecute("/Home");

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            //loggerFactory.AddNLog();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
