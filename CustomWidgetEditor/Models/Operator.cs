namespace CustomWidgetEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Operator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Operator()
        {
            Sites = new HashSet<Site>();
        }

        public int OperatorID { get; set; }

        [Required]
        [StringLength(100)]
        public string OperatorName { get; set; }

        [Required]
        [StringLength(2)]
        public string StateAbbr { get; set; }

        [StringLength(255)]
        public string OperatorUID { get; set; }

        public int? NSchStateID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Site> Sites { get; set; }
    }
}
