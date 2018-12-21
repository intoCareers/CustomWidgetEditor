using System.Web;
using System.Web.Optimization;

namespace CustomWidgetEditor
{
  public class BundleConfig
  {
    public static void RegisterBundles( BundleCollection bundles )
    {
      bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
        "~/Scripts/jquery-{version}.js",
        "~/Scripts/bootstrap.js",
        "~/Scripts/DataTables/jquery.dataTables.js",
        "~/Scripts/DataTables/dataTables.bootstrap.js",
        "~/Scripts/bootbox.js" ) );

      bundles.Add( new ScriptBundle( "~/bundles/jquery-ui" ).Include(
        "~/Scripts/jquery-ui-{version}.js" ) );

      bundles.Add( new ScriptBundle( "~/bundles/jqueryval" ).Include(
        "~/Scripts/jquery.validate*",
        "~/Scripts/jquery.unobtrusive*" ) );

      bundles.Add( new ScriptBundle( "~/bundles/modernizr" ).Include(
        "~/Scripts/modernizr-*" ) );

      bundles.Add( new StyleBundle( "~/Content/css" ).Include(
        "~/Content/DataTables/css/*.css",
        "~/Content/*.css" ) );
    }
  }
}
