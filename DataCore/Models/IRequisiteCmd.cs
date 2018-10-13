using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Models
{
    public interface IRequisiteCmd
    {
        void BindParams(MySqlCommand cmd);
        void Insert();
        Task InsertAsync();
        void Update();
        Task UpdateAsync();
        void Delete();
        Task DeleteAsync();
        object Select();
        Task<object> SelectAsync();
        object SelectAll();
        Task<object> SelectAllAsync();
        object DevSelect();
        Task<object> DevSelectAsync();
    }

    public interface ITableQuery<T>
    {
        List<T> ReadAll(DbDataReader reader);
        Task<List<T>> ReadAllAsync(DbDataReader reader);
    }
}
