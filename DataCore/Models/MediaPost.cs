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
    public class MediaPost : DataPost<Media>
    {
        private Media m_media;
        public MediaPost(Media media, AppDb db) : base(media, db)
        {
            this.m_media = media;
            m_UpdateSql = m_Helper.UpdateSql<Media>("`id`=@id");
            m_DeleSql = m_Helper.DeleteSql<Media>("`id`=@id");
            m_SelectSql = m_Helper.SelectSql<Media>("`newsId`=@newsId");
        }

        public override List<Media> ReadAll(DbDataReader reader)
        {
            var posts = new List<Media>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new Media()
                    {
                        id = reader.GetFieldValue<string>(0),
                        newsId = reader.GetFieldValue<string>(1),
                        index = reader.GetFieldValue<int>(2),
                        sourceUrl = reader.GetFieldValue<string>(3),
                        type = reader.GetFieldValue<byte>(4),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public async override Task<List<Media>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Media>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new Media()
                    {
                        id = await reader.GetFieldValueAsync<string>(0),
                        newsId = await reader.GetFieldValueAsync<string>(1),
                        index = await reader.GetFieldValueAsync<int>(2),
                        sourceUrl = await reader.GetFieldValueAsync<string>(3),
                        type = await reader.GetFieldValueAsync<byte>(4),
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
                Value = m_media.id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@newsId",
                DbType = DbType.String,
                Value = m_media.newsId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@index",
                DbType = DbType.String,
                Value = m_media.index,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@sourceUrl",
                DbType = DbType.String,
                Value = m_media.sourceUrl,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@type",
                DbType = DbType.Byte,
                Value = m_media.type,
            });
        }
    }
}
