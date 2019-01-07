using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomWidgetEditor.Models
{
  public static class ItemsManager
  {
    private const string _itemType = "FORM";
    private const string _itemTypeId = "CustomModule";
    private const int _itemScopeId = 3;
    private const string _metricValueNote = "Custom";

    //Get all items for specific state
    public static List<PlanLibraryItem> GetItems( string stateAbbr, string urlTest )
    {
      var items = new List<PlanLibraryItem>();
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          var operatorId = context.Operators.FirstOrDefault(o=>o.StateAbbr == stateAbbr).OperatorID;
          if ( urlTest.Contains( "localhost" ) || urlTest.Contains( "test" ) )
          {
            items = ( from p in context.PlanLibraryItems
                      join s in context.PlanLibraryItemCustomItemScopes on p.PlanLibCode equals s.PlanLibCode
                      join o in context.Operators on s.ScopeId equals o.OperatorID
                      where p.ItemTypeID == _itemTypeId && o.OperatorID == operatorId
                      select p ).ToList();
          }
          else
          {
            items = ( from p in context.PlanLibraryItems
                      join s in context.PlanLibraryItemCustomItemScopes on p.PlanLibCode equals s.PlanLibCode
                      where p.ItemTypeID == _itemTypeId
                      select p ).ToList();
          }
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }

      return items;
    }

    //Get single PlanLibraryItem
    public static PlanLibraryItem GetItem( string stateAbbr, string urlTest, int id )
    {
      var item = new PlanLibraryItem();
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          item = context.PlanLibraryItems.FirstOrDefault( i => i.PlanLibCode == id );
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }

      return item;
    }

    //get sites for selected State
    public static Dictionary<int, string> GetSites( string stateAbbr, string urlTest )
    {
      var sites = new Dictionary<int, string>();
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          var operatorId = context.Operators.FirstOrDefault(o => o.StateAbbr == stateAbbr).OperatorID;
          sites = context.Sites.Where( s => s.OperatorID == operatorId ).ToDictionary( x => x.SiteID, r => r.SiteName );
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }
      return sites;
    }

    //get single site information
    public static Site GetSite( string stateAbbr, string urlTest, int id )
    {
      var site = new Site();
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          site = context.Sites.FirstOrDefault( s => s.SiteID == id );
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }

      return site;
    }

    //save single PlanLibraryItem
    public static void Save( PlanLibraryItem libraryItem, string stateAbbr, string urlTest, int? siteId = null )
    {
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          libraryItem.ItemType = _itemType;
          libraryItem.ItemTypeID = _itemTypeId;
          libraryItem.PlanLibraryItemScopeID = _itemScopeId;
          libraryItem.MetricValueNote = _metricValueNote;
          context.PlanLibraryItems.Add( libraryItem );

          context.SaveChanges();
          var scopeId = siteId ?? context.Operators.FirstOrDefault(o => o.StateAbbr == stateAbbr).OperatorID;
          SaveCustomItemScope( libraryItem.PlanLibCode, scopeId, context );
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }
    }

    //save entry to PlanLibraryItemsCustomItemScope
    public static void SaveCustomItemScope( int itemId, int scopeId, LibraryItemsContext context )
    {
      using ( context )
      {
        var customItemScopeEntry = new PlanLibraryItemCustomItemScope
        {
          PlanLibCode = itemId,
          CustomItemScopeId = scopeId
        };
        context.SaveChanges();
      }
    }
  }
}