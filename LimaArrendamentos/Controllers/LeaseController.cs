using LimaArrendamentos.Data;
using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Helpers;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Controllers
{
    public class LeaseController : Controller
    {
        private readonly DataContext _context;
        private readonly ILeaseRepository _leaseRepository;
        private readonly IRealtyRepository _realtyRepository;
        private readonly IUserHelper _userHelper;

        public LeaseController(
            DataContext context,
            ILeaseRepository leaseRepository,
            IRealtyRepository realtyRepository,
            IUserHelper userHelper)
        {
            _context = context;
            _leaseRepository = leaseRepository;
            _realtyRepository = realtyRepository;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            return View(_leaseRepository.GetAll());
        }

        public async Task<IActionResult> Create()
        {
            var model = await _leaseRepository.GetDetailTempsAsync(this.User.Identity.Name);
            //var model = await _orderRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> AddRealty(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var casaid = await _realtyRepository.GetByIdAsync(id.Value);
            ////var find = await _context.Realties.FindAsync(Id);

            var model = new AddRealtyViewModel
            {
                RealtyId = casaid.Id,
                AnnouncementTitle = casaid.AnnouncementTitle
                
            };
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddRealty(LeaseTemp leaseTemp, string username)
        //{
        //    if (ModelState.IsValid)
        //     {

        //        _context.Add(leaseTemp);
        //        await _context.SaveChangesAsync();

        //        var tenat = await _userHelper.GetUserByIdAsync(this.User.Identity.Name);

        //        leaseTemp.IdTenant = tenat;

        //        var user = await _userHelper.GetUserByIdAsync(tenat.Id);

        //        var model = new LeaseMessageViewModel
        //        {
        //            Message = "A sua proposta foi enviada para o senhorio."
        //        };

        //        //await _leaseRepository.AddRealtyToLeaseAsync(model);


        //        var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

        //        if (user == null)
        //        {
        //            ViewBag.Message = "Não foi possivel realizar a operação.";

        //            return View(model);
        //        }



        //        await _leaseRepository.AddRealtyToLeaseAsync(model, user.Email);

        //        TempData["Message"] = "A sua proposta foi enviada para o senhorio.";

        //        return View(model);

        //        await _leaseRepository.AddRealtyToLeaseAsync(model, this.User.Identity.Name);
        //        return RedirectToAction("Create");

        //        //await _orderRepository.AddItemToOrderAsync(model, this.User.Identity.Name);
        //        //return RedirectToAction("Create");

        //    }

        //    return View(model);
        //}

        




        //public async Task<IActionResult> AddRealty(RealtyViewModel)
        //{
        //    TempData.Remove("Mensagem");

        //    var casa = await _realtyRepository.GetByIdAsync()
        //}


    }
}
