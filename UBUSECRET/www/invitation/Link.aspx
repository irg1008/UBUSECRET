<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="Link.aspx.cs" Inherits="www.invitation.Link" Title="Accept Secret" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="max-w-xl text-center text-white">
        <asp:Panel runat="server" ID="Inaccessible" Visible="false" CssClass="flex flex-col gap-8 mt-10">
            <h2>
                <asp:Label runat="server" Text="This invitation has expired"></asp:Label>
            </h2>
            <asp:Label CssClass="tetx-white" runat="server" ID="ExpiredTime"></asp:Label>
        </asp:Panel>

        <asp:Panel runat="server" ID="ValidInvitation" Visible="false" CssClass="mt-10 flex flex-col gap-8 items-center justify-center">
            <h2>
                <asp:Label runat="server" ID="ValidInvitationText"></asp:Label>
            </h2>
            <div class="flex flex-col gap-4 items-center justify-center">
                <button runat="server" id="AcceptButton" class="bg-green-600">Add this secret to your library</button>
                <a href="javascript:history.go(-1)" class="underline">Cancel</a>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="IsOwner_Panel" Visible="false">
            <h2>You already own this secret. Invitations can only be accepted by other users</h2>
        </asp:Panel>

        <asp:Panel runat="server" ID="NotLogged_Panel" Visible="false" CssClass="flex flex-col gap-4 items-center justify-center">
            <h2>You need to be logged in to accept an invitation</h2>
            <button runat="server" onserverclick="LogInAndRedirect" class="text-black bg-white font-bold">Log In</button>
        </asp:Panel>

        <asp:Panel runat="server" ID="AlreadyHasAccess_Panel" Visible="false">
            <h2>You already have access to this secret</h2>
        </asp:Panel>
    </div>
</asp:Content>
