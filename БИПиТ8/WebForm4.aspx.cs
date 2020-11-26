using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace БИПиТ8
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        WebService1 obj = new WebService1();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Content/Js/jquery-3.2.1.min.js",
            });
            if (!IsPostBack)
            {
                DropDownList1.DataSource = obj.Clients();
                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataBind();

                DropDownList2.DataSource = obj.Services();
                DropDownList2.DataTextField = "Value";
                DropDownList2.DataValueField = "Key";
                DropDownList2.DataBind();
            }
        }
        
        protected void Button1_Click(object sender, EventArgs e) //Добавление
        {
            if (Page.IsValid)
            {
                obj.NewRec(Convert.ToInt32(DropDownList1.SelectedValue), Convert.ToInt32(DropDownList2.SelectedValue), Convert.ToInt32(TextBox1.Text), Convert.ToDateTime(Txt1.Text));
                Page.Response.Redirect("WebForm3.aspx", true);
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = Convert.ToDouble(TextBox1.Text) > 0;
            }

            catch
            {
                args.IsValid = false;
            }
        }
    }
}