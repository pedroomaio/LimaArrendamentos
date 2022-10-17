using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Models
{
    public class LeaseViewModel 
    {
        [Display(Name = "Realty")]
        
        public int RealtyId { get; set; }

        public string AnnouncementTitle { get; set; }

        [Required]
        [Display(Name = "Data de Início")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Data de Fim")]
        public DateTime EndDate { get; set; }




    }
}
