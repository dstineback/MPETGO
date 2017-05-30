﻿using System;
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
        private MaintAttachmentObject _oObjeAttachments;
        private MaintenanceObject _oMaintObj;
        private bool _UseWeb;
        private string connection = ConfigurationManager.ConnectionStrings["ClientConnectionString"].ConnectionString;

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

        #region Combo functions

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
            _oMaintObj = new MaintenanceObject(connection, _UseWeb);

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
                chkChargeable.Checked,
                chkOEEFocus.Checked,
                chkRoute.Checked,
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
                startDate,
                tmpWarrantyDate,
                overheadRateID,
                responsibleID,
                conditionID,
                tmpLifeCycleDate,
                vendorID,
                Convert.ToDecimal(txtMilePost.Value),
                milePostDir,
                stateRouteID,
                Convert.ToDecimal(txtEasting.Value),
                Convert.ToDecimal(txtNorthing.Value),
                Convert.ToInt32(txtWarrantyInterval.Value),
                Convert.ToInt32(txtLifeCycleInterval.Value),
                uom,
                milepostTo,
                quantity,
                Convert.ToDecimal(txtHoursAvailable.Value),
                Convert.ToDecimal(txtPMHours.Value),
                Convert.ToDecimal(txtTotalAvailHrs.Value),
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
        //                        string objectId,
        //                        string desc,
        //                        int taskId,
        //                        int parentObjectId,
        //                        int areaId,
        //                        int costcodeId,
        //                        int locationId,
        //                        int mfgId,
        //                        int objectClassId,
        //                        int objectTypeId,
        //                        int productLineId,
        //                        int storeroomId,
        //                        string notes,
        //                        string assetNumber,
        //                        bool active,
        //                        bool chargeable,
        //                        bool oeeFocus,
        //                        bool routeable,
        //                        decimal chargeRate,
        //                        string fundMtlType,
        //                        decimal gpsX,
        //                        decimal gpsY,
        //                        decimal gpsZ,
        //                        int logicalOrder,
        //                        int idealCycle,
        //                        DateTime inServiceDate,
        //                        string mfgIdString,
        //                        string mfgModel,
        //                        string miscRef,
        //                        int objectCount,
        //                        DateTime purchaseDate,
        //                        decimal purchasePrice,
        //                        string remarks,
        //                        string serialNumber,
        //                        DateTime statusDate,
        //                        DateTime warrantyDate,
        //                        int overheadRateId,
        //                        int responsiblePersonId,
        //                        int conditionCode,
        //                        DateTime equipLifeTerminationDate,
        //                        int purchaseVendorId,
        //                        decimal milePost,
        //                        int milePostDirection,
        //                        int stateRouteId,
        //                        decimal easting,
        //                        decimal northing,
        //                        int warrantyInterval,
        //                        int lifeCycleInterval,
        //                        int uom,
        //                        decimal milepostTo,
        //                        decimal quantity,
        //                        decimal availHrs,
        //                        decimal pmHours,
        //                        decimal totalAvailHrs,
        //                        int fundSourceId,
        //                        int workOrderId,
        //                        int workOpId,
        //                        int orgCodeId,
        //                        int fundGroupId,
        //                        int equipmentNumberId,
        //                        int controlSectionId,
        //                        int createdBy
       

        #endregion
    }
}