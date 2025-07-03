using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDOGO.Models
{
    public class giohangsession
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public string anhsanpham {  get; set; }
        public double tongtien {  get; set; }
    }
}