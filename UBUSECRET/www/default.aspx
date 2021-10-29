<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="www._default" Title="Home" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <form runat="server">
        <asp:Panel runat="server" ID="Dashboard" Visible="false">
            <button runat="server" onserverclick="LogOut" type="button">Log Out</button>
        </asp:Panel>
        <asp:Panel runat="server" ID="Home" Visible="false">
            <asp:HyperLink CssClass="underline cursor-pointer" NavigateUrl="~/auth/LogIn.aspx" runat="server">Log In</asp:HyperLink>
            <asp:HyperLink CssClass="underline cursor-pointer" NavigateUrl="~/auth/SignUp.aspx" runat="server">Sign Up</asp:HyperLink>
        </asp:Panel>
    </form>
</asp:Content>
