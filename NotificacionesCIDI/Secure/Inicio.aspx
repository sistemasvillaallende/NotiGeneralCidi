<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Inicio.aspx.cs" Inherits="NotificacionesCIDI.Secure.Inicio" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>Inicio</h1>
        <asp:Button ID="btnMasivoDeuda" runat="server" Text="Masivo Deuda" PostBackUrl="~/Secure/MasivoDeuda.aspx" />
        <asp:Button ID="btnNotificacionesCIDI" runat="server" Text="Notificaciones CIDI"
            PostBackUrl="~/Secure/NotificacionesCIDI.aspx" />
        <asp:Button ID="btnPlantillas" runat="server" Text="Plantillas" PostBackUrl="~/Secure/Plantillas.aspx" />

        <div class="col-8">
            <div class="col-12" style="padding: 25px;">
                <h1
                    style="font-size: 36px !important; font-weight: 600 !important; margin-bottom: 5px !important; display: flex !important; align-items: start !important;">
                    <span class="fa fa-car-side"
                        style="color: #c09e76; border-right: solid 3px; padding-right: 10px;"></span>
                    <span
                        style="font-size: 20px !important; padding-left: 8px !important; margin-top: 5px !important;  padding-bottom: 8px !important; ">Notificaciones</span>
                </h1>
                <h1
                    style="font-size: 16px !important; font-weight: 500 !important; color: gray !important; margin-left: 65px !important; margin-top: -24px !important;">
                </h1>
            </div>
            <hr style="margin-top: -10px; border: 2px solid #c09e76; margin-bottom: 20px; opacity: 1; width: 100%;" />
            <div class="">
                <asp:GridView AutoGenerateColumns="false" CssClass="table" ID="gvMasivosAut" runat="server">
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
        <div class="row">
            <div class="col-7"></div>
        </div>
        <div class="row">
            <div class="col-7"></div>
        </div>
    </asp:Content>