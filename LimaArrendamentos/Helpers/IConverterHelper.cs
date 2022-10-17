using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using System;

namespace LimaArrendamentos.Helpers
{
    public interface IConverterHelper
    {
        Realty ToRealty(RealtyViewModel model, Guid imageId, bool isNew, string userName);
        Realty ToRealtyUpdate(Realty mode);
        RealtyViewModel ToRealtiesViewModel(Realty realty);
        
    }

}
