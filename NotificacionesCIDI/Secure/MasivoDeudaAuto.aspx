<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasivoDeudaAuto.aspx.cs" Inherits="NotificacionesCIDI.Secure.MasivoDeudaAuto" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Masivo Deuda Automotor</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />

    <link href="../App_Themes/Main/css/bootstrap.min.css" rel="stylesheet" />
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
    <link href="../App_Themes/fontawesome/css/all.css" rel="stylesheet" />
</head>
<body class="skin-blue sidebar-mini sidebar-collapse">
    <form id="form1" runat="server">

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
        <header class="clearfix no-mobile print"
            style="text-align: left; position: fixed; width: 100%; z-index: 1000; background-color: white; margin-top: -5px; height: 90px;">
            <div class="row" style="padding-top: 20px; padding-bottom: 0px;">
                <div class="col-4" style="padding-left: 4%;">
                    <a href="/BackEnd/Home.aspx">
                        <img src="../App_Themes/Main/img/LogoPablo.png" style="height: 50px;" />
                    </a>
                </div>
                <div class="col-md-4" style="align-items: center; display: flex; text-align: center; height: 60px;">
                    <a href="#" style="width: 100%; text-decoration: none;">
                        <h2 class="section-title__title"
                            style="color: #6f6f6e; font-size: 24px; line-height: 10px; text-decoration-line: overline;">
                            <img src="../App_Themes/Main/img/vecino.png" style="height: 50px;" />
                            SIIMVA WEB
                            <img src="../App_Themes/Main/img/condor.png" style="height: 70px; margin-top: -10px;" />
                        </h2>
                    </a>
                </div>
                <div class="col-md-4" style="padding-right: 5%; align-items: center; display: inline-grid; text-align: right; height: 60px;">
                    <div class="dropdown">
                        <button class="btn-usuario" type="button"
                            style="border: none; background-color: transparent; display: inline-flex;"
                            data-bs-toggle="dropdown" aria-expanded="false">
                            <img id="imgUsuario" runat="server" src="~/App_Themes/Main/img/usuario.png" class="img-thumbnail" alt="..." style="height: 55px; border: none;" />
                            <ul style="color: gray; list-style: none; text-align: left; padding-left: 0; margin-bottom: 0px;">
                                <li id="liNombre" runat="server">Ignacio Martin</li>
                                <li id="liApellido" runat="server">Velez Spitale</li>
                            </ul>
                        </button>
                        <ul class="dropdown-menu">
                            <li style="display: grid; padding: 15px 25px 0px 25px" class="li-usuario">
                                <strong id="mnuPcApellido" runat="server">Velez Spitale</strong>
                                <span id="mnuPcNombre" runat="server">Ignacio Martin</span></li>
                            <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                <span style="display: ruby;">Oficina: </span>
                                <span style="display: block; margin-left: 10px;" id="SpanOficina" runat="server"></span></li>
                            <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                <span style="display: ruby;">CUIT: </span>
                                <span style="display: block; margin-left: 10px;" id="mnuPcCuit" runat="server">23-27.173.499-9</span></li>
                            <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                <span style="display: block;">CIDI: </span>
                                <span style="display: block; margin-left: 10px;" id="mnuPcNivelCidi" runat="server">Nivel 2</span></li>
                            <li style="padding: 15px; border-top: solid 1px lightgray; margin-top: 15px;">
                                <a class="dropdown-item" href="#" runat="server"
                                    id="btnCerraSession" onserverclick="btnCerraSession_ServerClick" style="text-align: center; border: solid 3px lightgray; border-radius: 10px; padding: 8px;">Cerrar Sesion</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="container-fluid"
                style="background: linear-gradient(87deg, rgb(148 23 23) 0%, rgba(255,35,0,1) 41%, rgb(255 233 0) 79%);">
                <div class="row" style="height: 10px;">
                    <div class="col-md-12" style="padding-top: 0px;">
                    </div>
                </div>
            </div>
        </header>
        <div class="wrapper">
            <div class="content-wrapper" style="padding: 30px;">
                <section class="content" style="margin-top: 50px;">
                    <div class="row">
                        <div class="box-body no-padding">
                            <div class="nav-tabs-custom" style="padding-top: 30px;">
                                <ul class="nav nav-tabs">
                                    <li class="nav-item"><a class="nav-link" href="MasivoDeuda.aspx">INMUEBLES</a></li>
                                    <li class="nav-item"><a class="nav-link" href="MasivoDeudaIndyCom.aspx">INDUSTRIA Y COMERCIO</a></li>
                                    <li class="nav-item"><a class="nav-link active" href="#tab_3" data-toggle="tab" aria-expanded="false">AUTOMOTORES</a></li>
                                    <%--<li class="pull-right">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-success"
                                            Text="Exportar a Excel" OnClick="btnExportExcel_Click" /></li>--%>
                                </ul>
                                <div class="tab-content" style="min-height: 700px;">
                                    <div class="tab-pane" style="min-height: 300px;" id="tab_1">
                                        <div class="box">
                                            <!-- /.box-header -->
                                            <div class="box-body no-padding">
                                                <table class="table table-striped">
                                                    <tbody runat="server" id="table">
                                                    </tbody>
                                                </table>
                                            </div>

                                            <!-- /.box-body -->
                                        </div>
                                        <div style="text-align: right;">
                                            <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="Volver" />
                                        </div>
                                    </div>
                                    <div class="tab-pane" style="min-height: 300px;" id="tab_2">
                                        <div class="box">
                                            <!-- /.box-header -->
                                            <div class="box-body no-padding">
                                                <table class="table table-striped">
                                                    <tbody runat="server" id="table2">
                                                    </tbody>
                                                </table>
                                            </div>

                                            <!-- /.box-body -->
                                        </div>
                                        <div style="text-align: right;">
                                            <asp:Button ID="Button4" CssClass="btn btn-primary" runat="server" Text="Volver" />
                                        </div>
                                    </div>
                                    <div class="tab-pane active" style="min-height: 300px;" id="tab_3">
                                        <div class="box">
                                            <div class="col-md-12">
                                                <div id="divFiltros" runat="server">
                                                    <div class="row" style="margin-top: 25px;">
                                                        <div class="col-md-12">
                                                            <h3 style="color: #367fa9;">Filtros</h3>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label>Fecha</label>
                                                            <asp:DropDownList ID="ddlFecha" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="Sin filtro" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Deuda a partir del" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Deuda hasta" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Deuda entre" Value="2">Deuda entre</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <asp:TextBox ID="txtDesde" Enabled="false" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <asp:TextBox ID="txtHasta" Enabled="false" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <label>Categoria Deuda</label>
                                                            <asp:DropDownList ID="DDLCatDeuda" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="Toda" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Seleccionada" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:ListBox ID="lstCatDeuda" Height="90" CssClass="form-control list-group" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                        </div>


                                                        <div class="col-md-2">
                                                            <label>Barrio</label>
                                                            <asp:ListBox ID="lstBarrios" Height="143" CssClass="form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <label>Zona</label>
                                                            <asp:ListBox ID="lstZonas" Height="143" CssClass="form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
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
                                                                    <asp:ListBox ID="lstTipoDeuda" SelectionMode="Multiple"
                                                                        Height="143" CssClass="form-control list-group" runat="server">
                                                                        <asp:ListItem>Deuda Judicial</asp:ListItem>
                                                                        <asp:ListItem>Deuda Pre-Judicial</asp:ListItem>
                                                                        <asp:ListItem>Deuda Administrativa</asp:ListItem>
                                                                        <asp:ListItem>Deuda Normal</asp:ListItem>
                                                                    </asp:ListBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlFiltroDeuda" CssClass="form-control" runat="server">
                                                                        <asp:ListItem Text="Sin filtro" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Mayor a" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Menor a" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Entre" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtMontoDesde" MIN="0" Enabled="false" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtMontoHasta" MIN="0" Enabled="false" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <br />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12" style="text-align: right;">
                                                            <!--<button type="button" class="btn btn-warning" id="btnVolver" runat="server">
                                                                <span class="glyphicon glyphicon-arrow-left"></span>&nbsp;Volver
                                                            </button>-->
                                                            <button type="button" class="btn btn-outline" id="btnFiltros" runat="server"
                                                                onserverclick="btnFiltros_ServerClick">
                                                                <span class="fa fa-filter"></span>&nbsp;Aplicar Filtros
                                                            </button>

                                                        </div>
                                                    </div>

                                                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>

                                                </div>

                                                <div class="progress progress-striped active" id="divProgressBar" style="display: none;">
                                                    <strong>Procesando consulta...</strong>
                                                    <div class="progress-bar" role="progressbar"
                                                        aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"
                                                        style="width: 45%">
                                                        <span class="sr-only">45% completado</span>
                                                    </div>
                                                </div>
                                                <div id="divResultados" runat="server" visible="false" style="margin-top: 20px;">
                                                    <div class="row">
                                                        <div class="12" style="text-align: right">
                                                            <button type="button" class="btn btn-outline-danger" id="btnClearFiltros"
                                                                runat="server" onserverclick="btnClearFiltros_ServerClick">
                                                                <span class="fa fa-filter-circle-xmark"></span>&nbsp;Limpiar Filtros
                                                            </button>
                                                            <button type="button" id="btnNoti"
                                                                class="btn btn-outline" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                                                <span class="fa fa-sheet-plastic"></span>&nbsp; Generar notificación CIDI
                                                            </button>
                                                            <button type="button" runat="server" id="btnExportExcel" onserverclick="btnExportExcel_ServerClick"
                                                                class="btn btn-outline-excel" data-toggle="modal" data-target="#page-change-name">
                                                                <span class="fa fa-sheet-plastic"></span>&nbsp; Exportar a Excel
                                                            </button>
                                                        </div>
                                                    </div>


                                                    <div class="row" style="margin-top: 20px;">
                                                        <div class="col-md-12"
                                                            style="height: 320px; overflow-y: scroll; border: solid lightgray; border-radius: 15px;">
                                                            <asp:GridView ID="gvDeuda" CssClass="table" runat="server" OnRowDataBound="gvDeuda_RowDataBound"
                                                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Denominación">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNroCta" runat="server" Text=""></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="titular" ControlStyle-Width="10%" HeaderText="Titular"></asp:BoundField>
                                                                    <asp:BoundField DataField="cuil" ControlStyle-Width="10%" HeaderText="CUIT"></asp:BoundField>
                                                                    <asp:BoundField DataField="nom_calle" ControlStyle-Width="10%" HeaderText="Calle"></asp:BoundField>
                                                                    <asp:BoundField DataField="nro_dom_esp" ControlStyle-Width="10%" HeaderText="Nro"></asp:BoundField>
                                                                    <asp:BoundField DataField="barrio" ControlStyle-Width="10%" HeaderText="Barrio"></asp:BoundField>
                                                                    <asp:BoundField DataField="zona" ControlStyle-Width="10%" HeaderText="Zona"></asp:BoundField>
                                                                    <asp:BoundField DataField="deudaJudicial" ControlStyle-Width="10%" DataFormatString="{0:c}" HeaderText="Judicial"></asp:BoundField>
                                                                    <asp:BoundField DataField="deudaPreJudicial" ControlStyle-Width="10%" DataFormatString="{0:c}" HeaderText="Prejudicial"></asp:BoundField>
                                                                    <asp:BoundField DataField="deudaAdministrativa" ControlStyle-Width="10%" DataFormatString="{0:c}" HeaderText="Administrativa"></asp:BoundField>
                                                                    <asp:BoundField DataField="deudaNormal" ControlStyle-Width="10%" DataFormatString="{0:c}" HeaderText="Normal"></asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding-top: 20px;">
                                                        <div class="col-md-2 form-group">
                                                            <label>Total de registros</label>
                                                            <asp:TextBox ID="txtRegistros" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 form-group">
                                                            <label>Deuda Judicial</label>
                                                            <asp:TextBox ID="txtTotJudicial" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 form-group">
                                                            <label>Deuda Pre-Judicial</label>
                                                            <asp:TextBox ID="txtPreJudicial" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 form-group">
                                                            <label>Deuda Administrativa</label>
                                                            <asp:TextBox ID="txtAdministrativa" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 form-group">
                                                            <label>Deuda Normal</label>
                                                            <asp:TextBox ID="txtNormal" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2 form-group">
                                                            <label>Deuda Total</label>
                                                            <asp:TextBox ID="txtTotal" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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
                            <asp:TextBox ID="txtNombreNoti"
                                CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin-top: 25px;">
                            <label>Descripción</label>
                            <asp:TextBox ID="txtescripcion" TextMode="MultiLine"
                                CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary"
                            data-bs-dismiss="modal">
                            Cancelar</button>
                        <button runat="server" id="btnGenerarNoti"
                            onserverclick="btnGenerarNoti_ServerClick" type="button" class="btn btn-primary">
                            Aceptar</button>
                    </div>
                </div>
            </div>
        </div>


        <script src="../App_Themes/Main/js/jQuery-2.1.4.min.js"></script>
        <script src="../App_Themes/Main/js/jquery-ui-1.10.3.min.js"></script>
        <script src="../App_Themes/Main/js/bootstrap.min.js"></script>
        <script src="../App_Themes/Main/js/bootstrap.bundle.min.js"></script>
        <script src="../App_Themes/fontawesome/js/all.js"></script>


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
                    //$("#lstTipoDeuda").attr("disabled", true);
                }
                else {
                    //$("#lstTipoDeuda").removeAttr("disabled");
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


    </form>
</body>
</html>
