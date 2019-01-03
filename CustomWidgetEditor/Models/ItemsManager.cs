using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace CustomWidgetEditor.Models
{
  public static class ItemsManager
  {
    private const string _itemType = "FORM";
    private const string _itemTypeId = "CustomModule";
    private const int _itemScopeId = 3;
    private const string _metricValueNote = "Custom";

    //Get all items for specific state
    public static List<PlanLibraryItem> GetItems( string state, string urlTest )
    {
      var items = new List<PlanLibraryItem>();
      try
      {
        using ( var context = new LibraryItemsContext( state, urlTest ) )
        {
          items = ( from p in context.PlanLibraryItems
                    join s in context.PlanLibraryItemCustomItemScopes on p.PlanLibCode equals s.PlanLibCode
                    where p.ItemTypeID == _itemTypeId
                    select p ).ToList();
        }
      }
      catch ( Exception ex )
      {
        throw ex;
      }

      return items;
    }

    //Get single PlanLibraryItem
    public static PlanLibraryItem GetItem(string state, string urlTest, int id)
    {
      var item = new PlanLibraryItem();
      try
      {
        using (var context = new LibraryItemsContext(state, urlTest))
        {
          item = context.PlanLibraryItems.FirstOrDefault(i => i.PlanLibCode == id);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }

      return item;
    }

    //save single PlanLibraryItem
    public static void Save(PlanLibraryItem libraryItem, string state, string urlTest)
    {
      try
      {
        using (var context = new LibraryItemsContext(state, urlTest))
        {
          if (libraryItem.PlanLibCode == 0)
          {
            libraryItem.ItemType = _itemType;
            libraryItem.ItemTypeID = _itemTypeId;
            libraryItem.PlanLibraryItemScopeID = _itemScopeId;
            libraryItem.MetricValueNote = _metricValueNote;
            context.PlanLibraryItems.Add(libraryItem);
          }
          else
          {
            var itemInDb = context.PlanLibraryItems.FirstOrDefault(i => i.PlanLibCode == libraryItem.PlanLibCode);
            itemInDb.ItemTitle = libraryItem.ItemTitle;
            itemInDb.ItemDescription = libraryItem.ItemDescription;
            itemInDb.DefaultThreshold = libraryItem.DefaultThreshold;
            itemInDb.FormID = libraryItem.FormID;
          }

          context.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}