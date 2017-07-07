<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="ObjectList.aspx.cs" Inherits="MPETGO.Pages.ObjectList" %>

<%--<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>--%>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


<meta name="viewport" content="width=device-width, initial-scale=1.0">


    <dx:ASPxGridView runat="server" Theme="iOS" ID="objectList" SettingsPager-PageSize="50"
        AutoGenerateColumns="false">

    <SettingsSearchPanel Visible="true" />
    <SettingsAdaptivity AdaptivityMode="HideDataCells" AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
    <Styles Cell-Paddings-Padding="5px" Header-Paddings-Padding="5px"><Cell Wrap="False"></Cell></Styles>
    <SettingsBehavior AllowGroup="true" AllowDragDrop="true" AllowSort="true" AllowHeaderFilter="true" />
    <Columns>
        <dx:GridViewDataColumn FieldName="n_objectid" ReadOnly="true" Visible="false"></dx:GridViewDataColumn>
        <dx:GridViewDataHyperLinkColumn FieldName="objectid" Caption="Object ID" VisibleIndex="0">
            <PropertiesHyperLinkEdit NavigateUrlFormatString="~/pages/UpdateObject.aspx?n_objectid={0}"></PropertiesHyperLinkEdit>
        </dx:GridViewDataHyperLinkColumn>
        <dx:GridViewDataTextColumn FieldName="description" ReadOnly="true" VisibleIndex="1" Width="200px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataColumn FieldName="Current Condition" ReadOnly="true" VisibleIndex="2" ></dx:GridViewDataColumn>
        <dx:GridViewDataTextColumn FieldName="Storeroom" ReadOnly="true" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Area" ReadOnly="true" VisibleIndex="4" Settings-AllowFilterBySearchPanel="True"></dx:GridViewDataTextColumn>
    </Columns>

</dx:ASPxGridView>
<asp:SqlDataSource runat="server" ID="objectListDS" ConnectionString="<%$ ConnectionStrings:ClientConnectionString %>" SelectCommand="form_MaintenanceObjects_GetAllMaintenanceObjects" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:Parameter />
        <asp:Parameter />
        <asp:Parameter />
        <asp:Parameter />
        <asp:Parameter />
        <asp:Parameter />
        <asp:Parameter />
        <asp:Parameter />
        <asp:Parameter />
        <asp:Parameter />
    </SelectParameters>
</asp:SqlDataSource>



</asp:Content>

