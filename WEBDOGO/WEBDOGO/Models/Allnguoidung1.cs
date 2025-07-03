using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDOGO.Models
{
    public class Allnguoidung1
    {
        public IEnumerable<WEBDOGO.Models.NHANVIENSANXUAT> nHANVIENSANXUATs { get; set; }
        public IEnumerable<WEBDOGO.Models.NHANVIENTAICHINH> nHANVIENTAICHINHs { get; set; }
        public IEnumerable<WEBDOGO.Models.NHANVIENVANCHUYEN> nHANVIENVANCHUYENs { get; set; }
        public IEnumerable<WEBDOGO.Models.KHACHHANG> kHACHHANGs { get; set; }
    }
}