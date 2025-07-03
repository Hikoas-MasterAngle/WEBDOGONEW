using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDOGO.Models
{
    public class danhsachvanchuyen
    {
        public string Tinh { get; set; }
        public List<quanlyvanchuyen> DonHangList { get; set; }
    }
}