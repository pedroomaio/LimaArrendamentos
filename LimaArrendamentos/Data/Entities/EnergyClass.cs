using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class EnergyClass: IEntity
    {
        public int Id { get; set; }  // ID Classe Energetica

        public string EnergyClassDesc { get; set; } // Descriçao Classe Energetica
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
