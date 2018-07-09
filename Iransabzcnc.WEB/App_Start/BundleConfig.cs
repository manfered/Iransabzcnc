using Iransabzcnc.WEB.App_Start;
using System.Web;
using System.Web.Optimization;

namespace Iransabzcnc.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/navAnim").Include(
                      "~/Scripts/classie.js",
                      "~/Scripts/cbpAnimatedHeader.js"));

            bundles.Add(new ScriptBundle("~/bundles/easing").Include(
                      "~/Scripts/jquery.easing.1.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/nanoGallery2").Include(
                      "~/Scripts/jquery.nanogallery2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scrolling").Include(
                      "~/Scripts/scrolling.js"));

            bundles.Add(new ScriptBundle("~/bundles/SummernoteJS").Include(
                      "~/Scripts/summernote.js"));

            bundles.Add(new ScriptBundle("~/bundles/WowJs").Include(
                      "~/Scripts/wow.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/AngularJs").Include(
                      "~/Scripts/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ContactJs").Include(
                      "~/Scripts/jquery.form.js",
                      "~/Scripts/ContactScript.js"));

            bundles.Add(new StyleBundle("~/Content/preCss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/animate.css",
                      "~/Content/nanogallery2.min.css",
                      "~/Content/site.less"));


            //dotless wit bundle and minification
            var lessBundle = new Bundle("~/Content/css");

            lessBundle.Include("~/Content/NavbarParameters.less");
            lessBundle.Include("~/Content/parameters.less");
            lessBundle.Include("~/Content/mixins.less");
            lessBundle.Include("~/Content/site.less");

            lessBundle.Transforms.Add(new LessTransform(HttpRuntime.AppDomainAppPath + "/Content/"));
            lessBundle.Transforms.Add(new CssMinify());

            bundles.Add(lessBundle);
            /////////



            bundles.Add(new StyleBundle("~/Content/AdminCss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/AdminSite.css"));
            


        }
    }
}
