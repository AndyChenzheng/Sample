using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Czar.Cms.Core.CodeGenerator;
using Czar.Cms.Core.Models;
using Czar.Cms.Core.Options;
using Czar.Cms.IRepository;
using Czar.Cms.Repository.SqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Czar.Cms.Test
{
    /// <summary>
    /// 构造依赖注入容器，然后传入参数
    /// </summary>
    public class Common
    {
        //private static IHostingEnvironment _hostingEnvironment;
      
        public static IServiceProvider BuildServiceForSqlServer()
        {
            var services = new ServiceCollection();
            services.Configure<CodeGenerateOption>(options =>
                {
                    options.ConnectionString = "server=.;database=CMS;uid=sa;pwd=sa.;Max Pool Size=200;";
                    options.DbType = DatabaseType.SqlServer.ToString();
                    options.Author = "andy chen";
                    options.OutputPath = @"D:\Demo\NetCore\Sample01\";
                    options.ModelsNamespace = "Czar.Cms.Models";
                    options.IRepositoryNamespace = "Czar.Cms.IRepository";
                    options.RepositoryNamespace = "Czar.Cms.Repository.SqlServer";
                    options.IServicesNamespace = "Czar.Cms.IServices";
                    options.ServicesNamespace = "Czar.Cms.Services";
                });
            services.Configure<DbOption>("CzarCms", GetConfiguration().GetSection("DbOption"));
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<CodeGenerator>();
            //_hostingEnvironment = services.BuildServiceProvider().GetService<IHostingEnvironment>();
           

            return services.BuildServiceProvider();//构建服务提供程序
        }


        public static IConfiguration GetConfiguration()
        {
            var path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp2.2"," ");
            var builer = new ConfigurationBuilder()
                //.SetBasePath(AppContext.BaseDirectory)
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builer.Build();
        }
    }
}