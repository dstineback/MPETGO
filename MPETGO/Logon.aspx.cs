using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MPETGO
{
    public partial class Logon : System.Web.UI.Page
    {
        private LogonObject _oLogon;

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (Session["LogonInfo"] == null)
                {
                    txtUsername.Focus();

                } else
                {
                    if(Session["LogonInfo"] != null)
                    {
                        Response.Redirect("~/index.aspx");
                    }
                }

            }
        }

        protected void btnSubmitLoginCredentials_Click(object sender, EventArgs e)
        {
            //Instanciate Class & Values
            _oLogon = new LogonObject { Username = txtUsername.Text, Password = txtPassword.Text };

            //Run Login Routine
            if (_oLogon.PerformLogin())
            {
                //Check For Previous Session Variable
                if (HttpContext.Current.Session["LogonInfo"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("LogonInfo");
                }

                //Add New Session State For Logon
                HttpContext.Current.Session.Add("LogonInfo", _oLogon);

                Response.Redirect("/index.aspx");

            }
            else
            {
                Response.Write("<script language='javascript'>alert('Could not Log in. Please check Username and Password.');</script>");

                //Set Focus
                txtPassword.Focus();
            }

        }
    }
}