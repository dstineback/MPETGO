<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Objects.aspx.cs" Inherits="MPETGO.Pages.Parts" %>
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
</script>
<script>
  
    function refresh() {
        AttachmentGrid.Refresh();
    }

    function onFileUploadComplete(s, e) {  

        if (window.AttachmentGrid === undefined)
        {
            FileUploaded(s, e);
        } else
        {
            FileUploaded(s, e);
            AttachmentGrid.Visible = true;
            AttachmentGrid.Refresh();
        }
    }
</script>
<script>
    var fieldSeparator = "|";
    function FileUploadStart() {  
        document.getElementById("uploadedListFiles").innerHTML = "";       
    }

    function FileUploaded(s, e) {
        if (e.isValid) {
            var linkFile = document.createElement("a");
            var indexSeparator = e.callbackData.indexOf(fieldSeparator);
            var fileName = e.callbackData.substring(0, indexSeparator);
            var pictureUrl = e.callbackData.substring(indexSeparator + fieldSeparator.length);
            
            linkFile.innerHTML = fileName;
           
            var container = document.getElementById("uploadedListFiles");
            container.appendChild(linkFile);
            container.appendChild(document.createElement("br"));
        }
    }

</script>

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />--%>
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <dx:ASPxLabel runat="server" ID="objectLabel" Theme="iOS" Text="Object ID:" Visible="false"></dx:ASPxLabel>
    <dx:ASPxFormLayout ID="PartsForm" runat="server" Width="100%" Theme="iOS" SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsAdaptivity-SwitchToSingleColumnAtWindowInnerWidth="800">
<SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"></SettingsAdaptivity>
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
                        <dx:ASPxTextBox runat="server" ID="objectID" ClientInstanceName="objectID" 
                            Width="100%"> 
                            <ValidationSettings><RequiredField IsRequired="true" ErrorText="Object ID Required" /></ValidationSettings>                     
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
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
                            <ValidationSettings><RequiredField IsRequired="true" ErrorText="Description Required" /></ValidationSettings>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Description" CaptionSettings-Location="Top" Width="50%" Paddings-PaddingBottom="5px">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxMemo runat="server" ID="objectDesc" AutoPostBack="false" Width="100%">                         
                        </dx:ASPxMemo>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>

