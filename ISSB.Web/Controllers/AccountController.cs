using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISSB.Web.Models;
using ISSB.Web.Models.Helper;
using ISSB.Web.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISSB.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;

        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home"); 
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);

                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login.");

            return this.View(model);
        }

        public IActionResult Register()
        {
            //var model = new RegisterNewUserViewModel
            //{
            //    Countries = this.countryRepository.GetComboCountries(),
            //    Cities = this.countryRepository.GetComboCities(0)
            //};

            //return this.View(model);

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    //var city = await this.countryRepository.GetCityAsync(model.CityId);

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        //CityId = model.CityId,
                        //City = city
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);

                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var loginViewModel = new LoginViewModel
                    {
                        Username = model.Username,
                        Password = model.Password,
                        Rememberme = false
                    };

                    var result2 = await _userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError(string.Empty, "Este usuario no puede hacer login");
                    return this.View(model);

                    //var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    //var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    //{
                    //    userid = user.Id,
                    //    token = myToken
                    //}, protocol: HttpContext.Request.Scheme);

                    //this.mailHelper.SendMail(model.Username, "Shop Email confirmation", $"<h1>Shop Email Confirmation</h1>" +
                    //    $"To allow the user, " +
                    //    $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    //this.ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    //return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return this.View(model);
        }

        //public async Task<IActionResult> ChangeUser()
        //{
        //    var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //    var model = new ChangeUserViewModel();

        //    if (user != null)
        //    {
        //        model.FirstName = user.FirstName;
        //        model.LastName = user.LastName;
        //        model.Address = user.Address;
        //        model.PhoneNumber = user.PhoneNumber;

        //        var city = await this.countryRepository.GetCityAsync(user.CityId);
        //        if (city != null)
        //        {
        //            var country = await this.countryRepository.GetCountryAsync(city);
        //            if (country != null)
        //            {
        //                model.CountryId = country.Id;
        //                model.Cities = this.countryRepository.GetComboCities(country.Id);
        //                model.Countries = this.countryRepository.GetComboCountries();
        //                model.CityId = user.CityId;
        //            }
        //        }
        //    }

        //    model.Cities = this.countryRepository.GetComboCities(model.CountryId);
        //    model.Countries = this.countryRepository.GetComboCountries();
        //    return this.View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //        if (user != null)
        //        {
        //            var city = await this.countryRepository.GetCityAsync(model.CityId);

        //            user.FirstName = model.FirstName;
        //            user.LastName = model.LastName;
        //            user.Address = model.Address;
        //            user.PhoneNumber = model.PhoneNumber;
        //            user.CityId = model.CityId;
        //            user.City = city;

        //            var respose = await this.userHelper.UpdateUserAsync(user);
        //            if (respose.Succeeded)
        //            {
        //                this.ViewBag.UserMessage = "User updated!";
        //            }
        //            else
        //            {
        //                this.ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
        //            }
        //        }
        //        else
        //        {
        //            this.ModelState.AddModelError(string.Empty, "User no found.");
        //        }
        //    }

        //    return this.View(model);
        //}

        //public IActionResult ChangePassword()
        //{
        //    return this.View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //        if (user != null)
        //        {
        //            var result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return this.RedirectToAction("ChangeUser");
        //            }
        //            else
        //            {
        //                this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
        //            }
        //        }
        //        else
        //        {
        //            this.ModelState.AddModelError(string.Empty, "User no found.");
        //        }
        //    }

        //    return this.View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        var user = await this.userHelper.GetUserByEmailAsync(model.Username);
        //        if (user != null)
        //        {
        //            var result = await this.userHelper.ValidatePasswordAsync(
        //                user,
        //                model.Password);

        //            if (result.Succeeded)
        //            {
        //                var claims = new[]
        //                {
        //                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //                };

        //                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
        //                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //                var token = new JwtSecurityToken(
        //                    this.configuration["Tokens:Issuer"],
        //                    this.configuration["Tokens:Audience"],
        //                    claims,
        //                    expires: DateTime.UtcNow.AddDays(15),
        //                    signingCredentials: credentials);
        //                var results = new
        //                {
        //                    token = new JwtSecurityTokenHandler().WriteToken(token),
        //                    expiration = token.ValidTo
        //                };

        //                return this.Created(string.Empty, results);
        //            }
        //        }
        //    }

        //    return this.BadRequest();
        //}

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}