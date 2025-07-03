using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBDOGO.Models;
using PagedList;

namespace WEBDOGO.Controllers
{
    public class NhanvientaichinhController : BaseController
    {
        SQLWEB2Entities1 db = new SQLWEB2Entities1();
        // GET: Nhanvientaichinh
        public ActionResult showgiaodienquanly(int? page)
        {
            int size = 12;
            int ipagenumber = (page ?? 1);

            var danhsachsanpham = (from hd in db.HOADONs
                                   join kh in db.KHACHHANGs on hd.MAKHACHHANG equals kh.MAKHACHHANG
                                   orderby hd.NGAYLAP descending
                                   select new chitiethoadon_sanpham
                                   {
                                       hOADON = hd,
                                       kHACHHANG = kh
                                   }).ToList();

            ViewBag.thongkedonhang = db.HOADONs.Count();
            ViewBag.thongkesanpham = db.SANPHAMs.Count();
            ViewBag.thongkekhachhang = db.KHACHHANGs.Count();

            var doanhThuData = db.DOANHTHUs
                .Select(d => new
                {
                    NgayTao = d.NGAYTAO,
                    SoLuong = d.SOLUONG,
                    TongGia = d.GIADABAN
                })
                .ToList();



            ViewBag.DoanhThuData = doanhThuData;

            return View(danhsachsanpham.ToPagedList(ipagenumber, size));


        }

        public ActionResult showchitiethoadon(int mahd)
        {
            ViewBag.chitiethoadon = (from hd in db.HOADONs
                                     join kh in db.KHACHHANGs on hd.MAKHACHHANG equals kh.MAKHACHHANG
                                     join cthd in db.CHITIETHOADONs on hd.MAHOADON equals cthd.MAHOADON
                                     join sp in db.SANPHAMs on cthd.MASANPHAM equals sp.MASANPHAM
                                     where hd.MAHOADON == mahd
                                     select new chitiethoadon_sanpham
                                     {
                                         hOADON = hd,
                                         kHACHHANG = kh,
                                         sCHITIETHOADON = cthd,
                                         sSANPHAM = sp,
                                     }).ToList();

            ViewBag.chitiethoadonsptheoyecau = (from hd in db.HOADONs
                                                join sptyc in db.SANPHAMTHEOYEUCAUs on hd.MAHOADON equals sptyc.MAHOADON
                                                join kh in db.KHACHHANGs on sptyc.MAKHACHHANG equals kh.MAKHACHHANG
                                                where hd.MAHOADON == mahd
                                                select new chitiethoadon_sanpham
                                                {
                                                    hOADON = hd,
                                                    sANPHAMTHEOYEUCAU = sptyc,
                                                    kHACHHANG = kh,
                                                }).ToList();

            ViewBag.kh = (from hd in db.HOADONs
                          join kh in db.KHACHHANGs on hd.MAKHACHHANG equals kh.MAKHACHHANG
                          where hd.MAHOADON == mahd
                          select new chitiethoadon_sanpham
                          {
                              tenkhachhang = kh.HOVATEN,
                          }).FirstOrDefault();

            ViewBag.hd = db.HOADONs.FirstOrDefault(m => m.MAHOADON == mahd);
            return View();
        }

        public ActionResult chinhsuachitiethoadon(int mahd)
        {
            var danhsachsanpham = (from hd in db.HOADONs
                                   join kh in db.KHACHHANGs on hd.MAKHACHHANG equals kh.MAKHACHHANG
                                   where hd.MAHOADON == mahd
                                   select new chitiethoadon_sanpham
                                   {
                                       hOADON = hd,
                                       kHACHHANG = kh
                                   }).FirstOrDefault();

            return View(danhsachsanpham);
        }

