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
    public class OrderController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public OrderController(ApplicationContext context)
        {
            _db = context;
        }

        [Route("Dishes")]
        public async Task<IActionResult> Orders(int page = 1, string searchString = "")
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

                return View(await result.ToListAsync());
            }
            else
            {
                ViewBag.PageCount = (int)_db.Customers.Count() / _pageSize + 1;
                ViewBag.CurrentPage = page;

                var result = _db.Dishes
                    .OrderBy(o => o.Id)
                    .Reverse()
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize);

                return View(await result.ToListAsync());
            }


        }

        [HttpPost]
        [Route("AddOrder")]
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
        [Route("EditOrder")]
        public void Edit(int id, string name, string description, string price, bool archived)
        {
            //_db.Dishes.Update(new Dish()
            //{
            //    Id = id,
            //    Name = name,
            //    Description = description,
            //    Price = Tools.DecimalHelper.Parse(price),
            //    Archived = archived
            //});

            //_db.SaveChanges();
        }

        [HttpPost]
        [Route("DeleteOrder")]
        public void Delete(int id)
        {
            _db.Dishes.Remove(new Dish() { Id = id });
            _db.SaveChanges();
        }
    }
}
