using Antlr.Runtime.Tree;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net.Mail;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WEBDOGO.Models;
using System.Net;
using static WEBDOGO.Models.sepay;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Services;
using Google.Apis.Auth;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Owin.Logging;

namespace WEBDOGO.Controllers
{
    public class UserController : BaseController
    {
        private static readonly string clientId = ConfigurationManager.AppSettings["GoogleClientId"];
        private static readonly string clientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"];
        private const string redirectUri = "https://localhost:44344/User/GoogleLoginCallback";


        SQLWEB2Entities1 db = new SQLWEB2Entities1();

        public ActionResult dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult dangnhap(string taikhoan, string matkhau)
        {
            var captchaResponse = Request["g-recaptcha-response"];
            var secretKey = "6LetciorAAAAAMH4iekaqF36BuMPiRQa9LNurwqT";
            var client = new WebClient();
            var result = client.DownloadString(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}"
            );
            var obj = JObject.Parse(result);
            bool isCaptchaValid = (bool)obj["success"];
            if (!isCaptchaValid)
            {
                TempData["dangnhapthanhcong"] = false;
                ViewBag.thongbao = "Vui lòng xác nhận CAPTCHA.";
                return View();
            }

            if (taikhoan.Length >= 50 || matkhau.Length < 6 || matkhau.Length > 10)
            {
                ViewBag.thongbao = "Sai tài khoản hoặc mật khẩu.";
                TempData["dangnhapthanhcong"] = false;
                return View();
            }

            string hashedPassword = HashPasswordWithSHA512(matkhau);

            var users = new List<dynamic>
            {
                new { User = (object)db.KHACHHANGs.FirstOrDefault(u => u.TENDANGNHAP == taikhoan), Role = "KHACHHANG" },
                new { User = (object)db.ADMINs.FirstOrDefault(u => u.TENDANGNHAP == taikhoan), Role = "ADMIN" },
                new { User = (object)db.NHANVIENTAICHINHs.FirstOrDefault(u => u.TENDANGNHAP == taikhoan), Role = "TAICHINH" },
                new { User = (object)db.NHANVIENSANXUATs.FirstOrDefault(u => u.TENDANGNHAP == taikhoan), Role = "SANXUAT" },
                new { User = (object)db.NHANVIENVANCHUYENs.FirstOrDefault(u => u.TENDANGNHAP == taikhoan), Role = "VANCHUYEN" }
            };

            foreach (var item in users)
            {
                dynamic user = item.User;
                string role = item.Role;
                if (user == null) continue;

                if (taikhoandabikhoa(user))
                {
                    ViewBag.thongbao = "Tài khoản đã bị khóa do đăng nhập sai quá nhiều lần.";
                    TempData["dangnhapthanhcong"] = false;
                    return View();
                }

                if (user.MATKHAU == hashedPassword)
                {
                    resetlogin(user);
                    var sessionToken = Guid.NewGuid().ToString();
                    var cookie = new HttpCookie("SessionToken", sessionToken)
                    {
                        Expires = DateTime.Now.AddHours(1)
                    };
                    Response.Cookies.Add(cookie);
                    user.SessionToken = sessionToken;
                    db.SaveChanges();

                    Session["SessionToken"] = sessionToken;
                    Session["taikhoan"] = user;
                    Session["UserRole"] = role;
                    TempData["dangnhapthanhcong"] = true;

                    switch (role)
                    {
                        case "KHACHHANG":
                            XulyGioHang(user.MAKHACHHANG);
                            try
                            {
                                LICHSUDANGNHAP newls = new LICHSUDANGNHAP
                                {
                                    IDTAIKHOAN = user.MAKHACHHANG,
                                    THOIGIAN = DateTime.Now,
                                    DIA_CHI_IP = layip(Request),
                                    TRINH_DUYET = Request.UserAgent,
                                    THIET_BI = laythietbi(Request.UserAgent)
                                };
                                Debug.WriteLine(newls.THIET_BI);
                                db.LICHSUDANGNHAPs.Add(newls);
                                db.SaveChanges();
                            }
                            catch(Exception ex)
                            {
                                Debug.WriteLine("Lỗi khi lưu lịch sử đăng nhập: " + ex.Message);
                            }
                            Session["idtaikhoan"] = user.MAKHACHHANG;
                            return RedirectToAction("Trangchu", "TrangChu");
                        case "ADMIN":
                            Session["idtaikhoan"] = user.MAADMIN;
                            return RedirectToAction("showtrangquanly", "ADMIN");
                        case "TAICHINH":
                            Session["idtaikhoan"] = user.MANHANVIENTAICHINH;
                            return RedirectToAction("showgiaodienquanly", "Nhanvientaichinh");
                        case "SANXUAT":
                            Session["idtaikhoan"] = user.MANHANVIENSANXUAT;
                            return RedirectToAction("showyeucausanxuat", "Nhanviensanxuat");
                        case "VANCHUYEN":
                            Session["idtaikhoan"] = user.MANHANVIENVANCHUYEN;
                            return RedirectToAction(user.VAITRO == "Trưởng VC" ? "showgiaodienquanly" : "showvanchuyencuatoi", "Nhanvienvanchuyen");
                    }
                }
                else
                {
                    capnhaplansai(user);
                    ViewBag.thongbao = "Sai tài khoản hoặc mật khẩu.";
                    TempData["dangnhapthanhcong"] = false;
                    return View();
                }
            }

            ViewBag.thongbao = "Sai tài khoản hoặc mật khẩu.";
            TempData["dangnhapthanhcong"] = false;
            return View();
        }

        public string laythietbi(string userAgent)
        {
            userAgent = userAgent.ToLower();

            if (userAgent.Contains("mobile") || userAgent.Contains("android") || userAgent.Contains("iphone"))
            {
                return "Thiết bị di động";
            }
            else if (userAgent.Contains("windows") || userAgent.Contains("macintosh") || userAgent.Contains("linux"))
            {
                return "Máy tính";
            }
            else
            {
                return "Không xác định";
            }
        }


        private bool taikhoandabikhoa(dynamic user)
        {
            if (user.BIKHOA == true)
            {
                if (user.SOLANDANGNHAPSAI >= 5 &&
                    DateTime.Now.Subtract(user.THOIGIANDANGNHAPSAICUOICUNG).TotalMinutes >= 30)
                {
                    user.SOLANDANGNHAPSAI = 0;
                    user.BIKHOA = false;
                    db.SaveChanges();
                    return false;
                }
                return true;
            }
            return false;
        }

        private void resetlogin(dynamic user)
        {
            user.SOLANDANGNHAPSAI = 0;
            user.THOIGIANDANGNHAPSAICUOICUNG = null;
            db.SaveChanges();
        }

