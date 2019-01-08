using System;
using System.Collections.Generic;
using System.Linq;
using CustomWidgetEditor.ViewModels;

namespace CustomWidgetEditor.Models
{
  public static class ItemsManager
  {
    private const string _itemType = "FORM";
    private const string _itemTypeId = "CustomModule";
    private const int _itemScopeId = 3;
    private const string _metricValueNote = "Custom";

    //Get all items for specific state
    public static List<WidgetVm> GetItems( string stateAbbr, string urlTest )
    {
      var items = new List<WidgetVm>();
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          var operatorId = context.Operators.FirstOrDefault(o=>o.StateAbbr == stateAbbr).OperatorID;
          if ( urlTest.Contains( "localhost" ) || urlTest.Contains( "test" ) )
          {
            items = ( from p in context.PlanLibraryItems
                .Where( p => p.ItemTypeID == _itemTypeId )
              from s in context.PlanLibraryItemCustomItemScopes
                .Where( s => s.PlanLibCode == p.PlanLibCode && s.OperatorId == operatorId )
              from o in context.Operators
                .Where( o => o.OperatorID == operatorId )
              from si in context.Sites
                .Where( si => si.SiteID == s.ScopeId && si.OperatorID == operatorId )
                .DefaultIfEmpty()
              select new WidgetVm()
              {
                PlanLibCode = p.PlanLibCode,
                ItemTitle = p.ItemTitle,
                ItemDescription = p.ItemDescription,
                DefaultThreshold = p.DefaultThreshold,
                FormId = p.FormID,
                StateAbbr = stateAbbr,
                SiteId = si.SiteID,
                SiteName = si.SiteName
              } ).ToList();
          }
          else
          {
            items = ( from p in context.PlanLibraryItems
                      join s in context.PlanLibraryItemCustomItemScopes on p.PlanLibCode equals s.PlanLibCode
                      where p.ItemTypeID == _itemTypeId 
                      select new WidgetVm()
                      {

                      } ).ToList();
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
    public static WidgetVm GetItem( string stateAbbr, string urlTest, int id )
    {
      var widgetVm = new WidgetVm();
      try
      {
        using (var context = new LibraryItemsContext(stateAbbr, urlTest))
        {
          var operatorId = context.Operators.FirstOrDefault(o => o.StateAbbr == stateAbbr).OperatorID;
          widgetVm = (from p in context.PlanLibraryItems
              .Where(p => p.PlanLibCode == id)
            from s in context.PlanLibraryItemCustomItemScopes
              .Where(s => s.PlanLibCode == p.PlanLibCode && s.OperatorId == operatorId)
            from si in context.Sites
              .Where(si => si.SiteID == s.ScopeId && si.OperatorID == operatorId)
              .DefaultIfEmpty()
            select new WidgetVm()
            {
              PlanLibCode = p.PlanLibCode,
              ItemTitle = p.ItemTitle,
              ItemDescription = p.ItemDescription,
              DefaultThreshold = p.DefaultThreshold,
              FormId = p.FormID,
              StateAbbr = stateAbbr,
              SiteId = si.SiteID,
              SiteName = si.SiteName
            }).FirstOrDefault();

        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
   

      return widgetVm;
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

    //save single PlanLibraryItem
    public static void Save( WidgetVm widgetVm, string stateAbbr, string urlTest, int? siteId = null )
    {
      var libraryItem = new PlanLibraryItem();
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