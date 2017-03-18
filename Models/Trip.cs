using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class Trip  //we need to create 2 models (entities): Trip and Stops
    {
        public int Id { get; set; }
        public string Name { get; set; }  //trip name   
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }  //we can get trips for individual users


        public ICollection<Stop> Stops { get; set; } //as we will want to add and remove stops we can't use a IEnumerable (as is read only)
    }
}
