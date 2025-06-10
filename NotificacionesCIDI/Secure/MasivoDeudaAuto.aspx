<%@ Page Title="Nueva Notificación" Language="C#" AutoEventWireup="true" CodeBehind="MasivoDeudaAuto.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.MasivoDeudaAuto" MasterPageFile="~/Master/MasterPage.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
<%--            <script>
                var $j = jQuery.noConflict();
            </script>
        <script type="text/javascript">
            $j(window).on("load", function () {
                $j('#gvDeuda').DataTable({
                    "paging": false,
                    "searching": true,
                    "language": {
                        "url": "https://cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json"
                    }
                });
            });
        </script>--%>
    <title>Automotor</title>
    <style>
        .modal-confirm {
            color: #636363;
            width: 365px !important;
            margin: 150px auto 0;
        }

        .modal-plantillas {
            display: flex;
            align-items: center;
            min-height: calc(100% - 0.8rem);
        }

        .modal-title {
            text-align: center !important;
            font-size: 26px !important;
            margin: 15px !important;
        }

        .modal-dialog.modal-confirm {
            display: flex;
            align-items: center;
            min-height: calc(100% - 1rem);
        }

        .modal-confirm .modal-content {
            padding: 25px !important;
            border-radius: 5px;
            border: none;
        }

        .modal-confirm .modal-header {
            border-bottom: none;
            position: relative;
        }

        .modal-confirm .modal-title {
            font-family: 'Varela Round', sans-serif !important;
            text-align: center !important;
            font-size: 35px !important;
            margin: 30px 0 -15px !important;
        }

        .modal-confirm .form-control, .modal-confirm .btn {
            min-height: 40px !important;
            border-radius: 3px;
        }

        .modal-confirm .close {
            position: absolute;
            top: -5px;
            right: -5px;
        }

        .modal-confirm .modal-footer {
            border: none;
            text-align: center;
            border-radius: 5px;
            font-size: 13px;
        }

        .modal-confirm .icon-box {
            color: #fff;
            position: absolute;
            margin: 0 auto;
            left: 0;
            right: 0;
            top: -70px;
            width: 95px;
            height: 95px;
            border-radius: 50%;
            z-index: 9;
            background: #ef513a;
            padding: 15px;
            text-align: center;
            box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.1);
        }

            .modal-confirm .icon-box i {
                font-size: 56px;
                position: relative;
                top: 4px;
            }

        .modal-confirm .btn {
            color: #fff;
            border-radius: 4px;
            background: #ef513a;
            text-decoration: none;
            transition: all 0.4s;
            line-height: normal;
            border: none;
            width: 50% !important;
            margin: 0 auto !important;
            margin-bottom: 12px !important;
        }

            .modal-confirm .btn:hover, .modal-confirm .btn:focus {
                background: #da2c12;
                outline: none;
            }

            .body-plantillas{
                 min-height: 450px; 
                max-height: 500px; 
                overflow-y: auto; 
            }

    </style>

