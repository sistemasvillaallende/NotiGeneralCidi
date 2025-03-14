<%@ Page Title="Nueva Notificación" Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    MasterPageFile="~/Master/MasterPage.master" CodeBehind="MasivoDeuda.aspx.cs"
    Inherits="NotificacionesCIDI.Secure.MasivoDeuda"  ValidateRequest="false" %>

    <asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
        <title>Masivo Deuda</title>
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

        <div class="wrapper">
            <div class="content-wrapper">
                <section class="content">
                    <div class="box">
                        <div class="col-md-12">
                            <div id="divFiltros" runat="server">
                                <div class="row" style="margin-top: 25px;">
                                    <div class="col-md-12">
                                        <h3 style="color: #367fa9;">Inmueble - Nueva Notificación</h3>
                                            </div>
                                                </div>
                                    
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label>Categoria Deuda</label>
                                                    <br />
                                                    <asp:ListBox ID="lstCatDeuda" Height="180" Width="280" CssClass="form-control list-group"
                                                        runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Barrio</label>
                                                    <asp:ListBox ID="lstBarrios" Height="180" Width="280" CssClass="form-control" runat="server"
                                                        SelectionMode="Multiple" AutoPostBack="true" 
                                                        OnSelectedIndexChanged="lstBarrios_SelectedIndexChanged"></asp:ListBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <label>Zonas</label>
                                                    <asp:ListBox ID="lstZonas" Height="180" CssClass="form-control" runat="server" 
                                                    SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Calles</label>
                                                    <asp:ListBox ID="lstCalles" Height="180" CssClass="form-control" runat="server" 
                                                    SelectionMode="Multiple"></asp:ListBox>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <label>Desde</label>
                                                            <asp:TextBox ID="txtDesde" Enabled="false" Type="number" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <label>Hasta</label>
                                                            <asp:TextBox ID="txtHasta" Enabled="false" Type="number"  CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
 
                                <div class="row">
                                    <div class="col-md-12" style="text-align: right;">
                                        <button type="button" class="btn btn-outline" id="btnFiltros" runat="server"
                                            onserverclick="btnFiltros_ServerClick">
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
                            <asp:HiddenField ID="MyHiddenControl" value="name" runat="server" />
                            <asp:HiddenField ID="MyHiddenControl2" value="name" runat="server" ValidateRequestMode="Disabled"/>
                            <div id="divResultados" runat="server" visible="false" style="margin-top: 20px;">
                                <div class="row">
                                    <div class="12" style="text-align: right">
                                        <a class="btn btn-outline-danger"onclick="abrirModalPlantillas();">
                                            <i class="fa fa-file-excel-o"></i> PLANTILLA
                                        </a>
                                        <button type="button" class="btn btn-outline-danger" id="btnClearFiltros"
                                            runat="server" onserverclick="btnClearFiltros_ServerClick">
                                            <span class="fa fa-filter-circle-xmark"></span>&nbsp;Limpiar Filtros
                                        </button>
                                        <button type="button" id="btnNoti" class="btn btn-outline"
                                            data-bs-toggle="modal" data-bs-target="#exampleModal">
                                            <span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación CIDI
                                        </button>
                                        <button type="button" runat="server" id="btnExportExcel"
                                            onserverclick="btnExportExcel_ServerClick" class="btn btn-outline-excel"
                                            data-toggle="modal" data-target="#page-change-name">
                                            <span class="fa fa-sheet-plastic"></span>&nbsp;Exportar a Excel
                                            
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
                                                        <asp:Label ID="lblNroCta" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="nombre" ControlStyle-Width="10%" HeaderText="Nombre" />
                                                <asp:BoundField DataField="apellido" ControlStyle-Width="10%" HeaderText="Apellido" />
                                                <asp:BoundField DataField="cuit" ControlStyle-Width="10%" HeaderText="CUIT" />
                                                <asp:BoundField DataField="nom_calle" ControlStyle-Width="10%" HeaderText="Calle" />
                                                <asp:BoundField DataField="nom_barrio" ControlStyle-Width="10%" HeaderText="Barrio" />
                                            </Columns>
                                        </asp:GridView>
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
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <button runat="server" id="btnGenerarNoti" onserverclick="btnGenerarNoti_ServerClick"
                            type="button" class="btn btn-primary">Aceptar</button>
                        <button type="button" class=" btn btn-primary " id="btnNotas" >Notas</button>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="plantillaModal" tabindex="-1" aria-labelledby="plantillaModalLabel" aria-hidden="true">
            <div class=" modal-dialog">
                <div class=" modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" style="text-align: left;">Crear plantilla</h4>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div id="editor-container"
                                style="height: 200px; border: 1px solid #ccc; margin-left: 20px; margin-right: 20px;">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <div class="d-flex flex-row gap-3 mt-3 pb-3">
                            <button type="button" class=" btn btn-primary " onclick="insertVariable('{nombre}')">Insertar Nombre</button>
                            <button type="button" class=" btn btn-primary " onclick="insertVariable('{apellido}')">Insertar Apellido</button>
                            <button type="button" class=" btn btn-primary " onclick="insertVariable('{cuit}')">Insertar CUIT</button>
                            <button type="button" class=" btn btn-primary " onclick="generarNotas()">GENERAR NOTAS</button>
                        </div>
                            <asp:TextBox ID="hiddenInput2" runat="server" TextMode="MultiLine" Style="display: none;" ValidateRequestMode="Disabled"></asp:TextBox>
                            <asp:Literal ID="litNotasGeneradas" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="plantillaModalNotas" tabindex="-1" aria-labelledby="ModalLabel"
        aria-hidden="true">
        <div class="modal-dialog1 modal-dialog">
            <div class="modal-content1 modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">Lista de planillas</h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:GridView ID="gvPlantilla" CssClass="table" runat="server"
                            OnRowDataBound="gvPlantilla_RowDataBound"
                            OnRowCommand="gvPlantilla_RowCommand" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="None"  EnableViewState="true"
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
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
            </div>
        </div>


            <div class="modal fade" id="plantillaModalNombreNotas" aria-labelledby="plantillaModalNombreLabel"
                aria-hidden="true">
                <div class="modal-dialog1 modal-dialog">
                    <div class="modal-content1 modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Nombre de la nota</h4>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="txtNombreNota">Ingrese el nombre de la nota:</label>
                                <asp:TextBox ID="txtNombreNota" runat="server" CssClass="form-control"
                                    EnableViewState="true" ViewStateMode="Enabled"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"  onclick="volverAlModalPrincipal()">Cancelar</button>
                            <asp:Button ID="btnGuardarNota" runat="server" CssClass="btn btn-primary"
                                Text="Guardar" OnClick="btnGuardarNota_Click" UseSubmitBehavior="false"
                                ClientIDMode="Static" OnClientClick=" DoCustomPost();" />
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
           
           $(document).ready(function () {
    // Limpiar cualquier modal-backdrop al cargar la página
    $('.modal-backdrop').remove();
    $('body').removeClass('modal-open').css('padding-right', '');
    
    // Cuando se hace clic en el botón para abrir el primer modal
    $('#btnNoti').click(function(e) {
        e.preventDefault(); // Prevenir comportamiento por defecto
        
        // Limpiar campos del modal antes de abrirlo
        $("#<%= txtNombreNoti.ClientID %>").val('');
        $("#<%= txtescripcion.ClientID %>").val('');
        
        // Limpiar cualquier backdrop residual
        $('.modal-backdrop').remove();
        $('body').removeClass('modal-open').css('padding-right', '');
        
        // Mostrar el modal
        $('#exampleModal').modal('show');
    });
    
    // El resto del código que ya tienes
    // Cuando se hace clic en una fila del GridView
    $(document).on('click', '#<%= gvPlantilla.ClientID %> tr', function () {
        var contenido = $(this).attr("data-contenido");
        
        if (contenido) {
            $("#<%= txtescripcion.ClientID %>").val(contenido);
            $('#plantillaModalNotas').modal('hide');
            
            // Remover cualquier backdrop residual
            setTimeout(function() {
                $('.modal-backdrop').remove();
                $('body').removeClass('modal-open').css('padding-right', '');
                $('#exampleModal').modal('show');
            }, 500);
        }
    });
    
    // Botón de notas
    $('#btnNotas').click(function () {
        $('#exampleModal').modal('hide');
        
        setTimeout(function() {
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open').css('padding-right', '');
            $('#plantillaModalNotas').modal('show');
        }, 500);
    });
    
    // Limpiar datos al cerrar el primer modal
    $('#exampleModal').on('hidden.bs.modal', function () {
        $("#<%= txtNombreNoti.ClientID %>").val('');
        $("#<%= txtescripcion.ClientID %>").val('');
        
        // También limpiar cualquier backdrop residual
        $('.modal-backdrop').remove();
        $('body').removeClass('modal-open').css('padding-right', '');
    });
    
    // Botón cancelar del segundo modal
    $('#plantillaModalNotas .btn-secondary').click(function () {
        $('#plantillaModalNotas').modal('hide');
        
        setTimeout(function() {
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open').css('padding-right', '');
            $('#exampleModal').modal('show');
        }, 500);
    });
    
    // Botón cancelar del primer modal
    $('#exampleModal .btn-secondary').click(function () {
        $("#<%= txtNombreNoti.ClientID %>").val('');
        $("#<%= txtescripcion.ClientID %>").val('');
        $('#exampleModal').modal('hide');
        $('.modal-backdrop').remove();
        $('body').removeClass('modal-open').css('padding-right', '');
    });
    
    // Botón cerrar (X) del primer modal
    $('#exampleModal .btn-close').click(function () {
        $("#<%= txtNombreNoti.ClientID %>").val('');
        $("#<%= txtescripcion.ClientID %>").val('');
        $('#exampleModal').modal('hide');
        $('.modal-backdrop').remove();
        $('body').removeClass('modal-open').css('padding-right', '');
    });
    
    // Si tu aplicación utiliza UpdatePanel, asegúrate de reinicializar cuando haya postbacks parciales
    if (typeof Sys !== 'undefined' && Sys.WebForms) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
            // Reinicializar eventos después de cada postback parcial
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open').css('padding-right', '');
            
            // Volver a configurar el botón que abre el modal inicial
            $('#btnNoti').off('click').on('click', function(e) {
                e.preventDefault();
                $("#<%= txtNombreNoti.ClientID %>").val('');
                $("#<%= txtescripcion.ClientID %>").val('');
                $('.modal-backdrop').remove();
                $('body').removeClass('modal-open').css('padding-right', '');
                $('#exampleModal').modal('show');
            });
        });
    }
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



            function abrirModalPlantillas() {
                $('#plantillaModal').modal('show');
                    }

            function abrirModalNotas() {
                $('#exampleModal').modal('hide');
                $('#plantillaModalNotas').modal({
                    backdrop: 'static',
                    keyboard: false
                });
                $('#plantillaModalNotas').modal('show');
                }


