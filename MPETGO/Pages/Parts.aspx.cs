using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web;
using System.Configuration;
using System.Globalization;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using MPETDSFactory;

namespace MPETGO.Pages
{
    public partial class Parts : System.Web.UI.Page
    {
        private LogonObject _oLogon;
        private DateTime date = DateTime.Now;
        private MaintAttachmentObject _oObjAttachments;
        private MaintenanceObject _oMaintObj;
        private bool _useWeb;
        private string _connectionString = ConfigurationManager.ConnectionStrings["ClientConnectionString"].ConnectionString;

        private int taskId = -1;
        private int parentObjectID = -1;
        private int areaID;
        private int costCodeID;
        private int locationID;
        private int manufacturerID;
        private int objClassID;
        private int objTypeID;
        private int prodLineID;
        private int storeroomID;
        private string txtMaintObjectNotes;
        private string txtAssetNumber;


        private decimal txtChargeRate = 0;
        private string cboFundamentalType;
        private decimal txtGpsZ;
        private int txtLogicalOrder;
        private int idealCycle = 0;
        private DateTime tmpRebuildDate;
        private string cboManufacturer;
        private string txtModel;
        private string txtMiscRef;
        private int txtProductionNbr;
        private DateTime tmpPuchaseDate;
        private decimal txtPurchasePrice;
        private string txtRemarks;
        private string txtSerial;
        private DateTime tmpAsOfDate;
        private DateTime tmpWarrantyDate;
        private int overheadRateID;
        private int responsibleID;
        private int conditionID;
        private DateTime tmpLifeCycleDate;
        private int vendorID;
        private decimal txtMilePost;
        private int milePostDir;
        private int stateRouteID;
        private decimal txtEasting;
        private decimal txtNorthing;
        private int txtWarrantyInterval;
        private int txtLifeCycleInterval;
        private int uom;
        private decimal milepostTo;
        private decimal quantity;
        private decimal txtHoursAvailable;
        private decimal txtPMHours;
        private decimal txtTotalAvailHrs;
        private int fundSource;
        private int workOrder;
        private int workOp;
        private int orgCode;
        private int fundingGroup;
        private int equipNumber;
        private int controlSection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LogonInfo"] != null)
            {
                _oLogon = ((LogonObject)Session["LogonInfo"]);
             
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }

           if (!IsPostBack)
            {
                ResetSession();
                startDate.Value = DateTime.Now;
                activeCheckBox.Checked = true;               
            }          
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Set Connection Info
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            _useWeb = (ConfigurationManager.AppSettings["UsingWebService"] == "Y");

            //Initialize Classes
           
            _oObjAttachments = new MaintAttachmentObject(_connectionString, _useWeb);

            //Set DataSource
            ObjectTypeDataSource.ConnectionString = _connectionString;
            AreaSqlDatasource.ConnectionString = _connectionString;
            StateRouteDataSource.ConnectionString = _connectionString;

        }

