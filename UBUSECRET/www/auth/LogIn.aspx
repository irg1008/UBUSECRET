<%@ MasterType VirtualPath="~/master/Nested_Form.master" %>

<%@ Page Language="C#" MasterPageFile="~/master/Nested_Form.Master" AutoEventWireup="true" CodeBehind="~/auth/LogIn.aspx.cs" Inherits="www.LogIn" Title="Log In" %>

<asp:Content runat="server" ContentPlaceHolderID="Form_Title">
    log in
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Form_Body">
    <label>
        Email
        <asp:TextBox autofocus="autofocus" runat="server" ID="Email_Input" AutoCompleteType="Email" TextMode="Email" Text="" placeholder="example@email.com"></asp:TextBox>
        <asp:Label CssClass="error" ID="EmailError" runat="server" />
    </label>
    <label>
        Password
        <asp:TextBox runat="server" ID="Password_Input" TextMode="Password" Text="" placeholder="G6D6M4Q$Z7cYmb"></asp:TextBox>
        <asp:Label CssClass="error" ID="PasswordError" runat="server" />
    </label>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Form_Bottom">
    <p class="text-sm">
        Not registered yet?
        <asp:HyperLink CssClass="underline cursor-pointer" NavigateUrl="~/auth/SignUp.aspx" runat="server">Sign Up here</asp:HyperLink>
    </p>
    <button runat="server" onserverclick="Submit_Form" type="submit" class="flex gap-4 justify-around items-center">
        <p>continue</p>
        <asp:Image ID="button_arrow" ImageUrl="~/static/arrow.png" AlternateText="arrow" runat="server" />
    </button>
</asp:Content>
