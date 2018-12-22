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

    //Get all items for specific state
    public static List<PlanLibraryItem> GetItems(string state, string urlTest)
    {
      var items = new List<PlanLibraryItem>();
      try
      {
        using (var context = new LibraryItemsContext(state, urlTest))
        {
          items = (from p in context.PlanLibraryItems
            join s in context.PlanLibraryItemCustomItemScopes on p.PlanLibCode equals s.PlanLibCode
            where p.ItemTypeID == _itemTypeId
            select p).ToList();
        }
      }
      catch (Exception ex)
      {
        if (ex.InnerException != null) throw ex.InnerException;
      }

      return items;
    }
  }
}