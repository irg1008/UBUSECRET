<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="www.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="https://unpkg.com/purecss@2.0.6/build/pure-min.css" integrity="sha384-Uu6IeWbM+gzNVXJcM9XV3SohHtmWE+3VGi496jvgX1jyvDTXfdK+rfZc8C1Aehk5" crossorigin="anonymous" />
    <link href="https://unpkg.com/tailwindcss@%5E2/dist/tailwind.min.css" rel="stylesheet">
    <style type="text/css">
        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;400;800&display=swap');

        body {
            font-family: "Montserrat", sans-serif;
            padding: 50px;
            background: linear-gradient(90deg, #141E30 0%, #243B55 100%);
        }

        button {
            background-color: orangered;
            text-transform: uppercase;
        }

        input {
            min-height: 30px;
            color: black;
            box-shadow: 0 0 10px 0 rgba(0, 0, 0, 0.2);
            padding: 8px 12px;
            outline: none;
            border: 1px solid black;
        }

        .wrapper {
            display: flex;
            flex-direction: column;
            justify-content: space-around;
            gap: 20px;
        }

        img {
            height: 100px;
            margin: auto;
        }

        h1 {
            text-transform: uppercase;
            margin: auto;
            color: white;
            font-size: 4em;
        }

        .form {
        }
    </style>
</head>
<body>
    <img src="https://i.imgur.com/Hu2UMio.png" />
    <h1>Log In</h1>
    <form id="form1" runat="server">
        <div class="wrapper">
            <input type="text" autofocus="autofocus" />
            <button class="pure-button">Submit</button>
        </div>
    </form>
</body>
</html>
