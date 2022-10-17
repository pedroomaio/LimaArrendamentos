using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public class DashboardRepository : GenericRepository<Dashboard>, IDashboardRepository
    {
        private readonly DataContext _context;

        public DashboardRepository(
            DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddLeaseMessageAsync(LeaseMessageViewModel model)
        {
            var dashboard = await this.GetDashboardWithLeaseMessageAsync(1);

            if (dashboard == null)
            {
                return;
            }

            dashboard.LeaseMessages.Add(new LeaseMessage
            {
                Message = model.Message,
                LeaseId = model.LeaseId

            });

            _context.Dashboards.Update(dashboard);

            await _context.SaveChangesAsync();
        }

        public async Task<Dashboard> GetDashboardWithLeaseMessageAsync(int id)
        {
            return await _context.Dashboards
                .Include(d => d.LeaseMessages)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<LeaseMessage> GetLeaseMessageAsync(int id)
        {
            return await _context.LeaseMessages.FindAsync(id);
        }


    }
}
