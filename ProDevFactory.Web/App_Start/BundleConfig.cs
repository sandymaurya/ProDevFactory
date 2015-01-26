using System;
using System.Web;
using System.Web.Optimization;

namespace ProDevFactory.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .IncludeDirectory("~/Content/scripts/jquery", "jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .IncludeDirectory("~/Content/scripts/jquery-validate", "jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .IncludeDirectory("~/Content/scripts/modernizr", "modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .IncludeDirectory("~/Content/plugins/bootstrap/js", "*.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/angular")
                .IncludeDirectory("~/Content/scripts/angular/installed", "angular.*")
                .IncludeDirectory("~/Content/plugins/ui.bootstrap", "*-0.12.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/nghomepage")
                .IncludeDirectory("~/Content/scripts/angular/controller", "*-controller.js"));


            bundles.Add(new StyleBundle("~/Content/css")
                .IncludeDirectory("~/Content/styles/skartherp", "*.css"));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
