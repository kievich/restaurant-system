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
using System.ComponentModel.DataAnnotations;

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
                var searchResult = _db.Reservations.Where(s => s.Name.Contains(searchString) ||
                                                           s.Table.Name.Contains(searchString));

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
                                on t.Id equals r.Table.Id
                                select new
                                {
                                    r.Id,
                                    r.Name,
                                    Start = r.Start.ToString("MM/dd/yyyy HH:mm"),
                                    End = r.End.ToString("MM/dd/yyyy HH:mm"),
                                    Table = r.Table,
                                    TableName = t.Name,
                                    Hours = r.Hours
                                };

            model.Tables = _db.Tables.Where(t => t.Archived == false);

            return View(model);
        }

        public void ValidateReservation(Reservation reservation)
        {
            var maxReservationHours = 1000;

            if (reservation.Hours <= 0)
                throw new ValidationException(ErrorMesseges.ReservationLessThanNull);

            if (reservation.Hours > maxReservationHours)
                throw new ValidationException(String.Format(ErrorMesseges.ReservationMaxHour, maxReservationHours));

            var ReservationInnerCount = _db.Reservations
                                        .Where(r => r.Start < reservation.Start && r.End > reservation.End && r.Table.Id == reservation.Table.Id)
                                        .Count();

            var ReservationOuterCount = _db.Reservations
                            .Where(r => r.End > reservation.Start && r.Start < reservation.End && r.Table.Id == reservation.Table.Id)
                            .Count();

            if (ReservationInnerCount > 0 || ReservationOuterCount > 0)
                throw new ValidationException(ErrorMesseges.ReservationExists);

        }

        [HttpPost]
        [Route("AddReservation")]
        public IActionResult Add(string name, int tableId, string start, string end)
        {
            var reservation = new Reservation()
            {
                Name = name,
                Table = _db.Tables.Where(t => t.Id == tableId).FirstOrDefault(),
                Start = Tools.DateHelper.Parse(start),
                End = Tools.DateHelper.Parse(end)
            };

            ValidateReservation(reservation);

            _db.Reservations.Add(reservation);
            _db.SaveChanges();
            return Ok();

        }

        [HttpPost]
        [Route("EditReservation")]
        public void Edit(int id, string name, int tableId, string start, string end)
        {
            var reservation = new Reservation()
            {
                Id = id,
                Name = name,
                Table = _db.Tables.Where(t => t.Id == tableId).FirstOrDefault(),
                Start = DateTime.Parse(start),
                End = DateTime.Parse(end)
            };

            ValidateReservation(reservation);

            _db.Reservations.Update(reservation);

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
