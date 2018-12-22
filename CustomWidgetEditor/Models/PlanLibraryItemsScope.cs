namespace CustomWidgetEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanLibraryItemsScope")]
    public partial class PlanLibraryItemsScope
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlanLibraryItemsScope()
        {
            PlanLibraryItems = new HashSet<PlanLibraryItem>();
        }

        [Key]
        public int PlanLibraryItemScopeID { get; set; }

        [StringLength(50)]
        public string Scope { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanLibraryItem> PlanLibraryItems { get; set; }
    }
}
