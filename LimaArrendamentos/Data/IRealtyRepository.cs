using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data
{
    public interface IRealtyRepository : IGenericRepository<Realty>
    {
        IQueryable GetAllWithUsers();
        Realty GetAllId(int id);
        IQueryable<Realty> GetAllWithPrecomin(string typology);
        IQueryable<Realty> GetAllWithFavorites(string username);


        Task<IQueryable<RealtyViewModel>> GetPrice();
    }
}
