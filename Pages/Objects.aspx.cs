using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Reflection;
using Microsoft.WindowsAzure.Storage.Blob;
using MPETDSFactory;

namespace MPETGO.Pages
{
    public partial class Parts : System.Web.UI.Page
    {
        
        private readonly DateTime _nullDate = Convert.ToDateTime("1/1/1960 23:59:59");
        private LogonObject _oLogon;
        private DateTime date = DateTime.Now;
        private MaintAttachmentObject _oObjAttachments;
        private MaintenanceObject _oMaintObj;
        private bool _useWeb;
        private string _connectionString = ConfigurationManager.ConnectionStrings["ClientConnectionString"].ConnectionString;

        private int taskId = -1;
        private int parentObjectID = -1;
        private int areaID = -1;
        private int costCodeID = -1;
        private int locationID = -1;
        private int manufacturerID = -1;
        private int objClassID = -1;
        private int objTypeID = -1 ;
        private int prodLineID = -1;
        private int storeroomID = -1;
        private string txtMaintObjectNotes = "";
        private string txtAssetNumber = "";


        private decimal txtChargeRate = 0;
        private string cboFundamentalType = "";
        private decimal txtGpsZ = 0;
        private int txtLogicalOrder = 0;
        private int idealCycle = 0;
        private DateTime tmpRebuildDate;
        private string cboManufacturer = "";
        private string txtModel = "";
        private string txtMiscRef = "";
        private int txtProductionNbr = 0;
        private DateTime tmpPuchaseDate ;
        private decimal txtPurchasePrice = 0;
        private string txtRemarks = "";
        private string txtSerial = "";
        private DateTime tmpAsOfDate;
        private DateTime tmpWarrantyDate;
        private int overheadRateID = -1;
        private int responsibleID = -1;
        private int conditionID = -1;
        private DateTime tmpLifeCycleDate;
        private int vendorID =-1;
        private decimal txtMilePost = 0;
        private int milePostDir = -1;
        private int stateRouteID = -1;
        private decimal txtEasting = 0;
        private decimal txtNorthing = 0;
        private int txtWarrantyInterval = 0;
        private int txtLifeCycleInterval = 0;
        private int uom = -1;
        private decimal milepostTo = 0;
        private decimal quantity = 0;
        private decimal txtHoursAvailable = 0;
        private decimal txtPMHours = 0;
        private decimal txtTotalAvailHrs = 0;
        private int fundSource = -1;
        private int workOrder = -1;
        private int workOp = -1;
        private int orgCode = -1;
        private int fundingGroup = -1;
        private int equipNumber = -1;
        private int controlSection = -1;

        //Variables to pass for attachment add
        private string docType = "JPG";
        private int maintObjectId;
        private int creator;
        private string fullPath;
        private string desc;
        private string shortName;

        private string AzureAccountName = "";
        private string AzureAccountKey = "";
        private string AzureAccountContainerName = "";

        private object ses = HttpContext.Current.Session;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LogonInfo"] != null)
            {
                _oLogon = ((LogonObject)Session["LogonInfo"]);
            }
            else
            {
                Response.Redirect("~/index.aspx", true);
            }

            #region Attempt To Load Azure Details

                //Check For Null Azure Account
                if (!string.IsNullOrEmpty(AzureAccount))
                {

                    UploadControl.AzureSettings.StorageAccountName = AzureAccount;
                    AzureAccountName = AzureAccount;

                }

                //Check For Null Access Key
                if (!string.IsNullOrEmpty(AzureAccessKey))
                {
                    UploadControl.AzureSettings.AccessKey = AzureAccessKey;
                    AzureAccountKey = AzureAccessKey;
                }

                //Check For Null Container
                if (!string.IsNullOrEmpty(AzureContainer))
                {
                    UploadControl.AzureSettings.ContainerName = AzureContainer;
                    AzureAccountContainerName = AzureContainer;
                }

            #endregion


            if (HttpContext.Current.Request.IsSecureConnection == false)
            {
                LatLongBtn.Visible = false;
            }

#if DEBUG
            //Debug Mode only
            if(HttpContext.Current.IsDebuggingEnabled)
            {
                LatLongBtn.Visible = true;
            }
#endif
            if (IsPostBack)
            {
                
            }

