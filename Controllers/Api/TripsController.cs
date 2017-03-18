using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")] //we centralise the route here instead of adding it nexto to each action method
    [Authorize]
    public class TripsController : Controller //although we won't return views but data as an API with MVC6
    {
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger) //this constructor will allow us to inject the IWorldRepository into our controller
                                                                                             //also we can use an ILogger for our specific class and create an object
        {
            _repository = repository;
            _logger = logger;  //as always, we generate a  global field (up) from the local variable we injected in the constructor
        }

        [HttpGet("")]
        public IActionResult Get()  //Retrieval of data. we could use JsonResult, however this will not allow me to catch errors.Instead of returning "Json" we will return just a helper "Ok"
        {
            //old code //if (true) return BadRequest("Bad things happened");

            //            /* return Ok(new Trip() { Name = "My Trip", UserName = "Grack" }); *///to test this i deactivated the url launcher inside properties

            try
            {
                var results = _repository.GetTripsByUsername(User.Identity.Name); //we store the user that claims the trips into result

                                                                                   //the variable stores a collection of the trip entity

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results)); //as this is a get method, we are getting a collection //the mapper in startup also is configured to map from a collection of
                                                                            //entities, to a collection of viewmodels   
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get All Trips: {ex}");

                return BadRequest("Error occurred");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip) //! from body means: Please modelbind (map using the model) the data thats coming from the POST, and not from another URL(without this won't map it at all)
        {
            //modelbind,map it to this object by trying to match the json properties from the API!
            if (ModelState.IsValid)
            {
                // Save to the Database
                var newTrip = Mapper.Map<Trip>(theTrip);  //this maps the trip model into the "thetrip" object of type TripViewModel
                                                            //we need to configure this logic into startup

                newTrip.UserName = User.Identity.Name; //we assign the new created trip to the logged in user 

                _repository.AddTrip(newTrip);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));  //this returns and creates an object with an specified URI if the data is valid

                }
            }

            return BadRequest("Failed to save the trip"); //valud way to communicate the modelstate and any errors that are going down
        } 
    }
}
////we can call either an entity or a viewmodel. The viewmodel will hide the information to the client whereas the entity will show the parameters and the way our app is built

////when we use a viewmodel we need to convert this into an object (entity) that we can store into the database

////when we map create the object using an entity so we can store it to the database, we need to convert the viewmodel (if we use it)
////to entity (in this case from tripviewmodel to trip) and then create the object. However, we also need to convert it back to
////viewmodel, as otherwise we will be showing sensible contract shape to the client

//// viewmodel ---> map it so we can save it using model ---> viewmodel again with the newly created object 