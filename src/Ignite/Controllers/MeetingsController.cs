using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity;
using Ignite.Models;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using Ignite.ViewModels.Meetings;

namespace Ignite.Controllers
{
    public class Meetings : Controller
    {
        private ApplicationDbContext _context;

        public Meetings(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: Meetings
        public IActionResult Index()
        {
            var model = _context.Meetings.Include(x => x.Attendances).ToList();
            return View(model);
        }

        // GET: Meetings/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Meeting meeting = _context.Meetings.Single(m => m.Id == id);
            var viewmodel = new MeetingDetailsViewModel(meeting);
            try
            {
                var attendances = _context.Attendances.Include(c => c.Comments).Where(x => x.Meeting.Id == id);

                viewmodel.Attendances = attendances.ToList();
                var usermanager = new UserManager<ApplicationUser>(
                    new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(
                        _context), null, null, null, null, null, null, null, null, null);

                foreach (var attendee in attendances.GroupBy(a => a.Attendee))
                {
                    var user = usermanager.FindByIdAsync(attendee.Key.ToString()).Result;

                    viewmodel.Attendees.Add(user);
                }
                return View(viewmodel);
            }
            catch (NullReferenceException nrEx)
            {
                return HttpNotFound();
            }


        }

        public IActionResult Attend(int id, bool response, int rated)
        {
            var _userId = Guid.Parse(User.GetUserId());
            var meeting = _context.Meetings.Single(m => m.Id == id);
            bool willAttend = meeting.Start > DateTime.Now && response;
            bool didAttend = meeting.Start < DateTime.Now && response;

            Attendance attendance;

            var attendances = meeting.Attendances.Where(a => a.Attendee == _userId && a.Meeting.Id == id);

            if (attendances.Any())
            {
                attendance = attendances.FirstOrDefault();
                attendance.WillAttend = willAttend;
                attendance.DidAttend = didAttend;
                attendance.Rating = rated;
            }
            else
            {
                attendance = new Attendance()
                {
                    Attendee = _userId,
                    WillAttend = willAttend,
                    DidAttend = didAttend,
                    Rating = rated
                };

                meeting.Attendances.Add(attendance);
            }


            _context.SaveChanges();

            return RedirectToAction("Index");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Comment(int id, string comment)
        {
            var _userId = Guid.Parse(User.GetUserId());
            Attendance attend;
            var meeting = _context.Meetings.FirstOrDefault(m => m.Id == id);

            Models.Comment com = new Models.Comment() { Text = comment, Posted = DateTime.Now, Commenter = User.Identity.Name };

            // Check whether we attended this meeting
            if (meeting.Attendances.Any(x => x.Attendee == _userId))
            {
                attend = meeting.Attendances.FirstOrDefault(a => a.Attendee == _userId);
            }
            else
            {
                attend = new Attendance() { Attendee = _userId, Meeting = meeting };
                meeting.Attendances.Add(attend);
            }

            com.Meeting = attend;
            attend.Comments.Add(com);

            _context.SaveChanges();


            return RedirectToAction("Details", new { Id = id });
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Meetings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _context.Meetings.Add(meeting);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meeting);
        }

        // GET: Meetings/Edit/5
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

        // POST: Meetings/Edit/5
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

        // GET: Meetings/Delete/5
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

        // POST: Meetings/Delete/5
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
