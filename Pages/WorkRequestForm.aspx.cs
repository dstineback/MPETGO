using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            HwyRouteSqlDatasource.ConnectionString = _connectionString;

            AttachmentGrid.Enabled = true;
            AttachmentGrid.Visible = false;     
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Check for Logon
            if (Session["LogonInfo"] != null)
            {
                _oLogon = ((LogonObject)Session["logonInfo"]);
                userFirstName = _oLogon.FirstName;
                userLastName = _oLogon.LastName;
            } else
            {
                Response.Redirect("~/Logon.aspx", true);
            }

            #region Attempt To Load Azure Details

            ////Check For Null Azure Account
            if (!string.IsNullOrEmpty(AzureAccount))
            {
                UploadControl.AzureSettings.StorageAccountName = AzureAccount;
            }

            ////Check For Null Access Key
            if (!string.IsNullOrEmpty(AzureAccessKey))
            {
                UploadControl.AzureSettings.AccessKey = AzureAccessKey;
            }

            ////Check For Null Container
            if (!string.IsNullOrEmpty(AzureContainer))
            {
                UploadControl.AzureSettings.ContainerName = AzureContainer;
            }

            #endregion


            if (HttpContext.Current.Request.IsSecureConnection == false)
            {
                LatLongBtn.Visible = false;
            } else
            {
                LatLongBtn.Visible = true;
            }

#if DEBUG
            //Debug Mode only
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                LatLongBtn.Visible = true;
            }
