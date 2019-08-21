using PrimaryApi.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryApi.Core.Interfaces
{
    public interface ISeatsCommand
    {
        Task<int> AddSeatAsync(DepartmentSeat seat);

        Task<int> RemoveSeatAsync(string userId);
    }
}
