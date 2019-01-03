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

    public ActionResult Current( string stateAbbr )
    {
      if (Request.Url == null) return View();
      var urlTest = Request.Url.AbsoluteUri;
      var items = ItemsManager.GetItems(stateAbbr, urlTest);
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
      ViewBag.State = StatesDictionary.States.FirstOrDefault(s => s.Key == stateAbbr).Value;
      return View( widgetVm );

    }

    public ActionResult Edit(int id)
    {
      return View();
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