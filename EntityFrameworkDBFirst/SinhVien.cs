//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFrameworkDBFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class SinhVien
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public Nullable<int> MaLop { get; set; }
    
        public virtual Lop Lop { get; set; }
    }
}
