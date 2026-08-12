using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace P4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Disables unobtrusive validation for this specific page
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string gender = "";

                if (MALE.Checked)
                    gender = "Male";
                else if (FEMALE.Checked)
                    gender = "Female";

                string message = "Registration Successful!\n\n";
                message += "Name : " + TextBox1.Text + "\n";
                message += "Email : " + TextBox2.Text + "\n";
                message += "GR No : " + TextBox3.Text + "\n";
                message += "Class : " + DropDownList1.SelectedItem.Text + "\n";
                message += "Enrollment No : " + TextBox4.Text + "\n";
                message += "Phone No : " + TextBox5.Text + "\n";
                message += "Gender : " + gender + "\n";
                message += "Course : " + DropDownList3.SelectedItem.Text + "\n";
                message += "Event : " + DropDownList2.SelectedItem.Text;

                Response.Write("<script>alert('" +
                    message.Replace("\n", "\\n") +
                    "');</script>");
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";

            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;

            MALE.Checked = false;
            FEMALE.Checked = false;
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
