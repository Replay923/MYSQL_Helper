using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataHelper.Entity
{

    public class MySqlBaseView : BaseView
    {
        readonly MySqlHelper helper = MySqlHelper.GetHelper();

        public ResultT Insert<ResultT, EntityT>(SqlConnection conn, EntityT entity) where EntityT : MySqlBaseView
        {
            throw new NotImplementedException();
        }

        public void Update<EntityT>(SqlConnection conn, EntityT entity) where EntityT : MySqlBaseView
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityT> Select<EntityT>(SqlConnection conn, EntityT entity) where EntityT : MySqlBaseView
        {
            throw new NotImplementedException();
        }

        public void Delete<TPriKey>(SqlConnection conn, TPriKey entity) where TPriKey : MySqlBaseView
        {
            throw new NotImplementedException();
        }
    }
}
