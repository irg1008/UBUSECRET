<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="www.Error" Title="Error" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="w-full h-96 text-center flex flex-col items-center justify-center gap-4">
        <h1 class="text-9xl">404</h1>
        <h1>Not Found</h1>
        <asp:HyperLink CssClass="text-gray-400 hover:underline cursor-pointer" NavigateUrl="~/default.aspx" runat="server">Go Home</asp:HyperLink>
    </div>
</asp:Content>
