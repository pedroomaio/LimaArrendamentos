using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class Lease: IEntity
    {
        public int Id { get; set; }    // ID Arrendamento

        public int IdRealty { get; set; }   // ID Imovel

        public int IdTenant { get; set; }   // ID Locador

        public int IdLandlord { get; set; } // ID Locatario

        public int IdEmployee { get; set; } // ID Funcionario

        public int IdBondsman { get; set; } // ID Fiador

        public int IdAvailabilty { get; set; }  // ID Disponivel


        [Required]
        [Display(Name = "Data de Início")]
        public DateTime BeginDate { get; set; } // Data Inicio


        [Display(Name = "Data de Fim")]
        public DateTime? EndDate { get; set; }   // Data Fim


        [Display(Name = "Contrato")]
        public string ContractFile { get; set; }    // Ficheiro Contracto


    }
}
