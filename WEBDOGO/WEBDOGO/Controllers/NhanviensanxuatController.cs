using Microsoft.Ajax.Utilities;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBDOGO.Models;

namespace WEBDOGO.Controllers
{
    public class NhanviensanxuatController : BaseController
    {
        SQLWEB2Entities1 db = new SQLWEB2Entities1();
        // GET: Nhanviensanxuat
        public ActionResult showyeucausanxuat()
        {
            try
            {
                var query = from sp in db.SANPHAMTHEOYEUCAUs
                            join kh in db.KHACHHANGs on sp.MAKHACHHANG equals kh.MAKHACHHANG
                            select new SanPhamTheoYeuCauKhachHangViewModel
                            {
                                SanPham = sp,
                                TenKhachHang = kh.HOVATEN,
                                idkhachhang = kh.MAKHACHHANG,
                            };
                var sanPhamKhachHangList = query.ToList();

                ViewBag.tiendo = (from td in db.TIENDOSANXUATs
                                  join sptyc in db.SANPHAMTHEOYEUCAUs on td.MASANPHAMTHEOYEUCAU equals sptyc.MASANPHAMTHEOYEUCAU
                                  join kh in db.KHACHHANGs on sptyc.MAKHACHHANG equals kh.MAKHACHHANG
                                  join nvsx in db.NHANVIENSANXUATs on td.MANHANVIENSANXUAT equals nvsx.MANHANVIENSANXUAT
                                  where td.GIAIDOANSANXUAT != "Hoàn thiện"
                                  select new quanlttiendo
                                  {
                                      sTIENDOSANXUAT = td,
                                      tenkhachhang = kh.HOVATEN,
                                      tennhanviensanxuat = nvsx.TENNHANVIENSANXUAT,
                                  }).ToList();

                ViewBag.tiendohoanthien = (from td in db.TIENDOSANXUATs
                                           join sptyc in db.SANPHAMTHEOYEUCAUs on td.MASANPHAMTHEOYEUCAU equals sptyc.MASANPHAMTHEOYEUCAU
                                           join kh in db.KHACHHANGs on sptyc.MAKHACHHANG equals kh.MAKHACHHANG
                                           join nvsx in db.NHANVIENSANXUATs on td.MANHANVIENSANXUAT equals nvsx.MANHANVIENSANXUAT
                                           where td.GIAIDOANSANXUAT == "Hoàn thiện"
                                           select new quanlttiendo
                                           {
                                               sTIENDOSANXUAT = td,
                                               tenkhachhang = kh.HOVATEN,
                                               tennhanviensanxuat = nvsx.TENNHANVIENSANXUAT,
                                           }).ToList();

                var nhanviensx = (WEBDOGO.Models.NHANVIENSANXUAT)Session["taikhoan"];

                ViewBag.sanxuatchuahoanthanhcuatoi = (from nvsx in db.NHANVIENSANXUATs
                                                      join tdsx in db.TIENDOSANXUATs on nvsx.MANHANVIENSANXUAT equals tdsx.MANHANVIENSANXUAT
                                                      where nvsx.MANHANVIENSANXUAT == nhanviensx.MANHANVIENSANXUAT
                                                      && tdsx.GIAIDOANSANXUAT != "Hoàn thiện"
                                                      select new quanlttiendo
                                                      {
                                                          sTIENDOSANXUAT = tdsx,
                                                      }).ToList();

                ViewBag.sanxuatdahoanthanhcuatoi = (from nvsx in db.NHANVIENSANXUATs
                                                    join tdsx in db.TIENDOSANXUATs on nvsx.MANHANVIENSANXUAT equals tdsx.MANHANVIENSANXUAT
                                                    where nvsx.MANHANVIENSANXUAT == nhanviensx.MANHANVIENSANXUAT
                                                    && tdsx.GIAIDOANSANXUAT == "Hoàn thiện"
                                                    select new quanlttiendo
                                                    {
                                                        sTIENDOSANXUAT = tdsx,
                                                    }).ToList();

                ViewBag.SanPhamKhachHangList = sanPhamKhachHangList;

                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Trangchu", "Trangchu");
            }
        }

