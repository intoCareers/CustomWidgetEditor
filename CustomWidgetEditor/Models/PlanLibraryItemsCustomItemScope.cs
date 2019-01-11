namespace CustomWidgetEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanLibraryItemsCustomItemScope")]
    public partial class PlanLibraryItemsCustomItemScope
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanLibCode { get; set; }

        public int? SiteId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OperatorId { get; set; }

        public virtual PlanLibraryItem PlanLibraryItem { get; set; }

        public virtual PlanLibraryItem PlanLibraryItem1 { get; set; }
    }
}
