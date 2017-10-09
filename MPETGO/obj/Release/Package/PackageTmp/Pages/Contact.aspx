﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Contact.aspx.cs" Inherits="MPETGO.Pages.Contact" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

<meta name="viewport" content="width=device-width, initial-scale=1.0">
<script>
    function ShowPopUp() {
        EmailPopUp.Show();
    }
</script>



    <dx:ASPxPopupControl runat="server" ID="EmailPopUp" ClientInstanceName="EmailPopUp" HeaderText="Contact Four Winds Group support" Modal="true" 
        PopupAnimationType="Fade" AllowDragging="true" AllowResize="true" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" CloseOnEscape="true" 
        OnLoad="EmailPopUp_Load" Width="100%">
        <ClientSideEvents Init="ShowPopUp" />
        <ContentCollection>
            <dx:PopupControlContentControl>
                
                <dx:ASPxFormLayout Theme="iOS" runat="server" >
        <Items>
            <dx:LayoutItem Caption="Name:" CaptionSettings-Location="Top" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txtName" runat="server" Width="300px" ClientEnabled="true" ClientInstanceName="txtName">
                            <ValidationSettings>
                                <RequiredField IsRequired="true" ErrorText="Name Required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
            <dx:LayoutItem RequiredMarkDisplayMode="Required" Caption="Subject" CaptionSettings-Location="Top" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox runat="server" ID="txtSubject" Width="100%" ClientEnabled="true" ClientInstanceName="txtSubject">
                            <ValidationSettings>
                                <RequiredField IsRequired="true" ErrorText="Subject required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
            <dx:LayoutItem RequiredMarkDisplayMode="Required" Caption="Email" CaptionSettings-Location="Top" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txtEmail" runat="server" Width="100%" ClientEnabled="true" ClientInstanceName="txtEmail">
                            <ValidationSettings>
                                <RequiredField IsRequired="true" ErrorText="Email address required" />
                                <RegularExpression ErrorText="Invalid Email Address"
                                    ValidationExpression=".*@.*\..*" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>


            <dx:LayoutItem RequiredMarkDisplayMode="Required" Caption="Body" CaptionSettings-Location="Top" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txtBody" runat="server" ClientEnabled="true" ClientInstanceName="txtBody" Width="100%"
                            TextMode="MultiLine">
                            <ValidationSettings ErrorText="Body text required" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="true" ErrorText=""  />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>

            <dx:LayoutItem Caption="" Width="100%" CaptionSettings-Location="Top">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>

                        <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" />
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" CaptionSettings-Location="Top" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton ID="btnSend" runat="server" AutoPostBack="true" Text="Send" Width="100%" OnClick="btnSend_Click" >                         
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>

            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxLabel ID="lblMessage" runat="server" Text="" ForeColor = "Green"></dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>

            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    

</asp:Content>