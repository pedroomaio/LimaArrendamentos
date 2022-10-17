using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public interface IDashboardRepository : IGenericRepository<Dashboard>
    {
        Task AddLeaseMessageAsync(LeaseMessageViewModel model);

        Task<Dashboard> GetDashboardWithLeaseMessageAsync(int id);

        Task<LeaseMessage> GetLeaseMessageAsync(int id);
    }
}
