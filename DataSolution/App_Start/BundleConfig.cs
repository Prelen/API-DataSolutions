using System.Web;
using System.Web.Optimization;

namespace DataSolution
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                "~/Assets/libs/popperjs/dist/umd/popper.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Assets/libs/perfect-scrollbar/dist/perfect-scrollbar.min.js",
                "~/Assets/extra-libs/sparkline/sparkline.js",
                "~/Assets/dist/js/waves.js",
                "~/Assets/dist/js/sidebarmenu.js",
                "~/Assets/dist/js/custom.min.js",
                "~/Assets/libs/flot/excanvas.js",
                "~/Assets/libs/flot/jquery.flot.js",
                "~/Assets/libs/flot/jquery.flot.pie.js",
                "~/Asset/libs/flot/jquery.flot.time.js",
                "~/Asset/libs/flot/jquery.flot.stack.js",
                "~/Asset/libs/flot/jquery.flot.crosshair.js",
                "~/Asset/libs/flot.tooltip/js/jquery.flot.tooltip.min.js",
                "~/Assets/dist/js/pages/chart/chart-page-init.js"
                ));

            bundles.Add(new StyleBundle("~/Content/themecss").Include(
                    "~/Assets/dist/css/icons/font-awesome/css/fontawesome-all.css",
                    "~/Assets/dist/css/icons/themify-icons/themify-icons.css",
                    "~/Assets/dist/css/icons/material-design-iconic-font/css/materialdesignicons.min.css"
                ));
        }
    }
}
