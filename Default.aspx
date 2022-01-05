<%@ Page Title="" Language="VB" MasterPageFile="~/app.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cnnstr %>" SelectCommand="select 1 as data"></asp:SqlDataSource>

    <h1>HSF.NET</h1>
</asp:Content>

