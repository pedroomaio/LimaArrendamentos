using LimaArrendamentos.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Models
{
    public class DashboardViewModel : Dashboard
    {

        public int TotalRealties { get; set; }

        public int TotalUsers { get; set; }

    }
}
