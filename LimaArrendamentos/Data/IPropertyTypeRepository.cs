using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public interface IPropertyTypeRepository : IGenericRepository<PropertyType>
    {
        PropertyType ToPropertyType(PropertyType models, bool isNew, string userid);
        PropertyTypeViewModel ToPropertyTypeViewModel(PropertyType service, string userid);
        Task<PropertyType> GetPropertyType();
        Task<PropertyType> GetPropertyTypeAsync(int id);
        IEnumerable<SelectListItem> GetComboPropertyType();

        PropertyType ToService(Typology models, bool isNew);
        PropertyTypeViewModel ToServiceViewModel(PropertyType propertyType, string userid);

    }
}
