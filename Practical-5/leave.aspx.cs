using System;

namespace Practical5
{
    public partial class Leave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get Leave Date from Session
                if (Session["LeaveDate"] != null)
                {
                    lblLeaveDate.Text =
                        Session["LeaveDate"].ToString();
                }

                // Check Employee Name Cookie
                if (Request.Cookies["EmployeeName"] != null)
                {
                    txtEmployeeName.Text =
                        Request.Cookies["EmployeeName"].Value;
                }

                // Hide result initially
                pnlResult.Visible = false;
            }
        }

        // Show Other Leave TextBox
        protected void ddlLeaveType_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (ddlLeaveType.SelectedValue == "Other")
            {
                trOtherLeave.Visible = true;
            }
            else
            {
                trOtherLeave.Visible = false;
                txtOtherLeave.Text = "";
            }
        }

        // Submit Leave Application
        protected void btnSubmit_Click(
            object sender, EventArgs e)
        {
            // Save employee name in Cookie
            if (chkRemember.Checked)
            {
                Response.Cookies["EmployeeName"].Value =
                    txtEmployeeName.Text;

                Response.Cookies["EmployeeName"].Expires =
                    DateTime.Now.AddDays(30);
            }

            // Get Leave Type
            string leaveType;

            if (ddlLeaveType.SelectedValue == "Other")
            {
                leaveType = txtOtherLeave.Text;
            }
            else
            {
                leaveType =
                    ddlLeaveType.SelectedItem.Text;
            }

            // Show submitted details
            lblResultName.Text =
                txtEmployeeName.Text;

            lblResultDate.Text =
                lblLeaveDate.Text;

            lblResultType.Text =
                leaveType;

            lblResultReason.Text =
                txtReason.Text;

            lblResultStatus.Text =
                "Submitted";

            // Show result panel
            pnlResult.Visible = true;
        }
    }
}
