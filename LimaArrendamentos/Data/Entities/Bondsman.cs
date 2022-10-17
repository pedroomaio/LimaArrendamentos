using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Bondsman: IEntity
    {

        public int Id { get; set; } // ID Fiador


        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }    // Nome


        [Required]
        public int IdPostalCode { get; set; }   // ID Codigo Postal


        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string Email { get; set; }   // E-mail

        public int Tel { get; set; }    // Telefone


        [Required]
        [Display(Name = "Nº Cartão Cidadão")]
        [MaxLength(8, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public int CCnumber { get; set; }   // Cartao Cidadao


        [Required]
        [Display(Name = "Nº Contribuinte")]
        [MaxLength(9, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public int NIF { get; set; }    // Numero Contribuinte


        [Display(Name = "Data de Nascimento")]
        public DateTime DateOfBirth { get; set; }   // Data Nascimento


    }
}
