using NUnit.Framework;
using System.IO;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Services;

namespace NUnitTestProject1
{
    [TestFixture]
    public class DbRestaurantsTest
    {

        [Test, Order(0)]
        public void InitDbConnexion()
        {
            using (var dbCtxt = new RestaurantContext())
            {
                dbCtxt.Database.EnsureCreated();
            }
        }

        [Test, Order(1)]
        public void CreateRestaurant()
        {
            var Ctxt = new RestaurantContext();
            var srv = new RestaurantsServices(Ctxt);
            var nbrestoav = srv.getAll().Count();
            using (var dbCtxt = new RestaurantContext())
            {
                dbCtxt.Database.EnsureCreated();
                var resto = new Restaurant();
                resto.Nom = "mon resto";
                resto.Numtel = "0476533325";
                resto.Commentaire = "Resto chinois";
                resto.Mail = "resto1@gmail.com";
                resto.address = new Address();
                resto.address.Rue = "rue 1";
                resto.address.CodePostal = "38000";
                resto.address.Ville = "Grenoble";
                resto.note = new Note();
                resto.note.DateLastVisite = "19/09/2019";
                resto.note.note = "13";
                resto.note.CommentaireNote = "Délicieux";

                dbCtxt.Restaurants.Add(resto);
                dbCtxt.SaveChanges();
            }
            using (var dbCtxt = new RestaurantContext())
            {
                Assert.AreEqual(nbrestoav + 1, dbCtxt.Restaurants.Count());
            }
        }

        [Test, Order(2)]
        public void UpdateRestaurant()
        {
            var ctxt = new RestaurantContext();
            var srv = new RestaurantsServices(ctxt);

            var idrestoav = srv.getAll().Last().ID;
            var resto = srv.getRestobyid(idrestoav);
            Assert.AreEqual("mon resto", resto.Nom);
            resto.Nom = "mon resto 2";
            srv.update(resto);
            var idrestoap = srv.getAll().Last().ID;
            var restoap = srv.getRestobyid(idrestoap);
            Assert.AreEqual("mon resto 2", restoap.Nom);
        }

        [Test, Order(3)]
        public void UpdateNote()
        {
            var ctxt = new RestaurantContext();
            var srv = new RestaurantsServices(ctxt);

            var idrestoav = srv.getAll().Last().ID;
            var resto = srv.getRestobyid(idrestoav);
            Assert.AreEqual("13", resto.note.note);
            resto.note.note = "20";
            srv.update(resto);
            var idrestoap = srv.getAll().Last().ID;
            resto = srv.getRestobyid(idrestoap);

            Assert.AreEqual("20", resto.note.note);
        }

        [Test, Order(4)]
        public void DeleteRestaurant()
        {
            var ctxt = new RestaurantContext();
            var srv = new RestaurantsServices(ctxt);
            var nbrestoav = srv.getAll().Count();
            var idresto = srv.getAll().Last().ID;
            var resto = srv.getRestobyid(idresto);
            srv.deleteRestaurant(resto.ID);
            var nbrestoap = srv.getAll().Count();

            Assert.AreEqual(nbrestoav - 1, nbrestoap);
        }
    }
}