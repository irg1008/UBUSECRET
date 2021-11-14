<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="www.admin.Users" Title="Admin Panel" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <h1>ADMIN PANEL</h1>
    <asp:Panel runat="server" CssClass="flex flex-col gap-8 mt-8 w-full" ID="UsersContainer">
        <div>
            <h2>AUTHORIZE USERS</h2>
            <hr />
        </div>
        <asp:Table runat="server" ID="UsersTable" CssClass="self-center" CellPadding="1">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>Email</asp:TableHeaderCell>
                <asp:TableHeaderCell>Role</asp:TableHeaderCell>
                <asp:TableHeaderCell>Last Seen</asp:TableHeaderCell>
                <asp:TableHeaderCell>Actions</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <asp:Label CssClass="text-white mt-4" runat="server" ID="NoUsers" Visible="false" Text="No users left to authorize"></asp:Label>
        <div>
            <h2>LOG</h2>
            <hr />
        </div>
        <asp:Table runat="server" ID="LogTable" CssClass="self-center" CellPadding="1">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Type</asp:TableHeaderCell>
                <asp:TableHeaderCell>Message</asp:TableHeaderCell>
                <asp:TableHeaderCell>Date</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <div runat="server" id="ClearLogButton" class="w-full flex items-center justify-center">
            <button runat="server" onserverclick="ClearLog" type="button" class="bg-red-600">Clear Log</button>
        </div>
        <asp:Label CssClass="text-white mt-4" runat="server" ID="NoLog" Visible="false" Text="Wait for some activity to happen to appear here"></asp:Label>
    </asp:Panel>
</asp:Content>
