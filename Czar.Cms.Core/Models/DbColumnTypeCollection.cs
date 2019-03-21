using System.Collections.Generic;

namespace Czar.Cms.Core.Models
{
    /// <summary>
    /// 数据库字段类型集合
    /// </summary>
    public class DbColumnTypeCollection
    {
        public static IList<DbColumnDataType> DbColumnDataTypes=>new List<DbColumnDataType>()
        {
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "bigint",CSharpType = "Int64"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "binary,image,varbinary(max),rowversion,timestamp,varbinary",CSharpType = "Byte[]"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "bit",CSharpType = "Boolean"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "char,nchar,text,ntext,varchar,nvarchar",CSharpType = "String"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "date,datetime,datetime2,smalldatetime",CSharpType = "DateTime"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "datetimeoffset",CSharpType = "DateTimeOffset"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "decimal,money,numeric,smallmoney",CSharpType = "Decimal"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "float",CSharpType = "Double"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "int",CSharpType = "Int32"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "real",CSharpType = "Single"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "smallint",CSharpType = "Int16"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "sql_variant",CSharpType = "Object"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "time",CSharpType = "TimeSpan"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "tinyint",CSharpType = "Byte"},
            new DbColumnDataType(){DatabaseType = DatabaseType.SqlServer,ColumnTypes = "uniqueidentifier",CSharpType = "Guid"},


        };
    }
}