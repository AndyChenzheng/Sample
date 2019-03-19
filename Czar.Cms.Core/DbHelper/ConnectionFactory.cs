using System;
using System.Data;
using System.Data.SqlClient;
using Czar.Cms.Core.Extensions;
using Czar.Cms.Core.Models;
using MySql.Data.MySqlClient;
using Npgsql;

namespace Czar.Cms.Core.DbHelper
{
    /// <summary>
    /// 数据库连接工厂类
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static IDbConnection CreateConnection(string dbtype, string strConn)
        {
            if (dbtype.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("请上传数据库连接类型");
            }

            if (strConn.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("请上传数据库连接字符串");
            }

            var dbType = GetDataBaseType(dbtype);
            return CreateConnnection(dbType, strConn);
        }


        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static IDbConnection CreateConnnection(DatabaseType dbType,string strConn)
        {
            IDbConnection connection = null;
            if (strConn.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("上传的数据库类型为空");
            }

            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection=new SqlConnection(strConn);
                    break;
                case DatabaseType.MySQL:
                    connection=new MySqlConnection(strConn);
                    break;
                case DatabaseType.PostgreSQL:
                    connection = new NpgsqlConnection(strConn);
                    break;
                    default:
                        throw new ArgumentNullException($"暂时还不支持{dbType.ToString()}数据库类型");
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;

        }

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbtype">数据库类型字符串</param>
        /// <returns>数据库类型</returns>
        public static DatabaseType GetDataBaseType(string dbtype)
        {
            if (dbtype.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("上传的数据库类型为空");
            }

            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if(dbType.ToString().Equals(dbtype,StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }

            return returnValue;
        }
        
    }


}