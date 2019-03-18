using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Sample05.Models;

namespace Sample05.DBUnit
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class DBUnit
    {
        public static void TestInsert()
        {
            var content = new Content
            {
                Title = "标题1",
                ContentString = "内容1",
            };
            using (var conn=new SqlConnection("Server=.;Database=CMS;UID=sa;PWD=sa.;Pooling=true;Max Pool Size=100;"))
            {
                var sql_insert = @"insert into [Content] (title,contentString,status,addTime,modifyTime)
                                     values (@title,@contentString,@status,@addTime,@modifyTime)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_insert:插入了{result}条数据");
            }
        }

        public static void Test_mult_insert()
        {
            List<Content> contents=new List<Content>()
            {
                new Content()
                {
                    Title = "批量插入标题1",
                    ContentString = "批量插入内容1",
                },
                new Content()
                {
                    Title = "批量插入标题2",
                    ContentString = "批量插入内容2",
                },
            };

            using (var conn=new SqlConnection("Server=.;Database=CMS;UID=sa;PWD=sa.;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"insert into [Content] (title,contentString,status,addTime,modifyTime)
                                 values (@title,@contentString,@status,@addTime,@modifyTime)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_insert:插入了{result}条数据");
            }
        }

        public static void Test_Del()
        {
            var delContent=new Content()
            {
                Id = 4
            };

            using (var conn=new SqlConnection("Server=.;Database=CMS;UID=sa;PWD=sa.;Pooling=true;Max Pool Size=100;"))
            {
                var delString = @"delete from [Content] where (id=@id)";
                var result=conn.Execute(delString, delContent);
                Console.WriteLine($"delString:删除了{result}条数据");
                
            }
        }

        public static void Test_Update()
        {
            var updateContents = new List<Content>
            {
                new Content()
                {
                    Id = 2,
                    Title = "更新后的Title2",
                    ContentString = "更新后的Content2"
                },
                new Content()
                {
                    Id =3,
                    Title = "更新后的Title3",
                    ContentString = "更新后的Content3"
                }
            };


            using (var conn=new SqlConnection("Server=.;Database=CMS;UID=sa;PWD=sa.;Pooling=true;Max Pool Size=100;"))
            {
                var updateSql =
                    @"update [Content] set title=@title,contentString=@contentString,modifyTime=getdate() where id=@id";
                var result = conn.Execute(updateSql, updateContents);
                Console.WriteLine($"updatesql:更新了{result}条数据");
            }
        }

        public static void Test_select_one()
        {
            using (var conn=new SqlConnection(@"Server=.;Database=CMS;UID=sa;PWD=sa.;Pooling=true;Max Pool Size=100;"))
            {
                var selectSql = @"select * from [Content] where id=@id";
                var result = conn.QueryFirstOrDefault<Content>(selectSql, new {id = 1});
                Console.WriteLine($"Select_one的结果是{result.ToString()}");
            }
        }

        public static void Test_select_multiple()
        {
            using (var conn = new SqlConnection(@"Server=.;Database=CMS;UID=sa;PWD=sa.;Pooling=true;Max Pool Size=100;"))
            {
                var selectSql = @"select * from [Content] where id in @ids";
                var result = conn.Query<Content>(selectSql, new { ids = new int[]{2,3} });

                foreach (var res in result)
                {
                    Console.WriteLine($"Select_multiple的结果是{res.ToString()}");
                }
                
            }
        }

        public static void Test_select_contentWithComment()
        {
            using (var conn = new SqlConnection(@"Server=.;Database=CMS;UID=sa;PWD=sa.;Pooling=true;Max Pool Size=100;"))
            {
                var selectSql = @"select * from [Content] where id =@id;select * from Comment where contentId=@id";
                using (var result = conn.QueryMultiple(selectSql, new { id = 2 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.Comments = result.Read<Comment>();
                    Console.WriteLine($"Test_select_contentWithComment的结果是{content.ToString()}");

                }

               
               

            }
        }
    }
}