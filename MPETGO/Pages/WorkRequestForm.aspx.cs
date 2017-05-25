using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Web.Configuration;
using DevExpress.Web;
using MPETDSFactory;
using MPETGO;

namespace MPETGO.Pages
{
    public partial class WorkRequestForm : Page
    {
        private WorkOrder _oJob;
        
        
        private LogonObject _oLogon;
        private JobIdGenerator _oJobIdGenerator;
        private AttachmentObject _oAttachments;
        private MaintAttachmentObject _oObjAttachments;
        public string AssignedGuid = "";
        public string AssignedJobId = "";
        private bool _userCanDelete;
        private bool _userCanAdd;
        private bool _userCanEdit;
        private bool _userCanView;
        private const int AssignedFormId = 3;
        private string _connectionString = "";
        private bool _useWeb;
        private string userFirstName = "";
        private string userLastName = "";
        private int requestorValue = -1;
        private string requestorText = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Check for Logon
            if(Session["LogonInfo"] != null)
            {
                _oLogon = ((LogonObject)Session["logonInfo"]);
                userFirstName = _oLogon.FirstName;
                userLastName = _oLogon.LastName;
            } else
            {
                Response.Redirect("/Logon.aspx");
            }

            #endregion
            #region Inital Page Load
            if (!IsPostBack)
            {
                if (Session["editingJobID"] != null)
                {
                    ResetSession();
                   
                } else
                {
                   
                    txtWorkDescription.Focus();
                }
            } else
            {
                if (!String.IsNullOrEmpty(Request.QueryString["jobid"]))
                {
                    

                    if (Session["editingJobID"] == null)
                    {
                        var jobGuid = "";

                        #region Load Job Info from Guid
                        //Load Job Info & Populate Session Variables
                        if (_oJob.GetJobGuidFromJobID(-1, Request.QueryString["jobid"], ref jobGuid))
                        {
                            var userId = -1;

                            //if (_oLogon != null)
                            //{
                            //    userId = _oLogon.UserID;
                            //}

                            //Load Job Info From GUID
                            if (_oJob.LoadDataByGuid(jobGuid, userId))
                            {
                                //Exepcted Schema
                                //tbl_Jobs.n_Jobid AS 'n_Jobid',
                                //tbl_Jobs.Jobid AS 'Jobid',
                                //tbl_Jobs.Title AS 'Title',
                                //tbl_Jobs.TypeOfJob AS 'TypeOfJob',
                                //tbl_Jobs.AssignedGUID AS 'AssignedGUID',
                                //tbl_Jobs.JobAgainstID AS 'JobAgainstID',
                                //tbl_Jobs.n_MaintObjectID AS 'n_MaintObjectID',
                                //tbl_Jobs.n_AreaObjectID AS 'n_AreaObjectID',
                                //tbl_Jobs.n_LocationObjectID AS 'n_LocationObjectID',
                                //tbl_Jobs.n_GPSObjectID AS 'n_GPSObjectID',
                                //tbl_Jobs.n_ActionPriority AS 'n_ActionPriority',
                                //tbl_Jobs.n_jobreasonid AS 'nJobReasonID' ,
                                //tbl_Jobs.Notes AS 'Notes',
                                //tbl_Jobs.EstimatedJobHours AS 'EstimatedJobHours',
                                //tbl_Jobs.ActualJobHours AS 'ActualJobHours',
                                //tbl_Jobs.IsRequestOnly AS 'IsRequestOnly',
                                //tbl_Jobs.n_RouteTo AS 'n_RouteTo',
                                //tbl_Jobs.RequestDate AS 'RequestDate',
                                //tbl_Jobs.n_requestPriority AS 'n_priorityid' ,
                                //tbl_Jobs.n_requestor AS 'UserID' ,
                                //tbl_Jobs.pmcalc_startdate AS 'pmcalc_startdate',
                                //tbl_Jobs.n_MobileOwner AS 'n_MobileOwner',
                                //tbl_Jobs.MobileDate AS 'MobileDate',
                                //tbl_Jobs.IssuedDate AS 'IssuedDate',
                                //tbl_Jobs.GPS_X AS 'GPS_X',
                                //tbl_Jobs.GPS_Y AS 'GPS_Y',
                                //tbl_Jobs.GPS_Z AS 'GPS_Z',
                                //tbl_Jobs.n_group AS 'n_group',
                                //tbl_Jobs.SentToPDA AS 'SentToPDA',
                                //tbl_Jobs.JobOpen AS 'JobOpen',
                                //tbl_Jobs.IsHistory AS 'IsHistory',
                                //tbl_Jobs.ServicingEquipment AS 'ServicingEquipment',
                                //tbl_Jobs.completed_units1 AS 'completed_units1',
                                //tbl_Jobs.completed_units2 AS 'completed_units2',
                                //tbl_Jobs.completed_units3 AS 'completed_units3',
                                //tbl_Jobs.n_OutcomeID AS 'n_OutcomeID',
                                //tbl_Jobs.n_OwnerID AS 'n_OwnerID',
                                //tbl_Jobs.n_TaskID AS 'n_TaskID',
                                //tbl_Jobs.n_workeventid AS 'n_workeventid',
                                //tbl_Jobs.n_worktypeid AS 'n_WorkOpID' ,
                                //tbl_Jobs.PostedDate AS 'PostedDate',
                                //tbl_Jobs.SubAssemblyID AS 'SubAssemblyID',
                                //tbl_Jobs.n_StateRouteID AS 'n_StateRouteID' ,
                                //tbl_Jobs.Milepost AS 'Milepost' ,
                                //tbl_Jobs.IncreasingMP AS 'n_MilePostDirectionID' ,
                                //tbl_Jobs.b_AdditionalDamage AS 'b_AdditionalDamage',
                                //tbl_Jobs.PercentOverage AS 'PercentOverage',
                                //ISNULL(tbl_IsFlaggedRecord.RecordID, -1) AS 'FlaggedRecordID' ,
                                //tbl_Requestor.Username AS 'Username' ,
                                //tbl_Priorities.priorityid AS 'priorityid' ,
                                //tbl_JobReasons.JobReasonID AS 'JobReasonID' ,
                                //tbl_Owner.Username AS 'OwnerID' ,
                                //tbl_StateRoutes.StateRouteID AS 'StateRouteID' ,
                                //tbl_MPDirections.MilePostDirectionID AS 'MilePostDirectionID' ,
                                //tbl_WorkOP.WorkOpID AS 'WorkOpID' ,
                                //tbl_Jobs.n_CostCodeID AS 'n_CostCodeID' ,
                                //tbl_CostCodes.costcodeid AS 'costcodeid' ,
                                //tbl_CostCodes.SupplementalCode as 'SupplementalCode' ,
                                //tbl_Jobs.n_FundSrcCodeID AS 'n_FundSrcCodeID' ,
                                //tbl_FSC.FundSrcCodeID AS 'FundSrcCodeID' ,
                                //tbl_Jobs.n_WorkOrderCodeID AS 'n_WorkOrderCodeID' ,
                                //tbl_WOCodes.WorkOrderCodeID AS 'WorkOrderCodeID' ,
                                //tbl_Jobs.n_OrganizationCodeID AS 'n_OrganizationCodeID' ,
                                //tbl_OrgCodes.OrganizationCodeID AS 'OrganizationCodeID' ,
                                //tbl_Jobs.n_FundingGroupCodeID AS 'n_FundingGroupCodeID' ,
                                //tbl_FundGroup.FundingGroupCodeID AS 'FundingGroupID' ,
                                //tbl_Jobs.n_ControlSectionID AS 'n_ControlSectionID' ,
                                //tbl_CtlSections.ControlSectionID AS 'ControlSectionID' ,
                                //tbl_Jobs.n_EquipmentNumberID AS 'n_EquipmentNumberID' ,
                                //tbl_EquipNum.EquipmentNumberID AS 'EquipmentNumberID'
                                //tbl_MO.objectid AS 'ObjectID',
                                //tbl_MO.description AS 'ObjectDesc',
                                //tbl_MO.assetnumber AS 'ObjectAsset',
                                //tbl_MO.locationid AS 'ObjectLoc',
                                //tbl_MO.areaid AS 'ObjectArea'

                                #region Setup Job Data

                                //Add Job ID Class
                                HttpContext.Current.Session.Add("oJob", _oJob);

                                //Add Editing Job ID
                                HttpContext.Current.Session.Add("editingJobID",
                                    ((int)_oJob.Ds.Tables[0].Rows[0]["n_Jobid"]));

                                //Add Job String ID
                                HttpContext.Current.Session.Add("AssignedJobID", _oJob.Ds.Tables[0].Rows[0]["Jobid"]);

                                //Add Description
                                HttpContext.Current.Session.Add("txtWorkDescription", _oJob.Ds.Tables[0].Rows[0]["Title"]);

                                //Add Request Date
                                HttpContext.Current.Session.Add("TxtWorkRequestDate",
                                    _oJob.Ds.Tables[0].Rows[0]["RequestDate"]);

                                #endregion

                                #region Setup Object Info

                                HttpContext.Current.Session.Add("ObjectIDCombo",
                                    _oJob.Ds.Tables[0].Rows[0]["n_MaintObjectID"]);
                                HttpContext.Current.Session.Add("ObjectIDComboText", _oJob.Ds.Tables[0].Rows[0]["ObjectID"]);
                                HttpContext.Current.Session.Add("txtObjectDescription",
                                    _oJob.Ds.Tables[0].Rows[0]["ObjectDesc"]);
                                HttpContext.Current.Session.Add("txtObjectArea", _oJob.Ds.Tables[0].Rows[0]["ObjectArea"]);
                                HttpContext.Current.Session.Add("txtObjectLocation", _oJob.Ds.Tables[0].Rows[0]["ObjectLoc"]);
                                HttpContext.Current.Session.Add("txtObjectAssetNumber",
                                    _oJob.Ds.Tables[0].Rows[0]["ObjectAsset"]);


                                #endregion

                                #region Seetup Requestor

                                HttpContext.Current.Session.Add("ComboRequestor", _oJob.Ds.Tables[0].Rows[0]["UserID"]);
                                HttpContext.Current.Session.Add("ComboRequestorText", _oJob.Ds.Tables[0].Rows[0]["Username"]);
                                requestorValue = Convert.ToInt32(HttpContext.Current.Session["ComboRequestor"]);
                                requestorText = HttpContext.Current.Session["ComboRequestorText"].ToString();



                                #endregion

                                #region Setup Priority

                                HttpContext.Current.Session.Add("ComboPriority", _oJob.Ds.Tables[0].Rows[0]["n_priorityid"]);
                                HttpContext.Current.Session.Add("ComboPriorityText",
                                    _oJob.Ds.Tables[0].Rows[0]["priorityid"]);

                                #endregion

                                #region Setup Reason

                                HttpContext.Current.Session.Add("comboReason", _oJob.Ds.Tables[0].Rows[0]["nJobReasonID"]);
                                HttpContext.Current.Session.Add("comboReasonText", _oJob.Ds.Tables[0].Rows[0]["JobReasonID"]);

                                #endregion

                                #region Setup Route To

                                HttpContext.Current.Session.Add("comboRouteTo", _oJob.Ds.Tables[0].Rows[0]["n_RouteTo"]);
                                HttpContext.Current.Session.Add("comboRouteToText", _oJob.Ds.Tables[0].Rows[0]["OwnerID"]);

                                #endregion

                                #region Setup Hwy Route

                                HttpContext.Current.Session.Add("comboHwyRoute",
                                    _oJob.Ds.Tables[0].Rows[0]["n_StateRouteID"]);
                                HttpContext.Current.Session.Add("comboHwyRouteText",
                                    _oJob.Ds.Tables[0].Rows[0]["StateRouteID"]);

                                #endregion

                                #region Setup Milepost

                                HttpContext.Current.Session.Add("txtMilepost", _oJob.Ds.Tables[0].Rows[0]["Milepost"]);
                                HttpContext.Current.Session.Add("txtMilepostTo", _oJob.Ds.Tables[0].Rows[0]["MilepostTo"]);
                                HttpContext.Current.Session.Add("comboMilePostDir",
                                    _oJob.Ds.Tables[0].Rows[0]["n_MilePostDirectionID"]);
                                HttpContext.Current.Session.Add("comboMilePostDirText",
                                    _oJob.Ds.Tables[0].Rows[0]["MilePostDirectionID"]);

                                #endregion

                                #region Setup Cost Code

                                HttpContext.Current.Session.Add("ComboCostCode", _oJob.Ds.Tables[0].Rows[0]["n_CostCodeID"]);
                                HttpContext.Current.Session.Add("ComboCostCodeText",
                                    _oJob.Ds.Tables[0].Rows[0]["costcodeid"]);

                                #endregion

                                #region Setup Fund Source

                                HttpContext.Current.Session.Add("ComboFundSource",
                                    _oJob.Ds.Tables[0].Rows[0]["n_FundSrcCodeID"]);
                                HttpContext.Current.Session.Add("ComboFundSourceText",
                                    _oJob.Ds.Tables[0].Rows[0]["FundSrcCodeID"]);

                                #endregion

                                #region Setup Work Order

                                HttpContext.Current.Session.Add("ComboWorkOrder",
                                    _oJob.Ds.Tables[0].Rows[0]["n_WorkOrderCodeID"]);
                                HttpContext.Current.Session.Add("ComboWorkOrderText",
                                    _oJob.Ds.Tables[0].Rows[0]["WorkOrderCodeID"]);

                                #endregion

                                #region Setup Work Op

                                HttpContext.Current.Session.Add("ComboWorkOp", _oJob.Ds.Tables[0].Rows[0]["n_WorkOpID"]);
                                HttpContext.Current.Session.Add("ComboWorkOpText", _oJob.Ds.Tables[0].Rows[0]["WorkOpID"]);

                                #endregion

                                #region Setup Org Code

                                HttpContext.Current.Session.Add("ComboOrgCode",
                                    _oJob.Ds.Tables[0].Rows[0]["n_OrganizationCodeID"]);
                                HttpContext.Current.Session.Add("ComboOrgCodeText",
                                    _oJob.Ds.Tables[0].Rows[0]["OrganizationCodeID"]);

                                #endregion

                                #region Setup Fund Group

                                HttpContext.Current.Session.Add("ComboFundGroup",
                                    _oJob.Ds.Tables[0].Rows[0]["n_FundingGroupCodeID"]);
                                HttpContext.Current.Session.Add("ComboFundGroupText",
                                    _oJob.Ds.Tables[0].Rows[0]["FundingGroupCodeID"]);

                                #endregion

                                #region Setup Control Section

                                HttpContext.Current.Session.Add("ComboCtlSection",
                                    _oJob.Ds.Tables[0].Rows[0]["n_ControlSectionID"]);
                                HttpContext.Current.Session.Add("ComboCtlSectionText",
                                    _oJob.Ds.Tables[0].Rows[0]["ControlSectionID"]);

                                #endregion

                                #region Setup Equip Num

                                HttpContext.Current.Session.Add("ComboEquipNum",
                                    _oJob.Ds.Tables[0].Rows[0]["n_EquipmentNumberID"]);
                                HttpContext.Current.Session.Add("ComboEquipNumText",
                                    _oJob.Ds.Tables[0].Rows[0]["EquipmentNumberID"]);

                                #endregion
                            }


                        }

                        #endregion
                        #region Get Attachment photo
                        //Load Object Attachments To Get First Photo
                        if (_oObjAttachments.GetAttachments(((int)_oJob.Ds.Tables[0].Rows[0]["n_MaintObjectID"])))
                        {
                            //Check For Table
                            if (_oObjAttachments.Ds.Tables.Count > 0)
                            {
                                //Create Control Flag
                                var firstPicFound = false;

                                //Loop Attachments
                                for (var rowIndex = 0;
                                    // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
                                    rowIndex < _oObjAttachments.Ds.Tables[0].Rows.Count;
                                    rowIndex++)
                                {
                                    //Determine Attachment Type
                                    switch (_oObjAttachments.Ds.Tables[0].Rows[rowIndex][1].ToString().ToUpper())
                                    {
                                        case "GIF":
                                        case "BMP":
                                        case "JPG":
                                            {
                                                //Check For Prior Value
                                                if (HttpContext.Current.Session["ObjectPhoto"] != null)
                                                {
                                                    //Remove Old One
                                                    HttpContext.Current.Session.Remove("ObjectPhoto");
                                                }

                                                //Add New Value
                                                HttpContext.Current.Session.Add("ObjectPhoto",
                                                    _oObjAttachments.Ds.Tables[0].Rows[rowIndex]["LocationOrURL"]
                                                        .ToString());

                                                firstPicFound = true;

                                                //Break
                                                break;
                                            }
                                        default:
                                            {
                                                //Do Nothing
                                                break;
                                            }
                                    }

                                    //Check Control
                                    if (firstPicFound)
                                    {
                                        //Break Loop
                                        break;
                                    }
                                }
                            }
                        }

                        //Refresh Attachments
                        //AttachmentGrid.DataBind();

                        //Enable Tab

                    }
                    else
                    {
                        if (HttpContext.Current.Session["ObjectPhoto"] != null)
                        {
                            //Set Image
                            //objectImg.ImageUrl = HttpContext.Current.Session["ObjectPhoto"].ToString();
                        }
                    }
                }
            
        #endregion

    }
            #endregion