        public ActionResult showchitietyeucausanxuat(int masansp, String tenkh, int idkh)
        {

            if (masansp != null && tenkh != null && idkh != null)
            {
                SANPHAMTHEOYEUCAU spcantim1 = db.SANPHAMTHEOYEUCAUs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == masansp);

                var sptheoyeucau = new SanPhamTheoYeuCauKhachHangViewModel
                {
                    SanPham = spcantim1,
                    TenKhachHang = tenkh,
                    idkhachhang = idkh,
                };

                return View(sptheoyeucau);
            }
            else
            {
                return Content("Không thể tìm thấy sản phẩm theo yêu cầu");
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

        [HttpPost]
        public ActionResult luuthongtinsanphamtheoyeucau(SanPhamTheoYeuCauKhachHangViewModel nHANVIENSANXUAT)
        {
            if (nHANVIENSANXUAT.SanPham != null)
            {
                TIENDOSANXUAT td = db.TIENDOSANXUATs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == nHANVIENSANXUAT.SanPham.MASANPHAMTHEOYEUCAU);
                if (td != null)
                {
                    td.SOLUONG = nHANVIENSANXUAT.SanPham.SOLUONG;
                    td.MASANPHAMTHEOYEUCAU = nHANVIENSANXUAT.SanPham.MASANPHAMTHEOYEUCAU;
                    db.Entry(td).State = EntityState.Modified;
                    db.SaveChanges();

                    HOADON hd1 = db.HOADONs.FirstOrDefault(n => n.MAHOADON == nHANVIENSANXUAT.SanPham.MAHOADON);
                    if (hd1 != null)
                    {
                        hd1.TONGTIEN = nHANVIENSANXUAT.SanPham.TONGSOTIENSANXUAT;
                        db.Entry(hd1).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        return Content("Không tìm thấy hóa đơn.");
                    }
                    nHANVIENSANXUAT.SanPham.MAKHACHHANG = nHANVIENSANXUAT.idkhachhang;
                    db.Entry(nHANVIENSANXUAT.SanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("showyeucausanxuat");
                }
                else if (nHANVIENSANXUAT.SanPham.TRANGTHAIDUYET.ToLower() == "duyệt" && nHANVIENSANXUAT.SanPham.TONGSOTIENSANXUAT != null)
                {
                    NHANVIENSANXUAT nv = (WEBDOGO.Models.NHANVIENSANXUAT)Session["taikhoan"];

                    nHANVIENSANXUAT.SanPham.MAKHACHHANG = nHANVIENSANXUAT.idkhachhang;
                    db.Entry(nHANVIENSANXUAT.SanPham).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("showyeucausanxuat");
                }
                else if(nHANVIENSANXUAT.SanPham.TRANGTHAIDUYET.ToLower() == "không thể làm")
                {
                    nHANVIENSANXUAT.SanPham.MAKHACHHANG = nHANVIENSANXUAT.idkhachhang;
                    db.Entry(nHANVIENSANXUAT.SanPham).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("showyeucausanxuat");
                }
                else
                {
                    return RedirectToAction("showyeucausanxuat");
                }
            }
            else
            {
                return Content("Không tìm thấy");
            }
        }

        public ActionResult showchitiettiendo(int matd, string tenkh, string tennhanvien)
        {
            if (matd != null && tenkh != null && tennhanvien != null)
            {
                TIENDOSANXUAT tiendosx = db.TIENDOSANXUATs.FirstOrDefault(m => m.MATIENDOSANXUAT == matd);
                var tiendo = new quanlttiendo
                {
                    sTIENDOSANXUAT = tiendosx,
                    tenkhachhang = tenkh,
                    tennhanviensanxuat = tennhanvien,

                };
                ViewBag.nhanviensx = db.NHANVIENSANXUATs.ToList();
                return View(tiendo);
            }
            else
            {
                return Content("Không tìm thấy tiến độ sản phẩm");
            }
        }

        [HttpPost]
        public ActionResult luutiendosanxuat(quanlttiendo qltd)
        {
            if (qltd.sTIENDOSANXUAT == null)
            {
                return Content("Không thể tìm thấy tiến độ sản xuất");
            }
            else
            {
                if (qltd.sTIENDOSANXUAT.NGHIEMTHU == "Phê duyệt")
                {
                    qltd.sTIENDOSANXUAT.NGHIEMTHU = "Đang chờ";
                    switch (qltd.sTIENDOSANXUAT.GIAIDOANSANXUAT)
                    {
                        case "Lập kế hoạch và thiết kế":
                            qltd.sTIENDOSANXUAT.GIAIDOANSANXUAT = "Xử lý nguyên liệu";
                            break;

                        case "Xử lý nguyên liệu":
                            qltd.sTIENDOSANXUAT.GIAIDOANSANXUAT = "Gia công sản phẩm";
                            break;

                        case "Gia công sản phẩm":
                            qltd.sTIENDOSANXUAT.GIAIDOANSANXUAT = "Xử lý bề mặt";
                            break;

                        case "Xử lý bề mặt":
                            qltd.sTIENDOSANXUAT.GIAIDOANSANXUAT = "Lắp ráp";
                            break;

                        case "Lắp ráp":
                            qltd.sTIENDOSANXUAT.GIAIDOANSANXUAT = "Hoàn thiện";
                            break;
                    }

                    if (qltd.sTIENDOSANXUAT.GIAIDOANSANXUAT == "Hoàn thiện")
                    {
                        qltd.sTIENDOSANXUAT.NGHIEMTHU = "Phê duyệt"; 
                        qltd.sTIENDOSANXUAT.NGAYHOANTHANHTHUCTE = DateTime.Now;
                        var vc = db.VANCHUYENs.FirstOrDefault(m => m.MASANPHAMTHEOYEUCAU == qltd.sTIENDOSANXUAT.MASANPHAMTHEOYEUCAU);
                        if (vc != null)
                        {
                            return RedirectToAction("showyeucausanxuat");
                        }
                        else
                        {
                            VANCHUYEN newvc = new VANCHUYEN();
                            newvc.NGAYBATDAUGUI = DateTime.Now;
                            var ngaybatdaugui = DateTime.Now;
                            newvc.NGAYDUKIENDUOCGIAO = ngaybatdaugui.AddDays(5);
                            newvc.PHUONGTHUCVANCHUYEN = "Vận chuyển nhanh";
                            newvc.CHIPHIVANCHUYEN = 0;
                            newvc.MOTA = "Sản phẩm đã được hoàn thành vào " + DateTime.Now.ToString() + " hãy vận chuyển đơn hàng đến khách hàng sớm nhất có thể";
                            newvc.TRANGTHAIVANCHUYEN = "Đơn hàng đã tạo";
                            newvc.MANHANVIENVANCHUYEN = 1;
                            newvc.MASANPHAMTHEOYEUCAU = qltd.sTIENDOSANXUAT.MASANPHAMTHEOYEUCAU;

                            db.VANCHUYENs.Add(newvc);
                            db.SaveChanges();

                        }
                    }
                }
                else if (qltd.sTIENDOSANXUAT.NGHIEMTHU == "Phân công")
                {
                    qltd.sTIENDOSANXUAT.NGHIEMTHU = "Đang chờ";
                }


                if (qltd.sTIENDOSANXUAT.NGAYDUKIENHOANTHANH != null && qltd.sTIENDOSANXUAT.NGAYHOANTHANHTHUCTE != null)
                {

                }
                else if (qltd.sTIENDOSANXUAT.NGAYDUKIENHOANTHANH == null && qltd.sTIENDOSANXUAT.NGAYHOANTHANHTHUCTE != null)
                {
                    var ngaydukienhoanthanh = db.TIENDOSANXUATs
                        .Where(m => m.MATIENDOSANXUAT == qltd.sTIENDOSANXUAT.MATIENDOSANXUAT)
                        .Select(m => m.NGAYDUKIENHOANTHANH)
                        .FirstOrDefault();
                    qltd.sTIENDOSANXUAT.NGAYHOANTHANHTHUCTE = ngaydukienhoanthanh;
                }
                else if (qltd.sTIENDOSANXUAT.NGAYDUKIENHOANTHANH != null && qltd.sTIENDOSANXUAT.NGAYHOANTHANHTHUCTE == null)
                {

                    var ngayhoanthanhthucte = db.TIENDOSANXUATs
                        .Where(m => m.MATIENDOSANXUAT == qltd.sTIENDOSANXUAT.MATIENDOSANXUAT)
                        .Select(m => m.NGAYHOANTHANHTHUCTE)
                        .FirstOrDefault();
                    qltd.sTIENDOSANXUAT.NGAYHOANTHANHTHUCTE = ngayhoanthanhthucte;
                }
                else
                {
                    var ngayhoanthanhthucte = db.TIENDOSANXUATs
                        .Where(m => m.MATIENDOSANXUAT == qltd.sTIENDOSANXUAT.MATIENDOSANXUAT)
                        .Select(m => m.NGAYHOANTHANHTHUCTE)
                        .FirstOrDefault();

                    var ngaydukienhoanthanh = db.TIENDOSANXUATs
                        .Where(m => m.MATIENDOSANXUAT == qltd.sTIENDOSANXUAT.MATIENDOSANXUAT)
                        .Select(m => m.NGAYDUKIENHOANTHANH)
                        .FirstOrDefault();
                    qltd.sTIENDOSANXUAT.NGAYDUKIENHOANTHANH = ngaydukienhoanthanh;
                    qltd.sTIENDOSANXUAT.NGAYHOANTHANHTHUCTE = ngayhoanthanhthucte;
                }

                db.Entry(qltd.sTIENDOSANXUAT).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("showyeucausanxuat");
            }
        }

        public ActionResult tiendocuatoi(int matiendo)
        {
            var tiendo = db.TIENDOSANXUATs.FirstOrDefault(m => m.MATIENDOSANXUAT == matiendo);
            return View(tiendo);
        }

        [HttpPost]
        public ActionResult tiendocuatoi(TIENDOSANXUAT tIENDOSANXUAT, HttpPostedFileBase HinhAnh)
        {

            if (tIENDOSANXUAT != null)
            {
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/IMG/sanpham"), fileName);
                    HinhAnh.SaveAs(path); // Lưu ảnh vào thư mục

                    tIENDOSANXUAT.ANHMINHHOA = "../IMG/sanpham/" + fileName;
                }


                tIENDOSANXUAT.NGHIEMTHU = "Chờ xác nhận";

                db.Entry(tIENDOSANXUAT).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("showyeucausanxuat");
            }
            else
            {
                return Content("Không lấy được dữ liệu của sản phẩm theo yêu cầu");
            }
        }

        public ActionResult pheduyet(int matiendosanxuat, string tenkh1, string tennv1)
        {
            TIENDOSANXUAT tdsx = db.TIENDOSANXUATs.FirstOrDefault(m => m.MATIENDOSANXUAT == matiendosanxuat);
            if(tdsx == null)
            {
                return Content("Không tìm thấy tiến độ sản xuất");
            }
            else
            {
                tdsx.NGHIEMTHU = "Phê duyệt";
                db.Entry(tdsx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("showchitiettiendo", new { matd  = matiendosanxuat, tenkh = tenkh1, tennhanvien = tennv1});
            }
        }

        public ActionResult khongpheduyet(int matiendosanxuat, string tenkh1, string tennv1)
        {
            TIENDOSANXUAT tdsx = db.TIENDOSANXUATs.FirstOrDefault(m => m.MATIENDOSANXUAT == matiendosanxuat);
            if (tdsx == null)
            {
                return Content("Không tìm thấy tiến độ sản xuất");
            }
            else
            {
                tdsx.NGHIEMTHU = "Đang chờ";
                db.Entry(tdsx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("showchitiettiendo", new { matd = matiendosanxuat, tenkh = tenkh1, tennhanvien = tennv1 });
            }
        }


    }
}