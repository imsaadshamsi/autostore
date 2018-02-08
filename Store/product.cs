//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Store
{
    using System;
    using System.Collections.Generic;
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.items = new HashSet<item>();
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public long product_Id { get; set; }
        public int productCategory_Id { get; set; }
        public string name { get; set; }
        public int model_Id { get; set; }
        public Nullable<decimal> SpecialPrice { get; set; }
        public Nullable<decimal> OldPrice { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<long> StockQuantity { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public string OEM_num { get; set; }
        public string ST_num { get; set; }
        public string Part_num { get; set; }
        public Nullable<long> StockShopQTY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item> items { get; set; }
        public virtual model model { get; set; }
        public virtual Product_Category Product_Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
