<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="UpdateObject.aspx.cs" Inherits="MPETGO.Pages.UpdateObject" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


<meta name="viewport" content="width=device-width, initial-scale=1.0" />

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
        latValue.SetValue(lat);
        latValue.SetText(lat);
        lngValue.SetValue(long);
        lngValue.SetText(long);
    }
</script>

<dx:ASPxGridView runat="server" ID="objectGridView" OnLoad="objectGridView_Load" AutoGenerateColumns="false">
    <Columns>
        <dx:GridViewDataTextColumn FieldName="n_objectid">

        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="objectid"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="description"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="GPS_Y" Name="Latitude" Caption="Latitude"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="GPS_X" Name="Longitude" Caption="Longitude"></dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>
 <dx:ASPxFormLayout runat="server">
     <Items>
         <dx:LayoutGroup>
             <Items>
                 <dx:LayoutItem>
                     <LayoutItemNestedControlCollection>
                         <dx:LayoutItemNestedControlContainer>
                             <dx:ASPxComboBox runat="server" ID="ObjectComboSearch" 
                                 ClientInstanceName="ObjectComboSearch" 
                                 OnItemsRequestedByFilterCondition="ObjectComboSearch_ItemsRequestedByFilterCondition" 
                                 OnItemRequestedByValue="ObjectcomboSearch_ItemsRequestedByValue">                                
                                 <Columns>
                                     <dx:ListBoxColumn FieldName="n_objectid" Caption="n_objectid"></dx:ListBoxColumn>
                                     <dx:ListBoxColumn FieldName="objectid" Caption="Object ID"></dx:ListBoxColumn>
                                     <dx:ListBoxColumn FieldName="description" Caption="Description"></dx:ListBoxColumn>
                                 </Columns>
                             </dx:ASPxComboBox>
                         </dx:LayoutItemNestedControlContainer>
                     </LayoutItemNestedControlCollection>
                 </dx:LayoutItem>
                 <dx:LayoutItem>
                     <LayoutItemNestedControlCollection>
                         <dx:LayoutItemNestedControlContainer>
                             <dx:ASPxHiddenField runat="server" ID="objectIDValue" ></dx:ASPxHiddenField>
                         </dx:LayoutItemNestedControlContainer>
                     </LayoutItemNestedControlCollection>
                 </dx:LayoutItem>
             </Items>
         </dx:LayoutGroup>
         <dx:LayoutGroup Caption="Current Location">
           <Items>
         <dx:LayoutItem Caption="Latitude" CaptionSettings-Location="Top">
             <LayoutItemNestedControlCollection>
                 <dx:LayoutItemNestedControlContainer>
                     <dx:ASPxTextBox runat="server" ID="latValue" ClientInstanceName="latValue"></dx:ASPxTextBox>
                 </dx:LayoutItemNestedControlContainer>
             </LayoutItemNestedControlCollection>
         </dx:LayoutItem>
         <dx:LayoutItem CaptionSettings-Location="Top" Caption="Longitude">
             <LayoutItemNestedControlCollection>
                 <dx:LayoutItemNestedControlContainer>
                     <dx:ASPxTextBox runat="server" ID="lngValue" ClientInstanceName="lngValue"></dx:ASPxTextBox>
                 </dx:LayoutItemNestedControlContainer>
             </LayoutItemNestedControlCollection>
         </dx:LayoutItem>
           </Items>
         </dx:LayoutGroup>
         <dx:LayoutItem Caption="">
             <LayoutItemNestedControlCollection>
                 <dx:LayoutItemNestedControlContainer>
                     <dx:ASPxButton runat="server" ID="getCords" ClientInstanceName="getCords" Visible="true" Text="Get Location">                     
                         <ClientSideEvents Init="getLocation" />
                         <ClientSideEvents Click="getLocation" />
                     </dx:ASPxButton>
                 </dx:LayoutItemNestedControlContainer>
             </LayoutItemNestedControlCollection>
         </dx:LayoutItem>
         <dx:LayoutItem Caption="">
             <LayoutItemNestedControlCollection>
                 <dx:LayoutItemNestedControlContainer>
                     <dx:ASPxButton runat="server" ID="saveLatLng" OnClick="updateLatLng" Text="Update GPS"></dx:ASPxButton>
                 </dx:LayoutItemNestedControlContainer>
             </LayoutItemNestedControlCollection>
         </dx:LayoutItem>
     </Items>
 </dx:ASPxFormLayout>


</asp:Content>
