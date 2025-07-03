using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDOGO.Models
{
    public class Allnguoidung
    {
        public WEBDOGO.Models.NHANVIENSANXUAT nHANVIENSANXUATs { get; set; }
        public WEBDOGO.Models.NHANVIENTAICHINH nHANVIENTAICHINHs { get; set; }
        public WEBDOGO.Models.NHANVIENVANCHUYEN nHANVIENVANCHUYENs { get; set; }
        public WEBDOGO.Models.KHACHHANG kHACHHANGs { get; set; }
    }
}