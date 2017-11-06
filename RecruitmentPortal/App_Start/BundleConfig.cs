using System.Web;
using System.Web.Optimization;

namespace RecruitmentPortal
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

            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
                        "~/Scripts/jquery.maskedinput-1.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.10.4.js"));            

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/datatables/jquery.dataTables.js",
                        "~/Scripts/datatables/dataTables.tableTools.js"));

            bundles.Add(new ScriptBundle("~/bundles/recruitmentportal").Include(
                        "~/Scripts/recruitmentportal/recruitmentportal.js",
                        "~/Scripts/recruitmentportal/recruitmentportalBootstrapModals.js"));   

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));            

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-combobox.js",
                        "~/Scripts/datatables/dataTables.bootstrap.js",
                        "~/Scripts/TwitterBootstrapMvcJs.js",
                        "~/Scripts/respond.js"));

            /* js files of views */
            bundles.Add(new ScriptBundle("~/bundles/resourceedit/js").Include(
                        "~/Scripts/views/resource_edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/documentupload/js").Include(
                "~/Scripts/views/document_upload.js"));

            bundles.Add(new ScriptBundle("~/bundles/interviewercreate/js").Include(
                "~/Scripts/views/interviewer_create.js"));

            bundles.Add(new ScriptBundle("~/bundles/interviewcreate/js").Include(
                "~/Scripts/views/interview_create.js"));

            bundles.Add(new ScriptBundle("~/bundles/usersearch/js").Include(
                "~/Scripts/views/user_search.js"));

            bundles.Add(new ScriptBundle("~/bundles/requirementcreate/js").Include(
                "~/Scripts/views/requirement_create.js"));

            bundles.Add(new ScriptBundle("~/bundles/list/js").Include(
                "~/Scripts/views/list.js"));

            // CSS Bundles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/bootstrap-datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/datatables/css").Include(
                        "~/Content/dataTables.bootstrap.css"));  
        }
    }
}
