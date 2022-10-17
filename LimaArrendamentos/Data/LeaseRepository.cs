using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Helpers;
using LimaArrendamentos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace LimaArrendamentos.Data
{
    public class LeaseRepository : GenericRepository<Lease>, ILeaseRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public LeaseRepository(
            DataContext context,
            IUserHelper userHelper) : base(context)
{
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<Lease> GetbyRealtyIdAsync(int id)
        {
            return await _context.Set<Lease>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.IdRealty == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //public async Task<Lease> CreateLease(AddRealtyViewModel model)
        //{
        //    var user = _userHelper.GetByIdAsync();

        //    return new Lease
        //    {
        //        IdRealty = model.RealtyId,
        //        BeginDate = model.BeginDate,
        //        EndDate = model.EndDate,
        //        Id = user.Id

        //    };  
        //}

        public async Task AddRealtyToLeaseAsync(AddRealtyViewModel model, string username)
        {
            var user = await _userHelper.GetUserByEmailAsync(username);
            if (user == null)
            {
                return;
            }

            var house = await _context.Realties.FindAsync(model.RealtyId);
            if (house == null)
            {
                return;
            }

            var leasetemp = await _context.LeaseDetailTemp
                 .Where(x => x.User == user && x.Realty == house)
                 .FirstOrDefaultAsync();

            if (leasetemp == null)
            {
                leasetemp = new LeaseDetailTemp
                {
                    User = user,
                    Realty = house,
                    Price = house.Value

                };

                _context.LeaseDetailTemp.Add(leasetemp);
            }
            else
            {
                _context.LeaseDetailTemp.Update(leasetemp);

            }

            await _context.SaveChangesAsync();

        }

        public async Task<IQueryable<LeaseDetailTemp>> GetDetailTempsAsync(string userName)
        {

            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.LeaseDetailTemp
                .Include(p => p.Realty)
                .Where(o => o.User == user)
                .OrderBy(o => o.Realty.AnnouncementTitle);
        }

        public IQueryable GetLeaseWithRealtyandUsers()
        {
            return _context.Leases
                .Include(c => c.IdRealty)
                .Include(c => c.IdLandlord)
                .OrderBy(c => c.Id);
        }
        public IQueryable GetRealties()
        {
            return _context.Realties.OrderBy(c => c.AnnouncementTitle);
        }

        public async Task<Lease> GetLeaseWithRealtyandUsersAsync(int id)
        {
            return await _context.Leases
               .Include(c => c.IdRealty)
               .Include(c => c.IdLandlord)
               .Where(c => c.Id == id)
               .FirstOrDefaultAsync();
        }

    }
}
