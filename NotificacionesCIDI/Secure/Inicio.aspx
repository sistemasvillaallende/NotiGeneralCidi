<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Inicio.aspx.cs" Inherits="NotificacionesCIDI.Secure.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Usar DataTables con Bootstrap 5 -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.4/css/dataTables.bootstrap5.min.css" />
    <!-- Cargar primero la librería base de DataTables -->
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js"></script>
    <!-- Luego, la integración con Bootstrap 5 (sin atributo onload) -->
    <script src="https://cdn.datatables.net/1.13.4/js/dataTables.bootstrap5.min.js"></script>
    <script type="text/javascript">
        jQuery(window).on("load", function(){
            jQuery('#gvMasivosAut').DataTable({
                "paging": true,
                "searching": true,
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json"
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-12">
        <div class="col-12">
            <h1>Notificaciones</h1>
        </div>
        <div class="">
            <asp:GridView AutoGenerateColumns="false" ClientIDMode="Static" CssClass="table table-striped table-hover" ID="gvMasivosAut"
                runat="server">
                <Columns>
                    <asp:BoundField DataField="Nro_Emision" HeaderText="Emision" />
                    <asp:BoundField DataField="Fecha_Emision" HeaderText="Fecha" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="Fecha_Vencimiento" HeaderText="Fecha Vencimiento"
                        DataFormatString="{0:d}" />
                    <asp:BoundField DataField="Cod_tipo_notificacion" HeaderText="Cod. Notificacion" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                    <asp:BoundField DataField="Subsistema" HeaderText="Subsistema" />
                    <asp:BoundField DataField="Cantidad_Reg" HeaderText="Registros" />
                    <asp:BoundField DataField="Total" HeaderText="Total" />
                    <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href='DetNotificacionesGeneral.aspx?nro_emision=<%# Eval("Nro_Emision") %>'><span
                                    class="fa fa-envelope-circle-check" style="color: darkcyan"></span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>