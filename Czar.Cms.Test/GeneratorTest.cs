using Czar.Cms.Core.CodeGenerator;
using Czar.Cms.Core.Models;
using Microsoft.Extensions.DependencyInjection;
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
    }
}