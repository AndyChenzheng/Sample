using System;
using System.Collections.Generic;
using Czar.Cms.Core.Models;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using Dapper;

namespace Czar.Cms.Core.Extensions
{
    /// <summary>
    /// IDbConnection扩展方法
    /// </summary>
    public static class IDbConnectionExtensions
    {

        public static List<DbTable> GetCurrentDatabaseTableList(this IDbConnection dbConnection,DatabaseType dbType)
        {
            List<DbTable> tables = dbConnection.GetCurrentDatabaseAllTables(dbType);
            tables.ForEach(item =>
            {
                item.Columns = dbConnection.GetColumnsByTableName(dbType, item.TableName);
                item.Columns.ForEach(x =>
                {
                    var csharpType = DbColumnTypeCollection.DbColumnDataTypes.FirstOrDefault(t =>
                        t.DatabaseType == dbType && t.ColumnTypes.Split(',').Any(p =>
                            p.Trim().Equals(x.ColumnType, StringComparison.OrdinalIgnoreCase)))?.CSharpType;
                    if (string.IsNullOrWhiteSpace(csharpType))
                    {
                        throw new SqlTypeException($"未从字典中找到\"{x.ColumnType}\"对应的C#数据类型，请更新DbColumnTypeCollection类型映射字典。");
                    }

                    x.CSharpType = csharpType;
                });
            });

            return tables;
        }

        /// <summary>
        /// 根据数据库的类型获取数据库中所有的表
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static List<DbTable> GetCurrentDatabaseAllTables(this IDbConnection dbConnection,DatabaseType dbType)
        {
            if (dbConnection == null) throw new ArgumentNullException(nameof(dbConnection));
                if(dbConnection.State==ConnectionState.Closed) dbConnection.Open();
                return dbConnection.Query<DbTable>(dbConnection.strGetAllTablesSql(dbType)).ToList();
           
        }

        /// <summary>
        /// 根据表名，获取表所有的列
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbType"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static List<DbTableColumn> GetColumnsByTableName(this IDbConnection dbConnection, DatabaseType dbType,
            string tableName)
        {
            if(dbConnection==null) throw new ArgumentNullException(nameof(dbConnection));
            if(dbConnection.State==ConnectionState.Closed) dbConnection.Open();
            
            return dbConnection.Query<DbTableColumn>(dbConnection.strGetAllColumnsSql(dbType,tableName)).ToList();
        }


        private static string strGetAllTablesSql(this IDbConnection dbConnection, DatabaseType dbType)
        {
            string strGetAllTables = string.Empty;
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    strGetAllTables = @"SELECT DISTINCT d.name as TableName, f.value as TableComment
FROM      sys.syscolumns AS a LEFT OUTER JOIN
                sys.systypes AS b ON a.xusertype = b.xusertype INNER JOIN
                sys.sysobjects AS d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties' LEFT OUTER JOIN
                sys.syscomments AS e ON a.cdefault = e.id LEFT OUTER JOIN
                sys.extended_properties AS g ON a.id = g.major_id AND a.colid = g.minor_id LEFT OUTER JOIN
                sys.extended_properties AS f ON d.id = f.major_id AND f.minor_id = 0";
                    break;
                case DatabaseType.MySQL:
                    strGetAllTables = "SELECT TABLE_NAME as TableName," +
                                      " Table_Comment as TableComment" +
                                      " FROM INFORMATION_SCHEMA.TABLES" +
                                      $" where TABLE_SCHEMA = '{dbConnection.Database}'";
                    break;
                default:
                    throw new ArgumentNullException($"还不支持{dbType.ToString()}类型");

            }

            return strGetAllTables;
        }

        private static string strGetAllColumnsSql(this IDbConnection dbConnection, DatabaseType dbType,
            string tableName)
        {
            var strGetTableColumns = string.Empty;
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    strGetTableColumns = $@"SELECT   a.name AS ColName, CONVERT(bit, (CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') 
                = 1 THEN 1 ELSE 0 END)) AS IsIdentity, CONVERT(bit, (CASE WHEN
                    (SELECT   COUNT(*)
                     FROM      sysobjects
                     WHERE   (name IN
                                         (SELECT   name
                                          FROM      sysindexes
                                          WHERE   (id = a.id) AND (indid IN
                                                              (SELECT   indid
                                                               FROM      sysindexkeys
                                                               WHERE   (id = a.id) AND (colid IN
                                                                                   (SELECT   colid
                                                                                    FROM      syscolumns
                                                                                    WHERE   (id = a.id) AND (name = a.name))))))) AND (xtype = 'PK')) 
                > 0 THEN 1 ELSE 0 END)) AS IsPrimaryKey, b.name AS ColumnType, COLUMNPROPERTY(a.id, a.name, 'PRECISION') 
                AS ColumnLength, CONVERT(bit, (CASE WHEN a.isnullable = 1 THEN 1 ELSE 0 END)) AS IsNullable, ISNULL(e.text, '') 
                AS DefaultValue, ISNULL(g.value, ' ') AS Comment
FROM      sys.syscolumns AS a LEFT OUTER JOIN
                sys.systypes AS b ON a.xtype = b.xusertype INNER JOIN
                sys.sysobjects AS d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties' LEFT OUTER JOIN
                sys.syscomments AS e ON a.cdefault = e.id LEFT OUTER JOIN
                sys.extended_properties AS g ON a.id = g.major_id AND a.colid = g.minor_id LEFT OUTER JOIN
                sys.extended_properties AS f ON d.id = f.class AND f.minor_id = 0
WHERE   (b.name IS NOT NULL) AND (d.name = '{tableName}')
ORDER BY a.id, a.colorder";
                    break;
                case DatabaseType.MySQL:
                    strGetTableColumns =
                   "select column_name as ColName, " +
                   " column_default as DefaultValue," +
                   " IF(extra = 'auto_increment','TRUE','FALSE') as IsIdentity," +
                   " IF(is_nullable = 'YES','TRUE','FALSE') as IsNullable," +
                   " DATA_TYPE as ColumnType," +
                   " CHARACTER_MAXIMUM_LENGTH as ColumnLength," +
                   " IF(COLUMN_KEY = 'PRI','TRUE','FALSE') as IsPrimaryKey," +
                   " COLUMN_COMMENT as Comment " +
                   $" from information_schema.columns where table_schema = '{dbConnection.Database}' and table_name = '{tableName}'";
                    break;
                default:
                    throw new ArgumentNullException($"还不支持{dbType.ToString()}数据库");

            }

            return strGetTableColumns;
        }


    }

   
}