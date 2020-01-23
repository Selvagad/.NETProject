using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace NUnitTestProject1
{
    [TestFixture]
    class JSONTest
    {

        [Test, Order(1)]
        //Only test the import function : create a database file into Test directory
        public void ImportTest()
        {
            var context = new RestaurantContext();
            var nbav = new RestaurantsServices(context).getAll().Count;
            new JSONServices().importDatabase();
            var nbap = new RestaurantsServices(context).getAll().Count;
            Assert.AreEqual(nbav + 6, nbap);
        }

        [Test]
        //Create the database for the application and copy it into the run directory
        public void ImportRestaurants()
        {
            new JSONServices().importDatabase();
            var appPath = Directory.GetParent(Directory.GetCurrentDirectory());
            appPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(appPath.ToString()).ToString()).ToString());
            var here = appPath + @"\NUnitTestProject1\bin\Debug\netcoreapp3.0\myDb3.db";
            var dest = appPath + @"\WebApplication1\bin\Debug\netcoreapp3.0\myDb3.db";
            File.Copy(here, dest);

            Assert.IsTrue(File.Exists(dest));
        }

        [Test, Order(2)]
        public void ExportTest()
        {
            var appPath = Directory.GetParent(Directory.GetCurrentDirectory());
            appPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(appPath.ToString()).ToString()).ToString());
            var fullPath = appPath + @"\WebApplication1\JSON\dumps\";
            var nbav = Directory.GetFiles(fullPath, "*.json").Length;
            new JSONServices().ExportDataBase();
            var nbap = Directory.GetFiles(fullPath, "*.json").Length;

            Assert.AreEqual(nbav + 1, nbap);
        }
    }
}
