//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CVGS
{
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        public string username { get; set; }
        public decimal orderId { get; set; }
        public decimal gameId { get; set; }
        public System.DateTime orderDate { get; set; }
        public Nullable<System.DateTime> shipDate { get; set; }
    
        public virtual game game { get; set; }
        public virtual user user { get; set; }
    }
}