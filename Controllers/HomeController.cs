using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private MyContext context;

        public HomeController(MyContext dbc)
        {
            context = dbc;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        // ===================REGISTRATION===================
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (context.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already in use!");
            }
            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);
                context.Users.Add(user);
                context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return Redirect("/dashboard");
            }
            return View("Index");
        }
        // ===================LOGIN===================
        [HttpPost("login")]
        public IActionResult Login(LoginUser userData)
        {
            User userInDb = context.Users.FirstOrDefault(u => u.Email == userData.LoginEmail);
            if (userInDb == null)
            {
                ModelState.AddModelError("LoginEmail", "Email not found!");
            }
            else
            {
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userData, userInDb.Password, userData.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Incorrect Password!");
                }
            }
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            HttpContext.Session.SetString("Name", userInDb.Name);
            return Redirect("/dashboard");
        }
        // ===================LOGOUT===================
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return Redirect("/");
        }
        // ===================CRUD===================
        [HttpGet("new")]
        public IActionResult NewPlan()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return Redirect("/");
            }
            return View("NewPlan");
        }

        [HttpPost("create")]
        public IActionResult Create(Plan plan)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return Redirect("/");
            }
            if (ModelState.IsValid)
            {
                plan.PlannerId = (int)UserId;
                context.Plans.Add(plan);
                context.SaveChanges();
                return Redirect($"ViewPlan/{plan.PlanId}");
            }
            return View("NewPlan", plan);
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return Redirect("/");
            }
            if (ModelState.IsValid)            
            {
                List<Plan> Plans = context.Plans
                    .OrderBy(plan => plan.Date)
                    .ThenBy(plan => plan.Time)
                    .Include(plan => plan.Planner)
                    .Include(plan => plan.AttendingUsers)
                    // .ThenInclude(going => going.User)
                    .ToList();
                ViewBag.Plans = Plans;
                ViewBag.UserId = (int)UserId;
                ViewBag.Name = HttpContext.Session.GetString("Name");
                return View("Dashboard");
            }
            return Redirect("/");
        }

        [HttpGet("ViewPlan/{PlanId}")]
        public IActionResult ViewPlan(int PlanId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return Redirect("/");
            }
            Plan p = context.Plans
                .Include(plan => plan.Planner)
                .Include(plan => plan.AttendingUsers)
                .ThenInclude(plan => plan.Joiner)
                // .ThenInclude(going => going.User)
                .FirstOrDefault(plan => plan.PlanId == PlanId);
            ViewBag.Plans = p;
            ViewBag.UserId = (int)UserId;
            ViewBag.Joins = p.AttendingUsers;
            // return View("ViewPlan");
            return View(p);
        }

        [HttpGet("join/{PlanId}")]
        public IActionResult Join(int PlanId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return Redirect("/");
            }
            Join x = new Join()
            {
                UserId = (int)UserId,
                PlanId = PlanId
            };
            context.Joins.Add(x);
            context.SaveChanges();
            return Redirect("/dashboard");
        }

        [HttpGet("delete/{PlanId}")]
        public IActionResult Delete(int PlanId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return Redirect("/");
            }
            Plan plan = context.Plans.FirstOrDefault(p => p.PlanId == PlanId);
            context.Plans.Remove(plan);
            context.SaveChanges();
            return Redirect("/dashboard");
        }

        [HttpGet("leave/{PlanId}")]
        public IActionResult Leave(int PlanId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return Redirect("/");
            }
            Join join = context.Joins
                .Where(j => j.PlanId == PlanId)
                .FirstOrDefault(j => j.UserId == (int)UserId);
            context.Joins.Remove(join);
            context.SaveChanges();
            return Redirect("/dashboard");
        }
    }
}
