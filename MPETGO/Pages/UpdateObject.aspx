<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="UpdateObject.aspx.cs" Inherits="MPETGO.Pages.UpdateObject" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


<meta name="viewport" content="width=device-width, initial-scale=1.0" />

<asp:TextBox runat="server" ID="latValue"></asp:TextBox>
<asp:TextBox runat="server" ID="lngValue"></asp:TextBox>

    
<asp:Button runat="server" ID="saveLatLng" OnClick="updateLatLng"/>


</asp:Content>
