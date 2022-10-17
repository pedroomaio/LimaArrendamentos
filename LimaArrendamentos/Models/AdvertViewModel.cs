using LimaArrendamentos.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Models
{
    public class AdvertViewModel : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }
    }
}
