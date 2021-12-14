using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant_system.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace restaurant_system.Controllers
{
    [Authorize(Roles = UserRoles.Manager)]
    public class EventController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public EventController(ApplicationContext context, RoleManager<IdentityRole> roleManager)
        {
            _db = context;
        }

        [Route("Events")]
        public async Task<IActionResult> Events(int page = 1, string searchString = "")
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var searchResult = _db.Events.Where(s => s.Name.Contains(searchString)
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
                ViewBag.PageCount = (int)_db.Events.Count() / _pageSize + 1;
                ViewBag.CurrentPage = page;

                var result = _db.Events
                    .OrderBy(o => o.Id)
                    .Reverse()
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize);

                return View(await result.ToListAsync());
            }


        }

        [Route("CreateEvent")]
        public RedirectResult Create()
        {
            var Event = new Event()
            {
                Name = String.Empty,
                Description = String.Empty
            };

            _db.Events.Add(Event);
            _db.SaveChanges();
            return Redirect("/EditEvent/" + Event.Id);
        }

        [Route("EditEvent/{id:int}")]
        public IActionResult Edit(int id, int page = 1, string searchString = "")
        {
            var Event = _db.Events.Where(o => o.Id == id).FirstOrDefault();
            dynamic model = new ExpandoObject();
            var CustomerController = new CustomerController(_db);

            model.Event = Event;
            model.CustomerEvents = from d in _db.Customers
                                   join od in _db.CustomerEvent
                                   on d.Id equals od.Customer.Id
                                   where od.Event.Id == id
                                   select new
                                   {
                                       Id = d.Id,
                                       Firstname = d.Firstname,
                                       Lastname = d.Lastname,
                                       CustomerEventId = od.Id
                                   };

            model.Customeres = CustomerController.GetCustomers(page, searchString);
            ViewBag.CustomerBag = CustomerController.ViewBag;
            return View(model);

        }

        [HttpPost]
        [Route("EditEventField")]
        public void EditField(int id, string name, string description, string date)
        {
            var editEvent = new Event() { Id = id, Name = name, Description = description, Date = Tools.DateHelper.Parse(date) };

            _db.Events.Update(editEvent);
            _db.SaveChanges();
        }

        [HttpPost]
        [Route("DeleteEvent")]
        public void Delete(int id)
        {
            var customerEvents = _db.CustomerEvent.Where(x => x.Event.Id == id);
            _db.CustomerEvent.RemoveRange(customerEvents);

            _db.Events.Remove(new Event() { Id = id });
            _db.SaveChanges();
        }


        [HttpPost]
        [Route("DeleteCustomerEvent")]
        public void DeleteCustomer(int id)
        {
            _db.CustomerEvent.Remove(new CustomerEvent() { Id = id });
            _db.SaveChanges();
        }

        private void ValidatCustomerEvente(Customer customer, Event @event)
        {
            var customerCount = _db.CustomerEvent.Where(c => c.Customer.Id == customer.Id && c.Event.Id == @event.Id).Count();
            if (customerCount > 0)
                throw new ValidationException(ErrorMesseges.EventCustomerExists);
        }

        [HttpPost]
        [Route("AddCustomerEvent")]
        public void AddCustomerEvent(int customerId, int eventId)
        {
            var customer = _db.Customers.Where(c => c.Id == customerId).FirstOrDefault();
            var @event = _db.Events.Where(e => e.Id == eventId).FirstOrDefault();
            ValidatCustomerEvente(customer, @event);


            _db.CustomerEvent.Add(new CustomerEvent()
            {
                Customer = customer,
                Event = @event,

            });
            _db.SaveChanges();
        }
    }
}

