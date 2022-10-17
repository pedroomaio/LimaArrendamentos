using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class PropertyType: IEntity
    {
        public int Id { get; set; }   // ID Tipo Imovel

        public string PropertyTypeDesc { get; set; }  // Descriçao Tipo Imovel
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
