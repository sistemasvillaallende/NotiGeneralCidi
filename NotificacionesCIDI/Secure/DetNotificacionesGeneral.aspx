<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetNotificacionesGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.DetNotificacionesGeneral" Title="Masivo Deuda"
    MasterPageFile="~/Master/MasterPage.master" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <div class="row">
                <div class="col-12">
                    <div class="row">
                        <div class="col-7" style="padding: 20px; padding-bottom: 0px;">
                            <h1 style="display: inline-block; margin-right: 20px;">Notificación</h1>
                        </div>
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

        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true"data-bs-backdrop="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title"> Plantilla</h5>
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
                        <button runat="server" id="btnGenerarNoti" type="button" class="btn btn-primary">Aceptar</button>
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
                       
        </script>
    </asp:Content>