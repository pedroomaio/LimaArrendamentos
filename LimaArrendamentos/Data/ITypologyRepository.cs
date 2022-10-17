using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public interface ITypologyRepository : IGenericRepository<Typology>
    {
        public IQueryable GetAllWithUsers();
        Typology ToService(Typology models, bool isNew, string userid);
        TypologyViewModel ToServiceViewModel(Typology service, string userid);
        Task<Typology> GetTypologiesAsync();
        Task<Typology> GetTypologiesAsync(int id);
        IEnumerable<SelectListItem> GetComboTypologies();
    }
}
