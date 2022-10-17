using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public class EnergyClassRepository : GenericRepository<EnergyClass>, IEnergyClassRepository
    {
        private readonly DataContext _context;

        public EnergyClassRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.EnergyClasses.Include(p => p.User);
        }

        public EnergyClass ToEnergyClass(EnergyClass models, bool isNew, string userid)
        {
            return new EnergyClass
            {
                Id = isNew ? 0 : models.Id,
                EnergyClassDesc = models.EnergyClassDesc,
                UserId= userid
            };
        }

        public EnergyClassViewModel ToEnergyClassViewModel(EnergyClass service, string userid)
        {
            return new EnergyClassViewModel
            {
                Id = service.Id,
                EnergyClassDesc = service.EnergyClassDesc,
                UserId= userid,
            };
        }
        public async Task<EnergyClass> GetEnergyClassAsync()
        {
            return await _context.EnergyClasses
                .FirstOrDefaultAsync();
        }

        public async Task<EnergyClass> GetEnergyClassAsync(int id)
        {
            //return await _context.Brands.FindAsync(id);

            var a = _context.EnergyClasses;
            return await _context.EnergyClasses
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
        public IEnumerable<SelectListItem> GetComboEnergyClass()
        {

            var list = _context.EnergyClasses.Select(p => new SelectListItem
            {
                Text = p.EnergyClassDesc,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Selecione uma classe energética...)",
                Value = "0"
            });

            return list;

        }
    }
}
