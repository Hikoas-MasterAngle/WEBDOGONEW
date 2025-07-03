using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDOGO.Models
{
    public class chitiethoadon_sanpham
    {
        public CHITIETHOADON sCHITIETHOADON { get; set; }

        public VANCHUYEN vANCHUYEN { get; set; }

        public NHANVIENVANCHUYEN hANVIENVANCHUYEN { get; set; }

        public HOADON hOADON { get; set; }
        public SANPHAM sSANPHAM { get; set; }

        public KHACHHANG kHACHHANG { get; set; }

        public string tenkhachhang { get; set; }
        public string diachi {  get; set; }

        public SANPHAMTHEOYEUCAU sANPHAMTHEOYEUCAU { get; set; }
    }
}