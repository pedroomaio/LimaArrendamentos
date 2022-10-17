using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimaArrendamentos.Models;

namespace LimaArrendamentos.Helpers
{
    public interface IAdvertHelper
    {
        Task<AdvertViewModel> GetAdApi();
    }
}
