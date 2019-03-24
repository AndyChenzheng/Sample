using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Czar.Cms.Core.DbHelper;
using Czar.Cms.Core.Extensions;
using Czar.Cms.Core.Models;
using Czar.Cms.Core.Options;
using Microsoft.Extensions.Options;

namespace Czar.Cms.Core.CodeGenerator
{
    /// <summary>
    /// 根据数据库表以及表对应的列生成对应的数据库实体
    /// </summary>
    public class CodeGenerator
    {
        private readonly string Delimiter = "\\";//分隔符，默认为windows下的\\分隔符
        private static CodeGenerateOption _options;

        public CodeGenerator(IOptions<CodeGenerateOption> options)
        {
            if(options==null) throw new ArgumentNullException(nameof(options));
            _options = options.Value;
            if (_options.ConnectionString.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("必须指定数据库连接字符串");
            }

            if (_options.DbType.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("必须指定数据库类型");
            }

            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (_options.OutputPath.IsNullOrWhiteSpace())
            {
                _options.OutputPath = path;
            }

            var flag = path.IndexOf("/bin");
            if (flag > 0)
            {
                Delimiter = "/";
            }

        }

        /// <summary>
        /// 根据数据库连接字符串生成数据库表对应的模板代码
        /// </summary>
        /// <param name="isCoveredExsited">是否覆盖已存在的同名文件</param>
        public void GenerateTemplateCodesFromDatabase(bool isCoveredExsited = true)
        {
            DatabaseType dbType = ConnectionFactory.GetDataBaseType(_options.DbType);
            List<DbTable> tables=new List<DbTable>();
            using (var dbConnection=ConnectionFactory.CreateConnection(dbType,_options.ConnectionString))
            {
                tables = dbConnection.GetCurrentDatabaseTableList(dbType);
            }

            if (tables != null && tables.Any())
            {
                foreach (var table in tables)
                {
                    GenerateEntity(table,isCoveredExsited);
                    if (table.Columns.Any(c => c.IsPrimaryKey))
                    {
                        var pkTypeName = table.Columns.First(m => m.IsPrimaryKey).CSharpType;
                        GenerateIRepository(table,pkTypeName,isCoveredExsited);
                        GenerateRepository(table, pkTypeName, isCoveredExsited);
                    }

                    GenerateIServices(table, isCoveredExsited);
                    GenerateServices(table,isCoveredExsited);


                }
            }
        }

        private void GenerateIServices(DbTable table, bool ifExsitedConvered = true)
        {
            var iServicesPath = _options.OutputPath + "Czar.Cms.IServices";
            if (Directory.Exists(iServicesPath))
            {
                Directory.CreateDirectory(iServicesPath);
            }

            var fullPath = iServicesPath + Delimiter + "I" + table.TableName + "Services.cs";
            if (File.Exists(fullPath) && !ifExsitedConvered)
            {
                return;
            }

            var content = ReadTemplate("IServicesTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                .Replace("{Author}", _options.Author)
                .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{IServicesNamespace}", _options.IServicesNamespace)
                .Replace("{ModelName}", table.TableName);
            WriteAndSave(fullPath,content);
        }

        private void GenerateServices(DbTable table, bool ifExsitedCovered = true)
        {
            var repositoryPath = _options.OutputPath + "Czar.Cms.Services";
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }
            var fullPath = repositoryPath + Delimiter + table.TableName + "Service.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("ServiceTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                .Replace("{Author}", _options.Author)
                .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{ServicesNamespace}", _options.ServicesNamespace)
                .Replace("{ModelName}", table.TableName);
            WriteAndSave(fullPath, content);
        }

