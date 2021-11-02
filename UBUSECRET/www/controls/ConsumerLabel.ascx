<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsumerLabel.ascx.cs" Inherits="www.controls.ConsumerLabel" %>

<asp:Panel runat="server" CssClass="rounded-full bg-white text-black px-3 py-2 flex gap-2 items-center justify-between">
    <asp:Label runat="server" ID="ConsumerName"></asp:Label>
    <button runat="server" onserverclick="DeleteConsumer" type="button" class="rounded-full bg-red-600 text-white p-1 h-5 w-5 flex items-center justify-center text-center align-middle transform hover:scale-105">x</button>
</asp:Panel>
