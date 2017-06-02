<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Parts.aspx.cs" Inherits="MPETGO.Pages.Parts" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
<script>
    var lat;
    var long;
    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
           window.alert = "Geolocation is not supported by this browser.";
        }
    }
    function showPosition(position) {
        lat = position.coords.latitude;
        long = position.coords.longitude;
        txtLat.SetValue(lat);
        txtLat.SetText(lat);
        txtLong.SetValue(long);
        txtLong.SetText(long);
    }
</script>
    <dx:ASPxFormLayout ID="PartsForm" runat="server" Theme="iOS">
        <Items>
            <dx:LayoutItem Caption="Active">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxCheckBox runat="server" CheckState="Checked" ReadOnly="true"
                            ID="activeCheckBox"></dx:ASPxCheckBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Object ID">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" ID="objectID"></dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Object/Asset Name">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox runat="server" ID="ComboObjectType" 
                            OnItemRequestedByValue="ComboObjectType_OnItemRequestedByValue_SQL" 
                            OnItemsRequestedByFilterCondition="ComboObjectType_OnItemsRequestedByFilterCondition_SQL" 
                            DropDownButton-Enabled="true" DropDownStyle="DropDown" 
                            AutoPostBack="false" CallbackPageSize="10" EnableCallbackMode="true" 
                            TextField="objecttypeid" ValueField="n_objtypeid" ValueType="System.Int32"
                            TextFormatString="{0} - {1}" ClientInstanceName="ComboObjectType"
                            >
                            <Columns>
                                <dx:ListBoxColumn FieldName="n_objtypeid" Visible="false"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="objtypeid" Width="75px" Caption="Object Type" ToolTip="M-PET Go Object Type"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="descr" Width="150px" Caption="Description" ToolTip="M-PET go Object Type Description"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Description">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo runat="server" ID="objectDesc"></dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Street/Road">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox runat="server" ID="ComboStreet" 
                            OnItemRequestedByValue="ComboStreet_OnItemRequestedByValue_SQL" 
                            OnItemsRequestedByFilterCondition="ComboStreet_OnItemRequestedByFilterCondition_SQL" 
                            DropDownButton-Enabled="true" DropDownStyle="DropDown" 
                            AutoPostBack="false" EnableCallbackMode="true" CallbackPageSize="10"
                            TextField="StateRouteID" ValueField="n_StateRouteID" ValueType="System.String" 
                            TextFormatString="{0} - {1}" ClientInstanceName="ComboStreet" >
                            <Columns>
                                <dx:ListBoxColumn FieldName="n_StateRouteID" Visible="false"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="StateRouteID" Caption="Street/Route" Width="75px" ToolTip="M-PET Go Street Name"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="Description" Caption="Description" Width="150px" ToolTip="M-PET Go Street Description"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Area">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox runat="server" ID="ComboArea" 
                            OnItemRequestedByValue="ComboArea_OnItemRequestedByValue_SQL" 
                            OnItemsRequestedByFilterCondition="ComboArea_OnItemRequestedByFilterCondition_SQL" 
                            DropDownButton-Enabled="true" DropDownStyle="DropDown" 
                            AutoPostBack="false" EnableCallbackMode="true" 
                            CallbackPageSize="10" TextFormatString="{0} - {1}" ClientInstanceName="ComboArea" 
                            TextField="areaid" ValueField="n_areaid" ValueType="System.String">

                            <Columns>
                                <dx:ListBoxColumn FieldName="n_areaid" Visible="false"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="areaid" Caption="Area" Width="75px" ToolTip="M-PET Go Area ID"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="descr" Caption="Description" Width="150px" ToolTip="M-PET Go Area Description"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="As Of">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit runat="server" ID="startDate">
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Latitude">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" ID="txtLat" ClientInstanceName="txtLat" AutoPostBack="false">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Longitude">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" ID="txtLong" ClientInstanceName="txtLong" AutoPostBack="false"></dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxButton runat="server" Text="Add Coordinates" ID="LatLongBtn" ClientInstanceName="LatLongBtn" AutoPostBack="false">
                                <ClientSideEvents Click="getLocation" />
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
             </dx:LayoutItem>
            <dx:LayoutItem>
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="SaveBtn" Text="Save" ClientInstanceName="SaveBtn" AutoPostBack="false">
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem>
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="AddPartBtn" Text="Add Part" OnClick="AddPartBtn_Click" ClientInstanceName="AddPartBtn" AutoPostBack="false">
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
         <input runat="server" id="uploadFile" type="file" accept="image/*;capture=camera" onselect="addImg">

<asp:SqlDataSource ID="ObjectTypeDataSource" runat="server"></asp:SqlDataSource>
<asp:SqlDataSource ID="AreaSqlDatasource" runat="server" />
<asp:SqlDataSource ID="StateRouteDataSource" runat="server"></asp:SqlDataSource>
</asp:Content>

