using LimaArrendamentos.Data;
using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Helpers;
using LimaArrendamentos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LimaArrendamentos.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IConfiguration _configuration;
        private readonly IBlobHelper _blobHelper;

        public AccountController(
            IUserHelper userHelper,
            IMailHelper mailHelper,
            IConfiguration configuration,
            IBlobHelper blobHelper)
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _configuration = configuration;
            _blobHelper = blobHelper;
        }


        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if (user == null)
                {
                    ViewBag.Message = "Utilizador não registado";
                    return View(model);
                }

                var IsInRole = await _userHelper.IsUserInRoleAsync(user, "Staff");

                var result = await _userHelper.LoginAsync(model);

                if (IsInRole)
                {
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Dashboard");

                    }

                    return this.RedirectToAction("Index", "Dashboard");
                }


                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            ViewBag.message = "Erro ao entrar, verifique o e-mail e/ou palavra-passe";
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Username,
                        UserName = model.Username,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        CCnumber = model.CCnumber,
                        NIF = model.NIF,
                        AgreeTerm = model.AgreeTerm,
                        
                    };


                    var result = await _userHelper.AddUserAsync(user, model.Password);

                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "O utilizador não pode ser criado, por favor tente mais tarde");
                        return View(model);
                    }

                    await _userHelper.AddUserToRoleAsync(user, "Cliente");

                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    string tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    Response response = _mailHelper.SendEmail(model.Username, "LimArrendamentos - Registo",
                        $"<h1>Confirmação de Conta</h1>" +
                        $"Clique aqui para confirmar a sua conta:" +
                        $"<br /><br /><a href = \"{tokenLink}\">Confirmar Conta</a>");


                    if (response.IsSuccess)
                    {
                        ViewBag.message = "E-mail enviado, por favor confirme a sua conta";
                        return View(model);
                    }
                    else
                    {
                        ViewBag.message = "Utilizador já existente";
                        return View(model);
                    }

                }
            }

            return View(model);
        }



        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.DateOfBirth = user.DateOfBirth;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;
                model.CCnumber = user.CCnumber;
                model.NIF = user.NIF;
                model.ProfilePic = user.ProfilePic;
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

                Guid imageId = Guid.Empty;

                if (user != null)
                {
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                        user.ProfilePic = imageId;
                    }
                    else
                    {
                        user.ProfilePic = user.ProfilePic;
                    }

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.DateOfBirth = model.DateOfBirth;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.CCnumber = model.CCnumber;
                    user.NIF = model.NIF;

                    var response = await _userHelper.UpdateUserAsync(user);

                    if (response.Succeeded)
                    {
                        ViewBag.Message = "Perfil atualizado";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
            }

            return RedirectToAction($"ChangeUser", new { id = model.Id });
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    
                    if (result.Succeeded)
                    {
                        ViewBag.Message = "A palavra-passe foi atualizada";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }

                return View(model);
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return BadRequest();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();

        }

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "O email não corresponde a um utilizador registado.");
                    return View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendEmail(model.Email, "LimArrendamentos - Recuperação de palavra-passe",
                    $"<h1>Recuperação da palavra-passe</h1>" +
                    $"Para recuperar a sua palavra-passe clique aqui:<br /><br />" +
                    $"<a href = \"{link}\">Recuperar Palavra-passe</a>");

                if (response.IsSuccess)
                {
                    this.ViewBag.Message = "As instruções para alterar a sua palavra-passe foram enviadas para o seu email.";
                }

                return this.View();

            }

            return this.View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Palavra-passe mudada com sucesso";
                    return View();
                }

                this.ViewBag.Message = "Houve um problema a realizar a operação, por favor tente mais tarde";
                return View(model);
            }

            this.ViewBag.Message = "O utilizador não foi encontrado";
            return View(model);
        }



        public IActionResult NotAuthorized()
        {
            return View();
        }


        //[HttpPost]
        //[Route("Account/GetCitiesAsync")]
        //public async Task<JsonResult> GetCitiesAsync(int countryId)
        //{
        //    var country = await _countryRepository.GetCountryWithCitiesAsync(countryId);
        //    return Json(country.Cities.OrderBy(c => c.Name));
        //}
    }
}
