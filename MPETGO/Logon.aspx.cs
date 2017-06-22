using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data;

namespace MPETGO
{
    public partial class Logon : Page
    {
        private LogonObject _oLogon;
        protected global::DevExpress.Web.ASPxTextBox txtUsername;
        protected global::DevExpress.Web.ASPxTextBox txtPassword;
        protected global::DevExpress.Web.ASPxButton submitBtn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LogonInfo"] != null)
            {
                Response.Redirect("~/", true);

            }
            else
            {
                LogonPopUp.Enabled = true;
            }


            

        }

        protected void Page_Init()
        {

        }

        protected void SetLogon()
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

                Response.Redirect("/index.aspx", true);

            }
            else
            {
                Response.Write("<script language='javascript'>alert('Could not Log in. Please check Username and Password.');</script>");

                //Set Focus
                txtPassword.Focus();
            }
        }

        protected void btnSubmitLoginCredentials_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["LogonInfo"] != null)
                {
                    Response.Redirect("~/index.aspx", true);

                }
                else
                {
                   

                    if ((txtUsername.Text != "") && (txtPassword.Text != ""))
                    {
                        if (Session["userName"] != null)
                        {
                            Session.Remove("userName");
                        }
                        Session.Add("userName", txtUsername.Text);

                        if (Session["password"] != null)
                        {
                            Session.Remove("password");
                        }
                        Session.Add("password", txtPassword.Text);

                        //SetLogon();
                    }

                }
            }
            if (!IsPostBack)
            {
                if (Session["LogonInfo"] == null)
                {
                    txtUsername.Focus();

                }
                else
                {
                    if (Session["LogonInfo"] != null)
                    {
                        Response.Redirect("~/index.aspx", true);
                    }
                }

            }

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

                Response.Redirect("~/index.aspx", true);

            }
            else
            {
                Response.Write("<script language='javascript'>alert('Could not Log in. Please check Username and Password.');</script>");

                //Set Focus
                txtPassword.Focus();
            }

        }

        protected void LogonPopUp_Load(object sender, EventArgs e)
        {
            LogonPopUp.Enabled = true;
        }
    }
}