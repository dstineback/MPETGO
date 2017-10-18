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
    public partial class ObjectList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];

            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "form_MaintenanceObjects_GetAllMaintenanceObjects";

            cmd.Connection = con;
            try
            {
                con.Open();
                objectList.DataSource = cmd.ExecuteReader();
                objectList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}