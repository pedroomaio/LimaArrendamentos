using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Typology : IEntity
    {
        public int Id { get; set; } // ID Tipologia

        public string TypologyDesc { get; set; }    // Descriçao Tipologia

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
