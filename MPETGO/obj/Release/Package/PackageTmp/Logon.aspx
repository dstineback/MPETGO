<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Logon.aspx.cs" Inherits="MPETGO.Logon" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


    <script>
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
                            <dx:ASPxFormLayout runat="server" Theme="iOS">
                               <Items>
                                   <dx:LayoutGroup>
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
                        
</asp:Content>