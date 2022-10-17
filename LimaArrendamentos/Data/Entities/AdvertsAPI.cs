using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class AdvertsAPI
    {
        // Ad Title
        public string Title { get; set; }

        // Redirects user to
        public string Url { get; set; }

        // Ad Image
        public string Image { get; set; }
    }
}
