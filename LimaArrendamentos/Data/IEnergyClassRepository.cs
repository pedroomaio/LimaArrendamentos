using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public interface IEnergyClassRepository : IGenericRepository<EnergyClass>
    {
        public IQueryable GetAllWithUsers();
        EnergyClass ToEnergyClass(EnergyClass models, bool isNew, string userid);
        EnergyClassViewModel ToEnergyClassViewModel(EnergyClass service, string userid);
        Task<EnergyClass> GetEnergyClassAsync();
        Task<EnergyClass> GetEnergyClassAsync(int id);
        IEnumerable<SelectListItem> GetComboEnergyClass();
    }
}