$(document).ready(function() {
    // Inicializar Quill
    quill = new Quill('#editor-container', {
        theme: 'snow',
        modules: {
            toolbar: [['bold', 'italic', 'underline'], ['clean']]
        }
    });
    
    // Limpiar Quill cuando se cierra el modal
    $('#plantillaModal').on('hidden.bs.modal', function() {
        if (quill) {
            quill.root.innerHTML = '';
        }
        $('#hiddenInput2').val('');
    });
    
    // Configurar el botón X para cerrar el modal de nombre de notas
    $("#plantillaModalNombreNotas .btn-close").click(function() {
        $("#plantillaModalNombreNotas").modal('hide');
        
        // Opcional: Volver al modal principal después de cerrar este
        setTimeout(function() {
            $('#plantillaModal').modal('show');
        }, 500);
    });
    
    // También asegúrate de que el botón Cancelar cierre correctamente
    $("#plantillaModalNombreNotas .btn-secondary").click(function() {
        $("#plantillaModalNombreNotas").modal('hide');
        
        // Si deseas volver al modal principal
        setTimeout(function() {
            $('#plantillaModal').modal('show');
        }, 500);
    });
});

function volverAlModalPrincipal() {
    // Guardar una referencia al modal que deberíamos abrir
    var modalToShow = 'plantillaModal';
    
    // Cerrar el modal actual
    $("#plantillaModalNombreNotas").modal('hide');
    
    // Limpiar cualquier backdrop residual y abrir el modal correcto
    setTimeout(function() {
        // Eliminar todos los backdrops y limpiar clases del body
        $('.modal-backdrop').remove();
        $('body').removeClass('modal-open').css('padding-right', '');
        
        // Asegurarse de que todos los modales estén cerrados
        $('.modal').modal('hide');
        
        // Abrir específicamente el modal plantillaModal, no exampleModal
        $('#' + modalToShow).modal('show');
    }, 500);
}

