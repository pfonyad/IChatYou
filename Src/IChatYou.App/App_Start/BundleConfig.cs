namespace IChatYou.App
{
    using System.Web;
    using System.Web.Optimization;

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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/message")
                .Include("~/Scripts/Message.js"));

            bundles.Add(new ScriptBundle("~/bundles/serilog")
                .Include("~/Scripts/Serilog.js"));

            bundles.Add(new StyleBundle("~/Content/serilog")
                .Include("~/Content/Serilog.css"));

            bundles.Add(new ScriptBundle("~/bundles/setting")
                .Include("~/Scripts/Setting.js"));

            bundles.Add(new ScriptBundle("~/bundles/message")
                .Include("~/Scripts/Message.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr")
                .Include("~/Scripts/jquery.signalR-2.2.2.min.js")
                .Include("~/signalr/hubs"));

            bundles.Add(new ScriptBundle("~/bundles/datatables")
                .Include("~/Scripts/moment-with-locales.min.js",
                "~/Scripts/daterangepicker.js",
                "~/Scripts/DataTables/jquery.dataTables.min.js",
                "~/Scripts/DataTables/dataTables.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                "~/Content/daterangepicker.css",
                "~/Content/DataTables/css/dataTables.bootstrap.min.css"));
        }
    }
}
