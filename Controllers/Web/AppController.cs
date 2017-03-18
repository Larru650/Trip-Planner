using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailServices _mailService;
        private IConfigurationRoot _config; //also here we get it on the constructor but we store it at the class level just in case we need it elsewhere
        private IWorldRepository _repository; //this member will allow us to use the repository instead of the context directly into the action method
        private ILogger<AppController> _logger;

        public AppController(IMailServices mailService, IConfigurationRoot config, IWorldRepository repository, ILogger<AppController> logger) //world context can now be added in the constructor as is injectable (after adding it into services)
        {//when we inject the worldrepository into the constructor we are allowing the dependency injection layer to determine everything we need.
            //we need the interface for world repository that knows that we need the class worldrepository. for this to be created needs a
            //world context class and this one needs a Iconfigurationroot and dbcontextoptions(injected in worldcontext class


            _mailService = mailService; //is going to take the implementation that works, this way when the controller gets a request
                                        //it will look first at this constructor and implement the interface to use this service
                                        //this also has to be registered i  n Startup.cs as a service!
            _config = config;

            _repository = repository;

            _logger = logger; //this will allow us to trap errors (try and catch if something happens with my queries and use the logger to log what kind of message we get)
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize] //a gate in front of the trips for only authenticated users
        public IActionResult Trips()
        {


               /* var data = _repository.GetAllTrips(); not gonna use it anymore as we will acess the api through our client side code  */   /*context.Trips.ToList(); *///context will query for the trips on the database and pass it into the view as a list
                return View(); //we will just return the tripsView.html as our markup (client side code)



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
                ModelState.AddModelError("Email", "We do not support AOL.COM email addresses"); //we can also use an empty string corresponding to
                                                                                //the model property to add the error as a model error and not  property error
            }

            if (ModelState.IsValid) { //validates the errors that may ocurr in the Client part { 

            _mailService.Sendmail(_config["MailSettings:ToAddress"], model.Email, "First email the world", model.Message);

                ModelState.Clear(); //will clear the page before showing the message, but after sending the message

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
