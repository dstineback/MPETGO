<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Parts.aspx.cs" Inherits="MPETGO.Pages.Parts" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

 <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyARk0BJK-cnQ27jHObwdI4xtqsNY9n7z9E"></script>

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
    function onFileUploadComplete(s, e) {
        if (e.callbackData) {
            var fileData = e.callbackData.split('|');
            var fileName = fileData[0],
                fileUrl = fileData[1],
                fileSize = fileData[2];
            DXUploadedFilesContainer.AddFile(fileName, fileUrl, fileSize);
        }
    }
</script>

    <dx:ASPxFormLayout ID="PartsForm" runat="server" Width="100%" Theme="iOS" SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsAdaptivity-SwitchToSingleColumnAtWindowInnerWidth="800">
        <Items>
            <dx:LayoutItem Caption="Active">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxCheckBox runat="server" CheckState="Checked" ReadOnly="true"
                            ID="activeCheckBox"></dx:ASPxCheckBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Object ID" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" ID="objectID" Width="100%"></dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
            <dx:LayoutItem Caption="Object/Asset Name" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox runat="server" ID="ComboObjectType" Width="100%" 
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
            <dx:LayoutItem Caption="Description" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo runat="server" ID="objectDesc" Width="100%"></dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Street/Road" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox runat="server" ID="ComboStreet" Width="100%" 
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
            <dx:LayoutItem Caption="Area" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox runat="server" ID="ComboArea" Width="100%"
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
            <dx:LayoutItem Caption="As Of" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit runat="server" ID="startDate" Width="100%">
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxUploadControl runat="server" Width="100%" ID="UploadControl" ClientInstanceName="UploadControl" ShowTextBox="true" NullText="Upload image" UploadStorage="Azure" FileUploadMode="OnPageLoad" ShowUploadButton="true" ShowProgressPanel="true" OnFileUploadComplete="addImg">
                           <%-- <AzureSettings AccountName="UploadAzureAccount" ContainerName="attachments" />--%>
                            <AdvancedModeSettings EnableMultiSelect="true" EnableDragAndDrop="true" EnableFileList="true"></AdvancedModeSettings>
                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg, .jpeg, .gif, .png"></ValidationSettings>
                            <ClientSideEvents FileUploadComplete="onFileUploadComplete" />
                        </dx:ASPxUploadControl>
                        
                    </dx:LayoutItemNestedControlContainer>
                    
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Latitude" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" Width="100%" ID="txtLat" ClientInstanceName="txtLat" AutoPostBack="false">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
            <dx:LayoutItem Caption="Longitude" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" Width="100%" ID="txtLong" ClientInstanceName="txtLong" AutoPostBack="false"></dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:EmptyLayoutItem Width="50%">

            </dx:EmptyLayoutItem>
            <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxButton runat="server" Width="50%" Text="Add Coordinates" ID="LatLongBtn" ClientInstanceName="LatLongBtn" AutoPostBack="false">
                                <ClientSideEvents Click="getLocation" />
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
             </dx:LayoutItem>
            <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
            <%--<dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>                      
                        <dx:ASPxLabel Text="Add Photo" runat="server"></dx:ASPxLabel>
                        <input runat="server" id="uploadFile" type="file" accept="image/*;capture=camera" onselect="addImg" />  
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>--%>
            
            <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" Width="50%" ID="AddPartBtn" Text="Add Part" OnClick="AddPartBtn_Click" ClientInstanceName="AddPartBtn" AutoPostBack="false">
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           
        </Items>
    </dx:ASPxFormLayout>
<asp:SqlDataSource ID="ObjectTypeDataSource" runat="server"></asp:SqlDataSource>
<asp:SqlDataSource ID="AreaSqlDatasource" runat="server" />
<asp:SqlDataSource ID="StateRouteDataSource" runat="server"></asp:SqlDataSource>
</asp:Content>

