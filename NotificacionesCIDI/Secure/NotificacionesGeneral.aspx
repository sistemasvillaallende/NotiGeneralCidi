<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotificacionesGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.NotificacionesGeneral" Title="Masivo Deuda"
    MasterPageFile="~/Master/MasterPage.master" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <div class="row">
                <div class="col-12">
                    <h1>Notificaciones</h1>
                </div>
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView AutoGenerateColumns="false" CssClass="table table-striped table-hover"
                                ID="gvMasivosAut" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="Nro_Emision" HeaderText="Emisión" />
                                    <asp:BoundField DataField="Fecha_Emision" HeaderText="Fecha"
                                        DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="Subsistema" HeaderText="Subsistema" />
                                    <asp:BoundField DataField="Cantidad_Reg" HeaderText="Registros" />
                                    <asp:BoundField DataField="SinNotificar" HeaderText="Sin Notificar" />  
                                    <asp:BoundField DataField="Notificadas" HeaderText="Notificado" />  
                                    <asp:BoundField DataField="Rechazadas" HeaderText="Rechazo" />  
                                                                
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a
                                                href='DetNotificacionesGeneral.aspx?nro_emision=<%# Eval("Nro_Emision") %>&subsistema=<%= Request.QueryString["subsistema"] %>'>
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
                <div class="row">
                    <div class="col-12"></div>
                </div>
                <div class="row">
                    <div class="col-12"></div>
                </div>
            </div>
    </asp:Content>