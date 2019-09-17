using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PrimaryApi.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryApi.Core.Interfaces
{
    public interface ISeatsQuery
    {
        Task<IEnumerable<DepartmentSeat>> GetAllSeatsAsync(UserAssertion userAssertion);

        Task<DepartmentSeat> GetSeatByUserIdAsync(string userId);
    }
}
