using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDOGO.Models
{
    public class quanly_nonguoi
    {
        public IEnumerable<WEBDOGO.Models.SANPHAM> sanphamlist { get; set; }

        public IEnumerable<WEBDOGO.Models.LIENHE> lIENHEs { get; set; }
    }
}