using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MPETGO
{
    public partial class index : System.Web.UI.Page
    {
        private LogonObject _oLogon;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Check for Logon
            if (Session["LogonInfo"] != null)
            {
                _oLogon = ((LogonObject)Session["logonInfo"]);

            }
            else
            {
                Response.Redirect("~/Logon.aspx", true);
            }
            #endregion
        }

        protected void setSessionLat(object sender, EventArgs e)
        {
            
            if (Session["latValue"] != null)
            {
                Session.Remove("latValue");
            }
            //Session.Add("latValue", latValue.Text);

        }

        protected void setSessionLng(object sender, EventArgs e)
        {
            
            if (Session["lngValue"] != null)
            {
                Session.Remove("lngValue");
            }
            //Session.Add("lngValue", lngValue.Text);
        }
    }
}