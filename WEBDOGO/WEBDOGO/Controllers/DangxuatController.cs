using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBDOGO.Controllers
{
    public class DangxuatController : Controller
    {
        // GET: Dangxuat
        public ActionResult dangxuat()
        {
            Session.Clear();
            Session.Abandon();
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("TrangChu", "TrangChu");
        }
    }
}