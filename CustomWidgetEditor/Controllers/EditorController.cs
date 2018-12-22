using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using CustomWidgetEditor.Models;
using CustomWidgetEditor.ViewModels;

namespace CustomWidgetEditor.Controllers
{
  public class EditorController : Controller
  {
    // GET: Editor
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Current(string state)
    {
      if (Request.Url != null)
      {
        var urlTest = Request.Url.AbsoluteUri;
        var items = ItemsManager.GetItems(state, urlTest);
      
        return View( items );
      }
    }

    public ActionResult AddNew( int id )
    {
      var widget = _context.PlanLibraryItems.FirstOrDefault( m => m.PlanLibCode == id );
      if ( widget == null ) RedirectToAction( "Index" );
      var widgetVm = new WidgetVm
      {
        Id = widget.PlanLibCode,
        ItemTitle = widget.ItemTitle,
        ItemDescription = widget.ItemDescription,
        DefaultThreshold = widget.DefaultThreshold,
        FormId = widget.FormID
      };
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

      if ( widgetVm.Id == 0 )
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