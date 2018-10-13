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
    public class NewsDetailPost : DataPost<NewsDetail>
    {
        private NewsDetail m_newsDetail;

        public NewsDetailPost(NewsDetail newsDetail, AppDb db) : base(newsDetail, db)
        {
            m_newsDetail = newsDetail;
            m_UpdateSql = m_Helper.UpdateSql<NewsDetail>("`id`=@id");
            m_DeleSql = m_Helper.DeleteSql<NewsDetail>("`id`=@id");
            m_SelectSql = m_Helper.SelectSql<NewsDetail>("`id`=@id");
        }

        public override List<NewsDetail> ReadAll(DbDataReader reader)
        {
            var posts = new List<NewsDetail>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new NewsDetail()
                    {
                        id = reader.GetFieldValue<string>(0),
                        title = reader.GetFieldValue<string>(1),
                        author = reader.GetFieldValue<string>(2),
                        contentText = reader.GetFieldValue<string>(3),
                        contentHtml = reader.GetFieldValue<string>(4),
                        existMedia = reader.GetFieldValue<bool>(5),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
        public async override Task<List<NewsDetail>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<NewsDetail>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new NewsDetail()
                    {
                        id = await reader.GetFieldValueAsync<string>(0),
                        title = await reader.GetFieldValueAsync<string>(1),
                        author = await reader.GetFieldValueAsync<string>(2),
                        contentText = await reader.GetFieldValueAsync<string>(3),
                        contentHtml = await reader.GetFieldValueAsync<string>(4),
                        existMedia = await reader.GetFieldValueAsync<bool>(5),
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
                Value = m_newsDetail.id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@title",
                DbType = DbType.String,
                Value = m_newsDetail.title,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@author",
                DbType = DbType.String,
                Value = m_newsDetail.author,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@contentText",
                DbType = DbType.String,
                Value = m_newsDetail.contentText,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@contentHtml",
                DbType = DbType.String,
                Value = m_newsDetail.contentHtml,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@existMedia",
                DbType = DbType.Boolean,
                Value = m_newsDetail.existMedia,
            });
        }
    }
}
