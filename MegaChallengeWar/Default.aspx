﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MegaChallengeWar.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Play war!<br />
        <br />
        <asp:Button ID="playButton" runat="server" OnClick="playButton_Click" Text="Play" />
        <br />
    
    </div>
        <asp:Label ID="gamePlayLabel" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="winnerLabel" runat="server"></asp:Label>
    </form>
</body>
</html>