        private void GenerateIRepository(DbTable table,string keyTypeName,bool isExsitedCovered=true)
        {
            var iRepositoryPath = _options.OutputPath + "Czar.Cms.IRepository";// + Delimiter + "IRepository";
            if (!Directory.Exists(iRepositoryPath))
            {
                Directory.CreateDirectory(iRepositoryPath);
            }

            var fullPath = iRepositoryPath + Delimiter + "I" + table.TableName + "Repository.cs";
            if (File.Exists(fullPath) && !isExsitedCovered)
            {
                return;
            }

            var content = ReadTemplate("IRepositoryTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                .Replace("{Author}", _options.Author)
                .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{IRepositoryNamespace}", _options.IRepositoryNamespace)
                .Replace("{ModelName}", table.TableName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath,content);
        }

        private void GenerateRepository(DbTable table,string keyTypeName,bool isExsitedConvered=true)
        {
            var repositoryPath = _options.OutputPath + "Czar.Cms.Repository.SqlServer";
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }

            var fullPath = repositoryPath + Delimiter + table.TableName + "Repository.cs";
            if (File.Exists(fullPath) && !isExsitedConvered)
            {
                return;
            }

            var content = ReadTemplate("RepositoryTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                .Replace("{Author}", _options.Author)
                .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{RepositoryNamespace}", _options.RepositoryNamespace)
                .Replace("{ModelName}", table.TableName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath,content);
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="table"></param>
        /// <param name="isCoveredExsited"></param>

        private void GenerateEntity(DbTable table, bool isCoveredExsited = true)
        {
            var pkTypeName = table.Columns.First(m => m.IsPrimaryKey).CSharpType;
            var sb = new StringBuilder();
            foreach (var column in table.Columns)
            {
                var temp = GenerateEntityProperty(table.TableName, column);
                sb.AppendLine(temp);
            }
            GenerateModelpath(table,out string path,out string pathP);
            var content = ReadTemplate("ModelTemplate.txt");
            content = content.Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{ModelsNamespace}", _options.ModelsNamespace)
                .Replace("{Author}", _options.Author)
                .Replace("{Comment}", table.TableComment)
                .Replace("{ModelName}", table.TableName)
                .Replace("{ModelProperties}", sb.ToString());
            WriteAndSave(path,content);

            var contentp = ReadTemplate("ModelTemplate.txt");
            contentp = contentp.Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{ModelsNamespace}", _options.ModelsNamespace)
                .Replace("{Author}", _options.Author)
                .Replace("{Comment}", table.TableComment)
                .Replace("{ModelName}", table.TableName)
                .Replace("{ModelProperties}","");
            WriteAndSave(pathP, contentp);

        }

        private static string GenerateEntityProperty(string tableName, DbTableColumn column)
        {
            var sb=new StringBuilder();
            if (!string.IsNullOrWhiteSpace(column.Comment))
            {
                sb.AppendLine("\t\t/// <summary>");
                sb.AppendLine("\t\t/// " + column.Comment);
                sb.AppendLine("\t\t/// </summary>");
            }

            if (column.IsPrimaryKey)
            {
                sb.AppendLine("\t\t[Key]");
                sb.AppendLine($"\t\tpublic {column.CSharpType} Id" + "{get;set;}");
            }
            else
            {
                if (!column.IsNullable)
                {
                    sb.AppendLine("\t\t[Required]");
                }

                if (column.ColumnLength.HasValue && column.ColumnLength.Value > 0)
                {
                    sb.AppendLine($"\t\t[MaxLength({column.ColumnLength.Value})]");
                }

                var colType = column.CSharpType;
                if (colType.ToLower() != "string" && colType.ToLower() != "byte[]" && colType.ToLower() != "object" &&
                    column.IsNullable)
                {
                    colType = colType + "?";
                }

                sb.AppendLine($"\t\tpublic {colType} {column.ColName} " + "{get;set;}");
            }

            return sb.ToString();
        }

        private void GenerateModelpath(DbTable table, out string path, out string pathP)
        {
            var modelPath = _options.OutputPath+ "Czar.Cms.Models" + Delimiter + "Models";
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }
            StringBuilder fullPath=new StringBuilder();
            fullPath.Append(modelPath);
            fullPath.Append(Delimiter);
            fullPath.Append("Partial");
            if (!Directory.Exists(fullPath.ToString()))
            {
                Directory.CreateDirectory(fullPath.ToString());
            }

            fullPath.Append(Delimiter);
            fullPath.Append(table.TableName);
            fullPath.Append(".cs");
            pathP = fullPath.ToString();
            path = fullPath.Replace("Partial" + Delimiter, "").ToString();
        }

        private string ReadTemplate(string templateName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var content = string.Empty;
            using (var stream=currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.CodeTemplate.{templateName}"))
            {
                if (stream != null)
                {
                    using (var reader=new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }

            return content;
        }


        private static void WriteAndSave(string fileName, string content)
        {
            using (var fs=new FileStream(fileName,FileMode.Create,FileAccess.Write))
            {
                using (var sw=new StreamWriter(fs))
                {
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
        }















    }
}