using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Controllers.Api
{
    public class TripViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}



//Do we need to use ViewModels or we can just stay with Entities? That depends upon the size of the project and how you
//want to expose the contract. The shape of the data is part of this contract. We can hide it using viewmodel