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
    <div class="wrapper" style="padding-right: 4%; padding-left: 2%">
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
                                <div class="col-md-2">
                                    <label class="label">Activo</label>
                                    <asp:DropDownList ID="DDLExento" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label class="label">Zona</label>
                                    <asp:ListBox ID="lstZonas" Style="width: 100%"
                                        CssClass="form-control" runat="server"
                                        SelectionMode="Multiple"></asp:ListBox>
                                </div>

                                <div class="col-md-3">
                                    <label class="label">Activo</label>
                                    <asp:ListBox ID="lstCalles" Height="250px"
                                        CssClass="form-control" runat="server"
                                        Style="width: 100%; height: 37.6px;"
                                        SelectionMode="Multiple"></asp:ListBox>
                                </div>
                                <div class="col-md-1">
                                    <label class="label">Desde</label>
                                    <asp:TextBox
                                        ID="txtDesde"
                                        Enabled="false"
                                        Type="Number"
                                        CssClass="form-control"
                                        runat="server">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <label class="label">Hasta</label>
                                    <asp:TextBox
                                        ID="txtHasta"
                                        Enabled="false"
                                        Type="Number"
                                        CssClass="form-control"
                                        runat="server"> 
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <button type="button" style="margin-top: 27px;" class="btn btn-outline-primary" id="btnFiltros"
                                        runat="server" onserverclick="btnFiltros_ServerClick">
                                        <span class="fa fa-filter"></span>&nbsp;&nbsp;Aplicar Filtro
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
                <div id="divResultados" runat="server" visible="false" style="margin-top: 20px;">
                    <div class="row">
                        <div class="12" style="text-align: right">
                            <a class="btn btn-outline-danger" onclick="abrirModalPlantillas();">
                                <i class="fa fa-list" aria-hidden="true"></i>Plantilla
                            </a>
                            <button runat="server" id="btnGenerarNoti" onserverclick="btnGenerarNoti_ServerClick"
                                type="button" class="btn btn-outline-primary">
                                <span class="fa fa-sheet-plastic"></span>&nbsp;Generar notificación
                            </button>
                            <button type="button" runat="server" id="btnExportExcel"
                                onserverclick="btnExportExcel_ServerClick" class="btn btn-outline-success"
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
                                    <asp:BoundField DataField="legajo" ControlStyle-Width="10%"
                                        HeaderText="Denominacion"></asp:BoundField>
                                    <asp:BoundField DataField="nombre" ControlStyle-Width="10%"
                                        HeaderText="Nombre"></asp:BoundField>
                                    <asp:BoundField DataField="apellido" ControlStyle-Width="10%"
                                        HeaderText="Apellido"></asp:BoundField>
                                    <asp:BoundField DataField="cuit" ControlStyle-Width="10%"
                                        HeaderText="CUIT"></asp:BoundField>
                                    <asp:BoundField DataField="cod_rubro" ControlStyle-Width="10%"
                                        HeaderText="Cod_rubro"></asp:BoundField>
                                    <asp:BoundField DataField="concepto" ControlStyle-Width="10%"
                                        HeaderText="Concepto "></asp:BoundField>
                                    <asp:BoundField DataField="nom_calle" ControlStyle-Width="10%"
                                        HeaderText="Calle"></asp:BoundField>
                                    <asp:BoundField DataField="Nom_barrio" ControlStyle-Width="10%"
                                        HeaderText="Nom_barrio"></asp:BoundField>

                                </Columns>
                            </asp:GridView>
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
                            ForeColor="#333333" GridLines="None" EnableViewState="true"
                            ShowHeader="false" ShowFooter="false"
                            CellPadding="0" CellSpacing="0"
                            DataKeyNames="id,contenido">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionar" runat="server" onclick="SoloUnCheckbox(this); event.cancelBubble=true;" />
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
    <!-- 2. Después cargá Select2 JS y CSS -->
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script>

        //// Modal de plantillas
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

        /////////////////////////////

        $(document).ready(function () {

            $('#<%= lstCalles.ClientID %>').select2({
                placeholder: "Seleccione una o mas calles"
            });
            $('#<%= lstZonas.ClientID %>').select2({
                placeholder: "Seleccione una o mas zonas"
            });

        });



        // para agregar spinner en generar notificaciones
        if (window.jQuery) {
            $(document).ready(function () {
                $('#<%= btnGenerarNoti.ClientID %>').on('click', function (e) {
                    var $btn = $(this);

                    if ($btn.prop('disabled')) {
                        e.preventDefault();
                        return false;
                    }

                    $btn.prop('disabled', true)
                        .addClass('disabled')
                        .html('<span class="spinner-border spinner-border-sm mr-1"></span> Procesando...');

                    setTimeout(function () {
                        $btn.prop('disabled', false)
                            .removeClass('disabled')
                            .html('<span class="fa fa-sheet-plastic"></span> Generar notificación');
                    }, 30000);

                    return true;
                });
            });
        }

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
