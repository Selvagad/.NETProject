using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class RestaurantsController : Controller
    {
        private RestaurantsServices _restaurantsService;

        public RestaurantsController(RestaurantsServices restauSrv) //restauSrv a été créé par le sys d'injection de dependances (startup.cs)
        {
            this._restaurantsService = restauSrv;
        }
        public IActionResult Index()
        {
            var RestauMV = new RestaurantsIndexViewModel()
            {
                Restaurants = this._restaurantsService.getAll()
            };

            return View(RestauMV);
        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant resto = _restaurantsService.getRestobyid(id);

            if (resto == null)
            {
                return NotFound();
            }

            return View(resto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Restaurant resto = _restaurantsService.getRestobyid(id);
            if (resto == null)
            {
                return NotFound();
            }
            return View(resto);
        }
        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            _restaurantsService.deleteRestaurant(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant resto = _restaurantsService.getRestobyid(id);

            if (resto == null)
            {
                return NotFound();
            }
            return View(resto);
        }

        [HttpPost]
        public IActionResult Edit(int id, IFormCollection formCollection)
        {
            var r = _restaurantsService.getRestobyid(id);   //sinon on a un acces a la BD et il croit qu'on crée un objet qui existe deja
            r.Nom = formCollection["Nom"];
            r.Mail = formCollection["Mail"];
            r.Numtel = formCollection["Numtel"];
            r.Commentaire = formCollection["Commentaire"];
            r.address.Rue = formCollection["address.Rue"];
            r.address.Ville = formCollection["address.Ville"];
            r.address.CodePostal = formCollection["address.CodePostal"];
        //    Restaurant resto = new Restaurant
        //    {
        //        ID = id,    //Sinon il essaye de modif l'id 0 et pas 1
        //        Nom = formCollection["Nom"],
        //        Mail = formCollection["Mail"],
        //        Numtel = formCollection["Numtel"],
        //        Commentaire = formCollection["Commentaire"],
        //        address = new Address
        //        {
        //            ID = r.address.ID,
        //            RestaurantID = r.Restaurant_ID,
        //            Rue = formCollection["address.Rue"],
        //            Ville = formCollection["address.Ville"],
        //            CodePostal = formCollection["address.CodePostal"]
        //}
            //};

            this._restaurantsService.update(r);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection formCollection)
        {
            var restos = _restaurantsService.getAll();
            var newrestoId = 1;
            if (restos.Count() != 0)
            {
                newrestoId = restos.OrderByDescending(p => p.ID).First().ID+1;
            }

            Address a = new Address
            {
                Rue = formCollection["address.Rue"],
                Ville = formCollection["address.Ville"],
                CodePostal = formCollection["address.CodePostal"],
                RestaurantID = newrestoId
            };
            Note n = new Note
            {
                DateLastVisite = formCollection["note.DateLastVisite"],
                note = formCollection["note.note"],
                CommentaireNote = formCollection["note.CommentaireNote"],
                RestaurantID = newrestoId
            };
            Restaurant r = new Restaurant
            {
                Nom = formCollection["Nom"],
                Mail = formCollection["Mail"],
                Numtel = formCollection["Numtel"],
                Commentaire = formCollection["Commentaire"],
                address = a,
                Restaurant_ID = newrestoId
            };

            this._restaurantsService.createResto(r);
            this._restaurantsService.createNote(n);
            this._restaurantsService.createAddr(a);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> NoteIndex()
        {
            var RestauMV = new RestaurantsIndexViewModel()
            {
                Restaurants = this._restaurantsService.getAll()
            };

            return View(RestauMV);
        }

        public IActionResult NoteEdit(int id)
        {
            Restaurant resto = _restaurantsService.getRestobyid(id);
            return View(resto);
        }

        [HttpPost]
        public IActionResult NoteEdit(int id, IFormCollection formCollection)
        {
            var r = _restaurantsService.getRestobyid(id);   //sinon on a un acces a la BD et il croit qu'on crée un objet qui existe deja
            r.note.DateLastVisite = formCollection["note.DateLastVisite"];
            r.note.note = formCollection["note.note"];
            r.note.CommentaireNote = formCollection["note.CommentaireNote"];

            this._restaurantsService.update(r);
            return RedirectToAction(nameof(NoteIndex));
        }

        public async Task<IActionResult> MeilleurResto()
        {
            var best = this._restaurantsService.BestResto();
            var bestRestaurants = await best;
            return View(bestRestaurants);
        }
    }
}
