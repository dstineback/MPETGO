﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="WorkRequestForm.aspx.cs" Inherits="MPETGO.Pages.WorkRequestForm" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

<meta name="viewport" content="width=device-width, initial-scale=1.0">


<script type='text/javascript'>
    var lat;
    var long;
    var x = document.getElementById("noGEO");

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
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
<script type="text/javascript">
    DXUploadedFilesContainer = {
        nameCellStyle: "",
        sizeCellStyle: "",
        useExtendedPopup: false,

        AddFile: function (fileName, fileUrl, fileSize) {
            var self = DXUploadedFilesContainer;
            var builder = ["<tr>"];

            builder.push("<td class='nameCell'");
            if (self.nameCellStyle)
                builder.push(" style='" + self.nameCellStyle + "'");
            builder.push(">");
            self.BuildLink(builder, fileName, fileUrl);
            builder.push("</td>");

            builder.push("<td class='sizeCell'");
            if (self.sizeCellStyle)
                builder.push(" style='" + self.sizeCellStyle + "'");
            builder.push(">");
            builder.push(fileSize);
            builder.push("</td>");

            builder.push("</tr>");

            var html = builder.join("");
            DXUploadedFilesContainer.AddHtml(html);
        },
        Clear: function () {
            DXUploadedFilesContainer.ReplaceHtml("");
        },
        BuildLink: function (builder, text, url) {
            builder.push("<a target='blank' onclick='return DXDemo.ShowScreenshotWindow(event, this, " + this.useExtendedPopup + ");'");
            builder.push(" href='" + url + "'>");
            builder.push(text);
            builder.push("</a>");
        },
        AddHtml: function (html) {
            var fileContainer = document.getElementById("uploadedFilesContainer"),
                fullHtml = html;
            if (fileContainer) {
                var containerBody = fileContainer.tBodies[0];
                fullHtml = containerBody.innerHTML + html;
            }
            DXUploadedFilesContainer.ReplaceHtml(fullHtml);
        },
        ReplaceHtml: function (html) {
            var builder = ["<table id='uploadedFilesContainer' class='uploadedFilesContainer'><tbody>"];
            builder.push(html);
            builder.push("</tbody></table>");
            var contentHtml = builder.join("");
            window.FilesRoundPanel.SetContentHtml(contentHtml);
        },
        ApplySettings: function (nameCellStyle, sizeCellStyle, useExtendedPopup) {
            var self = DXUploadedFilesContainer;
            self.nameCellStyle = nameCellStyle;
            self.sizeCellStyle = sizeCellStyle;
            self.useExtendedPopup = useExtendedPopup;
        }
    };

    function onFileUploadComplete(s, e) {
        if (e.callbackData) {
            var fileData = e.callbackData.split('|');
            var fileName = fileData[0],
                fileUrl = fileData[1],
                fileSize = fileData[2];
            //DXUploadedFilesContainer.AddFile(fileName, fileUrl, fileSize);
            //window.AttachmentGrid.Refresh();
        }
    }
