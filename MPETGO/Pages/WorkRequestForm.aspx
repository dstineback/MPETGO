<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="WorkRequestForm.aspx.cs" Inherits="MPETGO.Pages.WorkRequestForm" %>
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
    <dx:ASPxLabel runat="server" ID="lblHeader"></dx:ASPxLabel>

        <dx:ASPxFormLayout ID="ASPxFormLayout1"
            runat="server" Theme="iOS" EnableTheming="True"  Width="100%" SettingsAdaptivity-SwitchToSingleColumnAtWindowInnerWidth="800" SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit">            
            <Items>
                <dx:LayoutItem Caption="Work Request Issue"  CaptionSettings-Location="Top" Width="100%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxMemo runat="server" ID="txtWorkDescription" MaxLength="250" AutoPostBack="false" Width="57.5%">
                                <ValidationSettings ErrorText="Description required">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxMemo>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutGroup Width="50%" Caption="Asset/Object" SettingsItemCaptions-Location="Top">
                    <Items>
                        <dx:LayoutItem Caption="Object ID" CaptionSettings-Location="Top" Width="50%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxComboBox ID="ObjectIDCombo" 
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
                                    <dx:ASPxTextBox ID="txtObjectDescription" ClientInstanceName="txtObjectDescription" ReadOnly="true"
                                        runat="server" AutoPostBack="false">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:EmptyLayoutItem Width="50%" ></dx:EmptyLayoutItem>
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
                            <dx:ASPxDateEdit runat="server" ID="startDate" AutoPostBack="false"></dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:EmptyLayoutItem Width="50"></dx:EmptyLayoutItem>
                <dx:LayoutItem Caption="Priority" CaptionSettings-Location="Top" Width="25%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox runat="server" ID="ComboPriority" 
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
                            <dx:ASPxComboBox runat="server" ID="ComboReason" 
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
                            <dx:ASPxTextBox runat="server" ID="txtLong" ClientInstanceName="txtLong" Width="48%" AutoPostBack="false">

                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>

                <dx:LayoutItem Caption="Latitude" CaptionSettings-Location="Top" Width="50%"> 
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxTextBox runat="server" ID="txtLat" ClientInstanceName="txtLat" AutoPostBack="false" Width="48%">

                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxButton runat="server" Text="Add Coordinates" ID="LatLongBtn" Width="20%">
                                <ClientSideEvents Click="getLocation" />
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:EmptyLayoutItem Width="15%"></dx:EmptyLayoutItem>
                <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="50%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">

                            <dx:ASPxButton runat="server" Text="Submit" ID="submitBtn" Width="20%"
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
