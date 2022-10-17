using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public interface ILeaseRepository : IGenericRepository<Lease>
    {
        Task<Lease> GetbyRealtyIdAsync(int id);
        Task<bool> SaveAllAsync();

        Task AddRealtyToLeaseAsync(AddRealtyViewModel model, string username);

        Task<IQueryable<LeaseDetailTemp>> GetDetailTempsAsync(string userName);

        //Task<Lease> CreateLease(AddRealtyViewModel model);
    }
}
