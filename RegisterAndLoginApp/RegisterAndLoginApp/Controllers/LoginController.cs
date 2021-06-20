using Microsoft.AspNetCore.Mvc;
using RegisterAndLoginApp.Models;
using RegisterAndLoginApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterAndLoginApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValid(userModel))
            {
                return View("LoginSuccess", userModel); //Two propreperty, first is the page i'm looking for, second is the data going to it
            }
            else
            {
                return View("LoginFailure", userModel);
            }
        }
    }
}
