namespace CustomWidgetEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PlanLibraryItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlanLibraryItem()
        {
            PlanLibraryItemCustomItemScopes = new HashSet<PlanLibraryItemCustomItemScope>();
        }

        [Key]
        public int PlanLibCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemType { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemTypeID { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemTitle { get; set; }

        public string ItemDescription { get; set; }

        public int DefaultThreshold { get; set; }

        [StringLength(255)]
        public string ItemTypeNote { get; set; }

        [StringLength(50)]
        public string MetricValueNote { get; set; }

        public int? PlanLibraryItemScopeID { get; set; }

        public string CustomItemDefinition { get; set; }

        public string CustomItemScoreFunction { get; set; }

        [StringLength(50)]
        public string FormID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanLibraryItemCustomItemScope> PlanLibraryItemCustomItemScopes { get; set; }

        public virtual PlanLibraryItemsScope PlanLibraryItemsScope { get; set; }
    }
}
