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
        public async Task<IEnumerable<REDUDEMOSEAT>> GetAllSeatsAsync()
        {
            using (var conn = OracleConnection)
            {
                var sql = "SELECT * FROM REDUDEMOSEAT";

                conn.Open();
                var seatingPlan = await conn.QueryAsync<REDUDEMOSEAT>(sql);

                return seatingPlan;
            }
        }

        public async Task<int> CreateSeatAsync(REDUDEMOSEAT seat)
        {
            using (var conn = OracleConnection)
            {
                var sql = @"INSERT INTO REDUDEMOSEAT(IDNO, CNM, EXT, PHONE, SEAT)
                                VALUES(:idno, :cnm, :ext, :phone, :seat)";

                conn.Open();
                var affectRows = await conn.ExecuteAsync(sql, new
                {
                    idno = seat.IDNO,
                    cnm = seat.CNM,
                    ext = seat.EXT,
                    phone = seat.PHONE,
                    seat = seat.SEAT
                });

                return affectRows;
            }
        }

        public async Task<int> DeleteSeatAsync(string idno)
        {
            using (var conn = OracleConnection)
            {
                var sql = "DELETE FROM REDUDEMOSEAT WHERE IDNO = :idno";

                conn.Open();
                var affectRows = await conn.ExecuteAsync(sql, new { idno });

                return affectRows;
            }
        }
    }
}