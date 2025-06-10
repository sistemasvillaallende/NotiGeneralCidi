<%@ Page Title="Nueva Notificación" Language="C#" AutoEventWireup="true"
    CodeBehind="MasivoDeudaIyC.aspx.cs" EnableEventValidation="false"
    Inherits="NotificacionesCIDI.Secure.MasivoDeudaIyC" MasterPageFile="~/Master/MasterPage.master" ValidateRequest="false" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Industria y comercio</title>
    <style>
        .modal-confirm {
            color: #636363;
            width: 365px !important;
            margin: 150px auto 0;
        }

        .modal-plantillas {
            display: flex;
            align-items: center;
            min-height: calc(100% - 1rem);
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

        .label {
            margin-bottom: 5px;
        }

        .select2-selection {
            padding-bottom: 12px !important;
            border-color: #dee2e6 !important;
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
    <asp:ScriptManager ID="ScriptManager1" ClientIDMode="AutoId" runat="server"></asp:ScriptManager>
    <div class="wrapper" style="padding-right: 4%;">
        <div class="content-wrapper">
            <section class="content">
                <div class="box">
                    <div class="col-md-12">
                        <div id="divFiltros" runat="server">
                            <div class="row">
                                <div class="col-12" style="padding: 0px;">
                                    <h1 style="font-size: 36px !important; font-weight: 600 !important; margin-bottom: 5px !important; display: flex !important; align-items: start !important;">
                                        <span class="fa fa-car-side" style="color: #367fa9; border-right: solid 3px; padding-right: 10px;"></span>
                                        <span style="font-size: 20px !important; padding-left: 8px !important; margin-top: -5px !important;">Industria y Comercio</span> </h1>
                                    <h1 style="font-size: 16px !important; font-weight: 500 !important; color: gray !important; margin-left: 65px !important; margin-top: -24px !important;">Notificaciones - Nueva Notificación</h1>
                                    <hr style="margin-top: 5px; border: 2px solid #c09e76; margin-bottom: 20px; opacity: 1;">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <label class="label">Dado de Baja</label>
                                    <asp:DropDownList ID="Activo" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <label class="label">Zona</label>
                                    <asp:ListBox ID="lstZonas" Style="width: 100%; height: 243.8px;"
                                        CssClass="form-control" runat="server"
                                        SelectionMode="Multiple"></asp:ListBox>
                                </div>

                                <div class="col-md-3"  >
                                    <label class="label">Calles</label>
                                    <div class="input-group mb-2">
                                        <input type="text" id="txtSearchCalle" class="form-control" placeholder="Buscar calle..." />
                                    </div>
                                    <asp:ListBox ID="lstCalles" 
                                        CssClass="form-control" runat="server"
                                        Style="width: 100%; height: 200px;"
                                        SelectionMode="Multiple" onchange="checkSelection()"></asp:ListBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="label">Desde</label>
                                    <asp:TextBox
                                        ID="txtDesde"
                                        Enabled="false"
                                        Type="Number"
                                        CssClass="form-control"
                                        runat="server">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="label">Hasta</label>
                                    <asp:TextBox
                                        ID="txtHasta"
                                        Enabled="false"
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
                        </div>
                    </div>
                </div>

                <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Text="">
                </asp:Label>

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
                                 <i class="fa-solid fa-arrow-left"></i> Volver
                             </a>
                        </div>
                     </div>
                    <div class="row">
                        <div class="col-md-4">
                            <h2>Generar Dataset Notificación</h2>
                        </div>
                        <div class="col-md-8" style="text-align: right">
                            <a class="btn btn-outline-danger" onclick="abrirModalPlantillas();">
                                <i class="fa fa-list" aria-hidden="true"></i> Plantillas
                            </a>
                              <button id="btnGenerarNoti" type="button" class="btn btn-outline-primary" onclick="procesarYGenerarNotificaciones()"> 
                                  <span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación
                              </button>
                              <button type="button" id="btnExportExcel" class="btn btn-outline-secondary" onclick="exportarSeleccionadosDirecto()">
                                      <span class="fa fa-sheet-plastic"></span>&nbsp;Exportar a Excel  
                              </button>
                        </div>
                    </div>
                    <div style="margin-top: 20px;">
                          <table id="tablaIyC" class="display" style="width: 100%">
                              <thead>
                                  <tr>
                                      <th>
                                          <input type="checkbox" id="selectAll" class="filaCheckbox" data-id="AH019FU" /></th>
                                      <th>Denominación</th>
                                      <th>Nombre</th>
                                      <th>Apellido</th>
                                      <th>CUIT</th>
                                      <th>Cod_rubro</th>
                                      <th>Concepto</th>
                                      <th>Calle</th>
                                      <th>Nom_barrio</th>
                                      <th>Baja</th>
                                  </tr>
                              </thead>
                          </table>
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
    <div class="modal fade" id="modalNotif" tabindex="-1" aria-labelledby="ModalLabel" aria-hidden="true">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Se ha generado la notificacion.</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p style="text-align: center">
                        Se ha generado la notificacion.
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
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





function inicializarBuscador() {
            var $listBox = $("#<%= lstCalles.ClientID %>");
            var allOptions = [];

            $listBox.find("option").each(function () {
                allOptions.push({
                    value: $(this).val(),
                    text: $(this).text()
                });
            });

            $("#txtSearchCalle").off("input");

            $("#txtSearchCalle").on("input", function () {
                var searchText = $(this).val().toLowerCase();

                $listBox.empty();

                $.each(allOptions, function (i, option) {
                    if (searchText === '' || option.text.toLowerCase().indexOf(searchText) > -1) {
                        $listBox.append(new Option(option.text, option.value));
                    }
                });
            });
        }


        //// Modal de plantillas
        // Abrir modal de plantillas y mostrar el dataTable con las plantillas
        function abrirModalPlantillas() {
            $('#plantillaModalNotas').modal('show');

            if (!$.fn.DataTable.isDataTable('#tablaPlantillas')) {
                $.ajax({
                    url: 'MasivoDeudaIyC.aspx/ObtenerPlantillas',
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

        $('#btnFiltros').on('click', function () {

            let dado_baja = $('#<%= Activo.ClientID %>').val() === '1' ? true : false;
            console.log(dado_baja)

            let cod_zona = ($('#<%= lstZonas.ClientID %>').val() || [])[0] ?? null;
            console.log("zonas:", cod_zona);

            let cod_calle = parseInt($('#<%= lstCalles.ClientID %>').val());
            console.log(cod_calle)

            // Obtener valores de rango numérico
            let desde = parseInt($('#<%= txtDesde.ClientID %>').val()) || 0;
            let hasta = parseInt($('#<%= txtHasta.ClientID %>').val()) || 0;

            console.log(hasta)
            console.log(desde)

            $.ajax({
                type: "POST",
                url: "MasivoDeudaIyC.aspx/fillGrillaJSON",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    dado_baja: dado_baja,
                    cod_zona: cod_zona,
                    cod_calle: cod_calle,
                    desde: desde,
                    hasta: hasta
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

        /////////////////////////////

        $(document).ready(function () {

            $('#<%= lstCalles.ClientID %>').select2({
                placeholder: "Seleccione una o mas calles"
            });
            $('#<%= lstZonas.ClientID %>').select2({
                placeholder: "Seleccione una o mas zonas"
            });

        });


        function cargarDatos(datos) {

            let tabla = $('#tablaIyC').DataTable({
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
                    { data: 'legajo' },
                    { data: 'nombre' },
                    { data: 'apellido' },
                    { data: 'cuit' },
                    { data: 'cod_rubro' },
                    { data: 'concepto' },
                    { data: 'nom_calle' },
                    { data: 'nom_barrio' },
                    {
                        data: 'Baja',
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

        // Selecciona todos los checkboxs del dataset
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
        // fin de  // Selecciona todos los checkboxs del dataset
        // solamente se habilita desde y hasta con una sola calle 
        function checkSelection() {
            var listBox = document.getElementById('<%= lstCalles.ClientID %>');
            var txtDesde = document.getElementById('<%= txtDesde.ClientID %>');
            var txtHasta = document.getElementById('<%= txtHasta.ClientID %>');

            var selectedCount = 0;
            for (var i = 0; i < listBox.options.length; i++) {
                if (listBox.options[i].selected) {
                    selectedCount++;
                }
            }

            if (selectedCount === 1) {
                txtDesde.disabled = false; 
                txtHasta.disabled = false;
            } else {
                txtDesde.disabled = true;
                txtHasta.disabled = true;
                txtDesde.value = '';
                txtHasta.value = '';
            }
        }
        // fin de  // solamente se habilita desde y hasta con una sola calle

       function obtenerSeleccionados() {
            let seleccionados = [];

            // Verificar si tablaIyC existe y es una instancia válida de DataTable
            if (typeof tablaIyC !== 'undefined' && tablaIyC && $.fn.dataTable.isDataTable('#tablaIyC')) {
                try {
                    tablaIyC.rows().every(function () {
                        let $checkbox = $(this.node()).find('input.filaCheckbox');
                        if ($checkbox.is(':checked')) {
                            console.log("Comercio seleccionado");
                            let rowData = tablaIyC.row(this).data();
                            seleccionados.push({
                                legajo: rowData.legajo,
                                cuit: rowData.cuit,
                                nombre: rowData.nombre,
                                apellido: rowData.apellido,
                                nom_calle: rowData.nom_calle,
                                nom_barrio: rowData.nom_barrio,
                                concepto: rowData.concepto,
                                cod_rubro: rowData.cod_rubro
                            });
                        }
                    });
                } catch (error) {
                    console.log("Error al obtener seleccionados:", error);
                    // Método alternativo usando jQuery directamente
                    $('#tablaIyC tbody input.filaCheckbox:checked').each(function () {
                        let $checkbox = $(this);
                        let $row = $checkbox.closest('tr');
                        let legajo = $checkbox.data('id'); 

                        seleccionados.push({
                            legajo: $row.find('td:eq(1)').text(),
                            nombre: $row.find('td:eq(2)').text(), 
                            apellido: $row.find('td:eq(3)').text(),
                            cuit: $row.find('td:eq(4)').text(),
                            cod_rubro: $row.find('td:eq(5)').text(),
                            concepto: $row.find('td:eq(6)').text(),
                            nom_calle: $row.find('td:eq(7)').text(),
                            nom_barrio: $row.find('td:eq(8)').text()
                        });
                    });
                }
            } else {
                console.log("La tabla no está inicializada, usando método alternativo");
                $('#tablaIyC tbody input.filaCheckbox:checked').each(function () {
                    let $checkbox = $(this);
                    let $row = $checkbox.closest('tr');
                    let legajo = $checkbox.data('id'); 

                    seleccionados.push({
                        legajo: $row.find('td:eq(1)').text(),
                        nombre: $row.find('td:eq(2)').text(),
                        apellido: $row.find('td:eq(3)').text(),
                        cuit: $row.find('td:eq(4)').text(),
                        cod_rubro: $row.find('td:eq(5)').text(),
                        concepto: $row.find('td:eq(6)').text(),
                        nom_calle: $row.find('td:eq(7)').text(),
                        nom_barrio: $row.find('td:eq(8)').text()
                    });
                });
            }

            console.log("Comercios seleccionados:", seleccionados);
            return seleccionados;
        }

        function enviarSeleccionados() {
            let seleccionados = obtenerSeleccionados();

            console.log("Comercios para enviar antes de enviar:", seleccionados);

            if (seleccionados.length === 0) {
                $('#modalErrorTexto').text('Por favor seleccione al menos un comercio.');
                $('#modalError').modal('show');
                restaurarBotonGenerar();
                return;
            }

            $.ajax({
                type: "POST",
                url: "MasivoDeudaIyC.aspx/ProcesarSeleccionados", 
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    comerciosSeleccionados: seleccionados 
                }),
                success: function (response) {
                    console.log("Respuesta completa del servidor:", response);
                    console.log("response.d:", response.d);

                    if (response.d === "OK") {
                        console.log("Comercios procesados correctamente");
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
                url: "MasivoDeudaIyC.aspx/ContinuarGenerarNotificaciones", 
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
            console.log("aca entro");
            window.callbackDespuesDeSeleccionar = function () {
                generarNotificacionesDesdeJS();
            };
            console.log("aca salio");

            enviarSeleccionados();
        }

        function restaurarBotonGenerar() {
            var $btn = $('#btnGenerarNoti');
            $btn.prop('disabled', false)
                .removeClass('disabled')
                .html('<span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación');
        }





    </script>
</asp:Content>
