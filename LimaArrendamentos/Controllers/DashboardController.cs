using LimaArrendamentos.Data;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DataContext _context;

        public DashboardController(
            DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalRealties = _context.Realties.Count(),
                TotalUsers = _context.Users.Count()
            };

            return View(model);
        }
    }
}
