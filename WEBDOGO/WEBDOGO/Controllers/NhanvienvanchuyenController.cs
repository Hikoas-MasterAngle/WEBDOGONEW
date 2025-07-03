using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBDOGO.Models;

namespace WEBDOGO.Controllers
{
    public class NhanvienvanchuyenController : BaseController
    {
        SQLWEB2Entities1 db = new SQLWEB2Entities1 ();
        // GET: Nhanvienvanchuyen
        public ActionResult showgiaodienquanly()
        {
            var nv = (WEBDOGO.Models.NHANVIENVANCHUYEN)Session["taikhoan"];


            // lấy danh sách vận chuyển cho quản lý
            var sanphamcosan = (from kh in db.KHACHHANGs
                                    join hd in db.HOADONs on kh.MAKHACHHANG equals hd.MAKHACHHANG
                                    join cthd in db.CHITIETHOADONs on hd.MAHOADON equals cthd.MAHOADON
                                    join vc in db.VANCHUYENs on cthd.MACHITIETHOADON equals vc.MACHITIETHOADON
                                    join sp in db.SANPHAMs on cthd.MASANPHAM equals sp.MASANPHAM
                                    where vc.TRANGTHAIVANCHUYEN != "Hoàn thành"
                                select new quanlyvanchuyen
                                    {
                                        kHACHHANG = kh,
                                        cHITIETHOADON = cthd,
                                        hOADON = hd,
                                        vANCHUYEN = vc,
                                        sANPHAM = sp,
                                    }).ToList();

            var groupedByTinh = new Dictionary<string, List<quanlyvanchuyen>>();

            for (int i = 0; i < sanphamcosan.Count; i++)
            {
                var item = sanphamcosan[i];
                var tinh = item.kHACHHANG.TINH;

                if (!groupedByTinh.ContainsKey(tinh))
                {
                    groupedByTinh[tinh] = new List<quanlyvanchuyen>();
                }

                groupedByTinh[tinh].Add(item);
            }


            var sanphamtheoyeucau = (from sptyc in db.SANPHAMTHEOYEUCAUs
                                             join hd in db.HOADONs on sptyc.MAHOADON equals hd.MAHOADON
                                             join kh in db.KHACHHANGs on sptyc.MAKHACHHANG equals kh.MAKHACHHANG
                                             join vc in db.VANCHUYENs on sptyc.MASANPHAMTHEOYEUCAU equals vc.MASANPHAMTHEOYEUCAU
                                             where vc.TRANGTHAIVANCHUYEN != "Hoàn thành"
                                     select new quanlyvanchuyen
                                             {
                                                 kHACHHANG = kh,
                                                 sANPHAMTHEOYEUCAU = sptyc,
                                                 hOADON = hd,
                                                 vANCHUYEN = vc,
                                             }).ToList();

            for (int i = 0; i < sanphamtheoyeucau.Count; i++)
            {
                var item = sanphamtheoyeucau[i];
                var tinh = item.kHACHHANG.TINH;

                if (!groupedByTinh.ContainsKey(tinh))
                {
                    groupedByTinh[tinh] = new List<quanlyvanchuyen>();
                }

                groupedByTinh[tinh].Add(item);
            }

            ViewBag.GroupedOrders = groupedByTinh;

            
            return View();

        }

        public ActionResult showvanchuyencuatoi()
        {
            var nv = (WEBDOGO.Models.NHANVIENVANCHUYEN)Session["taikhoan"];

            var sanphamconcuatoi = (from kh in db.KHACHHANGs
                                    join hd in db.HOADONs on kh.MAKHACHHANG equals hd.MAKHACHHANG
                                    join cthd in db.CHITIETHOADONs on hd.MAHOADON equals cthd.MAHOADON
                                    join vc in db.VANCHUYENs on cthd.MACHITIETHOADON equals vc.MACHITIETHOADON
                                    join sp in db.SANPHAMs on cthd.MASANPHAM equals sp.MASANPHAM
                                    where vc.TRANGTHAIVANCHUYEN != "Hoàn thành" && vc.MANHANVIENVANCHUYEN == nv.MANHANVIENVANCHUYEN
                                    select new quanlyvanchuyen
                                    {
                                        kHACHHANG = kh,
                                        cHITIETHOADON = cthd,
                                        hOADON = hd,
                                        vANCHUYEN = vc,
                                        sANPHAM = sp,
                                    }).ToList();

            var groupedByTinhcuatoi = new Dictionary<string, List<quanlyvanchuyen>>();

            for (int i = 0; i < sanphamconcuatoi.Count; i++)
            {
                var item = sanphamconcuatoi[i];
                var tinh = item.kHACHHANG.TINH;

                if (!groupedByTinhcuatoi.ContainsKey(tinh))
                {
                    groupedByTinhcuatoi[tinh] = new List<quanlyvanchuyen>();
                }

                groupedByTinhcuatoi[tinh].Add(item);
            }

            var sanphamtheoyeucaucuatoi = (from sptyc in db.SANPHAMTHEOYEUCAUs
                                           join hd in db.HOADONs on sptyc.MAHOADON equals hd.MAHOADON
                                           join kh in db.KHACHHANGs on sptyc.MAKHACHHANG equals kh.MAKHACHHANG
                                           join vc in db.VANCHUYENs on sptyc.MASANPHAMTHEOYEUCAU equals vc.MASANPHAMTHEOYEUCAU
                                           where vc.TRANGTHAIVANCHUYEN != "Hoàn thành" && vc.MANHANVIENVANCHUYEN == nv.MANHANVIENVANCHUYEN
                                           select new quanlyvanchuyen
                                           {
                                               kHACHHANG = kh,
                                               sANPHAMTHEOYEUCAU = sptyc,
                                               hOADON = hd,
                                               vANCHUYEN = vc,
                                           }).ToList();

            for (int i = 0; i < sanphamtheoyeucaucuatoi.Count; i++)
            {
                var item = sanphamtheoyeucaucuatoi[i];
                var tinh = item.kHACHHANG.TINH;

                if (!groupedByTinhcuatoi.ContainsKey(tinh))
                {
                    groupedByTinhcuatoi[tinh] = new List<quanlyvanchuyen>();
                }

                groupedByTinhcuatoi[tinh].Add(item);
            }

            ViewBag.groupedByTinhcuatoi1 = groupedByTinhcuatoi;
            return View();
        }

