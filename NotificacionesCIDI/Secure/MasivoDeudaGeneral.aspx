<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasivoDeudaGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.MasivoDeudaGeneral" EnableEventValidation="false" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <meta charset="UTF-8" />
        <title>Notificador General</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />

        <link href="../App_Themes/Main/css/bootstrap.min.css" rel="stylesheet" />
        <link href="https://cdn.quilljs.com/1.3.7/quill.snow.css" rel="stylesheet">

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

            .ql-variable {
                background-color: #f0f0f0;
                border: 1px dashed #ccc;
                border-radius: 4px;
                color: #000;
                padding: 2px 4px;
                pointer-events: none;
                /* Evita clics */
                user-select: none;
                /* Evita selección */
            }

            #editor-container1 {
                height: 200px;
                border: 1px solid #ccc;
                margin-bottom: 20px;
            }

            .modal-dialog1 {
                max-width: 60%;
                margin-top: 30vh;
                /* Baja el modal */
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
                    <div class="col-md-4"
                        style="padding-right: 5%; align-items: center; display: inline-grid; text-align: right; height: 60px;">
                        <div class="dropdown">
                            <button class="btn-usuario" type="button"
                                style="border: none; background-color: transparent; display: inline-flex;"
                                data-bs-toggle="dropdown" aria-expanded="false">
                                <img id="imgUsuario" runat="server" src="~/App_Themes/Main/img/usuario.png"
                                    class="img-thumbnail" alt="..." style="height: 55px; border: none;" />
                                <ul
                                    style="color: gray; list-style: none; text-align: left; padding-left: 0; margin-bottom: 0px;">
                                    <li id="liNombre" runat="server">Ignacio Martin</li>
                                    <li id="liApellido" runat="server">Velez Spitale</li>
                                </ul>
                            </button>
                            <ul class="dropdown-menu">
                                <li style="display: grid; padding: 15px 25px 0px 25px" class="li-usuario">
                                    <strong id="mnuPcApellido" runat="server">Velez Spitale</strong>
                                    <span id="mnuPcNombre" runat="server">Ignacio Martin</span>
                                </li>
                                <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                    <span style="display: ruby;">Oficina: </span>
                                    <span style="display: block; margin-left: 10px;" id="SpanOficina"
                                        runat="server"></span>
                                </li>
                                <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                    <span style="display: ruby;">CUIT: </span>
                                    <span style="display: block; margin-left: 10px;" id="mnuPcCuit"
                                        runat="server">23-27.173.499-9</span>
                                </li>
                                <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                    <span style="display: block;">CIDI: </span>
                                    <span style="display: block; margin-left: 10px;" id="mnuPcNivelCidi"
                                        runat="server">Nivel 2</span>
                                </li>
                                <li style="padding: 15px; border-top: solid 1px lightgray; margin-top: 15px;">
                                    <a class="dropdown-item" href="#" runat="server" id="btnCerraSession"
                                        onserverclick="btnCerraSession_ServerClick"
                                        style="text-align: center; border: solid 3px lightgray; border-radius: 10px; padding: 8px;">Cerrar
                                        Sesion</a>
                                </li>
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
                                        <li class="nav-item"><a class="nav-link" href="MasivoDeuda.aspx">INMUEBLES</a>
                                        </li>
                                        <li class="nav-item"><a class="nav-link"
                                                href="MasivoDeudaAuto.aspx">AUTOMOTORES</a></li>
                                        <li class="nav-item"><a class="nav-link" href="MasivoDeudaIyC.aspx">INDUSTRIA Y
                                                COMERCIO</a></li>
                                        <li class="nav-item"><a class="nav-link active" href="#tab_3" data-toggle="tab"
                                                aria-expanded="false">GENERAL</a></li>
                                        <%--<li class="pull-right">
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success"
                                                Text="Exportar a Excel" OnClick="btnExportExcel_Click" />
                                            </li>--%>
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
                                                <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server"
                                                    Text="Volver" />
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
                                                <asp:Button ID="Button4" CssClass="btn btn-primary" runat="server"
                                                    Text="Volver" />
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
                                                            <div class="row">
                                                                <div class="col-md-12" style="text-align: left;">
                                                                    <a class="btn btn-outline-excel"
                                                                        onclick="abrirmodalConceptos();">
                                                                        <i class="fa fa-file-excel-o"></i> Cargar Excel
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">

                                                            <div class="col-md-4">
                                                                <label>Barrio</label>
                                                                <asp:ListBox ID="lstBarrios" Height="143"
                                                                    CssClass="form-control" runat="server"
                                                                    SelectionMode="Multiple"></asp:ListBox>
                                                            </div>
                                                            <br />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12" style="text-align: right;">
                                                            <button type="button" class="btn btn-outline"
                                                                id="btnFiltros" runat="server"
                                                                onserverclick="btnFiltros_ServerClick">
                                                                <span class="fa fa-filter"></span>&nbsp;Aplicar Filtros
                                                            </button>

                                                        </div>
                                                    </div>

                                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"
                                                        Font-Bold="true" Text=""></asp:Label>

                                                </div>
                                                <asp:HiddenField ID="MyHiddenControl" value="name" runat="server" />
                                                <asp:HiddenField ID="MyHiddenControl2" value="name" runat="server"
                                                    ValidateRequestMode="Disabled" />
                                                <div class="progress progress-striped active" id="divProgressBar"
                                                    style="display: none;">
                                                    <strong>Procesando consulta...</strong>
                                                    <div class="progress-bar" role="progressbar" aria-valuenow="45"
                                                        aria-valuemin="0" aria-valuemax="100" style="width: 45%">
                                                        <span class="sr-only">45% completado</span>
                                                    </div>
                                                </div>
                                                <div id="divResultados" runat="server" visible="false"
                                                    style="margin-top: 20px;">
                                                    <div class="row">
                                                        <div class="12" style="text-align: right">
                                                            <a class="btn btn-outline-danger"
                                                                onclick="abrirModalPlantillas();">
                                                                <i class="fa fa-file-excel-o"></i> PLANTILLA
                                                            </a>
                                                            <button type="button" class="btn btn-outline-danger"
                                                                id="btnClearFiltros" runat="server"
                                                                onserverclick="btnClearFiltros_ServerClick">
                                                                <span
                                                                    class="fa fa-filter-circle-xmark"></span>&nbsp;Limpiar
                                                                Filtros
                                                            </button>
                                                            <button type="button" id="btnNoti" class="btn btn-outline"
                                                                data-bs-toggle="modal" data-bs-target="#exampleModal">
                                                                <span class="fa fa-sheet-plastic"></span>&nbsp; Generar
                                                                notificación CIDI
                                                            </button>
                                                            <button type="button" runat="server" id="btnExportExcel"
                                                                onserverclick="btnExportExcel_ServerClick"
                                                                class="btn btn-outline-excel" data-toggle="modal"
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
                                                                OnRowDataBound="gvDeuda_RowDataBound"
                                                                AutoGenerateColumns="False" CellPadding="4"
                                                                ForeColor="#333333" GridLines="None">
                                                                <AlternatingRowStyle BackColor="White"
                                                                    ForeColor="#284775"></AlternatingRowStyle>
                                                                <Columns>
                                                                    <asp:BoundField DataField="nombre"
                                                                        ControlStyle-Width="10%" HeaderText="nombre">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cuit"
                                                                        ControlStyle-Width="10%" HeaderText="CUIT">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="nom_calle"
                                                                        ControlStyle-Width="10%" HeaderText="Calle">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="nro_dom"
                                                                        ControlStyle-Width="10%" HeaderText="Nro">
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="barrio"
                                                                        ControlStyle-Width="10%" HeaderText="Barrio">
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:GridView ID="gvConceptos" CssClass="table"
                                                                AutoGenerateColumns="false"
                                                                OnRowCommand="gvConceptos_RowCommand"
                                                                OnRowDeleting="gvConceptos_RowDeleting"
                                                                DataKeyNames="cuit" EmptyDataText="No hay resultados..."
                                                                runat="server" CellPadding="4" ForeColor="#333333"
                                                                GridLines="None">
                                                                <AlternatingRowStyle BackColor="White"
                                                                    ForeColor="#284775">
                                                                </AlternatingRowStyle>
                                                                <Columns>
                                                                    <asp:BoundField DataField="Cuit"
                                                                        ControlStyle-Width="10%" HeaderText="CUIT">
                                                                        <HeaderStyle HorizontalAlign="Center">
                                                                        </HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
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

            <div class="modal fade" id="modalConceptos">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Subir lista de Cuit</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label>Subir archivo</label>
                                <asp:FileUpload ID="fUploadConceptos" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button data-dismiss="modal" class="btn btn-default">Cancelar</button>
                            <asp:Button ID="btnConceptos_x_legajos" CssClass="btn btn-primary"
                                OnClientClick="this.disabled=true;this.value = 'Procesando...'"
                                UseSubmitBehavior="false" OnClick="btnConceptos_x_legajos_Click" runat="server"
                                Text="Aceptar" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade in" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
                aria-hidden="true">
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
                                <asp:TextBox ID="txtescripcion" TextMode="MultiLine" CssClass="form-control"
                                    runat="server"></asp:TextBox>
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


            <div class="modal fade" id="plantillaModal" tabindex="-1" aria-labelledby="exampleModalLabel"
                aria-hidden="true">
                <div class="modal-dialog1 modal-dialog">
                    <div class="modal-content1 modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Crear plantilla</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div id="editor-container"
                                    style="height: 200px; border: 1px solid #ccc; margin-left: 20px; margin-right: 20px;">
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button data-dismiss="modal" class="btn btn-warning">Cancelar</button>
                            <div class="d-flex flex-row gap-3 mt-3 pb-3">
                                <button type="button" class=" btn btn-primary "
                                    onclick="insertVariable('{nombre}')">Insertar Nombre</button>
                                <button type="button" class=" btn btn-primary "
                                    onclick="insertVariable('{apellido}')">Insertar Apellido</button>
                                <button type="button" class=" btn btn-primary "
                                    onclick="insertVariable('{cuit}')">Insertar CUIT</button>
                                <button type="button" class=" btn btn-primary " onclick="generarNotas()">GENERAR
                                    NOTAS</button>
                                <button type="button" class=" btn btn-primary " onclick="abrirModalNotas()"
                                    OnClick="btnCargarPlantillas_Click">Notas</button>
                            </div>
                            <!-- Campo oculto para almacenar el contenido -->
                            <asp:TextBox ID="hiddenInput2" runat="server" TextMode="MultiLine" Style="display: none;"
                                ValidateRequestMode="Disabled"></asp:TextBox>
                            <!-- Botón ASP.NET para procesar el contenido -->
                            <!-- Contenedor para mostrar las notas generadas -->
                            <asp:Literal ID="litNotasGeneradas" runat="server"></asp:Literal>
                        </div>
                    </div>

                    <div class="modal fade" id="plantillaModalNotas" tabindex="-1" aria-labelledby="exampleModalLabel"
                        aria-hidden="true">
                        <div class="modal-dialog1 modal-dialog">
                            <div class="modal-content1 modal-content">

                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Lista de planillas</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <asp:GridView ID="gvPlantilla" CssClass="table" runat="server"
                                            OnRowDataBound="gvPlantilla_RowDataBound"
                                            OnRowCommand="gvPlantilla_RowCommand" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None"
                                            DataKeyNames="id,contenido">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775">
                                            </AlternatingRowStyle>
                                            <Columns>
                                                <asp:BoundField DataField="nom_plantilla" ControlStyle-Width="10%"
                                                    HeaderText="Nombre Plantilla" SortExpression="nom_plantilla" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <button data-dismiss="modal" class="btn btn-warning">Cancelar</button>
                                </div>
                            </div>


                            <div class="modal fade" id="plantillaModalNombreNotas" aria-labelledby="exampleModalLabel"
                                aria-hidden="true">
                                <div class="modal-dialog1 modal-dialog">
                                    <div class="modal-content1 modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                            <h4 class="modal-title">Nombre de la nota</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group">
                                                <label for="txtNombreNota">Ingrese el nombre de la nota:</label>
                                                <asp:TextBox ID="txtNombreNota" runat="server" CssClass="form-control"
                                                    EnableViewState="true" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button data-dismiss="modal" onclick="volverAlModalPrincipal()"
                                                class="btn btn-warning">Cancelar</button>
                                            <asp:Button ID="btnGuardarNota" runat="server" CssClass="btn btn-primary"
                                                Text="Guardar" OnClick="btnGuardarNota_Click" UseSubmitBehavior="false"
                                                ClientIDMode="Static" OnClientClick=" DoCustomPost();" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <script src="../App_Themes/Main/js/jQuery-2.1.4.min.js"></script>
                    <script src="../App_Themes/Main/js/jquery-ui-1.10.3.min.js"></script>
                    <script src="../App_Themes/Main/js/bootstrap.min.js"></script>
                    <script src="../App_Themes/Main/js/bootstrap.bundle.min.js"></script>
                    <script src="../App_Themes/fontawesome/js/all.js"></script>
                    <!-- Agregar Quill y Bootstrap JS -->
                    <script src="https://cdn.quilljs.com/1.3.7/quill.min.js"></script>
                    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
                    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>


                    <script>

                        $("#btnClearFiltros").click(function () {
                            document.getElementById('lstBarrios').selectedIndex = -1;
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


                        function abrirmodalConceptos() {
                            $('#modalConceptos').modal('show');
                        }

                        function abrirModalPlantillas() {
                            $('#plantillaModal').modal('show');
                        }

                        function abrirModalNotas() {
                            $('#plantillaModalNotas').modal('show');
                        }


                        function abrirModalNombrePlantilla() {
                            var modal = $("#plantillaModalNombreNotas");

                            modal.appendTo("body"); // Asegura que esté en el body
                            modal.modal("show");

                            setTimeout(function () {
                                var maxZIndex = Math.max(...Array.from(document.querySelectorAll(".modal"))
                                    .map(m => parseFloat(window.getComputedStyle(m).zIndex) || 1050));

                                modal.css({
                                    "display": "block",
                                    "z-index": maxZIndex + 10, // Siempre encima del último modal abierto
                                    "opacity": "1"
                                });

                                $(".modal-backdrop").not(".modal-stack").css("z-index", maxZIndex + 9).addClass("modal-stack");
                            }, 100);
                        }


                        document.addEventListener("DOMContentLoaded", function () {
                            var rows = document.querySelectorAll("#<%= gvPlantilla.ClientID %> tr");
                            rows.forEach(function (row, index) {
                                if (index > 0) {
                                    row.classList.add("gvRow");
                                    row.addEventListener("click", function () {
                                        __doPostBack('<%= gvPlantilla.UniqueID %>', 'Select$' + (index - 1));
                                    });
                                }
                            });
                        });


                    </script>
                    <script>

                        let quill; // Definir la variable globalmente

                        // Inicializar Quill cuando el modal se muestra
                        $('#plantillaModal').on('shown.bs.modal', function () {
                            if (!quill) { // Solo inicializa si aún no se ha creado
                                quill = new Quill('#editor-container', {
                                    theme: 'snow',
                                    modules: {
                                        toolbar: [['bold', 'italic', 'underline'], ['clean']]
                                    }
                                });
                            }

                            var contenido = $('#hiddenInput2').val();
                            if (contenido) {
                                setQuillContent(contenido);
                            }
                        });

                        $('#plantillaModal').on('hidden.bs.modal', function () {
                            if (quill) {
                                quill.root.innerHTML = ''; // Limpiar el contenido de Quill
                            }

                            // Opcionalmente, también puedes limpiar el campo oculto
                            $('#hiddenInput2').val('');
                        });

                        // Función para insertar contenido en Quill
                        function setQuillContent(content) {
                            if (quill) {
                                quill.root.innerHTML = content; // Asigna el contenido al editor de Quill
                            }
                        }

                        // Función para insertar variables en el editor
                        function insertVariable(variable) {
                            const range = quill.getSelection();
                            if (range) {
                                quill.insertText(range.index, variable, {
                                    'bold': true,
                                    'background': '#f0f0f0'
                                });
                                quill.setSelection(range.index + variable.length);
                            }
                        }



                        function volverAlModalPrincipal() {
                            $('#plantillaModalNombreNotas').modal('hide');
                            $('.modal-backdrop').remove();

                            $('#plantillaModal').modal('show');
                        }

                        function DoCustomPost() {
                            var ModalTextBox = document.getElementById("<%= txtNombreNota.ClientID %>");
                            var HiddenTextBox = document.getElementById("<%= MyHiddenControl.ClientID %>");

                            if (ModalTextBox) {
                                HiddenTextBox.value = ModalTextBox.value;
                            }
                            return true;
                        }

                        function DoCustomPost2() {
                            {
                                console.log('Contenido Quill:', quill.root.innerHTML);

                                var contenidoQuill = quill.root.innerHTML;

                                var hiddenInput = document.getElementById("<%= hiddenInput2.ClientID %>");
                                var myHiddenControl2 = document.getElementById("<%= MyHiddenControl2.ClientID %>");
                                if (hiddenInput) {
                                    hiddenInput.value = contenidoQuill;

                                }

                                if (myHiddenControl2) {
                                    myHiddenControl2.value = contenidoQuill;

                                }
                            }

                        }

                        function generarNotas() {
                            DoCustomPost2();
                            abrirModalNombrePlantilla();
                        }

                    </script>
        </form>
    </body>

    </html>