            #region If it is a Post Back But not Inital Page Load
            if (!IsPostBack)
            {
                //Check For Previous Session Variables
                if (HttpContext.Current.Session["txtWorkDescription"] != null)
                {
                    //Get Additional Info From Session
                    txtWorkDescription.Text = (HttpContext.Current.Session["txtWorkDescription"].ToString());
                }

                //Job ID
                if (HttpContext.Current.Session["AssignedJobID"] != null)
                {
                    //Get Additional Info From Session
                 ////   lblHeader.Text = (HttpContext.Current.Session["AssignedJobID"].ToString());
                }
                //Check For Previous Session Variables
                if (HttpContext.Current.Session["ObjectIDCombo"] != null)
                {
                    //Get Info From Session
                    ObjectIDCombo.Value = Convert.ToInt32((HttpContext.Current.Session["ObjectIDCombo"].ToString()));
                }

                //This would be code coming from the map session
                if (HttpContext.Current.Session["nobjectid"] != null)
                {
                    var txtObject = HttpContext.Current.Session["objectDescription"].ToString();
                    ObjectIDCombo.Value = Convert.ToInt32(HttpContext.Current.Session["nobjectId"]).ToString();

                    HttpContext.Current.Session.Add("txtObjectDescription", txtObject);
                    txtObjectDescription.Value = txtObject;

                }

                //Check For Previous Session Variables
                if (HttpContext.Current.Session["ObjectIDComboText"] != null)
                {
                    //Get Info From Session
                    ObjectIDCombo.Text = (HttpContext.Current.Session["ObjectIDComboText"].ToString());
                }

                //Check For Previous Session Variables
                if (HttpContext.Current.Session["txtObjectDescription"] != null)
                {
                    //Get Info From Session
                    txtObjectDescription.Text = (HttpContext.Current.Session["txtObjectDescription"].ToString());
                }

                //Check For Previous Session Variables
                if ((HttpContext.Current.Session["ComboPriority"] != null) &&
                    (HttpContext.Current.Session["ComboPriorityText"] != null))
                {
                    //Get Info From Session
                    ComboPriority.Value = Convert.ToInt32((HttpContext.Current.Session["ComboPriority"].ToString()));
                    ComboPriority.Text = (HttpContext.Current.Session["ComboPriorityText"].ToString());
                }

                //Check For Previous Session Variables
                if ((HttpContext.Current.Session["comboReason"] != null) &&
                    (HttpContext.Current.Session["comboReasonText"] != null))
                {
                    //Get Info From Session
                    ComboReason.Value = Convert.ToInt32((HttpContext.Current.Session["comboReason"].ToString()));
                    ComboReason.Text = (HttpContext.Current.Session["comboReasonText"].ToString());
                }

                //Check For Previous Session Variables
                if (requestorValue > -1 && requestorText != null)
                {
                    ComboRequestor.Value = requestorValue;
                    ComboRequestor.Text = requestorText;

                    HttpContext.Current.Session.Add("ComboRequestor", requestorValue);
                    HttpContext.Current.Session.Add("ComboRequestorText", requestorText);
                }
                else if (HttpContext.Current.Session["LogonInfo"] != null)
                {
                    _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);

                    //Set Requestor
                    ComboRequestor.Value = _oLogon.UserID;
                    ComboRequestor.Text = _oLogon.Username + "-" + _oLogon.FullName;

                    HttpContext.Current.Session.Add("ComboRequestor", _oLogon.UserID);
                    HttpContext.Current.Session.Add("ComboRequestorText", _oLogon.Username);
                }

