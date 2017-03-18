using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldUser : IdentityUser //the base class IdentityUser will add many classes (logins, tokens, etc.) 
                                          //this classes will appear as columns in the database
                                          //also we can extend it adding our own classes
    {
        public string FirstTrip { get; set; }

    }
}
