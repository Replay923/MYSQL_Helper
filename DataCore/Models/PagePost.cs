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
    public class PagePost : DataPost<Page>
    {
        private Page m_page;
        public PagePost(Page page, AppDb db) : base(page, db)
        {
            this.m_page = page;
            m_UpdateSql = m_Helper.UpdateSql<Page>("`hash`=@hash");
            m_DeleSql = m_Helper.DeleteSql<Page>("`hash`=@hash");
            m_SelectSqlAll = m_Helper.SelectSql<Page>();
            m_DevelopSelect = m_Helper.SelectSql<Page>("`hash`=@hash");
        }

        public override void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@hash",
                DbType = DbType.String,
                Value = m_page.hash,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@appName",
                DbType = DbType.String,
                Value = m_page.appName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@pageType",
                DbType = DbType.String,
                Value = m_page.pageType,
            });
        }


        public override List<Page> ReadAll(DbDataReader reader)
        {
            var posts = new List<Page>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new Page()
                    {
                        hash = reader.GetFieldValue<string>(0),
                        appName = reader.GetFieldValue<string>(1),
                        pageType = reader.GetFieldValue<string>(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public override async Task<List<Page>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Page>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new Page()
                    {
                        hash = await reader.GetFieldValueAsync<string>(0),
                        appName = await reader.GetFieldValueAsync<string>(1),
                        pageType = await reader.GetFieldValueAsync<string>(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
