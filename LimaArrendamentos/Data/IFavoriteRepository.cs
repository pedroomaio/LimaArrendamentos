using LimaArrendamentos.Data.Entities;
using System.Linq;

namespace LimaArrendamentos.Data
{
   public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        public IQueryable GetAllWithUsers();

        Favorite ToFavorite(string userid, int realtyId);
    }
}
