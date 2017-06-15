using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using MPETDSFactory;


namespace MPETGO
{
    public class LogonObject
    {
        private readonly string _errorLogPath;

        private bool _devSettings;
        private bool _demoSettings;
        //private decimal _defaultOverheadRate = 10;

        public DbBackendConnectonType ConnectionType = DbBackendConnectonType.Sql;

        private int _forceLogoutAt = 0;
        private string _currentDbConnectionString = "";
        private string _dbVersionToMatch = "";
        private int _defaultStoreroomID = -1;
        private DateTime _passwordExpirationDate = Convert.ToDateTime(@"1/1/1960 23:59:59");
        private string _passwordExpires = "N";
        private bool _useWebBackend;
        private string _areaid = "";
        private decimal _defaultOverheadRate = 10;
        private string _taxableEnvironment = "B";
        private bool _azureHostedAttachments = false;
        private bool _lockStartDateToAdmin = false;
        private bool _lockReturnWithinToAdmin = false;
        private bool _lockSupervisorToAdmin = false;
        private bool _lockStartDateToSupervisor = false;
        private bool _lockReturnWithinToSupervisor = false;
        private bool _lockSupervisorToSupervisor = false;
        private bool _userInAdminTemplate = false;
        private bool _userInSupervisorTemplate = false;

        //Approval Settings
        private bool _userIsBuyer = false;
        private bool _userIsSIReviewer = false;
        private bool _userIsReqReviewer = false;
        private bool _userIsPOReviewer = false;
        private bool _userIsReqSuperReviewer = false;
        private bool _userIsJobPoster = false;
        private bool _userIsJobUnposter = false;
        private bool _userIsTimeBatchReviewer = false;
        private bool _userIsTimeBatchApprover = false;

        /// <summary>
        /// Private Variable To Store User's Default Labor Class
        /// </summary>
        private int _laborClassID = -1;

        private int _nGroupID = -1;
        private int _systemLeadTimeDefault;
        private int _nUserID;
        private string _phoneNumber = "";

        private string _renameControlSectionTo = "";
        private string _renameCostCodeTo = "";
        private string _renameEquipNumberTo = "";
        private string _renameFundGroupTo = "";
        private string _renameFundSourceTo = "";
        private string _renameHwyRouteTo = "";
        private string _renameOrgCodeTo = "";
        private string _renameWorkOpTo = "";
        private string _renameWorkOrderTo = "";
        private string _sPassword = "";
        private string _sUsername = "";

        /// <summary>
        /// Private Variable To Store User's Default Shift
        /// </summary>
        private int _shiftID = -1;

        private bool _useSystemReports;
        private bool _bIsLoggedIn;
        private DateTime _dtLastCommandRequested;
        private int _nAreaID;

        private JobAlertDefaults _oJobAlerts;
        private MaintenanceObjectAlerts _oMoAlerts;
        private StoreroomPartsAlerts _oSrpAlerts;
        private PurchaseOrderAlerts _oPoAlerts;
        private StoresWantAlerts _oSwlAlerts;
        private StoresIssueAlerts _oSiAlerts;
        private string _sFullName = "";
        private string _sFirstName = "";
        private string _sLastName = "";
        private string _sLastError = "";

        #region Job Coloring

        #region Bools

        #region Request Level 1 Alerts

        #region Alert Defined Flag

        /// <summary>
        /// Public Request Level 1 Alert Defined Flag
        /// </summary>
        public bool ReqLevel1AlertDefined { get; set; }

        #endregion

        #region Apply Days Before Flags

        /// <summary>
        /// Private Apply Days Before For Request Level 1
        /// </summary>
        public bool JobRequestApplyDaysBefore1 { get; set; }

        #endregion

        #endregion

        #region Request Level 2 Alerts

        #region Alert Defined Flag

        /// <summary>
        /// Public Request Level 2 Alert Defined Flag
        /// </summary>
        public bool ReqLevel2AlertDefined { get; set; }

        #endregion

        #region Apply Days Before Flags

        /// <summary>
        /// Private Apply Days Before For Request Level 2
        /// </summary>
        public bool JobRequestApplyDaysBefore2 { get; set; }

        #endregion

        #endregion

        #region Request Level 3 Alerts

        #region Alert Defined Flag

        /// <summary>
        /// Public Request Level 3 Alert Defined Flag
        /// </summary>
        public bool ReqLevel3AlertDefined { get; set; }

        #endregion

        #region Apply Days Before Flags

        /// <summary>
        /// Private Apply Days Before For Request Level 3
        /// </summary>
        public bool JobRequestApplyDaysBefore3 { get; set; }

        #endregion

        #endregion

        #region Planned Level 1 Alerts

        #region Alert Defined Flag

        /// <summary>
        /// Public Planned Level 1 Alert Defined Flag
        /// </summary>
        public bool PlannedLevel1AlertDefine { get; set; }

        #endregion

        #region Apply Days Before Flags

        /// <summary>
        /// Private Apply Days Before For Planned Level 1
        /// </summary>
        public bool PlannedApplyDaysBefore1 { get; set; }

        #endregion

        #region Based On Start Date Flags

        /// <summary>
        /// Pubic Color Based On Start Date Alert 1
        /// </summary>
        public bool PlannedJobsBasedOnStartDate1 { get; set; }

        #endregion

        #region Use Return Within Flags

        /// <summary>
        /// Pubic Use Return Within Alert 1
        /// </summary>
        public bool PlannedUseReturnWithin1 { get; set; }

        #endregion

        #endregion

        #region Planned Level 2 Alerts

        #region Alerts Defined Flag

        /// <summary>
        /// Public Planned Level 2 Alert Defined Flag
        /// </summary>
        public bool PlannedLevel2AlertDefined { get; set; }

        #endregion

        #region Apply Days Before Flags

        /// <summary>
        /// Private Apply Days Before For Planned Level 2
        /// </summary>
        public bool PlannedApplyDaysBefore2 { get; set; }

        #endregion

        #region Based On Start Date Flags

        /// <summary>
        /// Pubic Color Based On Start Date Alert 2
        /// </summary>
        public bool PlannedJobsBasedOnStartDate2 { get; set; }

        #endregion

        #region Use Return Within Flags

        /// <summary>
        /// Pubic Use Return Within Alert 2
        /// </summary>
        public bool PlannedUseReturnWithin2 { get; set; }

        #endregion

        #endregion

        #region Planned Level 3 Alerts

        #region Alerts Defined Flag

        /// <summary>
        /// Public Planned Level 3 Alert Defined Flag
        /// </summary>
        public bool PlannedLevel3AlertDefined { get; set; }

        #endregion

        #region Apply Days Before Flags

        /// <summary>
        /// Private Apply Days Before For Planned Level 3
        /// </summary>
        public bool PlannedApplyDaysBefore3 { get; set; }

        #endregion

        #region Based On Start Date Flags

        /// <summary>
        /// Pubic Color Based On Start Date Alert 3
        /// </summary>
        public bool PlannedJobsBasedOnStartDate3 { get; set; }

        #endregion

        #region Use Return Within Flags

        /// <summary>
        /// Pubic Use Return Within Alert 3
        /// </summary>
        public bool PlannedUseReturnWithin3 { get; set; }

        #endregion

        #endregion

        #endregion

        #region Colors

        #endregion

        #region Integers

        #endregion

        #endregion

        #region Maintenance Object Coloring

        #region Build Date Coloring

        #region Bools

        /// <summary>
        /// Apply Days Before
        /// </summary>
        private bool _moBuild1ApplyDaysBefore;

        /// <summary>
        /// Apply Days Before Alert 2
        /// </summary>
        private bool _moBuild2ApplyDaysBefore;

        /// <summary>
        /// Apply Days Before Alert 3
        /// </summary>
        private bool _moBuild3ApplyDaysBefore;

        /// <summary>
        /// Alert Level 1 Defined Flag
        /// </summary>
        private bool _moBuildAlert1Defined;

        /// <summary>
        /// Alert Level 2 Defined Flag
        /// </summary>
        private bool _moBuildAlert2Defined;

        /// <summary>
        /// Alert Level 3 Defined Flag
        /// </summary>
        private bool _moBuildAlert3Defined;

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _moBuildAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// Build Background 1
        /// </summary>
        private Color _moBuildBg1;

        /// <summary>
        /// Build Background 2
        /// </summary>
        private Color _moBuildBg2;

        /// <summary>
        /// Build Background 3
        /// </summary>
        private Color _moBuildBg3;

        /// <summary>
        /// Build Foreground 1
        /// </summary>
        private Color _moBuildFg1;

        /// <summary>
        /// Build Foreground 2
        /// </summary>
        private Color _moBuildFg2;

        /// <summary>
        /// Build Foreground 3
        /// </summary>
        private Color _moBuildFg3;

        #endregion

        #region Integers

        /// <summary>
        /// Days Late Alert 1
        /// </summary>
        private int _moBuildDaysLate1;

        /// <summary>
        /// Days Late Alert 2
        /// </summary>
        private int _moBuildDaysLate2;

        /// <summary>
        /// Days Late Alert 3
        /// </summary>
        private int _moBuildDaysLate3;

        #endregion

        #endregion

        #region In-Service Date Coloring

        #region Bools

        /// <summary>
        /// Alert Level 1 Defined Flag
        /// </summary>
        private bool _moService1AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 1
        /// </summary>
        private bool _moService1ApplyDaysBefore;

        /// <summary>
        /// Alert Level 2 Defined Flag
        /// </summary>
        private bool _moService2AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 2
        /// </summary>
        private bool _moService2ApplyDaysBefore;

        /// <summary>
        /// Alert Level 3 Defined Flag
        /// </summary>
        private bool _moService3AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 3
        /// </summary>
        private bool _moService3ApplyDaysBefore;

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _moServiceAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// Service Background 1
        /// </summary>
        private Color _moServiceBg1;

        /// <summary>
        /// Service Background 2
        /// </summary>
        private Color _moServiceBg2;

        /// <summary>
        /// Service Background 3
        /// </summary>
        private Color _moServiceBg3;

        /// <summary>
        /// Service Foreground 1
        /// </summary>
        private Color _moServiceFg1;

        /// <summary>
        /// Service Foreground 2
        /// </summary>
        private Color _moServiceFg2;

        /// <summary>
        /// Service Foreground 3
        /// </summary>
        private Color _moServiceFg3;

        #endregion

        #region Integers

        /// <summary>
        /// Days Late Alert 1
        /// </summary>
        private int _moServiceDaysLate1;

        /// <summary>
        /// Days Late Alert 2
        /// </summary>
        private int _moServiceDaysLate2;

        /// <summary>
        /// Days Late Alert 3
        /// </summary>
        private int _moServiceDaysLate3;

        #endregion

        #endregion

        #region Exp. Date Coloring

        #region Bools

        /// <summary>
        /// Alert Level 1 Defined Flag
        /// </summary>
        private bool _moExp1AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 1
        /// </summary>
        private bool _moExp1ApplyDaysBefore;

        /// <summary>
        /// Alert Level 2 Defined Flag
        /// </summary>
        private bool _moExp2AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 2
        /// </summary>
        private bool _moExp2ApplyDaysBefore;

        /// <summary>
        /// Alert Level 3 Defined Flag
        /// </summary>
        private bool _moExp3AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 3
        /// </summary>
        private bool _moExp3ApplyDaysBefore;

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _moExpAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// Exp Background 1
        /// </summary>
        private Color _moExpBg1;

        /// <summary>
        /// Exp Background 2
        /// </summary>
        private Color _moExpBg2;

        /// <summary>
        /// Exp Background 3
        /// </summary>
        private Color _moExpBg3;

        /// <summary>
        /// Exp Foreground 1
        /// </summary>
        private Color _moExpFg1;

        /// <summary>
        /// Exp Foreground 2
        /// </summary>
        private Color _moExpFg2;

        /// <summary>
        /// Exp Foreground 3
        /// </summary>
        private Color _moExpFg3;

        #endregion

        #region Integers

        /// <summary>
        /// Days Late Alert 1
        /// </summary>
        private int _moExpDaysLate1;

        /// <summary>
        /// Days Late Alert 2
        /// </summary>
        private int _moExpDaysLate2;

        /// <summary>
        /// Days Late Alert 3
        /// </summary>
        private int _moExpDaysLate3;

        #endregion

        #endregion

        #region Cycle Date Coloring

        #region Bools

        /// <summary>
        /// Alert Level 1 Defined Flag
        /// </summary>
        private bool _moCycle1AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 1
        /// </summary>
        private bool _moCycle1ApplyDaysBefore;

        /// <summary>
        /// Alert Level 2 Defined Flag
        /// </summary>
        private bool _moCycle2AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 2
        /// </summary>
        private bool _moCycle2ApplyDaysBefore;

        /// <summary>
        /// Alert Level 3 Defined Flag
        /// </summary>
        private bool _moCycle3AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 3
        /// </summary>
        private bool _moCycle3ApplyDaysBefore;

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _moCycleAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// Cycle Background 1
        /// </summary>
        private Color _moCycleBg1;

        /// <summary>
        /// Cycle Background 2
        /// </summary>
        private Color _moCycleBg2;

        /// <summary>
        /// Cycle Background 3
        /// </summary>
        private Color _moCycleBg3;

        /// <summary>
        /// Cycle Foreground 1
        /// </summary>
        private Color _moCycleFg1;