<Paddings PaddingBottom="5px"></Paddings>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HelpText="" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxImage ID="objectImg" ImageAlign="Left" 
                            ImageUrl="~/Content/Images/noImage.png" 
                            AlternateText="No Picture Associated" Width="100%" 
                            ClientInstanceName="objectImg" runat="server" 
                            ShowLoadingImage="true"> 
                        </dx:ASPxImage>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

                <CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>                                

            <%--<dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>--%>
         <%--   <dx:LayoutItem Caption="Street/Road" CaptionSettings-Location="Top" Width="50%">
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
            </dx:LayoutItem>--%>
            <dx:LayoutItem Caption="Area" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox runat="server" ID="ComboArea" Width="100%"
                            OnItemRequestedByValue="ComboArea_OnItemRequestedByValue_SQL" 
                            OnItemsRequestedByFilterCondition="ComboArea_OnItemRequestedByFilterCondition_SQL" 
                            DropDownButton-Enabled="true" DropDownStyle="DropDown" 
                            AutoPostBack="false" EnableCallbackMode="true" 
                            CallbackPageSize="10" TextFormatString="{0} - {1}" ClientInstanceName="ComboArea" 
                            TextField="areaid" ValueField="n_areaid" ValueType="System.Int32">
                            <Columns>
                                <dx:ListBoxColumn FieldName="n_areaid" Visible="false"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="areaid" Caption="Area" Width="75px" ToolTip="M-PET Go Area ID"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="descr" Caption="Description" Width="150px" ToolTip="M-PET Go Area Description"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
            
           <dx:LayoutItem Caption="Location" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox runat="server" ID="ComboLocation" Width="100%" 
                            OnItemRequestedByValue="ComboLocation_OnItemRequestedByValue_SQL" 
                            OnItemsRequestedByFilterCondition="ComboLocation_OnItemRequestedByFilterCondition_SQL" 
                            DropDownButton-Enabled="true" DropDownStyle="DropDown" 
                            AutoPostBack="false" EnableCallbackMode="true" CallbackPageSize="10"
                            TextField="locationid" ValueField="n_locationid" ValueType="System.Int32"
                            TextFormatString="{0} - {1}" ClientInstanceName="ComboLocation" >
                            <Columns>
                                <dx:ListBoxColumn FieldName="n_locationid" Visible="false"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="locationid" Caption="Location" Width="75px" ToolTip="M-PET Go Location"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="description" Caption="Description" Width="150px" ToolTip="M-PET Go Location Description"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="As Of" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxDateEdit runat="server" ID="startDate" Width="100%">
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <div id="PhotoContainer" runat="server">
                            <div class="uploadContainer">

                        <dx:ASPxUploadControl runat="server" Width="100%" 
                            ID="UploadControl" 
                            ClientInstanceName="UploadControl" 
                            ShowTextBox="true" 
                            NullText="Object ID required to upload image" 
                            UploadStorage="Azure" 
                            UploadMode="Auto" FileInputCount="2"
                            FileUploadMode="OnPageLoad" AdvancedModeSettings-EnableMultiSelect="true"
                            ShowUploadButton="true" 
                            ShowProgressPanel="true"
                            OnFileUploadComplete="UploadControl_FileUploadComplete" OnCustomJSProperties="UploadControl_CustomJSProperties"
                            ShowAddRemoveButtons="true"> 
                            
                           <%--<AzureSettings AccountName="UploadAzureAccount" ContainerName="attachments" />--%>
                             
                            <AdvancedModeSettings EnableMultiSelect="true" EnableDragAndDrop="true" EnableFileList="true"></AdvancedModeSettings>
                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg, .jpeg, .gif, .png"></ValidationSettings>
                            <ClientSideEvents FileUploadComplete="function(s, e) { onFileUploadComplete(s, e) }" 
                                FilesUploadStart="function(s, e) { FileUploadStart(); }"
                                 />
                        </dx:ASPxUploadControl>
                            </div>
                        </div>                      
                    </dx:LayoutItemNestedControlContainer>                  
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem> 
            <dx:LayoutItem Caption="" Width="50%" CaptionSettings-Location="Top">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxRoundPanel runat="server" ID="ASPxRoundPanel1" Width="100%" ClientInstanceName="RoundPanel" HeaderText="Uploaded Files">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <div id="uploadedListFiles" style="height: 50px; font-family: Arial;"></div>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
                   
            <dx:LayoutItem Caption="" ShowCaption="False"  CaptionSettings-Location="Top">
                                                                        <LayoutItemNestedControlCollection >
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <asp:UpdatePanel ID="UpdatePanel" runat="server" OnUnload="UpdatePanel_Unload">
                                                                                    <ContentTemplate>
                                                                                        <dx:ASPxGridView 
                                                                                            ID="AttachmentGrid" 
                                                                                            runat="server" 
                                                                                            
                                                                                            KeyFieldName="LocationOrURL" 
                                                                                            Width="98%" 
                                                                                            KeyboardSupport="True" 
                                                                                            ClientInstanceName="AttachmentGrid" 
                                                                                            AutoPostBack="false" 
                                                                                            
                                                                                            Settings-HorizontalScrollBarMode="Auto" 
                                                                                            SettingsPager-Mode="ShowPager" 
                                                                                            SettingsBehavior-ProcessFocusedRowChangedOnServer="True" 
                                                                                            SettingsBehavior-AllowFocusedRow="True"
                                                                                            EnableCallBacks="true" AutoGenerateColumns="False" DataSourceID="AttachmentDataSource">
                                                                                            <Styles Header-CssClass="gridViewHeader" Row-CssClass="gridViewRow" FocusedRow-CssClass="gridViewRowFocused" 
                                                                                                    RowHotTrack-CssClass="gridViewRow" FilterRow-CssClass="gridViewFilterRow" >
                                                                                                <Header CssClass="gridViewHeader"></Header>

                                                                                                <Row CssClass="gridViewRow"></Row>

                                                                                                <RowHotTrack CssClass="gridViewRow"></RowHotTrack>

                                                                                                <FocusedRow CssClass="gridViewRowFocused"></FocusedRow>

                                                                                                <FilterRow CssClass="gridViewFilterRow"></FilterRow>
                                                                                            </Styles>
                                                                                            <Columns>
                                                                                                <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" Visible="false" VisibleIndex="0">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn FieldName="n_MaintObjectID" ReadOnly="True" Visible="false" VisibleIndex="1">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                </dx:GridViewDataTextColumn> 
                                                                                                <dx:GridViewDataTextColumn FieldName="ShortName" Caption="Name" Width="200px" VisibleIndex="3">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                    <%-- <DataItemTemplate>
                                                                                                        <dx:ASPxHyperLink ID="ASPxHyperLink1" NavigateUrl="javascript:void(0)" runat="server" Text='<%# Eval("ShortName") %>' Width="100%" Theme="Mulberry">
                                                                                                            <ClientSideEvents Click="onHyperLinkClick" />
                                                                                                        </dx:ASPxHyperLink>
                                                                                                     </DataItemTemplate>--%>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn FieldName="DocType" Caption="Name" Width="100px" VisibleIndex="4">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn FieldName="Description" Visible="false" Caption="Description" Width="200px" VisibleIndex="5">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataHyperLinkColumn FieldName="LocationOrURL" Caption="Location/URL" Width="150px" VisibleIndex="6">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                    <PropertiesHyperLinkEdit Text="Download" ></PropertiesHyperLinkEdit>
                                                                                                </dx:GridViewDataHyperLinkColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior EnableRowHotTrack="True" AllowFocusedRow="True" AllowClientEventsOnLoad="false" ColumnResizeMode="NextColumn" />
                                                                                            <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                                                                                            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="250" />
                                                                                            <SettingsPager PageSize="5">
                                                                                                <PageSizeItemSettings Visible="true" />
                                                                                            </SettingsPager>
                                                                                            <Templates>
                                                                                                <FooterRow>
                                                                                                    
                                                                                                    <dx:ASPxButton runat="server" ID="DeleteAttachmentButton" OnClick="DeleteAttachmentButton_Click"  Text="Delete Crew Member"></dx:ASPxButton>
                                                                                                </FooterRow>
                                                                                            </Templates>
                                                                                        </dx:ASPxGridView>      
                                                                                       
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>

                                                                        <CaptionSettings Location="Top"></CaptionSettings>
                                                                    </dx:LayoutItem>
           <%-- <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>--%>
            <dx:LayoutItem Caption="Latitude" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" Width="100%" ID="txtLat" ClientInstanceName="txtLat" AutoPostBack="false">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Longitude" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" Width="100%" ID="txtLong" ClientInstanceName="txtLong" AutoPostBack="false"></dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
            <%--<dx:EmptyLayoutItem Width="50%">

            </dx:EmptyLayoutItem>--%>
            <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxButton runat="server" Width="50%" Text="Add Coordinates" ID="LatLongBtn" ClientInstanceName="LatLongBtn" AutoPostBack="false">
                                <ClientSideEvents Click="getLocation" />
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
             </dx:LayoutItem>
            <%--<dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>--%>
               
            <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" Width="50%" ID="AddPartBtn" Text="Submit" OnClick="AddPartBtn_Click" ClientInstanceName="AddPartBtn" AutoPostBack="false">
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>

