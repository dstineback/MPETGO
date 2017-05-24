using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MPETDSFactory;

namespace MPETGO.Pages
{
    public partial class WorkRequestList : System.Web.UI.Page
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
                Response.Redirect("/Logon.aspx");
            }

            #endregion
        }
    }
}