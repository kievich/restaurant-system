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
    public class ReservationController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public ReservationController(ApplicationContext context)
        {
            _db = context;
        }

        [Route("Reservations")]
        public IActionResult Reservations(int page = 1, string searchString = "")
        {
            dynamic model = new ExpandoObject();
            var dishController = new DishController(_db);
            IQueryable<Reservation> result;

            if (!String.IsNullOrEmpty(searchString))
            {
                var searchResult = _db.Reservations.Where(s => s.Name.Contains(searchString));

                ViewBag.PageCount = (int)searchResult.Count() / _pageSize + 1;
                ViewBag.CurrentPage = page;
                ViewBag.searchString = searchString;
                result = searchResult.OrderBy(o => o.Id)
                                         .Reverse()
                                         .Skip((page - 1) * _pageSize)
                                         .Take(_pageSize);
            }
            else
            {
                ViewBag.PageCount = (int)_db.Reservations.Count() / _pageSize + 1;
                ViewBag.CurrentPage = page;

                result = _db.Reservations
                    .OrderBy(o => o.Id)
                    .Reverse()
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize);

            }

            model.Reservation = from t in _db.Tables
                                join r in result
                                on t.Id equals r.TableId
                                select new
                                {
                                    r.Id,
                                    r.Name,
                                    Start = r.Start.ToString("MM/dd/yyyy HH:mm"),
                                    End = r.End.ToString("MM/dd/yyyy HH:mm"),
                                    r.TableId,
                                    TableName = t.Name
                                };

            model.Tables = _db.Tables.Where(t => t.Archived == false);

            return View(model);
        }

        [HttpPost]
        [Route("AddReservation")]
        public void Add(string name, int tableId, string start, string end)
        {

            _db.Reservations.Add(new Reservation()
            {
                Name = name,
                TableId = tableId,
                Start = DateTime.Parse(start),
                End = DateTime.Parse(end)
            });

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("EditReservation")]
        public void Edit(int id, string name, int tableId, string start, string end)
        {
            _db.Reservations.Update(new Reservation()
            {
                Id = id,
                Name = name,
                TableId = tableId,
                Start = DateTime.Parse(start),
                End = DateTime.Parse(end)
            });

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("DeleteReservation")]
        public void Delete(int id)
        {
            _db.Reservations.Remove(new Reservation() { Id = id });
            _db.SaveChanges();
        }
    }
}
