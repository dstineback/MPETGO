<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="WorkRequestForm.aspx.cs" Inherits="MPETGO.Pages.WorkRequestForm" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

<meta name="viewport" content="width=device-width, initial-scale=1.0">

        <dx:ASPxFormLayout ID="ASPxFormLayout1"
            runat="server" Theme="iOS" EnableTheming="True" Width="100%" SettingsAdaptivity-SwitchToSingleColumnAtWindowInnerWidth="800" SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit">
            <Items>
                <dx:LayoutItem Caption="Work Request Issue" Width="100%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxMemo runat="server" ID="txtWorkDescription">
                            </dx:ASPxMemo>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutGroup Width="100%">
                    <Items>
                        <dx:LayoutItem Caption="Object ID" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxComboBox ID="ObjectIDCombo" OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue_SQL" OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition_SQL"
                                        runat="server" TextFormatString="{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8}" EnableCallbackMode="true" CallbackPageSize="10" ValueType="System.String" ValueField="n_objectid" DropDownStyle="DropDown" TextField="objectid" AutoPostBack="false" DropDownButton-Enabled="true" ClientInstanceName="ObjectIDCombo"  >
                                        <ClientSideEvents ValueChanged="function(s, e) { 
                                            var objectHasValue = ObjectIDCombo.GetValue();
                                                                                                    var selectedItem = s.GetSelectedItem();
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
                        <dx:LayoutItem Caption="Object Description" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxTextBox ID="txtObjectDescription" ReadOnly="true"
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutItem Caption="Requestor">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox runat="server" ID="ComboRequestor" 
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
                <dx:LayoutItem Caption="Created Date" Width="100%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxDateEdit runat="server" ID="startDate"></dx:ASPxDateEdit>


                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Priority" Width="100%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox runat="server" ID="ComboPriority" DataSourceID="PrioritySqlDatasource" OnItemRequestedByValue="ComboPriority_OnItemRequestedByValue_SQL" OnItemsRequestedByFilterCondition="ComboPriority_OnItemsRequestedByFilterCondition_SQL">
                            </dx:ASPxComboBox>

                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Reason">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox runat="server" ID="ComboReason" OnItemRequestedByValue="comboReason_OnItemRequestedByValue_SQL" OnItemsRequestedByFilterCondition="comboReason_OnItemsRequestedByFilterCondition_SQL" DataSourceID="ReasonSqlDatasource">
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>

                <dx:LayoutItem >
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            
                            <dx:ASPxButton runat="server" Text="Submit" ID="submitBtn" OnClick="submitBtn_Click">
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem  >
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
