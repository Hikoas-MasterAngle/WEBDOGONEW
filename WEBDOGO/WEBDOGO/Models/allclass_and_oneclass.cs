using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBDOGO.Models;

namespace WEBDOGO.Models
{
    public class allclass_and_oneclass
    {
        public WEBDOGO.Models.SANPHAM onesanpham {  get; set; }
        public IEnumerable<WEBDOGO.Models.SANPHAM> sanphamlist { get; set; }
    }

    
}