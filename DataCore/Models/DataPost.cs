using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace DataCore.Models
{
    public class DataPost<T> : IRequisiteCmd, ITableQuery<T>
    {
        protected T m_Table;
        protected string m_InsertSql, m_SelectSqlAll, m_SelectSql, m_UpdateSql, m_DeleSql;
        protected string m_DevelopSelect;
        protected DataHelper.MySqlHelper m_Helper
        {
            get
            {
                if (m_helper == null)
                    m_helper = DataHelper.MySqlHelper.GetHelper();
                return m_helper;
            }
        }
        private DataHelper.MySqlHelper m_helper;

        public AppDb Db { get; set; }

        public DataPost(T t, AppDb db = null)
        {
            this.m_Table = t;
            Db = db;
            m_InsertSql = m_Helper.InsertSql<T>();
            m_SelectSqlAll = m_Helper.SelectSql<T>();
        }

        public void Insert()
        {
            if (string.IsNullOrEmpty(m_InsertSql))
                return;
            var cmd = InsertCmd();
            cmd.ExecuteNonQuery();
        }

        public async Task InsertAsync()
        {
            if (string.IsNullOrEmpty(m_InsertSql))
                return;
            var cmd = InsertCmd();
            await cmd.ExecuteNonQueryAsync();
        }
        public object DevSelect()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = m_DevelopSelect;
            BindParams(cmd);
            return ReadAll(cmd.ExecuteReader());
        }
        public async Task<object> DevSelectAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = m_DevelopSelect;
            BindParams(cmd);
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public object Select()
        {
            if (string.IsNullOrEmpty(m_SelectSql))
                return default(T);
            var cmd = SelectCmd();
            var result = ReadAll(cmd.ExecuteReader());
            return result.Count > 0 ? result[0] : default(T);
        }

        public async Task<object> SelectAsync()
        {
            if (string.IsNullOrEmpty(m_SelectSql))
                return default(T);
            var cmd = SelectCmd();
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : default(T);
        }
        public object SelectAll()
        {
            if (string.IsNullOrEmpty(m_SelectSqlAll))
                return default(List<T>);
            var cmd = SelectAllCmd();
            return ReadAll(cmd.ExecuteReader());
        }

        public async Task<object> SelectAllAsync()
        {
            if (string.IsNullOrEmpty(m_SelectSqlAll))
                return default(List<T>);
            var cmd = SelectAllCmd();
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public void Update()
        {
            if (string.IsNullOrEmpty(m_UpdateSql))
                return;
            var cmd = UpdateCmd();
            cmd.ExecuteNonQuery();
        }

        public async Task UpdateAsync()
        {
            if (string.IsNullOrEmpty(m_UpdateSql))
                return;
            var cmd = UpdateCmd();
            await cmd.ExecuteNonQueryAsync();
        }

        public void Delete()
        {
            if (string.IsNullOrEmpty(m_DeleSql))
                return;
            var cmd = DeleteCmd();
            cmd.ExecuteNonQuery();
        }

        public async Task DeleteAsync()
        {
            if (string.IsNullOrEmpty(m_DeleSql))
                return;
            var cmd = DeleteCmd();
            await cmd.ExecuteNonQueryAsync();
        }

        protected virtual void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.String,
            });
        }

        public virtual void BindParams(MySqlCommand cmd)
        {
        }

        protected virtual MySqlCommand InsertCmd()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            // ReSharper disable once PossibleNullReferenceException
            cmd.CommandText = m_InsertSql;
            BindParams(cmd);
            return cmd;
        }
        protected virtual MySqlCommand SelectCmd()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            // ReSharper disable once PossibleNullReferenceException
            cmd.CommandText = m_SelectSql;
            BindParams(cmd);
            return cmd;
        }

        protected virtual MySqlCommand SelectAllCmd()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            // ReSharper disable once PossibleNullReferenceException
            cmd.CommandText = m_SelectSqlAll;
            BindParams(cmd);
            return cmd;
        }

        protected virtual MySqlCommand UpdateCmd()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            // ReSharper disable once PossibleNullReferenceException
            cmd.CommandText = m_UpdateSql;
            BindParams(cmd);
            return cmd;
        }

        protected virtual MySqlCommand DeleteCmd()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            // ReSharper disable once PossibleNullReferenceException
            cmd.CommandText = m_DeleSql;
            BindParams(cmd);
            return cmd;
        }

        public virtual List<T> ReadAll(DbDataReader reader)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<T>> ReadAllAsync(DbDataReader reader)
        {
            throw new System.NotImplementedException();
        }
    }
}
