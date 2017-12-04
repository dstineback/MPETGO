<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="index.aspx.cs" Inherits="MPETGO.index" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


<meta name="viewport" content="width=device-width, initial-scale=1.0">



<script>
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
        localStorage.setItem("lat", lat.ToString());
        localStorage.setItem("long", long.ToString());
        localStorage.setItem("testLat", JSON.stringify(lat));
        localStorage.setItem("testLong", JSON.stringify(long));
    }
  
   

</script>



    <dx:ASPxHiddenField runat="server" ID="getLatValue" ClientInstanceName="getLatValue" ClientSideEvents-Init="getLocation"><ClientSideEvents Init="getLocation" /></dx:ASPxHiddenField>
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
