using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using LimaArrendamentos.Data.Entities;

namespace LimaArrendamentos.Models
{
    public class RegisterNewUserViewModel : User
    {
        

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

          
        [Required]
        [MinLength(6)]
        public string Password { get; set; }


        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }


        [Display(Name = "Imagem")]
        public IFormFile ImageFile { get; set; }
    }
}
