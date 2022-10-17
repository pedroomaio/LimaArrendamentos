using LimaArrendamentos.Data;
using LimaArrendamentos.Helpers;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Controllers
{
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IUserHelper _userHelper;

        public PropertyTypeController(
            IPropertyTypeRepository propertyTypeRepository,
            IUserHelper userHelper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _userHelper = userHelper;
        }

        // GET: PropertyTypeController
        public ActionResult Index()
        {
            return View(_propertyTypeRepository.GetAll());
        }

        // GET: PropertyTypeController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var service = await _propertyTypeRepository.GetByIdAsync(id.Value);
            if (service == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(service);
        }

        // GET: PropertyTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                var service = _propertyTypeRepository.ToPropertyType(model, true, user.Id);

                await _propertyTypeRepository.CreateAsync(service);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PropertyTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropertyTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var service = await _propertyTypeRepository.GetByIdAsync(id.Value);
            if (service == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            var model = _propertyTypeRepository.ToPropertyTypeViewModel(service, user.Id);
            return View(model);
        }

        // GET: PropertyTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertyTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var a = await _propertyTypeRepository.GetByIdAsync(id.Value);
            if (a == null)
            {
                return NotFound();
            }

            return View(a);
        }
    }
}
