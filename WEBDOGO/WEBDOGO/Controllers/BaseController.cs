using Google;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBDOGO.Models;

namespace WEBDOGO.Controllers
{
    public class BaseController : Controller
    {
        SQLWEB2Entities1 db = new SQLWEB2Entities1();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string sessionToken = Request.Cookies["SessionToken"]?.Value;

            string currentController = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            int? iduser = Session["idtaikhoan"] as int?;
            string role = (Session["UserRole"]?.ToString() ?? "").ToLower();
            if (iduser == null || string.IsNullOrEmpty(role))
            {
                if (currentController.Contains("admin") || currentController.Contains("nhanviensanxuat") || currentController.Contains("nhanvientaichinh") || currentController.Contains("nhanvienvanchuyen"))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "User" },
                        { "action", "khongcoquyentruycap" }
                    });
                    return;
                }
            }

            if (iduser != null)
            {
                if (role == "khachhang" && kiemtra(db.KHACHHANGs.FirstOrDefault(u => u.MAKHACHHANG == iduser), sessionToken, filterContext)) return;
                if (role == "admin" && kiemtra(db.ADMINs.FirstOrDefault(u => u.MAADMIN == iduser), sessionToken, filterContext)) return;
                if (role == "taichinh" && kiemtra(db.NHANVIENTAICHINHs.FirstOrDefault(u => u.MANHANVIENTAICHINH == iduser), sessionToken, filterContext)) return;
                if (role == "sanxuat" && kiemtra(db.NHANVIENSANXUATs.FirstOrDefault(u => u.MANHANVIENSANXUAT == iduser), sessionToken, filterContext)) return;
                if (role == "vanchuyen" && kiemtra(db.NHANVIENVANCHUYENs.FirstOrDefault(u => u.MANHANVIENVANCHUYEN == iduser), sessionToken, filterContext)) return;

                if ((currentController.Contains("admin") && role != "admin") ||
                    (currentController.Contains("user") && role != "khachhang") ||
                    (currentController.Contains("nhanviensanxuat") && role != "sanxuat") ||
                    (currentController.Contains("nhanvientaichinh") && role != "taichinh") ||
                    (currentController.Contains("nhanvienvanchuyen") && role != "vanchuyen"))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "User" },
                        { "action", "khongcoquyentruycap" }
                    });
                    return;
                }

            }



            base.OnActionExecuting(filterContext);
        }

        private bool kiemtra(dynamic user, string sessionToken, ActionExecutingContext filterContext)
        {
            if (user != null && user.SessionToken != sessionToken)
            {
                Session.Clear();
                filterContext.Result = new RedirectResult("/Trangchu/Trangchu");
                return true;
            }
            return false;
        }
    }
}