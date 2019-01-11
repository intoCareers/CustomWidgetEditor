using CustomWidgetEditor.Models;
using CustomWidgetEditor.ViewModels;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

namespace CustomWidgetEditor.Controllers
{
  [RoutePrefix("Editor")]
  public class EditorController : Controller
  {
    [HttpGet]
    // GET: Editor
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    [Route( "Editor/Current/{stateAbbr}" )]
    public ActionResult Current( string stateAbbr )
    {
      if ( string.IsNullOrEmpty( stateAbbr ) ) return RedirectToAction( "Index" );
      if ( Request.Url == null ) return View();
      var urlTest = Request.Url.AbsoluteUri;
      var items = ItemsManager.GetItems( stateAbbr, urlTest );

      ViewBag.State = StatesDictionary.States.FirstOrDefault( s => s.Key == stateAbbr ).Value;
      ViewBag.StateAbbr = stateAbbr;
      return View( items );
    }

    [Route( "Editor/Edit/{id}/{stateAbbr}" )]
    public ActionResult Edit( int id, string stateAbbr )
    {
      if ( string.IsNullOrEmpty( stateAbbr ) ) return RedirectToAction( "Index" );
      if ( Request.Url == null ) return View( "Current" );
      var urlTest = Request.Url.AbsoluteUri;
      var widgetVm = ItemsManager.GetItem( stateAbbr, urlTest, id );
      widgetVm.State = StatesDictionary.States.FirstOrDefault( s => s.Key == stateAbbr ).Value;
      return View( "AddNew", widgetVm );
    }

    //[Route("Editor/AddNew/{stateAbbr}")]
    public ActionResult AddNew( string stateAbbr )
    {
      if ( string.IsNullOrEmpty( stateAbbr ) ) return RedirectToAction( "Index", "Editor" );
      if ( Request.Url == null ) return View( "Current" );
      var urlTest = Request.Url.AbsoluteUri;
      var widgetVm = new WidgetVm
      {
        State = StatesDictionary.States.FirstOrDefault( s => s.Key == stateAbbr ).Value,
        StateAbbr = stateAbbr,
        Sites = ItemsManager.GetSites( stateAbbr, urlTest )
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
          PlanLibCode = widgetVm.PlanLibCode,
          ItemTitle = widgetVm.ItemTitle,
          ItemDescription = widgetVm.ItemDescription,
          DefaultThreshold = widgetVm.DefaultThreshold,
          FormId = widgetVm.FormId,
          State = widgetVm.State,
          StateAbbr = widgetVm.StateAbbr,
          SiteId = widgetVm.SiteId,
          SiteName = widgetVm.SiteName
        };
        return View( "AddNew", vm );
      }
      if ( Request.Url == null ) return View( "Current" );
      var urlTest = Request.Url.AbsoluteUri;

      ItemsManager.Save( widgetVm, widgetVm.StateAbbr, urlTest, widgetVm.SiteId );
      return RedirectToAction( "Current", "Editor", new { stateAbbr = widgetVm.StateAbbr } );
    }

    [HttpDelete]
    [Route( "Editor/Delete" )]
    public void Delete( int id, string stateAbbr )
    {
      var urlTest = Request.Url.AbsoluteUri;
      ItemsManager.Delete( stateAbbr, urlTest, id );
    }
  }
}