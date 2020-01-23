using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class RestaurantsServices
    {
        private RestaurantContext _restaurantsContext;
        public RestaurantsServices(RestaurantContext ctxt)
        {
            this._restaurantsContext = ctxt;
        }

        public List<Restaurant> getAll()
        {
            this._restaurantsContext.Database.EnsureCreated();
            return this._restaurantsContext.Restaurants.Include(r=>r.address).Include(r=>r.note).ToList();
        }
        //public List<Note> getNotes()
        //{
        //    //return this._restaurantsContext.Grades.Include(r=>r.Restaurant).ToList();
        //    return this._restaurantsContext.Restaurants.To;
        //}
        public Restaurant getRestobyid(int id)
        {
            return _restaurantsContext.Restaurants.Include(r=>r.address).Include(r=>r.note).FirstOrDefault(r => r.ID == id);
        }
        public void update(Restaurant resto)
        {
            this._restaurantsContext.Entry(resto).State = EntityState.Modified;
            this._restaurantsContext.SaveChanges();
        }
        public async void deleteRestaurant(int id)
        {
            this._restaurantsContext.Database.EnsureCreated();
            var restaurant = await this._restaurantsContext.Restaurants.FindAsync(id);
            this._restaurantsContext.Restaurants.Remove(restaurant);
            await this._restaurantsContext.SaveChangesAsync();
        }
        public void createAddr(Address addr)
        {
            this._restaurantsContext.Entry(addr).State = EntityState.Added;
            this._restaurantsContext.SaveChanges();
        }
        public void createResto(Restaurant resto)
        {
            this._restaurantsContext.Entry(resto).State = EntityState.Added;
            this._restaurantsContext.SaveChanges();
        }
        public void createNote(Note note)
        {
            this._restaurantsContext.Entry(note).State = EntityState.Added;
            this._restaurantsContext.SaveChanges();
        }

        public async Task<IEnumerable<Restaurant>> BestResto()
        {
            this._restaurantsContext.Database.EnsureCreated();
            var restaurantList = this._restaurantsContext.Restaurants.Include(p => p.address).Include(p => p.note).ToList();
            var meilleur = restaurantList.OrderByDescending(restaurant => restaurant.note.note).Take(5);

            return meilleur;
        }
    }
}