</script>
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <dx:ASPxLabel runat="server" ID="lblHeader"></dx:ASPxLabel>

        <dx:ASPxFormLayout ID="ASPxFormLayout1"
            runat="server" Theme="iOS" EnableTheming="True"  Width="100%" SettingsAdaptivity-SwitchToSingleColumnAtWindowInnerWidth="800" SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit">            
            <Items>
                <dx:LayoutItem Caption="Work Request Issue"  CaptionSettings-Location="Top" Width="100%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxTextBox runat="server" ID="txtWorkDescription" MaxLength="250" AutoPostBack="false" Width="100%">
                                <ValidationSettings ErrorText="Description required">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutGroup Width="100%" Caption="Asset/Object" SettingsItemCaptions-Location="Top">
                    <Items>
                        <dx:LayoutItem Caption="Object ID" CaptionSettings-Location="Top" Width="50%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxComboBox ID="ObjectIDCombo" Width="100%"
                                        OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue_SQL" 
                                        OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition_SQL"
                                        runat="server" TextFormatString="{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8}" 
                                        EnableCallbackMode="true" CallbackPageSize="10" ValueType="System.String" ValueField="n_objectid" 
                                        DropDownStyle="DropDown" TextField="objectid" AutoPostBack="false" DropDownButton-Enabled="true" ClientInstanceName="ObjectIDCombo"  >
                                        <ClientSideEvents ValueChanged="function(s, e) { 
                                            var objectHasValue = ObjectIDCombo.GetValue();
                                                                                                    var selectedItem = s.GetSelectedItem();
                                                                                                    console.log('object', objectHasValue);
                                                                                                    console.log('selected object', selectedItem);
                                                                                                    if(objectHasValue!=null)
                                                                                                    {
                                                                                                        txtObjectDescription.SetText(selectedItem.GetColumnText('description'));
                                                                                                        ObjectIDComboText.SetText(selectedItem.GetColumnText('objectid'));
                                                                                                        txtObjectArea.SetText(selectedItem.GetColumnText('areaid'));
                                                                                                        txtObjectLocation.SetText(selectedItem.GetColumnText('locationid'));
                                                                                                        txtObjectAssetNumber.SetText(selectedItem.GetColumnText('assetnumber'));
                                                                                                        objectImg.SetImageUrl(selectedItem.GetColumnText('LocationOrURL'));
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        txtObjectDescription.SetText('');
                                                                                                        txtObjectArea.SetText('');
                                                                                                        txtObjectLocation.SetText('');
                                                                                                        txtObjectAssetNumber.SetText('');
                                                                                                    }
                                             }"  />
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="n_objectid" Visible="False" />
                                            <dx:ListBoxColumn FieldName="objectid" Caption="Object ID" Width="150px" ToolTip="M-PET.NET Maintenance Object ID"/>
                                            <dx:ListBoxColumn FieldName="description" Caption="Description" Width="250px" ToolTip="M-PET.NET Maintenance Object Description"/>
                                            <dx:ListBoxColumn FieldName="areaid" Caption="Area ID" Width="75px" ToolTip="M-PET.NET Maintenance Object Assigned Area ID" />
                                            <dx:ListBoxColumn FieldName="locationid" Caption="Location ID" Width="75px" ToolTip="M-PET.NET Maintenance Object Assigned Location ID" />
                                            <dx:ListBoxColumn FieldName="assetnumber" Caption="Asset #" Width="50px" ToolTip="M-PET.NET Maintenance Object Asset Number"/>
                                            <dx:ListBoxColumn FieldName="OrganizationCodeID" Caption="Org. Code ID" Width="100px" ToolTip="M-PET.NET Maintenance Object Assigned Org. Code ID" />
                                            <dx:ListBoxColumn FieldName="FundingGroupCodeID" Caption="Fund. Group Code ID" Width="100px" ToolTip="M-PET.NET Maintenance Object Assigned Funding Group Code ID" />
                                            <dx:ListBoxColumn FieldName="Following" Caption="Following" Width="50px" ToolTip="M-PET.NET Maintenance Object Following Yes/No?"/>
                                            <dx:ListBoxColumn FieldName="LocationOrURL" Caption="Photo" Width="50px" ToolTip="M-PET.NET Maintenance Object Photo"/>
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem Caption="Object Description" CaptionSettings-Location="Top"  Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxTextBox ID="txtObjectDescription" Width="100%" ClientInstanceName="txtObjectDescription" ReadOnly="true"
                                        runat="server" AutoPostBack="false">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutItem Caption="Requestor" CaptionSettings-Location="Top" Width="25%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox runat="server" ID="ComboRequestor" Width="100%" 
                                OnItemRequestedByValue="ComboRequestor_OnItemRequestedByValue_SQL" 
                                OnItemsRequestedByFilterCondition="ComboRequestor_OnItemsRequestedByFilterCondition_SQL"
                                DropDownStyle="DropDown" TextField="UserName" DropDownButton-Enabled="true"
                                EnableCallbackMode="true" CallbackPageSize="10" AutoPostBack="false" ValueType="System.String" ValueField="UserID" TextFormatString="{0} - {1}">
                             <Columns>
                                <dx:ListBoxColumn FieldName="UserID" Visible="False" />
                                <dx:ListBoxColumn FieldName="Username" Caption="Username" Width="75px" ToolTip="M-PET.NET User's Username"/>
                                <dx:ListBoxColumn FieldName="FullName" Caption="Full Name" Width="150px" ToolTip="M-PET.NET User's Full Name"/>
                            </Columns>                        
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Created Date" CaptionSettings-Location="Top" Width="25%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxDateEdit runat="server" Width="100%" ID="startDate" AutoPostBack="false"></dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:EmptyLayoutItem Width="50"></dx:EmptyLayoutItem>
                <dx:LayoutItem Caption="Priority" CaptionSettings-Location="Top" Width="25%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox runat="server" ID="ComboPriority" Width="100%"
                                OnItemRequestedByValue="ComboPriority_OnItemRequestedByValue_SQL" 
                                OnItemsRequestedByFilterCondition="ComboPriority_OnItemsRequestedByFilterCondition_SQL" 
                                DropDownButton-Enabled="true" DropDownStyle="DropDown" CallbackPageSize="10" 
                                EnableCallbackMode="true" TextField="priorityid" ValueField="n_priorityid" ValueType="System.String" 
                                TextFormatString="{0} - {1}" ClientInstanceName="ComboPriority" AutoPostBack="false">

                                 <Columns>
                                    <dx:ListBoxColumn FieldName="n_priorityid" Visible="False">
                                    </dx:ListBoxColumn>
                                    <dx:ListBoxColumn FieldName="priorityid" Width="75px"
                                        Caption="Priority ID" ToolTip="M-PET.NET Priority ID">
                                    </dx:ListBoxColumn>
                                    <dx:ListBoxColumn FieldName="description" Width="150px"
                                        Caption="Description" ToolTip="M-PET.NET Priority Description">
                                    </dx:ListBoxColumn>
                                </Columns>
                            </dx:ASPxComboBox>

                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Reason" CaptionSettings-Location="Top" Width="25%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox runat="server" ID="ComboReason" Width="100%" 
                                OnItemRequestedByValue="comboReason_OnItemRequestedByValue_SQL" 
                                OnItemsRequestedByFilterCondition="comboReason_OnItemsRequestedByFilterCondition_SQL" 
                                EnableCallbackMode="true" CallbackPageSize="10" ValueType="System.String" 
                                ValueField="n_reasonid" TextFormatString="{0} - {1}" DropDownStyle="DropDown" DropDownButton-Enabled="true" 
                                TextField="reasonid" AutoPostBack="false" ClientInstanceName="ComboReason">

                                <Columns>
                                    <dx:ListBoxColumn FieldName="n_reasonid" Visible="False" />
                                    <dx:ListBoxColumn FieldName="reasonid" Caption="Reason ID"
                                        Width="75px" ToolTip="M-PET.NET Reason ID" />
                                    <dx:ListBoxColumn FieldName="description" Caption="Description"
                                        Width="150px" ToolTip="M-PET.NET Reason Description" />
                                </Columns>
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                
                <dx:LayoutItem Caption="Longitude" CaptionSettings-Location="Top" Width="50%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxTextBox runat="server" ID="txtLong" ClientInstanceName="txtLong" Width="100%" AutoPostBack="false">

                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
              
                <dx:LayoutItem Caption="Latitude" CaptionSettings-Location="Top" Width="50%"> 
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxTextBox runat="server" ID="txtLat" ClientInstanceName="txtLat" AutoPostBack="false" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:EmptyLayoutItem Width="50%" ></dx:EmptyLayoutItem>

                <dx:LayoutItem Caption="" ShowCaption="False" CaptionSettings-Location="Top">
                                                                        <LayoutItemNestedControlCollection >
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <div id="PhotoContainer" runat="server">
                                                                                    <div class="uploadContainer">
                                                                                        <dx:ASPxUploadControl 
                                                                                            
                                                                                            ID="UploadControl" 
                                                                                            runat="server" 
                                                                                            ClientInstanceName="UploadControl" 
                                                                                            Width="98%" 
                                                                                            UploadMode="Auto" 
                                                                                            UploadStorage="Azure" 
                                                                                            FileUploadMode="OnPageLoad" 
                                                                                            ShowUploadButton="True" 
                                                                                            ShowProgressPanel="True" 
                                                                                            OnFileUploadComplete="UploadControl_FileUploadComplete" 
                                                                                            ShowAddRemoveButtons="True">
                                                                                            
                                                                                            <AzureSettings 
                                                                                                StorageAccountName="aspdemo" 
                                                                                                ContainerName="uploadcontroldemo"/>
                                                                                            
                                                                                            <BrowseButton Text="Browse">
                                                                                            </BrowseButton>
                                                                                            <AdvancedModeSettings 
                                                                                                EnableDragAndDrop="True" 
                                                                                                EnableFileList="false" 
                                                                                                EnableMultiSelect="true">
                                                                                                <FileListItemStyle CssClass="pending dxucFileListItem"></FileListItemStyle>
                                                                                            </AdvancedModeSettings>
                                                                                            <ValidationSettings 
                                                                                                MaxFileSize="4194304" 
                                                                                                AllowedFileExtensions=".jpg,.jpeg,.gif,.png">
                                                                                            </ValidationSettings>
                                                                                            <ClientSideEvents 
                                                                                                FileUploadComplete="onFileUploadComplete"/>
                                                                                        </dx:ASPxUploadControl>
                                                                                    </div>
                                                                                </div>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>

                                                                        <CaptionSettings Location="Top"></CaptionSettings>
                                                                    </dx:LayoutItem> <%--upload control--%>   
                                                                                
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
                                                                                            AutoPostBack="true" 
                                                                                            
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
                                                                                                <dx:GridViewDataTextColumn FieldName="nJobID" ReadOnly="True" Visible="false" VisibleIndex="1">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn FieldName="nJobstepID" ReadOnly="True" Visible="false" VisibleIndex="2">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn FieldName="ShortName" Caption="Name" Width="200px" VisibleIndex="3">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                     <DataItemTemplate>
                                                                                                        <dx:ASPxHyperLink ID="ASPxHyperLink1" NavigateUrl="javascript:void(0)" runat="server" Text='<%# Eval("ShortName") %>' Width="100%" Theme="Mulberry">
                                                                                                            <ClientSideEvents Click="onHyperLinkClick" />
                                                                                                        </dx:ASPxHyperLink>
                                                                                                     </DataItemTemplate>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn FieldName="DocType" Caption="Name" Width="100px" VisibleIndex="4">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn FieldName="Description" Visible="false" Caption="Description" Width="400px" VisibleIndex="5">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataHyperLinkColumn FieldName="LocationOrURL" Caption="Location/URL" Width="600px" VisibleIndex="6">
                                                                                                    <CellStyle Wrap="False"></CellStyle>
                                                                                                    <PropertiesHyperLinkEdit ></PropertiesHyperLinkEdit>
                                                                                                </dx:GridViewDataHyperLinkColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior EnableRowHotTrack="True" AllowFocusedRow="True" AllowClientEventsOnLoad="false" ColumnResizeMode="NextColumn" />
                                                                                            <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                                                                                            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="350" />
                                                                                            <SettingsPager PageSize="10">
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
                             
                <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxButton runat="server" Text="Add Coordinates" ID="LatLongBtn" Width="50%">
                                <ClientSideEvents Click="getLocation" />
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">

                            <dx:ASPxButton runat="server" Text="Submit" ID="submitBtn" Width="50%"
                                OnClick="submitBtn_Click">
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="" CaptionSettings-Location="Top" >
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                          
                            <dx:ASPxButton runat="server" Text="Save" ID="saveBtn" OnClick="saveBtn_Click">                            
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:ASPxFormLayout>
    
   

        <asp:SqlDataSource ID="AttachmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:connection %>" 
        SelectCommand="SELECT [ID], [nJobID], [nJobstepID], [DocType], [Description], [LocationOrURL], [ShortName] 
        FROM [Attachments] 
        WHERE (([nJobID] = @nJobID) AND ([nJobstepID] = @nJobstepID))">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="nJobID" SessionField="editingJobID" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="nJobstepID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ObjectDataSource" runat="server" />
    <asp:SqlDataSource ID="HwyRouteSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="CostCodeSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="FundSourceSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="WorkOrderSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="WorkOpSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="OrgCodeSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="FundGroupSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="CtlSectionSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="EquipNumSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="AreaSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="MilePostDirSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="PrioritySqlDatasource" runat="server" />
    <asp:SqlDataSource ID="ReasonSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="RequestorSqlDatasource" runat="server" />
    <asp:SqlDataSource ID="RouteToSqlDatasource" runat="server" />
    </asp:Content>
