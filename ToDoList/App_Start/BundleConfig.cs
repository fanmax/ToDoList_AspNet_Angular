using System.Web;
using System.Web.Optimization;

namespace ToDoList
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/styles").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-theme.min.css",
                "~/Content/ui-bootstrap-csp.css",
                "~/Content/iziToast.min.css",
                "~/Content/Site.css"
                ));

            bundles.Add(new ScriptBundle("~/bootstrap").Include(
                "~/Scripts/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/ng").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-cookies.min.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js"
                ));

            bundles.Add(new ScriptBundle("~/app").IncludeDirectory("~/Scripts/app", "*.js", true));

            bundles.Add(new ScriptBundle("~/jquery").Include(
                "~/Scripts/jquery-3.1.1.min.js"
                ));

            bundles.Add(new ScriptBundle("~/misc").Include(
                "~/Scripts/modernizr-2.8.3.js",
                "~/Scripts/respond.min.js",
                "~/Scripts/iziToast.min.js"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
