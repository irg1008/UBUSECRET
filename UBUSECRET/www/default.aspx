<%@ Page Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="www.Default" Title="Home" %>

<%@ MasterType VirtualPath="~/master/Main.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Panel runat="server" ID="Dashboard" Visible="false" CssClass="w-full flex justify-center items-center flex-col">
        <h1>SECRETS</h1>
        <div class="w-full flex flex-col gap-8 mt-8">
            <Custom:Secrets runat="server" ID="SecretList" />
        </div>
    </asp:Panel>

    <asp:Panel CssClass="eyes flex flex-col items-center justify-between w-full gap-16" runat="server" ID="Home" Visible="false">
        <div class="flex flex-col gap-8 items-center justify-between">
            <div class="logo-wrapper">
                <svg class="logo-container" width="130" height="141" viewBox="0 0 130 141" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path class="path" d="M12.9188 28.311L9.625 28.5053V31.8049V63.0818C9.625 78.5263 14.0971 92.0662 23.2293 103.594C32.3184 115.068 45.8568 124.343 63.6757 131.626L65 132.168L66.3244 131.626C84.1432 124.343 97.6816 115.068 106.771 103.594C115.903 92.0662 120.375 78.5263 120.375 63.0818V31.8049V28.5054L117.081 28.311C100.002 27.3032 83.3035 21.0925 67.0206 9.5801L65 8.15151L62.9794 9.58012C46.6968 21.0925 29.998 27.3032 12.9188 28.311ZM6.25 26.7702C6.25 25.8384 7.00546 25.0828 7.9375 25.0828C27.2494 25.0828 45.9147 18.225 63.9873 4.67094C64.5876 4.22077 65.4124 4.22077 66.0127 4.67094C84.0851 18.225 102.751 25.0828 122.063 25.0828C122.995 25.0828 123.75 25.8384 123.75 26.7702V63.0818C123.75 79.6338 118.875 93.8525 109.301 105.836C99.6871 117.868 85.1758 127.841 65.6188 135.546C65.2209 135.702 64.7793 135.702 64.3815 135.546C44.8242 127.842 30.3128 117.868 20.6992 105.836C11.1248 93.8526 6.25 79.6338 6.25 63.0818V26.7702Z" stroke="white" stroke-width="7" />
                    <path class="eyes" d="M47.6767 59.4355L53.4267 61.747L51.4584 66.0989L45.6247 63.4949L46.4116 69.4944L41.7421 70.0285L41.0985 63.9623L36.0092 67.866L33.1588 64.0653L38.2383 60.515L32.4826 58.1538L34.4012 53.8076L40.2406 56.4612L39.4977 50.4064L44.1672 49.8723L44.7611 55.9442L49.8505 52.0405L52.7505 55.8356L47.6767 59.4355ZM83.2844 55.3627L89.0343 57.6742L87.0661 62.0261L81.2324 59.4221L82.0193 65.4216L77.3497 65.9557L76.7062 59.8895L71.6169 63.7932L68.7665 59.9924L73.8459 56.4422L68.0903 54.081L70.0089 49.7348L75.8482 52.3884L75.1054 46.3336L79.7749 45.7995L80.3688 51.8714L85.4581 47.9677L88.3582 51.7628L83.2844 55.3627Z" fill="white" />
                    <path class="path" d="M52.5 100.5C62 100.333 82.7 96.7 89.5 83.5" stroke="white" stroke-width="7" stroke-linecap="round" />
                </svg>
            </div>
            <div class="text-center">
                <h1>¡WELCOME TO UBUSECRET!
                </h1>
                <h2 class="text-gray-400">The best secret chamber in the world</h2>
            </div>
        </div>

        <div class="flex items-center gap-8">
            <button runat="server" class="p-4 text-xl font-bold bg-white text-black" onserverclick="LogIn">Log In</button>
            <p class="text-white">or</p>
            <button runat="server" class="p-4 text-xl font-bold bg-yellow-400 text-black" onserverclick="SignUp">Sign Up</button>
        </div>
    </asp:Panel>
</asp:Content>
