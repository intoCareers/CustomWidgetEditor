using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using CustomWidgetEditor.Models;
using CustomWidgetEditor.ViewModels;

namespace CustomWidgetEditor.Controllers
{
  public class EditorController : Controller
  {
    private readonly LibraryItemsContext _context;

    public EditorController()
    {
      _context = new LibraryItemsContext();
    }

    protected override void Dispose( bool disposing )
    {
      _context.Dispose();
    }

    // GET: Editor
    public ActionResult Index()
    {
      var items = ( from p in _context.PlanLibraryItems
                    where p.ItemTypeID == "CustomModule"
                    select new WidgetVm
                    {
                      Id = p.PlanLibCode,
                      ItemTitle = p.ItemTitle,
                      ItemDescription = p.ItemDescription,
                      DefaultThreshold = p.DefaultThreshold,
                      FormId = p.FormID
                    } ).ToList();
      return View( items );
    }

    public ActionResult Details( int id )
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
        return View( "Details", vm );
      }

      if ( widgetVm.Id == 0 )
      {

      }
      else
      {
        var widgetInDb = _context.PlanLibraryItems.Single( m => m.PlanLibCode == widgetVm.Id );

      }

      _context.SaveChanges();

      return RedirectToAction( "Index", "Editor" );
    }
  }
}