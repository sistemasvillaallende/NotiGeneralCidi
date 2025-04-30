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
        <div class="wrapper" style="padding-right:4%;">
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
                                                <label>Categoria Deuda</label>
                                                <br />
                                                <asp:ListBox ID="lstCatDeuda" Height="180" Width="280" CssClass="form-control list-group"
                                                    runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                        
                                            <div class="col-md-3">
                                                <label>Barrio</label>
                                                <div class="input-group mb-2">
                                                    <input type="text" id="txtSearchBarrio" class="form-control" placeholder="Buscar barrio..." />
                                                </div>
                                                <asp:ListBox ID="lstBarrios" Height="203" CssClass="form-control" runat="server"
                                            SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                        
                                            <div class="col-md-1">
                                                <label>Zonas</label>
                                                <asp:ListBox ID="lstZonas" Height="180" CssClass="form-control" runat="server" 
                                                SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                        
                                            <div class="col-md-2">
                                                <label>Calles</label>
                                                <div class="input-group mb-2">
                                                    <input type="text" id="txtSearchCalle" class="form-control" placeholder="Buscar calle..." />
                                                </div>
                                                <asp:ListBox ID="lstCalles" Height="203px" CssClass="form-control" runat="server" 
                                            SelectionMode="Multiple"></asp:ListBox>
                                            </div>                                                                   
                                            <div class="col-md-3">   
                                                <div class="row"> 
                                                    <div class="col-md-12" >
                                                        <label style="padding-bottom: 15px;">Desde</label>
                                                        <asp:TextBox 
                                                            ID="txtDesde" 
                                                            Enabled="false" 
                                                            Type="number" 
                                                            CssClass="form-control" 
                                                            runat="server"
                                                            style="width: 150px;">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            
                                                <br />
                                            
                                                <div class="row" > 
                                                    <div class="col-md-12">
                                                        <label style="padding-bottom: 15px;">Hasta</label>
                                                        <asp:TextBox 
                                                            ID="txtHasta" 
                                                            Enabled="false" 
                                                            Type="number"  
                                                            CssClass="form-control" 
                                                            runat="server"
                                                            style="width: 150px;"> 
                                                        </asp:TextBox>
                                                    </div>
                                                </div>                                            
                                        </div>
                                    </div>
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
                                        <a class="btn btn-outline-danger"onclick="abrirModalPlantillas();">
                                            <i class="fa fa-list" aria-hidden="true"></i> Plantillas
                                        </a>
                                        <button runat="server" id="btnGenerarNoti" onserverclick="btnGenerarNoti_ServerClick"
                                           type="button" class="btn btn-outline-primary" >
                                           <span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación </button>
                                        <button type="button" runat="server" id="btnExportExcel"
                                            onserverclick="btnExportExcel_ServerClick" class="btn btn-outline-success"
                                            data-toggle="modal" data-target="#page-change-name">
                                            <span class="fa fa-sheet-plastic"></span>&nbsp;Exportar a Excel                                           
                                        </button>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 20px;  margin-right:15px;">
                                    <div class="col-md-12"
                                        style="margin-left: 15px; height: 70vh; overflow-y: scroll; border: solid lightgray; border-radius: 15px;">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvDeuda" CssClass="table"
                                                runat="server" OnRowDataBound="gvDeuda_RowDataBound" 
                                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                GridLines="None"  >
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
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
                                                    <asp:TemplateField HeaderText="Denominación">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNroCta" runat="server" Text="">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nombre" ControlStyle-Width="10%"
                                                        HeaderText="Nombre" />
                                                    <asp:BoundField DataField="apellido" ControlStyle-Width="10%"
                                                        HeaderText="Apellido" />
                                                    <asp:BoundField DataField="cuit" ControlStyle-Width="10%"
                                                        HeaderText="CUIT" />
                                                    <asp:BoundField DataField="nom_calle" ControlStyle-Width="10%"
                                                        HeaderText="Calle" />
                                                    <asp:BoundField DataField="nom_barrio" ControlStyle-Width="10%"
                                                        HeaderText="Barrio" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
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
            <div class="modal-dialog  modal-plantillas">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Lista de planillas</h4>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <asp:GridView ID="gvPlantilla" CssClass="table" runat="server"
                                OnRowDataBound="gvPlantilla_RowDataBound"
                                OnRowCommand="gvPlantilla_RowCommand" AutoGenerateColumns="False" ForeColor="#333333" 
                                GridLines="None" EnableViewState="true"
                                ShowHeader="false" ShowFooter="false"
                                CellPadding="0" CellSpacing="0"
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

        <div class="modal fade" id="modalSeleccionarPlantilla"  aria-labelledby="modalPlantillaLabel" >
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-warning text-dark">
                        <h5 class="modal-title" id="modalPlantillaLabel">Atención</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body">
                        Debe seleccionar una plantilla antes de generar la notificación.
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>

            <script src="https://cdn.quilljs.com/1.3.7/quill.min.js"></script>
            <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
            <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
        <script>
           

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
                        $('#plantillaModalNotas').modal('show');
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

        // Add event listener for the search input
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
        
        // Add event listener for the search input
        $("#txtSearchCalle").on("input", function() {
            var searchText = $(this).val().toLowerCase();
            
            // Clear the listbox
            $listBox.empty();
            
            // Add back only the matching options
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
        </script>
    </asp:Content>