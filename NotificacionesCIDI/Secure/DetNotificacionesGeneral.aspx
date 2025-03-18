<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetNotificacionesGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.DetNotificacionesGeneral" Title="Masivo Deuda"
    MasterPageFile="~/Master/MasterPage.master" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <div class="row">
                <div class="col-12">
                    <div class="row">
                        <!-- Título en la misma fila -->
                        <div class="col-7" style="padding: 20px; padding-bottom: 0px;">
                            <h1 style="display: inline-block; margin-right: 20px;">Notificación</h1>
                        </div>
                        
                        <!-- DropDownList -->
                        <div class="col-3" style="padding: 20px; padding-bottom: 0px;">
                            <div class="form-group">
                                <asp:DropDownList ID="DDLEstEnv" CssClass="form-control" runat="server"
                                    OnSelectedIndexChanged="DDLEstEnv_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="-1">Todos</asp:ListItem>
                                    <asp:ListItem Value="0" Text="Sin notificar"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Notificado"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Rechazado"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
            
                        <!-- Botón Notificar -->
                        <div class="col-2" style="padding: 20px; text-align: right; padding-bottom: 0px;">
                            <button type="button" id="btnNoti" class="btn btn-outline-primary"
                            data-bs-toggle="modal" data-bs-target="#exampleModal">
                            <span class="fa fa-sheet-plastic"></span>&nbsp;Notificación
                        </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-bottom: 20px;">
                <div class="col-md-12">
                    <hr style="margin-top: 5px; border: 2px solid #c09e76; margin-bottom: 20px; opacity: 1;" />
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="table-responsive">
                        <asp:GridView AutoGenerateColumns="false" CssClass="table table-striped table-hover"
                            ID="gvMasivosAut" runat="server">
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
                                <asp:TemplateField ItemStyle-VerticalAlign="Middle" >
                                    <ItemTemplate>
                                        <div style="text-align: center; padding: 5px;">
                                            <p style="margin: 0; font-weight: bold; font-size: 14px;"><%# Eval("Nombre") %></p>
                                            <p style="margin: 0; font-size: 13px; color: #555;"><%# Eval("Cuit") %></p>
                                        </div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:BoundField DataField="Denominacion" HeaderText="Denominacion" />
                                <asp:TemplateField HeaderText="Estado Cidi">
                                    <ItemTemplate>
                                        <%# GetEstadoCidi(Eval("Cod_estado_cidi")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12"></div>
            </div>
            <div class="row">
                <div class="col-12"></div>
            </div>
        </div>

        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Generar Notificación</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p style="text-align: center">
                            Le informamos que la notificación se generara solo con aquellos registros
                            que cuenten con un CUIT valido
                        </p>
                        <div class="form-group">
                            <label style="font-weight: bold; font-size: 1rem; padding-bottom: 10px;">Descripcion</label>
                            <asp:TextBox ID="txtNombreNoti" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin-top: 25px;">
                            <label style="font-weight: bold; font-size: 1rem; padding-bottom: 10px;">Contenido</label>
                            <asp:TextBox ID="txtescripcion" TextMode="MultiLine" CssClass="form-control" runat="server"
                            style="height: 30vh;">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <button runat="server" id="btnGenerarNoti" type="button" class="btn btn-primary">Aceptar</button>
                        <button type="button" class=" btn btn-primary " id="btnNotas" >Notas</button>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="plantillaModalNotas" tabindex="-1" aria-labelledby="ModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">Lista de planillas</h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:GridView ID="gvPlantilla" CssClass="table" runat="server"
                            OnRowDataBound="gvPlantilla_RowDataBound"
                            OnRowCommand="gvPlantilla_RowCommand" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="None"  EnableViewState="true"
                            DataKeyNames="id,contenido">
                          
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775">
                            </AlternatingRowStyle>
                            <Columns>
                                <asp:BoundField DataField="nom_plantilla" ControlStyle-Width="10%"
                                     SortExpression="nom_plantilla" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
            </div>
        </div>
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script>
                function SelectAllCheckboxes(spanChk) {
                    // Added as ASPX uses SPAN for checkbox
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

                $(document).ready(function () {
                    $('.modal-backdrop').remove();
                    $('body').removeClass('modal-open').css('padding-right', '');
                    
                    $('#btnNoti').click(function(e) {
                        e.preventDefault(); // Prevenir comportamiento por defecto
                        
                        $("#<%= txtNombreNoti.ClientID %>").val('');
                        $("#<%= txtescripcion.ClientID %>").val('');
                        
                        $('.modal-backdrop').remove();
                        $('body').removeClass('modal-open').css('padding-right', '');
                        
                        $('#exampleModal').modal('show');
                    });
                    
                    // El resto del código que ya tienes
                    // Cuando se hace clic en una fila del GridView
                    $(document).on('click', '#<%= gvPlantilla.ClientID %> tr', function () {
                        var contenido = $(this).attr("data-contenido");
                        
                        if (contenido) {
                            $("#<%= txtescripcion.ClientID %>").val(contenido);
                            $('#plantillaModalNotas').modal('hide');
                            
                            // Remover cualquier backdrop residual
                            setTimeout(function() {
                                $('.modal-backdrop').remove();
                                $('body').removeClass('modal-open').css('padding-right', '');
                                $('#exampleModal').modal('show');
                            }, 500);
                        }
                    });
                    
                    // Botón de notas
                    $('#btnNotas').click(function () {
                        $('#exampleModal').modal('hide');
                        
                        setTimeout(function() {
                            $('.modal-backdrop').remove();
                            $('body').removeClass('modal-open').css('padding-right', '');
                            $('#plantillaModalNotas').modal('show');
                        }, 500);
                    });
                    
                    // Limpiar datos al cerrar el primer modal
                    $('#exampleModal').on('hidden.bs.modal', function () {
                        $("#<%= txtNombreNoti.ClientID %>").val('');
                        $("#<%= txtescripcion.ClientID %>").val('');
                        
                        // También limpiar cualquier backdrop residual
                        $('.modal-backdrop').remove();
                        $('body').removeClass('modal-open').css('padding-right', '');
                    });
                    
                    // Botón cancelar del segundo modal
                    $('#plantillaModalNotas .btn-secondary').click(function () {
                        $('#plantillaModalNotas').modal('hide');
                        
                        setTimeout(function() {
                            $('.modal-backdrop').remove();
                            $('body').removeClass('modal-open').css('padding-right', '');
                            $('#exampleModal').modal('show');
                        }, 500);
                    });
                    
                    // Botón cancelar del primer modal
                    $('#exampleModal .btn-secondary').click(function () {
                        $("#<%= txtNombreNoti.ClientID %>").val('');
                        $("#<%= txtescripcion.ClientID %>").val('');
                        $('#exampleModal').modal('hide');
                        $('.modal-backdrop').remove();
                        $('body').removeClass('modal-open').css('padding-right', '');
                    });
                    
                    // Botón cerrar (X) del primer modal
                    $('#exampleModal .btn-close').click(function () {
                        $("#<%= txtNombreNoti.ClientID %>").val('');
                        $("#<%= txtescripcion.ClientID %>").val('');
                        $('#exampleModal').modal('hide');
                        $('.modal-backdrop').remove();
                        $('body').removeClass('modal-open').css('padding-right', '');
                    });
                    
                    // Si tu aplicación utiliza UpdatePanel, asegúrate de reinicializar cuando haya postbacks parciales
                    if (typeof Sys !== 'undefined' && Sys.WebForms) {
                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
                            // Reinicializar eventos después de cada postback parcial
                            $('.modal-backdrop').remove();
                            $('body').removeClass('modal-open').css('padding-right', '');
                            
                            // Volver a configurar el botón que abre el modal inicial
                            $('#btnNoti').off('click').on('click', function(e) {
                                e.preventDefault();
                                $("#<%= txtNombreNoti.ClientID %>").val('');
                                $("#<%= txtescripcion.ClientID %>").val('');
                                $('.modal-backdrop').remove();
                                $('body').removeClass('modal-open').css('padding-right', '');
                                $('#exampleModal').modal('show');
                            });
                        });
                    }
                });

                function abrirModalPlantillas() {
                $('#plantillaModal').modal('show');
                    }

            function abrirModalNotas() {
                $('#exampleModal').modal('hide');
                $('#plantillaModalNotas').modal({
                    backdrop: 'static',
                    keyboard: false
                });
                $('#plantillaModalNotas').modal('show');
                }

        </script>
    </asp:Content>