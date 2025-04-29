<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotificacionesGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.NotificacionesGeneral" Title="Masivo Deuda"
    MasterPageFile="~/Master/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se eliminan las referencias duplicadas a jQuery y DataTables, pues ya se cargan desde el MasterPage -->
    <script type="text/javascript">
        jQuery(document).ready(function () {
            var table = jQuery('#<%= gvMasivosAut.ClientID %>');
            var headerCount = table.find('thead tr th').length;
            var firstRowTDCount = table.find('tbody tr:first td').length;
            if (headerCount === firstRowTDCount) {
                table.DataTable({
                    "paging": true,
                    "searching": true,
                    "language": {
                        "url": "https://cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json"
                    }
                });
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="padding-right: 4%; padding-left: 2%;">
        <div class="row">
            <div class="col-12" style="padding: 0px;">
                <h1 style="font-size: 36px !important; font-weight: 600 !important; margin-bottom: 5px !important; display: flex !important; align-items: start !important;">
                    <span class="fa fa-car-side" style="color: #367fa9; border-right: solid 3px; padding-right: 10px;"></span>
                    <span style="font-size: 20px !important; padding-left: 8px !important; margin-top: -5px !important;">Industria y Comercio</span> </h1>
                <h1 style="font-size: 16px !important; font-weight: 500 !important; color: gray !important; margin-left: 65px !important; margin-top: -24px !important;">Notificaciones - Listado</h1>
                <hr style="margin-top: 5px; border: 2px solid #c09e76; margin-bottom: 20px; opacity: 1;">
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:GridView AutoGenerateColumns="false" CssClass="table table-striped table-hover"
                    ID="gvMasivosAut" runat="server" UseAccessibleHeader="true">
                    <Columns>
                        <asp:BoundField DataField="Nro_Emision" HeaderText="Emisión Nro." />
                        <asp:BoundField DataField="Fecha_Emision" HeaderText="Fecha de Emisión"
                            DataFormatString="{0:d}" />
                        <asp:TemplateField HeaderText="Subsistema">
                            <ItemTemplate>
                                <%# GetSubsistema(Eval("Subsistema")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Cantidad_Reg" HeaderText="Registros" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a
                                    href='DetNotificacionesGeneral.aspx?nro_emision=<%# Eval("Nro_Emision") %>&subsistema=<%# Eval("subsistema") %>'>
                                    <span class="fa fa-envelope-circle-check"
                                        style="color: darkcyan"></span>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
