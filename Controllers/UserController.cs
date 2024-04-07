using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDeskTrain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskTrain.Controllers
{
    [Authorize(Roles = "Администратор, Модератор, Исполнитель")]
    public class UserController : Controller
    {
        private HelpdeskContext db = new HelpdeskContext();
       [Route("User/Index")]
        /*public IActionResult Index()
       {
           return View("Index");
       }

       */
        [HttpGet]
        public IActionResult Index()
        {
            var users = db.Users.Include(u => u.Department).Include(u => u.Role).ToList();
            return View(users);
        }
    }
}

