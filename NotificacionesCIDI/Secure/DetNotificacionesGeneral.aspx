<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetNotificacionesGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.DetNotificacionesGeneral" MasterPageFile="~/Master/MasterPage.master" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <meta charset="UTF-8" />
        <title>Masivo Deuda</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />
        <link href="../App_Themes/Main/css/bootstrap.min.css" rel="stylesheet" />
        <link href="../App_Themes/fontawesome/css/all.css" rel="stylesheet" />
        <style>
            .btn-outline {
                background-color: transparent;
                border: solid darkcyan;
                color: darkcyan;
                font-weight: 500;
                border-radius: 15px;
            }

            .btn-outline-danger {
                background-color: transparent;
                border: solid #dc2626;
                color: #dc2626;
                font-weight: 500;
                border-radius: 15px;
            }

            .btn-outline-excel {
                background-color: transparent;
                border: solid #006e37;
                color: #006e37;
                font-weight: 500;
                border-radius: 15px;
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div style="padding-left: 5%; padding-right:5%;">
            <div class="row">
                <div class="col-12" style="padding: 25px;">
                    <h1
                        style="font-size: 36px !important; font-weight: 600 !important; margin-bottom: 5px !important; display: flex !important; align-items: start !important;">
                        <span class="fa fa-car-side"
                            style="color: #c09e76; border-right: solid 3px; padding-right: 10px;"></span>
                        <span
                            style="font-size: 20px !important; padding-left: 8px !important; margin-top: -5px !important;">Automotores</span>
                    </h1>
                    <h1
                        style="font-size: 16px !important; font-weight: 500 !important; color: gray !important; margin-left: 65px !important; margin-top: -24px !important;">
                        Procuraciones - Cambio de estado masivo</h1>
                    <hr style="margin-top: 5px; border: 2px solid #c09e76; margin-bottom: 20px; opacity: 1;" />
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