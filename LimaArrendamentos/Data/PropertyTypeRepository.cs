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
    public class PropertyTypeRepository : GenericRepository<PropertyType>, IPropertyTypeRepository
    {
        private readonly DataContext _context;

        public PropertyTypeRepository(
            DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PropertyType> GetPropertyType()
        {
            return await _context.PropertyTypes
                .FirstOrDefaultAsync();
        }

        public async Task<PropertyType> GetPropertyTypeAsync(int id)
        {
            //return await _context.Brands.FindAsync(id);

            var a = _context.PropertyTypes;
            return await _context.PropertyTypes
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public PropertyType ToPropertyType(PropertyType models, bool isNew, string userid)
        {
            return new PropertyType
            {
                Id = isNew ? 0 : models.Id,
                PropertyTypeDesc = models.PropertyTypeDesc,
                UserId = userid
            };
        }

        public PropertyTypeViewModel ToPropertyTypeViewModel(PropertyType service, string userid)
        {
            return new PropertyTypeViewModel
            {
                Id = service.Id,
                PropertyTypeDesc = service.PropertyTypeDesc,
                UserId = userid,
            };
        }

        public IEnumerable<SelectListItem> GetComboPropertyType()
        {

            var list = _context.PropertyTypes.Select(p => new SelectListItem
            {
                Text = p.PropertyTypeDesc,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Selecione uma tipo de propriedade...)",
                Value = "0"
            });

            return list;

        }


        public PropertyType ToService(Typology models, bool isNew)
        {
            return new PropertyType
            {
                Id = isNew ? 0 : models.Id,
                PropertyTypeDesc = models.TypologyDesc,

            };
        }

        public PropertyTypeViewModel ToServiceViewModel(PropertyType propertyType, string userid)
        {
            return new PropertyTypeViewModel
            {
                Id = propertyType.Id,
                PropertyTypeDesc = propertyType.PropertyTypeDesc,
                UserId = userid
            };
        }
    }
}
