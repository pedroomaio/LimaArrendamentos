using LimaArrendamentos.Data;
using LimaArrendamentos.Helpers;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LimaArrendamentos.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EnergyClassController : Controller
    {
        private readonly IEnergyClassRepository _energyClassRepository;
        private readonly IUserHelper _userHelper;

        public EnergyClassController(
            IEnergyClassRepository energyClassRepository, IUserHelper userHelper)
        {
            _energyClassRepository = energyClassRepository;
            _userHelper = userHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_energyClassRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var service = await _energyClassRepository.GetByIdAsync(id.Value);
            if (service == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(service);
        }


        ////// GET: Products/Create
        //[Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnergyClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                var service = _energyClassRepository.ToEnergyClass(model, true, user.Id);

                await _energyClassRepository.CreateAsync(service);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        ////// GET: Products/Edit/5
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var service = await _energyClassRepository.GetByIdAsync(id.Value);
            if (service == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            var model = _energyClassRepository.ToEnergyClassViewModel(service, user.Id);
            return View(model);
        }


        ////// POST: Products/Edit/5
        ////// To protect from overposting attacks, enable the specific properties you want to bind to.
        ////// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EnergyClassViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                    var service = _energyClassRepository.ToEnergyClass(model, false, user.Id);


                    await _energyClassRepository.UpdateAsync(service);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _energyClassRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        ////// GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AutoPieceNotFound");
            }

            var car = await _energyClassRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return new NotFoundViewResult("AutoPieceNotFound");
            }

            return View(car);
        }

        ////// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _energyClassRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _energyClassRepository.DeleteAsync(service);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{service.EnergyClassDesc} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{service.EnergyClassDesc} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                        $"Experimente primeiro apagar todas as encomendas que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }

        }

        public IActionResult AutoPieceNotFound()
        {
            return View();
        }

    }
}
