<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Secrets.ascx.cs" Inherits="www.controls.SecretsControl" %>


<div>
    <h2>MY SECRETS</h2>
    <hr />
</div>
<asp:Panel runat="server" ID="OwnedSecrets" CssClass="flex gap-2 flex-wrap w-full text-gray-200">
    <button runat="server" onserverclick="GoToAddSecret" title="Add new secret" class="h-20 p-0 w-20 bg-gray-500 opacity-60 text-8xl rounded-lg text-white flex items-center justify-center">+</button>
</asp:Panel>

<div>
    <h2>SHARED WITH ME</h2>
    <hr />
</div>
<asp:Panel runat="server" ID="InvitedSecrets">
    <asp:Label runat="server" CssClass="text-gray-200" ID="SharedEmpty" Text="No one has shared any secret with you for the moment"></asp:Label>
</asp:Panel>
