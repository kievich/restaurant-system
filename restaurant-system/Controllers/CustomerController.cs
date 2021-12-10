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

namespace restaurant_system.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CustomerController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public CustomerController(ApplicationContext context)
        {
            _db = context;
        }

        [Route("Customers")]
        public async Task<IActionResult> Customers(int page = 1, string searchString = "")
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var searchResult = _db.Customers.Where(s => s.Firstname.Contains(searchString)
                                        || s.Lastname.Contains(searchString)
                                        || s.Phone.Contains(searchString));

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

                var result = _db.Customers
                    .OrderBy(o => o.Id)
                    .Reverse()
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize);

                return View(await result.ToListAsync());
            }


        }

        [HttpPost]
        [Route("AddCustomer")]
        public void Add(string firstname, string lastname, string phone)
        {
            _db.Customers.Add(new Customer()
            {
                Firstname = firstname,
                Lastname = lastname,
                Phone = phone
            });

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("EditCustomer")]
        public void Edit(int id, string firstname, string lastname, string phone)
        {
            _db.Customers.Update(new Customer()
            {
                Id = id,
                Firstname = firstname,
                Lastname = lastname,
                Phone = phone
            });

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("DeleteCustomer")]
        public void Delete(int id)
        {
            _db.Customers.Remove(new Customer() { Id = id });
            _db.SaveChanges();
        }
    }
}
