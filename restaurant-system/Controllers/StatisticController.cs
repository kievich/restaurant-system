using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = UserRoles.Manager)]
    public class StatisticController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public StatisticController(ApplicationContext context)
        {
            _db = context;
        }

        [Route("Statistics")]
        public IActionResult Statistics(string start = "", string end = "")
        {
            dynamic model = new ExpandoObject();
            DateTime startDateTime;
            DateTime endDateTime;

            if (String.IsNullOrEmpty(start))
                startDateTime = DateTime.Now.AddDays(-1);
            else
                startDateTime = Tools.DateHelper.Parse(start);

            if (String.IsNullOrEmpty(end))
                endDateTime = DateTime.Now;
            else
                endDateTime = Tools.DateHelper.Parse(end);

            model.StartDateTime = startDateTime;
            model.EndDateTime = endDateTime;
            model.OrdersCount = _db.Orders.Where(o => o.Date > startDateTime && o.Date < endDateTime && o.Status == OrderStatus.Сompleted).Count();
            var orderDishes = from od in _db.OrderDishes
                              join d in _db.Dishes on od.Dish.Id equals d.Id
                              join o in _db.Orders on od.Order.Id equals o.Id
                              where o.Date > startDateTime && o.Date < endDateTime
                              select od;

            model.AverageСheck = Math.Round(orderDishes.Average(o => o.Dish.Price * o.Count), 2);

            model.EventCount = _db.Events.Where(e => e.Date > startDateTime && e.Date < endDateTime).Count();
            return View(model);
        }

    }
}
