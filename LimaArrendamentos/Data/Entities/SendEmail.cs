using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{

    public class SendEmail : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int RealtyId { get; set; }
        public Realty Realty { get; set; }
    }
}
