using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDOGO.Models
{
    public class SanPhamTheoYeuCauKhachHangViewModel
    {
        public SANPHAMTHEOYEUCAU SanPham { get; set; }
        public string TenKhachHang { get; set; }

        public int idkhachhang { get; set; }

        public VANCHUYEN VANCHUYENs { get; set; }

        public TIENDOSANXUAT TIENDOSANXUATs { get; set; }
    }
}