using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RestaurantsIndexViewModel
    {
        public string Prenom { get; set; }
        public string Prenom_amie { get; set; }
        public string Nom { get; set; }
        public string Numtel { get; set; }
        public string Commentaire { get; set; }
        public string Mail { get; set; }
        public Address address { get; set; }
        public int Restaurant_ID { get; set; }
        public String Restaurant { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public String DateLastVisite { get; set; }
        public String note { get; set; }
        public String CommentaireNote { get; set; }
        public List<Note> Notes { get; set; }
    }
}
