<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="WorkRequestForm.aspx.cs" Inherits="MPETGO.Pages.WorkRequestForm" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1.0">

        <dx:ASPxFormLayout ID="ASPxFormLayout1"
            runat="server" Theme="iOS" EnableTheming="True" Width="100%">
            <Items>
                <dx:LayoutItem Caption="Work Request Issue" Width="100%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxMemo runat="server" ID="ASPxFormLayout1_E6">
                            </dx:ASPxMemo>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutGroup Width="100%">
                    <Items>
                        <dx:LayoutItem Caption="Object ID" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E1"
                                        runat="server">
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Object Description" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E2"
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutItem Caption="Created Date" Width="100%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxDateEdit ID="ASPxFormLayout1_E3"
                                runat="server">
                            </dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Priority" Width="100%">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox ID="ASPxFormLayout1_E4" 
                                runat="server">
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:ASPxFormLayout>
       
    </asp:Content>
