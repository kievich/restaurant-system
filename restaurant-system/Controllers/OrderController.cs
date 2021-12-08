using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using restaurant_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
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

        [Route("Orders")]
        public async Task<IActionResult> Orders(int page = 1, string searchString = "")
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var searchResult = _db.Orders.Where(s => s.Name.Contains(searchString));

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
                ViewBag.PageCount = (int)_db.Orders.Count() / _pageSize + 1;
                ViewBag.CurrentPage = page;

                var result = _db.Orders
                    .OrderBy(o => o.Id)
                    .Reverse()
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize);

                return View(await result.ToListAsync());
            }


        }

        [Route("CreateOrder")]
        public RedirectResult Create()
        {
            var Order = new Order()
            {
                Name = String.Empty,
                Date = DateTime.Now
            };

            _db.Orders.Add(Order);
            _db.SaveChanges();
            return Redirect("/EditOrder/" + Order.Id);
        }

        [Route("EditOrder/{id:int}")]
        public IActionResult Edit(int id, int page = 1, string searchString = "")
        {

            var order = _db.Orders.Where(o => o.Id == id).FirstOrDefault();
            dynamic model = new ExpandoObject();
            var dishController = new DishController(_db);

            model.Order = order;
            model.OrderDishes = from d in _db.Dishes
                                join od in _db.OrderDishes
                                on d.Id equals od.DishId
                                where od.OrderId == id
                                select d;

            model.Dishes = dishController.GetDishes(page, searchString);
            ViewBag.DishBag = dishController.ViewBag;
            return View(model);

        }

        [HttpPost]
        [Route("DeleteOrder")]
        public void Delete(int id)
        {
            _db.Dishes.Remove(new Dish() { Id = id });
            _db.SaveChanges();
        }

        [HttpPost]
        [Route("DeleteOrder")]
        public void AddDishOrder(int dishId, int orderId, int number)
        {
            _db.OrderDishes.Add(new OrderDish()
            {
                DishId = dishId,
                OrderId = orderId,
                Count = number

            });
            _db.SaveChanges();
        }
    }
}
