using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Helpers;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UsersController(
            IUserHelper userHelper,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userHelper = userHelper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.GetUsersInRoleAsync("Cliente");
            var clients = new List<RegisterNewUserViewModel>();

            foreach (User user in users)
            {
                var model = new RegisterNewUserViewModel();
                //model.Id = user.Id;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;
                model.Username = user.UserName;
                clients.Add(model);
            }

            return View(clients);
        }

        public async Task<IActionResult> Staff()
        {
            var users = await _userManager.GetUsersInRoleAsync("Staff");
            var clients = new List<RegisterNewUserViewModel>();

            foreach (User user in users)
            {
                var model = new RegisterNewUserViewModel();
                //model.Id = user.Id;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;
                model.Username = user.UserName;
                clients.Add(model);
            }

            return View(clients);
        }

    }
}