        /// <summary>
        /// Cycle Foreground 2
        /// </summary>
        private Color _moCycleFg2;

        /// <summary>
        /// Cycle Foreground 3
        /// </summary>
        private Color _moCycleFg3;

        #endregion

        #region Integers

        /// <summary>
        /// Days Late Alert 1
        /// </summary>
        private int _moCycleDaysLate1;

        /// <summary>
        /// Days Late Alert 2
        /// </summary>
        private int _moCycleDaysLate2;

        /// <summary>
        /// Days Late Alert 3
        /// </summary>
        private int _moCycleDaysLate3;

        #endregion

        #endregion

        #endregion

        #region Inventory Coloring

        #region Stores Want Coloring

        #region Bools

        /// <summary>
        /// Alert Level 1 Defined Flag
        /// </summary>
        private bool _storesWant1AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 1
        /// </summary>
        private bool _storesWant1ApplyDaysBefore;

        /// <summary>
        /// Alert Level 2 Defined Flag
        /// </summary>
        private bool _storesWant2AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 2
        /// </summary>
        private bool _storesWant2ApplyDaysBefore;

        /// <summary>
        /// Alert Level 3 Defined Flag
        /// </summary>
        private bool _storesWant3AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 3
        /// </summary>
        private bool _storesWant3ApplyDaysBefore;

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _storesWantAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// Cycle Background 1
        /// </summary>
        private Color _storesWantBg1;

        /// <summary>
        /// Cycle Background 2
        /// </summary>
        private Color _storesWantBg2;

        /// <summary>
        /// Cycle Background 3
        /// </summary>
        private Color _storesWantBg3;

        /// <summary>
        /// Cycle Foreground 1
        /// </summary>
        private Color _storesWantFg1;

        /// <summary>
        /// Cycle Foreground 2
        /// </summary>
        private Color _storesWantFg2;

        /// <summary>
        /// Cycle Foreground 3
        /// </summary>
        private Color _storesWantFg3;

        #endregion

        #region Integers

        /// <summary>
        /// Days Late Alert 1
        /// </summary>
        private int _storesWantDaysLate1;

        /// <summary>
        /// Days Late Alert 2
        /// </summary>
        private int _storesWantDaysLate2;

        /// <summary>
        /// Days Late Alert 3
        /// </summary>
        private int _storesWantDaysLate3;

        #endregion

        #endregion

        #region Purchase Order Coloring

        #region Bools

        /// <summary>
        /// Alert Level 1 Defined Flag
        /// </summary>
        private bool _purchaseOrder1AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 1
        /// </summary>
        private bool _purchaseOrder1ApplyDaysBefore;

        /// <summary>
        /// Alert Level 2 Defined Flag
        /// </summary>
        private bool _purchaseOrder2AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 2
        /// </summary>
        private bool _purchaseOrder2ApplyDaysBefore;

        /// <summary>
        /// Alert Level 3 Defined Flag
        /// </summary>
        private bool _purchaseOrder3AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 3
        /// </summary>
        private bool _purchaseOrder3ApplyDaysBefore;

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _purchaseOrderAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// Cycle Background 1
        /// </summary>
        private Color _purchaseOrderBg1;

        /// <summary>
        /// Cycle Background 2
        /// </summary>
        private Color _purchaseOrderBg2;

        /// <summary>
        /// Cycle Background 3
        /// </summary>
        private Color _purchaseOrderBg3;

        /// <summary>
        /// Cycle Foreground 1
        /// </summary>
        private Color _purchaseOrderFg1;

        /// <summary>
        /// Cycle Foreground 2
        /// </summary>
        private Color _purchaseOrderFg2;

        /// <summary>
        /// Cycle Foreground 3
        /// </summary>
        private Color _purchaseOrderFg3;

        #endregion

        #region Integers

        /// <summary>
        /// Days Late Alert 1
        /// </summary>
        private int _purchaseOrderDaysLate1;

        /// <summary>
        /// Days Late Alert 2
        /// </summary>
        private int _purchaseOrderDaysLate2;

        /// <summary>
        /// Days Late Alert 3
        /// </summary>
        private int _purchaseOrderDaysLate3;

        #endregion

        #endregion

        #region Stores Issue Coloring

        #region Bools

        /// <summary>
        /// Alert Level 1 Defined Flag
        /// </summary>
        private bool _storesIssue1AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 1
        /// </summary>
        private bool _storesIssue1ApplyDaysBefore;

        /// <summary>
        /// Alert Level 2 Defined Flag
        /// </summary>
        private bool _storesIssue2AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 2
        /// </summary>
        private bool _storesIssue2ApplyDaysBefore;

        /// <summary>
        /// Alert Level 3 Defined Flag
        /// </summary>
        private bool _storesIssue3AlertDefined;

        /// <summary>
        /// Apply Days Before Alert 3
        /// </summary>
        private bool _storesIssue3ApplyDaysBefore;

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _storesIssueAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// Cycle Background 1
        /// </summary>
        private Color _storesIssueBg1;

        /// <summary>
        /// Cycle Background 2
        /// </summary>
        private Color _storesIssueBg2;

        /// <summary>
        /// Cycle Background 3
        /// </summary>
        private Color _storesIssueBg3;

        /// <summary>
        /// Cycle Foreground 1
        /// </summary>
        private Color _storesIssueFg1;

        /// <summary>
        /// Cycle Foreground 2
        /// </summary>
        private Color _storesIssueFg2;

        /// <summary>
        /// Cycle Foreground 3
        /// </summary>
        private Color _storesIssueFg3;

        #endregion

        #region Integers

        /// <summary>
        /// Days Late Alert 1
        /// </summary>
        private int _storesIssueDaysLate1;

        /// <summary>
        /// Days Late Alert 2
        /// </summary>
        private int _storesIssueDaysLate2;

        /// <summary>
        /// Days Late Alert 3
        /// </summary>
        private int _storesIssueDaysLate3;

        #endregion

        #endregion

        #region Zero QOH Coloring

        #region Bools

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _storeroomQohAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// StoreroomQOH Background
        /// </summary>
        private Color _storeroomQohbg;

        /// <summary>
        /// StoreroomQOH Foreground
        /// </summary>
        private Color _storeroomQohfg;

        #endregion

        #endregion

        #region ROP Coloring

        #region Bools

        /// <summary>
        /// Alerts Defined Bool
        /// </summary>
        private bool _storeroomRopAlertsDefined;

        #endregion

        #region Colors

        /// <summary>
        /// StoreroomROP Background
        /// </summary>
        private Color _storeroomRopbg;

        /// <summary>
        /// StoreroomROP Foreground
        /// </summary>
        private Color _storeroomRopfg;

        #endregion

        #endregion

        #endregion

        #region Launchpad Settings

        //Job Stats Settings
        private bool _displayJobStats = true;

        //Top 10 Asset Cost Settings
        private bool _displayTop10AssetCosts = true;

        //Top 10 Asset Job Count Settings
        private bool _displayTop10AssetJobs = true;

        public bool DisplayJobStats
        {
            get { return _displayJobStats; }
            set { _displayJobStats = value; }
        }

        public bool DisplayTop10AssetCosts
        {
            get { return _displayTop10AssetCosts; }
            set { _displayTop10AssetCosts = value; }
        }

        public bool DisplayTop10AssetJobs
        {
            get { return _displayTop10AssetJobs; }
            set { _displayTop10AssetJobs = value; }
        }

        #endregion

        public LogonObject()
        {
            _nUserID = 0;
            _sUsername = "";
            _sPassword = "";
            _sFullName = "";
            _nAreaID = 0;
            _bIsLoggedIn = false;
            _sLastError = "";
            _currentDbConnectionString = ConfigurationManager.ConnectionStrings["ClientConnectionString"].ToString();
            //_currentDbConnectionString = HttpContext.Current.Session["connection"].ToString();
            _useWebBackend = (ConfigurationManager.AppSettings["UsingWebService"] == "Y");
            DbVersionToMatch = ConfigurationManager.AppSettings["DatabaseVersion"];
            _nGroupID = -1;
            _errorLogPath = HttpContext.Current.Server.MapPath("~/Logs");
            _devSettings = (ConfigurationManager.AppSettings["DevSettings"] == "true");
            _demoSettings = (ConfigurationManager.AppSettings["DemoSettings"] == "true");
        }

        public LogonObject(string demoConnection)
        {
            _nUserID = 0;
            _sUsername = "";
            _sPassword = "";
            _sFullName = "";
            _nAreaID = 0;
            _bIsLoggedIn = false;
            _sLastError = "";
            _currentDbConnectionString = demoConnection;
            _useWebBackend = (ConfigurationManager.AppSettings["UsingWebService"] == "Y");
            DbVersionToMatch = ConfigurationManager.AppSettings["DatabaseVersion"];
            _nGroupID = -1;
            _errorLogPath = HttpContext.Current.Server.MapPath("~/Logs");
            _devSettings = (ConfigurationManager.AppSettings["DevSettings"] == "true");
            _demoSettings = (ConfigurationManager.AppSettings["DemoSettings"] == "true");
        }

        public string TaxableEnvironment
        {
            get { return _taxableEnvironment; }
            set { _taxableEnvironment = value; }
        }

        public bool AzureHostedAttachments
        {
            get { return _azureHostedAttachments; }
            set { _azureHostedAttachments = value; }
        }

        public bool LockStartDateToAdmin
        {
            get { return _lockStartDateToAdmin; }
            set { _lockStartDateToAdmin = value; }
        }

        public bool LockReturnWithinToAdmin
        {
            get { return _lockReturnWithinToAdmin; }
            set { _lockReturnWithinToAdmin = value; }
        }

        public bool LockSupervisorToAdmin
        {
            get { return _lockSupervisorToAdmin; }
            set { _lockSupervisorToAdmin = value; }
        }

        public bool LockStartDateToSupervisor
        {
            get { return _lockStartDateToSupervisor; }
            set { _lockStartDateToSupervisor = value; }
        }

        public bool LockReturnWithinToSupervisor
        {
            get { return _lockReturnWithinToSupervisor; }
            set { _lockReturnWithinToSupervisor = value; }
        }

        public bool LockSupervisorToSupervisor
        {
            get { return _lockSupervisorToSupervisor; }
            set { _lockSupervisorToSupervisor = value; }
        }

        public bool UserInSupervisorTemplate
        {
            get { return _userInSupervisorTemplate; }
            set { _userInSupervisorTemplate = value; }
        }

        public bool UserInAdminTemplate
        {
            get { return _userInAdminTemplate; }
            set { _userInAdminTemplate = value; }
        }

        public bool UserIsBuyer
        {
            get { return _userIsBuyer; }
            set { _userIsBuyer = value; }
        }

        public bool UserIsSIReviewer
        {
            get { return _userIsSIReviewer; }
            set { _userIsSIReviewer = value; }
        }

        public bool UserIsReqReviewer
        {
            get { return _userIsReqReviewer; }
            set { _userIsReqReviewer = value; }
        }

        public bool UserIsPOReviewer
        {
            get { return _userIsPOReviewer; }
            set { _userIsPOReviewer = value; }
        }

        public bool UserIsReqSuperReviewer
        {
            get { return _userIsReqSuperReviewer; }
            set { _userIsReqSuperReviewer = value; }
        }

        public bool UserIsJobPoster
        {
            get { return _userIsJobPoster; }
            set { _userIsJobPoster = value; }
        }

        public bool UserIsJobUnposter
        {
            get { return _userIsJobUnposter; }
            set { _userIsJobUnposter = value; }
        }

        public bool UserIsTimeBatchReviewer
        {
            get { return _userIsTimeBatchReviewer; }
            set { _userIsTimeBatchReviewer = value; }
        }

        public bool UserIsTimeBatchApprover
        {
            get { return _userIsTimeBatchReviewer; }
            set { _userIsTimeBatchReviewer = value; }
        }

        public string PasswordExpires
        {
            get { return _passwordExpires; }
            set { _passwordExpires = value; }
        }

        public int PasswordExpiresIn { get; set; }

        public DateTime PasswordExpirationDate
        {
            get { return _passwordExpirationDate; }
            set { _passwordExpirationDate = value; }
        }

        public bool PasswordHasExpired { get; set; }

        public string DatabaseConnnectionString
        {
            get { return _currentDbConnectionString.Trim(); }
            set { _currentDbConnectionString = value.Trim(); }
        }

        public bool UsingWeb
        {
            get { return _useWebBackend; }
            set { _useWebBackend = value; }
        }

        public string DbVersionToMatch
        {
            get { return _dbVersionToMatch; }
            set { _dbVersionToMatch = value; }
        }

        public bool LoggedIn
        {
            get { return _bIsLoggedIn; }
            set { _bIsLoggedIn = value; }
        }

        public string LastError
        {
            get { return _sLastError; }
        }

        public int UserID
        {
            get { return _nUserID; }
        }

        public int AreaID
        {
            get { return _nAreaID; }
        }

