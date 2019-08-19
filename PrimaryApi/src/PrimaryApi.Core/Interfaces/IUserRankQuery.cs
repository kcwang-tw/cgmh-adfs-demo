using PrimaryApi.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryApi.Core.Interfaces
{
    public interface IUserRankQuery
    {
        Task<UserRank> GetUserRankAsync(string userId);
    }
}