function abrirModalNombrePlantilla() {
    var modal = $("#plantillaModalNombreNotas");
    
    // Ocultar el modal principal primero
    $("#plantillaModal").modal('hide');
    
    // Limpiar cualquier backdrop residual
    setTimeout(function() {
        $('.modal-backdrop').remove();
        $('body').removeClass('modal-open').css('padding-right', '');
        
        // Mostrar el modal de nombre
        modal.appendTo("body");
        modal.modal("show");
        
        setTimeout(function() {
            var maxZIndex = Math.max(...Array.from(document.querySelectorAll(".modal"))
                .map(m => parseFloat(window.getComputedStyle(m).zIndex) || 1050));
            
            modal.css({
                "display": "block",
                "z-index": maxZIndex + 10,
                "opacity": "1"
            });
            
            $(".modal-backdrop").not(".modal-stack").css("z-index", maxZIndex + 9).addClass("modal-stack");
        }, 100);
    }, 500);
}

// Tus funciones existentes
function setQuillContent(content) {
    if (quill) {
        quill.root.innerHTML = content;
    }
}

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

function DoCustomPost() {
    var ModalTextBox = document.getElementById("<%= txtNombreNota.ClientID %>");
    var HiddenTextBox = document.getElementById("<%= MyHiddenControl.ClientID %>");
    
    if (ModalTextBox) {
        HiddenTextBox.value = ModalTextBox.value;
    }
    return true;
}

function DoCustomPost2() {
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

function generarNotas() {
    DoCustomPost2();
    abrirModalNombrePlantilla();
}
        </script>
    </asp:Content>