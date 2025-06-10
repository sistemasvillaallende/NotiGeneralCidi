<%@ Page Title="Nueva Notificación" Language="C#" AutoEventWireup="true" 
EnableEventValidation="false" MasterPageFile="~/Master/MasterPage.master" CodeBehind="MasivoDeuda.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.MasivoDeuda"  ValidateRequest="false" %>

    <asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
        <title>Masivo Deuda</title>
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
		text-align: center !important ;
		font-size: 26px !important;
		margin: 15px  !important;
        
	}

    .modal-dialog.modal-confirm {
        display: flex;
        align-items: center;
        min-height: calc(100% - 1rem);
    }
	.modal-confirm ..modal-content {
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
		text-align: center !important ;
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
        margin-bottom:12px !important;
        
    }
	.modal-confirm .btn:hover, .modal-confirm .btn:focus {
		background: #da2c12;
		outline: none;
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
    <div class="wrapper" style="padding-right: 4%;">
        <div class="content-wrapper">
            <section class="content">
                <div class="box">
                    <div class="col-md-12">
                        <div id="divFiltros" runat="server">
                            <div class="row" style="margin-top: 25px;">
                                <div class="col-md-12">
                                    <h3 style="color: #367fa9;">Inmueble - Nueva Notificación</h3>

                                    <div class="row">

                                        <div class="col-md-3">
                                            <label style="margin-bottom: 10px">Categoria Deuda</label>
                                            <br />
                                            <asp:ListBox ID="lstCatDeuda" Height="250" CssClass="form-control list-group"
                                                runat="server"></asp:ListBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label style="margin-bottom: 10px">Barrio</label>
                                            <div class="input-group mb-2">
                                                <input type="text" id="txtSearchBarrio" class="form-control" placeholder="Buscar barrio..." />
                                            </div>
                                            <asp:ListBox ID="lstBarrios" Height="203" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>

                                        <div class="col-md-1">
                                            <label style="margin-bottom: 10px">Zonas</label>
                                            <asp:ListBox ID="lstZonas" Height="250" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label style="margin-bottom: 10px">Calles</label>
                                            <div class="input-group mb-2">
                                                <input type="text" id="txtSearchCalle" class="form-control" placeholder="Buscar calle..." />
                                            </div>
                                            <asp:ListBox ID="lstCalles" Height="203px" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label style="margin-bottom: 10px">Desde</label>
                                                    <asp:TextBox
                                                        ID="txtDesde"
                                                        Enabled="false"
                                                        Type="number"
                                                        CssClass="form-control"
                                                        runat="server"
                                                        Style="width: 150px;">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label style="padding-bottom: 15px;">Hasta</label>
                                                    <asp:TextBox
                                                        ID="txtHasta"
                                                        Enabled="false"
                                                        Type="number"
                                                        CssClass="form-control"
                                                        runat="server"
                                                        Style="width: 150px;"> 
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2" style="text-align: right; padding-top: 23px;">
                                            <button type="button" class="btn btn-outline-primary" id="btnFiltros">
                                                <span class="fa fa-filter"></span>&nbsp;Aplicar Filtros
                                            </button>
                                        </div>
                                    </div>
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
                        <asp:HiddenField ID="MyHiddenControl" Value="name" runat="server" />
                        <asp:HiddenField ID="MyHiddenControl2" Value="name" runat="server" ValidateRequestMode="Disabled" />
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
                            <div class="row" style="margin-top: 20px; margin-right: 15px;">
                                <table id="tablaInmuebles" class="display" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <input type="checkbox" id="selectAll" class="filaCheckbox" data-id="AH019FU" /></th>
                                            <th>Denominacion</th>
                                            <th>Nombre</th>
                                            <th>Apellido</th>
                                            <th>CUIT</th>
                                            <th>Calle</th>
                                            <th>Barrio</th>
                                        </tr>
                                    </thead>
                                </table>
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
                    <div class="modal-body body-plantillas" style="margin-right: 20px; margin-left: 20px;">
                        <div class="form-group">
                            <table id="tablaPlantillas" class="table" style="width: 100%">
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
                        url: 'MasivoDeuda.aspx/ObtenerPlantillas',
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
                    url: "MasivoDeuda.aspx/GuardarPlantillaEnSesion",
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

            // Este es para manejar los evento de desde a hasta de las calles
                    $(document).ready(function () {
                            $('#<%= lstCalles.ClientID %>').on('change', function () {
                                var valoresSeleccionados = $(this).val();

                                if (valoresSeleccionados && valoresSeleccionados.includes("0")) {
                                    $('#<%= txtDesde.ClientID %>').prop('disabled', true); 
                                    $('#<%= txtHasta.ClientID %>').prop('disabled', true); 
                                } else {
                                    $('#<%= txtDesde.ClientID %>').prop('disabled', false); 
                                    $('#<%= txtHasta.ClientID %>').prop('disabled', false); 
                                }
                            });
                        });

                   
            //////////////////////////////////////////////// buscador de barrios /////////////////////////7
            $(document).ready(function () {
                $("#txtSearchBarrio").on("input", function () {
                    var searchText = $(this).val().toLowerCase();

                    // Show all options first
                    $("#<%= lstBarrios.ClientID %> option").show();

            // Hide options that don't match the search
            if (searchText.length > 0) {
                $("#<%= lstBarrios.ClientID %> option").each(function () {
                    var text = $(this).text().toLowerCase();
                    if (text.indexOf(searchText) === -1) {
                        $(this).hide();
                    }
                });
            }
        });
        });

            $(document).ready(function () {
                var $listBox = $("#<%= lstBarrios.ClientID %>");
        var allOptions = [];

        $listBox.find("option").each(function () {
            allOptions.push({
                value: $(this).val(),
                text: $(this).text()
            });
        });

        $("#txtSearchBarrio").on("input", function () {
            var searchText = $(this).val().toLowerCase();

            $listBox.empty();

            $.each(allOptions, function (i, option) {
                if (searchText === '' || option.text.toLowerCase().indexOf(searchText) > -1) {
                    $listBox.append(new Option(option.text, option.value));
                }
            });
        });
    });

            //////////// fin buscador de barrios /////////////////////////
            ////////// buscador de calles ////////


            $(document).ready(function () {
                var $listBox = $("#<%= lstCalles.ClientID %>");
        var allOptions = [];
        
        $listBox.find("option").each(function() {
            allOptions.push({
                value: $(this).val(),
                text: $(this).text()
            });
        });
        
        $("#txtSearchCalle").on("input", function() {
            var searchText = $(this).val().toLowerCase();
            
            $listBox.empty();
            
            $.each(allOptions, function(i, option) {
                if (searchText === '' || option.text.toLowerCase().indexOf(searchText) > -1) {
                    $listBox.append(new Option(option.text, option.value));
                }
            });
        });         
    });

    $(document).ready(function() {
    var $listBox = $("#<%= lstCalles.ClientID %>");
    var $txtDesde = $("#<%= txtDesde.ClientID %>");
        var $txtHasta = $("#<%= txtHasta.ClientID %>");

        $listBox.on("change", function () {
            var selectedValue = $listBox.val();
            if (selectedValue) {
                $txtDesde.prop("disabled", false);
                $txtHasta.prop("disabled", false);
            } else {
                $txtDesde.prop("disabled", true);
                $txtHasta.prop("disabled", true);
            }

            $listBox.val(selectedValue);
        });
    });
            /////////////////fin buscador de calles ////////////////7


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


            //// fill lista filtro
            $('#btnFiltros').on('click', function () {
                let desde = parseInt($('#<%= txtDesde.ClientID %>').val()) || 0;
                let hasta = parseInt($('#<%= txtHasta.ClientID %>').val()) || 0;
                console.log("numero desde:", desde);
                console.log("numero hasta:", hasta);

                let cod_categoria = parseInt($('#<%= lstCatDeuda.ClientID %>').val()) || 0;
                console.log("categoriasDeuda:", cod_categoria);

                let cod_barrio = parseInt($('#<%= lstBarrios.ClientID %>').val()) || 0;
                console.log("barrios:", cod_barrio);

                let cod_zona = ($('#<%= lstZonas.ClientID %>').val() || [])[0] ?? null;
                console.log("zonas:", cod_zona);

                let cod_calle = parseInt($('#<%= lstCalles.ClientID %>').val()) || 0;
                console.log("calles:", cod_calle);

                $.ajax({
                    type: "POST",
                    url: "MasivoDeuda.aspx/fillGrillaJSON",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        cod_categoria: cod_categoria,
                        cod_barrio: cod_barrio,
                        cod_calle: cod_calle,
                        cod_zona: cod_zona,
                        desde: desde,
                        hasta: hasta
                    }),
                    success: function (response) {
                        let datos = JSON.parse(response.d);
                        console.log("datos",datos);
                        cargarDatos(datos);
                        $('#' + '<%= divResultados.ClientID %>').show();
                         $('#' + '<%= divFiltros.ClientID %>').hide();
        },
        error: function (xhr, status, error) {
            console.error("Error:", xhr.responseText);
            alert("Error al cargar los datos de inmuebles. Por favor, intente nuevamente.");
        }
    });
            });

            function armoDenominacion(cir, sec, man, par, p_h) {
                try {
                    let denominacion = "";

                    // CIR
                    if (cir < 10)
                        denominacion += `CIR: 0${cir} - `;
                    else if (cir >= 10 && cir < 100)
                        denominacion += `CIR: ${cir} - `;

                    // SEC
                    if (sec < 10)
                        denominacion += `SEC: 0${sec} - `;
                    else if (sec >= 10 && sec < 100)
                        denominacion += `SEC: ${sec} - `;

                    // MAN
                    if (man < 10)
                        denominacion += `MAN: 00${man} - `;
                    else if (man >= 10 && man < 100)
                        denominacion += `MAN: 0${man} - `;
                    else if (man >= 100)
                        denominacion += `MAN: ${man} - `;

                    // PAR
                    if (par < 10)
                        denominacion += `PAR: 00${par} - `;
                    else if (par >= 10 && par < 100)
                        denominacion += `PAR: 0${par} - `;
                    else if (par >= 100)
                        denominacion += `PAR: ${par} - `;

                    // P_H (sin guión al final)
                    if (p_h < 10)
                        denominacion += `P_H: 00${p_h}`;
                    else if (p_h >= 10 && p_h < 100)
                        denominacion += `P_H: 0${p_h}`;
                    else if (p_h >= 100)
                        denominacion += `P_H: ${p_h}`;

                    return denominacion;
                } catch (ex) {
                    console.error("Error en armoDenominacion:", ex);
                    return "";
                }
            }

            // Tu función cargarDatos modificada
            function cargarDatos(datos) {
                let tabla = $('#tablaInmuebles').DataTable({
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
                                // Usar la denominación como ID único (cir-sec-man-par-p_h)
                                let id = `${row.cir}-${row.sec}-${row.man}-${row.par}-${row.p_h}`;
                                return `<input type="checkbox" class="filaCheckbox" data-id="${id}">`;
                            }
                        },
                        {
                            data: null, 
                            title: 'Denominación',
                            render: function (data, type, row) {
                                return armoDenominacion(row.cir, row.sec, row.man, row.par, row.p_h);
                            }
                        },
                        { data: 'nombre'},
                        { data: 'apellido'},
                        {data: 'cuit'},
                        { data: 'nom_calle'},
                        { data: 'nom_barrio'}
                    ],
                    drawCallback: function () {
                        // Volver a vincular el checkbox de "seleccionar todos"
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

            // Función para obtener los IDs seleccionados
            function obtenerSeleccionados() {
                let seleccionados = [];
                $('.filaCheckbox:checked').each(function () {
                    seleccionados.push($(this).data('id'));
                });
                return seleccionados;
            }

            // Función para limpiar filtros
            function limpiarFiltros() {
                $('#<%= txtDesde.ClientID %>').val('');
                $('#<%= txtHasta.ClientID %>').val('');
                $('#<%= lstCatDeuda.ClientID %>').val([]);
                $('#<%= lstBarrios.ClientID %>').val([]);
                $('#<%= lstZonas.ClientID %>').val([]);
                $('#<%= lstCalles.ClientID %>').val([]);
                $('#txtSearchBarrio').val('');
                $('#txtSearchCalle').val('');
            }


            function obtenerSeleccionados() {
                let seleccionados = [];

                // Verificar si tablaInmuebles existe y es una instancia válida de DataTable
                if (typeof tablaInmuebles !== 'undefined' && tablaInmuebles && $.fn.dataTable.isDataTable('#tablaInmuebles')) {
                    try {
                        tablaInmuebles.rows().every(function () {
                            let $checkbox = $(this.node()).find('input.filaCheckbox');
                            if ($checkbox.is(':checked')) {
                                console.log("Inmueble seleccionado");
                                let rowData = tablaInmuebles.row(this).data();
                                seleccionados.push({
                                    cir: rowData.cir,
                                    sec: rowData.sec,
                                    man: rowData.man,
                                    par: rowData.par,
                                    p_h: rowData.p_h,
                                    cuit: rowData.cuit,
                                    nombre: rowData.nombre,
                                    apellido: rowData.apellido,
                                    nom_calle: rowData.nom_calle,
                                    nom_barrio: rowData.nom_barrio,
                                });
                            }
                        });
                    } catch (error) {
                        console.log("Error al obtener seleccionados:", error);
                        // Método alternativo usando jQuery directamente
                        $('#tablaInmuebles tbody input.filaCheckbox:checked').each(function () {
                            let $checkbox = $(this);
                            let $row = $checkbox.closest('tr');
                            let id = $checkbox.data('id'); // Formato: "cir-sec-man-par-p_h"
                            let partes = id.split('-');

                            seleccionados.push({
                                cir: parseInt(partes[0]),
                                sec: parseInt(partes[1]),
                                man: parseInt(partes[2]),
                                par: parseInt(partes[3]),
                                p_h: parseInt(partes[4]),
                                nombre: $row.find('td:eq(2)').text(),
                                apellido: $row.find('td:eq(3)').text(),
                                cuit: $row.find('td:eq(4)').text(),
                                nom_calle: $row.find('td:eq(5)').text(),
                                nom_barrio: $row.find('td:eq(6)').text()
                            });
                        });
                    }
                } else {
                    console.log("La tabla no está inicializada, usando método alternativo");
                    // Método alternativo usando jQuery directamente
                    $('#tablaInmuebles tbody input.filaCheckbox:checked').each(function () {
                        let $checkbox = $(this);
                        let $row = $checkbox.closest('tr');
                        let id = $checkbox.data('id'); // Formato: "cir-sec-man-par-p_h"
                        let partes = id.split('-');

                        seleccionados.push({
                            cir: parseInt(partes[0]),
                            sec: parseInt(partes[1]),
                            man: parseInt(partes[2]),
                            par: parseInt(partes[3]),
                            p_h: parseInt(partes[4]),
                            nombre: $row.find('td:eq(2)').text(),
                            apellido: $row.find('td:eq(3)').text(),
                            cuit: $row.find('td:eq(4)').text(),
                            nom_calle: $row.find('td:eq(5)').text(),
                            nom_barrio: $row.find('td:eq(6)').text()
                        });
                    });
                }

                console.log("Inmuebles seleccionados:", seleccionados);
                return seleccionados;
            }

            function enviarSeleccionados() {
                let seleccionados = obtenerSeleccionados();

                console.log("Inmuebles para enviar antes de enviar:", seleccionados);

                if (seleccionados.length === 0) {
                    $('#modalErrorTexto').text('Por favor seleccione al menos un inmueble.');
                    $('#modalError').modal('show');
                    restaurarBotonGenerar();
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "MasivoDeuda.aspx/ProcesarSeleccionados", 
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        inmueblesSeleccionados: seleccionados 
                    }),
                    success: function (response) {
                        console.log("Respuesta completa del servidor:", response);
                        console.log("response.d:", response.d);

                        if (response.d === "OK") {
                            console.log("Inmuebles procesados correctamente");
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
                    url: "MasivoDeuda.aspx/ContinuarGenerarNotificaciones", // Ajustar URL según tu página
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


        </script>
    </asp:Content>