            if (!IsPostBack)
            {       
                ResetSession();
                startDate.Value = DateTime.Now;
                activeCheckBox.Checked = true;
                UploadControl.Enabled = false;
                #region Check for Object id from string
            if (!String.IsNullOrEmpty(Request.QueryString["n_objectID"]))
            {
                    var location = "";
                    var area = "";
                ResetSession();
                if (Session["n_objectID"] == null)
                {
                    var recordID = Convert.ToInt32(Request.QueryString["n_objectID"]);
                    if(recordID > 0)
                        {
                            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];

                            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "form_MaintenanceObjects_GetMaintenanceObjectInfo";
                            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = recordID;
                            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = _oLogon.UserID;

                            cmd.Connection = con;
                            try
                            {
                                var dt = new DataTable();
                                con.Open();
                                var dataReader = cmd.ExecuteReader();
                                dt.Load(dataReader);
                                //ComboLocation.Value = dt.Rows[0]["locationid"].ToString();
                                dt.Load(dataReader);
                                location = dt.Rows[0]["locationid"].ToString();
                                area = dt.Rows[0]["areaid"].ToString();
                                                                
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                    if (_oMaintObj.LoadData(recordID))
                    {
                        var n_objectID = _oMaintObj.Ds.Tables[0].Rows[0]["n_objectid"];

                        //Adding Session varibles
                        //Object ID number
                        Session.Add("n_objectID", n_objectID);
                        //Object Type
                        Session.Add("ComboObjectType", _oMaintObj.Ds.Tables[0].Rows[0]["n_objtypeid"]);         
                        //Object Name
                        Session.Add("objectID", _oMaintObj.Ds.Tables[0].Rows[0]["objectid"]);
                        //Object Description
                        Session.Add("objectDesc", _oMaintObj.Ds.Tables[0].Rows[0]["description"]);
                        //Area
                        Session.Add("ComboArea", _oMaintObj.Ds.Tables[0].Rows[0]["n_areaid"]);
                            var areaValue = Session["ComboArea"];
                        Session.Add("ComboAreaText", area);
                        //Longitudee
                        Session.Add("txtLong", _oMaintObj.Ds.Tables[0].Rows[0]["GPS_X"]);
                        //Latitude
                        Session.Add("txtLat", _oMaintObj.Ds.Tables[0].Rows[0]["GPS_Y"]);
                        //Date Object was Created
                        Session.Add("startDate", _oMaintObj.Ds.Tables[0].Rows[0]["CreatedOn"]);
                        //Location associated with the object
                        Session.Add("ComboLocation", _oMaintObj.Ds.Tables[0].Rows[0]["n_locationid"]);
                            var locationValue = Session["ComboLocation"];
                        Session.Add("ComboLocationText", location);
                        //active checked
                        Session.Add("active", _oMaintObj.Ds.Tables[0].Rows[0]["b_active"]);
                    }

                    //Load form inputs from Session based off of Object ID number
                    if (Session["n_objectID"] != null)
                    {
                        objectID.Value = (Session["objectID"].ToString());

                        if(Session["active"] != null)
                        {
                            var active = Session["active"].ToString();
                            active.ToUpper();
                            switch (active)
                            {
                                //Set Active Checkbox to true
                                case "Y":
                                    activeCheckBox.Checked = true;
                                    break;
                                //Set Active Checkbox to false
                                case "N":
                                    activeCheckBox.Checked = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                        //Set the value of Object Name/Type 
                        if(Session["ComboObjectType"] != null)
                        {
                            ComboObjectType.Value = Convert.ToInt32(Session["ComboObjectType"]);
                        }

                        //Set Object description text
                        if(Session["objectDesc"] != null)
                        {
                            objectDesc.Text = Session["objectDesc"].ToString();
                        }

                        //Set Object location value
                        if(Session["ComboLocation"] != null)
                        {
                            ComboLocation.Value = Convert.ToInt32(Session["ComboLocation"]);
                               
                        }

                        if(Session["ComboLocationText"] != null)
                        {
                            ComboLocation.Text = Session["ComboLocationText"].ToString();
                        }

                        //Set Object area value
                        if(Session["ComboArea"] != null)
                        {
                            ComboArea.Value = Convert.ToInt32(Session["ComboArea"]);
                        }

                        if (Session["ComboAreaText"] != null)
                        {
                            ComboArea.Text = Session["ComboAreaText"].ToString();
                        }

                        //Set Object latitude value
                        if (Session["txtLat"] != null)
                        {
                            txtLat.Text = Session["txtLat"].ToString();
                        }
                        //Set Object longitude value
                        if (Session["txtLong"] != null)
                        {
                            txtLong.Value = Session["txtLong"].ToString();
                        }

                        //Set Object Created date
                        if (Session["startDate"] != null)
                        {
                            startDate.Value = Session["startDate"];
                        }
                        #region Get attachment phonto
                        //Get any photo attachments
                        var n_objectID = Convert.ToInt32(Session["n_objectID"]);
                        
                        if (_oObjAttachments.GetAttachments(n_objectID))
                        {
                            if(_oObjAttachments.Ds.Tables.Count > 0)
                            {
                                var firstPicFound = false;

                                for (var rowIndex = 0;
                                    rowIndex < _oObjAttachments.Ds.Tables[0].Rows.Count;
                                    rowIndex++) 
                                {
                                    switch (_oObjAttachments.Ds.Tables[0].Rows[rowIndex][1].ToString().ToUpper())
                                    {
                                        case "JPG":
                                            {
                                                if(Session["ObjectPhoto"] != null)
                                                {
                                                    Session.Remove("ObjectPhoto");
                                                }
                                                var url = _oObjAttachments.Ds.Tables[0].Rows[rowIndex]["LocationORURL"].ToString();
                                                Session.Add("ObjectPhoto", url);

                                                firstPicFound = true;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                    if (firstPicFound)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion

                        if(Session["ObjectPhoto"] != null)
                        {
                            objectImg.ImageUrl = Session["ObjectPhoto"].ToString();
                        } else
                        {
                            objectImg.Visible = false;
                        }
                    }
                }            
            }

            if(Session["n_objectID"] != null)
            {
                SavePartBtn.Visible = true;
                AddPartBtn.Visible = false;
                AttachmentGrid.Visible = true;
                ASPxRoundPanel1.Visible = true;
                UploadControl.Visible = true;

                if (Session["ObjectPhoto"] != null)
                {
                    objectImg.ImageUrl = Session["ObjectPhoto"].ToString();
                }
                else
                {
                    objectImg.Visible = false;
                }

            } else
            {
                AddPartBtn.Visible = true;
                SavePartBtn.Visible = false;
                AttachmentGrid.Visible = false;
                ASPxRoundPanel1.Visible = true;
                UploadControl.Enabled = false;
                objectImg.Visible = false;
                objectID.Focus();
            }
         
        #endregion
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            // Set Connection Info
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            _useWeb = (ConfigurationManager.AppSettings["UsingWebService"] == "Y");

            //Initialize Classes
            _oObjAttachments = new MaintAttachmentObject(_connectionString, _useWeb);
            _oMaintObj = new MaintenanceObject(_connectionString, _useWeb);

            //Set DataSource
            ObjectTypeDataSource.ConnectionString = _connectionString;
            AreaSqlDatasource.ConnectionString = _connectionString;
            StateRouteDataSource.ConnectionString = _connectionString;
            LocationDataSource.ConnectionString = _connectionString;
        }
        #region Azure Setup
        string AzureAccount
        {
            get
            {
               return WebConfigurationManager.AppSettings["StorageAccount"];
            }
        }

        string AzureAccessKey
        {
            get
            {
                return WebConfigurationManager.AppSettings["StorageKey"];
            }
        }

        string AzureContainer
        {
            get
            {
                return WebConfigurationManager.AppSettings["StorageContainer"];
            }
        }
        #endregion

        #region Upload Image
        protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            // RemoveFileWithDelay(e.UploadedFile.FileNameInStorage, 5);
           
            
            string name = e.UploadedFile.FileName;
            string url = GetImageUrl(e.UploadedFile.FileNameInStorage);
            long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
            string sizeText = sizeInKilobytes + " KB";
            e.CallbackData = name + "|" + url + "|" + sizeText;

            Session.Add("ObjectPhoto", url);
            Session.Add("url", url);
            Session.Add("name", name);
            SaveSession();

            ASPxRoundPanel1.Visible = true;

            //Check For Job ID
            if (Session["n_objectID"] != null)
            {
                //Check For Previous Session Variable
                if (HttpContext.Current.Session["LogonInfo"] != null)
                {
                    //Get Logon Info From Session
                    _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);
                    var n_objectID = Convert.ToInt32(Session["n_objectID"]);

                    var folderName = "Maintenance Object Attachments";
                    moveFile(url);

                    if (_oObjAttachments.Add(n_objectID,
                        _oLogon.UserID,
                        url,
                        "JPG",
                        "Mobile Web Attachment",
                        name.Trim()))
                    {
                        //Check For Prior Value
                        if (HttpContext.Current.Session["HasAttachments"] != null)
                        {
                            //Remove Old One
                            HttpContext.Current.Session.Remove("HasAttachments");
                        }

                        //Add New Value
                        HttpContext.Current.Session.Add("HasAttachments", true);

                        if (Session["ObjectPhoto"] != null)
                        {
                            objectImg.Visible = true;
                            objectImg.ImageUrl = Session["ObjectPhoto"].ToString();
                        }
                        else
                        {
                            objectImg.Visible = false;
                        }

                        //Refresh Attachments
                        AttachmentGrid.Visible = true;
                        AttachmentGrid.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "refreshAttachments", "refreshAttachments();", true);

                    }
                }
            }
            else
            {
                if(Session["AddNewImage"] != null)
                {
                    Session.Remove("AddNewImage");
                }
                Session.Add("AddNewImage", true);
                Response.Write("<script language='javascript'>window.alert('File uploaded, Attachment Grid will be displayed after a Object ID is created is submitted.')</script>");
            }
        }



        private void moveFile(string url)
        {
            AzureFileSystemProvider provider = new AzureFileSystemProvider("");

            if (WebConfigurationManager.AppSettings["StorageAccount"] != null)
            {
                provider.StorageAccountName = UploadControl.AzureSettings.StorageAccountName;
                provider.AccessKey = UploadControl.AzureSettings.AccessKey;
                provider.ContainerName = UploadControl.AzureSettings.ContainerName;
            }
            else
            {
                Console.WriteLine("No Azure Account");
            }



        }

        string GetImageUrl(string fileName)
        {
            AzureFileSystemProvider provider = new AzureFileSystemProvider("");

            if (WebConfigurationManager.AppSettings["StorageAccount"] != null)
            {
                provider.StorageAccountName = UploadControl.AzureSettings.StorageAccountName;
                provider.AccessKey = UploadControl.AzureSettings.AccessKey;
                provider.ContainerName = UploadControl.AzureSettings.ContainerName;               
            }
            else
            {
            }
            FileManagerFolder folder = new FileManagerFolder(provider, "Maintenance Object Attachments");
            FileManagerFile file = new FileManagerFile(provider, fileName);
            var newFolderName = "";
            if(objectID.Text.Length > 0)
            {
                newFolderName = objectID.Text;
            }
            else
            {
                provider.DeleteFile(file);
                objectID.Focus();
            }
            var folderPath = Path.Combine(folder.Name.ToString(), newFolderName);
            FileManagerFolder newFolder = new FileManagerFolder(provider, folderPath);

            provider.MoveFile(file, newFolder);

            var testPath = Path.Combine("https://" + UploadControl.AzureSettings.StorageAccountName + ".blob.core.windows.net", provider.ContainerName, folderPath, fileName).Replace("\\", "/");

            //var path = Path.Combine(folder.Name.ToString(), newFolder.Name.ToString(), file.Name.ToString());
            //FileManagerFile d = new FileManagerFile(provider, path);
            //var b = provider.GetFiles(newFolder);
            //var wut = b.Contains(d);           
            //var f = b.ToList();
            //var wat = f.IndexOf(d);
            //var index = wat;
            
            //var value = f[index].FullName.ToString();
            //FileManagerFile newLocation = new FileManagerFile(provider, value );
            //FileManagerFile[] files = new FileManagerFile[] { newLocation };

            string url = testPath;
            return url;           
        }

        protected void DeleteAttachmentButton_Click(object sender, EventArgs e)
        {
            DeleteGridViewAttachment();
        }

        public void DeleteGridViewAttachment()
        {
            for (int i = 0; i < AttachmentGrid.VisibleRowCount; i++)
            {
                if (AttachmentGrid.GetRowLevel(i) == AttachmentGrid.GroupCount)
                {
                    object keyValue = AttachmentGrid.GetRowValues(i, new string[] { "ID" });
                    var id = Convert.ToInt32(keyValue.ToString());
                    if (keyValue != null)

                        _oObjAttachments.Delete(id);
                }
            }
        }

        protected void UpdatePanel_Unload(object sender, EventArgs e)
        {
            //MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            //    .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            //methodInfo.Invoke(ScriptManager.GetCurrent(Page),
            //    new object[] { sender as UpdatePanel });

            RegisterUpdatePanel((UpdatePanel)sender);
        }

        protected void RegisterUpdatePanel(UpdatePanel panel)
        {
            var sType = typeof(ScriptManager);
            var mInfo = sType.GetMethod("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel", BindingFlags.NonPublic | BindingFlags.Instance);
            if (mInfo != null)
                mInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { panel });
        }

        #endregion

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

        protected void ComboLocation_OnItemRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            LocationDataSource.SelectCommand =
                @"SELECT [n_locationid]
                        ,[locationid]
                        ,[description]
                FROM (SELECT dbo.locations.[n_locationid] AS n_locationid,
                         dbo.locations.[locationid] AS locationid,
                         dbo.locations.[description] AS description,
                         ROW_NUMBER() OVER (ORDER BY locationid) AS [rn]
                FROM    dbo.locations
                WHERE   ((locationid + ' ' + description) Like @filter)
                        AND dbo.locations.b_IsActive = 'Y'
                        AND dbo.locations.n_locationid > 0 ) AS st
                WHERE st.[rn] BETWEEN @startIndex AND @endIndex";
            LocationDataSource.SelectParameters.Clear();
            LocationDataSource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            LocationDataSource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString(CultureInfo.InvariantCulture));
            LocationDataSource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString(CultureInfo.InvariantCulture));
            comboBox.DataSource = LocationDataSource;
            comboBox.DataBind();

        }

        protected void ComboLocation_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            var comboBox = (ASPxComboBox)source;
            LocationDataSource.SelectCommand =
                @"SELECT  
                            dbo.locations.[n_locationid] AS n_locationid,
                            dbo.locations.[locationid] AS locationid,
                            dbo.locations.[description] AS description
                    FROM    dbo.locations
                    WHERE   dbo.locations.b_IsActive = 'Y'
                            AND n_locationid > 0
                            AND n_locationid = @ID
                   ORDER   By locationid ASC";
            LocationDataSource.SelectParameters.Clear();
            if (!IsPostBack)
            {
                LocationDataSource.SelectParameters.Add("ID", TypeCode.Int32, Convert.ToInt32(Session["ComboLocation"]).ToString());
            }
            else
            {
                LocationDataSource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());

            }           
            comboBox.DataSource = LocationDataSource;
            comboBox.DataBind();
            DataView dv = (DataView)LocationDataSource.Select(DataSourceSelectArguments.Empty );
            foreach (DataRowView drv in dv)
            {
                comboBox.Text = drv["locationid"].ToString() + " - " + drv["description"].ToString();
            }
            
            

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
            if (!IsPostBack)
            {
                AreaSqlDatasource.SelectParameters.Add("ID", TypeCode.Int32, Convert.ToInt32(Session["ComboArea"]).ToString());
            } else
            {
                AreaSqlDatasource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());

            }
            comboBox.DataSource = AreaSqlDatasource;
            comboBox.DataBind();

