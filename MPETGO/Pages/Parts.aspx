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
    //function onFileUploadComplete(s, e) {
    //    if (e.callbackData) {
    //        var fileData = e.callbackData.split('|');
    //        var fileName = fileData[0],
    //            fileUrl = fileData[1],
    //            fileSize = fileData[2];
    //        DXUploadedFilesContainer.AddFile(fileName, fileUrl, fileSize);
    //    }
    //}
</script>
<script>
    //DXUploadedFilesContainer = {
    //    nameCellStyle: "",
    //    sizeCellStyle: "",
    //    useExtendedPopup: false,

    //    AddFile: function (fileName, fileUrl, fileSize) {
    //        var self = DXUploadedFilesContainer;
    //        var builder = ["<tr>"];

    //        builder.push("<td class='nameCell'");
    //        if (self.nameCellStyle)
    //            builder.push(" style='" + self.nameCellStyle + "'");
    //        builder.push(">");
    //        self.BuildLink(builder, fileName, fileUrl);
    //        builder.push("</td>");

    //        builder.push("<td class='sizeCell'");
    //        if (self.sizeCellStyle)
    //            builder.push(" style='" + self.sizeCellStyle + "'");
    //        builder.push(">");
    //        builder.push(fileSize);
    //        builder.push("</td>");

    //        builder.push("</tr>");

    //        var html = builder.join("");
    //        DXUploadedFilesContainer.AddHtml(html);
    //    },
    //    Clear: function () {
    //        DXUploadedFilesContainer.ReplaceHtml("");
    //    },
    //    BuildLink: function (builder, text, url) {
    //        builder.push("<a target='blank' onclick='return DXDemo.ShowScreenshotWindow(event, this, " + this.useExtendedPopup + ");'");
    //        builder.push(" href='" + url + "'>");
    //        builder.push(text);
    //        builder.push("</a>");
    //    },
    //    AddHtml: function (html) {
    //        var fileContainer = document.getElementById("uploadedFilesContainer"),
    //            fullHtml = html;
    //        if (fileContainer) {
    //            var containerBody = fileContainer.tBodies[0];
    //            fullHtml = containerBody.innerHTML + html;
    //        }
    //        DXUploadedFilesContainer.ReplaceHtml(fullHtml);
    //    },
    //    ReplaceHtml: function (html) {
    //        var builder = ["<table id='uploadedFilesContainer' class='uploadedFilesContainer'><tbody>"];
    //        builder.push(html);
    //        builder.push("</tbody></table>");
    //        var contentHtml = builder.join("");
    //        window.FilesRoundPanel.SetContentHtml(contentHtml);
    //    },
    //    ApplySettings: function (nameCellStyle, sizeCellStyle, useExtendedPopup) {
    //        var self = DXUploadedFilesContainer;
    //        self.nameCellStyle = nameCellStyle;
    //        self.sizeCellStyle = sizeCellStyle;
    //        self.useExtendedPopup = useExtendedPopup;
    //    }
    //};

    //function onFileUploadComplete(s, e) {
    //    if (e.callbackData) {
    //        var fileData = e.callbackData.split('|');
    //        var fileName = fileData[0],
    //            fileUrl = fileData[1],
    //            fileSize = fileData[2];
            //DXUploadedFilesContainer.AddFile(fileName, fileUrl, fileSize);
            //window.AttachmentGrid.Refresh();
        //}
    //}
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

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
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
            <%--<dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>--%>
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
                        <div id="PhotoContainer" runat="server">
                            <div class="uploadContainer">

                        <dx:ASPxUploadControl runat="server" Width="100%" 
                            ID="UploadControl" 
                            ClientInstanceName="UploadControl" 
                            ShowTextBox="true" 
                            NullText="Upload image" 
                            UploadStorage="Azure" 
                            UploadMode="Auto" FileInputCount="2"
                            FileUploadMode="OnPageLoad" 
                            ShowUploadButton="true" 
                            ShowProgressPanel="true" 
                            OnFileUploadComplete="UploadControl_FileUploadComplete" 
                            ShowAddRemoveButtons="true">
                           <%--<AzureSettings AccountName="UploadAzureAccount" ContainerName="attachments" />--%>
                             
                            <AdvancedModeSettings EnableMultiSelect="true" EnableDragAndDrop="true" EnableFileList="true"></AdvancedModeSettings>
                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg, .jpeg, .gif, .png"></ValidationSettings>
                            <ClientSideEvents FileUploadComplete="function(s, e) { FileUploaded(s, e) }" 
                                FilesUploadStart="function(s, e) { FileUploadStart(); }"
                                 />
                        </dx:ASPxUploadControl>
                            </div>
                        </div>                      
                    </dx:LayoutItemNestedControlContainer>                  
                </LayoutItemNestedControlCollection>
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
           <%-- <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>--%>
            <dx:LayoutItem Caption="Latitude" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" Width="100%" ID="txtLat" ClientInstanceName="txtLat" AutoPostBack="false">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Longitude" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxTextBox runat="server" Width="100%" ID="txtLong" ClientInstanceName="txtLong" AutoPostBack="false"></dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
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
             </dx:LayoutItem>
            <%--<dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>--%>
           
            
            <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" Width="50%" ID="AddPartBtn" Text="Submit" OnClick="AddPartBtn_Click" ClientInstanceName="AddPartBtn" AutoPostBack="false">
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
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
    <asp:SqlDataSource ID="AttachmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:connection %>" 
        SelectCommand="SELECT [ID], [nJobID], [nJobstepID], [DocType], [Description], [LocationOrURL], [ShortName] 
        FROM [Attachments] 
        WHERE (([nJobID] = @nJobID) AND ([nJobstepID] = @nJobstepID))">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="nJobID" SessionField="editingJobID" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="nJobstepID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
<asp:SqlDataSource ID="ObjectTypeDataSource" runat="server"></asp:SqlDataSource>
<asp:SqlDataSource ID="AreaSqlDatasource" runat="server" />
<asp:SqlDataSource ID="StateRouteDataSource" runat="server"></asp:SqlDataSource>
</asp:Content>

