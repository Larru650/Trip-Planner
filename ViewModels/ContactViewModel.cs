using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{ //we use a class to define the data as we need to use model binding. accepts the method from the form inside the method of the controller
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(4096, MinimumLength = 4)]
        public string Message { get; set; }
    }
}
