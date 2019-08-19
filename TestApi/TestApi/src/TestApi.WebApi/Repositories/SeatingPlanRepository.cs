using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestApi.WebApi.Models;

namespace TestApi.WebApi.Repositories
{
    public class SeatingPlanRepository : RepositoryBase
    {
        public async Task<IEnumerable<REDUDEMOSEATING>> GetSeatingPlanAsync()
        {
            using (var conn = OracleConnection)
            {
                var sql = "SELECT * FROM REDUDEMOSEATING";

                conn.Open();
                var seatingPlan = await conn.QueryAsync<REDUDEMOSEATING>(sql);

                return seatingPlan;
            }
        }
    }
}