using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAPERWALA.Filters;
using PAPERWALA.Models;
using PAPERWALA.Repository;
using System.IO;

namespace PAPERWALA.Controllers
{
    public class DashboardController : Controller
    {
        [MyExceptionHandler]
        [AllowAnonymous]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}