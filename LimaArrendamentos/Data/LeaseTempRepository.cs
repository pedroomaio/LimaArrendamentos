using LimaArrendamentos.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public class LeaseTempRepository : GenericRepository<LeaseTemp>, ILeaseTempRepository
    {
        private readonly DataContext _context;

        public LeaseTempRepository(
            DataContext context) : base(context)
        {
            _context = context;
        }


    }
}
