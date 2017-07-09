using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;
using TheWorld.Models;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        IMailService _mailservice;
        private IConfigurationRoot _config;
        private IWorldRepository _repo;
        private ILogger<AppController> _loger;

        public AppController(IMailService mailservice, 
            IConfigurationRoot config, 
            IWorldRepository context,
            ILogger<AppController> loger)
        {
            _mailservice = mailservice;
            _config = config;
            _repo = context;
            _loger = loger;
        }


        public IActionResult Index()
        {
            try
            {
                var trips = _repo.GetAllTrips();
                return View(trips);
            }
            catch (Exception ex)
            {
                _loger.LogError(ex.ToString());
                return Redirect("/error ");
            }            
        }

        public IActionResult DynamicMenu()
        {
            var generatedMenu = GetMenu();
            var vm = new DynamicMenuViewModel() { menu = generatedMenu };
            return View(vm);
        }

        private Menu GetMenu()
        {
            var menu = new Menu();
            var menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem() { LinkText = "Home", ActionName = "Index", ControllerName = "App" });
            menuItems.Add(new MenuItem() { LinkText = "About", ActionName = "About", ControllerName = "App" });
            menuItems.Add(new MenuItem() { LinkText = "Contact", ActionName = "Contact", ControllerName = "App" });
            menu.menuItems = menuItems;

            return menu;
        }




        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("", "We dont support aol.com");
            }

            if (ModelState.IsValid)
            {
                _mailservice.SendMail(_config["MailSettings:ToAddress"], model.Email, "ms", model.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";

            }            

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
