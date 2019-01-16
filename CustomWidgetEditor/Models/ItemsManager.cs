using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
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
          var operatorId = context.Operators.FirstOrDefault( o => o.StateAbbr == stateAbbr ).OperatorID;
          items = ( from p in context.PlanLibraryItems
              .Where( p => p.ItemTypeID == _itemTypeId )
                    from s in context.PlanLibraryItemsCustomItemScopes
                      .Where( s => s.PlanLibCode == p.PlanLibCode && s.OperatorId == operatorId )
                    from o in context.Operators
                      .Where( o => o.OperatorID == operatorId )
                    from si in context.Sites
                      .Where( si => si.SiteID == s.SiteId && si.OperatorID == operatorId )
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
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          var operatorId = context.Operators.FirstOrDefault( o => o.StateAbbr == stateAbbr ).OperatorID;
          widgetVm = ( from p in context.PlanLibraryItems
               .Where( p => p.PlanLibCode == id )
                       from s in context.PlanLibraryItemsCustomItemScopes
                         .Where( s => s.PlanLibCode == p.PlanLibCode && s.OperatorId == operatorId )
                       from si in context.Sites
                         .Where( si => si.SiteID == s.SiteId && si.OperatorID == operatorId )
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
                       } ).FirstOrDefault();
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }

      widgetVm.State = StatesDictionary.States.FirstOrDefault(x => x.Key == stateAbbr).Value;
      return widgetVm;
    }

    //get sites for selected State
    public static List<SelectListItem> GetSites( string stateAbbr, string urlTest )
    {
      var selectList = new List<SelectListItem>();
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          var operatorId = context.Operators.FirstOrDefault( o => o.StateAbbr == stateAbbr ).OperatorID;
          var sites = context.Sites.Where( s => s.OperatorID == operatorId ).ToDictionary( x => x.SiteID, r => r.SiteName );
          foreach ( var site in sites )
          {
            selectList.Add( new SelectListItem
            {
              Value = site.Key.ToString(),
              Text = site.Value
            } );
          }
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }
      return selectList;
    }

    //save single PlanLibraryItem
    public static void Save( WidgetVm widgetVm, string stateAbbr, string urlTest, int? siteId = null )
    {
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          var libraryItem = new PlanLibraryItem();
          if ( widgetVm.PlanLibCode == 0 )
          {

            libraryItem = new PlanLibraryItem
            {
              ItemType = _itemType,
              ItemTypeID = _itemTypeId,
              ItemTitle = widgetVm.ItemTitle,
              ItemDescription = widgetVm.ItemDescription,
              DefaultThreshold = widgetVm.DefaultThreshold,
              MetricValueNote = _metricValueNote,
              PlanLibraryItemScopeID = _itemScopeId,
              FormID = widgetVm.FormId
            };
            context.PlanLibraryItems.Add( libraryItem );
          }
          else
          {
            libraryItem = context.PlanLibraryItems.FirstOrDefault( i => i.PlanLibCode == widgetVm.PlanLibCode );
            if ( libraryItem == null ) return;
            libraryItem.ItemTitle = widgetVm.ItemTitle;
            libraryItem.ItemDescription = widgetVm.ItemDescription;
            libraryItem.DefaultThreshold = widgetVm.DefaultThreshold;
            libraryItem.FormID = widgetVm.FormId;
          }

          context.SaveChanges();

          if ( widgetVm.PlanLibCode != 0 ) return;
          var operatorId = context.Operators.FirstOrDefault( o => o.StateAbbr == stateAbbr ).OperatorID;
          var scopeId = siteId;
          SaveCustomItemScope( libraryItem.PlanLibCode, scopeId, operatorId, context );
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }
    }

    //save entry to PlanLibraryItemsCustomItemScope
    public static void SaveCustomItemScope( int itemId, int? scopeId, int operatorId, LibraryItemsContext context )
    {
      using ( context )
      {
        var customItemScopeEntry = new PlanLibraryItemsCustomItemScope
        {
          PlanLibCode = itemId,
          SiteId = scopeId,
          OperatorId = operatorId
        };
        context.PlanLibraryItemsCustomItemScopes.Add( customItemScopeEntry );
        context.SaveChanges();
      }
    }

    public static void Delete( string stateAbbr, string urlTest, int id )
    {
      try
      {
        using ( var context = new LibraryItemsContext( stateAbbr, urlTest ) )
        {
          var formInDb = context.PlanLibraryItems.FirstOrDefault( i => i.PlanLibCode == id );
          context.PlanLibraryItems.Remove( formInDb );
          context.SaveChanges();
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }
    }
  }
}