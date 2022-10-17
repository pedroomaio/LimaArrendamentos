using LimaArrendamentos.Data;
using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Helpers;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LimaArrendamentos.Controllers
{
    public class RealtyController : Controller
    {
        private readonly DataContext _context;
        private readonly IRealtyRepository _realtyRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IAdvertHelper _advertHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ITypologyRepository _typologyRepository;
        private readonly IEnergyClassRepository _energyClassRepository;
        private readonly ISendEmailRepository _sendGmailRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPropertyTypeRepository _propertyTypeRepository;

        public RealtyController(
            DataContext context,
            IRealtyRepository realtyRepository,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            ITypologyRepository typologyRepository,
            IEnergyClassRepository energyClassRepository,
            ISendEmailRepository sendGmailRepository,
            IFavoriteRepository favoriteRepository,
            IPropertyTypeRepository propertyTypeRepository,
            IBlobHelper blobHelper,
            IAdvertHelper advertHelper)
        {
            _context = context;
            _realtyRepository = realtyRepository;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _typologyRepository = typologyRepository;
            _energyClassRepository = energyClassRepository;
            _sendGmailRepository = sendGmailRepository;
            _favoriteRepository = favoriteRepository;
            _propertyTypeRepository = propertyTypeRepository;
            _blobHelper = blobHelper;
            _advertHelper = advertHelper;
        }
        
        bool IsUsernameDetails = false;
        
        // GET: Products
        [Authorize(Roles = "Admin, Cliente")]
        public IActionResult Index(/*bool? IsUsername*/)
        {

            if (User.IsInRole("Admin"))
            {
                if (TempData["UserNameLease"] != null)
                {
                    ViewBag.UserName = TempData["UserNameLease"];

                    TempData["UserNameLease"] = null;
                }

                return View(_realtyRepository.GetAll().OrderBy(p => p.CreatedInSite));

            }
            else 
            {

                TempData["UserNameLease"] = null;

                return View(_realtyRepository.GetAll().Where(p => p.User.Email == User.Identity.Name).OrderBy(p => p.CreatedInSite));

            }


            //if (IsUsername == true)
            //{
            //    if (TempData["UserNameLessee"] != null)
            //    {
            //        ViewBag.UserName = TempData["UserNameLessee"];

            //        TempData["UserNameLessee"] = null;
            //    }
            //}
            //else
            //{
            //    TempData["UserNameLessee"] = null;
            //}
            ////if (User.IsInRole("Admin"))
            ////{
            //return View(_realtyRepository.GetAll().OrderBy(p => p.CreatedInSite));
            ////}
            ////else
            ////{
            ////    return View(_realtyRepository.GetAll().Where(p => p.User.Email == User.Identity.Name).OrderBy(p => p.CreatedInSite));
            ////}
        }

        public async Task<IActionResult> Index1(string Typology)
        {          
            if (Typology != null)
            {
                var a = _realtyRepository.GetAllWithPrecomin(Typology);
                return View(a);
            }

            
            var aa = _realtyRepository.GetAll();

            await _advertHelper.GetAdApi();

            return View(_realtyRepository.GetAll().OrderBy(p => p.CreatedInSite));           
        }

        // GET: Products/Details/5
        //[Authorize(Roles = "Admin, Cliente")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("RealtyNotFound");
            }

            var realty = await _realtyRepository.GetByIdAsync(id.Value);
            if (realty == null)
            {
                return new NotFoundViewResult("RealtyNotFound");
            }

            foreach (var item in _realtyRepository.GetAll().Where(p => p.User.Email == User.Identity.Name))
            {
                if (id == item.Id)
                {
                    var model = _converterHelper.ToRealtiesViewModel(realty);
                    return View(model);
                }
            }

            return View(realty);
        }


        //// GET: Products/Create
        [Authorize(Roles = "Cliente")]
        public IActionResult Create()
        {

            var model = new RealtyViewModel
            {
                Typologies = _typologyRepository.GetComboTypologies(),
                EnergiesClass = _energyClassRepository.GetComboEnergyClass(),
                PropertyTypes = _propertyTypeRepository.GetComboPropertyType(),
            };

            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Create(RealtyViewModel modelView)
        {

            if (ModelState.IsValid)
            {

                Guid imageId = Guid.Empty;

                if (modelView.ImageFile != null && modelView.ImageFile.Length > 0)
                {

                    imageId = await _blobHelper.UploadBlobAsync(modelView.ImageFile, "realties");
                }

                //tipologia
                var typology = await _typologyRepository.GetTypologiesAsync(modelView.TypologyId);

                modelView.Typology = typology.TypologyDesc;
                modelView.TypologyId = typology.Id;

                var a = modelView.price;


                //classe energetica
                var energyClass = await _energyClassRepository.GetEnergyClassAsync(modelView.EnergyClassId);

                modelView.EnergyClass = energyClass.EnergyClassDesc;
                modelView.EnergyClassId = energyClass.Id;


                //tipo de propriedade
                var propertyType = await _propertyTypeRepository.GetPropertyTypeAsync(modelView.PropertyTypeId);

                modelView.PropertyType = propertyType.PropertyTypeDesc;
                modelView.PropertyTypeId = propertyType.Id;


                var realty = _converterHelper.ToRealty(modelView, imageId, true, User.Identity.Name);

                realty.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _realtyRepository.CreateAsync(realty);


                return RedirectToAction(nameof(Index));
            }
            return View(modelView);
        }



        //// GET: Products/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return new NotFoundViewResult("RealtyNotFound");
            }

            var realty = await _realtyRepository.GetByIdAsync(id.Value);



            if (realty == null)
            {
                return new NotFoundViewResult("RealtyNotFound");
            }
            foreach (var item in _realtyRepository.GetAll().Where(p => p.User.UserName == User.Identity.Name))
            {
                if (id == item.Id)
                {
                    var model = _converterHelper.ToRealtiesViewModel(realty);

                    var typology = await _typologyRepository.GetByIdAsync(realty.TypologyId);
                    if (typology != null)
                    {
                        model.Typologies = _typologyRepository.GetComboTypologies();
                    }
                    var propertyType = await _propertyTypeRepository.GetByIdAsync(realty.PropertyTypeId);
                    if (propertyType != null)
                    {
                        model.PropertyTypes = _propertyTypeRepository.GetComboPropertyType();

                    }
                    var energyClass = await _energyClassRepository.GetByIdAsync(realty.EnergyClassId);
                    if (energyClass != null)
                    {
                        model.EnergiesClass = _energyClassRepository.GetComboEnergyClass();

                        return View(model);
                    }
                    
                }
            }


            return new NotFoundViewResult("RealtyNotFound");
        }


        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Edit(RealtyViewModel modelViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    Guid imageId = modelViewModel.ImageId;

                    if (modelViewModel.ImageFile != null && modelViewModel.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(modelViewModel.ImageFile, "realties");
                    }

                    var typology = await _typologyRepository.GetTypologiesAsync(modelViewModel.TypologyId);

                    modelViewModel.Typology = typology.TypologyDesc;
                    modelViewModel.TypologyId = typology.Id;


                    var energyClass = await _energyClassRepository.GetEnergyClassAsync(modelViewModel.EnergyClassId);

                    modelViewModel.EnergyClass = energyClass.EnergyClassDesc;
                    modelViewModel.EnergyClassId = energyClass.Id;

                    var propertyType = await _propertyTypeRepository.GetPropertyTypeAsync(modelViewModel.PropertyTypeId);

                    modelViewModel.PropertyType = propertyType.PropertyTypeDesc;
                    modelViewModel.PropertyTypeId = propertyType.Id;

                    var realty = _converterHelper.ToRealty(modelViewModel, imageId, false, User.Identity.Name);


                    //TODO: Modificar para o user que tiver logado
                    realty.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _realtyRepository.UpdateAsync(realty);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _realtyRepository.ExistAsync(modelViewModel.Id))
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
            return View(modelViewModel);
        }

        //// GET: Products/Delete/5

        [Authorize(Roles = "Admin, Cliente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("RealtyNotFound");
            }


            var realty = await _realtyRepository.GetByIdAsync(id.Value);
            if (realty == null)
            {
                return new NotFoundViewResult("RealtyNotFound");
            }

            foreach (var item in _realtyRepository.GetAll().Where(p => p.User.UserName == User.Identity.Name))
            {
                if (id == item.Id)
                {
                    var model = _converterHelper.ToRealtiesViewModel(realty);
                    return View(model);
                }
            }
            return View(realty);
        }

        //// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin, Cliente")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realty = await _realtyRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _realtyRepository.DeleteAsync(realty);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{realty.Address} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{realty.Address} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                        $"Experimente primeiro apagar todas as encomendas que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }

        }

        public IActionResult RealtyNotFound()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(int id)
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            var service = new SendEmail();
            var sendGmail = _sendGmailRepository.GetAll();
            var realtyUsername = "";
            bool IsValid = false;

            foreach (var item in _realtyRepository.GetAll())
            {
                if (item.Id == id)
                {
                    realtyUsername = item.Username;
                }
            }


            if (!sendGmail.Any())
            {

                service = _sendGmailRepository.ToSendEmail(user.Id, id);
                await _sendGmailRepository.CreateAsync(service);

                return Redirect($"mailto:{realtyUsername}?subject = Feedback&body = Message");
            }
            else
            {
                foreach (var item in sendGmail)
                {
                    if (item.UserId == user.Id)
                    {
                        if (item.RealtyId != id)
                        {

                            service = _sendGmailRepository.ToSendEmail(user.Id, id);
                            IsValid = true;
                        }
                    }
                    if (item.UserId != user.Id)
                    {
                        service = _sendGmailRepository.ToSendEmail(user.Id, id);
                        IsValid = true;
                    }
                }
                if (IsValid)
                {
                    await _sendGmailRepository.CreateAsync(service);
                    return Redirect($"mailto:{realtyUsername}?subject = Feedback&body = Message");
                }
            }


            return Redirect($"mailto:{realtyUsername}?subject = Feedback&body = Message");
        }


        public async Task<IActionResult> Favorite(int id)
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            var service = new Favorite();
            var favorites = _favoriteRepository.GetAll();
            bool IsValid = false;
            var userRealty = new Realty();


            if (!favorites.Any())
            {
                service = _favoriteRepository.ToFavorite(user.Id, id);
                await _favoriteRepository.CreateAsync(service);


                foreach (var item in _favoriteRepository.GetAll())
                {
                    if (item.Username == User.Identity.Name)
                    {
                        var a = _realtyRepository.GetAllWithFavorites(item.Username);
                        return View(a);
                    }
                }

            }
            else
            {
                foreach (var item in favorites)
                {
                    if (item.Username == user.UserName)
                    {
                        if (item.RealtyId != id)
                        {

                            service = _favoriteRepository.ToFavorite(user.Id, id);
                            IsValid = true;
                        }
                    }

                    if (item.Username == user.UserName)
                    {
                        if (item.RealtyId == id)
                        {
                            return RedirectToAction("Remove", "realty", new { UserId = item.Username, item.RealtyId });
                        }
                    }
                    if (item.Username != user.UserName)
                    {
                        service = _favoriteRepository.ToFavorite(user.Id, id);
                        IsValid = true;
                    }
                }
                if (IsValid)
                {
                    await _favoriteRepository.CreateAsync(service);



                    foreach (var item in _favoriteRepository.GetAll())
                    {
                        if (item.Username == User.Identity.Name)
                        {
                            var a = _realtyRepository.GetAllWithFavorites(item.Username);
                            return View(a);
                        }
                    }

                }
            }

            foreach (var item in _favoriteRepository.GetAll())
            {
                if (item.Username == User.Identity.Name)
                {
                    var a = _realtyRepository.GetAllWithFavorites(item.Username);
                    return View(a);
                }
            }

            return View();
        }

        public async Task<IActionResult> Remove(string UserNameId, int RealtyId)
        {
            var favoriteRemove = new Favorite();

            foreach (var item in _favoriteRepository.GetAll())
            {
                if (item.Username == UserNameId)
                {
                    if (item.RealtyId == RealtyId)
                    {
                        favoriteRemove = item;
                    }
                }
            }

            await _favoriteRepository.DeleteAsync(favoriteRemove);
            return RedirectToAction("Favorite", "realty");
        }

        public IActionResult LeaseUser(int id)
        {
            var sendEmail = _sendGmailRepository.GetAll();
            List<string> userId = new List<string>();
            List<string> userName = new List<string>();

            foreach (var item in sendEmail)
            {
                if (item.RealtyId == id)
                {
                    userId.Add(item.UserId);
                }
            }

            foreach (var item in _userHelper.GetAll())
            {
                foreach (var itemUser in userId)
                {
                    if (item.Id == itemUser)
                    {
                        userName.Add(item.UserName);
                    }
                }
            }

            if (userName.Count > 0)
            {
                TempData["UserNameLease"] = userName;
            }
            return RedirectToAction("Index", "realty", new { IsUsername = true });
        }

        public IActionResult Filter(int? precoMin, int? precoMax, int? areaMin, int? areaMax, string typology, string energyClass)
        {
            return RedirectToAction("Index1", "realty", new { Typology = typology });
        }


        public  IActionResult OrderByPrice(RealtyViewModel model)
        {
            _realtyRepository.GetPrice();

            return View(model);
        }
    }
}
