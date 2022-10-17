using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Ad : IEntity
    {
        public int Id { get; set; }   // ID Anuncio


        [Required]
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Value { get; set; }  // Valor


        public DateTime TimeStamp { get; set; }  // Data Anuncio Publicaçao

        public bool IsActive { get; set; }  // Está Activo?

    }
}
