using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Availability : IEntity
    {
        public int Id { get; set; }    // ID Disponivel

        public bool IsAvailable { get; set; }   // Está Disponível?
    }
}