                //Check For Prior Value
                if (HttpContext.Current.Session["ObjectPhoto"] != null)
                {
                    //Set Image
                    ////objectImg.ImageUrl = HttpContext.Current.Session["ObjectPhoto"].ToString();
                }
                ////AttachmentGrid.DataBind();
            }

        
        #endregion
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //Set Connection Info
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            _useWeb = (ConfigurationManager.AppSettings["UsingWebService"] == "Y");

            //Initialize Classes
            _oJob = new WorkOrder(_connectionString, _useWeb);
            _oAttachments = new AttachmentObject(_connectionString, _useWeb);
            _oObjAttachments = new MaintAttachmentObject(_connectionString, _useWeb);

            //Set Datasources

            ObjectDataSource.ConnectionString = _connectionString;
            PrioritySqlDatasource.ConnectionString = _connectionString;
            ReasonSqlDatasource.ConnectionString = _connectionString;
            RequestorSqlDatasource.ConnectionString = _connectionString;


            //Setup Fields
            if (Session["editingJobID"] != null)
            {
                submitBtn.Visible = false;
                

                
            } else
            {
                saveBtn.Visible = false;
               
            }

        }

        #region Combo Loading Events



        protected void ComboPriority_OnItemsRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            PrioritySqlDatasource.SelectCommand =
                @"SELECT  [n_priorityid] ,
                            [priorityid] ,
                            [description]
                    FROM    ( SELECT    tblPriority.[n_priorityid] ,
                                        tblPriority.[priorityid] ,
                                        tblPriority.[description] ,
                                        ROW_NUMBER() OVER ( ORDER BY tblPriority.[n_priorityid] ) AS [rn]
                              FROM      dbo.Priorities AS tblPriority
                              WHERE     ( ( [priorityid] + ' ' + [description] ) LIKE @filter )
                                        AND tblPriority.Active = 'Y'
                                        AND tblPriority.n_priorityid > 0
                            ) AS st
                    WHERE   st.[rn] BETWEEN @startIndex AND @endIndex";

            PrioritySqlDatasource.SelectParameters.Clear();
            PrioritySqlDatasource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            PrioritySqlDatasource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString(CultureInfo.InvariantCulture));
            PrioritySqlDatasource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString(CultureInfo.InvariantCulture));
            comboBox.DataSource = PrioritySqlDatasource;
            comboBox.DataBind();

        }

        protected void ComboPriority_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            var comboBox = (ASPxComboBox)source;
            PrioritySqlDatasource.SelectCommand = @"SELECT  tblPriority.[n_priorityid] ,
                                                            tblPriority.[priorityid] ,
                                                            tblPriority.[description] ,
                                                            ROW_NUMBER() OVER ( ORDER BY tblPriority.[n_priorityid] ) AS [rn]
                                                    FROM    dbo.Priorities AS tblPriority
                                                    WHERE   ( n_priorityid = @ID )
                                                    ORDER BY priorityid";

            PrioritySqlDatasource.SelectParameters.Clear();
            PrioritySqlDatasource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
            comboBox.DataSource = PrioritySqlDatasource;
            comboBox.DataBind();
        }

        protected void comboReason_OnItemsRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            ReasonSqlDatasource.SelectCommand =
                @"SELECT  [n_reasonid] ,
                            [reasonid] ,
                            [description]
                    FROM    ( SELECT    tblReasons.nJobReasonID AS 'n_reasonid' ,
                                        tblReasons.JobReasonID AS 'reasonid' ,
                                        tblReasons.Description AS 'description' ,
                                        ROW_NUMBER() OVER ( ORDER BY tblReasons.[nJobReasonID] ) AS [rn]
                              FROM      dbo.JobReasons AS tblReasons
                              WHERE     ( ( [JobReasonID] + ' ' + [description] ) LIKE @filter )
                                        AND tblReasons.b_IsActive = 'Y'
                                        AND tblReasons.nJobReasonID > 0
                            ) AS st
                    WHERE   st.[rn] BETWEEN @startIndex AND @endIndex";

            ReasonSqlDatasource.SelectParameters.Clear();
            ReasonSqlDatasource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            ReasonSqlDatasource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString(CultureInfo.InvariantCulture));
            ReasonSqlDatasource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString(CultureInfo.InvariantCulture));
            comboBox.DataSource = ReasonSqlDatasource;
            comboBox.DataBind();
        }

        protected void comboReason_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            var comboBox = (ASPxComboBox)source;
            ReasonSqlDatasource.SelectCommand = @"SELECT  tblReason.nJobReasonID AS 'n_reasonid' ,
                                                        tblReason.JobReasonID AS 'reasonid' ,
                                                        tblReason.[description] ,
                                                        ROW_NUMBER() OVER ( ORDER BY tblReason.nJobReasonID ) AS [rn]
                                                FROM    dbo.JobReasons AS tblReason
                                                WHERE   ( nJobReasonID = @ID )
                                                ORDER BY JobReasonID";

            ReasonSqlDatasource.SelectParameters.Clear();
            ReasonSqlDatasource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
            comboBox.DataSource = ReasonSqlDatasource;
            comboBox.DataBind();
        }
        protected void ASPxComboBox_OnItemsRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {

            //Get Requestor
            var requestor = -1;
            if ((HttpContext.Current.Session["LogonInfo"] != null))
            {
                //Get Info From Session
                _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);

                requestor = _oLogon.UserID;
            }

            var comboBox = (ASPxComboBox)source;
            ObjectDataSource.SelectCommand =
                @"DECLARE @areaFilteringOn VARCHAR(1)
                --Setup Area Filering Variable
                IF ( ( SELECT   COUNT(dbo.UsersAreaFilter.RecordID)
                       FROM     dbo.UsersAreaFilter WITH ( NOLOCK )
                       WHERE    UsersAreaFilter.UserID = " + requestor + @"
                                AND UsersAreaFilter.FilterActive = 'Y'
                     ) <> 0 )
                    BEGIN
                        SET @areaFilteringOn = 'Y'
                    END
                ELSE
                    BEGIN
                        SET @areaFilteringOn = 'N'
                    END

                SELECT  [n_objectid] ,
                        [objectid] ,
                        [description] ,
                        [areaid] ,
                        [locationid] ,
                        [assetnumber] ,
                        CASE ISNULL(RecordID, -1)
                          WHEN -1 THEN 'N'
                          ELSE 'Y'
                        END AS [Following] ,
                        ISNULL(LocationOrURL, '') AS LocationOrURL ,
                        OrganizationCodeID ,
                        FundingGroupCodeID
                FROM    ( SELECT    tblmo.[n_objectid] ,
                                    tblmo.[objectid] ,
                                    tblmo.[description] ,
                                    tblarea.[areaid] ,
                                    tbllocation.[locationid] ,
                                    tblmo.[assetnumber] ,
                                    ROW_NUMBER() OVER ( ORDER BY tblmo.[n_objectid] ) AS [rn] ,
                                    tbl_IsFlaggedRecord.RecordID ,
                                    tblFirstPhoto.LocationOrURL ,
                                    tbl_OrgCode.OrganizationCodeID ,
                                    tbl_FGC.FundingGroupCodeID
                          FROM      dbo.MaintenanceObjects AS tblmo
                                    INNER JOIN ( SELECT tbl_Area.n_areaid ,
                                                        tbl_Area.areaid
                                                 FROM   dbo.Areas tbl_Area
                                                 WHERE  ( ( @areaFilteringOn = 'Y'
                                                            AND EXISTS ( SELECT recordMatches.AreaFilterID
                                                                         FROM   dbo.UsersAreaFilter AS recordMatches
                                                                         WHERE  tbl_Area.n_areaid = recordMatches.AreaFilterID
                                                                                AND recordMatches.UserID = " + requestor + @"
                                                                                AND recordMatches.FilterActive = 'Y' )
                                                          )
                                                          OR ( @areaFilteringOn = 'N' )
                                                        )
                                               ) tblarea ON tblmo.n_areaid = tblarea.n_areaid
                                    INNER JOIN ( SELECT n_locationid ,
                                                        locationid
                                                 FROM   dbo.locations
                                               ) tbllocation ON tblmo.n_locationid = tbllocation.n_locationid
                                    INNER JOIN ( SELECT dbo.OrganizationCodes.n_OrganizationCodeID ,
                                                        dbo.OrganizationCodes.OrganizationCodeID
                                                 FROM   dbo.OrganizationCodes
                                               ) tbl_OrgCode ON tbl_OrgCode.n_OrganizationCodeID = tblmo.n_OrganizationCodeID
                                    INNER JOIN ( SELECT dbo.FundingGroupCodes.n_FundingGroupCodeID ,
                                                        dbo.FundingGroupCodes.FundingGroupCodeID
                                                 FROM   dbo.FundingGroupCodes
                                               ) tbl_FGC ON tbl_FGC.n_FundingGroupCodeID = tblmo.n_FundingGroupCodeID
                                    LEFT JOIN ( SELECT  dbo.UsersFlaggedRecords.RecordID ,
                                                        dbo.UsersFlaggedRecords.n_objectid
                                                FROM    dbo.UsersFlaggedRecords
                                                WHERE   dbo.UsersFlaggedRecords.UserID = " + requestor + @"
                                                        AND dbo.UsersFlaggedRecords.n_objectid > 0
                                              ) tbl_IsFlaggedRecord ON tblmo.n_objectid = tbl_IsFlaggedRecord.n_objectid
							                  OUTER apply ( SELECT TOP 1  tblAttach.LocationOrURL ,
                                                        tblAttach.n_MaintObjectID
                                                FROM    dbo.Attachments tblAttach
								                WHERE tblAttach.n_MaintObjectID = tblmo.n_objectid 
                                              ) tblFirstPhoto 
                          WHERE     ( ( [objectid] + ' ' + [description] + ' ' + [areaid] + ' ' + [locationid] + ' ' + [assetnumber] + ' ' + [OrganizationCodeID] + ' ' + [FundingGroupCodeID] ) LIKE @filter )
                                    AND tblmo.b_active = 'Y'
                                    AND tblmo.n_objectid > 0
                        ) AS st
		                WHERE   st.[rn] BETWEEN @startIndex AND @endIndex";

            ObjectDataSource.SelectParameters.Clear();
            ObjectDataSource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            ObjectDataSource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString(CultureInfo.InvariantCulture));
            ObjectDataSource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString(CultureInfo.InvariantCulture));
            comboBox.DataSource = ObjectDataSource;
            comboBox.DataBind();

        }

        protected void ASPxComboBox_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {

            //Get Requestor
            var requestor = -1;
            if ((HttpContext.Current.Session["LogonInfo"] != null))
            {
                //Get Info From Session
                _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);
                requestor = _oLogon.UserID;
            }


            long value;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            var comboBox = (ASPxComboBox)source;
            ObjectDataSource.SelectCommand = @"SELECT    tblmo.[n_objectid] ,
                                                            tblmo.[objectid] ,
                                                            tblmo.[description] ,
                                                            tblarea.[areaid] ,
					                                        tbllocation.[locationid] ,
					                                        tblmo.[assetnumber] ,
                                                            ROW_NUMBER() OVER ( ORDER BY tblmo.[n_objectid] ) AS [rn],
                                                            CASE ISNULL(RecordID, -1)
                                                                                      WHEN -1 THEN 'N'
                                                                                      ELSE 'Y'
                                                                                    END AS [Following],
                                                            isnull(LocationOrURL, '') AS LocationOrURL,
								                            tbl_OrgCode.OrganizationCodeID,
								                            tbl_FGC.FundingGroupCodeID
                                                  FROM      dbo.MaintenanceObjects AS tblmo
			                                        JOIN ( SELECT   n_areaid ,
							                                        areaid
				                                           FROM     dbo.Areas
				                                         ) tblarea ON tblmo.n_areaid = tblarea.n_areaid
			                                        JOIN ( SELECT   n_locationid ,
							                                        locationid
				                                           FROM     dbo.locations
				                                         ) tbllocation ON tblmo.n_locationid = tbllocation.n_locationid
									                INNER JOIN (SELECT dbo.OrganizationCodes.n_OrganizationCodeID,
													                   dbo.OrganizationCodes.OrganizationCodeID
												                FROM dbo.OrganizationCodes) tbl_OrgCode ON tbl_OrgCode.n_OrganizationCodeID = tblmo.n_OrganizationCodeID
									                INNER JOIN (SELECT dbo.FundingGroupCodes.n_FundingGroupCodeID,
													                   dbo.FundingGroupCodes.FundingGroupCodeID
													                   FROM dbo.FundingGroupCodes) tbl_FGC ON tbl_FGC.n_FundingGroupCodeID = tblmo.n_FundingGroupCodeID
                                                    LEFT JOIN ( SELECT  dbo.UsersFlaggedRecords.RecordID ,
                                                            dbo.UsersFlaggedRecords.n_objectid
                                                    FROM    dbo.UsersFlaggedRecords
                                                    WHERE   dbo.UsersFlaggedRecords.UserID = " + requestor + @"
                                                            AND dbo.UsersFlaggedRecords.n_objectid > 0
                                                  ) tbl_IsFlaggedRecord ON tblmo.n_objectid = tbl_IsFlaggedRecord.n_objectid
                                                OUTER APPLY ( SELECT TOP 1
                                                            tblAttach.LocationOrURL ,
                                                            tblAttach.n_MaintObjectID
                                                  FROM      dbo.Attachments tblAttach
                                                  WHERE     tblAttach.n_MaintObjectID = tblmo.n_objectid
                                                ) tblFirstPhoto                                                   
                                        WHERE (tblmo.n_objectid = @ID) ORDER BY objectid";

            ObjectDataSource.SelectParameters.Clear();
            ObjectDataSource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
            comboBox.DataSource = ObjectDataSource;
            if (HttpContext.Current.Session["nobjectid"] != null)
            {
                txtObjectDescription.Text = HttpContext.Current.Session["objectDescription"].ToString();
            }
            else
            {
                txtObjectDescription.Text = comboBox.TextField[1].ToString();

            }
            comboBox.DataBind();
        }

        protected void ComboRequestor_OnItemsRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            RequestorSqlDatasource.SelectCommand =
                @"SELECT  [UserID] ,
                            [username] ,
                            [FullName] 
                    FROM    ( SELECT    tblUsers.[UserID] ,
                                        tblUsers.[Username] ,
                                        tblUsers.[lastname] + ', ' + tblUsers.[firstname] AS 'FullName' ,
                                        ROW_NUMBER() OVER ( ORDER BY tblUsers.[UserID] ) AS [rn]
                              FROM      dbo.MPetUsers AS tblUsers
                              WHERE     ( ( [Username] + ' ' + [firstname] + ' ' + [lastname] ) LIKE @filter )
                                        AND tblUsers.Active = 1
                                        AND tblUsers.UserID > 0
                            ) AS st
                    WHERE   st.[rn] BETWEEN @startIndex AND @endIndex";

            RequestorSqlDatasource.SelectParameters.Clear();
            RequestorSqlDatasource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            RequestorSqlDatasource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString(CultureInfo.InvariantCulture));
            RequestorSqlDatasource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString(CultureInfo.InvariantCulture));
            comboBox.DataSource = RequestorSqlDatasource;
            if (requestorValue > 0)
            { }
            else
            {
                comboBox.Value = _oLogon.UserID;
                comboBox.Text = _oLogon.Username;
            }

            comboBox.DataBind();


        }


        protected void ComboRequestor_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {

            long value;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            var comboBox = (ASPxComboBox)source;
            RequestorSqlDatasource.SelectCommand = @"SELECT  tblUsers.[UserID] ,
                                                        tblUsers.[Username] ,
                                                        tblUsers.[lastname] + ', ' + tblUsers.[firstname] AS 'FullName' ,
                                                        ROW_NUMBER() OVER ( ORDER BY tblUsers.[UserID] ) AS [rn]
                                                FROM    dbo.MPetUsers AS tblUsers
                                                WHERE   ( UserID = @ID )
                                                ORDER BY Username";

            RequestorSqlDatasource.SelectParameters.Clear();
            RequestorSqlDatasource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
            comboBox.DataSource = RequestorSqlDatasource;
            comboBox.DataBind();
        }



        #endregion

        protected bool AddRequest()
        {
            //Set Defaults
            const int actionPriority = -1;
            const int mobileEquip = -1;
            const bool additionalDamage = false;
            const decimal percentOverage = 0;
            const int subAssemblyId = -1;
            var errorFromJobIdGeneration = "";
            var poolTypeForJob = JobPoolType.Global;

            //Get Requestor
            var requestor = -1;
            if ((HttpContext.Current.Session["ComboRequestor"] != null))
            {
                //Get Info From Session
                requestor = Convert.ToInt32((HttpContext.Current.Session["ComboRequestor"].ToString()));
            }
            else if (HttpContext.Current.Session["LogonInfo"] != null)
            {
                //Get Logon Info
               _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);

                ////Set Requestor
               requestor = _oLogon.UserID;
            }

            //Get Object ID
            var objectAgainstId = -1;
            if (HttpContext.Current.Session["ObjectIDCombo"] != null)
            {
                //Get Info From Session
                objectAgainstId = Convert.ToInt32((HttpContext.Current.Session["ObjectIDCombo"].ToString()));
            }

            //Get Description
            var workDesc = "";
            if (HttpContext.Current.Session["txtWorkDescription"] != null)
            {
                //Get Additional Info From Session
                workDesc = (HttpContext.Current.Session["txtWorkDescription"].ToString());
            }

            //Get Work Date
            var requestDate = DateTime.Now;
            if (HttpContext.Current.Session["TxtWorkRequestDate"] != null)
            {
                //Get Info From Session
                requestDate = Convert.ToDateTime(HttpContext.Current.Session["TxtWorkRequestDate"].ToString());
            }

            //Get Priority
            var requestPriority = -1;
            if ((HttpContext.Current.Session["ComboPriority"] != null))
            {
                //Get Info From Session
                requestPriority = Convert.ToInt32((HttpContext.Current.Session["ComboPriority"].ToString()));
            }

            //Get State Route
            var stateRouteId = -1;
            if ((HttpContext.Current.Session["comboHwyRoute"] != null))
            {
                //Get Info From Session
                stateRouteId = Convert.ToInt32((HttpContext.Current.Session["comboHwyRoute"].ToString()));
            }

            //Get Milepost
            decimal milepost = 0;
            if (HttpContext.Current.Session["txtMilepost"] != null)
            {
                //Get Info From Session
                milepost = Convert.ToDecimal(HttpContext.Current.Session["txtMilepost"].ToString());
            }

            //Get Milepost To
            decimal milepostTo = 0;
            if (HttpContext.Current.Session["txtMilepostTo"] != null)
            {
                //Get Info From Session
                milepostTo = Convert.ToDecimal(HttpContext.Current.Session["txtMilepostTo"].ToString());
            }

            //Get Milepost Direction
            var mpIncreasing = -1;
            if ((HttpContext.Current.Session["comboMilePostDir"] != null))
            {
                //Get Info From Session
                mpIncreasing = Convert.ToInt32((HttpContext.Current.Session["comboMilePostDir"].ToString()));
            }

            //Get Work Op
            var workOp = -1;
            if ((HttpContext.Current.Session["ComboWorkOp"] != null))
            {
                //Get Info From Session
                workOp = Convert.ToInt32((HttpContext.Current.Session["ComboWorkOp"].ToString()));
            }

            //Get Job Reason
            var reasonCode = -1;
            if ((HttpContext.Current.Session["comboReason"] != null))
            {
                //Get Info From Session
                reasonCode = Convert.ToInt32((HttpContext.Current.Session["comboReason"].ToString()));
            }

            //Get Route To
            var routeTo = -1;
            if ((HttpContext.Current.Session["comboRouteTo"] != null))
            {
                //Get Info From Session
                routeTo = Convert.ToInt32((HttpContext.Current.Session["comboRouteTo"].ToString()));
            }

            //Get Cost Code
            var costCodeId = -1;
            if ((HttpContext.Current.Session["ComboCostCode"] != null))
            {
                //Get Info From Session
                costCodeId = Convert.ToInt32((HttpContext.Current.Session["ComboCostCode"].ToString()));
            }

            //Get Fund Source
            var fundSource = -1;
            if ((HttpContext.Current.Session["ComboFundSource"] != null))
            {
                //Get Info From Session
                fundSource = Convert.ToInt32((HttpContext.Current.Session["ComboFundSource"].ToString()));
            }

            //Get Work Order
            var workOrder = -1;
            if ((HttpContext.Current.Session["ComboWorkOrder"] != null))
            {
                //Get Info From Session
                workOrder = Convert.ToInt32((HttpContext.Current.Session["ComboWorkOrder"].ToString()));
            }

            //Get Org Code
            var orgCode = -1;
            if ((HttpContext.Current.Session["ComboOrgCode"] != null))
            {
                //Get Info From Session
                orgCode = Convert.ToInt32((HttpContext.Current.Session["ComboOrgCode"].ToString()));
            }

            //Get Fund Group
            var fundingGroup = -1;
            if ((HttpContext.Current.Session["ComboFundGroup"] != null))
            {
                //Get Info From Session
                fundingGroup = Convert.ToInt32((HttpContext.Current.Session["ComboFundGroup"].ToString()));
            }

            //Get Equip Num
            var equipNumber = -1;
            if ((HttpContext.Current.Session["ComboEquipNum"] != null))
            {
                //Get Info From Session
                equipNumber = Convert.ToInt32((HttpContext.Current.Session["ComboEquipNum"].ToString()));
            }

            //Get Ctl Section
            var controlSection = -1;
            if ((HttpContext.Current.Session["ComboCtlSection"] != null))
            {
                //Get Info From Session
                controlSection = Convert.ToInt32((HttpContext.Current.Session["ComboCtlSection"].ToString()));
            }

            //Get Notes
            var notes = "";
            if (HttpContext.Current.Session["txtAddDetail"] != null)
            {
                //Get Additional Info From Session
                notes = (HttpContext.Current.Session["txtAddDetail"].ToString());
            }

            //Get GPS X
            decimal gpsX = 0;
            if (HttpContext.Current.Session["GPSX"] != null)
            {
                //Get Info From Session
                gpsX = Convert.ToDecimal((HttpContext.Current.Session["GPSX"].ToString()));
            }

            //Get GPS Y
            decimal gpsY = 0;
            if (HttpContext.Current.Session["GPSY"] != null)
            {
                //Get Info From Session
                gpsY = Convert.ToDecimal((HttpContext.Current.Session["GPSY"].ToString()));
            }

            //Get GPS Z
            decimal gpsZ = 0;
            if (HttpContext.Current.Session["GPSZ"] != null)
            {
                //Get Info From Session
                gpsZ = Convert.ToDecimal((HttpContext.Current.Session["GPSZ"].ToString()));
            }

            //Clear Errors
            _oJob.ClearErrors();

            try
            {
                if (HttpContext.Current.Session["LogonInfo"] != null)
                {
                    //Get Logon Info From Session
                    _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);


                    //Setup Job ID Generator For Logon
                    _oJobIdGenerator =
                        new JobIdGenerator(_connectionString, _useWeb, _oLogon.UserID, _oLogon.AreaID);
                }
                else
                {
                    //Setup For Non Logged In User
                    _oJobIdGenerator =
                        new JobIdGenerator(_connectionString, _useWeb, -1, -1);
                }

                //Clear Errors
                _oJobIdGenerator.ClearErrors();

                //Get Pool
                if (!_oJobIdGenerator.GetJobPoolInUse(ref poolTypeForJob))
                {
                    //Return False
                    return false;
                }

                //Add Job
                if (_oJob.Add(true,
                    JobType.Corrective,
                    JobAgainstType.MaintenanceObjects,
                    objectAgainstId,
                    workDesc,
                    actionPriority,
                    reasonCode,
                    notes,
                    0, 0, true,
                    -1,
                    requestDate,
                    requestPriority,
                    requestor,
                    _oJob.NullDate,
                    gpsX, gpsY, gpsZ,
                    -1,
                    mobileEquip,
                    0, 0, 0, -1,
                    routeTo,
                    -1, -1,
                    workOp,
                    subAssemblyId,
                    stateRouteId,
                    milepost,
                    milepostTo,
                    mpIncreasing,
                    additionalDamage,
                    percentOverage,
                    ref AssignedJobId,
                    ref errorFromJobIdGeneration,
                    ref AssignedGuid,
                    requestor))
                {
                    //Add Job To Session
                    HttpContext.Current.Session.Add("oJob", _oJob);
                    HttpContext.Current.Session.Add("editingJobID", _oJob.RecordID);
                    HttpContext.Current.Session.Add("AssignedJobID", AssignedJobId);

                    //Refresh Attachments
               ////     AttachmentGrid.DataBind();

                    //Set Text
               ////     lblHeader.Text = (HttpContext.Current.Session["AssignedJobID"].ToString());

                    //Update Costing Information
                    if (!_oJob.UpdateJobCosting(_oJob.RecordID,
                        costCodeId,
                        fundSource,
                        workOrder,
                        workOp,
                        orgCode,
                        fundingGroup,
                        equipNumber,
                        controlSection,
                        _oLogon.UserID))
                    {
                        //Return False To Prevent Navigation
                        return false;
                    }

                    //Setup For Editing
                  ////  SetupForEditing();

                    //Return True
                    return true;

                }

                //Return False To Prevent Navigation
                return false;
            }
            catch (Exception ex)
            {
                //Show Error
          ////      Master.ShowError(ex.Message);

                //Return False To Prevent Navigation
                return false;
            }
        }

        protected bool UpdateRequest()
        {
            const int actionPriority = -1;
            const int mobileEquip = -1;
            const int subAssemblyId = -1;
            const bool additionalDamage = false;
            const decimal percentOverage = 0;

            //Get Logon Info
            if (HttpContext.Current.Session["LogonInfo"] != null)
            {
                //Get Logon Info From Session
                _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);
            }


            var objectAgainstId = -1;
            if (HttpContext.Current.Session["ObjectIDCombo"] != null)
            {
                //Get Info From Session
                objectAgainstId = Convert.ToInt32((HttpContext.Current.Session["ObjectIDCombo"].ToString()));
            }

            //Get GPS X
            decimal gpsX = 0;
            if (HttpContext.Current.Session["GPSX"] != null)
            {
                //Get Info From Session
                gpsX = Convert.ToDecimal((HttpContext.Current.Session["GPSX"].ToString()));
            }

            //Get GPS Y
            decimal gpsY = 0;
            if (HttpContext.Current.Session["GPSY"] != null)
            {
                //Get Info From Session
                gpsY = Convert.ToDecimal((HttpContext.Current.Session["GPSY"].ToString()));
            }

            //Get GPS Z
            decimal gpsZ = 0;
            if (HttpContext.Current.Session["GPSZ"] != null)
            {
                //Get Info From Session
                gpsZ = Convert.ToDecimal((HttpContext.Current.Session["GPSZ"].ToString()));
            }

            //Get Description
            var workDesc = "";
            if (HttpContext.Current.Session["txtWorkDescription"] != null)
            {
                //Get Additional Info From Session
                workDesc = (HttpContext.Current.Session["txtWorkDescription"].ToString());
            }

            //Get Work Date
            var requestDate = DateTime.Now;
            if (HttpContext.Current.Session["TxtWorkRequestDate"] != null)
            {
                //Get Info From Session
                requestDate = Convert.ToDateTime(HttpContext.Current.Session["TxtWorkRequestDate"].ToString());
            }

            //Get Priority
            var requestPriority = -1;
            if ((HttpContext.Current.Session["ComboPriority"] != null))
            {
                //Get Info From Session
                requestPriority = Convert.ToInt32((HttpContext.Current.Session["ComboPriority"].ToString()));
            }

            //Get State Route
            var stateRouteId = -1;
            if ((HttpContext.Current.Session["comboHwyRoute"] != null))
            {
                //Get Info From Session
                stateRouteId = Convert.ToInt32((HttpContext.Current.Session["comboHwyRoute"].ToString()));
            }

            //Get Milepost
            decimal milepost = 0;
            if (HttpContext.Current.Session["txtMilepost"] != null)
            {
                //Get Info From Session
                milepost = Convert.ToDecimal(HttpContext.Current.Session["txtMilepost"].ToString());
            }

            //Get Milepost To
            decimal milepostTo = 0;
            if (HttpContext.Current.Session["txtMilepostTo"] != null)
            {
                //Get Info From Session
                milepostTo = Convert.ToDecimal(HttpContext.Current.Session["txtMilepostTo"].ToString());
            }

            //Get Milepost Direction
            var mpIncreasing = -1;
            if ((HttpContext.Current.Session["comboMilePostDir"] != null))
            {
                //Get Info From Session
                mpIncreasing = Convert.ToInt32((HttpContext.Current.Session["comboMilePostDir"].ToString()));
            }

            //Get Work Op
            var workOp = -1;
            if ((HttpContext.Current.Session["ComboWorkOp"] != null))
            {
                //Get Info From Session
                workOp = Convert.ToInt32((HttpContext.Current.Session["ComboWorkOp"].ToString()));
            }

            //Get Requestor
            var requestor = -1;
            if ((HttpContext.Current.Session["ComboRequestor"] != null))
            {
                //Get Info From Session
                requestor = Convert.ToInt32((HttpContext.Current.Session["ComboRequestor"].ToString()));
            }
            else if (HttpContext.Current.Session["LogonInfo"] != null)
            {
                //Get Logon Info
                _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);

                //Set Requestor
                requestor = _oLogon.UserID;
            }

            //Get Job Reason
            var reasonCode = -1;
            if ((HttpContext.Current.Session["comboReason"] != null))
            {
                //Get Info From Session
                reasonCode = Convert.ToInt32((HttpContext.Current.Session["comboReason"].ToString()));
            }

            //Get Route To
            var routeTo = -1;
            if ((HttpContext.Current.Session["comboRouteTo"] != null))
            {
                //Get Info From Session
                routeTo = Convert.ToInt32((HttpContext.Current.Session["comboRouteTo"].ToString()));
            }

            //Get Cost Code
            var costCodeId = -1;
            if ((HttpContext.Current.Session["ComboCostCode"] != null))
            {
                //Get Info From Session
                costCodeId = Convert.ToInt32((HttpContext.Current.Session["ComboCostCode"].ToString()));
            }

            //Get Fund Source
            var fundSource = -1;
            if ((HttpContext.Current.Session["ComboFundSource"] != null))
            {
                //Get Info From Session
                fundSource = Convert.ToInt32((HttpContext.Current.Session["ComboFundSource"].ToString()));
            }

            //Get Work Order
            var workOrder = -1;
            if ((HttpContext.Current.Session["ComboWorkOrder"] != null))
            {
                //Get Info From Session
                workOrder = Convert.ToInt32((HttpContext.Current.Session["ComboWorkOrder"].ToString()));
            }

            //Get Org Code
            var orgCode = -1;
            if ((HttpContext.Current.Session["ComboOrgCode"] != null))
            {
                //Get Info From Session
                orgCode = Convert.ToInt32((HttpContext.Current.Session["ComboOrgCode"].ToString()));
            }

            //Get Fund Group
            var fundingGroup = -1;
            if ((HttpContext.Current.Session["ComboFundGroup"] != null))
            {
                //Get Info From Session
                fundingGroup = Convert.ToInt32((HttpContext.Current.Session["ComboFundGroup"].ToString()));
            }

            //Get Equip Num
            var equipNumber = -1;
            if ((HttpContext.Current.Session["ComboEquipNum"] != null))
            {
                //Get Info From Session
                equipNumber = Convert.ToInt32((HttpContext.Current.Session["ComboEquipNum"].ToString()));
            }

            //Get Ctl Section
            var controlSection = -1;
            if ((HttpContext.Current.Session["ComboCtlSection"] != null))
            {
                //Get Info From Session
                controlSection = Convert.ToInt32((HttpContext.Current.Session["ComboCtlSection"].ToString()));
            }

            //Get Notes
            var notes = "";
            if (HttpContext.Current.Session["txtAddDetail"] != null)
            {
                //Get Additional Info From Session
                notes = (HttpContext.Current.Session["txtAddDetail"].ToString());
            }

            var AssignedJobId = "";
              if ( Session["AssignedJobID"] != null)
            {
                AssignedJobId = Session["AssignedJobID"].ToString();
            }
            //Clear Errors
            _oJob.ClearErrors();

            try
            {

                if (_oJob.Update(Convert.ToInt32(HttpContext.Current.Session["editingJobID"].ToString()),
                    workDesc,
                    JobType.Corrective,
                    JobAgainstType.MaintenanceObjects,
                    objectAgainstId,
                    actionPriority,
                    reasonCode,
                    notes,
                    routeTo,
                    true,
                    requestDate,
                    requestPriority,
                    requestor,
                    0,
                    0,
                    gpsX,
                    gpsY,
                    gpsZ,
                    0,
                    0,
                    0,
                    -1,
                    mobileEquip,
                    _oJob.NullDate,
                    routeTo,
                    -1,
                    -1,
                    workOp,
                    -1,
                    subAssemblyId,
                    stateRouteId,
                    milepost,
                    milepostTo,
                    mpIncreasing,
                    additionalDamage,
                    percentOverage,
                    _oLogon.UserID,
                    ref AssignedJobId))
                {
                    //Update Costing Information
                    if (
                        !_oJob.UpdateJobCosting(
                            Convert.ToInt32(HttpContext.Current.Session["editingJobID"].ToString()),
                            costCodeId,
                            fundSource,
                            workOrder,
                            workOp,
                            orgCode,
                            fundingGroup,
                            equipNumber,
                            controlSection,
                            _oLogon.UserID))
                    {
                        //Return False To Prevent Navigation
                        return false;
                    }

                    //Check For Value
                    if (HttpContext.Current.Session["AssignedJobID"] != null)
                    {
                        //Get Additional Info From Session
                ////        lblHeader.Text = (HttpContext.Current.Session["AssignedJobID"].ToString());

                        //Refresh Attachments
               ///         AttachmentGrid.DataBind();

                        //Setup For Editing
               ///         SetupForEditing();
                    }

                    //Success Return True
                    return true;
                }

                //Return False To Prevent Navigation
                return false;

            }
            catch (Exception ex)
            {
                //Show Error
            ////    Master.ShowError(ex.Message);

                //Return False To Prevent Navigation
                return false;
            }
        }

        public void ResetSession()
        {
            //Clear Session & Fields
            if (HttpContext.Current.Session["navObject"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("navObject");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["editingJobID"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("editingJobID");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["HasAttachments"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("HasAttachments");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["AssignedJobID"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("AssignedJobID");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtWorkDescription"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtWorkDescription");
            }
            //Check For Prior Value
            if (HttpContext.Current.Session["ObjectIDCombo"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ObjectIDCombo");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ObjectIDComboText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ObjectIDComboText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtObjectDescription"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtObjectDescription");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtObjectArea"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtObjectArea");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtObjectLocation"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtObjectLocation");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtObjectAssetNumber"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtObjectAssetNumber");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["TxtWorkRequestDate"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("TxtWorkRequestDate");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboRequestor"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboRequestor");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboRequestorText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboRequestorText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboPriority"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboPriority");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboPriorityText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboPriorityText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["comboReason"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("comboReason");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["comboReasonText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("comboReasonText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["comboRouteTo"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("comboRouteTo");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["comboRouteToText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("comboRouteToText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["comboHwyRoute"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("comboHwyRoute");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["comboHwyRouteText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("comboHwyRouteText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtMilepost"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtMilepost");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtMilepostTo"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtMilepostTo");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["comboMilePostDir"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("comboMilePostDir");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["comboMilePostDirText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("comboMilePostDirText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboCostCode"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboCostCode");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboCostCodeText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboCostCodeText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboFundSource"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboFundSource");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboFundSourceText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboFundSourceText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboWorkOrder"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboWorkOrder");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboWorkOrderText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboWorkOrderText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboWorkOp"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboWorkOp");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboWorkOpText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboWorkOpText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboOrgCode"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboOrgCode");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboOrgCodeText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboOrgCodeText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboFundGroup"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboFundGroup");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboFundGroupText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboFundGroupText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboCtlSection"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboCtlSection");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboCtlSectionText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboCtlSectionText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboEquipNum"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboEquipNum");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboEquipNumText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboEquipNumText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtFN"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtFN");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtLN"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtLN");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtEmail"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtEmail");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtPhone"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtPhone");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtExt"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtExt");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtMail"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtMail");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtBuilding"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtBuilding");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtRoomNum"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtRoomNum");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboServiceOffice"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboServiceOffice");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ComboServiceOfficeText"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ComboServiceOfficeText");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["GPSX"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("GPSX");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["GPSY"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("GPSY");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["GPSZ"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("GPSZ");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["txtAddDetail"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("txtAddDetail");
            }

            //Check For Prior Value
            if (HttpContext.Current.Session["ObjectPhoto"] != null)
            {
                //Remove Old One
                HttpContext.Current.Session.Remove("ObjectPhoto");
            }
        }

        protected void SaveSessionData()
        {
            #region Request Description 

            //Check For Input
            if (txtWorkDescription.Text.Length > 0)
            {
                //Check For Prior Value
                if (HttpContext.Current.Session["txtWorkDescription"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("txtWorkDescription");
                }

                //Add New Value
                HttpContext.Current.Session.Add("txtWorkDescription", txtWorkDescription.Text.Trim());
            }

            #endregion

            #region Object Info

            //Check For Input
            if (ObjectIDCombo.Value != null)
            {
                //See If Selection Changed

                #region Combo Value

                //Check For Prior Value
                if (HttpContext.Current.Session["ObjectIDCombo"] != null)
                {
                    //See If Value Changed
                    if (HttpContext.Current.Session["ObjectIDComboText"].ToString() != ObjectIDCombo.Text)
                    {
                        //Remove Old One
                        HttpContext.Current.Session.Remove("ObjectIDCombo");

                        //Add New Value
                        HttpContext.Current.Session.Add("ObjectIDCombo", ObjectIDCombo.Value.ToString());
                    }
                }
                else
                {
                    //Add New Value
                    HttpContext.Current.Session.Add("ObjectIDCombo", ObjectIDCombo.Value.ToString());
                }

                #endregion

                #region Combo Text

                //Check For Prior Value
                if (HttpContext.Current.Session["ObjectIDComboText"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("ObjectIDComboText");
                }

                //Add New Value
                HttpContext.Current.Session.Add("ObjectIDComboText", ObjectIDCombo.Text.Trim());

                #endregion

                #region Description

                //Check For Prior Value
                if (HttpContext.Current.Session["txtObjectDescription"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("txtObjectDescription");
                }

                //Add New Value
                HttpContext.Current.Session.Add("txtObjectDescription", txtObjectDescription.Text.Trim());

                #endregion



                #region Photo

                //Check For Prior Value
                if (HttpContext.Current.Session["ObjectPhoto"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("ObjectPhoto");
                }

                //Add New Value
     ////           HttpContext.Current.Session.Add("ObjectPhoto", objectImg.ImageUrl);

                #endregion
            }

            #endregion



            #region Priority

            if (ComboPriority.Value != null)
            {
                #region Combo Value

                //Check For Prior Value
                if (HttpContext.Current.Session["ComboPriority"] != null)
                {
                    //See If Value Changed
                    if (HttpContext.Current.Session["ComboPriorityText"].ToString() != ComboPriority.Text)
                    {
                        //Remove Old One
                        HttpContext.Current.Session.Remove("ComboPriority");

                        //Add New Value
                        HttpContext.Current.Session.Add("ComboPriority", ComboPriority.Value.ToString());
                    }
                }
                else
                {
                    //Add New Value
                    HttpContext.Current.Session.Add("ComboPriority", ComboPriority.Value.ToString());
                }

                #endregion

                #region Combo Text

                //Check For Prior Value
                if (HttpContext.Current.Session["ComboPriorityText"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("ComboPriorityText");
                }

                //Add New Value
                HttpContext.Current.Session.Add("ComboPriorityText", ComboPriority.Text.Trim());

                #endregion
            }

            #endregion

            #region Reason

            if (ComboReason.Value != null)
            {
                #region Combo Value

                //Check For Prior Value
                if (HttpContext.Current.Session["comboReason"] != null)
                {
                    //See If Value Changed
                    if (HttpContext.Current.Session["comboReasonText"].ToString() != ComboReason.Text)
                    {
                        //Remove Old One
                        HttpContext.Current.Session.Remove("comboReason");

                        //Add New Value
                        HttpContext.Current.Session.Add("comboReason", ComboReason.Value.ToString());
                    }
                }
                else
                {
                    //Add New Value
                    HttpContext.Current.Session.Add("comboReason", ComboReason.Value.ToString());
                }

                #endregion

                #region Combo Text

                //Check For Prior Value
                if (HttpContext.Current.Session["comboReasonText"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("comboReasonText");
                }

                //Add New Value
                HttpContext.Current.Session.Add("comboReasonText", ComboReason.Text.Trim());

                #endregion
            }

            #endregion

            #region Requestor

            if (ComboRequestor.Value != null)
            {
                #region Combo Value

                //Check For Prior Value
                if (HttpContext.Current.Session["ComboRequestor"] != null)
                {
                    //See If Value Changed
                    if (HttpContext.Current.Session["ComboRequestorText"].ToString() != ComboRequestor.Text)
                    {
                        //Remove Old One
                        HttpContext.Current.Session.Remove("ComboRequestor");

                        //Add New Value
                        HttpContext.Current.Session.Add("ComboRequestor", ComboRequestor.Value.ToString());
                    }
                }
                else
                {
                    //Add New Value
                    HttpContext.Current.Session.Add("ComboRequestor", ComboRequestor.Value.ToString());
                }

                #endregion

                #region Combo Text

                //Check For Prior Value
                if (HttpContext.Current.Session["ComboRequestorText"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("ComboRequestorText");
                }

                //Add New Value
                HttpContext.Current.Session.Add("ComboRequestorText", ComboRequestor.Text.Trim());

                #endregion
            }
            #endregion

        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            SaveSessionData();
            //Update Job
            AddRequest();

            var savedID = Session["AssignedJobID"];

            Response.Write("<script language='javascript'>window.alert('Work Request Created. " + savedID + "');window.location='./../../index.aspx';</script>");
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            SaveSessionData();
            UpdateRequest();
            var savedID = Session["AssignedJobID"];

            Response.Write("<script language='javascript'>window.alert('Work Request Created. " + savedID + "');</script>");
        }
    }
}