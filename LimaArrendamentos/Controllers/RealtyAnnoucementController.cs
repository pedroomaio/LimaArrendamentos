//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using LimaArrendamentos.Data;
//using LimaArrendamentos.Helpers;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace AutoRepair.Controllers
//{
//    [Authorize]
//    public class InspecionController : Controller
//    {
//        private readonly IRealtyRepository _realtyRepository;
//        private readonly ITypologyRepository _typologyRepository;
//        private readonly IUserHelper _userHelper;
//        private readonly IEnergyClassRepository _energyClassRepository;
//        //private readonly IRealtyAnnoucementHelper _realtyAnnoucementHelper;
//        //private readonly IRealtyAnnoucementRepository _realtyAnnoucementRepository;
//        private readonly IMailHelper _mailHelper;



//        public InspecionController(
//            IRealtyRepository realtyRepository,
//             IUserHelper userHelper,
//            ITypologyRepository typologyRepository,
//            IEnergyClassRepository energyClassRepository,
//            //IRealtyAnnoucementHelper realtyAnnoucementHelper,
//            //IRealtyAnnoucementRepository realtyAnnoucementRepository,
//            IMailHelper mailHelper)
//        {
//            _realtyRepository = realtyRepository;
//            _userHelper = userHelper;
//            _typologyRepository = typologyRepository;
//            _energyClassRepository = energyClassRepository;
//            //_inspecionHelper = inspecionHelper;
//            //_inspecionRepository = inspecionRepository;
//            _mailHelper = mailHelper;
//        }

//        public async Task<IActionResult> Index()
//        {
//            ViewBag.StatusValue = "";
//            foreach (var item in _inspecionRepository.GetAll())
//            {
//                if (item != null)
//                {
//                    if (User.IsInRole("Mechanic"))
//                    {
//                        if (item.Status == "In Processing")
//                        {
//                            ViewBag.StatusValue = "In Processing";
//                            var modelInProcessing = _inspecionRepository.GetOrderInProcessing();
//                            return View(modelInProcessing);
//                        }
//                    }
//                    if (User.IsInRole("Employee"))
//                    {
//                        if (item.Status == "Completed" || item.Status == "Finished" || item.Status == "Pending")
//                        {
//                            ViewBag.StatusValue = "PendingOrCompletedOrFinished";
//                            var modelCompleted = _inspecionRepository.GetOrderCompleted();
//                            return View(modelCompleted);
//                        }
//                    }
//                    //if (item.Status == "In Processing")
//                    //{
//                    //    ViewBag.StatusValue = "In Processing";
//                    //    var a = _inspecionRepository.GetOrderInProcessingAsync();
//                    //    return View(a);
//                    //}

//                }
//            }

//            var model = await _inspecionRepository.GetOrderAsync(this.User.Identity.Name);

//            //if (TempData["InspecionStatus"] == "Sucess")
//            //{
//            //    ViewBag.Inspecion = "Inspection created successfully, wait now until it is scheduled (you will receive other gmail)";
//            //    TempData["InspecionStatus"] = "aaa";
//            //}
//            return View(model);
//        }
//        public async Task<IActionResult> Creater()
//        {
//            var model = await _inspecionRepository.GetDetailTempsAsync(this.User.Identity.Name);
//            return View(model);
//        }
//        public IActionResult AddProduct(int? Id)
//        {
//            var a = Request.QueryString.ToString();

//            if (Id != null)
//            {
//                if (Id == 1)
//                {
//                    var list = new List<Service>();
//                    foreach (var item in _serviceRepository.GetAll())
//                    {
//                        if (item.ServiceName == "Revision")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Oil Change")
//                        {
//                            list.Add(item);
//                        }


//                    }
//                    ViewBag.serviceType = list;

//                }
//                if (Id == 2)
//                {
//                    var list = new List<Service>();
//                    foreach (var item in _serviceRepository.GetAll())
//                    {
//                        if (item.ServiceName == "Revision")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Oil Change")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Battery Checkup")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Braking")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Visibility")
//                        {
//                            list.Add(item);
//                        }


//                    }
//                    ViewBag.serviceType = list;
//                }
//                if (Id == 3)
//                {
//                    var list = new List<Service>();
//                    foreach (var item in _serviceRepository.GetAll())
//                    {
//                        if (item.ServiceName == "Revision")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Oil Change")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Battery Checkup")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Braking")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Air Conditioning")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Suspension")
//                        {
//                            list.Add(item);
//                        }


//                    }
//                    ViewBag.serviceType = list;
//                }
//            }

//            ViewBag.serviceTypeAll = _serviceRepository.GetAll();

//            var model = new AddItemViewModel
//            {
//                Cars = _carRepository.GetComboCars(),
//            };

//            return View(model);
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddProduct(AddItemViewModel model)
//        {
//            if (ModelState.IsValid)
//            {

//                var arr = TempData["service"] as string[];
//                int[] ints = Array.ConvertAll(arr, int.Parse);


//                var inspecion = _inspecionHelper.ToInspecion(model);

//                inspecion.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

//                double totalPrice = 0;
//                var services = _serviceRepository.GetAll();
//                if (ints != null)
//                {
//                    foreach (var item in services)
//                    {
//                        for (int i = 0; i < ints.Length; i++)
//                        {

//                            if (ints[i] == item.Id)
//                            {
//                                totalPrice += item.Price;
//                            }
//                        }
//                    }
//                }

//                model.Price = totalPrice;

//                await _inspecionRepository.AddItemToOrderAsync(model, this.User.Identity.Name);
//                return RedirectToAction("Creater");
//            }

