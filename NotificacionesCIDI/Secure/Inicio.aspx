<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Inicio.aspx.cs" Inherits="NotificacionesCIDI.Secure.Inicio" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>Inicio</h1>
        <asp:Button ID="btnMasivoDeuda" runat="server" Text="Masivo Deuda" PostBackUrl="~/Secure/MasivoDeuda.aspx" />
        <asp:Button ID="btnNotificacionesCIDI" runat="server" Text="Notificaciones CIDI"
            PostBackUrl="~/Secure/NotificacionesCIDI.aspx" />
        <asp:Button ID="btnPlantillas" runat="server" Text="Plantillas" PostBackUrl="~/Secure/Plantillas.aspx" />
    </asp:Content>