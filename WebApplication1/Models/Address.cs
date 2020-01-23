using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Address
    {
        public int ID { get; set; } //ne pas oublier la primary key sinon il ne génère pas en auto
        public String Rue { get; set; }
        public String Ville { get; set; }
        public String CodePostal { get; set; }
        public int RestaurantID { get; set; }
    }
}
