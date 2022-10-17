using LimaArrendamentos.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LimaArrendamentos.Models
{
    public class ChangeUserViewModel : User
    {
        [Display(Name = "Imagem")]
        public IFormFile ImageFile { get; set; }
    }
}
