using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WEBDOGO.Models;

namespace WEBDOGO.Controllers
{
    public class ADMINController : BaseController
    {
        SQLWEB2Entities1 db = new SQLWEB2Entities1();

        public ActionResult showtrangquanly()
        {
            return View();
        }

        public ActionResult shownhanvientaichinh()
        {
            var nhanvientc = db.NHANVIENTAICHINHs.ToList();
            ViewBag.nHANVIENTAICHINHs = nhanvientc;
            return PartialView("_Partialnhanvientc");
        }

        [HttpGet]
        public ActionResult caplaimatkhau(int id, int loai)
        {
            ViewBag.Id = id;
            ViewBag.Loai = loai;
            return View();
        }



        [HttpPost]
        public ActionResult caplaimatkhau(int id,int loai, string newPassword, string confirmPassword)
        {
            if (newPassword == confirmPassword)
            {
                newPassword = HashPasswordWithSHA512(newPassword);

                object nv = null;

                if (loai == 1)
                    nv = db.NHANVIENTAICHINHs.FirstOrDefault(p => p.MANHANVIENTAICHINH == id);
                else if (loai == 3)
                    nv = db.NHANVIENVANCHUYENs.FirstOrDefault(p => p.MANHANVIENVANCHUYEN == id);
                else if (loai == 2)
                    nv = db.NHANVIENSANXUATs.FirstOrDefault(p => p.MANHANVIENSANXUAT == id);

                if (nv != null)
                {
                    if (loai == 1)
                        ((NHANVIENTAICHINH)nv).MATKHAU = newPassword;
                    else if (loai == 3)
                        ((NHANVIENVANCHUYEN)nv).MATKHAU = newPassword;
                    else if (loai == 2)
                        ((NHANVIENSANXUAT)nv).MATKHAU = newPassword;

                    db.SaveChanges();
                }

                return RedirectToAction("showtrangquanly");
            }

            ModelState.AddModelError("", "Mật khẩu không khớp");
            ViewBag.Id = id;
            ViewBag.Loai = loai;
            return View();
        }

        public ActionResult showsanpham()
        {
            var sanpham = db.SANPHAMs.ToList();
            ViewBag.sanphamlist = sanpham;
            return PartialView("_Partialshowsanpham");
        }

        public ActionResult shownhanviensanxuat()
        {
            var nhanviensanxuat = db.NHANVIENSANXUATs.ToList();
            ViewBag.nHANVIENSANXUATs = nhanviensanxuat;
            return PartialView("_Partialshownhanviensanxuat");
        }

        public ActionResult showkhachhang()
        {
            var khachhang = db.KHACHHANGs.ToList();
            ViewBag.kHACHHANGs = khachhang;
            return PartialView("_Partialshowkhachhang");
        }

        public ActionResult shownhanvienvanchuyen()
        {
            var nhanvienvanchuyen = db.NHANVIENVANCHUYENs.ToList();
            ViewBag.nHANVIENVANCHUYENs = nhanvienvanchuyen;
            return PartialView("_Partialnhanvienvanchuyen");
        }

        public ActionResult showlienhe()
        {
            var lienhe = db.LIENHEs.ToList();
            ViewBag.lIENHEs = lienhe;
            return PartialView("_Partiallienhe");
        }

        [HttpGet]
        public ActionResult showchitietnvtc(int id)
        {
            var nhanvientc = db.NHANVIENTAICHINHs.FirstOrDefault(m => m.MANHANVIENTAICHINH == id);
            return View(nhanvientc);
        }


        [HttpPost]         
        public ActionResult luucapnhapnvtc(NHANVIENTAICHINH nvtc)
        {
            var nhanvientc = db.NHANVIENTAICHINHs.AsNoTracking().SingleOrDefault(m => m.MANHANVIENTAICHINH == nvtc.MANHANVIENTAICHINH);
            if(nvtc.TRANGTHAI == "Đã Nghỉ Việc" && nhanvientc.NGAYNGHILAM != null)
            {
                if (nvtc.NGAYVAOLAM == null && nvtc.NGAYNGHILAM != null)
                {
                    nvtc.NGAYVAOLAM = nhanvientc.NGAYVAOLAM;
                }
                else if (nvtc.NGAYNGHILAM == null && nvtc.NGAYVAOLAM != null)
                {
                    nvtc.NGAYNGHILAM = nhanvientc.NGAYNGHILAM;
                }
                else if (nvtc.NGAYVAOLAM == null && nvtc.NGAYNGHILAM == null)
                {
                    nvtc.NGAYNGHILAM = nhanvientc.NGAYNGHILAM;
                    nvtc.NGAYVAOLAM = nhanvientc.NGAYVAOLAM;
                }
            }
            else if (nvtc.TRANGTHAI == "Đã Nghỉ Việc" && nhanvientc.NGAYNGHILAM == null)
            {
                if (nvtc.NGAYVAOLAM == null)
                {
                    nvtc.NGAYVAOLAM = nhanvientc.NGAYVAOLAM;
                }
                nvtc.NGAYNGHILAM = DateTime.Now;
            }
            else if (nvtc.TRANGTHAI != "Đã Nghỉ Việc")
            {
                if (nvtc.NGAYVAOLAM == null)
                {
                    nvtc.NGAYVAOLAM = nhanvientc.NGAYVAOLAM;
                }
            }
            nvtc.MANHANVIENTAICHINH = nhanvientc.MANHANVIENTAICHINH;
            db.Entry(nvtc).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("showchitietnvtc", new { id = nvtc.MANHANVIENTAICHINH });
        }

