namespace CT_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Destription { get; set; }

        public string ImageMain { get; set; }

        public string ImageSub1 { get; set; }

        public string ImageSub2 { get; set; }

        public string ImageSub3 { get; set; }

        public string ImageSub4 { get; set; }

        public string ROM { get; set; }

        public string RAM { get; set; }

        public string ManHinh { get; set; }

        public string HeDieuHanh { get; set; }

        [StringLength(50)]
        public string CamTruoc { get; set; }

        [StringLength(50)]
        public string CamSau { get; set; }

        public string Chip { get; set; }

        public string SIM { get; set; }

        [StringLength(50)]
        public string Pin { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
