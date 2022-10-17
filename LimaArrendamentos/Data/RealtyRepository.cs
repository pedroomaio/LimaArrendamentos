using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public class RealtyRepository : GenericRepository<Realty>, IRealtyRepository
    {
        private readonly DataContext _context;

        public RealtyRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<RealtyViewModel>> GetPrice()
        {
            var realties = Enumerable.Empty<RealtyViewModel>().AsQueryable();

            await Task.Run(() =>
            {
                realties = _context.Realties.OrderByDescending(r => r.Value).Select(r => new RealtyViewModel
                {
                    Id = r.Id
                });
            });

            return realties;
        }


        public IQueryable GetAllWithUsers()
        {
            return _context.Realties.Include(p => p.User);
        }

        public Realty GetAllId(int id)
        {
            var a = new Realty();

            return _context.Realties
                 .Include(c => c.User)
                 .Where(c => c.Id == id).FirstOrDefaultAsync().Result;
        }

        public IQueryable<Realty> GetAllWithFavorites(string username)
        {
            return _context.Set<Realty>().AsNoTracking().Where(c => c.Username == username); ;
        }
        public IQueryable<Realty> GetAllWithPrecomin(string typology)
        {
            var RealtyPreco = new Realty();
            if (typology != "")
            {

                //var linqRealtyPreco = from I in _context.Realties
                //                      where I.Typology == typology
                //                      select new
                //                      {
                //                          I.Id,
                //                          I.Address,
                //                          I.Area,
                //                          I.nBathrooms,
                //                          I.nBedrooms,
                //                          I.ConstructionYear,
                //                          I.CreatedInSite,
                //                          I.Typology,
                //                          I.TypologyId,
                //                          I.EnergyClass,
                //                          I.EnergyClassId,
                //                          I.AnnouncementTitle,
                //                          I.Description,
                //                          I.Value,
                //                          I.Deposit,
                //                          I.Username
                //                      };


                //foreach (var item in linqRealtyPreco)
                //{
                //    RealtyPreco.Id = item.Id;
                //    RealtyPreco.Address = item.Address;
                //    RealtyPreco.Area = item.Area;
                //    RealtyPreco.nBathrooms = item.nBathrooms;
                //    RealtyPreco.nBedrooms = item.nBedrooms;
                //    RealtyPreco.ConstructionYear = item.ConstructionYear;
                //    RealtyPreco.Typology = item.Typology;
                //    RealtyPreco.TypologyId = item.TypologyId;
                //    RealtyPreco.EnergyClass = item.EnergyClass;
                //    RealtyPreco.EnergyClassId = item.EnergyClassId;
                //    RealtyPreco.AnnouncementTitle = item.AnnouncementTitle;
                //    RealtyPreco.Description = item.Description;
                //    RealtyPreco.Value = item.Value;
                //    RealtyPreco.Deposit = item.Deposit;
                //    RealtyPreco.Username = item.Username;
                //}


                //return RealtyPreco;
            }
            //return RealtyPreco;
            return _context.Realties
                 .Where(c => c.Typology == typology);
        }

    }
}
