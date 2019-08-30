using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TestApi.WebApi.Repositories
{

    public abstract class RepositoryBase
    {
        protected IDbConnection OracleConnection { get; private set; }

        public RepositoryBase()
        {
            // 建立資料庫連線
            var connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["JIATEST4"].ConnectionString;
            //OracleConnection = new Oracle.DataAccess.Client.OracleConnection(connString);
        }
    }
}