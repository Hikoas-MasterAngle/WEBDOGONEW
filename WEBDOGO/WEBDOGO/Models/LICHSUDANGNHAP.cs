//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEBDOGO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LICHSUDANGNHAP
    {
        public int IDLICHSUDANGNHAP { get; set; }
        public Nullable<int> IDTAIKHOAN { get; set; }
        public Nullable<System.DateTime> THOIGIAN { get; set; }
        public string DIA_CHI_IP { get; set; }
        public string TRINH_DUYET { get; set; }
        public string THIET_BI { get; set; }
    
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
