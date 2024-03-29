﻿<%@ MasterType VirtualPath="~/master/Nested_Form.master" %>

<%@ Page Language="C#" MasterPageFile="~/master/Nested_Form.Master" AutoEventWireup="true" CodeBehind="~/auth/SignUp.aspx.cs" Inherits="www.auth.SignUp" Title="Sign Up" %>

<asp:Content runat="server" ContentPlaceHolderID="Form_Title">
    sign up
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Form_Body">
    <label>
        Name
        <asp:TextBox autofocus="autofocus" runat="server" ID="Name_Input" AutoCompleteType="FirstName" Text="" placeholder="Juancho Gómez"></asp:TextBox>
        <asp:Label CssClass="error" ID="NameError" runat="server" />
    </label>
    <label>
        Email
        <asp:TextBox runat="server" ID="Email_Input" AutoCompleteType="Email" TextMode="Email" Text="" placeholder="example@email.com"></asp:TextBox>
        <asp:Label CssClass="error" ID="EmailError" runat="server" />
    </label>
    <label>
        Password
        <asp:TextBox runat="server" ID="Password_Input" TextMode="Password" Text="" placeholder="G6D6M4Q$Z7cYmb"></asp:TextBox>
        <asp:Label CssClass="error" ID="PasswordError" runat="server" />
    </label>
    <label>
        Confirm Password
        <asp:TextBox runat="server" ID="ConfirmPassword_Input" TextMode="Password" Text="" placeholder="G6D6M4Q$Z7cYmb"></asp:TextBox>
        <asp:Label CssClass="error" ID="ConfirmPasswordError" runat="server" />
    </label>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Form_Bottom">
    <p class="text-sm">
        Already registered?
        <asp:HyperLink CssClass="underline cursor-pointer" NavigateUrl="~/auth/LogIn.aspx" runat="server">Log In here</asp:HyperLink>
    </p>
    <button runat="server" id="Submit_Button" onserverclick="Submit_Form" type="submit" class="flex gap-4 justify-around items-center">
        <p>continue</p>
        <asp:Image ID="button_arrow" ImageUrl="~/static/arrow.png" AlternateText="arrow" runat="server" />
    </button>
</asp:Content>
