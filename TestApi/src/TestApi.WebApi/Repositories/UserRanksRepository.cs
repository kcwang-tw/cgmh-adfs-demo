using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestApi.WebApi.Models;

namespace TestApi.WebApi.Repositories
{
    public class UserRanksRepository : RepositoryBase
    {
        public async Task<REDUDEMORNK> GetRankByUserIdAsync(string userId)
        {
            using (var conn = OracleConnection)
            {
                var sql = "SELECT IDNO, RNK FROM REDUDEMORNK WHERE IDNO = :userId";

                conn.Open();
                var rank = await conn.QueryFirstOrDefaultAsync<REDUDEMORNK>(sql, new { userId });

                return rank;
            }
        }
    }
}