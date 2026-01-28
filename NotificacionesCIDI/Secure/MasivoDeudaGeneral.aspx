<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasivoDeudaGeneral.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.MasivoDeudaGeneral" MasterPageFile="~/Master/MasterPage.master"
    title="Notificador General" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <style>
    
	.modal-confirm {		
		color: #636363;
		width: 365px !important;
		margin: 330px auto 0;
	}

    .modal-plantillas {
        display: flex;
        align-items: center;
        min-height: calc(100% - 0.5rem);
            }

    .modal-upload{
         display: flex;
         align-items: center;
         min-height: calc(100% - 0.5rem);
    }

    .modal-title {
		text-align: left !important ;
		font-size: 26px !important;
		margin: 10px 15px 10px 0px !important;
        
	}
    .modal-dialog .modal-confirm {
        display: flex;
        align-items: center;
        justify-content: center;
        min-height: 100vh; 
        margin: 0 auto; 
        pointer-events: none; 
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

        .table-example {
        max-width: 300px;
        margin: 0 auto;
        font-size: 0.875rem !important;
    }
    
    .table-example th {
        background-color: #495057 !important;
        color: white;
        font-weight: bold;
        text-align: center;
    }
    
    .table-example td {
        text-align: center;
        font-family: 'Courier New', monospace;
        font-weight: 500;
    }
    
    .modal-dialog-centered {
        display: flex;
        align-items: center;
        min-height: calc(100% - 1rem);
    }
    
    .btn-lg {
        padding: 0.75rem 1.5rem;
        font-size: 1.1rem;
    }
    
    .table-excel-wrapper {
        display: table;
        margin: 0 auto; 
        margin-top: 1rem;
    }

    .table-excel {
        border-collapse: collapse;
        font-family: Calibri, sans-serif;
        font-size: 14px;
    }

    .table-excel th,
    .table-excel td {
        border: 1px solid #b6b6b6;
        padding: 6px 12px;
        min-width: 140px;
        max-width: 140px;
        text-align: left;
        background-color: #fff;
    }

    .table-excel th {
        background-color: #f9f9f9;
        color: #000;
        font-weight: bold;
        text-align: center;
    }

    .table-excel td.selected {
        border: 2px solid #00b050;
        box-shadow: inset 0 0 0 1px white;
    }

</style>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" ClientIDMode="AutoId" />
        <div class="wrapper" style="padding-right:4%;">
            <div class="content-wrapper">
                <section class="content">
                    <div class="box">
                        <div class="col-md-12">
                            <div id="divFiltros" runat="server">
                                <div class="row" style="margin-top: 25px;">
                                    <div class="col-md-12">
                                        <h1>Filtros</h1>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: left;">
                                            <a class="btn btn-primary" onclick="abrirmodalConceptos();">
                                                <i class="fa fa-upload" aria-hidden="true"></i> Cargar Excel
                                            </a>
                                            <span class="text-muted ml-3">Nota: Descargar el modelo en Excel para cargar el excel.</span>
                                        </div>                                     
                                        <div style="width:200px">
                                        <asp:Label ID="lblUploadStatus" runat="server" CssClass="alert alert-success d-block mt-3"  Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>                                
                            
                            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Text="">
                            </asp:Label>
                            </div>
                        </div>
                        <asp:HiddenField ID="MyHiddenControl" value="name" runat="server" />
                        <asp:HiddenField ID="MyHiddenControl2" value="name" runat="server"
                            ValidateRequestMode="Disabled" />
                        <div class="progress progress-striped active" id="divProgressBar" style="display: none;">
                            <strong>Procesando consulta...</strong>
                            <div class="progress-bar" role="progressbar" aria-valuenow="45" aria-valuemin="0"
                                aria-valuemax="100" style="width: 45%">
                                <span class="sr-only">45% completado</span>
                            </div>
                        </div>
                        <div id="divResultados"  style="display: none; margin-top: 20px;">
                             <div class="row">                             
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <h2>Generar Dataset Notificación</h2>
                                </div>
                                <div class="col-md-8" style="text-align: right">
                                    <a class="btn btn-outline-danger" onclick="abrirModalPlantillas();">
                                        <i class="fa fa-list" aria-hidden="true"></i> Plantillas
                                    </a>
                                    <button  id="btnGenerarNoti"
                                        onclick="generarNotificacion();"
                                        type="button" class="btn btn-outline-primary" >
                                        <span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación 
                                    </button>
                                    <button type="button" runat="server" id="btnExportExcel"
                                        onserverclick="btnExportExcel_ServerClick" class="btn btn-outline-success"
                                        data-toggle="modal" data-target="#page-change-name">
                                        <span class="fa fa-sheet-plastic"></span>&nbsp;Exportar a Excel                                           
                                    </button>                                                                      
                                </div>
                            </div>
                            <div class="row" style="margin-top: 20px; margin-right:15px;">
                            <div class="col-md-12" style="margin-left: 15px;">
                                <table id="tableDatosExcel" class="table table-striped table-bordered" style="width:100%">
                                    <thead>
                                        <tr>
                                            <!-- Las columnas se generarán dinámicamente -->
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <!-- Los datos se cargarán dinámicamente -->
                                    </tbody>
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
                          <button type="button" class="btn btn-danger btn-block" data-dismiss="modal">Cerrar</button>
                      </div>
                  </div>
              </div>
          </div>

        <div class="modal fade" id="modalImportExcel">
            <div class="modal-dialog modal-upload">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Importar Excel a notificar</h4>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label class="pt-2 pb-3">Subir archivo</label>
                            <input type="file" id="fileExcel" class="form-control" accept=".xlsx,.xls" />
                            <span id="lblFileError" class="text-danger" style="display: none;">*Archivo no ha sido seleccionado</span>
                        </div>
                        <div id="divProgress" style="display: none;" class="mt-3">
                            <div class="progress">
                                <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%">
                                    Procesando...
                       
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                        <button type="button" id="btnImportExcel" class="btn btn-primary" onclick="importarExcel()">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="plantillaModalNotas" tabindex="-1" aria-labelledby="plantillaModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header d-flex justify-content-between align-items-start">
                        <div>
                            <h4 class="modal-title" >Nueva Plantilla</h4>
                            <h8 class="text-muted mb-0">Revisá que los datos estén correctos antes de continuar.</h8>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="transform: scale(0.7);"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="form-label">Nombre de la plantilla</label>
                                <input type="text" id="txtNombrePlantilla" class="form-control" placeholder="Ingrese un nombre para la plantilla">
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="form-label">Variables disponibles</label>
                                <select id="selectVariables" class="form-select" onchange="insertarVariableAutomatica()">
                                    <option value="">Seleccione una variable para insertar</option>
                                    <!-- Se cargarán dinámicamente las columnas del Excel -->
                                </select>
                                <small class="text-muted">Seleccione una variable para insertarla automáticamente en el cursor</small>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Contenido de la plantilla</label>
                            <div id="editor-container" style="height: 40vh; width: 100%;"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="d-flex flex-row gap-3">
                            <button type="button" class="btn btn-primary btn-sm" onclick="guardarPlantillaEnServidor()">
                                <i class="fa fa-save"></i>Guardar Plantilla
       
                            </button>
                            <button type="button" class="btn btn-outline-danger btn-sm" data-bs-dismiss="modal">
                                <i class="fa fa-times"></i>Cancelar
       
                            </button>
                        </div>
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

            
            ///////////////////////


            function mostrarModalConCuit() {
                var modalEjemplo = new bootstrap.Modal(document.getElementById('modalEjemplo'));
                modalEjemplo.show();
            }

            // Función para ver ejemplo desde el modal de subida
            function verEjemplo() {
                var modalConceptos = bootstrap.Modal.getInstance(document.getElementById('modalImportExcel'));
                modalConceptos.hide();

                setTimeout(function () {
                    var modalEjemplo = new bootstrap.Modal(document.getElementById('modalEjemplo'));
                    modalEjemplo.show();
                }, 300);
            }

            // Función para ocultar mensaje de error
            function hideErrorMessage() {
                var lblError = document.getElementById('lblFileError');
        if (lblError) {
            lblError.style.display = 'none';
        }
    }


            function abrirmodalConceptos() {
                $('#modalImportExcel').modal('show');
            }
                  
                    // Funcion para poder seelccionar el check cuando aprieto cualquier lugar del gridview
                    function SeleccionarFila(fila, checkBoxId) {
                        var chk = document.getElementById(checkBoxId);
                
                        if (chk !== null) {
                            chk.checked = !chk.checked;
                            SoloUnCheckbox(chk);
                        }
                    }


// para agregar spinner en generar notificaciones
            if (window.jQuery) {
                $(document).ready(function() {
                    $('btnGenerarNoti').on('click', function(e) {
                        var $btn = $(this);
                        
                        if ($btn.prop('disabled')) {
                            e.preventDefault();
                            return false;
                        }
                        
                        $btn.prop('disabled', true)
                            .addClass('disabled')
                            .html('<span class="spinner-border spinner-border-sm mr-1"></span> Procesando...');
                        
                        setTimeout(function() {
                            $btn.prop('disabled', false)
                                .removeClass('disabled')
                                .html('<span class="fa fa-sheet-plastic"></span> Generar notificación');
                        }, 30000);
                        
                        return true;
                    });
                });
            } else {
                console.error('jQuery is not loaded');
            }


            function hideErrorMessage() {
                // Hide the error message when a file is selected
                document.getElementById('lblFileError').style.display = 'none';
            }

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


            function abrirModalPlantillas() {
                $('#plantillaModalNotas').modal('show');

                if (!$.fn.DataTable.isDataTable('#tablaPlantillas')) {
                    $.ajax({
                        url: 'MasivoDeudaGeneral.aspx/ObtenerPlantillas',
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

            //let plantillaSeleccionada = null;
            //// BOTON SELECCIONAR PLANTILLAS Y GUARDARLOS EN EL CODE BEHIND
            //$('#btnSeleccionar').on('click', function (e) {
            //    e.preventDefault();

            //    let seleccionado = $('input[name="plantilla"]:checked').val();

            //    if (!seleccionado) {
            //        alert("Seleccioná una plantilla.");
            //        return false;
            //    }

            //    plantillaSeleccionada = parseInt(seleccionado);

            //    $.ajax({
            //        type: "POST",
            //        url: "MasivoDeudaGeneral.aspx/GuardarPlantillaEnSesion",
            //        contentType: "application/json; charset=utf-8",
            //        dataType: "json",
            //        data: JSON.stringify({ idPlantilla: parseInt(seleccionado) }),
            //        beforeSend: function () {
            //            console.log("Enviando datos:", JSON.stringify({ idPlantilla: parseInt(seleccionado) }));
            //        },
            //        success: function (response) {
            //            $('#plantillaModalNotas').modal('hide');
            //            $('#divResultados').show();
            //            $('#divFiltros').hide();
            //        },
            //        error: function (xhr, status, error) {
            //            console.log("Status:", status);
            //            console.log("Error:", error);
            //            console.log("Response Text:", xhr.responseText);
            //            console.log("Status Code:", xhr.status);
            //            alert("Error al guardar plantilla en sesión: " + error);
            //        }
            //    });

            //    return false;
            //});

            ////////// Excel

            let tablaDatos = null; // Variable global para el DataTable
            let datosExcelActuales = null; // Guardar los datos actuales


            function importarExcel() {
                const fileInput = document.getElementById('fileExcel');
                const file = fileInput.files[0];
                const lblError = document.getElementById('lblFileError');
                const btnImport = document.getElementById('btnImportExcel');
                const divProgress = document.getElementById('divProgress');

                // Validar que se haya seleccionado un archivo
                if (!file) {
                    lblError.style.display = 'block';
                    return;
                }

                lblError.style.display = 'none';

                // Validar extensión
                const extension = file.name.split('.').pop().toLowerCase();
                if (extension !== 'xlsx' && extension !== 'xls') {
                    alert('Por favor seleccione un archivo Excel válido (.xlsx o .xls)');
                    return;
                }

                // Deshabilitar botón y mostrar progreso
                btnImport.disabled = true;
                btnImport.textContent = 'Procesando...';
                divProgress.style.display = 'block';

                // Crear FormData para enviar el archivo
                const formData = new FormData();
                formData.append('file', file);

                // Obtener URL base del web.config
                const urlBase = '<%= ConfigurationManager.AppSettings["urlBase"] %>';

                // Enviar archivo al endpoint
                fetch(urlBase + 'api/excel/parse', {
                    method: 'POST',
                    body: formData
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Error al procesar el archivo');
                        }
                        return response.json();
                    })
                    .then(data => {
                        console.log('Datos del Excel:', data);

                        // Aquí procesas los datos recibidos
                        procesarDatosExcel(data);

                        // Cerrar modal
                        $('#modalImportExcel').modal('hide');

                        // Limpiar input
                        fileInput.value = '';

                       // alert('Archivo procesado correctamente. ' + data.rowCount + ' filas encontradas.');
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        alert('Error al procesar el archivo: ' + error.message);
                    })
                    .finally(() => {
                        // Restaurar botón y ocultar progreso
                        btnImport.disabled = false;
                        btnImport.textContent = 'Aceptar';
                        divProgress.style.display = 'none';
                    });
            }

            function procesarDatosExcel(data) {
                console.log('Procesando datos:', data);

                // Destruir DataTable anterior si existe
                if (tablaDatos !== null) {
                    tablaDatos.destroy();
                    $('#tableDatosExcel').empty();
                }

                // Validar que haya datos
                if (!data.rows || data.rows.length === 0) {
                    alert('El archivo no contiene datos');
                    return;
                }

                // Construir las columnas dinámicamente
                let columns = [
                    {
                        title: '<input type="checkbox" id="chkAll" onclick="seleccionarTodos(this)" />',
                        data: null,
                        orderable: false,
                        width: '50px',
                        className: 'text-center',
                        render: function (data, type, row, meta) {
                            return `<input type="checkbox" class="chkRow" data-index="${meta.row}" />`;
                        }
                    }
                ];

                // Agregar las columnas del Excel
                data.columns.forEach(col => {
                    columns.push({
                        title: col,
                        data: col,
                        defaultContent: ''
                    });
                });

                // Inicializar DataTable
                tablaDatos = $('#tableDatosExcel').DataTable({
                    data: data.rows,
                    columns: columns,
                    paging: true,
                    pageLength: 25,
                    searching: true,
                    ordering: true,
                    info: true,
                    responsive: true,
                    language: {
                        emptyTable: "No hay datos disponibles",
                        search: "Buscar:",
                        lengthMenu: "Mostrar _MENU_ registros",
                        info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
                        paginate: {
                            first: "Primero",
                            last: "Último",
                            next: "Siguiente",
                            previous: "Anterior"
                        }
                    }
                });

                console.log('DataTable creado con', data.rowCount, 'filas');
                mostrarResultados();
                cargarVariablesDisponibles(data);
            }
 
            function mostrarResultados() {
                document.getElementById('divResultados').style.display = 'block';
            }

            /// FALTA PLANTILLA Y FALTA NOTIFICAR

            // Función para seleccionar/deseleccionar todos
            function seleccionarTodos(checkbox) {
                $('.chkRow').prop('checked', checkbox.checked);
            }

            // Función para obtener filas seleccionadas
            function obtenerFilasSeleccionadas() {
                let seleccionadas = [];

                $('.chkRow:checked').each(function () {
                    let index = $(this).data('index');
                    let rowData = tablaDatos.row(index).data();
                    seleccionadas.push(rowData);
                });

                return seleccionadas;
            }

            // Función para generar notificación
            function generarNotificacion() {
                let seleccionadas = obtenerFilasSeleccionadas();

                if (seleccionadas.length === 0) {
                    alert('Debe seleccionar al menos un registro');
                    return;
                }

                console.log('Generando notificación para:', seleccionadas);

                // Aquí puedes llamar a tu endpoint para generar la notificación
                // Por ejemplo:
                /*
                $.ajax({
                    url: 'TuPagina.aspx/GenerarNotificacion',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify({ datos: seleccionadas }),
                    success: function(response) {
                        alert('Notificación generada correctamente');
                    },
                    error: function(xhr, status, error) {
                        alert('Error al generar notificación');
                    }
                });
                */
            }



            /////////////////////// Insertar variables dinamicas en editor


            ////////////////////// Configuracion Plantillas

            var quill;
            var quillEditor = null;

            $(document).ready(function () {
                // Inicializar Quill
                quill = new Quill('#editor-container', {
                    theme: 'snow',
                    modules: {

                        toolbar: [
                            [{ 'header': [1, 2, 3, false] }],
                            [{ 'size': ['small', false, 'large', 'huge'] }],
                            ['bold', 'italic', 'underline'],
                            ['link'],
                            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                            ['clean']
                        ]
                    }
                });


                // Limpiar Quill cuando se cierra el modal
                $('#plantillaModalNotas').on('hidden.bs.modal', function () {
                    if (quill) {
                        quill.root.innerHTML = '';
                    }
                    $('#hiddenInput2').val('');
                });

            });

            function setQuillContent(content) {
                if (quillEditor) {
                    quillEditor.root.innerHTML = content;
                }
            }


            let columnasDisponibles = []; // Columnas del Excel cargado

            function cargarVariablesDisponibles(data) {
                const select = document.getElementById('selectVariables');
                select.innerHTML = '<option value="">Seleccione una variable para insertar</option>';

                if (!data.columns) {
                    alert('Primero debe cargar un archivo Excel');
                    return;
                }

                columnasDisponibles = data.columns;
                console.log("columnas", columnasDisponibles);

                columnasDisponibles.forEach(columna => {
                    const option = document.createElement('option');
                    option.value = `{${columna}}`;
                    option.textContent = columna;
                    select.appendChild(option);
                });
            }
            function insertarVariableAutomatica() {
                const select = document.getElementById('selectVariables');
                const variable = select.value;

                // Si no hay variable seleccionada, no hacer nada
                if (!variable) {
                    return;
                }

                // Insertar la variable en el editor
                insertVariable(variable);

                // Resetear el select a la opción por defecto
                select.value = '';

                // Mantener el foco en el editor
                quill.focus();
            }

            // Insertar variable en el cursor del editor
            function insertVariable(variable) {
                if (!quill) {
                    alert('Editor no inicializado');
                    return;
                }

                // Obtener la posición actual del cursor
                const range = quill.getSelection(true);

                // Insertar la variable en la posición del cursor
                quill.insertText(range.index, variable + ' ');

                // Mover el cursor después de la variable insertada
                quill.setSelection(range.index + variable.length + 1);
            }


            ///////////////////////////////////// Guardar Plantilla y seleccionarla

            function guardarPlantillaEnServidor(plantilla) {
                const urlBase = '<%= ConfigurationManager.AppSettings["urlBase"] %>';

                // Mostrar loading
                const btnGuardar = event.target;
                const textoOriginal = btnGuardar.innerHTML;
                btnGuardar.disabled = true;
                btnGuardar.innerHTML = '<i class="fa fa-spinner fa-spin"></i> Guardando...';

                fetch(urlBase + 'Plantillas/insertNuevaPlantilla', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(plantilla)
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Error al guardar la plantilla');
                        }
                        return response.json();
                    })
                    .then(data => {
                        console.log('Plantilla guardada:', data);
                        alert('Plantilla guardada correctamente');

                        // Cerrar modal
                        $('#plantillaModal').modal('hide');

                        // Limpiar campos
                        document.getElementById('txtNombrePlantilla').value = '';
                        quillEditor.setText('');
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        alert('Error al guardar la plantilla: ' + error.message);
                    })
                    .finally(() => {
                        // Restaurar botón
                        btnGuardar.disabled = false;
                        btnGuardar.innerHTML = textoOriginal;
                    });
            }



            //////////////////////////////////////////////// GUARDAR PLANTILLA ////////////////

            let plantillaSeleccionada = null;

            function guardarPlantillaEnServidor() {
                // Obtener valores del HTML
                const nombrePlantilla = document.getElementById('txtNombrePlantilla').value.trim();
                const contenido = quill.root.innerHTML;
                const contenidoTexto = quill.getText();

                // Validaciones
                if (!nombrePlantilla) {
                    alert('Debe ingresar un nombre para la plantilla');
                    document.getElementById('txtNombrePlantilla').focus();
                    return;
                }

                console.log("nombre Plantilla",nombrePlantilla);

                if (!contenidoTexto.trim()) {
                    alert('El contenido de la plantilla no puede estar vacío');
                    quill.focus();
                    return;
                }

                const urlBase = '<%= ConfigurationManager.AppSettings["urlBase"] %>';

                const btnGuardar = event.target;
                const textoOriginal = btnGuardar.innerHTML;
                btnGuardar.disabled = true;
                btnGuardar.innerHTML = '<i class="fa fa-spinner fa-spin"></i> Guardando...';

                let nuevoId = null;

                // Paso 1: Obtener el ID máximo
                fetch(urlBase + 'Plantillas/getMaxId', {
                    method: 'GET'
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Error al obtener el ID máximo');
                        }
                        return response.json();
                    })
                    .then(idMax => {
                        console.log('ID Máximo obtenido:', idMax);
                        nuevoId = idMax + 1;

                        // Preparar objeto para insertar según tu estructura
                        const plantillaParaInsertar = {
                            id: nuevoId,
                            fecha_alta: new Date().toISOString(),
                            id_subsistema: 8,
                            nom_plantilla: nombrePlantilla,
                            id_secretaria: 0,
                            id_oficina: 0,
                            contenido: contenido
                        };

                        console.log('Insertando plantilla:', plantillaParaInsertar);

                        // Paso 2: Insertar la nueva plantilla
                        return fetch(urlBase + 'Plantillas/insertNuevaPlantilla', {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify(plantillaParaInsertar)
                        });
                    })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Error al insertar la plantilla');
                        }
                        return;
                    })
                    .then(resultado => {
 
                        // Paso 3: Guardar el ID en sesión mediante WebMethod
                        return guardarPlantillaEnSesionWebMethod(nuevoId);
                    })
                    .then(resultadoSesion => {
                        console.log('Plantilla guardada en sesión:', resultadoSesion);

                        alert('Plantilla guardada y seleccionada correctamente');

                        // Cerrar modal
                        $('#plantillaModalNotas').modal('hide');

                        // Limpiar campos
                        document.getElementById('txtNombrePlantilla').value = '';
                        quill.setText('');

                        // Mostrar confirmación visual
                        //mostrarPlantillaSeleccionada(nombrePlantilla, nuevoId);
                    })
                    .catch(error => {
                        console.error('Error completo:', error);
                        alert('Error al guardar la plantilla: ' + error.message);
                    })
                    .finally(() => {
                        btnGuardar.disabled = false;
                        btnGuardar.innerHTML = textoOriginal;
                    });
            }

            // Guardar plantilla en sesión usando WebMethod
            function guardarPlantillaEnSesionWebMethod(idPlantilla) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        type: "POST",
                        url: "MasivoDeudaGeneral.aspx/GuardarPlantillaEnSesion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({
                            idPlantilla: parseInt(idPlantilla)                        }),
                        beforeSend: function () {
                            console.log("Guardando en sesión:", { idPlantilla });
                        },
                        success: function (response) {
                            if (response.d === "OK") {
                                plantillaSeleccionada = parseInt(idPlantilla);
                                resolve(response.d);
                            } else {
                                reject(new Error(response.d));
                            }
                        },
                        error: function (xhr, status, error) {
                            console.error("Error al guardar en sesión:", error);
                            reject(new Error("Error al guardar plantilla en sesión: " + error));
                        }
                    });
                });
            }


            //// Para mandarle al web method 


            function generarNotificacion() {
                let seleccionadas = obtenerFilasSeleccionadas();

                if (seleccionadas.length === 0) {
                    alert('Debe seleccionar al menos un registro');
                    return;
                }

                if (!plantillaSeleccionada) {
                    alert('Debe crear y seleccionar una plantilla primero');
                    abrirModalPlantilla();
                    return;
                }

                console.log('Generando notificación para:', seleccionadas.length, 'registros');
                console.log('Registros seleccionados:', seleccionadas);

                // Mostrar loading en el botón
                const btnGenerar = document.getElementById('btnGenerarNoti');
                const textoOriginal = btnGenerar.innerHTML;
                btnGenerar.disabled = true;
                btnGenerar.innerHTML = '<i class="fa fa-spinner fa-spin"></i> Generando...';

                // Convertir a JSON string cada registro individualmente
                const registrosComoJson = seleccionadas.map(registro => JSON.stringify(registro));

                console.log('Registros como JSON:', registrosComoJson);

                // Llamar al WebMethod
                $.ajax({
                    url: 'MasivoDeudaGeneral.aspx/GenerarNotificacion',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify({
                        registrosSeleccionados: registrosComoJson
                    }),
                    success: function (response) {
                        let resultado = JSON.parse(response.d);
                        console.log('Resultado:', resultado);

                        if (resultado.success) {
                            alert(resultado.mensaje);
                            window.location.href = `./DetNotificacionesGeneral.aspx?nro_emision=${resultado.nroEmision}&subsistema=${resultado.subsistema}`;
                        } else {
                            alert(resultado.mensaje);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error('Error:', error);
                        console.error('Response:', xhr.responseText);
                        alert('Error al generar notificaciones: ' + error);
                    },
                    complete: function () {
                        btnGenerar.disabled = false;
                        btnGenerar.innerHTML = textoOriginal;
                    }
                });
            }



        </script>
    </asp:Content>