namespace CustomWidgetEditor.Models
{
  using System.Data.Entity;
  using System.Configuration;
  public partial class LibraryItemsContext : DbContext
  {
    public LibraryItemsContext( string stateAbbr, string urlTest )
    {
      string dbName;
      var connString = ConfigurationManager.ConnectionStrings["LibraryItemsContext"].ConnectionString;
      if ( urlTest.ToLower().Contains( "test" ) )
      {
        dbName = "CisMain";
      }
      else
      {
        if ( urlTest.ToLower().Contains( "localhost" ) )
        {
          dbName = "CisMain_Dev";
        }
        else
        {
          dbName = stateAbbr == "IC" ? "CisMain" : "CisMain_" + stateAbbr;
        }
      }
      Database.Connection.ConnectionString = connString.Replace( "_dbName_", dbName );
    }

    public virtual DbSet<Operator> Operators { get; set; }
    public virtual DbSet<PlanLibraryItem> PlanLibraryItems { get; set; }
    public virtual DbSet<PlanLibraryItemsScope> PlanLibraryItemsScopes { get; set; }
    public virtual DbSet<Site> Sites { get; set; }
    public virtual DbSet<PlanLibraryItemCustomItemScope> PlanLibraryItemCustomItemScopes { get; set; }

    protected override void OnModelCreating( DbModelBuilder modelBuilder )
    {
      modelBuilder.Entity<Operator>()
          .Property( e => e.OperatorName )
          .IsUnicode( false );

      modelBuilder.Entity<Operator>()
          .Property( e => e.StateAbbr )
          .IsFixedLength()
          .IsUnicode( false );

      modelBuilder.Entity<Operator>()
          .Property( e => e.OperatorUID )
          .IsUnicode( false );

      modelBuilder.Entity<Operator>()
          .HasMany( e => e.Sites )
          .WithRequired( e => e.Operator )
          .WillCascadeOnDelete( false );

      modelBuilder.Entity<PlanLibraryItem>()
          .HasMany( e => e.PlanLibraryItemCustomItemScopes )
          .WithRequired( e => e.PlanLibraryItem )
          .WillCascadeOnDelete( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SiteName )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.ContactName )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.ContactEMail )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SiteAddress1 )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SiteAddress2 )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SiteCity )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SiteCounty )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SiteState )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SiteZip )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SitePhoneNumber )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.ReferenceID )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.PetSiteID )
          .IsFixedLength()
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.CISConnSchoolID )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.CISConnOrganizations )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.Memo )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.CoursePlanLink )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.CoursePlanLinkDesc )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.UEPAuthCode )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.SsiGradYears )
          .IsUnicode( false );

      modelBuilder.Entity<Site>()
          .Property( e => e.PREPAuthCode )
          .IsUnicode( false );
    }
  }
}
