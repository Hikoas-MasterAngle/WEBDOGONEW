using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDOGO.Models
{
    public class xemthongtin_choquanly
    {
        public WEBDOGO.Models.NHANVIENSANXUAT nHANVIENSANXUATs { get; set; }
        public WEBDOGO.Models.NHANVIENTAICHINH nHANVIENTAICHINHs { get; set; }
        public WEBDOGO.Models.NHANVIENVANCHUYEN nHANVIENVANCHUYENs { get; set; }
        public WEBDOGO.Models.KHACHHANG kHACHHANGs { get; set; }

        public WEBDOGO.Models.SANPHAM sanphamlist { get; set; }

        public WEBDOGO.Models.LIENHE lIENHEs { get; set; }
    }
}