        public void chinhsuathanhtoan(chitiethoadon_sanpham ds)
        {
            var danhsachchitiethoadon = db.CHITIETHOADONs.Where(m => m.MAHOADON == ds.hOADON.MAHOADON).ToList();

            if (danhsachchitiethoadon.Any())
            {
                foreach (var item in danhsachchitiethoadon)
                {
                    DateTime ngaybatdaugui = DateTime.Now;
                    DateTime ngaybatdaugui1 = ngaybatdaugui.AddDays(1);

                    DateTime ngaybatdukienduocgiao = DateTime.Now;
                    DateTime ngaybatdukienduocgiao1 = ngaybatdukienduocgiao.AddDays(5);

                    VANCHUYEN newvc = new VANCHUYEN
                    {
                        NGAYBATDAUGUI = ngaybatdaugui1,
                        NGAYDUKIENDUOCGIAO = ngaybatdukienduocgiao1,
                        PHUONGTHUCVANCHUYEN = "Vận chuyển nhanh",
                        CHIPHIVANCHUYEN = 0,
                        MOTA = "Ngày thanh toán hóa đơn là " + DateTime.Now.ToString() + " hãy vận chuyển đơn hàng đến khách hàng sớm nhất có thể",
                        TRANGTHAIVANCHUYEN = "Đơn hàng đã tạo",
                        MACHITIETHOADON = item.MACHITIETHOADON
                    };
                    db.VANCHUYENs.Add(newvc);

                    var chitiethoadon = db.CHITIETHOADONs.Where(m => m.MAHOADON == ds.hOADON.MAHOADON).ToList();

                    foreach (var itemcon1 in chitiethoadon)
                    {
                        var doanhthucon = db.DOANHTHUs.FirstOrDefault(m => m.MACHITIETHOADON == itemcon1.MACHITIETHOADON);
                        if (doanhthucon != null)
                        {

                        }
                        else
                        {
                            DOANHTHU newdt = new DOANHTHU();
                            newdt.GIADABAN = item.TONGTIEN;
                            newdt.NGAYTAO = DateTime.Now;
                            newdt.MACHITIETHOADON = item.MACHITIETHOADON;
                            newdt.SOLUONG = item.SOLUONG;

                            db.DOANHTHUs.Add(newdt);
                            db.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                var sanphamtheoyc = db.SANPHAMTHEOYEUCAUs.FirstOrDefault(m => m.MAHOADON == ds.hOADON.MAHOADON);

                TIENDOSANXUAT newtd = new TIENDOSANXUAT();
                var ngaysanxuatnhap = DateTime.Now.AddDays(5);
                newtd.NGAYSANXUAT = ngaysanxuatnhap;

                switch (sanphamtheoyc.LOAISANPHAM)
                {
                    case "Sản phẩm đặc biệt":
                        var ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(45);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Quà tặng":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(15);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Gỗ liền tấm":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(30);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Sập gỗ, phản gỗ liền tấm":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(50);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Lục bình gỗ":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(20);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Bình hoa gỗ":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(15);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Tượng gỗ phong thủy":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(35);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Linh vật phong thủy":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(30);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Tranh phong thủy":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(25);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Đồ thờ":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(30);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Gỗ mỹ nghệ khác":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(30);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Nội thất phòng khách":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(45);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                    case "Nội thất phòng ngủ":
                        ngaydukienhoanthanh = ngaysanxuatnhap.AddDays(40);
                        newtd.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                        break;
                }

                var tiendo = db.TIENDOSANXUATs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == sanphamtheoyc.MASANPHAMTHEOYEUCAU);
                if (tiendo != null)
                {

                }
                else
                {
                    newtd.GIAIDOANSANXUAT = "Lập kế hoạch và thiết kế";
                    newtd.MANHANVIENSANXUAT = 1;
                    newtd.SOLUONG = sanphamtheoyc.SOLUONG;
                    newtd.MASANPHAMTHEOYEUCAU = sanphamtheoyc.MASANPHAMTHEOYEUCAU;
                    newtd.NGHIEMTHU = "Phân công";

                    db.TIENDOSANXUATs.Add(newtd);
                    db.SaveChanges();
                }

                var doanhthu = db.DOANHTHUs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == sanphamtheoyc.MASANPHAMTHEOYEUCAU);
                if (doanhthu != null)
                {

                }
                else
                {
                    DOANHTHU newdt = new DOANHTHU();
                    newdt.GIADABAN = sanphamtheoyc.TONGSOTIENSANXUAT;
                    newdt.NGAYTAO = DateTime.Now;
                    newdt.MASANPHAMTHEOYEUCAU = sanphamtheoyc.MASANPHAMTHEOYEUCAU;
                    newdt.SOLUONG = sanphamtheoyc.SOLUONG;

                    db.DOANHTHUs.Add(newdt);
                    db.SaveChanges();
                }
            }
        }

        [HttpPost]
        public ActionResult chinhsuachitiethoadon(chitiethoadon_sanpham ds)
        {
            if (ds.hOADON == null)
            {
                return Content("Không tìm thấy hóa đơn");
            }
            else
            {
                if (ds.hOADON.TRANGTHAITHANHTOAN.ToLower() == "đã thanh toán" && ds.hOADON.NGAYLAP != null)
                {

                    db.Entry(ds.hOADON).State = EntityState.Modified;
                    chinhsuathanhtoan(ds);

                }
                else if (ds.hOADON.TRANGTHAITHANHTOAN.ToLower() == "đã thanh toán" && ds.hOADON.NGAYLAP == null)
                {
                    var hd = db.HOADONs.FirstOrDefault(m => m.MAHOADON == ds.hOADON.MAHOADON);
                    if (hd != null)
                    {
                        ds.hOADON.NGAYLAP = hd.NGAYLAP;
                        db.Entry(hd).State = EntityState.Detached;
                    }
                    db.Entry(ds.hOADON).State = EntityState.Modified;
                    chinhsuathanhtoan(ds);

                }
                else
                {
                    db.Entry(ds.hOADON).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("showchitiethoadon", new { mahd = ds.hOADON.MAHOADON });
            }
        }

        public ActionResult timkiemhd()
        {
            return PartialView("_Partialtimkiemhoadon");
        }

        [HttpPost]
        public ActionResult timkiemhoadon(HOADON hd)
        {

            ViewBag.thongkedonhang = db.HOADONs.Count();
            ViewBag.thongkesanpham = db.SANPHAMs.Count();
            ViewBag.thongkekhachhang = db.KHACHHANGs.Count();

            var doanhThuData = db.DOANHTHUs
                .Select(d => new
                {
                    NgayTao = d.NGAYTAO,
                    SoLuong = d.SOLUONG,
                    TongGia = d.GIADABAN
                })
                .ToList();



            ViewBag.DoanhThuData = doanhThuData;
            var hoadon = db.HOADONs.FirstOrDefault(m => m.SOHOADON == hd.SOHOADON);

            ViewBag.kh = db.HOADONs.FirstOrDefault(m => m.SOHOADON == hd.SOHOADON)?.KHACHHANG;
            return View(hoadon);
        }

        [HttpPost]
        public ActionResult TimKiemDoanhThu(int? SelectedNgay, int? SelectedThang, int? SelectedNam)
        {
            var query = db.DOANHTHUs.AsQueryable();

            if (SelectedNgay.HasValue && SelectedThang.HasValue && SelectedNam.HasValue)
            {
                query = query.Where(d => d.NGAYTAO.Value.Day == SelectedNgay.Value &&
                                         d.NGAYTAO.Value.Month == SelectedThang.Value &&
                                         d.NGAYTAO.Value.Year == SelectedNam.Value);
            }
            else if (SelectedNgay.HasValue && SelectedThang.HasValue)
            {
                query = query.Where(d => d.NGAYTAO.Value.Day == SelectedNgay.Value &&
                                         d.NGAYTAO.Value.Month == SelectedThang.Value);
            }
            else if (SelectedThang.HasValue && SelectedNam.HasValue)
            {
                query = query.Where(d => d.NGAYTAO.Value.Day == SelectedThang.Value &&
                                         d.NGAYTAO.Value.Month == SelectedNam.Value);
            }
            else if (SelectedNam.HasValue)
            {
                query = query.Where(d => d.NGAYTAO.Value.Year == SelectedNam.Value);
            }

            double tongDoanhThu = query.Sum(d => d.SOLUONG * d.GIADABAN) ?? 0;
            TempData["tongDoanhThu"] = tongDoanhThu;

            double tongsosanphamdaban = query.Sum(d => d.SOLUONG) ?? 0;
            TempData["tongsosanphamdaban"] = tongsosanphamdaban;

            return RedirectToAction("showgiaodienquanly");
        }
    }
}