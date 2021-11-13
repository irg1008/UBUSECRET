<%@ MasterType VirtualPath="~/master/Nested_Form.master" %>

<%@ Page Language="C#" MasterPageFile="~/master/Nested_Form.Master" AutoEventWireup="true" CodeBehind="~/auth/RecoverPassword.aspx.cs" Inherits="www.auth.RecoverPassword" Title="Log In" %>

<asp:Content runat="server" ContentPlaceHolderID="Form_Title">
    Recover password
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Form_Body">
    <label>
        Email
        <asp:TextBox autofocus="autofocus" runat="server" ID="Email_Input" AutoCompleteType="Email" TextMode="Email" Text="" placeholder="example@email.com"></asp:TextBox>
        <asp:Label CssClass="error" ID="EmailError" runat="server" />
    </label>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Form_Bottom">
    <p class="text-sm">
        <asp:HyperLink CssClass="underline cursor-pointer" NavigateUrl="~/auth/LogIn.aspx" runat="server">Go back to log in</asp:HyperLink>
    </p>
    <button runat="server" onserverclick="Submit_Form" type="submit" class="flex gap-4 justify-around items-center">
        <p>new password</p>
        <asp:Image ID="button_arrow" ImageUrl="~/static/arrow.png" AlternateText="arrow" runat="server" />
    </button>
</asp:Content>
