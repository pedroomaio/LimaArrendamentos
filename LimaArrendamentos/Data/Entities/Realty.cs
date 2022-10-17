using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Realty : IEntity
    {
        public int Id { get; set; }   // ID Imovel

        public int IdPostalCode { get; set; }   // ID Codigo Postal
        [Display(Name = "Morada")]
        public string Typology { get; set; } // Tipologia
        [Display(Name = "Titulo do anúncio")]
        public string AnnouncementTitle { get; set; } // Titulo do anúncio
        [Display(Name = "Descrição detalhada")]
        public string Description { get; set; }   
        
      
        public string EnergyClass { get; set; }  // ID Classe Energetica

        //public int IdRealtyType { get; set; }   // ID Tipo Imovel


        [Display(Name = "Morada")]
        public string Address { get; set; } // Morada

        [Display(Name = "Tipo de propriedade")]
        public string PropertyType { get; set; } //Tipo de propriedade

        [Display(Name = "Tipo de propriedade")]
        public int PropertyTypeId { get; set; }


        [Required]
        [Display(Name = "Área por m2")]
        public int Area { get; set; } // Area m2


        [Required]
        [Display(Name = "Nº de Casas de Banho")]
        public int nBathrooms { get; set; } // Qta Casas Banho


        [Required]
        [Display(Name = "Nº de Quartos")]
        public int nBedrooms { get; set; }  // Qta Quartos 

        [Required]
        [Display(Name = "Preço")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Value { get; set; }  // Valor


        [Display(Name = "Preço da Caução")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Deposit { get; set; }    // Valor Cauçao/Deposito

        [Display(Name = "Data de Contrução")]
        public DateTime ConstructionYear { get; set; }  // Ano Contruçao
        public DateTime CreatedInSite { get; set; }  // Ano Contruçao


        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Latitude { get; set; } // Latitude


        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Longitude { get; set; }   // Longitude
        public User User { get; set; }
        [Display(Name = "Tipologia")]
        public int TypologyId { get; set; }
        [Display(Name = "Classe energética")]
        public int EnergyClassId { get; set; }

        public int UserId { get; set; }
        [Display(Name = "Locatário")]
        public string Username { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://limarrendamentos.azurewebsites.net/img/1024px-No_image_available.svg.png"
            : $"https://limarrendamentos.blob.core.windows.net/realties/{ImageId}";


    }
}
