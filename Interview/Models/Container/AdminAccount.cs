//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Interview.Models.Container
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminAccount
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string AdminName { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