        public string Area
        {
            get { return _areaid; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
        }

        public int GroupID
        {
            get { return _nGroupID; }
            set { _nGroupID = value; }
        }

        public string FirstName
        {
            get { return _sFirstName; }
        }

        public string LastName
        {
            get { return _sLastName; }
        }
        public string FullName
        {
            get { return _sFullName; }
        }

        public string Username
        {
            get { return _sUsername.Trim(); }
            set { _sUsername = value; }
        }

        public bool AdInUse { get; set; }

        public string Password
        {
            get { return _sPassword.Trim(); }
            set { _sPassword = value; }
        }

        public DateTime LoggedInAt { get; set; }

        /// <summary>
        /// LastCommandGivenAt: Returns the last datestamp a command was given
        ///                     used for determining auto-logout
        /// </summary>
        public DateTime LastCommandGivenAt
        {
            get { return _dtLastCommandRequested; }
            set { _dtLastCommandRequested = value; }
        }

        /// <summary>
        /// Sets/Returns User's Default Storeroom ID
        /// </summary>
        public int DefaultStoreroom
        {
            get { return _defaultStoreroomID; }
            set { _defaultStoreroomID = value; }
        }

        /// <summary>
        /// Sets/Returns True/False If User Is In A Super Group User
        /// </summary>
        public bool SuperGroupUser { get; set; }

        /// <summary>
        /// Public Variable To Access Private Labor Class 
        /// </summary>
        public int LaborClassID
        {
            get { return _laborClassID; }
            set { _laborClassID = value; }
        }

        /// <summary>
        /// Public Variable To Access Private Shift ID
        /// </summary>
        public int ShiftID
        {
            get { return _shiftID; }
            set { _shiftID = value; }
        }

        /// <summary>
        /// Public Variable To Access Private UseSystemReports
        /// </summary>
        public bool UseSystemReports
        {
            get { return _useSystemReports; }
            set { _useSystemReports = value; }
        }

        /// <summary>
        /// Public Variable To Access Private ValidateWorkOperations
        /// </summary>
        public bool ValidateWorkOperations { get; set; }

        /// <summary>
        /// Public Variable To Access Private UsersStorerooms
        /// </summary>
        public DataTable UsersStorerooms { get; set; }

        /// <summary>
        /// Public Variable To Access Private UsersWorkOperations
        /// </summary>
        public DataTable UsersWorkOperations { get; set; }

        /// <summary>
        /// Public Variable To Access Private DefaultOverheadRate
        /// </summary>
        public decimal DefaultOverheadRate
        {
            get { return _defaultOverheadRate; }
            set { _defaultOverheadRate = value; }
        }

        /// <summary>
        /// Public Variable To Access Private UseSimpleCostLinking
        /// </summary>
        public bool UseSimpleCostLinking { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameCostCode
        /// </summary>
        public bool RenameCostCode { get; set; }

        public bool RequireCostCodeInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameFundSource
        /// </summary>
        public bool RenameFundSource { get; set; }

        public bool RequireFundSourceInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameWorkOrder
        /// </summary>
        public bool RenameWorkOrder { get; set; }

        public bool RequireWorkOrderInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameWorkOp
        /// </summary>
        public bool RenameWorkOp { get; set; }

        public bool RequireWorkOpInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameOrgCode
        /// </summary>
        public bool RenameOrgCode { get; set; }

        public bool RequireOrgCodeInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameFundGroup
        /// </summary>
        public bool RenameFundGroup { get; set; }

        public bool RequireFundGroupInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameControlSection
        /// </summary>
        public bool RenameControlSection { get; set; }

        public bool RequireControlSectionInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameEquipNumber
        /// </summary>
        public bool RenameEquipNumber { get; set; }

        public bool RequireEquipNumberInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameHwyRoute
        /// </summary>
        public bool RenameHwyRoute { get; set; }

        public bool RequireHwyRouteInput { get; set; }

        /// <summary>
        /// Public Variable to Access Private _renameCostCodeTo
        /// </summary>
        public string RenameCostCodeTo
        {
            get { return _renameCostCodeTo; }
            set { _renameCostCodeTo = value; }
        }

        /// <summary>
        /// Public Variable to Access Private _renameFundSourceTo
        /// </summary>
        public string RenameFundSourceTo
        {
            get { return _renameFundSourceTo; }
            set { _renameFundSourceTo = value; }
        }

        /// <summary>
        /// Public Variable to Access Private _renameWorkOrderTo
        /// </summary>
        public string RenameWorkOrderTo
        {
            get { return _renameWorkOrderTo; }
            set { _renameWorkOrderTo = value; }
        }

        /// <summary>
        /// Public Variable to Access Private _renameWorkOpTo
        /// </summary>
        public string RenameWorkOpTo
        {
            get { return _renameWorkOpTo; }
            set { _renameWorkOpTo = value; }
        }

        /// <summary>
        /// Public Variable to Access Private _renameOrgCodeTo
        /// </summary>
        public string RenameOrgCodeTo
        {
            get { return _renameOrgCodeTo; }
            set { _renameOrgCodeTo = value; }
        }

        /// <summary>
        /// Public Variable to Access Private _renameFundGroupTo
        /// </summary>
        public string RenameFundGroupTo
        {
            get { return _renameFundGroupTo; }
            set { _renameFundGroupTo = value; }
        }

        /// <summary>
        /// Public Variable to Access Private _renameControlSectionTo
        /// </summary>
        public string RenameControlSectionTo
        {
            get { return _renameControlSectionTo; }
            set { _renameControlSectionTo = value; }
        }

        /// <summary>
        /// Public Variable to Access Private _renameEquipNumberTo
        /// </summary>
        public string RenameEquipNumberTo
        {
            get { return _renameEquipNumberTo; }
            set { _renameEquipNumberTo = value; }
        }

        /// <summary>
        /// Public Variable to Access Private _renameHwyRouteTo
        /// </summary>
        public string RenameHwyRouteTo
        {
            get { return _renameHwyRouteTo; }
            set { _renameHwyRouteTo = value; }
        }

        /// <summary>
        /// Public Variable To Access Private ValidateStorerooms
        /// </summary>
        public bool ValidateStorerooms { get; set; }

        #region Public Jobs Alert Functions

        /// <summary>
        /// UserAlertsDefined: Returns True If User Has Alerts Defined
        /// </summary>
        public bool UserAlertsDefined { get; set; }

        /// <summary>
        /// PlannedJobDaysLate1: Returns Days Late For Alert Level 1
        /// </summary>
        public int PlannedJobDaysLate1 { get; set; }

        /// <summary>
        /// PlannedJobDaysLate2: Returns Days Late For Alert Level 2
        /// </summary>
        public int PlannedJobDaysLate2 { get; set; }

        /// <summary>
        /// PlannedJobDaysLate3: Returns Days Late For Alert Level 3
        /// </summary>
        public int PlannedJobDaysLate3 { get; set; }

        /// <summary>
        /// JobRequestDaysLate1: Returns Days Late For Alert Level 1
        /// </summary>
        public int JobRequestDaysLate1 { get; set; }

        /// <summary>
        /// JobRequestDaysLate2: Returns Days Late For Alert Level 2
        /// </summary>
        public int JobRequestDaysLate2 { get; set; }

        /// <summary>
        /// JobRequestDaysLate3: Returns Days Late For Alert Level 3
        /// </summary>
        public int JobRequestDaysLate3 { get; set; }

        /// <summary>
        /// JobRequestFG1: Returns Job Request Forground Color For Alert Level 1
        /// </summary>
        public Color JobRequestFg1 { get; set; }

        /// <summary>
        /// JobRequestBG1: Returns Job Request Background Color For Alert Level 1
        /// </summary>
        public Color JobRequestBg1 { get; set; }

        /// <summary>
        /// JobRequestFG2: Returns Job Request Forground Color For Alert Level 2
        /// </summary>
        public Color JobRequestFg2 { get; set; }

        /// <summary>
        /// JobRequestBG2: Returns Job Request Background Color For Alert Level 2
        /// </summary>
        public Color JobRequestBg2 { get; set; }

        /// <summary>
        /// JobRequestBG3: Returns Job Request Background Color For Alert Level 3
        /// </summary>
        public Color JobRequestBg3 { get; set; }

        /// <summary>
        /// JobRequestFG3: Returns Job Request Forground Color For Alert Level 3
        /// </summary>
        public Color JobRequestFg3 { get; set; }

        /// <summary>
        /// PlannedJobFG1: Returns Planned Job Forground Color For Alert Level 1
        /// </summary>
        public Color PlannedJobFg1 { get; set; }

        /// <summary>
        /// PlannedJobBG1: Returns Planned Job Background Color For Alert Level 1
        /// </summary>
        public Color PlannedJobBg1 { get; set; }

        /// <summary>
        /// PlannedJobBG2: Returns Planned Job Background Color For Alert Level 2
        /// </summary>
        public Color PlannedJobBg2 { get; set; }

        /// <summary>
        /// PlannedJobFG2: Returns Planned Job Forground Color For Alert Level 2
        /// </summary>
        public Color PlannedJobFg2 { get; set; }

        /// <summary>
        /// PlannedJobFG3: Returns Planned Job Forground Color For Alert Level 3
        /// </summary>
        public Color PlannedJobFg3 { get; set; }

        /// <summary>
        /// PlannedJobBG3: Returns Planned Job Background Color For Alert Level 3
        /// </summary>
        public Color PlannedJobBg3 { get; set; }

        #endregion

        #region Public Maintenance Object Build Alert Functions

        /// <summary>
        /// Returns True If User Has Maintenance Object Build Alerts Defined
        /// </summary>
        public bool UserMoBuildAlertsDefined
        {
            //Return Value
            get { return _moBuildAlertsDefined; }

            //Set Value
            set { _moBuildAlertsDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Build 1 Alerts Defined
        /// </summary>
        public bool UserMoBuild1AlertDefined
        {
            //Return Value
            get { return _moBuildAlert1Defined; }

            //Set Value
            set { _moBuildAlert1Defined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Build 2 Alert Defined
        /// </summary>
        public bool UserMoBuild2AlertDefined
        {
            //Return Value
            get { return _moBuildAlert2Defined; }

            //Set Value
            set { _moBuildAlert2Defined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Build 3 Alert Defined
        /// </summary>
        public bool UserMoBuild3AlertDefined
        {
            //Return Value
            get { return _moBuildAlert3Defined; }

            //Set Value
            set { _moBuildAlert3Defined = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Build 1 Alert
        /// </summary>
        public bool MaintObjBuild1ApplyDaysBefore
        {
            //Get Value
            get { return _moBuild1ApplyDaysBefore; }

            //Set Value
            set { _moBuild1ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Build 2 Alert
        /// </summary>
        public bool MaintObjBuild2ApplyDaysBefore
        {
            //Get Value
            get { return _moBuild2ApplyDaysBefore; }

            //Set Value
            set { _moBuild2ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Build 3 Alert
        /// </summary>
        public bool MaintObjBuild3ApplyDaysBefore
        {
            //Get Value
            get { return _moBuild3ApplyDaysBefore; }

            //Set Value
            set { _moBuild3ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Days Late Level 1
        /// </summary>
        public int MaintObjBuildDaysLate1
        {
            //Get Value
            get { return _moBuildDaysLate1; }

            //Set Value
            set { _moBuildDaysLate1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Days Late Level 2
        /// </summary>
        public int MaintObjBuildDaysLate2
        {
            //Get Value
            get { return _moBuildDaysLate2; }

            //Set Value
            set { _moBuildDaysLate2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Days Late Level 3
        /// </summary>
        public int MaintObjBuildDaysLate3
        {
            //Get Value
            get { return _moBuildDaysLate3; }

            //Set Value
            set { _moBuildDaysLate3 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Foreground 1
        /// </summary>
        public Color MaintObjBuildFg1
        {
            //Get
            get { return _moBuildFg1; }

            //Set
            set { _moBuildFg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Background 1
        /// </summary>
        public Color MaintObjBuildBG1
        {
            //Get
            get { return _moBuildBg1; }

            //Set
            set { _moBuildBg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Foreground 2
        /// </summary>
        public Color MaintObjBuildFg2
        {
            //Get
            get { return _moBuildFg2; }

            //Set
            set { _moBuildFg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Background 2
        /// </summary>
        public Color MaintObjBuildBG2
        {
            //Get
            get { return _moBuildBg2; }

            //Set
            set { _moBuildBg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Foreground 3
        /// </summary>
        public Color MaintObjBuildFg3
        {
            //Get
            get { return _moBuildFg3; }

            //Set
            set { _moBuildFg3 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Build Background 3
        /// </summary>
        public Color MaintObjBuildBG3
        {
            //Get
            get { return _moBuildBg3; }

            //Set
            set { _moBuildBg3 = value; }
        }

        #endregion

        #region Public Maintenance Object Service Alert Functions

        /// <summary>
        /// Returns True If User Has Maintenance Object In-Service Alerts Defined
        /// </summary>
        public bool UserMoServiceAlertsDefined
        {
            //Return Value
            get { return _moServiceAlertsDefined; }

            //Set Value
            set { _moServiceAlertsDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object In-Service Alert 1 Defined
        /// </summary>
        public bool UserMoService1AlertDefined
        {
            //Return Value
            get { return _moService1AlertDefined; }

            //Set Value
            set { _moService1AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object In-Service Alert 2 Defined
        /// </summary>
        public bool UserMoService2AlertDefined
        {
            //Return Value
            get { return _moService2AlertDefined; }

            //Set Value
            set { _moService2AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object In-Service Alert 3 Defined
        /// </summary>
        public bool UserMoService3AlertDefined
        {
            //Return Value
            get { return _moService3AlertDefined; }

            //Set Value
            set { _moService3AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Service Alert 1
        /// </summary>
        public bool MaintObjService1ApplyDaysBefore
        {
            //Get Value
            get { return _moService1ApplyDaysBefore; }

            //Set Value
            set { _moService1ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Service Alert 2
        /// </summary>
        public bool MaintObjService2ApplyDaysBefore
        {
            //Get Value
            get { return _moService2ApplyDaysBefore; }

            //Set Value
            set { _moService2ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Service Alert 3
        /// </summary>
        public bool MaintObjService3ApplyDaysBefore
        {
            //Get Value
            get { return _moService3ApplyDaysBefore; }

            //Set Value
            set { _moService3ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Days Late Level 1
        /// </summary>
        public int MaintObjServiceDaysLate1
        {
            //Get Value
            get { return _moServiceDaysLate1; }

            //Set Value
            set { _moServiceDaysLate1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Days Late Level 2
        /// </summary>
        public int MaintObjServiceDaysLate2
        {
            //Get Value
            get { return _moServiceDaysLate2; }

            //Set Value
            set { _moServiceDaysLate2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Days Late Level 3
        /// </summary>
        public int MaintObjServiceDaysLate3
        {
            //Get Value
            get { return _moServiceDaysLate3; }

            //Set Value
            set { _moServiceDaysLate3 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Foreground 1
        /// </summary>
        public Color MaintObjServiceFg1
        {
            //Get
            get { return _moServiceFg1; }

            //Set
            set { _moServiceFg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Background 1
        /// </summary>
        public Color MaintObjServiceBG1
        {
            //Get
            get { return _moServiceBg1; }

            //Set
            set { _moServiceBg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Foreground 2
        /// </summary>
        public Color MaintObjServiceFg2
        {
            //Get
            get { return _moServiceFg2; }

            //Set
            set { _moServiceFg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Background 2
        /// </summary>
        public Color MaintObjServiceBG2
        {
            //Get
            get { return _moServiceBg2; }

            //Set
            set { _moServiceBg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Foreground 3
        /// </summary>
        public Color MaintObjServiceFg3
        {
            //Get
            get { return _moServiceFg3; }

            //Set
            set { _moServiceFg3 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Service Background 3
        /// </summary>
        public Color MaintObjServiceBG3
        {
            //Get
            get { return _moServiceBg3; }

            //Set
            set { _moServiceBg3 = value; }
        }

        #endregion

        #region Public Maintenance Object Exp Alert Functions

        /// <summary>
        /// Returns True If User Has Maintenance Object Exp. Alerts Defined
        /// </summary>
        public bool UserMoExpAlertsDefined
        {
            //Return Value
            get { return _moExpAlertsDefined; }

            //Set Value
            set { _moExpAlertsDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Exp. Alert 1 Defined
        /// </summary>
        public bool UserMoExp1AlertDefined
        {
            //Return Value
            get { return _moExp1AlertDefined; }

            //Set Value
            set { _moExp1AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Exp. Alert 2 Defined
        /// </summary>
        public bool UserMoExp2AlertDefined
        {
            //Return Value
            get { return _moExp2AlertDefined; }

            //Set Value
            set { _moExp2AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Exp. Alert 3 Defined
        /// </summary>
        public bool UserMoExp3AlertDefined
        {
            //Return Value
            get { return _moExp3AlertDefined; }

            //Set Value
            set { _moExp3AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Exp Build Alert 1
        /// </summary>
        public bool MaintObjExp1ApplyDaysBefore
        {
            //Get Value
            get { return _moExp1ApplyDaysBefore; }

            //Set Value
            set { _moExp1ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Exp Build Alert 2
        /// </summary>
        public bool MaintObjExp2ApplyDaysBefore
        {
            //Get Value
            get { return _moExp2ApplyDaysBefore; }

            //Set Value
            set { _moExp2ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Exp Build Alert 3
        /// </summary>
        public bool MaintObjExp3ApplyDaysBefore
        {
            //Get Value
            get { return _moExp3ApplyDaysBefore; }

            //Set Value
            set { _moExp3ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Days Late Level 1
        /// </summary>
        public int MaintObjExpDaysLate1
        {
            //Get Value
            get { return _moExpDaysLate1; }

            //Set Value
            set { _moExpDaysLate1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Days Late Level 2
        /// </summary>
        public int MaintObjExpDaysLate2
        {
            //Get Value
            get { return _moExpDaysLate2; }

            //Set Value
            set { _moExpDaysLate2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Days Late Level 3
        /// </summary>
        public int MaintObjExpDaysLate3
        {
            //Get Value
            get { return _moExpDaysLate3; }

            //Set Value
            set { _moExpDaysLate3 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Foreground 1
        /// </summary>
        public Color MaintObjExpFg1
        {
            //Get
            get { return _moExpFg1; }

            //Set
            set { _moExpFg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Background 1
        /// </summary>
        public Color MaintObjExpBG1
        {
            //Get
            get { return _moExpBg1; }

            //Set
            set { _moExpBg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Foreground 2
        /// </summary>
        public Color MaintObjExpFg2
        {
            //Get
            get { return _moExpFg2; }

            //Set
            set { _moExpFg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Background 2
        /// </summary>
        public Color MaintObjExpBG2
        {
            //Get
            get { return _moExpBg2; }

            //Set
            set { _moExpBg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Foreground 3
        /// </summary>
        public Color MaintObjExpFg3
        {
            //Get
            get { return _moExpFg3; }

            //Set
            set { _moExpFg3 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Exp Background 3
        /// </summary>
        public Color MaintObjExpBG3
        {
            //Get
            get { return _moExpBg3; }

            //Set
            set { _moExpBg3 = value; }
        }

        #endregion

        #region Public Maintenance Object Cycle Alert Functions

        /// <summary>
        /// Returns True If User Has Maintenance Object Cycle Alerts Defined
        /// </summary>
        public bool UserMoCycleAlertsDefined
        {
            //Return Value
            get { return _moCycleAlertsDefined; }

            //Set Value
            set { _moCycleAlertsDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Cycle Alert 1 Defined
        /// </summary>
        public bool UserMoCycle1AlertDefined
        {
            //Return Value
            get { return _moCycle1AlertDefined; }

            //Set Value
            set { _moCycle1AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Cycle Alert 2 Defined
        /// </summary>
        public bool UserMoCycle2AlertDefined
        {
            //Return Value
            get { return _moCycle2AlertDefined; }

            //Set Value
            set { _moCycle2AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Maintenance Object Cycle Alert 3 Defined
        /// </summary>
        public bool UserMoCycle3AlertDefined
        {
            //Return Value
            get { return _moCycle3AlertDefined; }

            //Set Value
            set { _moCycle3AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Cycle Alert 1
        /// </summary>
        public bool MaintObjCycle1ApplyDaysBefore
        {
            //Get Value
            get { return _moCycle1ApplyDaysBefore; }

            //Set Value
            set { _moCycle1ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Cycle Alert 2
        /// </summary>
        public bool MaintObjCycle2ApplyDaysBefore
        {
            //Get Value
            get { return _moCycle2ApplyDaysBefore; }

            //Set Value
            set { _moCycle2ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Maintenance Object Cycle Alert 3
        /// </summary>
        public bool MaintObjCycle3ApplyDaysBefore
        {
            //Get Value
            get { return _moCycle3ApplyDaysBefore; }

            //Set Value
            set { _moCycle3ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Days Late Level 1
        /// </summary>
        public int MaintObjCycleDaysLate1
        {
            //Get Value
            get { return _moCycleDaysLate1; }

            //Set Value
            set { _moCycleDaysLate1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Days Late Level 2
        /// </summary>
        public int MaintObjCycleDaysLate2
        {
            //Get Value
            get { return _moCycleDaysLate2; }

            //Set Value
            set { _moCycleDaysLate2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Days Late Level 3
        /// </summary>
        public int MaintObjCycleDaysLate3
        {
            //Get Value
            get { return _moCycleDaysLate3; }

            //Set Value
            set { _moCycleDaysLate3 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Foreground 1
        /// </summary>
        public Color MaintObjCycleFg1
        {
            //Get
            get { return _moCycleFg1; }

            //Set
            set { _moCycleFg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Background 1
        /// </summary>
        public Color MaintObjCycleBG1
        {
            //Get
            get { return _moCycleBg1; }

            //Set
            set { _moCycleBg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Foreground 2
        /// </summary>
        public Color MaintObjCycleFg2
        {
            //Get
            get { return _moCycleFg2; }

            //Set
            set { _moCycleFg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Background 2
        /// </summary>
        public Color MaintObjCycleBG2
        {
            //Get
            get { return _moCycleBg2; }

            //Set
            set { _moCycleBg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Foreground 3
        /// </summary>
        public Color MaintObjCycleFg3
        {
            //Get
            get { return _moCycleFg3; }

            //Set
            set { _moCycleFg3 = value; }
        }

        /// <summary>
        /// Gets/Sets Maintenance Object Cycle Background 3
        /// </summary>
        public Color MaintObjCycleBG3
        {
            //Get
            get { return _moCycleBg3; }

            //Set
            set { _moCycleBg3 = value; }
        }

        #endregion

        #region Public Stores Want Alert Functions

        /// <summary>
        /// Returns True If User Has Stores Want Alerts Defined
        /// </summary>
        public bool UserStoresWantAlertsDefined
        {
            //Return Value
            get { return _storesWantAlertsDefined; }

            //Set Value
            set { _storesWantAlertsDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Stores Want Alert 1 Defined
        /// </summary>
        public bool UserStoresWant1AlertDefined
        {
            //Return Value
            get { return _storesWant1AlertDefined; }

            //Set Value
            set { _storesWant1AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Stores Want Alert 2 Defined
        /// </summary>
        public bool UserStoresWant2AlertDefined
        {
            //Return Value
            get { return _storesWant2AlertDefined; }

            //Set Value
            set { _storesWant2AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Stores Want Alert 3 Defined
        /// </summary>
        public bool UserStoresWant3AlertDefined
        {
            //Return Value
            get { return _storesWant3AlertDefined; }

            //Set Value
            set { _storesWant3AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Stores Want Alert 1
        /// </summary>
        public bool StoresWant1ApplyDaysBefore
        {
            //Get Value
            get { return _storesWant1ApplyDaysBefore; }

            //Set Value
            set { _storesWant1ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Stores Want Alert 2
        /// </summary>
        public bool StoresWant2ApplyDaysBefore
        {
            //Get Value
            get { return _storesWant2ApplyDaysBefore; }

            //Set Value
            set { _storesWant2ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Stores Want Alert 3
        /// </summary>
        public bool StoresWant3ApplyDaysBefore
        {
            //Get Value
            get { return _storesWant3ApplyDaysBefore; }

            //Set Value
            set { _storesWant3ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Days Late Level 1
        /// </summary>
        public int StoresWantDaysLate1
        {
            //Get Value
            get { return _storesWantDaysLate1; }

            //Set Value
            set { _storesWantDaysLate1 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Days Late Level 2
        /// </summary>
        public int StoresWantDaysLate2
        {
            //Get Value
            get { return _storesWantDaysLate2; }

            //Set Value
            set { _storesWantDaysLate2 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Days Late Level 3
        /// </summary>
        public int StoresWantDaysLate3
        {
            //Get Value
            get { return _storesWantDaysLate3; }

            //Set Value
            set { _storesWantDaysLate3 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Foreground 1
        /// </summary>
        public Color StoresWantFg1
        {
            //Get
            get { return _storesWantFg1; }

            //Set
            set { _storesWantFg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Background 1
        /// </summary>
        public Color StoresWantBG1
        {
            //Get
            get { return _storesWantBg1; }

            //Set
            set { _storesWantBg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Foreground 2
        /// </summary>
        public Color StoresWantFg2
        {
            //Get
            get { return _storesWantFg2; }

            //Set
            set { _storesWantFg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Background 2
        /// </summary>
        public Color StoresWantBG2
        {
            //Get
            get { return _storesWantBg2; }

            //Set
            set { _storesWantBg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Foreground 3
        /// </summary>
        public Color StoresWantFg3
        {
            //Get
            get { return _storesWantFg3; }

            //Set
            set { _storesWantFg3 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Want Background 3
        /// </summary>
        public Color StoresWantBG3
        {
            //Get
            get { return _storesWantBg3; }

            //Set
            set { _storesWantBg3 = value; }
        }

        #endregion

        #region Public Stores Issue Functions

        /// <summary>
        /// Returns True If User Has Stores Issue Alerts Defined
        /// </summary>
        public bool UserStoresIssueAlertsDefined
        {
            //Return Value
            get { return _storesIssueAlertsDefined; }

            //Set Value
            set { _storesIssueAlertsDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Stores Issue Alert 1 Defined
        /// </summary>
        public bool UserStoresIssue1AlertDefined
        {
            //Return Value
            get { return _storesIssue1AlertDefined; }

            //Set Value
            set { _storesIssue1AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Stores Issue Alert 2 Defined
        /// </summary>
        public bool UserStoresIssue2AlertDefined
        {
            //Return Value
            get { return _storesIssue2AlertDefined; }

            //Set Value
            set { _storesIssue2AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Stores Issue Alert 3 Defined
        /// </summary>
        public bool UserStoresIssue3AlertDefined
        {
            //Return Value
            get { return _storesIssue3AlertDefined; }

            //Set Value
            set { _storesIssue3AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Stores Issue Alert 1
        /// </summary>
        public bool StoresIssue1ApplyDaysBefore
        {
            //Get Value
            get { return _storesIssue1ApplyDaysBefore; }

            //Set Value
            set { _storesIssue1ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Stores Issue Alert 2
        /// </summary>
        public bool StoresIssue2ApplyDaysBefore
        {
            //Get Value
            get { return _storesIssue2ApplyDaysBefore; }

            //Set Value
            set { _storesIssue2ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Stores Issue Alert 3
        /// </summary>
        public bool StoresIssue3ApplyDaysBefore
        {
            //Get Value
            get { return _storesIssue3ApplyDaysBefore; }

            //Set Value
            set { _storesIssue3ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Days Late Level 1
        /// </summary>
        public int StoresIssueDaysLate1
        {
            //Get Value
            get { return _storesIssueDaysLate1; }

            //Set Value
            set { _storesIssueDaysLate1 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Days Late Level 2
        /// </summary>
        public int StoresIssueDaysLate2
        {
            //Get Value
            get { return _storesIssueDaysLate2; }

            //Set Value
            set { _storesIssueDaysLate2 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Days Late Level 3
        /// </summary>
        public int StoresIssueDaysLate3
        {
            //Get Value
            get { return _storesIssueDaysLate3; }

            //Set Value
            set { _storesIssueDaysLate3 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Foreground 1
        /// </summary>
        public Color StoresIssueFg1
        {
            //Get
            get { return _storesIssueFg1; }

            //Set
            set { _storesIssueFg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Background 1
        /// </summary>
        public Color StoresIssueBG1
        {
            //Get
            get { return _storesIssueBg1; }

            //Set
            set { _storesIssueBg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Foreground 2
        /// </summary>
        public Color StoresIssueFg2
        {
            //Get
            get { return _storesIssueFg2; }

            //Set
            set { _storesIssueFg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Background 2
        /// </summary>
        public Color StoresIssueBG2
        {
            //Get
            get { return _storesIssueBg2; }

            //Set
            set { _storesIssueBg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Foreground 3
        /// </summary>
        public Color StoresIssueFg3
        {
            //Get
            get { return _storesIssueFg3; }

            //Set
            set { _storesIssueFg3 = value; }
        }

        /// <summary>
        /// Gets/Sets Stores Issue Background 3
        /// </summary>
        public Color StoresIssueBG3
        {
            //Get
            get { return _storesIssueBg3; }

            //Set
            set { _storesIssueBg3 = value; }
        }

        #endregion

        #region Public Purchase Order Functions

        /// <summary>
        /// Returns True If User Has Purchase Order Alerts Defined
        /// </summary>
        public bool UserPurchaseOrderAlertsDefined
        {
            //Return Value
            get { return _purchaseOrderAlertsDefined; }

            //Set Value
            set { _purchaseOrderAlertsDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Purchase Order Alert 1 Defined
        /// </summary>
        public bool UserPurchaseOrder1AlertDefined
        {
            //Return Value
            get { return _purchaseOrder1AlertDefined; }

            //Set Value
            set { _purchaseOrder1AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Purchase Order Alert 2 Defined
        /// </summary>
        public bool UserPurchaseOrder2AlertDefined
        {
            //Return Value
            get { return _purchaseOrder2AlertDefined; }

            //Set Value
            set { _purchaseOrder2AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If User Has Purchase Order Alert 3 Defined
        /// </summary>
        public bool UserPurchaseOrder3AlertDefined
        {
            //Return Value
            get { return _purchaseOrder3AlertDefined; }

            //Set Value
            set { _purchaseOrder3AlertDefined = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Purchase Order Alert 1
        /// </summary>
        public bool PurchaseOrder1ApplyDaysBefore
        {
            //Get Value
            get { return _purchaseOrder1ApplyDaysBefore; }

            //Set Value
            set { _purchaseOrder1ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Purchase Order Alert 2
        /// </summary>
        public bool PurchaseOrder2ApplyDaysBefore
        {
            //Get Value
            get { return _purchaseOrder2ApplyDaysBefore; }

            //Set Value
            set { _purchaseOrder2ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Returns True If Days Before Is To Be Applied For Purchase Order Alert 3
        /// </summary>
        public bool PurchaseOrder3ApplyDaysBefore
        {
            //Get Value
            get { return _purchaseOrder3ApplyDaysBefore; }

            //Set Value
            set { _purchaseOrder3ApplyDaysBefore = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Days Late Level 1
        /// </summary>
        public int PurchaseOrderDaysLate1
        {
            //Get Value
            get { return _purchaseOrderDaysLate1; }

            //Set Value
            set { _purchaseOrderDaysLate1 = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Days Late Level 2
        /// </summary>
        public int PurchaseOrderDaysLate2
        {
            //Get Value
            get { return _purchaseOrderDaysLate2; }

            //Set Value
            set { _purchaseOrderDaysLate2 = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Days Late Level 3
        /// </summary>
        public int PurchaseOrderDaysLate3
        {
            //Get Value
            get { return _purchaseOrderDaysLate3; }

            //Set Value
            set { _purchaseOrderDaysLate3 = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Foreground 1
        /// </summary>
        public Color PurchaseOrderFg1
        {
            //Get
            get { return _purchaseOrderFg1; }

            //Set
            set { _purchaseOrderFg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Background 1
        /// </summary>
        public Color PurchaseOrderBG1
        {
            //Get
            get { return _purchaseOrderBg1; }

            //Set
            set { _purchaseOrderBg1 = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Foreground 2
        /// </summary>
        public Color PurchaseOrderFg2
        {
            //Get
            get { return _purchaseOrderFg2; }

            //Set
            set { _purchaseOrderFg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Background 2
        /// </summary>
        public Color PurchaseOrderBG2
        {
            //Get
            get { return _purchaseOrderBg2; }

            //Set
            set { _purchaseOrderBg2 = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Foreground 3
        /// </summary>
        public Color PurchaseOrderFg3
        {
            //Get
            get { return _purchaseOrderFg3; }

            //Set
            set { _purchaseOrderFg3 = value; }
        }

        /// <summary>
        /// Gets/Sets Purchase Order Background 3
        /// </summary>
        public Color PurchaseOrderBG3
        {
            //Get
            get { return _purchaseOrderBg3; }

            //Set
            set { _purchaseOrderBg3 = value; }
        }

        #endregion

        #region Public Inventory QOH Alert Functions

        /// <summary>
        /// Returns True If User Has Storeroom QOH Alerts Defined
        /// </summary>
        public bool UserStoreroomQohAlertsDefined
        {
            //Return Value
            get { return _storeroomQohAlertsDefined; }

            //Set Value
            set { _storeroomQohAlertsDefined = value; }
        }

        /// <summary>
        /// Gets/Sets Storeroom Parts QOH Foreground 1
        /// </summary>
        public Color SrQohfg
        {
            //Get
            get { return _storeroomQohfg; }

            //Set
            set { _storeroomQohfg = value; }
        }

        /// <summary>
        /// Gets/Sets Storeroom Parts QOH Background 1
        /// </summary>
        public Color SrQohbg
        {
            //Get
            get { return _storeroomQohbg; }

            //Set
            set { _storeroomQohbg = value; }
        }

        #endregion

        #region Public Inventory ROP Alert Functions

        /// <summary>
        /// Returns True If User Has Storeroom ROP Alerts Defined
        /// </summary>
        public bool UserStoreroomRopAlertsDefined
        {
            //Return Value
            get { return _storeroomRopAlertsDefined; }

            //Set Value
            set { _storeroomRopAlertsDefined = value; }
        }

        public int SystemLeadTimeDefault
        {
            //Return Value
            get { return _systemLeadTimeDefault; }

            //Set Value
            set { _systemLeadTimeDefault = value; }
        }

        /// <summary>
        /// Gets/Sets Storeroom Parts ROP Foreground 1
        /// </summary>
        public Color SrRopfg
        {
            //Get
            get { return _storeroomRopfg; }

            //Set
            set { _storeroomRopfg = value; }
        }

        /// <summary>
        /// Gets/Sets Storeroom Parts ROP Background 1
        /// </summary>
        public Color SrRopbg
        {
            //Get
            get { return _storeroomRopbg; }

            //Set
            set { _storeroomRopbg = value; }
        }

        #endregion

        #region Industry Settings

        private bool _isFacIndustry;
        public bool IsFacIndustry
        {
            //Get
            get { return _isFacIndustry; }

            //Set
            set { _isFacIndustry = value; }
        }

        private bool _isDotIndustry;
        public bool IsDotIndustry
        {
            //Get
            get { return _isDotIndustry; }

            //Set
            set { _isDotIndustry = value; }
        }

        private bool _isMfgIndustry;
        public bool IsMfgIndustry
        {
            //Get
            get { return _isMfgIndustry; }

            //Set
            set { _isMfgIndustry = value; }
        }

        #endregion

        public void ResetError()
        {
            _sLastError = "";
        }

        public void UpdateTimestamp()
        {
            _dtLastCommandRequested = DateTime.Now;
        }

        public bool PerformLogin()
        {
            _sLastError = "";
            _bIsLoggedIn = false;
            PasswordHasExpired = false;
            var oLogInUser = new UserDatabaseLogon(_currentDbConnectionString, _useWebBackend);

            try
            {
                if ((_currentDbConnectionString != "") && (_dbVersionToMatch != ""))
                {
                    oLogInUser = new UserDatabaseLogon(_currentDbConnectionString,
                                                       _useWebBackend);
                    //Setup the connection type...
                    switch (_useWebBackend)
                    {
                        case true:
                            {
                                //Assume the default for now:
                                ConnectionType = DbBackendConnectonType.SqlWebService;
                                break;
                            }
                        case false:
                            {
                                //Assume the default for now:
                                ConnectionType = DbBackendConnectonType.Sql;
                                break;
                            }
                    }
                    oLogInUser.DbConnectionType = ConnectionType;

                    if (oLogInUser.LogUserIn(_sUsername, _sPassword, false))
                    {
                        if (oLogInUser.InvalidLogin == false)
                        {
                            //
                            //The user has successfully been found
                            //in the desired database.
                            //
                            //Now check the version of the exe
                            //against the version contained in the database
                            //
                            var oVersion = new MpetSystemSettings(_currentDbConnectionString, _useWebBackend)
                            {
                                DbConnectionType = ConnectionType
                            };
                            if (oVersion.GetExeVersionToMatch())
                            {
                                if (oVersion.DatabaseVersion != _dbVersionToMatch)
                                {
                                    _sLastError = "The Expected Database Version Is " + _dbVersionToMatch + ".\n" +
                                                 "The Connecting Database Is Version " + oVersion.DatabaseVersion + ".\n" +
                                                 "Please Contact Your Administrator.";
                                }
                                else
                                {
                                    //_bIsLoggedIn = true;
                                    //_nUserID = oLogInUser.UserID;
                                    //_nAreaID = oLogInUser.NAreaID;
                                    //_sFullName = oLogInUser.FirstName;
                                    //_sFullName = _sFullName.Trim() + " " + oLogInUser.LastName;
                                    //_sFullName = _sFullName.Trim();
                                    //PasswordExpires = oLogInUser.PasswordExpiresSetting;
                                    //PasswordExpirationDate = oLogInUser.PasswordExpirationDate;
                                    //PasswordExpiresIn = oLogInUser.NumberOfDaysUntilExpiration;
                                    //GroupID = oLogInUser.GroupID;
                                    //DefaultOverheadRate = oLogInUser.DefaultOverheadRate;
                                    //SuperGroupUser = oLogInUser.SuperGroupUser;

                                    //LoadDefaultAlerts();

                                    _bIsLoggedIn = true;
                                    _nUserID = oLogInUser.UserID;
                                    _nAreaID = oLogInUser.NAreaID;
                                    _sFullName = oLogInUser.FirstName;
                                    _sFullName = _sFullName.Trim() + " " + oLogInUser.LastName;
                                    _sFullName = _sFullName.Trim();
                                    _sFirstName = oLogInUser.FirstName;
                                    _sLastName = oLogInUser.LastName;
                                    PasswordExpires = oLogInUser.PasswordExpiresSetting;
                                    PasswordExpirationDate = oLogInUser.PasswordExpirationDate;
                                    PasswordExpiresIn = oLogInUser.NumberOfDaysUntilExpiration;
                                    GroupID = oLogInUser.GroupID;
                                    DefaultStoreroom = oLogInUser.StoreroomID;
                                    SuperGroupUser = oLogInUser.SuperGroupUser;
                                    _phoneNumber = oLogInUser.WorkPhone;
                                    _areaid = oLogInUser.AreaID;
                                    ShiftID = oLogInUser.ShiftID;
                                    LaborClassID = oLogInUser.LaborClassID;
                                    _useSystemReports = oLogInUser.UseSystemReports;
                                    ValidateStorerooms = oLogInUser.ValidateStorerooms;
                                    ValidateWorkOperations = oLogInUser.ValidateWorkOps;
                                    UsersStorerooms = oLogInUser.DtUsersStorerooms;
                                    UsersWorkOperations = oLogInUser.DtUsersWorkOps;
                                    DefaultOverheadRate = oLogInUser.DefaultOverheadRate;
                                    UseSimpleCostLinking = oLogInUser.UseSimpleCostLinking;

                                    //Cost Code
                                    RenameCostCode = oLogInUser.CostCodeRenamed;
                                    RequireCostCodeInput = oLogInUser.CostCodeRequired;
                                    RenameCostCodeTo = oLogInUser.CostCodeName;

                                    //Fund Source Code
                                    RenameFundSource = oLogInUser.FundSourceRenamed;
                                    RequireFundSourceInput = oLogInUser.FundSourceRequired;
                                    RenameFundSourceTo = oLogInUser.FundSourceName;

                                    //Work Order
                                    RenameWorkOrder = oLogInUser.WorkOrderRenamed;
                                    RequireWorkOrderInput = oLogInUser.WorkOrderRequired;
                                    RenameWorkOrderTo = oLogInUser.WorkOrderName;

                                    //Work Op
                                    RenameWorkOp = oLogInUser.WorkOpRenamed;
                                    RequireWorkOpInput = oLogInUser.WorkOpRequired;
                                    RenameWorkOpTo = oLogInUser.WorkOpName;

                                    //Org Code
                                    RenameOrgCode = oLogInUser.OrgCodeRenamed;
                                    RequireOrgCodeInput = oLogInUser.OrgCodeRequired;
                                    RenameOrgCodeTo = oLogInUser.OrgCodeName;

                                    //Fund Group
                                    RenameFundGroup = oLogInUser.FundGroupRenamed;
                                    RequireFundGroupInput = oLogInUser.FundGroupRequired;
                                    RenameFundGroupTo = oLogInUser.FundGroupName;

                                    //Control Section
                                    RenameControlSection = oLogInUser.CtlSectionRenamed;
                                    RequireControlSectionInput = oLogInUser.CtlSectionRequired;
                                    RenameControlSectionTo = oLogInUser.CtlSectionName;

                                    //Equip Num
                                    RenameEquipNumber = oLogInUser.EquipNumRenamed;
                                    RequireEquipNumberInput = oLogInUser.EquipNumRequired;
                                    RenameEquipNumberTo = oLogInUser.EquipNumName;

                                    //Hwy Route
                                    RenameHwyRoute = oLogInUser.HwyRouteRenamed;
                                    RequireHwyRouteInput = oLogInUser.HwyRouteRequired;
                                    RenameHwyRouteTo = oLogInUser.HwyRouteName;

                                    //Lead Time
                                    SystemLeadTimeDefault = oLogInUser.SystemLeadTimeDefault;

                                    //Taxable Environment
                                    TaxableEnvironment = oLogInUser.TaxableEnvironment;

                                    //Azure Hosted Attachments
                                    AzureHostedAttachments = (oLogInUser.AzureHostedAttachments == "Y");

                                    //LockStartDateToAdmin
                                    LockStartDateToAdmin = (oLogInUser.LockStartDateToAdmin == "Y");

                                    //LockReturnWithinToAdmin
                                    LockReturnWithinToAdmin = (oLogInUser.LockReturnWithinToAdmin == "Y");

                                    //LockSupervisorToAdmin
                                    LockSupervisorToAdmin = (oLogInUser.LockSupervisorToAdmin == "Y");

                                    //LockStartDateToSupervisor
                                    LockStartDateToSupervisor = (oLogInUser.LockStartDateToSupervisor == "Y");

                                    //LockReturnWithinToSupervisor
                                    LockReturnWithinToSupervisor = (oLogInUser.LockReturnWithinToSupervisor == "Y");

                                    //LockSupervisorToSupervisor
                                    LockSupervisorToSupervisor = (oLogInUser.LockSupervisorToSupervisor == "Y");

                                    //UserInAdminTemplate
                                    UserInAdminTemplate = (oLogInUser.UserInAdminTemplate == "Y");

                                    //UserInSupervisorTemplate
                                    UserInSupervisorTemplate = (oLogInUser.UserInSupervisorTemplate == "Y");

                                    //UserIsBuyer
                                    UserIsBuyer = oLogInUser.UserIsBuyer;

                                    //UserIsSIReviewer
                                    UserIsSIReviewer = oLogInUser.UserIsSiReviewer;

                                    //UserIsReqReviewer
                                    UserIsReqReviewer = oLogInUser.UserIsReqReviewer;

                                    //UserIsPOReviewer
                                    UserIsPOReviewer = oLogInUser.UserIsPoReviewer;

                                    //UserIsReqSuperReviewer
                                    UserIsReqSuperReviewer = oLogInUser.UserIsReqSuperReviewer;

                                    //UserIsJobPoster
                                    UserIsJobPoster = oLogInUser.UserIsJobPoster;

                                    //UserIsJobUnposter
                                    UserIsJobUnposter = oLogInUser.UserIsJobUnposter;

                                    //UserIsTimeBatchReviewer
                                    UserIsTimeBatchReviewer = oLogInUser.UserIsTimeBatchReviewer;

                                    //UserIsTimeBatchApprover
                                    UserIsTimeBatchApprover = oLogInUser.UserIsTimeBatchApprover;

                                    IsFacIndustry = oLogInUser.IsFacIndustry;
                                    IsMfgIndustry = oLogInUser.IsMfgIndustry;
                                    IsDotIndustry = oLogInUser.IsDotIndustry;

                                    LoadDefaultAlerts();
                                }
                            }
                            else
                            {
                                //There was a failure getting the version #
                                _sLastError = "Error Getting DB Version #:" + oVersion.LastError;
                            }
                        }
                        else
                            _sLastError = oLogInUser.LastError;
                    }
                    else
                    {
                        //An Error occured
                        _sLastError = oLogInUser.LastError;
                    }
                }
                else
                {
                    _sLastError = _currentDbConnectionString == "" ? "Connection String Not Specified" : "EXE Version not specified";
                }
            }
            catch (Exception ex)
            {
                _sLastError = ex.Message;
            }


            //Insure the password information is present!
            if (_sLastError == "Password has expired")
            {
                PasswordHasExpired = true;
                _bIsLoggedIn = false;
                _nUserID = oLogInUser.UserID;
                _nAreaID = oLogInUser.NAreaID;
                _sFullName = oLogInUser.FirstName;
                _sFullName = _sFullName.Trim() + " " + oLogInUser.LastName;
                _sFullName = _sFullName.Trim();
                PasswordExpires = oLogInUser.PasswordExpiresSetting;
                PasswordExpirationDate = oLogInUser.PasswordExpirationDate;
                PasswordExpiresIn = oLogInUser.NumberOfDaysUntilExpiration;
                GroupID = oLogInUser.GroupID;
            }

            return ((_bIsLoggedIn) && (_sLastError == ""));
        }

        public bool PerformDemoLogin(string connection)
        {
            _sLastError = "";
            _bIsLoggedIn = false;
            PasswordHasExpired = false;
            var oLogInUser = new UserDatabaseLogon(connection, _useWebBackend);

            try
            {
                if ((connection != "") && (_dbVersionToMatch != ""))
                {
                    oLogInUser = new UserDatabaseLogon(connection,
                                                       _useWebBackend);
                    //Setup the connection type...
                    switch (_useWebBackend)
                    {
                        case true:
                            {
                                //Assume the default for now:
                                ConnectionType = DbBackendConnectonType.SqlWebService;
                                break;
                            }
                        case false:
                            {
                                //Assume the default for now:
                                ConnectionType = DbBackendConnectonType.Sql;
                                break;
                            }
                    }
                    oLogInUser.DbConnectionType = ConnectionType;

                    if (oLogInUser.LogUserIn(_sUsername, _sPassword, false))
                    {
                        if (oLogInUser.InvalidLogin == false)
                        {
                            //
                            //The user has successfully been found
                            //in the desired database.
                            //
                            //Now check the version of the exe
                            //against the version contained in the database
                            //
                            var oVersion = new MpetSystemSettings(connection, _useWebBackend)
                            {
                                DbConnectionType = ConnectionType
                            };
                            if (oVersion.GetExeVersionToMatch())
                            {
                                if (oVersion.DatabaseVersion != _dbVersionToMatch)
                                {
                                    _sLastError = "The Expected Database Version Is " + _dbVersionToMatch + ".\n" +
                                                 "The Connecting Database Is Version " + oVersion.DatabaseVersion + ".\n" +
                                                 "Please Contact Your Administrator.";
                                }
                                else
                                {
                                    //_bIsLoggedIn = true;
                                    //_nUserID = oLogInUser.UserID;
                                    //_nAreaID = oLogInUser.NAreaID;
                                    //_sFullName = oLogInUser.FirstName;
                                    //_sFullName = _sFullName.Trim() + " " + oLogInUser.LastName;
                                    //_sFullName = _sFullName.Trim();
                                    //PasswordExpires = oLogInUser.PasswordExpiresSetting;
                                    //PasswordExpirationDate = oLogInUser.PasswordExpirationDate;
                                    //PasswordExpiresIn = oLogInUser.NumberOfDaysUntilExpiration;
                                    //GroupID = oLogInUser.GroupID;
                                    //SuperGroupUser = oLogInUser.SuperGroupUser;

                                    _bIsLoggedIn = true;
                                    _nUserID = oLogInUser.UserID;
                                    _nAreaID = oLogInUser.NAreaID;
                                    _sFullName = oLogInUser.FirstName;
                                    _sFullName = _sFullName.Trim() + " " + oLogInUser.LastName;
                                    _sFullName = _sFullName.Trim();
                                    PasswordExpires = oLogInUser.PasswordExpiresSetting;
                                    PasswordExpirationDate = oLogInUser.PasswordExpirationDate;
                                    PasswordExpiresIn = oLogInUser.NumberOfDaysUntilExpiration;
                                    GroupID = oLogInUser.GroupID;
                                    DefaultStoreroom = oLogInUser.StoreroomID;
                                    SuperGroupUser = oLogInUser.SuperGroupUser;
                                    _phoneNumber = oLogInUser.WorkPhone;
                                    _areaid = oLogInUser.AreaID;
                                    ShiftID = oLogInUser.ShiftID;
                                    LaborClassID = oLogInUser.LaborClassID;
                                    _useSystemReports = oLogInUser.UseSystemReports;
                                    ValidateStorerooms = oLogInUser.ValidateStorerooms;
                                    ValidateWorkOperations = oLogInUser.ValidateWorkOps;
                                    UsersStorerooms = oLogInUser.DtUsersStorerooms;
                                    UsersWorkOperations = oLogInUser.DtUsersWorkOps;
                                    DefaultOverheadRate = oLogInUser.DefaultOverheadRate;
                                    UseSimpleCostLinking = oLogInUser.UseSimpleCostLinking;

                                    //Cost Code
                                    RenameCostCode = oLogInUser.CostCodeRenamed;
                                    RequireCostCodeInput = oLogInUser.CostCodeRequired;
                                    RenameCostCodeTo = oLogInUser.CostCodeName;

                                    //Fund Source Code
                                    RenameFundSource = oLogInUser.FundSourceRenamed;
                                    RequireFundSourceInput = oLogInUser.FundSourceRequired;
                                    RenameFundSourceTo = oLogInUser.FundSourceName;

                                    //Work Order
                                    RenameWorkOrder = oLogInUser.WorkOrderRenamed;
                                    RequireWorkOrderInput = oLogInUser.WorkOrderRequired;
                                    RenameWorkOrderTo = oLogInUser.WorkOrderName;

                                    //Work Op
                                    RenameWorkOp = oLogInUser.WorkOpRenamed;
                                    RequireWorkOpInput = oLogInUser.WorkOpRequired;
                                    RenameWorkOpTo = oLogInUser.WorkOpName;

                                    //Org Code
                                    RenameOrgCode = oLogInUser.OrgCodeRenamed;
                                    RequireOrgCodeInput = oLogInUser.OrgCodeRequired;
                                    RenameOrgCodeTo = oLogInUser.OrgCodeName;

                                    //Fund Group
                                    RenameFundGroup = oLogInUser.FundGroupRenamed;
                                    RequireFundGroupInput = oLogInUser.FundGroupRequired;
                                    RenameFundGroupTo = oLogInUser.FundGroupName;

                                    //Control Section
                                    RenameControlSection = oLogInUser.CtlSectionRenamed;
                                    RequireControlSectionInput = oLogInUser.CtlSectionRequired;
                                    RenameControlSectionTo = oLogInUser.CtlSectionName;

                                    //Equip Num
                                    RenameEquipNumber = oLogInUser.EquipNumRenamed;
                                    RequireEquipNumberInput = oLogInUser.EquipNumRequired;
                                    RenameEquipNumberTo = oLogInUser.EquipNumName;

                                    //Hwy Route
                                    RenameHwyRoute = oLogInUser.HwyRouteRenamed;
                                    RequireHwyRouteInput = oLogInUser.HwyRouteRequired;
                                    RenameHwyRouteTo = oLogInUser.HwyRouteName;

                                    //Lead Time
                                    SystemLeadTimeDefault = oLogInUser.SystemLeadTimeDefault;

                                    //Taxable Environment
                                    TaxableEnvironment = oLogInUser.TaxableEnvironment;

                                    //Azure Hosted Attachments
                                    AzureHostedAttachments = (oLogInUser.AzureHostedAttachments == "Y");

                                    //LockStartDateToAdmin
                                    LockStartDateToAdmin = (oLogInUser.LockStartDateToAdmin == "Y");

                                    //LockReturnWithinToAdmin
                                    LockReturnWithinToAdmin = (oLogInUser.LockReturnWithinToAdmin == "Y");

                                    //LockSupervisorToAdmin
                                    LockSupervisorToAdmin = (oLogInUser.LockSupervisorToAdmin == "Y");

                                    //LockStartDateToSupervisor
                                    LockStartDateToSupervisor = (oLogInUser.LockStartDateToSupervisor == "Y");

                                    //LockReturnWithinToSupervisor
                                    LockReturnWithinToSupervisor = (oLogInUser.LockReturnWithinToSupervisor == "Y");

                                    //LockSupervisorToSupervisor
                                    LockSupervisorToSupervisor = (oLogInUser.LockSupervisorToSupervisor == "Y");

                                    //UserInAdminTemplate
                                    UserInAdminTemplate = (oLogInUser.UserInAdminTemplate == "Y");

                                    //UserInSupervisorTemplate
                                    UserInSupervisorTemplate = (oLogInUser.UserInSupervisorTemplate == "Y");

                                    //UserIsBuyer
                                    UserIsBuyer = oLogInUser.UserIsBuyer;

                                    //UserIsSIReviewer
                                    UserIsSIReviewer = oLogInUser.UserIsSiReviewer;

                                    //UserIsReqReviewer
                                    UserIsReqReviewer = oLogInUser.UserIsReqReviewer;

                                    //UserIsPOReviewer
                                    UserIsPOReviewer = oLogInUser.UserIsPoReviewer;

                                    //UserIsReqSuperReviewer
                                    UserIsReqSuperReviewer = oLogInUser.UserIsReqSuperReviewer;

                                    //UserIsJobPoster
                                    UserIsJobPoster = oLogInUser.UserIsJobPoster;

                                    //UserIsJobUnposter
                                    UserIsJobUnposter = oLogInUser.UserIsJobUnposter;

                                    //UserIsTimeBatchReviewer
                                    UserIsTimeBatchReviewer = oLogInUser.UserIsTimeBatchReviewer;

                                    //UserIsTimeBatchApprover
                                    UserIsTimeBatchApprover = oLogInUser.UserIsTimeBatchApprover;

                                    LoadDefaultAlerts();
                                }
                            }
                            else
                            {
                                //There was a failure getting the version #
                                _sLastError = "Error Getting DB Version #:" + oVersion.LastError;
                            }
                        }
                        else
                            _sLastError = oLogInUser.LastError;
                    }
                    else
                    {
                        //An Error occured
                        _sLastError = oLogInUser.LastError;
                    }
                }
                else
                {
                    _sLastError = connection == "" ? "Connection String Not Specified" : "EXE Version not specified";
                }
            }
            catch (Exception ex)
            {
                _sLastError = ex.Message;
            }


            //Insure the password information is present!
            if (_sLastError == "Password has expired")
            {
                PasswordHasExpired = true;
                _bIsLoggedIn = false;
                _nUserID = oLogInUser.UserID;
                _nAreaID = oLogInUser.NAreaID;
                _sFullName = oLogInUser.FirstName;
                _sFullName = _sFullName.Trim() + " " + oLogInUser.LastName;
                _sFullName = _sFullName.Trim();
                PasswordExpires = oLogInUser.PasswordExpiresSetting;
                PasswordExpirationDate = oLogInUser.PasswordExpirationDate;
                PasswordExpiresIn = oLogInUser.NumberOfDaysUntilExpiration;
                GroupID = oLogInUser.GroupID;
            }

            return ((_bIsLoggedIn) && (_sLastError == ""));
        }

        /// <summary>
        /// ReloadDefaultAlerts: Reloads Default Alerts
        /// </summary>
        public void LoadDefaultAlerts()
        {
            #region Set Up MPETDS Objects

            //Job Alerts
            _oJobAlerts = new JobAlertDefaults(_nUserID, DatabaseConnnectionString, UsingWeb);

            //Maintenance Object Alerts
            _oMoAlerts = new MaintenanceObjectAlerts(_nUserID, DatabaseConnnectionString, UsingWeb);

            //Storeroom Part Alerts
            _oSrpAlerts = new StoreroomPartsAlerts(_nUserID, DatabaseConnnectionString, UsingWeb);

            //Stores Want Alerts
            _oSwlAlerts = new StoresWantAlerts(_nUserID, DatabaseConnnectionString, UsingWeb);

            //Stores Issue Alerts
            _oSiAlerts = new StoresIssueAlerts(_nUserID, DatabaseConnnectionString, UsingWeb);

            //Purchase Order Alerts
            _oPoAlerts = new PurchaseOrderAlerts(_nUserID, DatabaseConnnectionString, UsingWeb);

            #endregion

            #region Load Job Alerts

            if (!_oJobAlerts.LoadData())
            {
                UserAlertsDefined = false;
            }

            if (_oJobAlerts.Ds.Tables.Count > 0)
            {
                UserAlertsDefined = _oJobAlerts.Ds.Tables[0].Rows.Count > 0;
            }
            else
            {
                UserAlertsDefined = false;
            }

            for (var i = 1; i <= 6; i++)
            {
                _oJobAlerts.SetLevelValues(i);
                switch (i)
                {
                    case 1:
                        {
                            if (_oJobAlerts.Active)
                            {
                                //Set Defined Flag
                                ReqLevel1AlertDefined = true;

                                //Set Apply Before Flag
                                JobRequestApplyDaysBefore1 = _oJobAlerts.ApplyDaysBeforeReq;

                                //Set BG
                                JobRequestBg1 = _oJobAlerts.BackgroundColor;

                                //Set FG
                                JobRequestFg1 = _oJobAlerts.ForegroundColor;

                                //Set Days Late
                                JobRequestDaysLate1 = _oJobAlerts.DaysLate;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (_oJobAlerts.Active)
                            {
                                //Set Defined Flag
                                ReqLevel2AlertDefined = true;

                                //Set Apply Before Flag
                                JobRequestApplyDaysBefore2 = _oJobAlerts.ApplyDaysBeforeReq;

                                //Set BG
                                JobRequestBg2 = _oJobAlerts.BackgroundColor;

                                //Set FG
                                JobRequestFg2 = _oJobAlerts.ForegroundColor;

                                //Set Days Late
                                JobRequestDaysLate2 = _oJobAlerts.DaysLate;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (_oJobAlerts.Active)
                            {
                                //Set Defined Flag
                                ReqLevel3AlertDefined = true;

                                //Set Apply Before Flag
                                JobRequestApplyDaysBefore3 = _oJobAlerts.ApplyDaysBeforeReq;

                                //Set BG
                                JobRequestBg3 = _oJobAlerts.BackgroundColor;

                                //Set FG
                                JobRequestFg3 = _oJobAlerts.ForegroundColor;

                                //SEt Days Late
                                JobRequestDaysLate3 = _oJobAlerts.DaysLate;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (_oJobAlerts.Active)
                            {
                                //Set Defined Flag
                                PlannedLevel1AlertDefine = true;

                                //SEt Apply Before Flag
                                PlannedApplyDaysBefore1 = _oJobAlerts.ApplyDaysBefore;

                                //Set Based On Startdate
                                PlannedJobsBasedOnStartDate1 = (_oJobAlerts.BasedOnJobIDType == 0);

                                //Set BG
                                PlannedJobBg1 = _oJobAlerts.BackgroundColor;

                                //Set FG
                                PlannedJobFg1 = _oJobAlerts.ForegroundColor;

                                //Set Days Late
                                PlannedJobDaysLate1 = _oJobAlerts.DaysLate;

                                //Set Use Return Within
                                PlannedUseReturnWithin1 = _oJobAlerts.UseReturnWithIn;
                            }
                            break;
                        }
                    case 5:
                        {
                            if (_oJobAlerts.Active)
                            {
                                //Set Defined Flag
                                PlannedLevel2AlertDefined = true;

                                //SEt Apply Before Flag
                                PlannedApplyDaysBefore2 = _oJobAlerts.ApplyDaysBefore;

                                //Set Based On Startdate
                                PlannedJobsBasedOnStartDate2 = (_oJobAlerts.BasedOnJobIDType == 0);

                                //Set BG
                                PlannedJobBg2 = _oJobAlerts.BackgroundColor;

                                //Set FG
                                PlannedJobFg2 = _oJobAlerts.ForegroundColor;

                                //Set Days Late
                                PlannedJobDaysLate2 = _oJobAlerts.DaysLate;

                                //Set Use Return Within
                                PlannedUseReturnWithin2 = _oJobAlerts.UseReturnWithIn;
                            }
                            break;
                        }
                    case 6:
                        {
                            if (_oJobAlerts.Active)
                            {
                                //Set Defined Flag
                                PlannedLevel3AlertDefined = true;

                                //SEt Apply Before Flag
                                PlannedApplyDaysBefore3 = _oJobAlerts.ApplyDaysBefore;

                                //Set Based On Startdate
                                PlannedJobsBasedOnStartDate3 = (_oJobAlerts.BasedOnJobIDType == 0);

                                //Set BG
                                PlannedJobBg3 = _oJobAlerts.BackgroundColor;

                                //Set FG
                                PlannedJobFg3 = _oJobAlerts.ForegroundColor;

                                //SEt Days Late
                                PlannedJobDaysLate3 = _oJobAlerts.DaysLate;

                                //Set Use Return Within
                                PlannedUseReturnWithin3 = _oJobAlerts.UseReturnWithIn;
                            }
                            break;
                        }
                    default:
                        {
                            //Do Nothing
                            break;
                        }
                }
            }

            #endregion

            #region Load Maintenance Object Alerts

            //Load Data
            if (_oMoAlerts.LoadData())
            {
                //Check Table Count
                if (_oMoAlerts.Ds.Tables.Count > 0)
                {
                    //Check Row Count
                    if (_oMoAlerts.Ds.Tables[0].Rows.Count > 0)
                    {
                        //Loop Through Alerts
                        for (var i = 0; i < _oMoAlerts.Ds.Tables[0].Rows.Count; i++)
                        {
                            //Determine Alert Type
                            switch (_oMoAlerts.Ds.Tables[0].Rows[i][1].ToString())
                            {
                                case "0":
                                    {
                                        //Build Date Alert

                                        //Determine If Active
                                        if (Convert.ToBoolean(_oMoAlerts.Ds.Tables[0].Rows[i][3]))
                                        {
                                            //Set Main Flag
                                            _moBuildAlertsDefined = true;

                                            //Determine Alert Level
                                            switch (_oMoAlerts.Ds.Tables[0].Rows[i][2].ToString())
                                            {
                                                case "1":
                                                    {
                                                        //Alert Level 1

                                                        //Set Alert Level Flag
                                                        _moBuildAlert1Defined = true;

                                                        //Set Days Late
                                                        _moBuildDaysLate1 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moBuildBg1 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moBuildFg1 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moBuild1ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                case "2":
                                                    {
                                                        //Alert Level 2

                                                        //Set Alert Level Flag
                                                        _moBuildAlert2Defined = true;

                                                        //Set Days Late
                                                        _moBuildDaysLate2 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moBuildBg2 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moBuildFg2 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moBuild2ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                case "3":
                                                    {
                                                        //Alert Level 3

                                                        //Set Alert Level Flag
                                                        _moBuildAlert3Defined = true;

                                                        //Set Days Late
                                                        _moBuildDaysLate3 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moBuildBg3 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moBuildFg3 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moBuild3ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                default:
                                                    {
                                                        //Do Nothing
                                                        break;
                                                    }
                                            }
                                        }
                                        break;
                                    }
                                case "1":
                                    {
                                        //In-Service Date Alert

                                        //Determine If Active
                                        if (Convert.ToBoolean(_oMoAlerts.Ds.Tables[0].Rows[i][3]))
                                        {
                                            //Set Main Flag
                                            _moServiceAlertsDefined = true;

                                            //Determine Alert Level
                                            switch (_oMoAlerts.Ds.Tables[0].Rows[i][2].ToString())
                                            {
                                                case "1":
                                                    {
                                                        //Alert Level 1

                                                        //Set Alert Level Flag
                                                        _moService1AlertDefined = true;

                                                        //Set Days Late
                                                        _moServiceDaysLate1 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moServiceBg1 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moServiceFg1 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moService1ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                case "2":
                                                    {
                                                        //Alert Level 2

                                                        //Set Alert Level Flag
                                                        _moService2AlertDefined = true;

                                                        //Set Days Late
                                                        _moServiceDaysLate2 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moServiceBg2 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moServiceFg2 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moService2ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                case "3":
                                                    {
                                                        //Alert Level 3

                                                        //Set Alert Level Flag
                                                        _moService3AlertDefined = true;

                                                        //Set Days Late
                                                        _moServiceDaysLate3 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moServiceBg3 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moServiceFg3 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moService3ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                default:
                                                    {
                                                        //Do Nothing
                                                        break;
                                                    }
                                            }
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        //Life Cycle Date Alert

                                        //Determine If Active
                                        if (Convert.ToBoolean(_oMoAlerts.Ds.Tables[0].Rows[i][3]))
                                        {
                                            //Set Main Flag
                                            _moCycleAlertsDefined = true;

                                            //Determine Alert Level
                                            switch (_oMoAlerts.Ds.Tables[0].Rows[i][2].ToString())
                                            {
                                                case "1":
                                                    {
                                                        //Alert Level 1

                                                        //Set Alert Level Flag
                                                        _moCycle1AlertDefined = true;

                                                        //Set Days Late
                                                        _moCycleDaysLate1 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moCycleBg1 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moCycleFg1 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moCycle1ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                case "2":
                                                    {
                                                        //Alert Level 2

                                                        //Set Alert Level Flag
                                                        _moCycle2AlertDefined = true;

                                                        //Set Days Late
                                                        _moCycleDaysLate2 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moCycleBg2 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moCycleFg2 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moCycle2ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                case "3":
                                                    {
                                                        //Alert Level 3

                                                        //Set Alert Level Flag
                                                        _moCycle3AlertDefined = true;

                                                        //Set Days Late
                                                        _moCycleDaysLate3 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moCycleBg3 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moCycleFg3 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moCycle3ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                default:
                                                    {
                                                        //Do Nothing
                                                        break;
                                                    }
                                            }
                                        }
                                        break;
                                    }
                                case "3":
                                    {
                                        //Expiration Date Alert

                                        //Determine If Active
                                        if (Convert.ToBoolean(_oMoAlerts.Ds.Tables[0].Rows[i][3]))
                                        {
                                            //Set Main Flag
                                            _moExpAlertsDefined = true;

                                            //Determine Alert Level
                                            switch (_oMoAlerts.Ds.Tables[0].Rows[i][2].ToString())
                                            {
                                                case "1":
                                                    {
                                                        //Alert Level 1

                                                        //Set Alert Level Flag
                                                        _moExp1AlertDefined = true;

                                                        //Set Days Late
                                                        _moExpDaysLate1 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moExpBg1 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moExpFg1 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moExp1ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                case "2":
                                                    {
                                                        //Alert Level 2

                                                        //Set Alert Level Flag
                                                        _moExp2AlertDefined = true;

                                                        //Set Days Late
                                                        _moExpDaysLate2 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moExpBg2 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moExpFg2 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moExp2ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                case "3":
                                                    {
                                                        //Alert Level 3

                                                        //Set Alert Level Flag
                                                        _moExp3AlertDefined = true;

                                                        //Set Days Late
                                                        _moExpDaysLate3 =
                                                            Convert.ToInt32(_oMoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                                        //Set Background
                                                        _moExpBg3 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                                        //Set Foreground
                                                        _moExpFg3 =
                                                            Color.FromArgb(
                                                                Convert.ToInt32(
                                                                    _oMoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                                        //Set Apply Days Before
                                                        _moExp3ApplyDaysBefore =
                                                            (_oMoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                                        break;
                                                    }
                                                default:
                                                    {
                                                        //Do Nothing
                                                        break;
                                                    }
                                            }
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        //Do Nothing
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        //Set Flags To False
                        _moBuildAlertsDefined = false;
                        _moServiceAlertsDefined = false;
                        _moExpAlertsDefined = false;
                        _moCycleAlertsDefined = false;
                    }
                }
                else
                {
                    //Set Flags To False
                    _moBuildAlertsDefined = false;
                    _moServiceAlertsDefined = false;
                    _moExpAlertsDefined = false;
                    _moCycleAlertsDefined = false;
                }
            }
            else
            {
                //Set Flags To False
                _moBuildAlertsDefined = false;
                _moServiceAlertsDefined = false;
                _moExpAlertsDefined = false;
                _moCycleAlertsDefined = false;
            }

            #endregion

            #region Load Storeroom Part Alerts

            //Load Data
            if (_oSrpAlerts.LoadData())
            {
                //Check Table Count
                if (_oSrpAlerts.Ds.Tables.Count > 0)
                {
                    //Check Row Count
                    if (_oSrpAlerts.Ds.Tables[0].Rows.Count > 0)
                    {
                        //Loop Through Alerts
                        for (var i = 0; i < _oSrpAlerts.Ds.Tables[0].Rows.Count; i++)
                        {
                            //Determine Type
                            switch (_oSrpAlerts.Ds.Tables[0].Rows[i][1].ToString())
                            {
                                case "0":
                                    {
                                        //QOH Alert

                                        //Determine If Active
                                        if (Convert.ToBoolean(_oSrpAlerts.Ds.Tables[0].Rows[i][3]))
                                        {
                                            //Set Flag
                                            _storeroomQohAlertsDefined = true;

                                            //Set Background
                                            _storeroomQohbg =
                                                Color.FromArgb(
                                                    Convert.ToInt32(_oSrpAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Forground
                                            _storeroomQohfg =
                                                Color.FromArgb(
                                                    Convert.ToInt32(_oSrpAlerts.Ds.Tables[0].Rows[i][4].ToString()));
                                        }
                                        else
                                        {
                                            //Set Flag To False
                                            _storeroomQohAlertsDefined = false;
                                        }

                                        break;
                                    }
                                case "1":
                                    {
                                        //ROP Alert

                                        //Determine If Active
                                        if (Convert.ToBoolean(_oSrpAlerts.Ds.Tables[0].Rows[i][3]))
                                        {
                                            //Set Flag
                                            _storeroomRopAlertsDefined = true;

                                            //Set Background
                                            _storeroomRopbg =
                                                Color.FromArgb(
                                                    Convert.ToInt32(_oSrpAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Forground
                                            _storeroomRopfg =
                                                Color.FromArgb(
                                                    Convert.ToInt32(_oSrpAlerts.Ds.Tables[0].Rows[i][4].ToString()));
                                        }
                                        else
                                        {
                                            //Set Flag To False
                                            _storeroomRopAlertsDefined = false;
                                        }

                                        break;
                                    }
                                default:
                                    {
                                        //Do Nothing
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        //Set Flags To False
                        _storeroomQohAlertsDefined = false;
                        _storeroomRopAlertsDefined = false;
                    }
                }
                else
                {
                    //Set Flags To False
                    _storeroomQohAlertsDefined = false;
                    _storeroomRopAlertsDefined = false;
                }
            }
            else
            {
                //Set Flags To False
                _storeroomQohAlertsDefined = false;
                _storeroomRopAlertsDefined = false;
            }

            #endregion

            #region Load Stores Want Alerts

            //Load Data
            if (_oSwlAlerts.LoadData())
            {
                //Check Table Count
                if (_oSwlAlerts.Ds.Tables.Count > 0)
                {
                    //Check Row Count
                    if (_oSwlAlerts.Ds.Tables[0].Rows.Count > 0)
                    {
                        //Loop Through Alerts
                        for (var i = 0; i < _oSwlAlerts.Ds.Tables[0].Rows.Count; i++)
                        {
                            //Determine If Active
                            if (Convert.ToBoolean(_oSwlAlerts.Ds.Tables[0].Rows[i][3]))
                            {
                                //Set Main Flag
                                _storesWantAlertsDefined = true;

                                //Determine Alert Level
                                switch (_oSwlAlerts.Ds.Tables[0].Rows[i][2].ToString())
                                {
                                    case "1":
                                        {
                                            //Alert Level 1

                                            //Set Alert Level Flag
                                            _storesWant1AlertDefined = true;

                                            //Set Days Late
                                            _storesWantDaysLate1 =
                                                Convert.ToInt32(_oSwlAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _storesWantBg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSwlAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _storesWantFg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSwlAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _storesWant1ApplyDaysBefore =
                                                (_oSwlAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    case "2":
                                        {
                                            //Alert Level 2

                                            //Set Alert Level Flag
                                            _storesWant2AlertDefined = true;

                                            //Set Days Late
                                            _storesWantDaysLate2 =
                                                Convert.ToInt32(_oSwlAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _storesWantBg2 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSwlAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _storesWantFg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSwlAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _storesWant2ApplyDaysBefore =
                                                (_oSwlAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    case "3":
                                        {
                                            //Alert Level 3

                                            //Set Alert Level Flag
                                            _storesWant3AlertDefined = true;

                                            //Set Days Late
                                            _storesWantDaysLate3 =
                                                Convert.ToInt32(_oSwlAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _storesWantBg3 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSwlAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _storesWantFg3 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSwlAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _storesWant3ApplyDaysBefore =
                                                (_oSwlAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    default:
                                        {
                                            //Do Nothing
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    else
                    {
                        //Set Flags To False
                        _storesWantAlertsDefined = false;
                    }
                }
                else
                {
                    //Set Flags To False
                    _storesWantAlertsDefined = false;
                }
            }
            else
            {
                //Set Flags To False
                _storesWantAlertsDefined = false;
            }

            #endregion

            #region Load Stores Issue Alerts

            //Load Data
            if (_oSiAlerts.LoadData())
            {
                //Check Table Count
                if (_oSiAlerts.Ds.Tables.Count > 0)
                {
                    //Check Row Count
                    if (_oSiAlerts.Ds.Tables[0].Rows.Count > 0)
                    {
                        //Loop Through Alerts
                        for (var i = 0; i < _oSiAlerts.Ds.Tables[0].Rows.Count; i++)
                        {
                            //Determine If Active
                            if (Convert.ToBoolean(_oSiAlerts.Ds.Tables[0].Rows[i][3]))
                            {
                                //Set Main Flag
                                _storesIssueAlertsDefined = true;

                                //Determine Alert Level
                                switch (_oSiAlerts.Ds.Tables[0].Rows[i][2].ToString())
                                {
                                    case "1":
                                        {
                                            //Alert Level 1

                                            //Set Alert Level Flag

                                            _storesIssue1AlertDefined = true;

                                            //Set Days Late
                                            _storesIssueDaysLate1 =
                                                Convert.ToInt32(_oSiAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _storesIssueBg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSiAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _storesIssueFg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSiAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _storesIssue1ApplyDaysBefore =
                                                (_oSiAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    case "2":
                                        {
                                            //Alert Level 2

                                            //Set Alert Level Flag
                                            _storesIssue2AlertDefined = true;

                                            //Set Days Late
                                            _storesIssueDaysLate2 =
                                                Convert.ToInt32(_oSiAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _storesIssueBg2 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSiAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _storesIssueFg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSiAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _storesIssue2ApplyDaysBefore =
                                                (_oSiAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    case "3":
                                        {
                                            //Alert Level 3

                                            //Set Alert Level Flag
                                            _storesIssue3AlertDefined = true;

                                            //Set Days Late
                                            _storesIssueDaysLate3 =
                                                Convert.ToInt32(_oSiAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _storesIssueBg3 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSiAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _storesIssueFg3 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oSiAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _storesIssue3ApplyDaysBefore =
                                                (_oSiAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    default:
                                        {
                                            //Do Nothing
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    else
                    {
                        //Set Flags To False
                        _storesIssueAlertsDefined = false;
                    }
                }
                else
                {
                    //Set Flags To False
                    _storesIssueAlertsDefined = false;
                }
            }
            else
            {
                //Set Flags To False
                _storesIssueAlertsDefined = false;
            }

            #endregion

            #region Load Purchase Order Alerts

            //Load Data
            if (_oPoAlerts.LoadData())
            {
                //Check Table Count
                if (_oPoAlerts.Ds.Tables.Count > 0)
                {
                    //Check Row Count
                    if (_oPoAlerts.Ds.Tables[0].Rows.Count > 0)
                    {
                        //Loop Through Alerts
                        for (var i = 0; i < _oPoAlerts.Ds.Tables[0].Rows.Count; i++)
                        {
                            //Determine If Active
                            if (Convert.ToBoolean(_oPoAlerts.Ds.Tables[0].Rows[i][3]))
                            {
                                //Set Main Flag
                                _purchaseOrderAlertsDefined = true;

                                //Determine Alert Level
                                switch (_oPoAlerts.Ds.Tables[0].Rows[i][2].ToString())
                                {
                                    case "1":
                                        {
                                            //Alert Level 1

                                            //Set Alert Level Flag
                                            _purchaseOrder1AlertDefined = true;

                                            //Set Days Late
                                            _purchaseOrderDaysLate1 =
                                                Convert.ToInt32(_oPoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _purchaseOrderBg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oPoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _purchaseOrderFg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oPoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _purchaseOrder1ApplyDaysBefore =
                                                (_oPoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    case "2":
                                        {
                                            //Alert Level 2

                                            //Set Alert Level Flag
                                            _purchaseOrder2AlertDefined = true;

                                            //Set Days Late
                                            _purchaseOrderDaysLate2 =
                                                Convert.ToInt32(_oPoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _purchaseOrderBg2 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oPoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _purchaseOrderFg1 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oPoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _purchaseOrder2ApplyDaysBefore =
                                                (_oPoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    case "3":
                                        {
                                            //Alert Level 3

                                            //Set Alert Level Flag
                                            _purchaseOrder3AlertDefined = true;

                                            //Set Days Late
                                            _purchaseOrderDaysLate3 =
                                                Convert.ToInt32(_oPoAlerts.Ds.Tables[0].Rows[i][4].ToString());

                                            //Set Background
                                            _purchaseOrderBg3 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oPoAlerts.Ds.Tables[0].Rows[i][6].ToString()));

                                            //Set Foreground
                                            _purchaseOrderFg3 =
                                                Color.FromArgb(
                                                    Convert.ToInt32(
                                                        _oPoAlerts.Ds.Tables[0].Rows[i][5].ToString()));

                                            //Set Apply Days Before
                                            _purchaseOrder3ApplyDaysBefore =
                                                (_oPoAlerts.Ds.Tables[0].Rows[i][8].ToString() == "Y");

                                            break;
                                        }
                                    default:
                                        {
                                            //Do Nothing
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    else
                    {
                        //Set Flags To False
                        _purchaseOrderAlertsDefined = false;
                    }
                }
                else
                {
                    //Set Flags To False
                    _purchaseOrderAlertsDefined = false;
                }
            }
            else
            {
                //Set Flags To False
                _purchaseOrderAlertsDefined = false;
            }

            #endregion
        }
    }
}