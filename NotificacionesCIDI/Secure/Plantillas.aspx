<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plantillas.aspx.cs" 
    Inherits="NotificacionesCIDI.Secure.Plantillas" MasterPageFile="~/Master/MasterPage.master"  
    Debug="true" ValidateRequest="false" Title="Plantillas" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <div class="row" style="padding: 20px; ">
                <div class="col-12">
                    <div class="row">
                        <div class="col-10" style="padding: 20px; padding-bottom: 0px;">
                            <h1 style="display: inline-block; margin-right: 20px;">Plantillas</h1>
                        </div>                                           
                        <div class="col-2" style="padding: 20px; text-align: right; padding-bottom: 0px;">
                            <button type="button" id="btnNoti" class="btn btn-outline-primary"
                            data-bs-toggle="modal" data-bs-target="#plantillaModal">
                            <span class="fa fa-sheet-plastic"></span>&nbsp;Nueva Plantilla
                        </button>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="MyHiddenControl" value="name" runat="server" />
            <asp:HiddenField ID="MyHiddenControl2" value="name" runat="server" ValidateRequestMode="Disabled"/>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanelPlantillas" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridPlantillas" runat="server"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-striped table-hover"
                                    GridLines="None"
                                    EnableViewState="true"
                                    OnRowDataBound="GridPlantillas_RowDataBound"
                                    OnRowCommand="GridPlantillas_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="Numero Plantilla" />
                                        <asp:BoundField DataField="nom_plantilla" HeaderText="Nombre de la Plantilla"/>
                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:LinkButton
                                                    ID="btnEditar"
                                                    runat="server"
                                                    CssClass="btn btn-outline-primary"
                                                    CommandName="Editar"
                                                    CommandArgument='<%# Eval("id") %>'>
                                                    <i class="fa-solid fa-pen"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton
                                                    ID="btnEliminar"
                                                    runat="server"
                                                    CssClass="btn btn-outline-danger"
                                                    CommandName="Eliminar"
                                                    CommandArgument='<%# Eval("id") %>'>
                                                    <i class="fa-solid fa-trash"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal fade" id="exampleModalDelete" data-bs-backdrop="false"  tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Plantilla Eliminada</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p style="text-align: center">
                                La plantilla se ha eliminado correctamente
                            </p>                            
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="exampleModalUpdate" data-bs-backdrop="false" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Plantilla Actualizada</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p style="text-align: center">
                                La plantilla se ha actualizado correctamente
                            </p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="plantillaModal" tabindex="-1" aria-labelledby="plantillaModalLabel" aria-hidden="true">
                <div class=" modal-dialog modal-lg">
                    <div class=" modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="text-align: left;">Nueva Plantilla</h4>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div id="editor-container"
                                    style="height: 50vh;width: 100%; ">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="d-flex flex-row gap-3 ">
                                <button type="button" class=" btn btn-primary " onclick="insertVariable('{nombre}')">Insertar Nombre</button>
                                <button type="button" class=" btn btn-primary " onclick="insertVariable('{apellido}')">Insertar Apellido</button>
                                <button type="button" class=" btn btn-primary " onclick="insertVariable('{cuit}')">Insertar CUIT</button>
                                <button type="button" class=" btn btn-primary " onclick="generarNotas()">GENERAR PLANTILLA</button>
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            </div>
                                <asp:TextBox ID="hiddenInput2" runat="server" TextMode="MultiLine" Style="display: none;" ValidateRequestMode="Disabled"></asp:TextBox>
                                <asp:Literal ID="litNotasGeneradas" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="plantillaModalEditar" tabindex="-1" aria-labelledby="plantillaModalLabel" aria-hidden="true">
                <div class=" modal-dialog modal-lg">
                    <div class=" modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="text-align: left;">Editar Plantilla</h4>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div id="editor-container-editar"
                                    style="height: 50vh;width: 100%; ">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="d-flex flex-row gap-3 ">
                                <button type="button" class=" btn btn-primary " onclick="insertVariableEditar('{nombre}')">Insertar Nombre</button>
                                <button type="button" class=" btn btn-primary " onclick="insertVariableEditar('{apellido}')">Insertar Apellido</button>
                                <button type="button" class=" btn btn-primary " onclick="insertVariableEditar('{cuit}')">Insertar CUIT</button>
                                <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" 
                                     CssClass="btn btn-primary" OnClick="btnGuardarCambios_Click"  OnClientClick="return prepararContenido();" />
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            </div>
                                <asp:TextBox ID="hiddenInput3" runat="server" TextMode="MultiLine" Style="display: none;" ValidateRequestMode="Disabled"></asp:TextBox>
                                <asp:Literal ID="litNotasGeneradas2" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="plantillaModalNombreNotas" aria-labelledby="plantillaModalNombreLabel"
            aria-hidden="true">
            <div class=" modal-dialog">
                <div class=" modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Nombre de la Plantilla</h4>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="txtNombreNota">Ingrese el nombre de la plantilla :</label>
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

            <script src="https://cdn.quilljs.com/1.3.7/quill.min.js"></script>
            <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
            <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
            <script>

                var quill;
                var quillEditar;

                $(document).ready(function() {
                    // Inicializar Quill
                    quill = new Quill('#editor-container', {
                        theme: 'snow',
                        modules: {
                            
                            toolbar: [
                                [{ 'header': [1, 2, 3, false] }],
                                [{ 'size': ['small', false, 'large', 'huge'] }], 
                                ['bold', 'italic', 'underline'],
                                [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                                ['clean']
                                    ]
                        }
                    });

                    $('#plantillaModalEditar').on('shown.bs.modal', function () {
                            if (!quillEditar) {
                                quillEditar = new Quill('#editor-container-editar', {
                                    theme: 'snow',
                                    modules: {
                                        toolbar: [
                                            [{ 'header': [1, 2, 3, false] }],
                                            [{ 'size': ['small', false, 'large', 'huge'] }],
                                            ['bold', 'italic', 'underline'],
                                            [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                                            ['clean']
                                        ]
                                    }
                                });
                            }
                        });
                    
                    // Limpiar Quill cuando se cierra el modal
                    $('#plantillaModal').on('hidden.bs.modal', function() {
                        if (quill) {
                            quill.root.innerHTML = '';
                        }
                        $('#hiddenInput2').val('');
                    });                   
                    
                    });
                    $('#plantillaModalEditar').on('hidden.bs.modal', function() {
                            if (quillEditar) {
                                quillEditar.root.innerHTML = '';
                            }
                        });



                function setQuillContent(content) {
                    if (quillEditar) {
                        quillEditar.root.innerHTML = content;
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
                function insertVariableEditar(variable) {
                    const range = quillEditar.getSelection();
                    if (range) {
                        quillEditar.insertText(range.index, variable, {
                            'bold': true,
                            'background': '#f0f0f0'
                        });
                        quill.setSelection(range.index + variable.length);
                    }
                }
                
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

                function prepararContenido() {
    // Obtener el contenido del editor
    var contenido = quillEditar.root.innerHTML;
    
    // Guardar el contenido en el campo oculto
    document.getElementById('<%= hiddenInput3.ClientID %>').value = contenido;
    
    return true; 
}
        

            </script>

        </div>
    </asp:Content>