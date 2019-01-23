
namespace CustomWidgetEditor.Models
{
  using System;
  using System.Configuration;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class LibraryItemsContext : DbContext
  {
    public LibraryItemsContext( string stateAbbr, string urlTest )
    {
      string dbName;
      var connString = ConfigurationManager.ConnectionStrings["LibraryItemsContext"].ConnectionString;
      if ( urlTest.ToLower().Contains( "test" ) || urlTest.ToLower().Contains( "temp" ) )
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
          dbName = string.IsNullOrEmpty(stateAbbr)? "CisMain" : "CisMain_" + stateAbbr;
        }
      }
      Database.Connection.ConnectionString = connString.Replace( "_dbName_", dbName );
    }

    public virtual DbSet<Operator> Operators { get; set; }
    public virtual DbSet<PlanLibraryItem> PlanLibraryItems { get; set; }
    public virtual DbSet<Site> Sites { get; set; }
    public virtual DbSet<PlanLibraryItemsCustomFormScope> PlanLibraryItemsCustomFormScopes { get; set; }

    protected override void OnModelCreating( DbModelBuilder modelBuilder )
    {
    
    }
  }
}
