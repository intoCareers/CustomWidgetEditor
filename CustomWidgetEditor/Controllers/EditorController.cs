using CustomWidgetEditor.Models;
using CustomWidgetEditor.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CustomWidgetEditor.Controllers
{
  public class EditorController : Controller
  {
    // GET: Editor
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult Current( string stateAbbr )
    {
      if ( Request.Url == null ) return View();
      var urlTest = Request.Url.AbsoluteUri;
      var items = ItemsManager.GetItems( stateAbbr, urlTest );
      var widgetVm = new List<WidgetVm>();
      if ( items != null )
      {
        widgetVm = items.Select( i => new WidgetVm
        {
          PlanLibCode = i.PlanLibCode,
          ItemTitle = i.ItemTitle,
          ItemDescription = i.ItemDescription,
          DefaultThreshold = i.DefaultThreshold,
          FormId = i.FormID,
          State = StatesDictionary.States.FirstOrDefault( s => s.Key == stateAbbr ).Value,
          StateAbbr = stateAbbr
        } ).ToList();
      }

      ViewBag.State = StatesDictionary.States.FirstOrDefault( s => s.Key == stateAbbr ).Value;
      ViewBag.StateAbbr = stateAbbr;
      return View( widgetVm );
    }

    public ActionResult Edit( int id, string stateAbbr )
    {
      if ( Request.Url == null ) return View( "Current" );
      var urlTest = Request.Url.AbsoluteUri;
      var item = ItemsManager.GetItem( stateAbbr, urlTest, id );
      var widgetVm = new WidgetVm
      {
        PlanLibCode = item.PlanLibCode,
        ItemTitle = item.ItemTitle,
        ItemDescription = item.ItemDescription,
        DefaultThreshold = item.DefaultThreshold,
        FormId = item.FormID,
        State = StatesDictionary.States.FirstOrDefault( s => s.Key == stateAbbr ).Value,
        StateAbbr = stateAbbr
      };
      return View( "AddNew", widgetVm );
    }

    public ActionResult AddNew( string stateAbbr = null )
    {
      var widgetVm = new WidgetVm();
      if ( stateAbbr == null ) return View( widgetVm );
      widgetVm.State = StatesDictionary.States.FirstOrDefault( s => s.Key == stateAbbr ).Value;
      widgetVm.StateAbbr = stateAbbr;
      return View( widgetVm );
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Save( WidgetVm widgetVm )
    {
      if ( !ModelState.IsValid )
      {
        var vm = new WidgetVm
        {
          PlanLibCode = widgetVm.PlanLibCode,
          ItemTitle = widgetVm.ItemTitle,
          ItemDescription = widgetVm.ItemDescription,
          DefaultThreshold = widgetVm.DefaultThreshold,
          FormId = widgetVm.FormId,
          State = widgetVm.State,
          StateAbbr = widgetVm.StateAbbr
        };
        return View( "AddNew", vm );
      }
      if ( Request.Url == null ) return View( "Current" );
      var urlTest = Request.Url.AbsoluteUri;
      var libraryItem = new PlanLibraryItem
      {
        ItemTitle = widgetVm.ItemTitle,
        ItemDescription = widgetVm.ItemDescription,
        DefaultThreshold = widgetVm.DefaultThreshold,
        FormID = widgetVm.FormId
      };
      if ( widgetVm.PlanLibCode != 0 )
      {
        libraryItem.PlanLibCode = widgetVm.PlanLibCode;
      }
      ItemsManager.Save( libraryItem, widgetVm.State, urlTest );
      return RedirectToAction( "Current", "Editor", new { stateAbbr = widgetVm.StateAbbr } );
    }
  }
}