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
		text-align: center !important ;
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
                                            <span class="text-muted ml-3">Nota: El archivo Excel debe contener únicamente una columna de CUIT en la primera columna.</span>
                                        </div>
                                        <div style="width:200px">
                                        <asp:Label ID="lblUploadStatus" runat="server" CssClass="alert alert-success d-block mt-3"  Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>                                
                            <div class="row">
                                <div class="col-md-12" style="text-align: right;">
                                    <button type="button" class="btn btn-outline-primary" id="btnFiltros" runat="server"
                                        onserverclick="btnFiltros_ServerClick">
                                        <span class="fa fa-filter"></span>&nbsp;Aplicar Filtros
                                    </button>
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
                        <div id="divResultados" runat="server" visible="false" style="margin-top: 20px;">
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
                                    <button runat="server" id="btnGenerarNoti"
                                        onserverclick="btnGenerarNoti_ServerClick"
                                        type="button" class="btn btn-outline-primary" >
                                        <span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación </button>
                                        <button type="button" runat="server" id="btnExportExcel"
                                        onserverclick="btnExportExcel_ServerClick" class="btn btn-outline-success"
                                        data-toggle="modal" data-target="#page-change-name">
                                        <span class="fa fa-sheet-plastic"></span>&nbsp;Exportar a Excel                                           
                                    </button>                                                                      
                                </div>
                            </div>
                            <div class="row" style="margin-top: 20px; margin-right:15px;">
                                <div class="col-md-12"
                                     style="margin-left: 15px; height: 70vh; overflow-y: scroll; border: solid lightgray; border-radius: 15px;">
                                        <asp:GridView ID="gvConceptos" CssClass="table "
                                            AutoGenerateColumns="false" OnRowCommand="gvConceptos_RowCommand"
                                            OnRowDeleting="gvConceptos_RowDeleting" DataKeyNames="cuit"
                                            EmptyDataText="No hay resultados..." runat="server" CellPadding="4"
                                            ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <HeaderTemplate>
                                                        <input type="checkbox" id="chkAll" name="chkAll"
                                                            onclick="javascript: SelectAllCheckboxes(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="Cuit" ControlStyle-Width="10%"
                                                     HeaderText="CUIT" />
                                                  <asp:BoundField DataField="Nombre" ControlStyle-Width="10%"
                                                     HeaderText="Nombre" />
                                                 <asp:BoundField DataField="Apellido" ControlStyle-Width="10%"
                                                     HeaderText="Apellido" />
                                            </Columns>
                                        </asp:GridView>
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
        <div class="modal fade" id="modalConceptos">
            <div class="modal-dialog modal-upload">
                <div class="modal-content">
                    <div class="modal-header">                       
                        <h4 class="modal-title">Subir lista de Cuit</h4>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label  class="pt-2 pb-3">Subir archivo</label>
                            <asp:FileUpload ID="fUploadConceptos" CssClass="form-control" runat="server" onchange="hideErrorMessage()" />
                            <asp:Label ID="lblFileError" runat="server" CssClass="text-danger" 
                                Text="*Archivo no ha sido seleccionado" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button data-dismiss="modal" class="btn btn-danger">Cancelar</button>
                        <asp:Button ID="btnConceptos_x_legajos" CssClass="btn btn-primary"
                            OnClientClick="this.disabled=true;this.value = 'Procesando...'" UseSubmitBehavior="false"
                            OnClick="btnConceptos_x_legajos_Click" runat="server" Text="Aceptar" />
                    </div>
                </div>
            </div>
        </div>
    <div class="modal fade" id="plantillaModalNotas" tabindex="-1" aria-labelledby="ModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-plantillas">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Lista de planillas</h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:GridView ID="gvPlantilla" CssClass="table" runat="server"
                            OnRowDataBound="gvPlantilla_RowDataBound"
                            OnRowCommand="gvPlantilla_RowCommand" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="None" EnableViewState="true"
                            DataKeyNames="id,contenido">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775">
                            </AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionar" runat="server"  onclick="SoloUnCheckbox(this); event.cancelBubble=true;"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nom_plantilla" ControlStyle-Width="10%"
                                    HeaderText="Nombre Plantilla" SortExpression="nom_plantilla" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">  
                <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-primary" OnClick="btnObtenerSeleccionados_Click" />
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

        <script src="https://cdn.quilljs.com/1.3.7/quill.min.js"></script>
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>


        <script>
             function abrirModalPlantillas() {
                        $('#plantillaModalNotas').modal('show');
            }

            function abrirmodalConceptos() {
                $('#modalConceptos').modal('show');
            }
                  
                    // Funcion para poder seelccionar el check cuando aprieto cualquier lugar del gridview
                    function SeleccionarFila(fila, checkBoxId) {
                        var chk = document.getElementById(checkBoxId);
                
                        if (chk !== null) {
                            chk.checked = !chk.checked;
                            SoloUnCheckbox(chk);
                        }
                    }

                    // Funcion para solo seleccionar un checkbox
                    function SoloUnCheckbox(chk) {
                        var grid = document.getElementById('<%= gvPlantilla.ClientID %>');
                        var checkboxes = grid.getElementsByTagName('input');

                        for (var i = 0; i < checkboxes.length; i++) {
                            var tipo = checkboxes[i].type;
                            if (tipo === 'checkbox' && checkboxes[i] !== chk) {
                                checkboxes[i].checked = false;
                            }
                        }
                    }

// para agregar spinner en generar notificaciones
            if (window.jQuery) {
                $(document).ready(function() {
                    $('#<%= btnGenerarNoti.ClientID %>').on('click', function(e) {
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
                document.getElementById('<%= lblFileError.ClientID %>').style.display = 'none';
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
        </script>
    </asp:Content>