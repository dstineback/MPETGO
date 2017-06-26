using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web;

namespace MPETGO.Pages
{
    public partial class UpdateObject : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["n_objectid"]))
            {
                var n_objectid = Request.QueryString["n_objectid"].ToString();
                Session.Add("n_objectid", n_objectid);
            }
        }

        protected void updateLatLng(object sender, EventArgs e)
        {
            var lat = latValue.Text.ToString();
            var lng = lngValue.Text.ToString();
            var nobj = Session["n_objectid"].ToString();
            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];
            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE dbo.MaintenanceObjects SET GPS_X = lng, GPS_Y = lat, GPS_Z = 0.0 WHERE n_objectid = nobj";

            cmd.Connection = con;
            try
            {
                con.Open();

                Response.Write("<script>alert('GPS updated');</script>");

            } catch (Exception ex)
            {
                throw ex;
            }



        }
    }
}