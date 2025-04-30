<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetNotificacionesGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.DetNotificacionesGeneral" Title="Masivo Deuda"
    MasterPageFile="~/Master/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="padding-right: 4%; padding-left: 2%;">
        <div class="row">
            <div class="col-12" style="padding: 0px;">
                <h1 style="font-size: 36px !important; font-weight: 600 !important; margin-bottom: 5px !important; display: flex !important; align-items: start !important;">
                    <span class="fa fa-car-side" id="spanIcono" runat="server" style="color: #367fa9; border-right: solid 3px; padding-right: 10px;"></span>
                    <span id="spanSubsistema" runat="server" style="font-size: 20px !important; padding-left: 8px !important; margin-top: -5px !important;">Industria y Comercio</span> </h1>
                <h1 style="font-size: 16px !important; font-weight: 500 !important; color: gray !important; margin-left: 65px !important; margin-top: -24px !important;">Notificaciones - Listado</h1>
                <hr style="margin-top: 5px; border: 2px solid #c09e76; margin-bottom: 20px; opacity: 1;">
            </div>
        </div>
        <div class="row">
            <div class="col-4" style="margin-bottom: 20px;">
                <div class="form-group">
                    <label>Estado</label>
                    <asp:DropDownList ID="DDLEstEnv" CssClass="form-control" runat="server"
                        OnSelectedIndexChanged="DDLEstEnv_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Selected="True" Value="-1">Todos</asp:ListItem>
                        <asp:ListItem Value="0" Text="Sin notificar"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Notificado"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Rechazado"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-4" style="text-align: left; padding-top: 23px;">
                <button type="button" id="btnNoti" class="btn btn-outline-primary"
                    data-bs-toggle="modal" data-bs-target="#exampleModal">
                    <span class="fa fa-sheet-plastic"></span>&nbsp;Notificación
                </button>
            </div>
           <div class="col-4 d-flex justify-content-end" style="margin-top:22px">
                 <a href="/Secure/NotificacionesGeneral.aspx?subsistema=<%= subsistema %>" class="fs-6 text-decoration-none" style="color: #367fa9"> 
                     <i class="fa-solid fa-arrow-left"></i> Volver
                 </a>
           </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:GridView AutoGenerateColumns="false" CssClass="table table-striped table-hover"
                    ID="gvMasivosAut" runat="server" DataKeyNames="Nro_Notificacion">
                    <Columns>
                        <asp:TemplateField HeaderText="Seleccionar">
                            <HeaderTemplate>
                                <input type="checkbox" id="chkAll" name="chkAll"
                                    onclick="javascript: SelectAllCheckboxes(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nro_Notificacion" HeaderText="Orden" />
                        <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <div style="text-align: left; padding: 5px;">
                                    <p style="margin: 0; font-weight: bold; font-size: 14px;"><%# Eval("Nombre") %></p>
                                    <p style="margin: 0; font-size: 13px; color: #555;"><%# Eval("Cuit") %></p>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Denominacion">
                            <ItemTemplate>
                                <%# Eval("Denominacion") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado Cidi">
                            <ItemTemplate>
                                <%# GetEstadoCidi(Eval("Cod_estado_cidi")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <div class="col-12"></div>
        </div>
        <div class="row">
            <div class="col-12"></div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" data-bs-backdrop="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Plantilla</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="form-group" style="margin-top: 25px;">
                        <label style="font-weight: bold; font-size: 1rem; padding-bottom: 10px;">Contenido</label>
                        <div id="divContenido" runat="server" class="form-control"
                            style="height: 45vh; overflow-y: auto; border: 1px solid #ced4da; padding: 10px;">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button runat="server" id="Button1" onserverclick="btnGenerarNoti_ServerClick"
                        type="button" class="btn btn-outline-primary">
                        <span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>

        // para seleccionar todos los checkboxs
        function SelectAllCheckboxes(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;

            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
        if (window.jQuery) {
            $(document).ready(function () {
                $('#<%= Button1.ClientID %>').on('click', function (e) {
                    var $btn = $(this);

                    if ($btn.prop('disabled')) {
                        e.preventDefault();
                        return false;
                    }

                    $btn.prop('disabled', true)
                        .addClass('disabled')
                        .html('<span class="spinner-border spinner-border-sm mr-1"></span> Procesando...');

                    setTimeout(function () {
                        $btn.prop('disabled', false)
                            .removeClass('disabled')
                            .html('<span class="fa fa-sheet-plastic"></span> Generar notificación');
                    }, 30000);

                    return true;
                });
            });
        } else {
            console.error('jQuery is not loaded');
        }
    </script>
</asp:Content>
