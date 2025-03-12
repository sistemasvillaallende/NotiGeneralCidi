<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetNotificacionesGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.DetNotificacionesGeneral" Title="Masivo Deuda"
    MasterPageFile="~/Master/MasterPage.master" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <div class="row">
                <div class="col-12">
                    <h1>Notificación</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:GridView AutoGenerateColumns="false" CssClass="table" ID="gvMasivosAut" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Nro_Emision" HeaderText="Emisión" />
                            <asp:BoundField DataField="Nro_Notificacion" HeaderText="Nro Notificacion" />
                            <asp:BoundField DataField="Dominio" HeaderText="Dominio" />
                            <asp:BoundField DataField="Circunscripcion" HeaderText="Cir" />
                            <asp:BoundField DataField="Seccion" HeaderText="Sec" />
                            <asp:BoundField DataField="Manzana" HeaderText="Man" />
                            <asp:BoundField DataField="Parcela" HeaderText="Par" />
                            <asp:BoundField DataField="P_h" HeaderText="P_h" />
                            <asp:BoundField DataField="Legajo" HeaderText="Legajo" />
                            <asp:BoundField DataField="Cuit" HeaderText="Cuit" />
                            <asp:BoundField DataField="Nro_Badec" HeaderText="Nro_Badec" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Cod_estado_cidi" HeaderText="Cod estado" />
                            <asp:BoundField DataField="Fecha_Inicio_Estado" HeaderText="Fecha Inicio"
                                DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Fecha_Fin_Estado" HeaderText="Fecha Fin"
                                DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Vencimiento" HeaderText="Vencimiento" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Nro_cedulon" HeaderText="Nro Cedulon" />
                            <asp:BoundField DataField="Debe" HeaderText="Debe" />
                            <asp:BoundField DataField="Monto_original" HeaderText="Monto" />
                            <asp:BoundField DataField="Interes" HeaderText="Interes" />
                            <asp:BoundField DataField="Descuento" HeaderText="Desc" />
                            <asp:BoundField DataField="Importe_pagar" HeaderText="Importe" />
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
    </asp:Content>