        public ActionResult chinhsuavanchuyenchitietsanpham(int mavc)
        {
            VANCHUYEN vccantim = db.VANCHUYENs.FirstOrDefault(m => m.MAVANCHUYEN == mavc);

            ViewBag.nhanvienvc = db.NHANVIENVANCHUYENs.ToList();
            return View(vccantim);
        }


        [HttpPost]
        public ActionResult chinhsuavanchuyenchitietsanpham(VANCHUYEN vanchuyen1)
        {
            var vanChuyenCu = db.VANCHUYENs.FirstOrDefault(m => m.MAVANCHUYEN == vanchuyen1.MAVANCHUYEN);

            if (vanChuyenCu == null)
            {
                return HttpNotFound();
            }

            if (vanchuyen1.NGAYBATDAUGUI != DateTime.MinValue)
            {
                vanChuyenCu.NGAYBATDAUGUI = vanchuyen1.NGAYBATDAUGUI;
            }

            if (vanchuyen1.NGAYDUKIENDUOCGIAO != null)
            {
                vanChuyenCu.NGAYDUKIENDUOCGIAO = vanchuyen1.NGAYDUKIENDUOCGIAO;
            }
            else
            {
                vanChuyenCu.NGAYDUKIENDUOCGIAO = vanChuyenCu.NGAYBATDAUGUI.AddDays(3);
            }
                vanChuyenCu.NGAYGIAOTHUCTE = vanchuyen1.NGAYGIAOTHUCTE;
            // Cập nhật các trường khác
            vanChuyenCu.CHIPHIVANCHUYEN = vanchuyen1.CHIPHIVANCHUYEN;
            vanChuyenCu.MOTA = vanchuyen1.MOTA;
            vanChuyenCu.PHUONGTHUCVANCHUYEN = vanchuyen1.PHUONGTHUCVANCHUYEN;
            vanChuyenCu.TRANGTHAIVANCHUYEN = vanchuyen1.TRANGTHAIVANCHUYEN;
            vanChuyenCu.MANHANVIENVANCHUYEN = vanchuyen1.MANHANVIENVANCHUYEN;

            db.Entry(vanChuyenCu).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("showgiaodienquanly");
        }

        public ActionResult vanchuyencuatoi(int mavc)
        {
            VANCHUYEN vc = db.VANCHUYENs.FirstOrDefault(m => m.MAVANCHUYEN == mavc);
            return View(vc);
        }

