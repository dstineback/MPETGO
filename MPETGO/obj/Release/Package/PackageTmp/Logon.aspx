<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Logon.aspx.cs" Inherits="MPETGO.Logon" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


    <script>
        var lat;
        var long;
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
                console.log("lat", lat, "long", long);
                window.alert = "Location found";
            } else {
                window.alert = "Geolocation is not supported by this browser.";
            }
        }
        function showPosition(position) {
            lat = position.coords.latitude;
            long = position.coords.longitude;
            console.log("latTWO", lat, "longTWO", long);
            localStorage.setItem("Lat", lat);
            localStorage.setItem("Lng", long);
           
            
        }

        function ShowPopUp() {
            LogonPopUp.Show();
        }


        function SetName() {
            var name = document.getElementById("txtUsername");
            var nt = name.Value;
            txtUsername.SetText(nt);
        }

        function SetPW() {
            var name = document.getElementById("txtPassword");
            var nt = name.Value;
            txtPassword.SetText(nt);
        }

        function SetText() {
            var ut = txtUsername.Text;
            var pt = txtPassword.Text;
            txtPassword.SetText(pt);
            txtUsername.SetText(ut);

        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="GetLocation"><ClientSideEvents Init="getLocation" /></dx:ASPxHiddenField>
    <dx:ASPxPopupControl runat="server" ID="LogonPopUp" ClientInstanceName="LogonPopUp" HeaderText="Logon" 
        Modal="true" PopupAnimationType="Fade" AllowDragging="true" AllowResize="true" 
        CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" CloseOnEscape="true" OnLoad="LogonPopUp_Load" >
        <ClientSideEvents Init="ShowPopUp" />             
        
        <ContentCollection >
            <dx:PopupControlContentControl> 
                            <dx:ASPxFormLayout runat="server" Theme="iOS">
                               <Items>
                                   <dx:LayoutGroup Caption="">
                                       <Items>
                                           <dx:LayoutItem Caption="Username">
                                               <LayoutItemNestedControlCollection>
                                                   <dx:LayoutItemNestedControlContainer runat="server">
                                                       <dx:ASPxTextBox runat="server" 
                                                           ID="txtUsername" 
                                                           ClientEnabled="true" 
                                                           ClientInstanceName="txtUsername">
                                                           
                                                           <ValidationSettings>
                                                               <RequiredField  IsRequired="true"/>
                                                           </ValidationSettings>
                                                       </dx:ASPxTextBox>
                                                   </dx:LayoutItemNestedControlContainer>
                                               </LayoutItemNestedControlCollection>
                                           </dx:LayoutItem>
                                           <dx:LayoutItem Caption="Password">
                                               <LayoutItemNestedControlCollection>
                                                   <dx:LayoutItemNestedControlContainer runat="server">
                                                       <dx:ASPxTextBox runat="server" 
                                                           ID="txtPassword" 
                                                           Password="true" 
                                                           ClientEnabled="true" 
                                                           ClientInstanceName="txtPassword">
                                                           
                                                        <ValidationSettings>
                                                               <RequiredField  IsRequired="true"/>
                                                           </ValidationSettings>
                                                       </dx:ASPxTextBox>
                                                   </dx:LayoutItemNestedControlContainer>
                                               </LayoutItemNestedControlCollection>
                                           </dx:LayoutItem>
                                           <dx:LayoutItem Caption="">
                                               <LayoutItemNestedControlCollection>
                                                   <dx:LayoutItemNestedControlContainer runat="server">
                                                       <dx:ASPxButton runat="server" AutoPostBack="true" ID="submitBtn" ClientEnabled="true" Text="Sign In" OnClick="btnSubmitLoginCredentials_Click" >
                                                          
                                                       </dx:ASPxButton>
                                                   </dx:LayoutItemNestedControlContainer>
                                               </LayoutItemNestedControlCollection>
                                           </dx:LayoutItem>
                                       </Items>           
                                   </dx:LayoutGroup>
                               </Items>
                            </dx:ASPxFormLayout>

                </dx:PopupControlContentControl>
            </ContentCollection>
    </dx:ASPxPopupControl>
                        
</asp:Content>