using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using restaurant_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace restaurant_system.Controllers
{
    [Authorize(Roles = UserRoles.Manager)]
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
            return View(GetDishes(page, searchString, true));
        }

        public IQueryable<Dish> GetDishes(int page, string searchString, bool includeArchived)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var searchResult = _db.Dishes.Where(s => s.Name.Contains(searchString)
                                       || s.Description.Contains(searchString)
                                       || s.Archived.ToString().Contains(searchString))
                        .Where(s => s.Archived == false || includeArchived);

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
                    .Where(s => s.Archived == false || includeArchived)
                    .OrderBy(o => o.Id)
                    .Reverse()
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize);

                return result;
            }
        }

        private void ValidateDish(Dish dish)
        {
            if (dish.Price < 0)
                throw new ValidationException(ErrorMesseges.DishPrice);
        }

        [HttpPost]
        [Route("AddDish")]
        public void Add(string name, string description, string price)
        {
            var dish = new Dish()
            {
                Name = name,
                Description = description,
                Price = Tools.DecimalHelper.Parse(price),
                Archived = false
            };

            ValidateDish(dish);

            _db.Dishes.Add(dish);

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("EditDish")]
        public void Edit(int id, string name, string description, string price, bool archived)
        {
            var dish = new Dish()
            {
                Id = id,
                Name = name,
                Description = description,
                Price = Tools.DecimalHelper.Parse(price),
                Archived = archived
            };
            ValidateDish(dish);

            _db.Dishes.Update(dish);

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
