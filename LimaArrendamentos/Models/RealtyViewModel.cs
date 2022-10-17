using LimaArrendamentos.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LimaArrendamentos.Models
{
    public class RealtyViewModel : Realty
    {
        [Display(Name = "Imagem")]
        public IFormFile ImageFile { get; set; }

        //public IEnumerable<SelectListItem> Realties { get; set; }
        [Display(Name = "Tipologia")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a typology.")]
        
        public IEnumerable<SelectListItem> Typologies { get; set; }
        
        public IEnumerable<SelectListItem> EnergiesClass { get; set; }
        
        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
        
        public string price { get; set; }
    }
}
