using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Ignite.Models;
using Microsoft.AspNet.Authorization;
using System.Collections.Generic;
using Microsoft.AspNet.Http.Internal;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace Ignite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Admin
        public IActionResult Index()
        {
            return View(_context.Meetings.ToList());
        }

        // GET: Admin/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Meeting meeting = _context.Meetings.Single(m => m.Id == id);
            if (meeting == null)
            {
                return HttpNotFound();
            }

            return View(meeting);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                var byteArray = new byte[Request.Body.Length];

                var form = Request.ReadFormAsync().Result;

                foreach(var file in form.Files)
                {
                    ContentDispositionHeaderValue parsedContentDisposition;
                    ContentDispositionHeaderValue.TryParse(file.ContentDisposition, out parsedContentDisposition);

                    if (parsedContentDisposition == null || (file.Length == 0 && string.IsNullOrEmpty(meeting.Image)))
                    {
                        continue;
                    }

                }
                //var form = await request.ReadFormAsync();
                //+
                //+                foreach (var file in form.Files)
                //    +                {
                //    +ContentDispositionHeaderValue parsedContentDisposition;
                //    +ContentDispositionHeaderValue.TryParse(file.ContentDisposition, out parsedContentDisposition);
                //    +
                //    +                    // If there is an <input type="file" ... /> in the form and is left blank.
                //    +                    if (parsedContentDisposition == null ||
                //    +(file.Length == 0 && string.IsNullOrEmpty(parsedContentDisposition.FileName)))
                //        +                    {
                //        +                        continue;
                //        +                    }
                //    +
                //    +var modelName = parsedContentDisposition.Name;
                //    +                    if (modelName.Equals(bindingContext.ModelName, StringComparison.OrdinalIgnoreCase))
                //        +                    {
                //        +postedFiles.Add(file);
                //        +                    }
                //    +                }



                _context.Meetings.Add(meeting);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meeting);
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Meeting meeting = _context.Meetings.Single(m => m.Id == id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _context.Update(meeting);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meeting);
        }

        public JsonResult Users()
        {
            var list = from u in _context.Users
                       select new
                       {
                           Name = u.UserName,
                           Email = u.Email,
                           Confirmed = u.EmailConfirmed
                       };

            return Json( list.ToList() );
        }


        // GET: Admin/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Meeting meeting = _context.Meetings.Single(m => m.Id == id);
            if (meeting == null)
            {
                return HttpNotFound();
            }

            return View(meeting);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = _context.Meetings.Single(m => m.Id == id);
            _context.Meetings.Remove(meeting);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
