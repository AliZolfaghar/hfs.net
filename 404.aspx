﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="404.aspx.vb" Inherits="_404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>404</title>

    <style>
        h1 {
            font-size: 80px;
            font-weight: 800;
            text-align: center;
            font-family: 'Roboto', sans-serif;
        }

        h2 {
            font-size: 25px;
            text-align: center;
            font-family: 'Roboto', sans-serif;
            margin-top: -40px;
        }

        p {
            text-align: center;
            font-family: 'Roboto', sans-serif;
            font-size: 12px;
        }

        .container {
            width: 300px;
            margin: 0 auto;
            margin-top: 15%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">



        <div class="container">
            <h1>404</h1>
            <h2>Page Not Found</h2>
            <p>The Page you are looking for doesn't exist or an other error occured. Go to <a href="/">Home Page.</a></p>
        </div>

    </form>
</body>
</html>
