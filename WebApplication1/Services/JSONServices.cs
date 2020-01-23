using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class JSONServices
    {
        private RestaurantContext _restaurantsContext;
        public JSONServices()
        {
            this._restaurantsContext = new RestaurantContext();
        }

        public void ExportDataBase()
        {
            var dbCtxt = new RestaurantContext();
            dbCtxt.Database.EnsureCreated();

            var list = dbCtxt.Restaurants.Include(p => p.address).Include(p => p.note).ToList();

            string json = JsonConvert.SerializeObject(list);
            var absoluteAppPath = Directory.GetParent(Directory.GetCurrentDirectory());
            string path = "";
            Assembly? assembly = Assembly.GetEntryAssembly();
            string assemblyMessage = assembly.ToString();
            if (assemblyMessage.Contains("testhost"))
            {
                absoluteAppPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(absoluteAppPath.ToString()).ToString()).ToString());
                path = absoluteAppPath + @"\WebApplication1\JSON\dumps\dump" + $"{DateTime.Now:dd-MM-yyyy_HH-mm-ss-fff}" + ".json";
            }
            else
            {
                path = absoluteAppPath + @"\WebApplication1\JSON\dumps\dump" + $"{DateTime.Now:dd-MM-yyyy_HH-mm-ss-fff}" + ".json";
            }
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.WriteLine(json);
            };
        }

        public void importDatabase()
        {
            using var dbCtxt = new RestaurantContext();
            dbCtxt.Database.EnsureCreated();
            var jsonContent = File.ReadAllText("../../../../WebApplication1/JSON/restaurant_to_import.json");
            var importData = JsonConvert.DeserializeObject<List<Restaurant>>(jsonContent);

            dbCtxt.Restaurants.AddRange(importData);
            dbCtxt.SaveChanges();
        }

    }
}
