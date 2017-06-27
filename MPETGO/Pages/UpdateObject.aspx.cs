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
            //Temp Vars 
            var lat = latValue.Text.ToString();
            var lng = lngValue.Text.ToString();
            var nobj = Session["n_objectid"].ToString();
            
            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];
            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            //Add Paramerters for SQL query
            cmd.Parameters.AddWithValue("@n_objectid", nobj);
            cmd.Parameters.AddWithValue("@GPS_Y", lat);
            cmd.Parameters.AddWithValue("@GPS_X", lng);

            //SQL Query
            cmd.CommandText = "UPDATE dbo.MaintenanceObjects SET GPS_X = @GPS_X, GPS_Y = @GPS_Y, GPS_Z = 0.0 WHERE n_objectid = @n_objectid";
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                con.Close();
                Response.Write("<script>alert('GPS updated');</script>");
                getObject();

            } catch (Exception ex)
            {
                Response.Write("<script>alert('Could not update GPS');</script>");
                throw ex;
            }
        }

        protected void getObject()
        {
            //Temp Vars 
            var nobj = Session["n_objectid"].ToString();

            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];
            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            //Add Paramerters for SQL query
            cmd.Parameters.AddWithValue("@n_objectid", nobj);

            //SQL Query
            cmd.CommandText = "SELECT * FROM dbo.MaintenanceObjects WHERE n_objectid = @n_objectid";
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            cmd.Connection = con;
            try
            {
                con.Open();
                dataAdapter.Fill(dt);
                objectGridView.DataSource = dt;
                objectGridView.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Could not update GPS');</script>");
                throw ex;
            }
        }

        protected void objectGridView_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                var nobj = Session["n_objectid"].ToString();

            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];
            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
            SqlCommand cmd = new SqlCommand();
          
            //Add Paramerters for SQL query
            cmd.Parameters.AddWithValue("@n_objectid", nobj);

            //SQL Query
            cmd.CommandText = "SELECT * FROM dbo.MaintenanceObjects WHERE n_objectid = @n_objectid";
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            cmd.Connection = con;
            try
            {
                con.Open();
                dataAdapter.Fill(dt);
                //reader = cmd.ExecuteReader();
                objectGridView.DataSource = dt;
                objectGridView.DataBind();
                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Could not update GPS');</script>");
                throw ex;
            }
            }
        }

        protected void ObjectcomboSearch_ItemsRequestedByFilterCondition(object sender, ListEditItemsRequestedByFilterConditionEventArgs e)
        {

        }

        protected void ObjectcomboSearch_ItemsRequestedByValue(object sender, ListEditItemRequestedByValueEventArgs e)
        {

        }

    }
}