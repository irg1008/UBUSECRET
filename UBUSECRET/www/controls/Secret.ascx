<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Secret.ascx.cs" Inherits="www.controls.SecretControl" %>

<asp:Panel runat="server" ID="SecretCard" CssClass="p-4 bg-gray-100 text-black rounded-xl flex flex-col justify-between gap-4 w-72 transition shadow-lg transform hover:scale-105">

    <div class="flex flex-col gap-4">
        <strong>
            <asp:Label runat="server" ID="SecretTitle" CssClass="text-2xl"></asp:Label>
        </strong>
        <asp:Label runat="server" ID="SecretMsg"></asp:Label>
    </div>

    <button runat="server" onserverclick="GoToSecretDetails">More Details</button>

    <asp:Panel runat="server" ID="SharedByContainer" Visible="false" CssClass="text-right text-gray-500">
        Shared by:
        <asp:Label runat="server" ID="SharedUserName"></asp:Label>
    </asp:Panel>
</asp:Panel>
