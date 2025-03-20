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
                                    <div class="12" style="text-align: right">
                                        <a class="btn btn-outline-danger"onclick="abrirModalPlantillas();">
                                            <i class="fa fa-file-excel-o"></i> PLANTILLA
                                        </a>
                                        <button type="button" class="btn btn-outline-danger" id="btnClearFiltros"
                                            runat="server" onserverclick="btnClearFiltros_ServerClick">
                                            <span class="fa fa-filter-circle-xmark"></span>&nbsp;Limpiar Filtros
                                        </button>
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

                                <div class="row" style="margin-top: 20px;">
                                    <div class="col-md-12"
                                        style="max-height: 75vh; overflow-y: auto; border: solid lightgray; border-radius: 15px;">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvDeuda" CssClass="table table-striped table-hover"
                                                runat="server" OnRowDataBound="gvDeuda_RowDataBound"
                                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                GridLines="None" AlternatingRowStyle-BackColor="White"
                                                AlternatingRowStyle-ForeColor="#284775">
                                                <Columns>
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

        <div class="modal fade" id="plantillaModalNotas" tabindex="-1" aria-labelledby="ModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
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



          
        </script>
    </asp:Content>