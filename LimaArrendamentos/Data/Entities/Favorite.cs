using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Favorite : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public User User { get; set; }
        public int RealtyId { get; set; }
        public Realty Realty { get; set; }
    }
}
