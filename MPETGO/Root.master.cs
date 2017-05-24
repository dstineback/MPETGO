using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace MPETGO {
    public partial class RootMaster : System.Web.UI.MasterPage {
        private LogonObject _oLogon;

        protected void Page_Load(object sender, EventArgs e) {
        
        if(Session["LogonInfo"] != null)
            {
                _oLogon = ((LogonObject)Session["LogonInfo"]);
                var userName = _oLogon.FullName.ToString();
                userNameLable.Text = userName;     
            } 
        ASPxLabel2.Text = DateTime.Now.Year + Server.HtmlDecode(" &copy; Copyright by Four Winds Group Inc.");
        }
    }
}