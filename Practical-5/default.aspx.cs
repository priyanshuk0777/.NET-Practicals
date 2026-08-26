using System;

namespace Practical5
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnApply_Click(
            object sender, EventArgs e)
        {
            // Get selected date from Calendar
            DateTime selectedDate =
                Calendar1.SelectedDate;

            // Store selected date in Session
            Session["LeaveDate"] =
                selectedDate.ToString("dd/MM/yyyy");

            // Display selected date
            lblSelectedDate.Text =
                "Selected Date: " +
                selectedDate.ToString("dd/MM/yyyy");

            // Open Leave page
            Response.Redirect("Leave.aspx");
        }
    }
}
