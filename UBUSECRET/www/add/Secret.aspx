<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="Secret.aspx.cs" Inherits="www.add.CreateSecret" Title="Add Secret" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Panel runat="server" ID="AddContainer" CssClass="w-full flex justify-center items-center flex-col">
        <h1>ADD SECRET</h1>
        <div class="w-full max-w-4xl mt-4 flex flex-col gap-4">
            <label class="text-white">
                Title (Max 25)
                <asp:TextBox runat="server" autofocus="autofocus" ID="SecretTitle" placeholder="A very interesting title" MaxLength="25"></asp:TextBox>
                <asp:Label CssClass="error" ID="SecretTitleError" runat="server" Text="" />
            </label>

            <label class="text-white">
                Message (Max 250)
                <asp:TextBox CssClass="w-full max-h-60" Rows="6" runat="server" TextMode="Multiline" ID="SecretMessage" placeholder="Here you should put something no one should know" MaxLength="250"></asp:TextBox>
                <asp:Label CssClass="error" ID="SecretMessageError" runat="server" Text="" />
            </label>

            <div class="w-full flex items-center justify-center p-8">
                <button runat="server" onserverclick="CreateNewSecret" type="submit" class="w-full bg-green-700 text-white border-2 border-white font-bold">Add</button>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
