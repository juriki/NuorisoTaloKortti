using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NuorisoTaloKortti.Models;

namespace NuorisoTaloKortti.Controllers
{
    public class KorttiController : Controller
    {
        // GET: Kortti
        public ActionResult Index()
        {
                return View();
        }
    }
}