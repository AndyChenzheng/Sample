using Czar.Cms.Core.CodeGenerator;
using Czar.Cms.Core.Models;
using Czar.Cms.IRepository;
using Czar.Cms.Models;
using Czar.Cms.Repository.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Czar.Cms.Test
{
    public class GeneratorTest
    {
        
        [Fact(DisplayName = "根据表生成实体类")]
        public void GeneratorModelForSqlServer()
        {
            var serviceProvider = Common.BuildServiceForSqlServer();
            var codeGenerator = serviceProvider.GetRequiredService<CodeGenerator>();
            codeGenerator.GenerateTemplateCodesFromDatabase(true);
            Assert.Equal("SqlServer", DatabaseType.SqlServer.ToString(),ignoreCase:true);

        }

        [Fact(DisplayName = "测试实体类的仓储")]
        public void TestArticleCategory()
        {
            var serviceProvider = Common.BuildServiceForSqlServer();
            var articleRepository = serviceProvider.GetRequiredService<IArticleCategoryRepository>();
            var articleCategory = new ArticleCategory()
            {
                ClassLayer = 1,
                Title = "测试数据",
                ClassList = "a,b,c",
                ImageUrl = "/img/img.jpg",
                IsDeleted = false,
                ParentId = 1,
                SeoDescription = "SEO描述",
                SeoKeywords = "SEO关健字",
                SeoTitle = "SEO标题",
                Sort = 1
            };
            articleRepository.Insert(articleCategory);
            var rtnList = articleRepository.GetList().ToList();
            Assert.Single(rtnList);
            var firstAc = rtnList.FirstOrDefault();
            Assert.Equal("SEO标题", firstAc.SeoTitle);
        }
    }
}