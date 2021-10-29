<%@ MasterType VirtualPath="~/master/Nested_Form.master" %>

<%@ Page Language="C#" MasterPageFile="~/master/Nested_Form.Master" AutoEventWireup="true" CodeBehind="NewPassword.aspx.cs" Inherits="www.auth.NewPassword" Title="Change Password" MaintainScrollPositionOnPostback="true" %>

<asp:Content runat="server" ContentPlaceHolderID="Form_Title">
    change password
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Form_Body">
    <label>
        Old password
        <asp:TextBox runat="server" ID="OldPassword_Input" TextMode="Password" Text="" placeholder="G6D6M4Q$Z7cYmb"></asp:TextBox>
        <asp:Label CssClass="error" ID="OldPasswordError" runat="server" />
    </label>
    <label>
        New password
        <asp:TextBox runat="server" ID="NewPassword_Input" TextMode="Password" Text="" placeholder="t^44c%pY**dJy5k$"></asp:TextBox>
        <asp:Label CssClass="error" ID="NewPasswordError" runat="server" />
    </label>
    <label>
        Confirm new password
        <asp:TextBox runat="server" ID="ConfirmNewPassword_Input" TextMode="Password" Text="" placeholder="t^44c%pY**dJy5k$"></asp:TextBox>
        <asp:Label CssClass="error" ID="ConfirmNewPasswordError" runat="server" />
    </label>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Form_Bottom">
    <p class="text-sm">
        <asp:HyperLink CssClass="underline cursor-pointer" NavigateUrl="~/auth/LogIn.aspx" runat="server">Go back to log in</asp:HyperLink>
    </p>
    <button runat="server" onserverclick="Submit_Form" type="submit" class="flex gap-4 justify-around items-center">
        <p>continue</p>
        <asp:Image ID="button_arrow" ImageUrl="~/static/arrow.png" AlternateText="arrow" runat="server" />
    </button>
</asp:Content>
