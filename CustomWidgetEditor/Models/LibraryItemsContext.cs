namespace CustomWidgetEditor.Models
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class LibraryItemsContext : DbContext
  {
    public LibraryItemsContext()
        : base( "name=LibraryItemsContext" )
    {
    }

    public virtual DbSet<PlanLibraryItem> PlanLibraryItems { get; set; }

    protected override void OnModelCreating( DbModelBuilder modelBuilder )
    {
    }
  }
}
