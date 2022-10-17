using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LimaArrendamentos.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Nome Próprio")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string FirstName { get; set; }


        [Display(Name = "Apelido")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string LastName { get; set; }


        [Display(Name = "Nome Completo")]
        public string FullName => $"{FirstName} {LastName}";


        [Display(Name = "Data de Nascimento")]
        public DateTime? DateOfBirth { get; set; }   // Data Nascimento


        [Display(Name = "Nº Cartão Cidadão")]
        public string CCnumber { get; set; }   // Numero Cartao Cidadao


        [Display(Name = "Nº Contribuinte")]
        public string NIF { get; set; }    // Numero Contribuinte


        [Display(Name = "Morada")]
        public string Address { get; set; } // Morada


        public bool AgreeTerm { get; set; }


        [Display(Name = "Foto de Perfil")]
        public Guid? ProfilePic { get; set; }  // Imagem Perfil


        public string ImageFullPath => ProfilePic == Guid.Empty
        ? $"https://limarrendamentos.azurewebsites.net/img/users/profilenoimage.png"
        : $"https://limarrendamentos.blob.core.windows.net/users/{ProfilePic}";


        [Display(Name = "Está Activo?")]
        public bool IsActive { get; set; }  // Está Activo?
    }
}
