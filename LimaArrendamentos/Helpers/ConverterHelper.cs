using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace LimaArrendamentos.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Realty ToRealty(RealtyViewModel model, Guid imageId, bool isNew, string userName)
        {
            return new Realty
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
                Address = model.Address,
                Area = model.Area,
                nBathrooms = model.nBathrooms,
                nBedrooms = model.nBedrooms,
                PropertyType = model.PropertyType,
                ConstructionYear = model.ConstructionYear,
                CreatedInSite = DateTime.Now,
                Typology = model.Typology,
                TypologyId = model.TypologyId,
                EnergyClass = model.EnergyClass,
                EnergyClassId = model.EnergyClassId,
                AnnouncementTitle = model.AnnouncementTitle,
                Description = model.Description,
                Value = model.Value,
                Deposit = model.Deposit,
                Username = userName
            };
        }

        public Realty ToRealtyUpdate(Realty model)
        {
            return new Realty
{
                Id = model.Id,
                ImageId = model.ImageId,
                Address = model.Address,
                Area = model.Area,
                nBathrooms = model.nBathrooms,
                nBedrooms = model.nBedrooms,
                PropertyType = model.PropertyType,
                ConstructionYear = model.ConstructionYear,
                Typology = model.Typology,
                TypologyId = model.TypologyId,
                EnergyClass = model.EnergyClass,
                EnergyClassId = model.EnergyClassId,
                AnnouncementTitle = model.AnnouncementTitle,
                Description = model.Description,
                Value = model.Value,
                Deposit = model.Deposit,
                Username = model.Username
            };
        }


        public RealtyViewModel ToRealtiesViewModel(Realty realty)
        {
            return new RealtyViewModel
            {
                Id = realty.Id,
                ImageId = realty.ImageId,
                Address = realty.Address,
                Area = realty.Area,
                nBathrooms = realty.nBathrooms,
                nBedrooms = realty.nBedrooms,
                PropertyType = realty.PropertyType,
                ConstructionYear = realty.ConstructionYear,
                Typology = realty.Typology,
                TypologyId = realty.TypologyId,
                EnergyClass = realty.EnergyClass,
                EnergyClassId = realty.EnergyClassId,
                AnnouncementTitle = realty.AnnouncementTitle,
                Description = realty.Description,
                Value = realty.Value,
                Deposit = realty.Deposit,
                Username = realty.Username
            };
        }
    }
}