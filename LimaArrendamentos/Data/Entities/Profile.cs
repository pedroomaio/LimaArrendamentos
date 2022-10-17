using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Profile: IEntity
    {
        public int Id { get; set; }

        public int ProfileType { get; set; }
    }
}
