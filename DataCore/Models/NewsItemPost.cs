using DataHelper;
using DataHelper.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Models
{
    public class NewsItemPost : DataPost<NewsItem>
    {
        private NewsItem m_newsItem;
        public NewsItemPost(NewsItem newsItem, AppDb db) : base(newsItem, db)
        {
            this.m_newsItem = newsItem;
            m_SelectSqlAll = m_Helper.SelectSql<NewsItem>("t1", null)
                .INNER_JOIN(
                    m_Helper.SelectSql<NewsItem>("`typeHash`=@typeHash")
                        .ORDER_BY("`time`").DESC()
                        .LIMIT((this.m_newsItem.pageIndex - 1) * this.m_newsItem.pageSize, 1))
                .AS("t2")
                .ON("t1.`time` <= t2.`time`")
                .WHERE("t1.`typeHash`=@typeHash")
                .ORDER_BY("t1.`time`").DESC()
                .LIMIT(this.m_newsItem.pageSize);

            m_UpdateSql = m_Helper.UpdateSql<NewsItem>("`id`=@id");
            m_DeleSql = m_Helper.DeleteSql<NewsItem>("`id`= @id");
            m_SelectSql = m_Helper.SelectSql<NewsItem>("`id`=@id");
            m_DevelopSelect = m_Helper.SelectSql<NewsItem>("`isDone`=@isDone").ORDER_BY("`time`").DESC();
        }
        public override List<NewsItem> ReadAll(DbDataReader reader)
        {
            var posts = new List<NewsItem>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new NewsItem()
                    {
                        id = reader.GetFieldValue<string>(0),
                        pageIndex = reader.GetFieldValue<int>(1),
                        typeHash = reader.GetFieldValue<string>(2),
                        time = reader.GetFieldValue<DateTime>(3),
                        index = reader.GetFieldValue<int>(4),
                        title = reader.GetFieldValue<string>(5),
                        titleImg = reader.GetFieldValue<string>(6),
                        linkUrl = reader.GetFieldValue<string>(7),
                        desc = reader.GetFieldValue<string>(8),
                        author = reader.GetFieldValue<string>(9),
                        isDone = reader.GetFieldValue<bool>(10),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public override async Task<List<NewsItem>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<NewsItem>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new NewsItem()
                    {
                        id = await reader.GetFieldValueAsync<string>(0),
                        pageIndex = await reader.GetFieldValueAsync<int>(1),
                        typeHash = await reader.GetFieldValueAsync<string>(2),
                        time = await reader.GetFieldValueAsync<DateTime>(3),
                        index = await reader.GetFieldValueAsync<int>(4),
                        title = await reader.GetFieldValueAsync<string>(5),
                        titleImg = await reader.GetFieldValueAsync<string>(6),
                        linkUrl = await reader.GetFieldValueAsync<string>(7),
                        desc = await reader.GetFieldValueAsync<string>(8),
                        author = await reader.GetFieldValueAsync<string>(9),
                        isDone = await reader.GetFieldValueAsync<bool>(10),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public override void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.String,
                Value = m_newsItem.id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@pageIndex",
                DbType = DbType.Int32,
                Value = m_newsItem.pageIndex,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@typeHash",
                DbType = DbType.String,
                Value = m_newsItem.typeHash,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@time",
                DbType = DbType.DateTime,
                Value = m_newsItem.time,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@index",
                DbType = DbType.String,
                Value = m_newsItem.index,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@title",
                DbType = DbType.String,
                Value = m_newsItem.title,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@titleImg",
                DbType = DbType.String,
                Value = m_newsItem.titleImg,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@linkUrl",
                DbType = DbType.String,
                Value = m_newsItem.linkUrl,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@desc",
                DbType = DbType.String,
                Value = m_newsItem.desc,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@author",
                DbType = DbType.String,
                Value = m_newsItem.author,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@isDone",
                DbType = DbType.Boolean,
                Value = m_newsItem.isDone,
            });
        }
    }
}
