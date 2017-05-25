<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Contact.aspx.cs" Inherits="MPETGO.Pages.Contact" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

<meta name="viewport" content="width=device-width, initial-scale=1.0">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<body>
    
    <dx:ASPxFormLayout Theme="iOS" runat="server" SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit"
        SettingsAdaptivity-SwitchToSingleColumnAtWindowInnerWidth="500"
        RequiredMarkDisplayMode="None">
        <Items>
            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txtName" Caption="Name" runat="server">
                            <ValidationSettings>
                                <RequiredField IsRequired="true" ErrorText="Name Required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem RequiredMarkDisplayMode="Required" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox Caption="Subject" runat="server" ID="txtSubject">
                            <ValidationSettings>
                                <RequiredField IsRequired="true" ErrorText="Subject required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>

            <dx:LayoutItem RequiredMarkDisplayMode="Required" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txtEmail" runat="server" Caption="Email">
                            <ValidationSettings>
                                <RequiredField IsRequired="true" ErrorText="Email address required" />
                                <RegularExpression ErrorText="Invalid Email Address"
                                    ValidationExpression=".*@.*\..*" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>


            <dx:LayoutItem RequiredMarkDisplayMode="Required" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txtBody" Caption="Body" runat="server"
                            TextMode="MultiLine">
                            <ValidationSettings ErrorText="Body text required" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="true" ErrorText=""  />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>

            <dx:LayoutItem >
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>

                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem >
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton ID="btnSend" runat="server" Text="Send"
                            OnClick="btnSend_Click"></dx:ASPxButton>

                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>

            <dx:LayoutItem >
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxLabel ID="lblMessage" runat="server" Text="" ForeColor = "Green"></dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    
</body>
</html>
</asp:Content>