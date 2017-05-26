<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Logon.aspx.cs" Inherits="MPETGO.Logon" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

                            <dx:ASPxFormLayout runat="server" Theme="iOS">
                               <Items>
                                   <dx:LayoutGroup>
                                       <Items>
                                           <dx:LayoutItem Caption="Username">
                                               <LayoutItemNestedControlCollection>
                                                   <dx:LayoutItemNestedControlContainer>
                                                       <dx:ASPxTextBox runat="server" ID="txtUsername" AutoPostBack="false">
                                                       </dx:ASPxTextBox>
                                                   </dx:LayoutItemNestedControlContainer>
                                               </LayoutItemNestedControlCollection>
                                           </dx:LayoutItem>
                                           <dx:LayoutItem Caption="Password">
                                               <LayoutItemNestedControlCollection>
                                                   <dx:LayoutItemNestedControlContainer>
                                                       <dx:ASPxTextBox runat="server" ID="txtPassword" AutoPostBack="false">
                                                       </dx:ASPxTextBox>
                                                   </dx:LayoutItemNestedControlContainer>
                                               </LayoutItemNestedControlCollection>
                                           </dx:LayoutItem>
                                           <dx:LayoutItem Caption="">
                                               <LayoutItemNestedControlCollection>
                                                   <dx:LayoutItemNestedControlContainer runat="server">
                                                       <dx:ASPxButton runat="server" AutoPostBack="false" ID="submitBtn" Text="Sign In" OnClick="btnSubmitLoginCredentials_Click"></dx:ASPxButton>
                                                   </dx:LayoutItemNestedControlContainer>
                                               </LayoutItemNestedControlCollection>
                                           </dx:LayoutItem>
                                       </Items>           
                                   </dx:LayoutGroup>
                               </Items>
                            </dx:ASPxFormLayout>
                        
</asp:Content>