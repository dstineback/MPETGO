<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="index.aspx.cs" Inherits="MPETGO.index" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


<meta name="viewport" content="width=device-width, initial-scale=1.0">



<script>
    var lat = localStorage.getItem("Lat");
    var lng = localStorage.getItem("Lng");
   

</script>
<script>
    function getValue() {
        var latValue = document.getElementById("latValue");
        var lngValue = document.getElementById("lngValue");
        latValue.value = lat;
        lngValue.value = lng;
    }
</script>


    <dx:ASPxHiddenField runat="server" ID="getLatValue" ClientInstanceName="getLatValue" ClientSideEvents-Init="getValue"><ClientSideEvents Init="getValue" /></dx:ASPxHiddenField>
   <div>
       <h1>Welcome To M-PET Go</h1> 
       <div>
           

          <%-- <asp:TextBox runat="server" ID="latValue" ClientIDMode="Static" OnInit="setSessionLat">
               
           </asp:TextBox>
           <asp:TextBox runat="server" ID="lngValue" ClientIDMode="Static" OnLoad="setSessionLng">
               
           </asp:TextBox>--%>
           
       </div>

   </div>

    

    
</asp:Content>
