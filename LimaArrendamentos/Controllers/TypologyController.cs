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
    public class TypologyController : Controller
    {
        private readonly ITypologyRepository _typologyRepository;
        private readonly IUserHelper _userHelper;

        public TypologyController(
            ITypologyRepository typologyRepository,
            IUserHelper userHelper)
        {
            _typologyRepository = typologyRepository;
            _userHelper = userHelper;
        }

        // GET: Typology
        public IActionResult Index()
        {
            return View(_typologyRepository.GetAll());
        }

        // GET: Typology/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var service = await _typologyRepository.GetByIdAsync(id.Value);
            if (service == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(service);
        }


        ////// GET: Typology/Create
        //[Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Typology/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypologyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                var service = _typologyRepository.ToService(model, true, user.Id);

                await _typologyRepository.CreateAsync(service);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        ////// GET: Typology/Edit/5
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var service = await _typologyRepository.GetByIdAsync(id.Value);
            if (service == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            var model = _typologyRepository.ToServiceViewModel(service, user.Id);
            return View(model);
        }


        ////// POST: Typology/Edit/5
        ////// To protect from overposting attacks, enable the specific properties you want to bind to.
        ////// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TypologyViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                    var service = _typologyRepository.ToService(model, false, user.Id);


                    await _typologyRepository.UpdateAsync(service);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _typologyRepository.ExistAsync(model.Id))
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

        ////// GET: Typology/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AutoPieceNotFound");
            }

            var car = await _typologyRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return new NotFoundViewResult("AutoPieceNotFound");
            }

            return View(car);
        }

        ////// POST: Typology/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _typologyRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _typologyRepository.DeleteAsync(service);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{service.TypologyDesc} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{service.TypologyDesc} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
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
