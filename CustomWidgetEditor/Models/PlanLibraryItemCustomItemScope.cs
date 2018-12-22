namespace CustomWidgetEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanLibraryItemCustomItemScope")]
    public partial class PlanLibraryItemCustomItemScope
    {
        [Key]
        [Column(Order = 0)]
        public int CustomItemScopeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanLibCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScopeId { get; set; }

        public virtual PlanLibraryItem PlanLibraryItem { get; set; }
    }
}
