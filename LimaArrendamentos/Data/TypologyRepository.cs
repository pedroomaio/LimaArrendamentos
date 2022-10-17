using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public class TypologyRepository : GenericRepository<Typology>, ITypologyRepository
    {
        private readonly DataContext _context;

        public TypologyRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.Typologies.Include(p => p.User);
        }

        public Typology ToService(Typology models, bool isNew, string userid)
        {
            return new Typology
            {
                Id = isNew ? 0 : models.Id,
                TypologyDesc = models.TypologyDesc,
                UserId = userid
            };
        }

        public TypologyViewModel ToServiceViewModel(Typology typology, string userid)
        {
            return new TypologyViewModel
            {
                Id = typology.Id,
                TypologyDesc = typology.TypologyDesc,
                UserId = userid
            };
        }
        public async Task<Typology> GetTypologiesAsync()
        {
            return await _context.Typologies
                .FirstOrDefaultAsync();
        }

        public async Task<Typology> GetTypologiesAsync(int id)
        {
            //return await _context.Brands.FindAsync(id);

            var a = _context.Typologies;
            return await _context.Typologies
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
        public IEnumerable<SelectListItem> GetComboTypologies()
        {

            var list = _context.Typologies.Select(p => new SelectListItem
            {
                Text = p.TypologyDesc,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Selecione uma tipologia...)",
                Value = "0"
            });

            return list;

        }
    }
}
