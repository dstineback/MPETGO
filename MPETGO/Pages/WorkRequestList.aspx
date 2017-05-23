<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="WorkRequestList.aspx.cs" Inherits="MPETGO.Pages.WorkRequestList" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1.0">

    
        <dx:ASPxGridView ID="ASPxGridView1" runat="server"
            AutoGenerateColumns="False" DataSourceID="WorkRequestListDS"
            KeyFieldName="n_Jobid">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="n_Jobid" ReadOnly="True"
                    VisibleIndex="0">
                    <EditFormSettings Visible="False"></EditFormSettings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Jobid" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TypeOfJob" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="n_MaintObjectID"
                    VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="n_GPSObjectID"
                    VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="n_ActionPriority"
                    VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="n_jobreasonid"
                    VisibleIndex="7"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="RequestDate"
                    VisibleIndex="9"></dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="GPS_X" VisibleIndex="10">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="GPS_Y" VisibleIndex="11">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:SqlDataSource runat="server" ID="WorkRequestListDS"
            ConnectionString='<%$ ConnectionStrings:ClientConnectionString %>'
            SelectCommand="SELECT [n_Jobid], [Jobid], [Title], [TypeOfJob], [n_MaintObjectID], [n_GPSObjectID], [n_ActionPriority], [n_jobreasonid], [Notes], [RequestDate], [GPS_X], [GPS_Y] FROM [Jobs] ORDER BY [CreationDate]">
        </asp:SqlDataSource>
    
</asp:Content>