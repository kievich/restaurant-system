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
    public class CustomerController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public CustomerController(ApplicationContext context)
        {
            _db = context;
        }

        [Route("Customers")]
        public async Task<IActionResult> Customers(int page = 1)
        {

            ViewBag.PageCount = (int)_db.Customers.Count() / _pageSize + 1;
            ViewBag.CurrentPage = page;


            var result = _db.Customers
                .OrderBy(o => o.CustomerId)
                .Reverse()
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize);

            return View(await result.ToListAsync());
        }

        [HttpPost]
        [Route("AddCustomer")]
        public void Add(int id, string firstname, string lastname, string phone)
        {
            _db.Customers.Add(new Customer()
            {
                CustomerId = id,
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
                CustomerId = id,
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
            _db.Customers.Remove(new Customer() { CustomerId = id });
            _db.SaveChanges();
        }
    }
}
