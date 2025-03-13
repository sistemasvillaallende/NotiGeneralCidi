<%@ Page Title="Nueva Notificación" Language="C#" AutoEventWireup="true" CodeBehind="MasivoDeudaAuto.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.MasivoDeudaAuto" MasterPageFile="~/Master/MasterPage.master" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script type="text/javascript">
            var pbControl = null;
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                pbControl = args.get_postBackElement();  //the control causing the postback
                pbControl.disabled = true;
                $("#divProgressBar").show("slow");
            }
            function EndRequestHandler(sender, args) {
                pbControl.disabled = false;
                pbControl = null;
                $("#divProgressBar").hide("slow");
            }
        </script>
        <asp:ScriptManager ID="ScriptManager1" ClientIDMode="AutoId" runat="server"></asp:ScriptManager>

        <script>
            $(document).ready(function () {
                $("#lstCatDeuda").attr("disabled", true);
            });
            $("#btnAddFilter").click(function () {
                $("#divBuscar").hide("slow");
                $("#divFiltros").show("slow");
                $("#tit").hide("slow");
            });
            $("#btnVolver").click(function () {
                $("#divBuscar").show("slow");
                $("#divFiltros").hide("slow");
                $("#tit").show("slow");
            });
            $("#ddlFecha").change(function () {
                cambioFecha();
            });
            function cambioFecha() {
                var selectedVal = $('#ddlFecha option:selected').attr('value');
                if (selectedVal == 0) {
                    $("#txtHasta").attr("disabled", true);
                    $("#txtDesde").removeAttr("disabled");
                }
                if (selectedVal == 1) {
                    $("#txtDesde").attr("disabled", true);
                    $("#txtHasta").removeAttr("disabled");
                }
                if (selectedVal == 2) {
                    $("#txtDesde").removeAttr("disabled");
                    $("#txtHasta").removeAttr("disabled");
                }
                if (selectedVal == 3) {
                    $("#txtDesde").attr("disabled", true);
                    $("#txtHasta").attr("disabled", true);
                }
                $("#txtHasta").val('');
                $("#txtDesde").val('');
            }
            $("#ddlFiltroDeuda").change(function () {
                if (document.getElementById('ddlFiltroDeuda').selectedIndex == 0) {
                    document.getElementById('lstTipoDeuda').selectedIndex = -1;
                    $("#txtMontoHasta").attr("disabled", true);
                    $("#txtMontoDesde").attr("disabled", true);
                    return;
                }

                var sel = 0;
                $.each($('#lstTipoDeuda :selected'), function () {

                    sel = 1;

                });
                if (sel == 0) {
                    alert('Debe espesificar el tipo de deuda a filtrar');
                    document.getElementById('ddlFiltroDeuda').selectedIndex = 0;
                    $("#txtMontoHasta").attr("disabled", true);
                    $("#txtMontoDesde").attr("disabled", true);
                    return;
                }
                else {
                    cambioMonto();
                }
            });
            function cambioMonto() {
                var selectedVal = $('#ddlFiltroDeuda option:selected').attr('value');
                if (selectedVal == 0) {
                    $("#txtMontoHasta").attr("disabled", true);
                    $("#txtMontoDesde").removeAttr("disabled");
                }
                if (selectedVal == 1) {
                    $("#txtMontoDesde").attr("disabled", true);
                    $("#txtMontoHasta").removeAttr("disabled");
                }
                if (selectedVal == 2) {
                    $("#txtMontoDesde").removeAttr("disabled");
                    $("#txtMontoHasta").removeAttr("disabled");
                }
                if (selectedVal == 3) {
                    $("#txtMontoDesde").attr("disabled", true);
                    $("#txtMontoHasta").attr("disabled", true);
                }
                $("#txtMontoDesde").val('');
                $("#txtMontoHasta").val('');
            }
            $("#btnClearFiltros").click(function () {
                document.getElementById('ddlFecha').selectedIndex = 0;
                document.getElementById('ddlFiltroDeuda').selectedIndex = 0;
                cambioFecha();
                cambioMonto();
                document.getElementById('lstBarrios').selectedIndex = -1;
                document.getElementById('lstZonas').selectedIndex = -1;
                document.getElementById('lstTipoDeuda').selectedIndex = -1;
            });
            $("#ddlBuscar").change(function () {
                if ($("#ddlBuscar").val() == "Denominacion Catastral") {
                    $("#divCatastro").show("slow");
                    $("#divNombre").hide("slow");
                }
                else {
                    $("#divCatastro").hide("slow");
                    $("#divNombre").show("slow");
                }
            });
            $("#DDLCatDeuda").change(function () {

                if ($("#DDLCatDeuda").val() == "1") {
                    $("#lstCatDeuda").attr("disabled", true);
                    document.getElementById('lstCatDeuda').selectedIndex = -1;
                }
                else {
                    $("#lstCatDeuda").removeAttr("disabled");
                }
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $(document).ready(function () {
                    $("#lstCatDeuda").attr("disabled", true);
                });
                $("#btnAddFilter").click(function () {
                    $("#divBuscar").hide("slow");
                    $("#divFiltros").show("slow");
                });
                $("#btnVolver").click(function () {
                    $("#divBuscar").show("slow");
                    $("#divFiltros").hide("slow");
                });
                $("#ddlFecha").change(function () {
                    cambioFecha();
                });
                function cambioFecha() {
                    var selectedVal = $('#ddlFecha option:selected').attr('value');
                    if (selectedVal == 0) {
                        $("#txtHasta").attr("disabled", true);
                        $("#txtDesde").removeAttr("disabled");
                    }
                    if (selectedVal == 1) {
                        $("#txtDesde").attr("disabled", true);
                        $("#txtHasta").removeAttr("disabled");
                    }
                    if (selectedVal == 2) {
                        $("#txtDesde").removeAttr("disabled");
                        $("#txtHasta").removeAttr("disabled");
                    }
                    if (selectedVal == 3) {
                        $("#txtDesde").attr("disabled", true);
                        $("#txtHasta").attr("disabled", true);
                    }
                    $("#txtHasta").val('');
                    $("#txtDesde").val('');
                }
                $("#ddlFiltroDeuda").change(function () {
                    if (document.getElementById('ddlFiltroDeuda').selectedIndex == 0) {
                        $("#txtMontoHasta").attr("disabled", true);
                        $("#txtMontoDesde").attr("disabled", true);
                        document.getElementById('lstTipoDeuda').selectedIndex = -1;
                        return;
                    }

                    var sel = 0;
                    $.each($('#lstTipoDeuda :selected'), function () {

                        sel = 1;

                    });
                    if (sel == 0) {
                        alert('Debe espesificar el tipo de deuda a filtrar');
                        document.getElementById('ddlFiltroDeuda').selectedIndex = 0;
                        $("#txtMontoHasta").attr("disabled", true);
                        $("#txtMontoDesde").attr("disabled", true);
                        return;
                    }
                    else {
                        cambioMonto();
                    }
                });
                function cambioMonto() {
                    var selectedVal = $('#ddlFiltroDeuda option:selected').attr('value');
                    if (selectedVal == 0) {
                        $("#txtMontoHasta").attr("disabled", true);
                        $("#txtMontoDesde").removeAttr("disabled");
                    }
                    if (selectedVal == 1) {
                        $("#txtMontoDesde").attr("disabled", true);
                        $("#txtMontoHasta").removeAttr("disabled");
                    }
                    if (selectedVal == 2) {
                        $("#txtMontoDesde").removeAttr("disabled");
                        $("#txtMontoHasta").removeAttr("disabled");
                    }
                    if (selectedVal == 3) {
                        $("#txtMontoDesde").attr("disabled", true);
                        $("#txtMontoHasta").attr("disabled", true);
                    }
                    $("#txtMontoDesde").val('');
                    $("#txtMontoHasta").val('');
                }
                $("#btnClearFiltros").click(function () {
                    document.getElementById('ddlFecha').selectedIndex = 0;
                    document.getElementById('ddlFiltroDeuda').selectedIndex = 0;
                    cambioFecha();
                    cambioMonto();
                    document.getElementById('lstBarrios').selectedIndex = -1;
                    document.getElementById('lstZonas').selectedIndex = -1;
                });
                $("#ddlBuscar").change(function () {
                    if ($("#ddlBuscar").val() == "Denominacion Catastral") {
                        $("#divCatastro").show("slow");
                        $("#divNombre").hide("slow");
                    }
                    else {
                        $("#divCatastro").hide("slow");
                        $("#divNombre").show("slow");
                    }
                });
                $("#DDLCatDeuda").change(function () {
                    if ($("#DDLCatDeuda").val() == "1") {
                        $("#lstCatDeuda").attr("disabled", true);
                        document.getElementById('lstCatDeuda').selectedIndex = -1;

                    }
                    else {
                        $("#lstCatDeuda").removeAttr("disabled");
                    }
                });
            });
        </script>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="wrapper">
            <div class="content-wrapper">
                <section class="content">
                    <div class="row">
                        <div class="box-body no-padding">
                            <div class="col-md-12">
                                <div id="divFiltros" runat="server">
                                    <div class="row" style="margin-top: 25px;">
                                        <div class="col-md-12">
                                            <h1>Automotores - Nueva Notificación</h1>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Fecha</label>
                                            <asp:DropDownList ID="ddlFecha" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Sin filtro" Value="3">
                                                </asp:ListItem>
                                                <asp:ListItem Text="Deuda a partir del" Value="0">
                                                </asp:ListItem>
                                                <asp:ListItem Text="Deuda hasta" Value="1">
                                                </asp:ListItem>
                                                <asp:ListItem Text="Deuda entre" Value="2">Deuda entre
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtDesde" Enabled="false" TextMode="Date"
                                                        CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtHasta" Enabled="false" TextMode="Date"
                                                        CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <label>Categoria Deuda</label>
                                            <asp:DropDownList ID="DDLCatDeuda" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Toda" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Seleccionada" Value="2">
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:ListBox ID="lstCatDeuda" Height="90" CssClass="form-control list-group"
                                                runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label>Barrio</label>
                                            <asp:ListBox ID="lstBarrios" Height="143" CssClass="form-control"
                                                runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label>Zona</label>
                                            <asp:ListBox ID="lstZonas" Height="143" CssClass="form-control"
                                                runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>Tipo Deuda</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <label>Monto</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:ListBox ID="lstTipoDeuda" SelectionMode="Multiple" Height="143"
                                                        CssClass="form-control list-group" runat="server">
                                                        <asp:ListItem>Deuda Judicial</asp:ListItem>
                                                        <asp:ListItem>Deuda Pre-Judicial</asp:ListItem>
                                                        <asp:ListItem>Deuda Administrativa
                                                        </asp:ListItem>
                                                        <asp:ListItem>Deuda Normal</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlFiltroDeuda" CssClass="form-control"
                                                        runat="server">
                                                        <asp:ListItem Text="Sin filtro" Value="3">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Mayor a" Value="0">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Menor a" Value="1">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Entre" Value="2">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:TextBox ID="txtMontoDesde" MIN="0" Enabled="false"
                                                                TextMode="Number" CssClass="form-control"
                                                                runat="server">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:TextBox ID="txtMontoHasta" MIN="0" Enabled="false"
                                                                TextMode="Number" CssClass="form-control"
                                                                runat="server">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: right;">
                                            <button type="button" class="btn btn-outline-primary" id="btnFiltros"
                                                runat="server" onserverclick="btnFiltros_ServerClick">
                                                <span class="fa fa-filter"></span>&nbsp;Aplicar Filtros
                                            </button>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Text="">
                                    </asp:Label>
                                </div>

                                <div class="progress progress-striped active" id="divProgressBar"
                                    style="display: none;">
                                    <strong>Procesando consulta...</strong>
                                    <div class="progress-bar" role="progressbar" aria-valuenow="45" aria-valuemin="0"
                                        aria-valuemax="100" style="width: 45%">
                                        <span class="sr-only">45% completado</span>
                                    </div>
                                </div>
                                <div id="divResultados" runat="server" visible="false" style="margin-top: 20px;">
                                    <div class="row">
                                        <div class="12" style="text-align: right">
                                            <button type="button" class="btn btn-outline-danger" id="btnClearFiltros"
                                                runat="server" onserverclick="btnClearFiltros_ServerClick">
                                                <span class="fa fa-filter-circle-xmark"></span>&nbsp;Limpiar
                                                Filtros
                                            </button>
                                            <button type="button" id="btnNoti" class="btn btn-outline-primary"
                                                data-bs-toggle="modal" data-bs-target="#exampleModal">
                                                <span class="fa fa-sheet-plastic"></span>&nbsp; Generar
                                                notificación CIDI
                                            </button>
                                            <button type="button" runat="server" id="btnExportExcel"
                                                onserverclick="btnExportExcel_ServerClick"
                                                class="btn btn-outline-success" data-toggle="modal"
                                                data-target="#page-change-name">
                                                <span class="fa fa-sheet-plastic"></span>&nbsp; Exportar
                                                a Excel
                                            </button>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-12"
                                            style="height: 320px; overflow-y: scroll; border: solid lightgray; border-radius: 15px;">
                                            <asp:GridView ID="gvDeuda" CssClass="table" runat="server"
                                                OnRowDataBound="gvDeuda_RowDataBound" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775">
                                                </AlternatingRowStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Denominación">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNroCta" runat="server" Text="">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="titular" ControlStyle-Width="10%"
                                                        HeaderText="Titular">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cuil" ControlStyle-Width="10%"
                                                        HeaderText="CUIT">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nom_calle" ControlStyle-Width="10%"
                                                        HeaderText="Calle">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nro_dom_esp" ControlStyle-Width="10%"
                                                        HeaderText="Nro">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="barrio" ControlStyle-Width="10%"
                                                        HeaderText="Barrio">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="zona" ControlStyle-Width="10%"
                                                        HeaderText="Zona">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="deudaJudicial" ControlStyle-Width="10%"
                                                        DataFormatString="{0:c}" HeaderText="Judicial">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="deudaPreJudicial"
                                                        ControlStyle-Width="10%" DataFormatString="{0:c}"
                                                        HeaderText="Prejudicial"></asp:BoundField>
                                                    <asp:BoundField DataField="deudaAdministrativa"
                                                        ControlStyle-Width="10%" DataFormatString="{0:c}"
                                                        HeaderText="Administrativa"></asp:BoundField>
                                                    <asp:BoundField DataField="deudaNormal" ControlStyle-Width="10%"
                                                        DataFormatString="{0:c}" HeaderText="Normal">
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 20px;">
                                        <div class="col-md-2 form-group">
                                            <label>Total de registros</label>
                                            <asp:TextBox ID="txtRegistros" CssClass="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-md-2 form-group">
                                            <label>Deuda Judicial</label>
                                            <asp:TextBox ID="txtTotJudicial" CssClass="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-md-2 form-group">
                                            <label>Deuda Pre-Judicial</label>
                                            <asp:TextBox ID="txtPreJudicial" CssClass="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-md-2 form-group">
                                            <label>Deuda Administrativa</label>
                                            <asp:TextBox ID="txtAdministrativa" CssClass="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-md-2 form-group">
                                            <label>Deuda Normal</label>
                                            <asp:TextBox ID="txtNormal" CssClass="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-md-2 form-group">
                                            <label>Deuda Total</label>
                                            <asp:TextBox ID="txtTotal" CssClass="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </section>
            </div>
        </div>

        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
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
                            <label>Nombre</label>
                            <asp:TextBox ID="txtNombreNoti" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin-top: 25px;">
                            <label>Descripción</label>
                            <asp:TextBox ID="txtescripcion" TextMode="MultiLine" CssClass="form-control" runat="server">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                            Cancelar</button>
                        <button runat="server" id="btnGenerarNoti" onserverclick="btnGenerarNoti_ServerClick"
                            type="button" class="btn btn-primary">
                            Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </asp:Content>