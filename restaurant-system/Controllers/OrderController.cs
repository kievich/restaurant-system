using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles = UserRoles.Waiter + "," + UserRoles.Cook)]
    public class OrderController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public OrderController(ApplicationContext context, RoleManager<IdentityRole> roleManager)
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
                                on d.Id equals od.Dish.Id
                                where od.Order.Id == id
                                select new
                                {
                                    Id = d.Id,
                                    Name = d.Name,
                                    Description = d.Description,
                                    Price = d.Price,
                                    Count = od.Count,
                                    OrderDishId = od.Id
                                };

            decimal totalPrice = 0M;
            foreach (dynamic d in model.OrderDishes)
            {
                totalPrice += d.Price * d.Count;
            }
            model.TotalPrice = totalPrice;
            model.Dishes = dishController.GetDishes(page, searchString, false);
            ViewBag.DishBag = dishController.ViewBag;
            return View(model);

        }

        [HttpPost]
        [Route("DeleteOrder")]
        public void Delete(int id)
        {
            var orderDishes = _db.OrderDishes.Where(x => x.Order.Id == id);
            _db.OrderDishes.RemoveRange(orderDishes);

            _db.Orders.Remove(new Order() { Id = id });
            _db.SaveChanges();
        }


        [HttpPost]
        [Route("DeleteOrderDish")]
        public void DeleteDish(int id)
        {
            _db.OrderDishes.Remove(new OrderDish() { Id = id });
            _db.SaveChanges();
        }


        [HttpPost]
        [Route("AddDishOrder")]
        public void AddDishOrder(int dishId, int orderId, int number)
        {
            _db.OrderDishes.Add(new OrderDish()
            {
                Dish = _db.Dishes.Where(d => d.Id == dishId).FirstOrDefault(),
                Order = _db.Orders.Where(o => o.Id == orderId).FirstOrDefault(),
                Count = number

            });
            _db.SaveChanges();
        }

        [HttpPost]
        [Route("ChangeOrderStatus")]
        public void ChangeStatus(int Id, int status)
        {
            var order = _db.Orders.Where(o => o.Id == Id).FirstOrDefault();
            if (CanStatusBeChanged(order.Status, (OrderStatus)status))
            {
                order.Status = (OrderStatus)status;
                _db.Orders.Update(order);
                _db.SaveChanges();
            }
        }

        private bool CanStatusBeChanged(OrderStatus oldStatus, OrderStatus newStatus)
        {
            bool CanDraft = false;
            bool CanActive = User.IsInRole(UserRoles.Waiter) && oldStatus == OrderStatus.Draft;
            bool CanСompleted = User.IsInRole(UserRoles.Cook) && oldStatus == OrderStatus.Active;
            bool CanСanceled = User.IsInRole(UserRoles.Cook) && oldStatus == OrderStatus.Active;

            if (newStatus == OrderStatus.Draft)
                return CanDraft;

            if (newStatus == OrderStatus.Active)
                return CanActive;

            if (newStatus == OrderStatus.Сompleted)
                return CanСompleted;

            if (newStatus == OrderStatus.Сanceled)
                return CanСanceled;

            return false;

        }

        [HttpPost]
        [Route("ChangeOrderDishCount")]
        public void ChangeOrderDishCount(int orderDishId, int count)
        {
            var dishOrder = _db.OrderDishes.Where(o => o.Id == orderDishId).FirstOrDefault();
            dishOrder.Count = count;
            _db.OrderDishes.Update(dishOrder);
            if (count >= 0)
            {
                _db.SaveChanges();
            }
        }
    }
}