        [HttpGet]
        public ActionResult showchitietnvsx(int id)
        {
            var nhanviensx = db.NHANVIENSANXUATs.FirstOrDefault(m => m.MANHANVIENSANXUAT == id);
            return View(nhanviensx);
        }

        [HttpPost]
        public ActionResult luucapnhapnvsx(NHANVIENSANXUAT nvsx)
        {
            var nhanviensx = db.NHANVIENSANXUATs.AsNoTracking().SingleOrDefault(m => m.MANHANVIENSANXUAT == nvsx.MANHANVIENSANXUAT);
            if (nvsx.TRANGTHAI == "Đã Nghỉ Việc" && nhanviensx.NGAYNGHILAM != null)
            {
                if (nvsx.NGAYVAOLAM == null && nvsx.NGAYNGHILAM != null)
                {
                    nvsx.NGAYVAOLAM = nhanviensx.NGAYVAOLAM;
                }
                else if (nvsx.NGAYNGHILAM == null && nvsx.NGAYVAOLAM != null)
                {
                    nvsx.NGAYNGHILAM = nhanviensx.NGAYNGHILAM;
                }
                else if (nvsx.NGAYVAOLAM == null && nvsx.NGAYNGHILAM == null)
                {
                    nvsx.NGAYNGHILAM = nhanviensx.NGAYNGHILAM;
                    nvsx.NGAYVAOLAM = nhanviensx.NGAYVAOLAM;
                }
            }
            else if (nvsx.TRANGTHAI == "Đã Nghỉ Việc" && nhanviensx.NGAYNGHILAM == null)
            {
                if (nvsx.NGAYVAOLAM == null)
                {
                    nvsx.NGAYVAOLAM = nhanviensx.NGAYVAOLAM;
                }
                nvsx.NGAYNGHILAM = DateTime.Now;
            }
            else if (nvsx.TRANGTHAI != "Đã Nghỉ Việc")
            {
                if (nvsx.NGAYVAOLAM == null)
                {
                    nvsx.NGAYVAOLAM = nhanviensx.NGAYVAOLAM;
                }
            }
            nvsx.MANHANVIENSANXUAT = nhanviensx.MANHANVIENSANXUAT;
            db.Entry(nvsx).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("showchitietnvsx", new { id = nvsx.MANHANVIENSANXUAT });
        }

        [HttpGet]
        public ActionResult showchitietnvvc(int id)
        {
            var nhanvienvc = db.NHANVIENVANCHUYENs.FirstOrDefault(m => m.MANHANVIENVANCHUYEN == id);
            return View(nhanvienvc);
        }

        [HttpPost]
        public ActionResult luucapnhapnvvc(NHANVIENVANCHUYEN nvvc)
        {
            var nhanvienvc = db.NHANVIENVANCHUYENs.AsNoTracking().SingleOrDefault(m => m.MANHANVIENVANCHUYEN == nvvc.MANHANVIENVANCHUYEN);
            if (nvvc.TRANGTHAI == "Đã Nghỉ Việc" && nhanvienvc.NGAYNGHILAM != null)
            {
                if (nvvc.NGAYVAOLAM == null && nvvc.NGAYNGHILAM != null)
                {
                    nvvc.NGAYVAOLAM = nhanvienvc.NGAYVAOLAM;
                }
                else if (nvvc.NGAYNGHILAM == null && nvvc.NGAYVAOLAM != null)
                {
                    nvvc.NGAYNGHILAM = nhanvienvc.NGAYNGHILAM;
                }
                else if (nvvc.NGAYVAOLAM == null && nvvc.NGAYNGHILAM == null)
                {
                    nvvc.NGAYNGHILAM = nhanvienvc.NGAYNGHILAM;
                    nvvc.NGAYVAOLAM = nhanvienvc.NGAYVAOLAM;
                }
            }
            else if (nvvc.TRANGTHAI == "Đã Nghỉ Việc" && nhanvienvc.NGAYNGHILAM == null)
            {
                if (nvvc.NGAYVAOLAM == null)
                {
                    nvvc.NGAYVAOLAM = nhanvienvc.NGAYVAOLAM;
                }
                nvvc.NGAYNGHILAM = DateTime.Now;
            }
            else if (nvvc.TRANGTHAI != "Đã Nghỉ Việc")
            {
                if (nvvc.NGAYVAOLAM == null)
                {
                    nvvc.NGAYVAOLAM = nhanvienvc.NGAYVAOLAM;
                }
            }
            nvvc.MANHANVIENVANCHUYEN = nhanvienvc.MANHANVIENVANCHUYEN;
            db.Entry(nvvc).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("showchitietnvsx", new { id = nvvc.MANHANVIENVANCHUYEN });
        }

