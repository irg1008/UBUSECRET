<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="Secret.aspx.cs" Inherits="www.details.SecretDetails" Title="Secret Details" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <h1>SECRET DETAILS</h1>
    <div class="w-full max-w-2xl gap-8 mt-8 bg-white text-white bg-opacity-10 shadow-inner rounded-2xl border-4 border-white p-8 flex flex-col">

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

            <div class="flex gap-4">
                <button runat="server" onserverclick="RemoveSecret" class="bg-red-600 text-white">Remove secret</button>
                <button runat="server" onserverclick="OpenPopUp" class="bg-green-600 text-white">Create invitation link</button>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="DetachContainer" Visible="false">
            <button runat="server" id="DetachButton" class="bg-red-600 text-white">Forget secret</button>
        </asp:Panel>
    </div>

    <asp:Panel runat="server" Visible="false" ID="InvitationPopUp" CssClass="fixed w-full h-screen top-0 left-0 flex items-center justify-center">
        <div class="z-10 w-96 bg-green-500 border-4 rounded-2xl p-4 text-white border-green-100">
            <asp:Panel runat="server" ID="Form">
                <label>
                    Expiry time
                <input runat="server" autofocus="autofocus" id="ExpiryTime_Input" type="datetime-local" />
                    <asp:Label CssClass="error" ID="ExpiryTime_Error" runat="server" />
                </label>
                <button runat="server" onserverclick="CreateLink" type="submit" class="bg-white text-black font-semibold mt-4">
                    Create link
                </button>
            </asp:Panel>

            <asp:Panel runat="server" Visible="false" ID="LinkContainer">
                <p class="text-lg font-bold">Copy this and share it to grant other users access to his secret</p>
                <div class="mt-4 text-black bg-white border-2 border-gray-600 rounded-2xl flex overflow-hidden">
                    <code class="m-2">
                        <asp:Label CssClass="break-all" runat="server" ID="InvitationLink"></asp:Label>
                    </code>
                </div>
            </asp:Panel>
        </div>
        <div class="absolute top-0 left-0 w-full h-full bg-black bg-opacity-20 backdrop-filter backdrop-blur" id="BG">
            <button runat="server" onserverclick="ClosePopUp" class="w-full h-full opacity-0 hover:opacity-0 p-0 m-0"></button>
        </div>
    </asp:Panel>
</asp:Content>
