using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using restaurant_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_system.Controllers
{
    public class DishController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public DishController(ApplicationContext context)
        {
            _db = context;
        }

        [Route("Dishes")]
        public IActionResult Dishes(int page = 1, string searchString = "")
        {
            return View(GetDishes(page, searchString));
        }

        public IQueryable<Dish> GetDishes(int page, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var searchResult = _db.Dishes.Where(s => s.Name.Contains(searchString)
                                       || s.Description.Contains(searchString));

                ViewBag.PageCount = (int)searchResult.Count() / _pageSize + 1;
                ViewBag.CurrentPage = page;
                ViewBag.searchString = searchString;
                var result = searchResult.OrderBy(o => o.Id)
                                         .Reverse()
                                         .Skip((page - 1) * _pageSize)
                                         .Take(_pageSize);

                return result;
            }
            else
            {
               int oo = _db.Customers.Count();
                ViewBag.PageCount = _db.Dishes.Count() / _pageSize + 1;
                ViewBag.CurrentPage = page;

                var result = _db.Dishes
                    .OrderBy(o => o.Id)
                    .Reverse()
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize);

                return result;
            }
        }

        [HttpPost]
        [Route("AddDish")]
        public void Add(string name, string description, string price)
        {
            _db.Dishes.Add(new Dish()
            {
                Name = name,
                Description = description,
                Price = Tools.DecimalHelper.Parse(price),
                Archived = false
            });

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("EditDish")]
        public void Edit(int id, string name, string description, string price, bool archived)
        {
            _db.Dishes.Update(new Dish()
            {
                Id = id,
                Name = name,
                Description = description,
                Price = Tools.DecimalHelper.Parse(price),
                Archived = archived
            });

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("DeleteDish")]
        public void Delete(int id)
        {
            _db.Dishes.Remove(new Dish() { Id = id });
            _db.SaveChanges();
        }
    }
}