</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var pbControl = null;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            pbControl = args.get_postBackElement();
            pbControl.disabled = true;
            $("#divProgressBar").show("slow");
        }
        function EndRequestHandler(sender, args) {
            pbControl.disabled = false;
            pbControl = null;
            $("#divProgressBar").hide("slow");
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" ClientIDMode="AutoId" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="wrapper">
        <div class="content-wrapper">
            <section class="content">
                <div class="box">
                    <div class="col-md-12">
                        <div id="divFiltros" runat="server">
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-md-12">
                                    <h1 style="border-bottom: solid 3px lightgray; padding-bottom: 5px; margin-bottom: 25px;">Automotor - Nueva Notificación</h1>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <label>Excento</label>
                                    <asp:DropDownList ID="DDLExento" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 form-group">
                                    <label>Año Desde</label>
                                    <asp:TextBox
                                        ID="txtAnio"
                                        Enabled="true"
                                        Type="Number"
                                        CssClass="form-control"
                                        runat="server">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-3 form-group">
                                    <label>Hasta</label>
                                    <asp:TextBox
                                        ID="TextBox1"
                                        Type="Number"
                                        CssClass="form-control"
                                        runat="server"> 
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-3" style="text-align: right; padding-top: 23px;">
                                    <button type="button" class="btn btn-outline-primary" id="btnFiltros">
                                        <span class="fa fa-filter"></span>&nbsp;Aplicar Filtros
                                    </button>
                                </div>
                            </div>

                            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Text="">
                            </asp:Label>
                        </div>
                        <div class="progress progress-striped active" id="divProgressBar" style="display: none;">
                            <strong>Procesando consulta...</strong>
                            <div class="progress-bar" role="progressbar" aria-valuenow="45" aria-valuemin="0"
                                aria-valuemax="100" style="width: 45%">
                                <span class="sr-only">45% completado</span>
                            </div>
                        </div>
                        <div id="divResultados" runat="server" style="display: none; margin-top: 20px;">
                            <div class="row">
                                <div class="col-md-12 mb-4 d-flex justify-content-end">
                                    <a href="javascript:history.back()" class=" fs-6 text-decoration-none" style="color: #367fa9">
                                        <i class="fa-solid fa-arrow-left"></i>Volver
                                    </a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <h2>Generar Dataset Notificación</h2>
                                </div>
                                <div class="col-md-8" style="text-align: right">
                                    <a class="btn btn-outline-danger" onclick="abrirModalPlantillas();">
                                        <i class="fa fa-list" aria-hidden="true"></i>Plantillas
                                    </a>
                                    <button id="btnGenerarNoti" type="button" class="btn btn-outline-primary" onclick="procesarYGenerarNotificaciones()"> 
                                        <span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación
                                    </button>
                                    <button type="button" id="btnExportExcel" class="btn btn-outline-secondary" onclick="exportarSeleccionadosDirecto()">
                                            <span class="fa fa-sheet-plastic"></span>&nbsp;Exportar a Excel  
                                    </button>
                                </div>
                            </div>
                            <div style="margin-top: 20px">
                                <table id="tablaAutos" class="display" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                            <input type="checkbox" id="selectAll" class="filaCheckbox" data-id="AH019FU"  /></th>
                                            <th>Dominio</th>
                                            <th>CUIT</th>
                                            <th>Nombre</th>
                                            <th>Apellido</th>
                                            <th>Año</th>
                                            <th>Exento</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div> 
                    </div>
                </div>
            </section>
        </div>
    </div>
    <div class="modal fade" id="modalError" tabindex="-1" aria-labelledby="ModalErrorLabel" aria-hidden="true">
        <div class="modal-dialog modal-confirm">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="icon-box">
                        <i class="fa fa-exclamation-circle fa-4x" aria-hidden="true"></i>
                    </div>
                    <div style="width: 100%; text-align: center;">
                        <h4 class="modal-title">Error</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <p style="text-align: center">
                        <span id="modalErrorTexto"></span>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger btn-block" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="plantillaModalNotas" tabindex="-1" aria-labelledby="ModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-plantillas">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Lista de plantillas</h4>
                    <button type="button" class="btn-close small me-1" data-bs-dismiss="modal" aria-label="Close" style="transform: scale(0.8);"></button>
                </div>
                <div class="modal-body body-plantillas" style=" margin-right:20px ; margin-left:20px;">
                    <div class="form-group">
                        <table id="tablaPlantillas" class="table" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Nombre de Plantilla</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSeleccionar" class="btn btn-primary">Seleccionar</button>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.quilljs.com/1.3.7/quill.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/2.3.1/css/dataTables.dataTables.css" />
    <script src="https://cdn.datatables.net/2.3.1/js/dataTables.js"></script>

    <script>

// Abrir modal de plantillas y mostrar el dataTable con las plantillas
        function abrirModalPlantillas() {
            $('#plantillaModalNotas').modal('show');

            if (!$.fn.DataTable.isDataTable('#tablaPlantillas')) {
                $.ajax({
                    url: 'MasivoDeudaAuto.aspx/ObtenerPlantillas',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify({}),
                    success: function (response) {
                        let datos = JSON.parse(response.d);
                              
                        $('#tablaPlantillas').DataTable({
                            data: datos,
                            columns: [
                                {
                                    data: 'id',
                                    title: 'Seleccionar',
                                    orderable: false,
                                    render: function (data, type, row) {
                                        return `<input type="checkbox" name="plantilla" value="${data}" onclick="SoloUnCheckbox(this)">`;
                                    }
                                },
                                {
                                    data: 'nom_plantilla',
                                    title: 'Nombre de Plantilla'
                                }
                            ],
                            paging: true,
                            searching: true,
                            info: false,
                            language: {
                                emptyTable: "No hay plantillas disponibles"
                            }
                        });

                    },
                    error: function (xhr, status, error) {
                        console.error('Error al cargar plantillas:', status, error);
                        console.error('Respuesta completa:', xhr.responseText);

                        $('#tablaPlantillas tbody').html(
                            '<tr><td colspan="2" class="text-center text-danger">Error al cargar plantillas</td></tr>'
                        );
                    }
                });
            } else {

                $('#tablaPlantillas').DataTable().clear().destroy();
                abrirModalPlantillas(); 
            }
        }

        //  PERMITIR SELECCIONAR SOLO UN CHECKBOX DE LAS PLANTILLAS
        function SoloUnCheckbox(checkbox) {
            $('input[name="plantilla"]').not(checkbox).prop('checked', false);
        }

        let plantillaSeleccionada = null;
        // BOTON SELECCIONAR PLANTILLAS Y GUARDARLOS EN EL CODE BEHIND
        $('#btnSeleccionar').on('click', function (e) {
            e.preventDefault();

            let seleccionado = $('input[name="plantilla"]:checked').val();

            if (!seleccionado) {
                alert("Seleccioná una plantilla.");
                return false;
            }

            plantillaSeleccionada = parseInt(seleccionado);

            $.ajax({
                type: "POST",
                url: "MasivoDeudaAuto.aspx/GuardarPlantillaEnSesion",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ idPlantilla: parseInt(seleccionado) }),
                beforeSend: function () {
                    console.log("Enviando datos:", JSON.stringify({ idPlantilla: parseInt(seleccionado) }));
                },
                success: function (response) {
                    $('#plantillaModalNotas').modal('hide');
                    $('#divResultados').show();
                    $('#divFiltros').hide();
                },
                error: function (xhr, status, error) {
                    console.log("Status:", status);
                    console.log("Error:", error);
                    console.log("Response Text:", xhr.responseText);
                    console.log("Status Code:", xhr.status);
                    alert("Error al guardar plantilla en sesión: " + error);
                }
            });

            return false;
        });


        // PARA SELECCIONAR TODOS LOS ELEMENTOS DE LA TABLA 
        $(document).on('change', '#selectAll', function () {
            let isChecked = $(this).is(':checked');
            $('.filaCheckbox').prop('checked', isChecked);
        });


        ////// MANEJAMOS ACA LOS VALORES SELECCIONADOS 

        $('#btnFiltros').on('click', function () {
            let anioDesde = parseInt($('#<%= txtAnio.ClientID %>').val()) || 0;
            let anioHasta = parseInt($('#<%= TextBox1.ClientID %>').val()) || 0;
            let exentoValor = $('#<%= DDLExento.ClientID %>').val();
            let exento = exentoValor === "1";

            $.ajax({
                type: "POST",
                url: "MasivoDeudaAuto.aspx/fillGrillaJSON",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    anioDesde: anioDesde,
                    anioHasta: anioHasta,
                    exento: exento
                }),
                success: function (response) {
                    let datos = JSON.parse(response.d);
                    cargarDatos(datos); 

                    $('#' + '<%= divResultados.ClientID %>').show();
                    $('#' + '<%= divFiltros.ClientID %>').hide();

                },
                error: function (xhr, status, error) {
                    console.error("Error:", xhr.responseText);
                }
            });
        });


        function cargarDatos(datos) {

            let tabla = $('#tablaAutos').DataTable({
                destroy: true,
                data: datos,
                paging: false,
                columns: [
                    {
                        data: null,
                        orderable: false,
                        className: 'select-checkbox',
                        defaultContent: '',
                        render: function (data, type, row, meta) {
                            return `<input type="checkbox" class="filaCheckbox" data-id="${row.dominio}">`;
                        }
                    },
                    { data: 'dominio' },
                    { data: 'cuit' },
                    { data: 'nombre' },
                    { data: 'apellido' },
                    { data: 'anio' },
                    {
                        data: 'exento',
                        render: function (data) {
                            return data ? 'Sí' : 'No';
                        }
                    }
                ],
                drawCallback: function () {
                    // Esto vuelve a vincular el checkbox de "seleccionar todos"
                    $('#selectAll').off('change').on('change', function () {
                        let checked = this.checked;
                        tabla.rows().every(function () {
                            let $checkbox = $(this.node()).find('input.filaCheckbox');
                            $checkbox.prop('checked', checked);
                        });
                    });
                }
            });
        }

        ///


        function obtenerSeleccionados() {
            let seleccionados = [];

            // Verificar si tablaAutos existe y es una instancia válida de DataTable
            if (tablaAutos && $.fn.dataTable.isDataTable('#tablaAutos')) {
                try {
                    tablaAutos.rows().every(function () {
                        let $checkbox = $(this.node()).find('input.filaCheckbox');
                        if ($checkbox.is(':checked')) {
                            console.log("hola")
                            let rowData = tablaAutos.row(this).data();
                            seleccionados.push({
                                dominio: rowData.dominio,
                                cuit: rowData.cuit,
                                nombre: rowData.nombre,
                                apellido: rowData.apellido,
                                anio: rowData.anio,
                                exento: rowData.exento
                            });
                        }
                    });
                } catch (error) {
                    console.log("Error al obtener seleccionados:", error);
                    // Método alternativo usando jQuery directamente
                    $('#tablaAutos tbody input.filaCheckbox:checked').each(function () {
                        let $checkbox = $(this);
                        let $row = $checkbox.closest('tr');
                        seleccionados.push({
                            dominio: $row.find('td:eq(1)').text(),
                            cuit: $row.find('td:eq(2)').text(),
                            nombre: $row.find('td:eq(3)').text(),
                            apellido: $row.find('td:eq(4)').text(),
                            anio: parseInt($row.find('td:eq(5)').text()) || 0,
                            exento: $row.find('td:eq(6)').text() === 'Sí'
                        });
                    });
                }
            } else {
                console.log("La tabla no está inicializada, usando método alternativo");
                // Método alternativo usando jQuery directamente
                $('#tablaAutos tbody input.filaCheckbox:checked').each(function () {
                    let $checkbox = $(this);
                    let $row = $checkbox.closest('tr');
                    seleccionados.push({
                        dominio: $checkbox.data('dominio'),
                        cuit: $checkbox.data('cuit'),
                        // Obtener datos de las celdas de la fila
                        nombre: $row.find('td:eq(3)').text(),
                        apellido: $row.find('td:eq(4)').text(),
                        anio: parseInt($row.find('td:eq(5)').text()) || 0,
                        exento: $row.find('td:eq(6)').text() === 'Sí'
                    });
                });
            }

            console.log("Seleccionados:", seleccionados);
            return seleccionados;
        }


        function enviarSeleccionados() {
            let seleccionados = obtenerSeleccionados();

            console.log("aca tengo los seleccionados para enviar antes de enviar ", seleccionados);

            if (seleccionados.length === 0) {
                $('#modalErrorTexto').text('Por favor seleccione al menos un elemento.');
                $('#modalError').modal('show');

                restaurarBotonGenerar();
                return;
            }

            $.ajax({
                type: "POST",
                url: "MasivoDeudaAuto.aspx/ProcesarSeleccionados",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    autosSeleccionados: seleccionados
                }),
                success: function (response) {
                    console.log("Respuesta completa del servidor:", response);
                    console.log("response.d:", response.d);

                    if (response.d === "OK") {
                        console.log("Datos procesados correctamente");
                        // Si tiene callback, lo ejecuta
                        if (typeof window.callbackDespuesDeSeleccionar === 'function') {
                            window.callbackDespuesDeSeleccionar();
                        }
                    } else {
                        $('#modalErrorTexto').text('Error: ' + response.d);
                        $('#modalError').modal('show');
                        console.error("Error detallado:", response.d);
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error al enviar datos:", xhr.responseText);
                    $('#modalErrorTexto').text('Error al procesar la solicitud');
                    $('#modalError').modal('show');
                }
            });
        }

        function generarNotificacionesDesdeJS() {
            $.ajax({
                type: "POST",
                url: "MasivoDeudaAuto.aspx/ContinuarGenerarNotificaciones",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.startsWith('OK:')) {
                        var params = response.d.substring(3);
                        window.location.href = './DetNotificacionesGeneral.aspx' + params;
                    } else {
                        $('#modalErrorTexto').text(response.d);
                        $('#modalError').modal('show');
                    }
                },
                error: function (xhr, status, error) {
                    $('#modalErrorTexto').text('Error al generar notificaciones: ' + error);
                    $('#modalError').modal('show');
                }
            });
        }


        function procesarYGenerarNotificaciones() {
            if (!plantillaSeleccionada) {
                $('#modalErrorTexto').text('Debe seleccionar una plantilla.');
                $('#modalError').modal('show');
                return;
            }


            var $btn = $('#btnGenerarNoti');
            $btn.prop('disabled', true)
                .addClass('disabled')
                .html('<span class="spinner-border spinner-border-sm me-1"></span> Procesando...');


            // Definir callback para después de procesar seleccionados
            window.callbackDespuesDeSeleccionar = function () {
                generarNotificacionesDesdeJS();
            };

            // Enviar seleccionados (que ejecutará el callback si todo sale bien)
            enviarSeleccionados();
        }


        function restaurarBotonGenerar() {
            var $btn = $('#btnGenerarNoti');
            $btn.prop('disabled', false)
                .removeClass('disabled')
                .html('<span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación');
        }




        ///////////////////////////////////// IMPRIMIR EXCEL ////////////////////////////////////////777

        function exportarSeleccionadosDirecto() {
            var seleccionados = obtenerSeleccionados(); // Tu función que obtiene los seleccionados

            if (seleccionados.length === 0) {
                alert('Debe seleccionar al menos un registro para exportar.');
                return;
            }

            // Cambiar estado del botón
            var btnExport = document.getElementById('btnExportExcel');
            var textoOriginal = btnExport.innerHTML;
            btnExport.innerHTML = 'Exportando...';
            btnExport.disabled = true;

            // PASO 1: Guardar seleccionados
            $.ajax({
                type: "POST",
                url: "MasivoDeudaAuto.aspx/GuardarSeleccionadosParaExport",
                data: JSON.stringify({ autosSeleccionados: seleccionados }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === "OK") {
                        // PASO 2: Generar Excel
                        generarExcelPaso2(btnExport, textoOriginal);
                    } else {
                        alert('Error al guardar seleccionados: ' + response.d);
                        restaurarBoton(btnExport, textoOriginal);
                    }
                },
                error: function (xhr, status, error) {
                    alert('Error en paso 1: ' + error);
                    restaurarBoton(btnExport, textoOriginal);
                }
            });
        }

        function generarExcelPaso2(btnExport, textoOriginal) {
            $.ajax({
                type: "POST",
                url: "MasivoDeudaAuto.aspx/GenerarExcel",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === "OK") {
                        // PASO 3: Descargar archivo - redirigir para trigger descarga
                        window.location.href = window.location.pathname + '?action=downloadExcel';
                    } else {
                        alert('Error al generar Excel: ' + response.d);
                    }
                    restaurarBoton(btnExport, textoOriginal);
                },
                error: function (xhr, status, error) {
                    alert('Error en paso 2: ' + error);
                    restaurarBoton(btnExport, textoOriginal);
                }
            });
        }

        function restaurarBoton(btnExport, textoOriginal) {
            btnExport.innerHTML = textoOriginal;
            btnExport.disabled = false;
        }
    </script>
</asp:Content>