<CaptionSettings Location="Top"></CaptionSettings>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" Width="50%" ID="SavePartBtn" Text="Save" OnClick="SavePartBtn_Click" ClientInstanceName="SavepartBtn" AutoPostBack="false"></dx:ASPxButton>
                     </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>          
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxHint runat="server" Content="Add Description of Object" ID="objectDescHint" TargetSelector=".objectDesc" TriggerAction="Hover" Animation="true" AllowFlip="true">
        
    </dx:ASPxHint>
    <asp:SqlDataSource ID="AttachmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:connection %>" 
        SelectCommand="SELECT [ID], [n_MaintObjectID], [DocType], [Description], [LocationOrURL], [ShortName] 
        FROM [Attachments] 
        WHERE ([n_MaintObjectID] = @n_MaintObjectID)">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="n_MaintObjectID" SessionField="n_objectID" Type="Int32" />           
        </SelectParameters>
    </asp:SqlDataSource>
<asp:SqlDataSource ID="LocationDataSource" runat="server"></asp:SqlDataSource>
<asp:SqlDataSource ID="ObjectTypeDataSource" runat="server"></asp:SqlDataSource>
<asp:SqlDataSource ID="AreaSqlDatasource" runat="server" />
<asp:SqlDataSource ID="StateRouteDataSource" runat="server"></asp:SqlDataSource>
</asp:Content>