        [HttpPost]
        public ActionResult vanchuyencuatoi(VANCHUYEN vc)
        {
            var vanchuyen = db.VANCHUYENs.FirstOrDefault(m => m.MAVANCHUYEN == vc.MAVANCHUYEN);
            if (vanchuyen == null) {
                return Content("Không tìm thấy vận chuyển");
            }
            else
            {
                switch(vc.TRANGTHAIVANCHUYEN)
                {
                    case "Đơn hàng đã tạo":
                        vanchuyen.TRANGTHAIVANCHUYEN = "Đang chuẩn bị";
                        break;
                    case "Đang chuẩn bị":
                        vanchuyen.TRANGTHAIVANCHUYEN = "Chờ vận chuyển";
                         break;
                    case "Chờ vận chuyển":
                        vanchuyen.TRANGTHAIVANCHUYEN = "Đang giao hàng";
                        break;
                    case "Đang giao hàng":
                        vanchuyen.TRANGTHAIVANCHUYEN = "Đang giao đến";
                        break;
                    case "Giao hàng thất bại":
                        vanchuyen.TRANGTHAIVANCHUYEN = "Hoàn hàng";
                        break;
                }

                db.Entry(vanchuyen).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("showvanchuyencuatoi");
        }

        public ActionResult vanchuyenthanhcong(int vc)
        {
            VANCHUYEN vanchuyen = db.VANCHUYENs.FirstOrDefault(m => m.MAVANCHUYEN == vc);
            if (vanchuyen == null)
            {
                return Content("Không tìm thấy vận chuyển");
            }
            else
            {
                vanchuyen.TRANGTHAIVANCHUYEN = "Hoàn thành";
                vanchuyen.NGAYGIAOTHUCTE = DateTime.Now;

                // Kiểm tra `cthd` không null
                var cthd = db.CHITIETHOADONs.FirstOrDefault(m => m.MACHITIETHOADON == vanchuyen.MACHITIETHOADON);
                if (cthd != null)
                {
                    // Kiểm tra `hd` không null
                    var hd = db.HOADONs.FirstOrDefault(m => m.MAHOADON == cthd.MAHOADON);
                    hd.TRANGTHAITHANHTOAN = "đã thanh toán";
                    if (hd != null && hd.TRANGTHAITHANHTOAN == "Thanh toán khi nhận hàng")
                    {
                        DOANHTHU dt = new DOANHTHU();
                        dt.SOLUONG = cthd.SOLUONG;
                        dt.GIADABAN = cthd.TONGTIEN;
                        dt.NGAYTAO = DateTime.Now;
                        dt.MACHITIETHOADON = cthd.MACHITIETHOADON;

                        db.DOANHTHUs.Add(dt);
                        db.SaveChanges();
                    }
                    db.Entry(hd).State = EntityState.Modified;
                }
                else
                {
                    var sptyc = db.SANPHAMTHEOYEUCAUs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == vanchuyen.MASANPHAMTHEOYEUCAU);
                }


                db.Entry(vanchuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("vanchuyencuatoi", new { mavc = vanchuyen.MAVANCHUYEN });
            }
        }

        public ActionResult vanchuyenthatbai(int vc)
        {
            VANCHUYEN vanchuyen = db.VANCHUYENs.FirstOrDefault(m => m.MAVANCHUYEN == vc);
            if (vanchuyen == null)
            {
                return Content("Không tìm thấy vận chuyển");
            }
            else
            {
                vanchuyen.TRANGTHAIVANCHUYEN = "Giao hàng thất bại";

                var sanphamcantim = (from vchuyen in db.VANCHUYENs
                                    join cthd in db.CHITIETHOADONs on vchuyen.MACHITIETHOADON equals cthd.MACHITIETHOADON
                                    where vchuyen.MAVANCHUYEN == vc
                                    select new chitiethoadon_sanpham
                                    {
                                        sCHITIETHOADON = cthd,
                                    }).ToList();

                foreach(var item in sanphamcantim)
                {
                    var sanpham = db.SANPHAMs.FirstOrDefault(m => m.MASANPHAM == item.sCHITIETHOADON.MASANPHAM);
                    sanpham.SOLUONG += item.sCHITIETHOADON.SOLUONG;
                    db.Entry(sanpham).State = EntityState.Modified;
                }
                
                db.Entry(vanchuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("vanchuyencuatoi", new { mavc = vanchuyen.MAVANCHUYEN });
            }
        }

        public ActionResult timkiemvc()
        {
            return PartialView("_Partialtimkiemvc");
        }

        public ActionResult timkiemvanchuyen(HOADON hOADON)
        {
            var hoadon = db.HOADONs.FirstOrDefault(m => m.SOHOADON == hOADON.SOHOADON);
            ViewBag.vanchuyensanphamthuong = (from vc in db.VANCHUYENs
                                join cthd in db.CHITIETHOADONs on vc.MACHITIETHOADON equals cthd.MACHITIETHOADON
                                join hd in db.HOADONs on cthd.MAHOADON equals hd.MAHOADON
                                join kh in db.KHACHHANGs on hd.MAKHACHHANG equals kh.MAKHACHHANG
                                join sp in db.SANPHAMs on cthd.MASANPHAM equals sp.MASANPHAM
                                where hd.SOHOADON == hOADON.SOHOADON 
                                select new showvanchuyen
                                {
                                    sVANCHUYEN = vc,
                                    cHITIETHOADON = cthd,
                                    kHACHHANG = kh,
                                    sHOADON = hd,
                                    sSANPHAM = sp,
                                }).ToList();

            ViewBag.vanchuyensanphamtyc = (from vc in db.VANCHUYENs
                                 join sptyc in db.SANPHAMTHEOYEUCAUs on vc.MASANPHAMTHEOYEUCAU equals sptyc.MASANPHAMTHEOYEUCAU
                                 join hd in db.HOADONs on sptyc.MAHOADON equals hd.MAHOADON
                                 join kh in db.KHACHHANGs on hd.MAKHACHHANG equals kh.MAKHACHHANG
                                 where hd.SOHOADON == hOADON.SOHOADON
                                 select new showvanchuyen
                                 {
                                     sVANCHUYEN = vc,
                                     sANPHAMTHEOYEUCAU = sptyc,
                                     kHACHHANG = kh,
                                     sHOADON = hd,
                                 }).ToList();
            return View();
        }
    }
}