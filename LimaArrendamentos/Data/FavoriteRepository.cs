using LimaArrendamentos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LimaArrendamentos.Data
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private readonly DataContext _context;

        public FavoriteRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.Favorites.Include(p => p.User);
        }

        public Favorite ToFavorite(string userid, int realtyId)
        {
            return new Favorite
            {
                Username = userid,
                RealtyId = realtyId
            };
        }
       
    }
}