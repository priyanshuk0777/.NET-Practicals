using System;
using CrystalDecisions.CrystalReports.Engine;

namespace PlacementDashboard
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportDocument report = new ReportDocument();

                report.Load(
                    Server.MapPath("~/Reports/PlacementReport.rpt"));

                CrystalReportViewer1.ReportSource = report;

                Session["PlacementReport"] = report;
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            // ReportDocument lifetime and disposal should be
            // managed according to the application's report
            // viewer lifecycle and Crystal Reports version.
        }
    }
}
