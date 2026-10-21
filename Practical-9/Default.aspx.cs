using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PlacementDashboard
{
    public partial class _Default : System.Web.UI.Page
    {
        string cs = ConfigurationManager
            .ConnectionStrings["PlacementConnection"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPlacementData();
            }
        }

        private void LoadPlacementData()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Placements";

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                lblTotal.Text = dt.Rows.Count.ToString();
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            lblMessage.Text =
                "Dashboard data is loaded. Configure the Crystal Report " +
                "file and CrystalReportViewer to display the report.";
        }
    }
}
