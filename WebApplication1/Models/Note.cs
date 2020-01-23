using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Note
    {
        public int ID { get; set; }
        public String DateLastVisite { get; set; }
        public String note { get; set; }
        public String CommentaireNote { get; set; }
        public int RestaurantID { get; set; }
    }
}
