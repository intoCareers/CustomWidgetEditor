using System.Collections.Generic;
using System.Linq;
using CustomWidgetEditor.Models;
using CustomWidgetEditor.ViewModels;
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
    public ActionResult Current( string state )
    {
      if (Request.Url == null) return View();
      var urlTest = Request.Url.AbsoluteUri;
      var items = ItemsManager.GetItems(state, urlTest);
      var widgetVm = new List<WidgetVm>();
      if (items != null)
      {
        widgetVm = items.Select(i => new WidgetVm
        {
          PlanLibCode = i.PlanLibCode,
          ItemTitle = i.ItemTitle,
          ItemDescription = i.ItemDescription,
          DefaultThreshold = i.DefaultThreshold,
          FormId = i.FormID
        }).ToList();
      }
      var stateDictionary = StatesDictionary.States.FirstOrDefault(s => s.Key == state);
      ViewBag.State = stateDictionary.Key;
      ViewBag.StateAbbr = stateDictionary.Value;
      return View( widgetVm );

    }

    public ActionResult Edit(int id, string state)
    {
      if (Request.Url == null) return View("Current");
      var urlTest = Request.Url.AbsoluteUri;
      var item = ItemsManager.GetItem(state, urlTest, id);
      var widgetVm = new WidgetVm
      {
        PlanLibCode = item.PlanLibCode,
        ItemTitle = item.ItemTitle,
        ItemDescription = item.ItemDescription,
        DefaultThreshold = item.DefaultThreshold,
        FormId = item.FormID,
        State = state
      };
      return View("AddNew", widgetVm);
    }

    public ActionResult AddNew( )
    {
      var widgetVm = new WidgetVm();
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

        };
        return View( "AddNew", vm );
      }

      if ( widgetVm.PlanLibCode == 0 )
      {

      }
      else
      {
        //var widgetInDb = _context.PlanLibraryItems.Single( m => m.PlanLibCode == widgetVm.Id );

      }

      //_context.SaveChanges();

      return RedirectToAction( "Index", "Editor" );
    }
  }
}