        private void capnhaplansai(dynamic user)
        {
            user.SOLANDANGNHAPSAI += 1;
            user.THOIGIANDANGNHAPSAICUOICUNG = DateTime.Now;
            if (user.SOLANDANGNHAPSAI >= 5)
            {
                user.BIKHOA = true;
            }
            db.SaveChanges();
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

        private void XulyGioHang(int maKhachHang)
        {
            var listsanpham = Session["sanphamnologin"] as List<giohangsession>;
            if (listsanpham == null) return;

            foreach (var item in listsanpham)
            {
                var sanphamGioHang = db.GIOHANGs.FirstOrDefault(m => m.MASANPHAM == item.ProductId && m.MAKHACHHANG == maKhachHang);
                if (sanphamGioHang != null)
                {
                    sanphamGioHang.SOLUONG += item.Quantity;
                    sanphamGioHang.TONGTIEN = sanphamGioHang.SOLUONG * item.Price;
                    db.Entry(sanphamGioHang).State = EntityState.Modified;
                }
                else
                {
                    db.GIOHANGs.Add(new GIOHANG
                    {
                        MAKHACHHANG = maKhachHang,
                        MASANPHAM = item.ProductId,
                        SOLUONG = item.Quantity,
                        TONGTIEN = item.tongtien,
                        NGAYTHEM = DateTime.Now
                    });
                }
            }
            db.SaveChanges();
        }

        public ActionResult dangky()
        {
            return View();
        }

        [HttpPost]
        public ActionResult dangky(KHACHHANG kh, string matkhaunhaplai)
        {
            var captchaResponse = Request["g-recaptcha-response"];
            var secretKey = "6LetciorAAAAAMH4iekaqF36BuMPiRQa9LNurwqT";
            var client = new WebClient();
            var result = client.DownloadString(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}"
            );
            var obj = JObject.Parse(result);
            bool isCaptchaValid = (bool)obj["success"];
            if (!isCaptchaValid)
            {
                ViewBag.thongbao = "Vui lòng xác nhận CAPTCHA.";
                return View();
            }

            if (kh.EMAIL.Length > 30 || kh.TENDANGNHAP.Length > 50 || kh.MATKHAU.Length > 10)
            {
                ViewBag.thongbao = "Thông tin nhập vào vượt quá độ dài cho phép.";
                return View();
            }

            if (kh.MATKHAU != matkhaunhaplai)
            {
                ViewBag.thongbao = "Mật khẩu nhập lại không khớp.";
                return View();
            }

            bool coChuHoa = kh.MATKHAU.Any(char.IsUpper);
            bool coChuThuong = kh.MATKHAU.Any(char.IsLower);
            bool coSo = kh.MATKHAU.Any(char.IsDigit);
            bool coKyTuDacBiet = kh.MATKHAU.Any(c => !char.IsLetterOrDigit(c));

            if (!(coChuHoa && coChuThuong && coSo && coKyTuDacBiet))
            {
                ViewBag.thongbao = "Mật khẩu phải có : chữ hoa, chữ thường, số và ký tự đặc biệt.";
                return View();
            }

            var khachhang = db.KHACHHANGs.FirstOrDefault(u => u.EMAIL == kh.EMAIL || u.TENDANGNHAP == kh.TENDANGNHAP);
            if (khachhang != null)
            {
                ViewBag.thongbao = "Email hoặc tên đăng nhập đã được sử dụng.";
                return View();
            }

            try
            {
                string code = new Random().Next(1000, 9999).ToString();
                kh.MATKHAU = HashPasswordWithSHA512(kh.MATKHAU);
                kh.BIKHOA = false;
                kh.SOLANDANGNHAPSAI = 0;
                Session["dangkytaikhoan"] = kh;
                Session["codexacnhan"] = code;
                Session["thoigian_code"] = DateTime.Now.AddMinutes(10);

                guigmaildangkytaikhoan(kh, code);

                return RedirectToAction("nhapcodexacnhantaikhoan");
            }
            catch (DbUpdateException)
            {
                ViewBag.thongbao = "Đã xảy ra lỗi khi lưu thông tin đăng ký. Vui lòng thử lại sau.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult nhapcodexacnhantaikhoan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult nhapcodexacnhantaikhoan(string userinputcode)
        {
            if (Session["codexacnhan"] != null && Session["thoigian_code"] != null)
            {
                string code = Session["codexacnhan"].ToString();
                DateTime thoigian_code = (DateTime)Session["thoigian_code"];

                if (DateTime.Now <= thoigian_code)
                {
                    if (userinputcode == code)
                    {
                        KHACHHANG newkh = (KHACHHANG)Session["dangkytaikhoan"];
                        db.KHACHHANGs.Add(newkh);
                        db.SaveChanges();
                        Session.Remove("dangkytaikhoan");
                        Session.Remove("code");
                        Session.Remove("thoigian_code");
                        TempData["dangkytaikhoanthanhcong"] = true;
                        return RedirectToAction("dangnhap");
                    }
                    else
                    {
                        ViewBag.thongbao = "Mã xác nhận không đúng.";
                        return View();
                    }
                }
                else
                {
                    Session.Remove("dangkytaikhoan");
                    Session.Remove("code");
                    Session.Remove("thoigian_code");
                    ViewBag.thongbao = "Mã xác nhận đã hết hạn. Vui lòng đăng ký lại.";
                    return RedirectToAction("dangky");
                }

            }
            else
            {
                ViewBag.thongbao = "Không tìm thấy mã xác nhận.";
                return RedirectToAction("dangky");
            }
        }

        public void guigmaildangkytaikhoan(KHACHHANG kh, string code)
        {
            string username = "2224802010208@student.tdmu.edu.vn";
            string password = "dbmd wckm qknh zazz";

            try
            {
                string body = $@"
                    <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #eee; border-radius: 10px; background-color: #f9f9f9; max-width: 500px; margin: auto;'>
                        <h2 style='color: #2d8cf0;'>Xác nhận đăng ký tài khoản</h2>
                        <p>Xin chào <strong>{kh.TENDANGNHAP}</strong>,</p>
                        <p>Bạn vừa đăng ký tài khoản tại hệ thống <strong>WEBDOGO</strong>.</p>
                        <p>Vui lòng sử dụng mã xác nhận sau để hoàn tất quá trình đăng ký:</p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <span style='font-size: 28px; font-weight: bold; color: #ffffff; background-color: #2d8cf0; padding: 10px 20px; border-radius: 6px;'>{code}</span>
                        </div>
                        <p>Mã xác nhận có hiệu lực trong vòng <strong>10 phút</strong>.</p>
                        <p>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.</p>
                        <p style='margin-top: 30px;'>Trân trọng,<br><strong>Đội ngũ WEBDOGO</strong></p>
                    </div>";

                // Cấu hình email
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(username);
                mail.To.Add(kh.EMAIL);
                mail.Subject = "Xác nhận đăng ký ( WEB ĐỒ GỖ XỊN XÒ )";
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(username, password);
                smtp.EnableSsl = true;

                // Gửi email
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gửi email thất bại: " + ex.Message);
            }
        }

        public ActionResult showgiohang()
        {
            if (Session["taikhoan"] != null)
            {
                var khachhang = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                var danhsachgiohang = (from gh in db.GIOHANGs
                                       join sp in db.SANPHAMs on gh.MASANPHAM equals sp.MASANPHAM
                                       where gh.MAKHACHHANG == khachhang.MAKHACHHANG
                                       select new thanhtoan_giohang
                                       {
                                           gIOHANG = gh,
                                           sANPHAM = sp,
                                       }).ToList();
                
                double tongsotienbandau = 0;
                foreach (var item in danhsachgiohang)
                {
                    tongsotienbandau += (double)item.gIOHANG.TONGTIEN;
                }
                ViewBag.danhsachgiohang = danhsachgiohang;
                ViewBag.tongsotienbandau = tongsotienbandau;
                ViewBag.Severname = AppConfig.ServerName;
                return View();
            }
            else
            {
                var listsanphamnologin = Session["sanphamnologin"] as List<giohangsession>;
                ViewBag.listsanphamnologin = listsanphamnologin;
                double tongtien = 0;
                foreach(var item in listsanphamnologin)
                {
                    var tongtienmoiitem = item.Price * item.Quantity;
                    tongtien += tongtienmoiitem;
                }
                ViewBag.tongtien = tongtien;
                ViewBag.Severname = AppConfig.ServerName;
                return View();
            }
        }

        [HttpPost]
        public ActionResult themvaogiohang(int idkh, int idsanpham, int giasanpham, DateTime ngaythem, int soluong)
        {
            Debug.WriteLine("đã tới hàm giỏ hàng");
            var kiemtragiohang = db.GIOHANGs.FirstOrDefault(g => g.MASANPHAM == idsanpham && g.MAKHACHHANG == idkh);
            if (kiemtragiohang == null)
            {
                Debug.WriteLine("giỏ hàng khác null");
                var sp = db.SANPHAMs.FirstOrDefault(m => m.MASANPHAM == idsanpham);
                if (sp.SOLUONG == 0)
                {
                    Debug.WriteLine("số lượng 0 : lỗi");
                    return RedirectToAction("chitietsanpham", "TrangChu", new { id = idsanpham });
                }
                else
                {
                    int tongtien = giasanpham * soluong;
                    WEBDOGO.Models.GIOHANG giohang = new WEBDOGO.Models.GIOHANG();
                    giohang.MASANPHAM = idsanpham;
                    giohang.MAKHACHHANG = idkh;
                    giohang.TONGTIEN = tongtien;
                    giohang.SOLUONG = soluong;
                    giohang.NGAYTHEM = ngaythem;
                    try
                    {
                        TempData["thongbaothemthanhcong"] = "Thêm sản phẩm vào giỏ hàng thành công";

                        db.GIOHANGs.Add(giohang);
                        db.SaveChanges();
                        return RedirectToAction("chitietsanpham", "TrangChu", new { id = idsanpham });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.thongbao = "Đã xảy ra lỗi khi lưu thông tin đăng ký. Vui lòng thử lại sau.";
                        return Content(ViewBag.thongbao);
                    }
                }
            }
            else
            {
                kiemtragiohang.SOLUONG = kiemtragiohang.SOLUONG + soluong;
                kiemtragiohang.TONGTIEN = kiemtragiohang.SOLUONG * giasanpham;
                try
                {
                    TempData["thongbaothemthanhcong"] = "Thêm sản phẩm vào giỏ hàng thành công";

                    db.Entry(kiemtragiohang).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("chitietsanpham", "TrangChu", new { id = idsanpham });
                }
                catch (Exception ex)
                {
                    ViewBag.thongbao = "Đã xảy ra lỗi khi lưu thông tin đăng ký. Vui lòng thử lại sau.";
                    return Content(ViewBag.thongbao);
                }
            }
        }

        public ActionResult xoakhoigiohang(int idgiohang)
        {
            if (Session["taikhoan"] != null)
            {
                var kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                var sanphamcanxoa = db.GIOHANGs.FirstOrDefault(g => g.MAGIOHANG == idgiohang);
                if (sanphamcanxoa != null)
                {
                    db.GIOHANGs.Remove(sanphamcanxoa);
                    db.SaveChanges();
                    return RedirectToAction("showgiohang", "User");
                }
                else
                {
                    return RedirectToAction("showgiohang", "User");
                }
            }
            else
            {
                var listsanpham = Session["sanphamnologin"] as List<giohangsession>;
                var sanpham = listsanpham.FirstOrDefault(sp => sp.ProductId == idgiohang);
                listsanpham.Remove(sanpham);
                Session["sanphamnologin"] = listsanpham;
                return RedirectToAction("showgiohang", "User");
            }
        }


        public  ActionResult capnhapthongtinnguoidung()
        {
            if (Session["taikhoan"] != null)
            {
                var kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                return View(kh);
            }
            else
            {
                return RedirectToAction("Trangchu","TrangChu");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult capnhapthongtinnguoidung(KHACHHANG khvao)
        {
            KHACHHANG tkkh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
            var existingKhachHang = db.KHACHHANGs.FirstOrDefault(kh => kh.MAKHACHHANG == tkkh.MAKHACHHANG);

            if (existingKhachHang != null)
            {
                // Cập nhật thông tin khách hàng
                existingKhachHang.HOVATEN = khvao.HOVATEN;
                existingKhachHang.TUOI = khvao.TUOI;
                existingKhachHang.TINH = khvao.TINH;
                existingKhachHang.DIACHICHITIET = khvao.DIACHICHITIET;
                existingKhachHang.EMAIL = khvao.EMAIL;
                existingKhachHang.SDT = khvao.SDT;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
                Session["taikhoan"] = existingKhachHang;

                return RedirectToAction("capnhapthongtinnguoidung" , "User");
            }
            else
            {
                ViewBag.thongbao = "Người dùng không tồn tại";
                return Content(ViewBag.thongbao);
            }
        }

        public ActionResult lienhe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult lienhe(LIENHE lh)
        {
            if(lh != null)
            {
                var kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                if(kh != null)
                {
                    lh.MAKHACHHANG = kh.MAKHACHHANG;
                    db.LIENHEs.Add(lh);
                    db.SaveChanges();
                    return RedirectToAction("lienhe", "User");
                }
                else
                {
                    return RedirectToAction("dangnhap");
                }
                
            }
            else
            {
                ViewBag.thongbao = "Không thể lưu";
                return Content(ViewBag.thongbao);
            }
        }

        public ActionResult showsanphamtheoyeucau()
        {
            if (Session["taikhoan"] != null)
            {

                var khachhang = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                var sanphamtheoyeucau = db.SANPHAMTHEOYEUCAUs.Where(m => m.MAKHACHHANG == khachhang.MAKHACHHANG);

                var listsanphamdadat = from sp in db.SANPHAMTHEOYEUCAUs
                                       join vc in db.VANCHUYENs on sp.MASANPHAMTHEOYEUCAU equals vc.MASANPHAMTHEOYEUCAU into vcGroup
                                       from vc in vcGroup.DefaultIfEmpty()
                                       join td in db.TIENDOSANXUATs on sp.MASANPHAMTHEOYEUCAU equals td.MASANPHAMTHEOYEUCAU into tdGroup
                                       from td in tdGroup.DefaultIfEmpty()
                                       where sp.MAKHACHHANG == khachhang.MAKHACHHANG
                                       select new SanPhamTheoYeuCauKhachHangViewModel
                                       {
                                           SanPham = sp,
                                           VANCHUYENs = vc,
                                           TIENDOSANXUATs = td,
                                       };
                ViewBag.danhsachsanphamdadat = listsanphamdadat.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("dangnhap");
            }
        }

        public ActionResult chitietvanchuyen(int mavc)
        {
            if(mavc != null)
            {
                var vc = db.VANCHUYENs.FirstOrDefault(m => m.MAVANCHUYEN == mavc);

                var khachhang = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                var sanphamtheoyeucau = db.SANPHAMTHEOYEUCAUs.Where(m => m.MAKHACHHANG == khachhang.MAKHACHHANG);

                var listsanphamdadat = from sp in db.SANPHAMTHEOYEUCAUs
                                       join vc1 in db.VANCHUYENs on sp.MASANPHAMTHEOYEUCAU equals vc1.MASANPHAMTHEOYEUCAU into vcGroup
                                       from vc1 in vcGroup.DefaultIfEmpty()
                                       join td in db.TIENDOSANXUATs on sp.MASANPHAMTHEOYEUCAU equals td.MASANPHAMTHEOYEUCAU into tdGroup
                                       from td in tdGroup.DefaultIfEmpty()
                                       where sp.MAKHACHHANG == khachhang.MAKHACHHANG
                                       select new SanPhamTheoYeuCauKhachHangViewModel
                                       {
                                           SanPham = sp,
                                           VANCHUYENs = vc1,
                                           TIENDOSANXUATs = td,
                                       };
                ViewBag.danhsachsanphamdadat = listsanphamdadat.ToList();
                var nhanvienvanchuyen = db.NHANVIENVANCHUYENs.FirstOrDefault(m => m.MANHANVIENVANCHUYEN == vc.MANHANVIENVANCHUYEN);
                ViewBag.vanchuyen = vc;
                ViewBag.nhanvienvanchuyen = nhanvienvanchuyen.TENNHANVIENVANCHUYEN;
                return PartialView("_Partialchitietvanchuyen_Khachhang");
            }
            else
            {
                return Content("Không tìm thấy vận chuyển");
            }
        }

        public ActionResult chitiettiendosanxuat(int matd)
        {
            if(matd != null)
            {
                var td = db.TIENDOSANXUATs.FirstOrDefault(m => m.MATIENDOSANXUAT == matd);
                var khachhang = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                var sanphamtheoyeucau = db.SANPHAMTHEOYEUCAUs.Where(m => m.MAKHACHHANG == khachhang.MAKHACHHANG);

                var listsanphamdadat = from sp in db.SANPHAMTHEOYEUCAUs
                                       join vc in db.VANCHUYENs on sp.MASANPHAMTHEOYEUCAU equals vc.MASANPHAMTHEOYEUCAU into vcGroup
                                       from vc in vcGroup.DefaultIfEmpty()
                                       join td1 in db.TIENDOSANXUATs on sp.MASANPHAMTHEOYEUCAU equals td1.MASANPHAMTHEOYEUCAU into tdGroup
                                       from td1 in tdGroup.DefaultIfEmpty()
                                       where sp.MAKHACHHANG == khachhang.MAKHACHHANG
                                       select new SanPhamTheoYeuCauKhachHangViewModel
                                       {
                                           SanPham = sp,
                                           VANCHUYENs = vc,
                                           TIENDOSANXUATs = td1,
                                       };
                ViewBag.danhsachsanphamdadat = listsanphamdadat.ToList();
                ViewBag.tiendosanxuat = td;
                var nhanviensanxuat = db.NHANVIENSANXUATs.FirstOrDefault(m => m.MANHANVIENSANXUAT == td.MANHANVIENSANXUAT);

                if(nhanviensanxuat != null )
                {
                    ViewBag.tennhanviensanxuat = nhanviensanxuat.TENNHANVIENSANXUAT;
                }
                else
                {
                    ViewBag.tennhanviensanxuat = "Chưa có";
                }
                return PartialView("_Partialchitiettiendosanxuat_khachhang");
            }
            else
            {
                return Content("Không tìm thấy vận chuyển");
            }
        }

        [HttpPost]
        public ActionResult themsanphamtheoyeucau(SANPHAMTHEOYEUCAU sptyc, HttpPostedFileBase HinhAnh)
        {
            Debug.WriteLine("đã đến hàm");
            if (sptyc != null)
            {
                var khachhang = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                sptyc.MAKHACHHANG = khachhang.MAKHACHHANG;
                sptyc.TRANGTHAIDUYET = "Chưa duyệt";
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/IMG/sanpham"), fileName);
                    HinhAnh.SaveAs(path); // Lưu ảnh vào thư mục

                    sptyc.ANHMINHHOA = "../IMG/sanpham/" + fileName;
                }
                sptyc.KHACHHANGCHAPNHAN = "Chưa có";
                db.SANPHAMTHEOYEUCAUs.Add(sptyc);
                db.SaveChanges();
                return RedirectToAction("showsanphamtheoyeucau");
            }
            else
            {
                return Content("Không lấy được dữ liệu của sản phẩm theo yêu cầu");
            }
        }

        public ActionResult thanhtoan()
        {
            if (Session["taikhoan"] !=  null)
            {
                KHACHHANG kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
                var danhsachgiohang = from gh in db.GIOHANGs
                                      join kh1 in db.KHACHHANGs on gh.MAKHACHHANG equals kh1.MAKHACHHANG
                                      join sp in db.SANPHAMs on gh.MASANPHAM equals sp.MASANPHAM
                                      where gh.MAKHACHHANG == kh.MAKHACHHANG
                                      select new thanhtoan_giohang
                                      {
                                          sANPHAM = sp,
                                          gIOHANG = gh,
                                      };

                var danhsachsanpham = danhsachgiohang.ToList();
                ViewBag.dssp = danhsachsanpham;
                return View();
            }
            else
            {
                return RedirectToAction("dangnhap");
            }
        }

        public string TaoSoHoaDon()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string soHoaDon;

            do
            {
                soHoaDon = new string(Enumerable.Repeat(chars, 5)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            } while (db.HOADONs.Any(h => h.SOHOADON == soHoaDon));

            return soHoaDon;
        }

        public ActionResult dathang(double tongsotien,string paymentMethod)
        {

            KHACHHANG kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];

            if(kh.HOVATEN == null || kh.TUOI == null || kh.SDT == null || kh.TINH == null || kh.DIACHICHITIET == null)
            {
                return RedirectToAction("capnhapthongtinnguoidung");
            }

            HOADON newhd = new HOADON();
            newhd.MAKHACHHANG = kh.MAKHACHHANG;
            newhd.TONGTIEN = tongsotien;
            newhd.TRANGTHAITHANHTOAN = "Chưa thanh toán";
            if(paymentMethod == "Thanhtoanonline")
            {
                newhd.PHUONGTHUCTHANHTOAN = "Thanh toán online";
            }
            else
            {
                newhd.PHUONGTHUCTHANHTOAN = "Thanh toán khi nhận hàng";
            }
            newhd.SOHOADON = TaoSoHoaDon();
            newhd.NGAYLAP = DateTime.Now;

            db.HOADONs.Add(newhd);
            db.SaveChanges();


            var danhsachgiohang = from gh in db.GIOHANGs
                                  join kh1 in db.KHACHHANGs on gh.MAKHACHHANG equals kh1.MAKHACHHANG
                                  join sp in db.SANPHAMs on gh.MASANPHAM equals sp.MASANPHAM
                                  where gh.MAKHACHHANG == kh.MAKHACHHANG
                                  select new thanhtoan_giohang
                                  {
                                      sANPHAM = sp,
                                      gIOHANG = gh,
                                  };

            var danhsachsanpham = danhsachgiohang.ToList();

            foreach(var item in danhsachsanpham)
            {
                CHITIETHOADON newcthd = new CHITIETHOADON();

                newcthd.MAHOADON = newhd.MAHOADON;
                newcthd.MASANPHAM = item.gIOHANG.MASANPHAM;
                newcthd.TONGTIEN = item.gIOHANG.TONGTIEN;
                newcthd.SOLUONG = item.gIOHANG.SOLUONG;

                var spphaitru = db.SANPHAMs.FirstOrDefault(s => s.MASANPHAM == item.sANPHAM.MASANPHAM);
                spphaitru.SOLUONG-=item.gIOHANG.SOLUONG;
                if(spphaitru.DABAN == null )
                {
                    spphaitru.DABAN = 0;
                    spphaitru.DABAN += item.gIOHANG.SOLUONG;
                }
                else
                {
                    spphaitru.DABAN += item.gIOHANG.SOLUONG;
                }

                db.Entry(spphaitru).State = EntityState.Modified;
                db.CHITIETHOADONs.Add(newcthd);
                db.SaveChanges();
            }

            var danhsachgh = (from gh in db.GIOHANGs
                                  where gh.MAKHACHHANG == kh.MAKHACHHANG
                                  select gh).ToList();
            foreach(var item in danhsachgh)
            {
                db.GIOHANGs.Remove(item);
                db.SaveChanges();
            }

            var danhsachchitietsanphamcuahoadon = (from cthd in db.CHITIETHOADONs
                                                   join sp in db.SANPHAMs on cthd.MASANPHAM equals sp.MASANPHAM
                                                   where cthd.MAHOADON == newhd.MAHOADON
                                                   select new chitiethoadon_sanpham
                                                   {
                                                       sCHITIETHOADON = cthd,
                                                       sSANPHAM = sp,
                                                   }).ToList();

            

            ViewBag.danhsachcthdcuahd = danhsachchitietsanphamcuahoadon;
            ViewBag.sohoadon = newhd;

            if(paymentMethod == "Thanhtoanonline")
            {
                return View();
            }
            else
            {
                TempData["Hoadon"] = newhd;
                return RedirectToAction("dathangoffline");
            }
        }

        public ActionResult replacethanhtoan(int mahd)
        {
            ViewBag.sohoadon = db.HOADONs.FirstOrDefault(m => m.MAHOADON == mahd);
            ViewBag.sanphamtheoyeucau = (from hd in db.HOADONs
                                         join sptyc in db.SANPHAMTHEOYEUCAUs on hd.MAHOADON equals sptyc.MAHOADON
                                         where hd.MAHOADON == mahd
                                         select new chitiethoadon_sanpham
                                         {
                                             hOADON = hd,
                                             sANPHAMTHEOYEUCAU = sptyc,
                                         }).ToList();
            

            if(ViewBag.sanphamtheoyeucau != null)
            {
                return View();
            }
            else
            {
                ViewBag.chitethoadon = (from hd in db.HOADONs
                                        join cthd in db.CHITIETHOADONs on hd.MAHOADON equals cthd.MAHOADON
                                        join sp in db.SANPHAMs on cthd.MASANPHAM equals sp.MASANPHAM
                                        where hd.MAHOADON == mahd
                                        select new chitiethoadon_sanpham
                                        {
                                            hOADON = hd,
                                            sCHITIETHOADON = cthd,
                                            sSANPHAM = sp,
                                        }).ToList();
                return View();
            }
        }

        public ActionResult dathangoffline()
        {
            HOADON hoadon = TempData["Hoadon"] as HOADON;

            var kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];

            var dsgiohang = (from hd in db.HOADONs
                             join cthd in db.CHITIETHOADONs on hd.MAHOADON equals cthd.MAHOADON
                             join sp in db.SANPHAMs on cthd.MASANPHAM equals sp.MASANPHAM
                             where hd.SOHOADON == hoadon.SOHOADON
                             select new chitiethoadon_sanpham
                             {
                                 sCHITIETHOADON = cthd,
                                 sSANPHAM = sp,
                             }).ToList();

            foreach(var item in dsgiohang)
            {
                VANCHUYEN newvc = new VANCHUYEN();
                newvc.NGAYBATDAUGUI = DateTime.Now.AddDays(1);
                newvc.PHUONGTHUCVANCHUYEN = "Vận chuyển nhanh";
                newvc.CHIPHIVANCHUYEN = 0;
                newvc.MOTA = "Đơn hàng đã được tạo hãy vận chuyển đơn hàng đến khách hàng sớm nhất có thể";
                newvc.TRANGTHAIVANCHUYEN = "Đơn hàng đã tạo";
                newvc.MACHITIETHOADON = item.sCHITIETHOADON.MACHITIETHOADON;
                db.VANCHUYENs.Add(newvc);
            }
            db.SaveChanges();
            guimai(dsgiohang, hoadon, kh);
            return View();
        }

        public void guimai(List<chitiethoadon_sanpham> dsgiohang, HOADON hoadon,KHACHHANG kh )
        {
            string username = "2224802010208@student.tdmu.edu.vn";
            string password = "dbmd wckm qknh zazz";

            try
            {
                // Nội dung email với CSS đẹp mắt
                string body = $@"
<div style='font-family:Arial, sans-serif; max-width:600px; margin:auto; border:1px solid #ddd; border-radius:8px; overflow:hidden; box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);'>
    <div style='background-color:#4CAF50; padding:25px; text-align:center; color:white;'>
        <h2 style='font-size:28px; margin-bottom:10px;'>Xác nhận đơn hàng</h2>
        <p style='font-size:16px; margin:0;'>Cảm ơn bạn đã đặt hàng tại nhà sách online!</p>
    </div>
    <div style='padding:30px; background-color:#fafafa;'>
        <h3 style='font-size:22px; color:#333; margin-bottom:15px;'>Chi tiết đơn hàng</h3>
        <table style='width:100%; border-collapse:collapse;'>
            <thead>
                <tr style='background-color:#FFB591; color:#555;'>
                    <th style='padding:12px; border-bottom:2px solid #ddd;'>Mã Sản Phẩm</th>
                    <th style='padding:12px; border-bottom:2px solid #ddd;'>Tên Sản Phẩm</th>
                    <th style='padding:12px; border-bottom:2px solid #ddd;'>Số Lượng</th>
                    <th style='padding:12px; border-bottom:2px solid #ddd;'>Đơn Giá</th>
                    <th style='padding:12px; border-bottom:2px solid #ddd;'>Thành Tiền</th>
                </tr>
            </thead>
            <tbody>";

                foreach (var item1 in dsgiohang)
                {
                    if(item1.sCHITIETHOADON != null)
                    {
                        body += $@"
                <tr style='background-color:#fff; border-bottom:1px solid #eee;'>
                    <td style='padding:10px; text-align:center; color:#333;'>{item1.sCHITIETHOADON.MASANPHAM}</td>
                    <td style='padding:10px; color:#333;'>{item1.sSANPHAM.TENSANPHAM}</td>
                    <td style='padding:10px; text-align:center; color:#333;'>{item1.sCHITIETHOADON.SOLUONG}</td>
                    <td style='padding:10px; text-align:right; color:#333;'>{item1.sSANPHAM.GIASANPHAM:#,##0} VNĐ</td>
                    <td style='padding:10px; text-align:right; color:#333;'>{item1.sCHITIETHOADON.TONGTIEN:#,##0} VNĐ</td>
                </tr>";
                    }
                    else
                    {
                        body += $@"
                <tr style='background-color:#fff; border-bottom:1px solid #eee;'>
                    <td style='padding:10px; text-align:center; color:#333;'>{item1.sANPHAMTHEOYEUCAU.MASANPHAMTHEOYEUCAU}</td>
                    <td style='padding:10px; color:#333;'>{item1.sANPHAMTHEOYEUCAU.LOAISANPHAM}</td>
                    <td style='padding:10px; text-align:center; color:#333;'>{item1.sANPHAMTHEOYEUCAU.SOLUONG}</td>
                    <td style='padding:10px; text-align:right; color:#333;'>{item1.sANPHAMTHEOYEUCAU.TONGSOTIENSANXUAT:#,##0} VNĐ</td>
                    <td style='padding:10px; text-align:right; color:#333;'>{item1.sANPHAMTHEOYEUCAU.TONGSOTIENSANXUAT:#,##0} VNĐ</td>
                </tr>";
                    }
                    
                }

                body += $@"
            </tbody>
        </table>
        <div style='text-align:right; margin-top:20px; font-size:16px; color:#333;'>
            <p><b>Tổng tiền:</b> {hoadon.TONGTIEN:#,##0} VNĐ</p>
        </div>
</div>";
                // Cấu hình email
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(username);
                mail.To.Add(kh.EMAIL);
                mail.Subject = "Xác Nhận Đơn Hàng";
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Cấu hình SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(username, password);
                smtp.EnableSsl = true;

                // Gửi email
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gửi email thất bại: " + ex.Message);
            }
        }

        public ActionResult hoadoncuauser()
        {
            if (Session["taikhoan"] != null)
            {
                KHACHHANG kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];

                var danhsachgh = db.HOADONs.Where(s => s.MAKHACHHANG == kh.MAKHACHHANG).ToList();
                ViewBag.danhsachhd = danhsachgh;

                return View();
            }
            else
            {
                return RedirectToAction("dangnhap");
            }
        }

        public ActionResult chitiethoadoncuauser(int mahd)
        {
            HOADON hd1 = db.HOADONs.FirstOrDefault(m => m.MAHOADON == mahd);
            if (hd1 == null)
            {
                return HttpNotFound("Hóa đơn không tồn tại.");
            }

            var chitiethoadonSanphamList = (from hd in db.HOADONs
                                            join cthd1 in db.CHITIETHOADONs on hd.MAHOADON equals cthd1.MAHOADON into cthd1group
                                            from cthd1 in cthd1group.DefaultIfEmpty()
                                            join sp in db.SANPHAMs on cthd1.MASANPHAM equals sp.MASANPHAM into spgroup
                                            from sp in spgroup.DefaultIfEmpty()
                                            join vc in db.VANCHUYENs on cthd1.MACHITIETHOADON equals vc.MACHITIETHOADON into vcgroup
                                            from vc in vcgroup.DefaultIfEmpty()
                                            where hd.MAHOADON == mahd
                                            select new chitiethoadon_sanpham
                                            {
                                                sCHITIETHOADON = cthd1,
                                                sSANPHAM = sp,
                                                vANCHUYEN = vc,
                                            }).ToList();

            if (chitiethoadonSanphamList.ElementAt(0).sCHITIETHOADON != null)
            {
                ViewBag.hoadoncuauser = hd1;
                ViewBag.chitiethoadoncantim = chitiethoadonSanphamList;
                return View();
            }
            else
            {
                var chitietsanphamtheoyeucauList = (from hd in db.HOADONs
                                                    join sptyc in db.SANPHAMTHEOYEUCAUs on hd.MAHOADON equals sptyc.MAHOADON into sptycGroup
                                                    from sptyc in sptycGroup.DefaultIfEmpty()
                                                    join vc in db.VANCHUYENs on sptyc.MASANPHAMTHEOYEUCAU equals vc.MASANPHAMTHEOYEUCAU into vcGroup
                                                    from vc in vcGroup.DefaultIfEmpty()
                                                    where hd.MAHOADON == mahd
                                                    select new chitiethoadon_sanpham
                                                    {
                                                        sANPHAMTHEOYEUCAU = sptyc,
                                                        hOADON = hd,
                                                        vANCHUYEN = vc,
                                                    }).ToList();

                ViewBag.hoadoncuauser = hd1;
                ViewBag.chitietsanphamtheoyeucau = chitietsanphamtheoyeucauList;
                return View();
            }

        }

        public ActionResult xemchitietvanchuyencuachitiethoadonuser(int mahd, int mavc)
        {

            HOADON hd1 = db.HOADONs.FirstOrDefault(m => m.MAHOADON == mahd);
            ViewBag.hoadoncuauser = hd1;

            if (hd1 == null)
            {
                return HttpNotFound("Hóa đơn không tồn tại.");
            }

            var cthd = from hd in db.HOADONs
                       join cthd1 in db.CHITIETHOADONs on hd.MAHOADON equals cthd1.MAHOADON
                       join sp in db.SANPHAMs on cthd1.MASANPHAM equals sp.MASANPHAM
                       join vc in db.VANCHUYENs on cthd1.MACHITIETHOADON equals vc.MACHITIETHOADON
                       join nvvc in db.NHANVIENVANCHUYENs on vc.MANHANVIENVANCHUYEN equals nvvc.MANHANVIENVANCHUYEN
                       where hd.MAHOADON == mahd
                       select new chitiethoadon_sanpham
                       {
                           sCHITIETHOADON = cthd1,
                           sSANPHAM = sp,
                           vANCHUYEN = vc,
                           hANVIENVANCHUYEN = nvvc,
                       };

            ViewBag.chitietsanphamtheoyeucau = (from hd in db.HOADONs
                                                join sptyc in db.SANPHAMTHEOYEUCAUs on hd.MAHOADON equals sptyc.MAHOADON
                                                join vc in db.VANCHUYENs on sptyc.MASANPHAMTHEOYEUCAU equals vc.MASANPHAMTHEOYEUCAU
                                                where hd.MAHOADON == mahd
                                                select new chitiethoadon_sanpham
                                                {
                                                    sANPHAMTHEOYEUCAU = sptyc,
                                                    hOADON = hd,
                                                    vANCHUYEN = vc,
                                                }).ToList();

            ViewBag.chitiethoadoncantim = cthd.ToList();

            ViewBag.vanchuyen = db.VANCHUYENs.FirstOrDefault(m => m.MAVANCHUYEN == mavc);

            ViewBag.nvvc = (from vc in db.VANCHUYENs
                           join nvvc in db.NHANVIENVANCHUYENs on vc.MANHANVIENVANCHUYEN equals nvvc.MANHANVIENVANCHUYEN
                           where vc.MAVANCHUYEN == mavc
                           select new vanchuyen_nvvanchuyen
                           {
                               nHANVIENVANCHUYEN = nvvc,
                               vANCHUYEN = vc,
                           }).FirstOrDefault();


            return View();
        }



        public ActionResult chapnhandonhang(int masp)
        {
            var sptyc = db.SANPHAMTHEOYEUCAUs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == masp);
            sptyc.KHACHHANGCHAPNHAN = "Chấp nhận";
            var kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];
            HOADON newhd = new HOADON()
            {
                MAKHACHHANG = kh.MAKHACHHANG,
                TONGTIEN = sptyc.TONGSOTIENSANXUAT,
                TRANGTHAITHANHTOAN = "Chưa thanh toán",
                PHUONGTHUCTHANHTOAN = "Thanh toán online",
                MANHANVIENTAICHINH = 1,
                NGAYLAP = DateTime.Now,
                SOHOADON = TaoSoHoaDon(),
            };
            db.HOADONs.Add(newhd);
            db.SaveChanges();

            sptyc.MAHOADON = newhd.MAHOADON;
            db.Entry(sptyc).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.hoadon = newhd;
            ViewBag.sptyc1 = sptyc;
            return View();
        }

        public ActionResult khongchapnhandonhang(int masp)
        {
            var sptyc = db.SANPHAMTHEOYEUCAUs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == masp);
            sptyc.KHACHHANGCHAPNHAN = "Không chấp nhận";
            sptyc.TRANGTHAIDUYET = "Hủy đơn hàng";
            db.Entry(sptyc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("showsanphamtheoyeucau");
        }

        private readonly sepay _model = new sepay();

        public async Task<ActionResult> sepay1(int mahd)
        {
            var data = await _model.FetchTransactionsAsync();
            var kh = (WEBDOGO.Models.KHACHHANG)Session["taikhoan"];

            if (data.error != null)
            {
                ViewBag.Error = data.error;
                return Content("Không tìm thấy hóa đơn hoặc lỗi không thể tìm thấy");
            }
            else
            {
                var danhsachgiaodich = data.transactions;
                var hoadon = db.HOADONs.FirstOrDefault(m => m.MAHOADON == mahd);

                foreach(var item in danhsachgiaodich)
                {
                    double hoadonTongTienValue = hoadon.TONGTIEN ?? 0.0;
                    string transactionContentFirst5 = item.transaction_content.ToString().Substring(0, 5);
                    
                    if (double.TryParse(item.amount_in.ToString(), out double amountInValue))
                    {
                        if (hoadon.SOHOADON == transactionContentFirst5 && Math.Abs(hoadonTongTienValue - amountInValue) < 0.001)
                        {
                            hoadon.TRANGTHAITHANHTOAN = "đã thanh toán";
                            db.Entry(hoadon).State = EntityState.Modified;
                            db.SaveChanges();

                            var sptyc = db.SANPHAMTHEOYEUCAUs.FirstOrDefault(m => m.MAHOADON == mahd);
                            if(sptyc != null)
                            {
                                var daylasptyc = (from hd in db.HOADONs
                                                  join sptyc1 in db.SANPHAMTHEOYEUCAUs on hd.MAHOADON equals sptyc1.MAHOADON
                                                  where hd.MAHOADON == mahd
                                                  select new chitiethoadon_sanpham
                                                  {
                                                      sANPHAMTHEOYEUCAU = sptyc1
                                                  }).ToList();

                                foreach(var i in daylasptyc)
                                {
                                    DOANHTHU dt = new DOANHTHU
                                    {
                                        SOLUONG = i.sANPHAMTHEOYEUCAU.SOLUONG,
                                        GIADABAN = i.sANPHAMTHEOYEUCAU.TONGSOTIENSANXUAT,
                                        NGAYTAO = DateTime.Now,
                                        MASANPHAMTHEOYEUCAU = i.sANPHAMTHEOYEUCAU.MASANPHAMTHEOYEUCAU,
                                    };
                                    db.DOANHTHUs.Add(dt);

                                    TIENDOSANXUAT td1 = new TIENDOSANXUAT
                                    {
                                       GIAIDOANSANXUAT = "Lập kế hoạch và thiết kế",
                                       NGAYSANXUAT = DateTime.Now,
                                        MANHANVIENSANXUAT = 9,
                                        MOTA = "Đơn hàng đã được thanh toán hãy làm việc năng nỗ nào !!!",
                                        SOLUONG = i.sANPHAMTHEOYEUCAU.SOLUONG,
                                        MASANPHAMTHEOYEUCAU = i.sANPHAMTHEOYEUCAU.MASANPHAMTHEOYEUCAU,
                                        NGHIEMTHU = "Phân công",
                                    };
                                    db.TIENDOSANXUATs.Add(td1);
                                    db.SaveChanges();
                                    Debug.WriteLine("tạo tiến độ thành công");
                                    
                                }
                                guimai(daylasptyc, hoadon, kh);
                                return View();
                            }
                            else
                            {
                                var dsgiohang = (from hd in db.HOADONs
                                                 join cthd in db.CHITIETHOADONs on hd.MAHOADON equals cthd.MAHOADON
                                                 join sp in db.SANPHAMs on cthd.MASANPHAM equals sp.MASANPHAM
                                                 where hd.MAHOADON == mahd
                                                 select new chitiethoadon_sanpham
                                                 {
                                                     sCHITIETHOADON = cthd,
                                                     sSANPHAM = sp,
                                                 }).ToList();

                                foreach (var i in dsgiohang)
                                {
                                    DOANHTHU dt = new DOANHTHU
                                    {
                                        SOLUONG = i.sCHITIETHOADON.SOLUONG,
                                        GIADABAN = i.sCHITIETHOADON.TONGTIEN,
                                        NGAYTAO = DateTime.Now,
                                        MACHITIETHOADON = i.sCHITIETHOADON.MACHITIETHOADON,
                                    };
                                    db.DOANHTHUs.Add(dt);
                                    db.SaveChanges();

                                    db.VANCHUYENs.Add(new VANCHUYEN
                                    {
                                        NGAYBATDAUGUI = DateTime.Now.AddDays(1),
                                        NGAYDUKIENDUOCGIAO = DateTime.Now.AddDays(5),
                                        PHUONGTHUCVANCHUYEN = "Vận chuyển nhanh",
                                        MOTA = "Đơn hàng đã được thanh toán, hãy làm việc năng nỗ nào!",
                                        CHIPHIVANCHUYEN = 0,
                                        MACHITIETHOADON = i.sCHITIETHOADON.MACHITIETHOADON,
                                        TRANGTHAIVANCHUYEN = "Đơn hàng đã tạo",
                                        MANHANVIENVANCHUYEN = 1
                                    });
                                    db.SaveChanges();
                                }
                                guimai(dsgiohang, hoadon, kh);
                                return View();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Lỗi: Không thể chuyển đổi amount_in thành kiểu double.");
                    }
                }
                Debug.WriteLine("Không tìm thấy hoá đơn thanh toán");
                return RedirectToAction("hoadoncuauser");
            }
        }


        public ActionResult quenmatkhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult quenmatkhau(KHACHHANG kh)
        {
            var khtim = db.KHACHHANGs.FirstOrDefault(m => m.EMAIL == kh.EMAIL);

            if(khtim != null)
            {
                string code = new Random().Next(1000, 9999).ToString();
                Session["codexacnhan"] = code;
                Session["thoigian_code"] = DateTime.Now.AddMinutes(10);
                Session["emailquenmatkhau"] = khtim.EMAIL;

                guigmaildoimk(code);
                return RedirectToAction("nhapcodequenmk");
            }
            else
            {
                ViewBag.thongbao = "Đã gửi mật khẩu mới vào email của bạn !!!";
            }
            return RedirectToAction("quenmatkhau");
        }

        [HttpGet]
        public ActionResult nhapcodequenmk()
        {
            return View();
        }

        [HttpPost]
        public ActionResult nhapcodequenmk(string userinputcode)
        {
            try
            {
                if (Session["codexacnhan"] != null && Session["thoigian_code"] != null)
                {
                    string code = Session["codexacnhan"].ToString();
                    DateTime thoigian_code = (DateTime)Session["thoigian_code"];

                    if (DateTime.Now <= thoigian_code)
                    {
                        if (userinputcode == code)
                        {
                            Session.Remove("code");
                            Session.Remove("thoigian_code");
                            return RedirectToAction("setmatkhau", "User");
                        }
                        else
                        {
                            ViewBag.thongbao = "Mã xác nhận không đúng.";
                            return View();
                        }
                    }
                    else
                    {
                        Session.Remove("code");
                        Session.Remove("thoigian_code");
                        ViewBag.thongbao = "Mã xác nhận đã hết hạn. Vui lòng đăng ký lại.";
                        return RedirectToAction("quenmatkhau");
                    }

                }
                else
                {
                    ViewBag.thongbao = "Không tìm thấy mã xác nhận.";
                    return RedirectToAction("quenmatkhau");
                }
            }
            catch(Exception ex)
            {
                ViewBag.thongbao = "Lỗi đã xảy ra";
                return RedirectToAction("Trangchu", "Trangchu");
            }
        }

        [HttpGet]
        public ActionResult setmatkhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult setmatkhau(string newpassword, string replaynewpassword)
        {
            var email = Session["emailquenmatkhau"] as string;
            KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(u => u.EMAIL == email);
            if (newpassword.Length > 10 || replaynewpassword.Length > 10)
            {
                TempData["resetmatkhau"] = false;
                ViewBag.ThongBao = "Mật khẩu không được dài quá 10 ký tự.";
                return View();
            }

            if (newpassword != replaynewpassword)
            {
                TempData["resetmatkhau"] = false;
                ViewBag.ThongBao = "Mật khẩu mới và xác nhận không khớp.";
                return View();
            }

            bool coChuHoa = newpassword.Any(char.IsUpper);
            bool coChuThuong = newpassword.Any(char.IsLower);
            bool coSo = newpassword.Any(char.IsDigit);
            bool coKyTuDacBiet = newpassword.Any(c => !char.IsLetterOrDigit(c));

            if (!(coChuHoa && coChuThuong && coSo && coKyTuDacBiet))
            {
                TempData["resetmatkhau"] = false;
                ViewBag.ThongBao = "Mật khẩu mới phải có chữ hoa, chữ thường, số và ký tự đặc biệt.";
                return View();
            }

            newpassword = HashPasswordWithSHA512(newpassword);
            kh.MATKHAU = newpassword;
            Session.Remove("emailquenmatkhau");
            db.SaveChanges();

            TempData["resetmatkhau"] = true;
            TempData["ThongBao"] = "Cập nhập mật khẩu thành công";
            return RedirectToAction("dangnhap", "User");
        }

        public ActionResult huydonhang(int id)
        {
            bool canCancel = false;
            var hd = db.HOADONs.FirstOrDefault(m => m.MAHOADON == id);

            if (hd != null)
            {
                if (hd.PHUONGTHUCTHANHTOAN.Equals("Thanh toán khi nhận hàng", StringComparison.OrdinalIgnoreCase))
                {
                    // Lấy danh sách chi tiết hóa đơn
                    var cthd = hd.CHITIETHOADONs.ToList();
                    if (cthd != null)
                    {
                        canCancel = true;
                        foreach (var item in cthd)
                        {
                            var vc = item.VANCHUYENs.FirstOrDefault();
                            if (vc != null &&
                                (vc.TRANGTHAIVANCHUYEN == "Đơn hàng đã tạo" || vc.TRANGTHAIVANCHUYEN == "Đang chuẩn bị"))
                            {
                                db.VANCHUYENs.Remove(vc);
                                db.CHITIETHOADONs.Remove(item);
                            }
                            else
                            {
                                canCancel = false;
                                break;
                            }
                        }

                        if (canCancel && !hd.CHITIETHOADONs.Any())
                        {
                            db.HOADONs.Remove(hd);
                            db.SaveChanges();
                        }
                    }
                }
                else if (hd.PHUONGTHUCTHANHTOAN.Equals("Thanh toán online", StringComparison.OrdinalIgnoreCase))
                {
                    var cthd = hd.CHITIETHOADONs.ToList();
                    if (cthd != null && cthd.Any())
                    {
                        foreach (var item in cthd)
                        {
                            var vc = db.VANCHUYENs.FirstOrDefault(n => n.MACHITIETHOADON == item.MACHITIETHOADON);
                            var dt = db.DOANHTHUs.FirstOrDefault(n => n.MACHITIETHOADON == item.MACHITIETHOADON);
                            var sp = db.SANPHAMs.FirstOrDefault(n => n.MASANPHAM == item.MASANPHAM);

                            if(vc == null && dt == null)
                            {
                                sp.SOLUONG += item.SOLUONG;
                                db.Entry(sp).State = EntityState.Modified;
                                db.CHITIETHOADONs.Remove(item);
                                canCancel = true;

                            }
                            else if (vc != null && (vc.TRANGTHAIVANCHUYEN == "Đơn hàng đã tạo" || vc.TRANGTHAIVANCHUYEN == "Đang chuẩn bị"))
                            {
                                db.DOANHTHUs.Remove(dt);
                                db.VANCHUYENs.Remove(vc);
                                sp.SOLUONG += item.SOLUONG;
                                db.Entry(sp).State = EntityState.Modified;
                                db.CHITIETHOADONs.Remove(item);
                                canCancel = true;
                            }
                            else
                            {
                                canCancel = false;
                                break;
                            }
                        }

                        if (canCancel)
                        {
                            db.HOADONs.Remove(hd);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var sptyc = db.SANPHAMTHEOYEUCAUs.FirstOrDefault(m => m.MAHOADON == id);
                        if (sptyc != null)
                        {
                            var dt = db.DOANHTHUs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == sptyc.MASANPHAMTHEOYEUCAU);
                            var tdsx = db.TIENDOSANXUATs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == sptyc.MASANPHAMTHEOYEUCAU);

                            if (tdsx != null && tdsx.GIAIDOANSANXUAT == "Lập kế hoạch và thiết kế")
                            {
                                db.TIENDOSANXUATs.Remove(tdsx);
                                db.DOANHTHUs.Remove(dt);
                                db.SANPHAMTHEOYEUCAUs.Remove(sptyc);
                                db.HOADONs.Remove(hd);
                                db.SaveChanges();
                                canCancel = true;
                            }
                            else
                            {
                                canCancel = false;
                            }
                        }
                    }
                }
            }

            // Cập nhật trạng thái hủy đơn cho ViewBag
            ViewBag.huydon = canCancel;

            // Trả về View
            return View();
        }

        public ActionResult gioithieu()
        {
            return View();
        }

        public ActionResult nutthemsoluongsanphamnologin(int id, int loai)
        {
            if(loai == 0)
            {
                var listsanpham = Session["sanphamnologin"] as List<giohangsession>;
                var sanpham = listsanpham.FirstOrDefault(sp => sp.ProductId == id);
                var spcantim = db.SANPHAMs.SingleOrDefault(m => m.MASANPHAM == id);
                if(spcantim.SOLUONG > sanpham.Quantity)
                {
                    sanpham.Quantity++;
                    sanpham.tongtien = sanpham.Quantity*sanpham.Price;
                }
                Session["sanphamnologin"] = listsanpham;
                return RedirectToAction("showgiohang", "User");
            }
            else
            {
                var listsanpham = Session["sanphamnologin"] as List<giohangsession>;
                var sanpham = listsanpham.FirstOrDefault(sp => sp.ProductId == id);

                if(sanpham.Quantity > 1)
                {
                    sanpham.Quantity--;
                    sanpham.tongtien =  sanpham.Price*sanpham.Quantity;
                }
                Session["sanphamnologin"] = listsanpham;
                return RedirectToAction("showgiohang", "User");
            }
        }

        public ActionResult nutthemsoluongsanphamlogin(int id, int loai)
        {
            if(loai == 0)
            {
                var giohang = db.GIOHANGs.SingleOrDefault(m => m.MAGIOHANG == id);
                var sp = db.SANPHAMs.SingleOrDefault(m => m.MASANPHAM == giohang.MASANPHAM);
                if(sp.SOLUONG > giohang.SOLUONG)
                {
                    giohang.SOLUONG++;
                    giohang.TONGTIEN = giohang.SOLUONG * double.Parse(sp.GIASANPHAM);
                }
                db.Entry(giohang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("showgiohang", "User");
            }
            else
            {
                var giohang = db.GIOHANGs.SingleOrDefault(m => m.MAGIOHANG == id);
                var sp = db.SANPHAMs.SingleOrDefault(m => m.MASANPHAM == giohang.MASANPHAM);
                if (giohang.SOLUONG > 1)
                {
                    giohang.SOLUONG--;
                    giohang.TONGTIEN = giohang.SOLUONG * double.Parse(sp.GIASANPHAM);
                }
                db.Entry(giohang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("showgiohang", "User");
            }
        }

        public ActionResult LoginWithGoogle()
        {
            string authorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth" +
                                      $"?client_id={clientId}" +
                                      $"&redirect_uri={redirectUri}" +
                                      $"&response_type=code" +
                                      $"&scope=openid%20email%20profile" +
                                      $"&access_type=offline";

            return Redirect(authorizationUrl);
        }
        public async System.Threading.Tasks.Task<ActionResult> GoogleLoginCallback(string code)
        {
            if (code == null)
                return RedirectToAction("Index", "Home");

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
                var responseString = await response.Content.ReadAsStringAsync();

                dynamic token = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
                string idToken = token.id_token;

                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

                string email = payload.Email;
                string name = payload.Name;

                var checkkh = db.KHACHHANGs.FirstOrDefault(u => u.EMAIL == email);
                if(checkkh == null)
                {
                    var sessionToken = Guid.NewGuid().ToString();
                    var cookie = new HttpCookie("SessionToken", sessionToken)
                    {
                        Expires = DateTime.Now.AddHours(1)
                    };
                    Response.Cookies.Add(cookie);
                    KHACHHANG newkh = new KHACHHANG
                    {
                        HOVATEN = name,
                        EMAIL = email,
                        TENDANGNHAP = email,
                        MATKHAU = "",
                        SessionToken = sessionToken,
                    };

                    db.KHACHHANGs.Add(newkh);
                    db.SaveChanges();

                    try
                    {
                        LICHSUDANGNHAP newls = new LICHSUDANGNHAP
                        {
                            IDTAIKHOAN = newkh.MAKHACHHANG,
                            THOIGIAN = DateTime.Now,
                            DIA_CHI_IP = layip(Request),
                            TRINH_DUYET = Request.UserAgent,
                            THIET_BI = laythietbi(Request.UserAgent)
                        };
                        Debug.WriteLine(newls.THIET_BI);
                        db.LICHSUDANGNHAPs.Add(newls);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Lỗi khi lưu lịch sử đăng nhập: " + ex.Message);
                    }
                    Session["idtaikhoan"] = newkh.MAKHACHHANG;
                    Session["taikhoan"] = newkh;
                    Session["UserRole"] = "KHACHHANG";
                }
                else
                {
                    try
                    {
                        LICHSUDANGNHAP newls = new LICHSUDANGNHAP
                        {
                            IDTAIKHOAN = checkkh.MAKHACHHANG,
                            THOIGIAN = DateTime.Now,
                            DIA_CHI_IP = layip(Request),
                            TRINH_DUYET = Request.UserAgent,
                            THIET_BI = laythietbi(Request.UserAgent)
                        };
                        Debug.WriteLine(newls.THIET_BI);
                        db.LICHSUDANGNHAPs.Add(newls);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Lỗi khi lưu lịch sử đăng nhập: " + ex.Message);
                    }
                    var sessionToken = Guid.NewGuid().ToString();
                    var cookie = new HttpCookie("SessionToken", sessionToken)
                    {
                        Expires = DateTime.Now.AddHours(1)
                    };
                    Response.Cookies.Add(cookie);
                    checkkh.SessionToken = sessionToken;
                    checkkh.BIKHOA = false;
                    checkkh.SOLANDANGNHAPSAI = 0;
                    db.SaveChanges();
                    Session["idtaikhoan"] = checkkh.MAKHACHHANG;
                    Session["taikhoan"] = checkkh;
                    Session["UserRole"] = "KHACHHANG";
                }
                TempData["dangnhapthanhcong"] = true;
                return RedirectToAction("Trangchu", "TrangChu");
            }
        }

        public ActionResult doimatkhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult doimatkhau(string matkhaucu, string matkhaumoi, string xacnhan)
        {
            if (Session["taikhoan"] == null)
            {
                TempData["doimatkhau"] = false;
                ViewBag.ThongBao = "Bạn chưa đăng nhập!";
                return RedirectToAction("DangNhap", "User");
            }

            KHACHHANG khSession = (KHACHHANG)Session["taikhoan"];
            var kh = db.KHACHHANGs.SingleOrDefault(k => k.MAKHACHHANG == khSession.MAKHACHHANG);

            if (matkhaucu.Length > 10 || matkhaumoi.Length > 10 || xacnhan.Length > 10)
            {
                TempData["doimatkhau"] = false;
                ViewBag.ThongBao = "Mật khẩu không được dài quá 10 ký tự.";
                return View();
            }

            matkhaucu = HashPasswordWithSHA512(matkhaucu);

            if (kh.MATKHAU != matkhaucu)
            {
                TempData["doimatkhau"] = false;
                ViewBag.ThongBao = "Mật khẩu cũ không đúng.";
                return View();
            }

            if (matkhaumoi != xacnhan)
            {
                TempData["doimatkhau"] = false;
                ViewBag.ThongBao = "Mật khẩu mới và xác nhận không khớp.";
                return View();
            }

            bool coChuHoa = matkhaumoi.Any(char.IsUpper);
            bool coChuThuong = matkhaumoi.Any(char.IsLower);
            bool coSo = matkhaumoi.Any(char.IsDigit);
            bool coKyTuDacBiet = matkhaumoi.Any(c => !char.IsLetterOrDigit(c));

            if (!(coChuHoa && coChuThuong && coSo && coKyTuDacBiet))
            {
                TempData["doimatkhau"] = false;
                ViewBag.ThongBao = "Mật khẩu mới phải có chữ hoa, chữ thường, số và ký tự đặc biệt.";
                return View();
            }
            string code = new Random().Next(1000, 9999).ToString();
            Session["codexacnhan"] = code;
            Session["thoigian_code"] = DateTime.Now.AddMinutes(10);
            Session["matkhaumoi"] = HashPasswordWithSHA512(matkhaumoi);
            guigmaildoimk(code);

            return RedirectToAction("nhapcodedoimatkhau");
        }

        [HttpGet]
        public ActionResult nhapcodedoimatkhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult nhapcodedoimatkhau(string userinputcode)
        {
            if (Session["codexacnhan"] != null && Session["thoigian_code"] != null)
            {
                string code = Session["codexacnhan"].ToString();
                DateTime thoigian_code = (DateTime)Session["thoigian_code"];

                if (DateTime.Now <= thoigian_code)
                {
                    if (userinputcode == code)
                    {
                        int? iduser = Session["idtaikhoan"] as int?;
                        KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(u => u.MAKHACHHANG == iduser);
                        string newpasssword = Session["matkhaumoi"]?.ToString();
                        kh.MATKHAU = newpasssword;
                        db.SaveChanges();
                        Session.Remove("code");
                        Session.Remove("thoigian_code");

                        ViewBag.ThongBao = "Đổi mật khẩu thành công";
                        TempData["doimatkhau"] = true;
                        return RedirectToAction("doimatkhau", "User");
                    }
                    else
                    {
                        ViewBag.thongbao = "Mã xác nhận không đúng.";
                        TempData["doimatkhau"] = false;
                        return View();
                    }
                }
                else
                {
                    Session.Remove("code");
                    Session.Remove("thoigian_code");
                    ViewBag.thongbao = "Mã xác nhận đã hết hạn. Vui lòng đăng ký lại.";
                    TempData["doimatkhau"] = false;
                    return RedirectToAction("doimatkhau");
                }

            }
            else
            {
                TempData["doimatkhau"] = false;
                ViewBag.thongbao = "Không tìm thấy mã xác nhận.";
                return RedirectToAction("doimatkhau");
            }
        }

        public void guigmaildoimk(string code)
        {
            string username = "2224802010208@student.tdmu.edu.vn";
            string password = "dbmd wckm qknh zazz";
            KHACHHANG kh = Session["taikhoan"] as KHACHHANG;
            string emailquenmk = Session["emailquenmatkhau"] as string;

            try
            {
                string body = $@"
                    <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #eee; border-radius: 10px; background-color: #f9f9f9; max-width: 500px; margin: auto;'>
                        <h2 style='color: #2d8cf0;'>Xác nhận đăng ký tài khoản</h2>
                        <p>Xin chào</p>
                        <p>Bạn vừa đổi mật khẩu tài khoản tại hệ thống <strong>WEBDOGO</strong>.</p>
                        <p>Vui lòng sử dụng mã xác nhận sau để hoàn tất quá trình đăng ký:</p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <span style='font-size: 28px; font-weight: bold; color: #ffffff; background-color: #2d8cf0; padding: 10px 20px; border-radius: 6px;'>{code}</span>
                        </div>
                        <p>Mã xác nhận có hiệu lực trong vòng <strong>10 phút</strong>.</p>
                        <p>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.</p>
                        <p style='margin-top: 30px;'>Trân trọng,<br><strong>Đội ngũ WEBDOGO</strong></p>
                    </div>";

                // Cấu hình email
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(username);
                if(emailquenmk != null)
                {
                    mail.To.Add(emailquenmk);
                }
                else
                {
                    mail.To.Add(kh.EMAIL);
                }
                mail.Subject = "Xác nhận đổi mật khẩu ( WEB ĐỒ GỖ XỊN XÒ )";
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(username, password);
                smtp.EnableSsl = true;

                // Gửi email
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gửi email thất bại: " + ex.Message);
            }
        }

        public string layip(HttpRequestBase request)
        {
            string ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ip))
            {
                string[] addresses = ip.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            ip = request.ServerVariables["REMOTE_ADDR"];

            if (ip == "::1")
            {
                return "127.0.0.1";
            }

            return ip;
        }

        public ActionResult lichsudangnhap()
        {
            int? idkh = Session["idtaikhoan"] as int?;
            if(idkh != null)
            {
                KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(id => id.MAKHACHHANG == idkh);
                var lsdn = db.LICHSUDANGNHAPs.Where(x => x.IDTAIKHOAN == idkh).OrderByDescending(x => x.THOIGIAN).ToList();
                return View(lsdn);
            }

            return View();
        }

        [HttpGet]
        public ActionResult khongcoquyentruycap()
        {
            return View();
        }        
    }
}