        [HttpGet]
        public ActionResult showchitietkh(int id)
        {
            var khachhang = db.KHACHHANGs.FirstOrDefault(m => m.MAKHACHHANG == id);
            return View(khachhang);
        }

        [HttpPost]
        public ActionResult luucapnhapchitietkh(KHACHHANG kh)
        {
            db.Entry(kh).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("showchitietkh", new { id = kh.MAKHACHHANG });
        }


        [HttpGet]
        public ActionResult showchitietsp(int id)
        {
            var sanpham = db.SANPHAMs.FirstOrDefault(m => m.MASANPHAM == id);
            return View(sanpham);
        }

        [HttpPost]
        public ActionResult luucapnhapchitietsanpham(SANPHAM sp, HttpPostedFileBase HinhAnh)
        {
            var sp1 = db.SANPHAMs.AsNoTracking().FirstOrDefault(m => m.MASANPHAM == sp.MASANPHAM);
            if (sp != null)
            {
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/IMG/sanpham"), fileName);
                    HinhAnh.SaveAs(path); // Lưu ảnh vào thư mục

                    sp.ANHSANPHAM = "../IMG/sanpham/" + fileName;
                }
                else
                {
                    sp.ANHSANPHAM = sp1.ANHSANPHAM;
                }
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("showchitietsp", new { id = sp.MASANPHAM });
            }
            else
            {
                return Content("Không lấy được dữ liệu của sản phẩm theo yêu cầu");
            }
        }


        public ActionResult showdivthemnguoi(string kiemtra)
        {
            ViewBag.loainguoi = kiemtra;
            return PartialView("_Partialbangthemnguoi");
        }

        public ActionResult themnguoi(Allnguoidung model)
        {
            try
            {
                if (model.nHANVIENSANXUATs != null)
                {
                    model.nHANVIENSANXUATs.MATKHAU = HashPasswordWithSHA512(model.nHANVIENSANXUATs.MATKHAU);
                    model.nHANVIENSANXUATs.NGAYVAOLAM = DateTime.Now;
                    model.nHANVIENSANXUATs.TRANGTHAI = "Đang làm";
                    db.NHANVIENSANXUATs.Add(model.nHANVIENSANXUATs);
                }
                else if (model.nHANVIENTAICHINHs != null)
                {
                    model.nHANVIENTAICHINHs.NGAYVAOLAM = DateTime.Now;
                    model.nHANVIENTAICHINHs.TRANGTHAI = "Đang làm";
                    db.NHANVIENTAICHINHs.Add(model.nHANVIENTAICHINHs);
                }
                else if (model.nHANVIENVANCHUYENs != null)
                {
                    model.nHANVIENVANCHUYENs.NGAYVAOLAM = DateTime.Now;
                    model.nHANVIENVANCHUYENs.TRANGTHAI = "Đang làm";
                    db.NHANVIENVANCHUYENs.Add(model.nHANVIENVANCHUYENs);
                }
                else
                {
                    ViewBag.thongbao = "Không tìm thấy đối tượng để cập nhật.";
                    return Content(ViewBag.thongbao);
                }

                db.SaveChanges();
                TempData["thongbaothemthanhcong"] = "Cập nhật thông tin thành công";
                return RedirectToAction("showtrangquanly");
            }
            catch (Exception ex)
            {
                ViewBag.thongbao = "Đã xảy ra lỗi khi lưu thông tin đăng ký. Vui lòng thử lại sau.";
                return Content(ViewBag.thongbao);
            }
        }

        

        [HttpGet]
        public ActionResult showchitietlienhe(int id)
        {
            var lienhe = db.LIENHEs.AsNoTracking().SingleOrDefault(m => m.MALIENHE == id);
            return View(lienhe);
        }

        public ActionResult themsanpham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult themsanpham(SANPHAM sp, HttpPostedFileBase HinhAnh)
        {
            if (sp != null)
            {
                sp.DABAN = 0;
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/IMG/sanpham"), fileName);
                    HinhAnh.SaveAs(path); // Lưu ảnh vào thư mục

                    sp.ANHSANPHAM = "../IMG/sanpham/" + fileName;
                }
                db.SANPHAMs.Add(sp);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("showtrangquanly");
                }
                catch (Exception ex)
                {
                    return Content("Lỗi đã xảy ra: " + ex.Message);
                }
            }
            else
            {
                return Content("Lỗi: Model không hợp lệ.");
            }
        }

        public static string HashPasswordWithSHA512(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha512.ComputeHash(bytes);

                // Chuyển hash thành chuỗi Hex
                StringBuilder result = new StringBuilder();
                foreach (byte b in hash)
                {
                    result.Append(b.ToString("x2"));
                }
                return result.ToString();
            }
        }
    }
}