using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MPETDSFactory;

namespace MPETGO.Pages
{
    public partial class WorkRequestList : System.Web.UI.Page
    {
        private int UserID = 1;
        private LogonObject _oLogon;

        private int MatchJobType = 0;
        private int MatchJobAgainst = 0;
        private string JobIDLike = "";
        private DateTime StartingReqDate = new DateTime(1960, 01, 01, 23, 59, 59);
        private DateTime EndingReqDate = new DateTime(1960, 01, 01, 23, 59, 59);
        private DateTime StartDateStart = new DateTime(1960, 01, 01, 23, 59, 59);
        private DateTime StartDateEnd = new DateTime(1960, 01, 01, 23, 59, 59);
        private string TitleContains = "";
        private string MatchPriority = "";
        private string MatchReason = "";
        private string MatchArea = "";
        private string MatchObjectType = "";
        private string MachineIDContains = "";
        private string ObjIDDescrContains = "";
        private DateTime CompStartDate = new DateTime(1960, 01, 01, 23, 59, 59);
        private DateTime CompEndDate = new DateTime(1960, 01, 01, 23, 59, 59);
        private string MatchLocation = "";
        private DateTime IssuedStartDate = new DateTime(1960, 01, 01, 23, 59, 59);
        private DateTime IssuedEndDate = new DateTime(1960, 01, 01, 23, 59, 59);
        private string RequestedBy = "";
        private string RouteTo = "";
        private string Notes = "";
        private string MiscRef = "";
        private string FundSourceID = "";
        private string WorkOrderCodeID = "";
        private string OrgCodeID = "";
        private string FundGroupID = "";
        private string ControlSectionID = "";
        private string EquipNmberID = "";
        private string HasAttachments = "B";


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

            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];

            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "filter_GetFilteredWorkRequestsList";

            cmd.Parameters.Add("@MatchJobType", SqlDbType.Int).Value = MatchJobType;
            cmd.Parameters.Add("@MatchJobAgainst", SqlDbType.Int).Value = MatchJobAgainst;
            cmd.Parameters.Add("@JobIDLike", SqlDbType.VarChar).Value = JobIDLike;
            cmd.Parameters.Add("@StartingReqDate", SqlDbType.DateTime).SqlValue = StartingReqDate;
            cmd.Parameters.Add("@EndingReqDate", SqlDbType.DateTime).SqlValue = EndingReqDate;
            cmd.Parameters.Add("@TitleContains", SqlDbType.VarChar).SqlValue = TitleContains;           
            cmd.Parameters.Add("@RequestedByMatch", SqlDbType.VarChar).SqlValue = RequestedBy;
            cmd.Parameters.Add("@ReasonCodeMatch", SqlDbType.VarChar).SqlValue = MatchReason;
            cmd.Parameters.Add("@PriorityMatch", SqlDbType.VarChar).SqlValue = MatchPriority;
            cmd.Parameters.Add("@MatchArea", SqlDbType.VarChar).SqlValue = MatchArea;
            cmd.Parameters.Add("@MatchObjectType", SqlDbType.VarChar).SqlValue = MatchObjectType;
            cmd.Parameters.Add("@StateRouteMatch", SqlDbType.VarChar).SqlValue = "";
            cmd.Parameters.Add("@MachineIDContains", SqlDbType.VarChar).SqlValue = MachineIDContains;
            cmd.Parameters.Add("@ObjectDescr", SqlDbType.VarChar).SqlValue = ObjIDDescrContains;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
            cmd.Parameters.Add("@RouteToID", SqlDbType.VarChar).SqlValue = RouteTo;
            cmd.Parameters.Add("@Notes", SqlDbType.VarChar).SqlValue = Notes;
            cmd.Parameters.Add("@MiscRef", SqlDbType.VarChar).SqlValue = MiscRef;
            cmd.Parameters.Add("@ObjLocation", SqlDbType.VarChar).SqlValue = MatchLocation;
            cmd.Parameters.Add("@WorkOpID", SqlDbType.VarChar).SqlValue = "";
            cmd.Parameters.Add("@SubAssembly", SqlDbType.VarChar).SqlValue = "";
            cmd.Parameters.Add("@MilepostStart", SqlDbType.Decimal).SqlValue = 0;
            cmd.Parameters.Add("@MilepostEnd", SqlDbType.Decimal).SqlValue = 0;
            cmd.Parameters.Add("@MilepostDirection", SqlDbType.VarChar).SqlValue = "";
            cmd.Parameters.Add("@ChargeCode", SqlDbType.VarChar).SqlValue = "";
            cmd.Parameters.Add("@FundSource", SqlDbType.VarChar).SqlValue = FundSourceID;
            cmd.Parameters.Add("@WorkOrder", SqlDbType.VarChar).SqlValue = WorkOrderCodeID;
            cmd.Parameters.Add("@OrgCode", SqlDbType.VarChar).SqlValue = OrgCodeID;
            cmd.Parameters.Add("@FundGroup", SqlDbType.VarChar).SqlValue = FundGroupID;
            cmd.Parameters.Add("@ControlSection", SqlDbType.VarChar).SqlValue = ControlSectionID;
            cmd.Parameters.Add("@EquipNum", SqlDbType.VarChar).SqlValue = EquipNmberID;
            cmd.Parameters.Add("@HasAttachments", SqlDbType.VarChar).SqlValue = HasAttachments;
            //cmd.Parameters.Add("@ElementID", SqlDbType.NVarChar).SqlValue = null;
            

            cmd.Connection = con;
            try
            {
                con.Open();
                WorkRequestGrid.DataSource = cmd.ExecuteReader();
                WorkRequestGrid.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }



            

            #endregion
        }

        protected void Page_Init()
        {

        }
    }
}