            if (!IsPostBack)
            {
                DataView dv = (DataView)AreaSqlDatasource.Select(DataSourceSelectArguments.Empty);
                foreach (DataRowView drv in dv)
                {
                    comboBox.Text = drv["areaid"].ToString() + " - " + drv["descr"].ToString();
                }
            }
            
            
        }
        #endregion
        #region Save Session
        private void SaveSession()
        {
            //Saving all the values for the form fields
           
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
                if(Session["startDate"] != null)
                {
                    Session.Remove("startDate");
                }
                Session.Add("startDate", startDate.Value);
            } else
            {
                Session.Add("startDate", date);
            }

           if(ComboObjectType.Value != null)
            {
                if(Session["ComboObjectType"] != null)
                {
                    Session.Remove("ComboObjectType");
                }
                Session.Add("ComboObjectType", ComboObjectType.Value.ToString());
            }

           if(ComboObjectType.Text.Length > 0)
            {
                if(Session["ComboObjectType"] != null)
                {
                    Session.Remove("ComboObjectTypeText");
                }
                Session.Add("ComboObjectTypeText", ComboObjectType.Text);
            }

            
            if (objectDesc.Text.Length > 0)
            {
                if(Session["objectDesc"] != null)
                {
                   Session.Remove("objectDesc");
                }
                Session.Add("objectDesc", objectDesc.Text.Trim());
            }

            if(ComboArea.Value != null && ComboArea.Text.Length > 0)
            {
                //check and clear session vars
                if(Session["ComboArea"] != null)
                {
                    Session.Remove("ComboArea");
                }
                
                if(Session["ComboAreaText"] != null)
                {
                    Session.Remove("ComboAreaText");
                }
                //Save to the session
                Session.Add("ComboArea", ComboArea.Value.ToString());
                Session.Add("ComboAreaText", ComboArea.Text);
            }

            if(txtLat.Text.Length > 0)
            {
                if(Session["txtLat"] != null)
                {
                    Session.Remove("txtLat");
                }
                Session.Add("txtLat", txtLat.Text.Trim());
            }

            if (txtLong.Text.Length > 0)
            {
                if(Session["txtLong"] != null)
                {
                    Session.Remove("txtLong");
                }
                Session.Add("txtLong", txtLong.Text.Trim());
            }

            //if (ComboStreet.Value != null && ComboStreet.Text.Length > 0)
            //{               
            //    Session.Remove("ComboStreet");
            //    Session.Remove("ComboStreetText");
            //    Session.Add("ComboStreet", ComboStreet.Value.ToString());
            //    Session.Add("ComboStreetText", ComboStreet.Text);
            //}

            if(objectImg.Visible == true)
            {
                if(Session["ObjectPhoto"] != null)
                {
                    Session.Remove("ObjectPhoto");
                }
                Session.Add("ObjectPhoto", objectImg.ImageUrl);
            }    
            
            if(ComboLocation.Value != null && ComboLocation.Text.Length > 0)
            {
                if(Session["ComboLocation"] != null)
                {
                    Session.Remove("ComboLocation");
                }

                if (Session["ComboLocationText"] != null)
                {
                    Session.Remove("ComboLocationText");
                }

                Session.Add("ComboLocation", ComboLocation.Value);
                Session.Add("ComboLocationText", ComboLocation.Text);
            }   
        }
        #endregion
        #region Reset Session
        public void ResetSession()
        {
            //Clearing out all the session fields for this form
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

            if (Session["ComboObjectTypeText"] != null)
            {
                Session.Remove("ComboObjectTypeText");
            }

            if (Session["objectDesc"] != null)
            {
                Session.Remove("objectDesc");
            }

            if (Session["ComboArea"] != null)
            {
                Session.Remove("ComboArea");
            }

            if (Session["ComboAreaText"] != null)
            {
                Session.Remove("ComboAreaText");
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

            if(Session["ComboStreetText"] != null)
            {
                Session.Remove("ComboStreetText");
            }

            if (Session["n_objectID"] != null)
            {
                Session.Remove("n_objectID");
            }

            if (Session["url"] != null)
            {
                Session.Remove("url");
            }

            if (Session["name"] != null)
            {
                Session.Remove("name");
            }

            if (Session["ObjectPhoto"] != null)
            {
                Session.Remove("ObjectPhoto");
            }

            if (Session["ComboLocation"] != null)
            {
                Session.Remove("ComboLocation");
            }

            if (Session["ComboLocationText"] != null)
            {
                Session.Remove("ComboLocationText");
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

            if (Session["active"] != null)
            {
                Session.Remove("active");
            }

            if (Session["attachmentImage"] != null)
            {
                Session.Remove("attachmentImage");
            }
            if (Session["AddNewImage"] != null)
            {
                Session.Remove("AddNewImage");
            }

        }
        #endregion
        #region Add Part
        private void AddParts()
        {
            tmpPuchaseDate = _nullDate;
            tmpRebuildDate = _nullDate;
            tmpWarrantyDate = _nullDate;
            tmpLifeCycleDate = _nullDate;
            
            objTypeID = Convert.ToInt32(ComboObjectType.Value);
            locationID = Convert.ToInt32(ComboLocation.Value);
            areaID = Convert.ToInt32(ComboArea.Value);
            _oMaintObj = new MaintenanceObject(_connectionString, _useWeb);
            decimal lat = 0;
            decimal lng = 0;
            if (txtLat.Text.Length > 0)
            {
                lat = Convert.ToDecimal(txtLat.Value.ToString());
            }
            
            if (txtLong.Text.Length > 0)
            {
                lng = Convert.ToDecimal(txtLong.Value.ToString());
            }

            if (_oMaintObj.Add(objectID.Text.Trim(),
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
                Convert.ToDecimal(lat),
                Convert.ToDecimal(lng),
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
                ))
            {
                var n_objectID = _oMaintObj.RecordID;
                if (Session["n_objectID"] != null)
                {
                    Session.Remove("n_objectID");
                }
                Session.Add("n_objectID", n_objectID);

                if(Session["AddNewImage"] != null)
                {
                    AddAttachment();
                    Session.Remove("AddNewImage");
                }
                SavePartBtn.Visible = true;
                AddPartBtn.Visible = false;
                
                UploadControl.Visible = true;
                ASPxRoundPanel1.Visible = true;
                var objectText = objectID.Text.ToString();
                objectLabel.Visible = true;
                objectLabel.Text = "Object ID: " + objectText;

                Response.Write("<script language='javascript'>window.alert('Object: " + objectID.Text.ToString() + " was created. ');window.location='..';</script>");
                
            }       
        }

        private void updateParts() {
            tmpPuchaseDate = _nullDate;
            tmpRebuildDate = _nullDate;
            tmpWarrantyDate = _nullDate;
            tmpLifeCycleDate = _nullDate;

            var recordID = Convert.ToInt32(Session["n_objectID"]);
            objTypeID = Convert.ToInt32(ComboObjectType.Value);
            locationID = Convert.ToInt32(ComboLocation.Value);
            areaID = Convert.ToInt32(ComboArea.Value);
            var txtObjectid = objectID.Text.ToString();
            var txtObjectDesc = objectDesc.Text.ToString();
            decimal lat = 0;
            if(txtLat.Text != null)
            {
                lat = Convert.ToDecimal(txtLat.Value.ToString());
            }
            decimal lng = 0;
            if(txtLong.Text != null)
            {
                lng = Convert.ToDecimal(txtLat.Value.ToString());
            }

            try
            {
                _oMaintObj = new MaintenanceObject(_connectionString, _useWeb);

                if (_oMaintObj.Update(recordID, txtObjectid, txtObjectDesc,
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
                    Convert.ToDecimal(lat),
                    Convert.ToDecimal(lng),
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
                    _oLogon.UserID))
                {
                    var objectText = objectID.Text.ToString();
                    Response.Write("<script language='javascript'>window.alert('Object: " + objectText + " was updated. ');</script>");
                    //var file = "";
                    //if (Session["url"] != null)
                    //{

                    //    file = Session["url"].ToString();
                    //}

                    //if (Session["name"] != null)
                    //{
                    //    shortName = Session["name"].ToString();
                    //}

                    //if (file != "")
                    //{
                    //    maintObjectId = objTypeID;
                    //    creator = _oLogon.UserID;
                    //    fullPath = file;

                    //    desc = "M-PET GO upload";

                    //    if (_oObjAttachments.Add(recordID,
                    //        creator,
                    //        fullPath,
                    //        docType,
                    //        desc,
                    //        shortName.Trim()))
                    //    {
                    //        objectImg.Visible = true;
                    //        objectImg.ImageUrl = Session["ObjectPhoto"].ToString();
                    //    }
                    //    else
                    //    {
                    //        throw new SystemException(@"Error adding attachment - " + _oObjAttachments.LastError);
                    //    }
                    //}

                    //Response.Redirect("/pages/parts.aspx?n_objectID=" + recordID, true);
                    
                }
                else
                {
                    throw new SystemException(@"Error adding - " + _oMaintObj.LastError);
                }
            }
            catch
            {
                throw new SystemException(@"Error updating Object - " + _oMaintObj.LastError);
            }
        
        }

        private void AddAttachment()
        {
            var n_objectID = -1;
            if(Session["n_objectID"] != null)
            {
                n_objectID = Convert.ToInt32(Session["n_objectID"]);
            }

            var file = "";
            if (Session["url"] != null)
            {
                file = Session["url"].ToString();
            }

            if (Session["name"] != null)
            {
                shortName = Session["name"].ToString();
            }

            if (file != "")
            {
                maintObjectId = objTypeID;
                creator = _oLogon.UserID;
                fullPath = file;
                desc = "M-PET GO upload";
            }


            if (_oObjAttachments.Add(n_objectID,
                    creator,
                    fullPath,
                    docType,
                    desc,
                    shortName.Trim()))
            {
                //Response.Write("<script language='javascript'>window.alert('File uploaded & attached to Object.')</script>");
            }
            else
            {
                throw new SystemException(@"Error adding attachment - " + _oObjAttachments.LastError);
            }      
        }

        protected void GetAttachments()
        {
            AzureFileSystemProvider provider = new AzureFileSystemProvider("");

            if (WebConfigurationManager.AppSettings["StorageAccount"] != null)
            {
                provider.StorageAccountName = UploadControl.AzureSettings.StorageAccountName;
                provider.AccessKey = UploadControl.AzureSettings.AccessKey;
                provider.ContainerName = UploadControl.AzureSettings.ContainerName;
            }
            else
            {
                AttachmentGrid.Visible = false;
            }

            if (Convert.ToInt32(Session["n_objectID"]) > 0)
            {
                var objectID = Convert.ToInt32(Session["objectId"]);



                Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];

                SqlConnection con = new SqlConnection(strConnString.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "form_MaintenanceObjects_GetMaintObjectAttachments";

                cmd.Parameters.Add("@MaintObjectID", SqlDbType.Int).Value = objectID;


                cmd.Connection = con;
                try
                {
                    var dt = new DataTable();
                    con.Open();
                    var dataReader = cmd.ExecuteReader();
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        var url = dt.Rows[0]["LocationOrUrl"].ToString();
                        objectImg.ImageUrl = url;
                        objectImg.Visible = true;
                        Session.Add("attachmentImage", objectImg.ImageUrl.ToString());
                    }
                    else
                    {
                        objectImg.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        #endregion

        #region Submit/Save Buttons
        protected void AddPartBtn_Click(object sender, EventArgs e)
        {
            SaveSession();
            AddParts();
        }

        protected void SavePartBtn_Click(object sender, EventArgs e)
        {
            SaveSession();
            updateParts();
            AttachmentGrid.DataBind();
            //Response.Write("<script language='javascript'>window.alert('Object Updated.');</script>");

        }
        #endregion
    }
}