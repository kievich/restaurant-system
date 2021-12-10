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
    [Authorize(Roles = UserRoles.Manager)]
    public class TableController : Controller
    {
        private ApplicationContext _db;
        private int _pageSize = 10;

        public TableController(ApplicationContext context)
        {
            _db = context;
        }

        [Route("Tablees")]
        public IActionResult Tables(int page = 1, string searchString = "")
        {
            return View(GetTables(page, searchString, true));
        }

        public IQueryable<Table> GetTables(int page, string searchString, bool includeArchived)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var searchResult = _db.Tables.Where(s => s.Name.Contains(searchString)
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
                ViewBag.PageCount = _db.Tables.Count() / _pageSize + 1;
                ViewBag.CurrentPage = page;

                var result = _db.Tables
                    .Where(s => s.Archived == false || includeArchived)
                    .OrderBy(o => o.Id)
                    .Reverse()
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize);

                return result;
            }
        }

        [HttpPost]
        [Route("AddTable")]
        public void Add(string name)
        {
            _db.Tables.Add(new Table()
            {
                Name = name,
                Archived = false
            });

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("EditTable")]
        public void Edit(int id, string name, bool archived)
        {
            _db.Tables.Update(new Table()
            {
                Id = id,
                Name = name,
                Archived = archived
            });

            _db.SaveChanges();
        }

        [HttpPost]
        [Route("DeleteTable")]
        public void Delete(int id)
        {
            _db.Tables.Remove(new Table() { Id = id });
            _db.SaveChanges();
        }
    }
}
