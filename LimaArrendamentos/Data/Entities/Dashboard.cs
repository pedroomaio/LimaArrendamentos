using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Dashboard : IEntity
    {
        public int Id { get; set; }

        public ICollection<LeaseMessage> LeaseMessages { get; set; }
    }
}