#endif

            if (HttpContext.Current.Session["TxtWorkRequestDate"] != null)
            {
                //Get Info From Session
                startDate.Value = Convert.ToDateTime(Session["TxtWorkRequestDate"]);
            } else
            {
                startDate.Value = DateTime.Now;
                Session.Add("txtWorkRequestDate", startDate.Value);
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
            }
            #endregion

            #region Check for query string and Set form vars

            //Check for query string
            if (!String.IsNullOrEmpty(Request.QueryString["Jobid"]))
            {
                //Check of editing job id
                if (Session["editingJobID"] == null)
                {
                    //Create jobGuid var
                    var jobGuid = "";
                    var tempJobID = Request.QueryString["Jobid"];

                    _oLogon = ((LogonObject)Session["LogonInfo"]);

                    #region Load Job info and populate Session vars
                    //Load job info and populate session vars
                    if (_oJob.GetJobGuidFromJobID(-1, tempJobID, ref jobGuid))
                    {
                        var userId = -1;

                        if (_oLogon != null)
                        {
                            userId = _oLogon.UserID;
                        }

                        //Load job info from GUID
                        if (_oJob.LoadDataByGuid(jobGuid, userId))
                        {

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

                            HttpContext.Current.Session.Add("ComboStateRoute",
                                _oJob.Ds.Tables[0].Rows[0]["n_StateRouteID"]);
                            HttpContext.Current.Session.Add("ComboStateRouteText",
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

                            GetAttachments();

                            #region Load attachments
                            //Load Object Attachments To Get First Photo
                            if (_oObjAttachments.GetAttachments(((int)_oJob.Ds.Tables[0].Rows[0]["n_MaintObjectID"])))
                            {
                                //Check For Table
                                if (_oObjAttachments.Ds.Tables.Count > 0)
                                {
                                    AttachmentGrid.Visible = true;
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
                                            objectImg.Visible = true;
                                            objectImg.ImageUrl = Session["ObjectPhoto"].ToString();
                                            //Break Loop
                                            break;
                                        }
                                        else
                                        {
                                            objectImg.Visible = false;
                                        }
                                    }
                                } else
                                {
                                    objectImg.Visible = false;
                                                                        
                                }
                            }
                            #endregion
                        }
                    }

                    #endregion
                }
            }
            //This is the final line of code ran before page is displayed


            #region set form var values from session data
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
                    lblHeader.Text = (HttpContext.Current.Session["AssignedJobID"].ToString());
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

                if ((Session["ComboStateRoute"] != null) && (Session["ComboStateRouteText"] != null))
                {
                    ComboStateRoute.Value = Convert.ToInt32(Session["ComboStateRoute"]);
                    ComboStateRoute.Text = Session["ComboStateRouteText"].ToString();
                }

                if(Session["txtMilepost"] != null)
                {
                    txtMilepost.Value = Convert.ToInt32(Session["txtMilepost"]);
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
                    objectImg.Visible = true;
                    objectImg.ImageUrl = HttpContext.Current.Session["ObjectPhoto"].ToString();
                } else
                {
                    objectImg.Visible = false;
                }

                if (Session["attachmentImage"] != null)
                {
                    attachImg.ImageUrl = Session["attachmentImage"].ToString();
                }
            }
            #endregion
            #endregion

            //Setup Fields

            //checks if their is a job id
            if (Session["editingJobID"] != null)
            {
                saveBtn.Visible = true;
                submitBtn.Visible = false;
                editingJobID.Value = Session["editingJobID"].ToString();
            }
            else
            {
                saveBtn.Visible = false;
                submitBtn.Visible = true;
                AttachmentGrid.Visible = false;
                UploadControl.Visible = true;
                ASPxRoundPanel1.Visible = true;
                attachImg.Visible = false;
            }
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

        protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {

            Cursor.Current = Cursors.Cross;

            string name = "";
            var originalUrl = "";
            string url = "";
            if (Session["editingJobID"] != null)
            {
                name = e.UploadedFile.FileName;
                originalUrl = e.UploadedFile.FileNameInStorage;
                url = GetImageUrl(e.UploadedFile.FileNameInStorage);


                long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
                string sizeText = sizeInKilobytes + " KB";
                e.CallbackData = name + "|" + url + "|" + sizeText;

                if (Session["url"] != null)
                {
                    Session.Remove("url");
                }
                Session.Add("url", url);

                if (Session["OriginalUrl"] != null)
                {
                    Session.Remove("OriginalUrl");
                }
                Session.Add("OriginalUrl", originalUrl);

                if (Session["name"] != null)
                {
                    Session.Remove("name");
                }
                Session.Add("name", name);

                ASPxRoundPanel1.Visible = true;
                
                //Check For Previous Session Variable
                if (HttpContext.Current.Session["LogonInfo"] != null)
                {
                    //Get Logon Info From Session
                    _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);

                    var jobStepID = -1;
                    if (Session["editingJobStepID"] != null)
                    {
                        jobStepID = Convert.ToInt32(Session["editingJobStepID"].ToString());
                    }

                    //MoveAttachement(url);

                    if (_oAttachments.Add(Convert.ToInt32(HttpContext.Current.Session["editingJobID"].ToString()),
                        jobStepID,
                        _oLogon.UserID,
                        url,
                        "JPG",
                        "M-PET Go upload",
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

                        //Refresh Attachments
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "uploadComplete", " refresh()", true);
                        AttachmentGrid.Visible = true;
                        GetAttachments();
                        AttachmentGrid.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "refreshAttachments", "refreshAttachments();", true);

                    }
                }

                Cursor.Current = Cursors.Default;                
            } else
            {
                url = GetImageUrlNoJobID(e.UploadedFile.FileNameInStorage);
                name = e.UploadedFile.FileName;

                long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
                string sizeText = sizeInKilobytes + " KB";
                e.CallbackData = name + "|" + url + "|" + sizeText;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetImageUrlNoJobID(string filename)
        {
            var url = "";
            AzureFileSystemProvider provider = new AzureFileSystemProvider("");

            if (WebConfigurationManager.AppSettings["StorageAccount"] != null)
            {
                provider.StorageAccountName = UploadControl.AzureSettings.StorageAccountName;
                provider.AccessKey = UploadControl.AzureSettings.AccessKey;
                provider.ContainerName = UploadControl.AzureSettings.ContainerName;
            }
            else
            {
                throw new Exception();
            }

            FileManagerFolder folder = new FileManagerFolder(provider, "temp");
            FileManagerFile file = new FileManagerFile(provider, filename);

            provider.MoveFile(file, folder);

            if (Session["MoveNewFile"] != null)
            {
                Session.Remove("MoveNewFile");
            }
            Session.Add("MoveNewFile", true);

            url = Path.Combine(folder.ToString(), filename).Replace("\\", "/");
            
            return url;
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
            
            FileManagerFile file = new FileManagerFile(provider, fileName);

            FileManagerFolder folder = new FileManagerFolder(provider, "Work Request Attachments");
            var newFolderName = Session["AssignedJobID"].ToString();
            var folderPath = Path.Combine(folder.Name.ToString(), newFolderName);
           
            FileManagerFolder newFolder = new FileManagerFolder(provider, folderPath);

            try
            {
                provider.MoveFile(file, newFolder);
            }
            catch
            {
                Response.Write("Error Saving file");
            }
            


            var testPath = Path.Combine("https://" + UploadControl.AzureSettings.StorageAccountName + ".blob.core.windows.net", provider.ContainerName, newFolder.ToString() ,fileName).Replace("\\", "/");
            return testPath;

        }

        
        protected void AddAttachment()
        {
            try
            {
                var url = "";
                var name = "";

                AzureFileSystemProvider provider = new AzureFileSystemProvider("");

                if (WebConfigurationManager.AppSettings["StorageAccount"] != null)
                {
                    provider.StorageAccountName = UploadControl.AzureSettings.StorageAccountName;
                    provider.AccessKey = UploadControl.AzureSettings.AccessKey;
                    provider.ContainerName = UploadControl.AzureSettings.ContainerName;
                }

                //Check For Job ID
                if (HttpContext.Current.Session["editingJobID"] != null)
                    {
                    //Check For Previous Session Variable
                    if (HttpContext.Current.Session["LogonInfo"] != null)
                    {
                        //Get Logon Info From Session
                        _oLogon = ((LogonObject)HttpContext.Current.Session["LogonInfo"]);

                        var jobStepID = -1;
                        if (Session["editingJobStepID"] != null)
                        {
                            jobStepID = Convert.ToInt32(Session["editingJobStepID"].ToString());
                        }

                        FileManagerFolder folder = new FileManagerFolder(provider, "temp");
                        FileManagerFolder WRAFolder = new FileManagerFolder(provider, "Work Request Attachments");
                        FileManagerFolder idFolder = new FileManagerFolder(provider, Session["AssignedJobID"].ToString());
                        var newFolderPath = Path.Combine(WRAFolder.ToString(), idFolder.ToString());
                        FileManagerFolder movedFolderPath = new FileManagerFolder(provider, newFolderPath);
                        
                        var x = provider.GetFiles(folder);

                        foreach (var file in x)
                        {
                            name = file.Name;
                            var path = file.FullName;
                            var modName = name.Split('_');
                            var shortName = modName[1];
                            FileManagerFile oldPath = new FileManagerFile(provider, path);

                            provider.MoveFile(oldPath, movedFolderPath);

                            url = Path.Combine("https://" + UploadControl.AzureSettings.StorageAccountName + ".blob.core.windows.net", provider.ContainerName, WRAFolder.ToString(), idFolder.ToString(), name).Replace("\\", "/");


                            _oAttachments.Add(Convert.ToInt32(HttpContext.Current.Session["editingJobID"].ToString()),
                                jobStepID,
                                _oLogon.UserID,
                                url,
                                "JPG",
                                "M-PET Go upload",
                                shortName.Trim());
                                        
                            }
                        
                    }
                    }
                
                
            } catch
            {
                Response.Write("<script language='javascript'>window.alert('Error, Could not attach photo')</script>");
                
            }

        }

        protected void GetAttachments(string url)
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

            var jobid = Convert.ToInt32(Session["editingJobID"]);

            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];

            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "form_WorkOrders_GetAttachments";

            cmd.Parameters.Add("@jobid", SqlDbType.Int).Value = jobid;
            cmd.Parameters.Add("@jobStepid", SqlDbType.Int).Value = -1;

            cmd.Connection = con;
            try
            {
                var imageID = 0;
                var dt = new DataTable();
                con.Open();
                var dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);

                if (dt.Rows.Count > 0)
                {
                    var newUrl = "";
                    var shortName = "";
                    for (var i = 0; dt.Rows.Count > i; i++ )
                    {
                         newUrl = dt.Rows[i]["LocationOrUrl"].ToString();
                        if(newUrl == url)
                        {
                            url = newUrl;
                            imageID = Convert.ToInt32(dt.Rows[i]["ID"]);
                            shortName = dt.Rows[i]["ShortName"].ToString();


                            if(Session["OldImage"] != null)
                            {
                                Session.Remove("OldImage");
                            }
                            Session.Add("OldImage", imageID);
                            if(Session["shortName"] != null)
                            {
                                Session.Remove("shortName");
                            }
                            Session.Add("shortName", shortName);

                            break;
                        }

                    }
            
                    
                }
                else
                {
                    
                }


            }
            catch (Exception ex)
            {
                throw ex;
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

            var jobid = Convert.ToInt32(Session["editingJobID"]);

            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings strConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["ClientConnectionString"];

            SqlConnection con = new SqlConnection(strConnString.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "form_WorkOrders_GetAttachments";

            cmd.Parameters.Add("@jobid", SqlDbType.Int).Value = jobid;
            cmd.Parameters.Add("@jobStepid", SqlDbType.Int).Value = -1;

            cmd.Connection = con;
            try
            {
                var dt = new DataTable();
                con.Open();
                var dataReader= cmd.ExecuteReader();
                dt.Load(dataReader);

                if(dt.Rows.Count > 0)
                {                 
                    var url = dt.Rows[0]["LocationOrUrl"].ToString();
                    attachImg.ImageUrl = url;
                    attachImg.Visible = true;
                    Session.Add("attachmentImage", attachImg.ImageUrl.ToString());
                    AttachmentGrid.Visible = true;
                } else
                {
                    attachImg.Visible = false;
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

                        _oAttachments.Delete(id);
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

        protected void comboHwyRoute_OnItemsRequestedByFilterCondition_SQL(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            HwyRouteSqlDatasource.SelectCommand =
                @"SELECT  [n_StateRouteID] ,
                            [StateRouteID] ,
                            [Description]
                    FROM    ( SELECT    StateRoutes.n_StateRouteID ,
                                        StateRoutes.StateRouteID ,
                                        StateRoutes.[Description] ,
                                        ROW_NUMBER() OVER ( ORDER BY StateRoutes.[n_StateRouteID] ) AS [rn]
                              FROM      dbo.StateRoutes AS StateRoutes
                              WHERE     ( ( [StateRouteID] + ' ' + [Description]) LIKE @filter )
                                        AND StateRoutes.b_IsActive = 'Y'
                                        AND StateRoutes.n_StateRouteID > 0
                            ) AS st
                    WHERE   st.[rn] BETWEEN @startIndex AND @endIndex";

            HwyRouteSqlDatasource.SelectParameters.Clear();
            HwyRouteSqlDatasource.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", e.Filter));
            HwyRouteSqlDatasource.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            HwyRouteSqlDatasource.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = HwyRouteSqlDatasource;
            comboBox.DataBind();
        }

        protected void comboHwyRoute_OnItemRequestedByValue_SQL(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            HwyRouteSqlDatasource.SelectCommand = @"SELECT  tblStateRoutes.n_StateRouteID ,
                                                            tblStateRoutes.StateRouteID ,
                                                            tblStateRoutes.[Description],
                                                            ROW_NUMBER() OVER ( ORDER BY tblStateRoutes.[n_StateRouteID] ) AS [rn]
                                                    FROM    dbo.StateRoutes AS tblStateRoutes
                                                    WHERE   ( n_StateRouteID = @ID )
                                                    ORDER BY StateRouteID";

            HwyRouteSqlDatasource.SelectParameters.Clear();
            HwyRouteSqlDatasource.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
            comboBox.DataSource = HwyRouteSqlDatasource;
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
                requestDate = Convert.ToDateTime(Session["TxtWorkRequestDate"].ToString());
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
            if ((HttpContext.Current.Session["ComboStateRoute"] != null))
            {
                //Get Info From Session
                stateRouteId = Convert.ToInt32((HttpContext.Current.Session["ComboStateRoute"].ToString()));
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

                    if (Convert.ToBoolean(Session["MoveNewFile"]) == true)
                    {
                       
                                                                  
                        //MoveAttachement();
                        AddAttachment();
                        Session.Remove("MoveNewFile");                         
                    }

                    
                    //Set Text
                    lblHeader.Text = (HttpContext.Current.Session["AssignedJobID"].ToString());

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
                    //SetupForEditing();

                    //Return True
                    return true;
                }

                //Show Error
                throw new SystemException(@"Error adding new Work Request - " + _oJob.LastError);            
            }
             catch 
            {
                //Show Error
                throw new SystemException(@"Error adding new Work Request - " + _oJob.LastError);          
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
            if ((HttpContext.Current.Session["ComboStateRoute"] != null))
            {
                //Get Info From Session
                stateRouteId = Convert.ToInt32((HttpContext.Current.Session["ComboStateRoute"].ToString()));
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
                        //Show Error
                        throw new SystemException(@"Could not update work request Cost Codes - " + _oJob.LastError);                     
                    }
                    
                    //Check For Value
                    if (HttpContext.Current.Session["AssignedJobID"] != null)
                    {
                        //Get Additional Info From Session
                       lblHeader.Text = (HttpContext.Current.Session["AssignedJobID"].ToString());
                    }

                    //Success Return True
                    return true;
                }

                //Show Error
                throw new SystemException(@"Could not update work request - " + _oJob.LastError);
            }
            catch 
            {
                //Show Error
                throw new SystemException(@"Could not update Work Request - " + _oJob.LastError);  
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

            //Check for prior value
            if(Session["ComboStateRoute"] != null)
            {
                Session.Remove("ComboStateRoute");
            }

            if(Session["ComboStateRouteText"] != null)
            {
                Session.Remove("ComboStateRouteText");
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

            if(Session["txtMilepost"] != null)
            {
                Session.Remove("txtMilepost");
            }

            if (Session["url"] != null)
            {
                Session.Remove("url");
            }

            if (Session["name"] != null)
            {
                Session.Remove("name");
            }

            if(Session["attachmentImage"] != null)
            {
                Session.Remove("attachmentImage");
            }

            if (Session["fileName"] != null)
            {
                Session.Remove("fileName");
            }

            if (Session["MoveNewFile"] != null)
            {
                Session.Remove("MoveNewFile");
            }
            if (Session["oJob"] != null)
            {
                Session.Remove("oJob");
            }
            if (Session["OldImage"] != null)
            {
                Session.Remove("OldImage");
            }
            if (Session["OriginalUrl"] != null)
            {
                Session.Remove("OriginalUrl");
            }
            if (Session["shortName"] != null)
            {
                Session.Remove("ShortName");
            }
            if (Session["txtLat"] != null)
            {
                Session.Remove("txtLat");
            }
            if (Session["txtLong"] != null)
            {
                Session.Remove("txtLong");
            }
            if (Session["txtWorkRequstDate"] != null)
            {
                Session.Remove("txtWorkRequestDate");
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
                
                if(Session["TxtWorkRequestDate"] != null)
                {
                    Session.Remove("TxtWorkRequestDate");
                }
                Session.Add("TxtWorkRequestDate", startDate.Value);

                #region Photo

                //Check For Prior Value
                if (HttpContext.Current.Session["ObjectPhoto"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("ObjectPhoto");
                }

                //Add New Value
                HttpContext.Current.Session.Add("ObjectPhoto", objectImg.ImageUrl);

                #endregion
            }

            #endregion

            #region Attachment

            if (attachImg.ImageUrl != null)
            {
                if(Session["attachmentImage"] != null)
                {
                    Session.Remove("attachmentImage");
                }
                Session.Add("attachmentImage", attachImg.ImageUrl.ToString());
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

            #region Mile Post
            if(txtMilepost.Text.Length > 0)
            {
                if(Session["txtMilepost"] != null)
                {
                    Session.Remove("txtMilepost");
                }
                Session.Add("txtMilepost", txtMilepost.Text.Trim());
            }
            #endregion

            #region State Route
            if(ComboStateRoute.Value != null)
            {
                #region Combo Value

                //Check For Prior Value
                if (HttpContext.Current.Session["ComboStateRoute"] != null)
                {
                    //See If Value Changed
                    if (HttpContext.Current.Session["ComboStateRouteText"].ToString() != ComboStateRoute.Text)
                    {
                        //Remove Old One
                        HttpContext.Current.Session.Remove("ComboStateRoute");

                        //Add New Value
                        HttpContext.Current.Session.Add("ComboStateRoute", ComboStateRoute.Value.ToString());
                    }
                }
                else
                {
                    //Add New Value
                    HttpContext.Current.Session.Add("ComboStateRoute", ComboStateRoute.Value.ToString());
                }

                #endregion

                #region Combo Text

                //Check For Prior Value
                if (HttpContext.Current.Session["ComboStateRouteText"] != null)
                {
                    //Remove Old One
                    HttpContext.Current.Session.Remove("ComboStateRouteText");
                }

                //Add New Value
                HttpContext.Current.Session.Add("ComboStateRouteText", ComboStateRoute.Text.Trim());

                #endregion
            }
            #endregion

            #region Lat/Long
            if (txtLat.Text.Length > 0)
            {
                //Check for session value
                if(Session["txtLat"] != null)
                {
                    //Remove session Value before adding new value
                    Session.Remove("txtLat");
                }
                //Add Value from texBox
                Session.Add("txtLat", txtLat.Text.Trim());
            }

            if(txtLong.Text.Length > 0)
            {   //Check for Session Value
                if(Session["txtLong"] != null)
                {
                    //Remove Session Value before adding new value
                    Session.Remove("txtLong");
                }
                //Add new value to session from textBox
                Session.Add("txtLong", txtLong.Text.Trim());
            }
            #endregion

        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            
            //Save Session Data
            SaveSessionData();
            //Add Job
            AddRequest();
                     
            //set lable header text to Work Request ID
            var savedID = Session["AssignedJobID"];
            //Response.Write("<script language='javascript'>window.alert('Work Request Created. " + savedID + "');</script>");
            lblHeader.Text = savedID.ToString();
            var jobID = Session["editingJobID"];
            //Set Alert
          
            Response.Write("<script language='javascript'>window.alert('Work Request Created. " + savedID + "');window.location='..';</script>");
            
         }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
             

            SaveSessionData();
            //Update Job
            UpdateRequest();
          
            //set lable header text to Work Request ID
            var savedID = Session["AssignedJobID"];
            lblHeader.Text = savedID.ToString();
           
            //Set Alert
            Response.Write("<script language='javascript'>window.alert('Work Request Updated. " + savedID + "');</script>");
            AttachmentGrid.DataBind();

            
        }

        protected void LatLongBtn_Click(object sender, EventArgs e)
        {

        }

       
    }
}