<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectOxford_ASP.NET_Sample._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <p><asp:Image ID="imgBox" runat="server" /></p>
    <p><asp:Button ID="loadButton" OnClick="LoadButton_Click" Text="Load..." runat="server"/></p>
    <p> <asp:FileUpload ID="fileUploader" runat="server"/> </p>

    <h1>Face Data</h1>
    <p> <asp:Label ID="numOfFacesLabel" Text="" runat="server"></asp:Label> </p>
    <p> <asp:Label ID="ageLabel" Text="" runat="server"></asp:Label> </p>
    <p> <asp:Label ID="genderLabel" Text="" runat="server"></asp:Label> </p>
    <p> <asp:Label ID="glassesLabel" Text="" runat="server"></asp:Label> </p>
    <p></p>
    <h1>Emotions Data</h1>
    <p> <asp:Label ID="emotionsLabel" Text="" runat="server"></asp:Label> </p>

</asp:Content>
