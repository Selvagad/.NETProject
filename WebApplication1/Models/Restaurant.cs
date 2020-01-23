using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public String Nom { get; set; }
        public String Numtel { get; set; }
        public String Commentaire { get; set; }
        public String Mail { get; set; }
        public Address address { get; set; }
        public Note note { get; set; }
        public int Restaurant_ID { get; set; }
    }
}