        #region Combo functions
        protected void ComboObjectType_OnItemsRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            ObjectTypeDataSource.SelectCommand =
                @"SELECT [n_objtypeid]
                         ,[objtypeid]
                         ,[descr]
                FROM (SELECT    dbo.objecttypes.n_objtypeid AS n_objtypeid,
                                dbo.objecttypes.objtypeid AS objtypeid,
                                dbo.objecttypes.descr AS descr,
                                ROW_NUMBER() OVER (ORDER BY n_objtypeid) AS [rn]
                    FROM        dbo.objecttypes
                    WHERE       ((objtypeid + ' ' + descr) LIKE @filter)
                                AND dbo.objecttypes.b_IsActive = 'Y'
                                AND n_objtypeid > 0 )
                                AS st
                    WHERE st.[rn] BETWEEN @startIndex AND @endIndex";
            ObjectTypeDataSource.SelectParameters.Clear();
            ObjectTypeDataSource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            ObjectTypeDataSource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString(CultureInfo.InvariantCulture));
            ObjectTypeDataSource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString(CultureInfo.InvariantCulture));
            comboBox.DataSource = ObjectTypeDataSource;
            comboBox.DataBind();
        }

        protected void ComboObjectType_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            ObjectTypeDataSource.SelectCommand =
                @" SELECT   dbo.objecttypes.n_objtypeid,
                            dbo.objecttypes.objtypeid,
                            dbo.objecttypes.descr
                            
                    FROM    dbo.objecttypes
                    WHERE   dbo.objecttypes.n_objtypeid = @ID
                            AND dbo.objecttypes.b_IsActive = 'Y'
                            AND dbo.objecttypes.n_objtypeid > 0      
                    ORDER BY dbo.objecttypes.objtypeid ASC";
            ObjectTypeDataSource.SelectParameters.Clear();
            ObjectTypeDataSource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
            comboBox.DataSource = ObjectTypeDataSource;
            comboBox.DataBind();
        }

        protected void ComboStreet_OnItemRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            StateRouteDataSource.SelectCommand =
                @"SELECT [n_StateRouteID]
		                ,[StateRouteID]
		                ,[Description]
                    FROM (SELECT  dbo.StateRoutes.[n_StateRouteID] AS n_StateRouteID,
                            dbo.StateRoutes.[StateRouteID] AS StateRouteID,
                            dbo.StateRoutes.[Description] AS Description,
			                ROW_NUMBER() OVER (ORDER BY StateRouteID) AS [rn]
                    FROM    dbo.StateRoutes
                    WHERE   (( StateRouteID + ' ' + Description) LIKE @filter)
			                AND dbo.StateRoutes.b_IsActive = 'Y'
                            AND n_StateRouteID > 0 ) AS st 
	                Where st.[rn] BETWEEN @startIndex AND @endIndex";

            StateRouteDataSource.SelectParameters.Clear();
            StateRouteDataSource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            StateRouteDataSource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString(CultureInfo.InvariantCulture));
            StateRouteDataSource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString(CultureInfo.InvariantCulture));
            comboBox.DataSource = StateRouteDataSource;
            comboBox.DataBind();
        }

        protected void ComboStreet_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            StateRouteDataSource.SelectCommand =
                @"SELECT 
                            dbo.StateRoutes.[n_StateRouteID] AS n_StateRouteID,
                            dbo.StateRoutes.[StateRouteID] AS StateRouteID,
                            dbo.StateRoutes.[Description] AS Description
			               
                    FROM    dbo.StateRoutes
                    WHERE   dbo.StateRoutes.b_IsActive = 'Y'
                            AND n_StateRouteID > 0 
                            AND n_StateRouteID = @ID
	                  
                    ORDER BY StateRouteID ASC";
            StateRouteDataSource.SelectParameters.Clear();
            StateRouteDataSource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
            comboBox.DataSource = StateRouteDataSource;
            comboBox.DataBind();

        }

        protected void ComboArea_OnItemRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            AreaSqlDatasource.SelectCommand =
                @" SELECT [n_areaid]
		                ,[areaid]
		                ,[descr]
                    From (SELECT  dbo.Areas.[n_areaid] AS n_areaid,
                            dbo.Areas.[areaid] AS areaid,
                            dbo.Areas.[descr] AS descr,
			                ROW_NUMBER() OVER (ORDER BY areaid) AS [rn]
                    FROM    dbo.Areas
                    WHERE   ((areaid + ' ' + descr) Like @filter)
			                AND dbo.Areas.b_IsActive = 'Y'
                            AND n_areaid > 0 ) AS st 
	                WHERE st.[rn] BETWEEN @startIndex AND @endIndex	"; 

            AreaSqlDatasource.SelectParameters.Clear();
            AreaSqlDatasource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            AreaSqlDatasource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString(CultureInfo.InvariantCulture));
            AreaSqlDatasource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString(CultureInfo.InvariantCulture));
            comboBox.DataSource = AreaSqlDatasource;
            comboBox.DataBind();
        }

        protected void ComboArea_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            AreaSqlDatasource.SelectCommand =
                @"SELECT 
                            dbo.Areas.[n_areaid] AS n_areaid,
                            dbo.Areas.[areaid] AS areaid,
                            dbo.Areas.[descr] AS descr
			               
                    FROM    dbo.Areas
                    WHERE   dbo.Areas.b_IsActive = 'Y'
                            AND n_areaid > 0
                            AND n_areaid = @ID 
	                ORDER BY areaid ASC	";
            AreaSqlDatasource.SelectParameters.Clear();
            AreaSqlDatasource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
            comboBox.DataSource = AreaSqlDatasource;
            comboBox.DataBind();
        }
        #endregion
        #region Save Session
        private void SaveSession()
        {
           
          if(objectID.Text.Length > 0)
           {
                if (Session["objectID"] != null)
                {
                    Session.Remove("objectID");
                }

                Session.Add("objectID", objectID.Text.Trim());
            }
           
           if(startDate != null)
            {
                Session.Remove("startDate");
                Session.Add("startDate", startDate.Value.ToString());
            }
            else { Session.Add("startDate", date); }

           if(ComboObjectType.Value != null)
            {
                Session.Remove("ComboObjectType");
            }
            Session.Add("ComboObjectType", ComboObjectType.Value.ToString());

            if (objectDesc.Text.Length > 0)
            {
                Session.Remove("objectDesc");
            }
            Session.Add("objectDesc", objectDesc.Text.Trim());

            if(ComboArea.Value != null)
            {
                Session.Remove("ComboArea"); 
            }
            Session.Add("ComboArea", ComboArea.Value.ToString());

            if(txtLat.Text.Length > 0)
            {
                Session.Remove("txtLat");
            }
            Session.Add("txtLat", txtLat.Text.Trim());

            if (txtLong.Text.Length > 0)
            {
                Session.Remove("txtLong");
            }
            Session.Add("txtLong", txtLong.Text.Trim());

            if (ComboStreet.Value != null)
            {
                Session.Remove("ComboStreet");
            }
            Session.Add("ComboStreet", ComboStreet.Value.ToString());
           
        }
        #endregion
        #region Reset Session
        public void ResetSession()
        {
            if (Session["objectID"] != null)
            {
                Session.Remove("objectID");
            }

            if (Session["startDate"] != null)
            {
                Session.Remove("startDate");
            }

            if (Session["ComboObjectType"] != null)
            {
                Session.Remove("ComboObjectType");
            }

            if (Session["objectDesc"] != null)
            {
                Session.Remove("objectDesc");
            }

            if (Session["ComboArea"] != null)
            {
                Session.Remove("ComboArea");
            }

            if (Session["txtLat"] != null)
            {
                Session.Remove("txtLat");
            }

            if (Session["txtLong"] != null)
            {
                Session.Remove("txtLong");
            }

            if (Session["ComboStreet"] != null)
            {
                Session.Remove("ComboStreet");
            }

            

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
        #endregion
        #region Add Part
        private void AddParts()
        {
            objTypeID = Convert.ToInt32(ComboObjectType.Value);
            areaID = Convert.ToInt32(ComboArea.Value);
            

            _oMaintObj = new MaintenanceObject(_connectionString, _useWeb);

            _oMaintObj.Add(objectID.Text.Trim(),
                objectDesc.Text.Trim(),
                taskId,
                parentObjectID,
                areaID,
                costCodeID,
                locationID,
                manufacturerID,
                objClassID,
                objTypeID,
                prodLineID,
                storeroomID,
                txtMaintObjectNotes,
                txtAssetNumber,
                activeCheckBox.Checked,
                true,
                true,
                true,
                txtChargeRate,
                cboFundamentalType,
                Convert.ToDecimal(txtLat.Value),
                Convert.ToDecimal(txtLong.Value),
                txtGpsZ,
                txtLogicalOrder,
                idealCycle,
                tmpRebuildDate,
                cboManufacturer,
                txtModel,
                txtMiscRef,
                txtProductionNbr,
                tmpPuchaseDate,
                txtPurchasePrice,
                txtRemarks,
                txtSerial,
                date,
                tmpWarrantyDate,
                overheadRateID,
                responsibleID,
                conditionID,
                tmpLifeCycleDate,
                vendorID,
                txtMilePost,
                milePostDir,
                stateRouteID,
                txtEasting,
                txtNorthing,
                txtWarrantyInterval,
                txtLifeCycleInterval,
                uom,
                milepostTo,
                quantity,
                txtHoursAvailable,
                txtPMHours,
                txtTotalAvailHrs,
                fundSource,
                workOrder,
                workOp,
                orgCode,
                fundingGroup,
                equipNumber,
                controlSection,
                _oLogon.UserID
                );  
            
        }
       
       

        #endregion
    }
}