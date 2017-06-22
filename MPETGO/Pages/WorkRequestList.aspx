<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="WorkRequestList.aspx.cs" Inherits="MPETGO.Pages.WorkRequestList" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1.0">

    
        <dx:ASPxGridView ID="WorkRequestGrid" runat="server" Theme="iOS"
            AutoGenerateColumns="False"
            KeyFieldName="n_Jobid">             
                   
            <SettingsAdaptivity AdaptivityMode="HideDataCells" 
                AllowOnlyOneAdaptiveDetailExpanded="true"              
                AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
             <Styles Cell-Paddings-Padding="3px" Header-Paddings-Padding="3px"></Styles> 
               <SettingsBehavior AllowEllipsisInText="true" />
            <Columns>
               
                
                <dx:GridViewDataTextColumn FieldName="n_Jobid" ReadOnly="True" Visible="false"
                    VisibleIndex="0">
                    <EditFormSettings Visible="False"></EditFormSettings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="T" Caption="Type of Job" VisibleIndex="6" Width="75px" EditFormCaptionStyle-HorizontalAlign="Left" CellStyle-HorizontalAlign="Center"
                    ReadOnly="True">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="A" VisibleIndex="2" Visible="false"
                    ReadOnly="True">
                </dx:GridViewDataTextColumn>
               
                <dx:GridViewDataHyperLinkColumn FieldName ="Jobid" Caption="Job ID" EditFormCaptionStyle-HorizontalAlign="Left" Width="75px" VisibleIndex="3" FixedStyle="Left">
                    <PropertiesHyperLinkEdit NavigateUrlFormatString="~/Pages/WorkRequestForm.aspx?jobid={0}"></PropertiesHyperLinkEdit>
                </dx:GridViewDataHyperLinkColumn>
                <dx:GridViewDataTextColumn FieldName="Object ID" Caption="Object ID" Width="75px"
                    VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Title" Caption="Description" Width="150px" CellStyle-Wrap="False" 
                    VisibleIndex="5">
                   
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="Request Date" Caption="Request Date" Width="75px"
                    ReadOnly="True" VisibleIndex="1" FixedStyle="Left">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="AssignedGUID" Visible="false"
                    VisibleIndex="7"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RouteToID" VisibleIndex="8" Visible="false">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="WorkOpID" VisibleIndex="9" Visible="false">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PriorityID" Caption="Priority" VisibleIndex="10" Width="75px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ReasonID" Caption="Reason" VisibleIndex="11" Width="75px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SubAssemblyName" Visible="false"
                    VisibleIndex="12"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="HwyRouteID" ReadOnly="True" Caption="Route/Street" Width="75px"
                    VisibleIndex="13"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Milepost" ReadOnly="True" Caption="Milepost" Width="75px"
                    VisibleIndex="14"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="MilePostDirID" Caption="MilePost Direction" Width="75px"
                    ReadOnly="True" VisibleIndex="15"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Requestor" VisibleIndex="16" Caption="Requestor" Width="75px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FlaggedRecordID" Visible="false"
                    VisibleIndex="17" ReadOnly="True">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CostCodeID" Visible="false"
                    VisibleIndex="18" ReadOnly="True">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FundSrcCodeID" Visible="false"
                    VisibleIndex="19" ReadOnly="True">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="WorkOrderCodeID" Visible="false"
                    ReadOnly="True" VisibleIndex="20"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="OrganizationCodeID" Visible="false"
                    ReadOnly="True" VisibleIndex="21"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FundingGroupCodeID" Visible="false"
                    ReadOnly="True" VisibleIndex="22"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ControlSectionID" Visible="false"
                    ReadOnly="True" VisibleIndex="23"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="EquipmentNumberID" Visible="false"
                    ReadOnly="True" VisibleIndex="24"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="MilePostTo" VisibleIndex="25" Width="75px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Area ID" VisibleIndex="26" Caption="Area" Width="75px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Created By" ReadOnly="True" Caption="Created By" Width="75px"
                    VisibleIndex="27"></dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="Created On" Caption="Created On" Width="75px"
                    VisibleIndex="28" ReadOnly="True">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="Modified By" Caption="Modified By" Visible="false"
                    VisibleIndex="29" ReadOnly="True">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="Modified On" Caption="Modified On" Visible="false"
                    ReadOnly="True" VisibleIndex="30"></dx:GridViewDataDateColumn>
            </Columns>
            
        </dx:ASPxGridView>
    <asp:SqlDataSource runat="server" ID="WorkRequestListDS"
        ConnectionString='<%$ ConnectionStrings:ClientConnectionString %>'
        SelectCommand="filter_GetFilteredWorkRequestsList"
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="MatchJobType" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="MatchJobAgainst" Type="Int32">
            </asp:Parameter>
            <asp:Parameter Name="JobIDLike" Type="String"></asp:Parameter>
            <asp:Parameter Name="StartingReqDate" Type="DateTime">
            </asp:Parameter>
            <asp:Parameter Name="EndingReqDate" Type="DateTime">
            </asp:Parameter>
            <asp:Parameter Name="TitleContains" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="RequestedByMatch" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="ReasonCodeMatch" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="PriorityMatch" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="MatchArea" Type="String"></asp:Parameter>
            <asp:Parameter Name="MatchObjectType" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="StateRouteMatch" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="MachineIDContains" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="ObjectDescr" Type="String"></asp:Parameter>
            <asp:Parameter Name="UserID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="RouteToID" Type="String"></asp:Parameter>
            <asp:Parameter Name="Notes" Type="String"></asp:Parameter>
            <asp:Parameter Name="MiscRef" Type="String"></asp:Parameter>
            <asp:Parameter Name="ObjLocation" Type="String"></asp:Parameter>
            <asp:Parameter Name="WorkOpID" Type="String"></asp:Parameter>
            <asp:Parameter Name="SubAssembly" Type="String"></asp:Parameter>
            <asp:Parameter Name="MilepostStart" Type="Decimal">
            </asp:Parameter>
            <asp:Parameter Name="MilepostEnd" Type="Decimal"></asp:Parameter>
            <asp:Parameter Name="MilepostDirection" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="ChargeCode" Type="String"></asp:Parameter>
            <asp:Parameter Name="FundSource" Type="String"></asp:Parameter>
            <asp:Parameter Name="WorkOrder" Type="String"></asp:Parameter>
            <asp:Parameter Name="OrgCode" Type="String"></asp:Parameter>
            <asp:Parameter Name="FundGroup" Type="String"></asp:Parameter>
            <asp:Parameter Name="ControlSection" Type="String">
            </asp:Parameter>
            <asp:Parameter Name="EquipNum" Type="String"></asp:Parameter>
            <asp:Parameter Name="HasAttachments" Type="String">
            </asp:Parameter>
        </SelectParameters>
    </asp:SqlDataSource>
    
</asp:Content>