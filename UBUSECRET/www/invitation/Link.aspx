<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="Link.aspx.cs" Inherits="www.invitation.Link" Title="Accept Secret" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Panel runat="server" ID="Inaccessible" Visible="false">
        <asp:Label runat="server" Text="This invitation has expired"></asp:Label>
        <asp:Label runat="server" ID="ExpiredTime"></asp:Label>
    </asp:Panel>

    <asp:Panel runat="server" ID="ValidInvitation" Visible="false">
        <asp:Label runat="server" Text="Do you wanna get this secret eh!"></asp:Label>
        <h2>
            <asp:Label runat="server" ID="SecretTitle"></asp:Label></h2>
        <button runat="server" id="AcceptButton">Add this secert to your libray</button>
    </asp:Panel>
</asp:Content>
