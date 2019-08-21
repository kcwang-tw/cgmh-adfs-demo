using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestApi.WebApi.Models;

namespace TestApi.WebApi.Repositories
{
    public class SeatsRepository : RepositoryBase
    {
        public async Task<IEnumerable<REDUDEMOSEAT>> GetSeatingPlanAsync()
        {
            using (var conn = OracleConnection)
            {
                var sql = "SELECT * FROM REDUDEMOSEAT";

                conn.Open();
                var seatingPlan = await conn.QueryAsync<REDUDEMOSEAT>(sql);

                return seatingPlan;
            }
        }
    }
}