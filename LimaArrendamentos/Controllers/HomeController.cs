using AutoRepair.Helpers;
using LimaArrendamentos.Helpers;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailHelper _mailHelper;

        public HomeController(ILogger<HomeController> logger, IMailHelper mailHelper)
        {
            _logger = logger;
            _mailHelper = mailHelper;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(SendMailViewModel model)
        {
            Response response = _mailHelper.SendEmail("limarrendamentos@gmail.com", $"{model.Subject} - {model.Name}",
                       $"<p>Email: {model.Email}<br/><br/></p>" +
                       $"<p>{model.Body}</p>");

            if (response.IsSuccess)
            {
                return View(model);
            }

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