//            return View(model);
//        }


//        public async Task<IActionResult> DeleteItem(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            await _inspecionRepository.DeleteDetailTempAsync(id.Value);
//            return RedirectToAction("Creater");
//        }

//        public async Task<IActionResult> ConfirmOrder()
//        {
//            var response = await _inspecionRepository.ConfirmOrderAsync(this.User.Identity.Name);
//            if (response)
//            {


//                _mailHelper.SendEmail(this.User.Identity.Name, "Scheduled inspection Success Created",

//                      $"\nInspection created successfully, wait now until it is scheduled (you will receive other gmail)</h2>");

//                return RedirectToAction("Index");
//            }

//            return RedirectToAction("Creater");
//        }


//        public async Task<IActionResult> Deliver(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var order = await _inspecionRepository.GetOrderAsync(id.Value);
//            if (order == null)
//            {
//                return NotFound();
//            }

//            ViewBag.Status = order.Status;


//            var model = new MarkViewModel
//            {
//                Id = order.Id,
//                DeliveryDate = order.InspecionDateStart,
//                InspecionHours = order.InspecionHours,
//                Status = order.Status
//            };




//            TempData["InspecionDate"] = order.InspecionDate;
//            TempData["InspecionHours"] = order.InspecionHours;




//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Deliver(MarkViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                await _inspecionRepository.DeliverOrder(model);


//                //var user = await _inspecionRepository.ConfirmOrderAsync(this.User.Identity.Name);



//                return RedirectToAction("Index");
//            }



//            return View();
//        }










//        public IActionResult Create(int? Id)
//        {
//            var a = Request.QueryString.ToString();

//            if (Id != null)
//            {
//                if (Id == 1)
//                {
//                    var list = new List<Service>();
//                    foreach (var item in _serviceRepository.GetAll())
//                    {
//                        if (item.ServiceName == "Revision")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Oil Change")
//                        {
//                            list.Add(item);
//                        }


//                    }
//                    ViewBag.serviceType = list;

//                }
//                if (Id == 2)
//                {
//                    var list = new List<Service>();
//                    foreach (var item in _serviceRepository.GetAll())
//                    {
//                        if (item.ServiceName == "Revision")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Oil Change")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Battery Checkup")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Braking")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Visibility")
//                        {
//                            list.Add(item);
//                        }


//                    }
//                    ViewBag.serviceType = list;
//                }
//                if (Id == 3)
//                {
//                    var list = new List<Service>();
//                    foreach (var item in _serviceRepository.GetAll())
//                    {
//                        if (item.ServiceName == "Revision")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Oil Change")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Battery Checkup")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Braking")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Air Conditioning")
//                        {
//                            list.Add(item);
//                        }
//                        if (item.ServiceName == "Suspension")
//                        {
//                            list.Add(item);
//                        }


//                    }
//                    ViewBag.serviceType = list;
//                }
//            }


//            var car = _carRepository.GetAll().Where(p => p.User.Email == User.Identity.Name);
//            var inspecionViewModel = new InspecionViewModel();
//            foreach (var item in car)
//            {
//                inspecionViewModel.Brand = item.Brand;
//                inspecionViewModel.Model = item.Model;
//                inspecionViewModel.RegisterCar = item.RegisterCar;
//            }
//            ViewBag.serviceTypeAll = _serviceRepository.GetAll();


//            return View(inspecionViewModel);
//        }


//        [HttpPost]

//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(AddItemViewModel model)
//        {
//            var arr = TempData["service"] as string[];
//            int[] ints = Array.ConvertAll(arr, int.Parse);


//            var inspecion = _inspecionHelper.ToInspecion(model);

//            inspecion.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

//            double totalPrice = 0;
//            var services = _serviceRepository.GetAll();
//            if (ints != null)
//            {
//                foreach (var item in services)
//                {
//                    for (int i = 0; i < ints.Length; i++)
//                    {

//                        if (ints[i] == item.Id)
//                        {
//                            totalPrice += item.Price;
//                        }
//                    }
//                }
//            }

//            //model.Price = totalPrice;

//            foreach (var item in _carRepository.GetAll().Where(p => p.User.Email == User.Identity.Name))
//            {
//                model.CarId = item.Id;
//            }
//            var product = _inspecionHelper.ToInspecion(model);

//            product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

//            //await _inspecionRepository.ConfirmOrderAsync(model, this.User.Identity.Name);
//            return RedirectToAction("Create");

//            await _inspecionRepository.CreateAsync(inspecion);
//            ModelState.AddModelError(string.Empty, "Sucess to create inspecion, please wait!\n\n" +
//                "when your inspection has a mark created you will receive an email");


//            ViewBag.message = "error";
//            ModelState.AddModelError(string.Empty, "Failed to create inspecion!");
//            return View();
//        }


//        //public Task<IActionResult> Index()
//        //{
//        //    var city = await _modelRepository.GetBrandAsync(user.CityId);
//        //    if (city != null)
//        //    {
//        //        var country = await _modelRepository.GetModelAsync(city);
//        //        if (country != null)
//        //        {
//        //            model.CountryId = country.Id;
//        //            model.Cities = _modelRepository.GetComboCities(country.Id);
//        //            model.Countries = _modelRepository.GetComboCountries();
//        //            model.CityId = user.CityId;
//        //        }
//        //    }
//        //}


//        [HttpPost]
//        public IActionResult GetServices(string ItemList)
//        {
//            string[] arr = ItemList.Split(',');

//            TempData["service"] = arr;

//            return this.RedirectToAction("AddProduct");
//        }
//    }
//}
