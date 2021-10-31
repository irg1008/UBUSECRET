<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="Secret.aspx.cs" Inherits="www.details.SecretDetails" Title="Secret Details" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="w-full max-w-2xl gap-8 bg-white text-white bg-opacity-10 shadow-inner rounded-2xl border-4 border-white p-8 flex flex-col">

        <div class="flex justify-between items-start gap-8">
            <div>
                <p class="text-gray-400">Name:</p>
                <h1>
                    <asp:Label runat="server" ID="SecretName"></asp:Label>
                </h1>
            </div>
            <div>
                <p class="text-gray-400">Owner:</p>
                <h2>
                    <asp:Label runat="server" ID="SecretOwner"></asp:Label>
                </h2>
            </div>
        </div>

        <div class="flex flex-col gap-2">
            <p class="text-gray-400">Message:</p>
            <asp:Label runat="server" ID="SecretMessage" CssClass="leading-7"></asp:Label>
        </div>

        <hr />

        <asp:Panel runat="server" ID="SecretConsumers" Visible="false" CssClass="flex flex-col gap-8">
            <div class="flex flex-col gap-2">
                <p class="text-gray-400">Consumers:</p>
                <asp:Label runat="server" ID="NoConsumers" Text="No consumers for this secret" Visible="false"></asp:Label>
                <asp:Panel runat="server" ID="ConsumerList" CssClass="flex flex-wrap gap-4" Visible="false"></asp:Panel>
            </div>

            <hr />

            <div>
                <label>
                    Email
                    <asp:TextBox autofocus="autofocus" runat="server" ID="Consumer_Input" TextMode="Email" AutoCompleteType="Email" Text="" placeholder="friend@email.com"></asp:TextBox>
                    <asp:Label CssClass="error" ID="ConsumerError" runat="server" Text="" />
                </label>
                <button runat="server" onserverclick="AddConsumer" type="submit" class="bg-white text-black font-semibold mt-4">Add consumer</button>
            </div>

            <hr />

            <div>
                <button runat="server" onserverclick="RemoveSecret" class="bg-red-600 text-white">Remove secret</button>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="DetachContainer" Visible="false">
            <button runat="server" id="DetachButton" class="bg-red-600 text-white">Forget secret</button>
        </asp:Panel>
    </div>
</asp:Content>
