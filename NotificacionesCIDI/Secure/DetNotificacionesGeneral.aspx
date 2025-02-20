<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetNotificacionesGeneral.aspx.cs" Inherits="NotificacionesCIDI.Secure.DetNotificacionesGeneral" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Masivo Deuda</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="../App_Themes/Main/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .btn-outline {
            background-color: transparent;
            border: solid darkcyan;
            color: darkcyan;
            font-weight: 500;
            border-radius: 15px;
        }

        .btn-outline-danger {
            background-color: transparent;
            border: solid #dc2626;
            color: #dc2626;
            font-weight: 500;
            border-radius: 15px;
        }

        .btn-outline-excel {
            background-color: transparent;
            border: solid #006e37;
            color: #006e37;
            font-weight: 500;
            border-radius: 15px;
        }



    </style>
    <link href="../App_Themes/fontawesome/css/all.css" rel="stylesheet" />
</head>
<body class="skin-blue sidebar-mini sidebar-collapse">
    <form id="form1" runat="server">

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
        <header class="clearfix no-mobile print"
            style="text-align: left; position: fixed; width: 100%; z-index: 1000; background-color: white; margin-top: -5px; height: 90px;">
            <div class="row" style="padding-top: 20px; padding-bottom: 0px;">
                <div class="col-4" style="padding-left: 4%;">
                    <a href="/BackEnd/Home.aspx">
                        <img src="../App_Themes/Main/img/LogoPablo.png" style="height: 50px;" />
                    </a>
                </div>
                <div class="col-md-4" style="align-items: center; display: flex; text-align: center; height: 60px;">
                    <a href="#" style="width: 100%; text-decoration: none;">
                        <h2 class="section-title__title"
                            style="color: #6f6f6e; font-size: 24px; line-height: 10px; text-decoration-line: overline;">
                            <img src="../App_Themes/Main/img/vecino.png" style="height: 50px;" />
                            SIIMVA WEB
                            <img src="../App_Themes/Main/img/condor.png" style="height: 70px; margin-top: -10px;" />
                        </h2>
                    </a>
                </div>
                <div class="col-md-4" style="padding-right: 5%; align-items: center; display: inline-grid; text-align: right; height: 60px;">
                    <div class="dropdown">
                        <button class="btn-usuario" type="button"
                            style="border: none; background-color: transparent; display: inline-flex;"
                            data-bs-toggle="dropdown" aria-expanded="false">
                            <img id="imgUsuario" runat="server" src="~/App_Themes/Main/img/usuario.png" class="img-thumbnail" alt="..." style="height: 55px; border: none;" />
                            <ul style="color: gray; list-style: none; text-align: left; padding-left: 0; margin-bottom: 0px;">
                                <li id="liNombre" runat="server">Ignacio Martin</li>
                                <li id="liApellido" runat="server">Velez Spitale</li>
                            </ul>
                        </button>
                        <ul class="dropdown-menu">
                            <li style="display: grid; padding: 15px 25px 0px 25px" class="li-usuario">
                                <strong id="mnuPcApellido" runat="server">Velez Spitale</strong>
                                <span id="mnuPcNombre" runat="server">Ignacio Martin</span></li>
                            <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                <span style="display: ruby;">Oficina: </span>
                                <span style="display: block; margin-left: 10px;" id="SpanOficina" runat="server"></span></li>
                            <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                <span style="display: ruby;">CUIT: </span>
                                <span style="display: block; margin-left: 10px;" id="mnuPcCuit" runat="server">23-27.173.499-9</span></li>
                            <li style="display: flex; padding: 15px 25px 0px 25px" class="li-usuario">
                                <span style="display: block;">CIDI: </span>
                                <span style="display: block; margin-left: 10px;" id="mnuPcNivelCidi" runat="server">Nivel 2</span></li>
                            <li style="padding: 15px; border-top: solid 1px lightgray; margin-top: 15px;">
                                <a class="dropdown-item" href="#" runat="server"
                                    id="btnCerraSession" onserverclick="btnCerraSession_ServerClick" style="text-align: center; border: solid 3px lightgray; border-radius: 10px; padding: 8px;">Cerrar Sesion</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="container-fluid"
                style="background: linear-gradient(87deg, rgb(148 23 23) 0%, rgba(255,35,0,1) 41%, rgb(255 233 0) 79%);">
                <div class="row" style="height: 10px;">
                    <div class="col-md-12" style="padding-top: 0px;">
                    </div>
                </div>
            </div>
        </header>
        

            
        <div style="padding-left: 5%; padding-right:5%;">
            <div class="row">
                <div class="col-12" style="padding: 25px;">
                    <h1 style="font-size: 36px !important; font-weight: 600 !important; margin-bottom: 5px !important; display: flex !important; align-items: start !important;">
                        <span class="fa fa-car-side" style="color: #c09e76; border-right: solid 3px; padding-right: 10px;"></span>
                        <span style="font-size: 20px !important; padding-left: 8px !important; margin-top: -5px !important;">Automotores</span> </h1>
                    <h1 style="font-size: 16px !important; font-weight: 500 !important; color: gray !important; margin-left: 65px !important; margin-top: -24px !important;">Procuraciones - Cambio de estado masivo</h1>
                    <hr style="margin-top: 5px; border: 2px solid #c09e76; margin-bottom: 20px; opacity: 1;" />
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:GridView AutoGenerateColumns="false" CssClass="table" ID="gvMasivosAut" runat="server">
                        <Columns>
                           
                            <asp:BoundField DataField="Nro_Emision" HeaderText="Emisión" />
                            <asp:BoundField DataField="Nro_Notificacion" HeaderText="Nro Notificacion" />
                            <asp:BoundField DataField="Dominio" HeaderText="Dominio" />
                            <asp:BoundField DataField="Circunscripcion" HeaderText="Cir" />
                            <asp:BoundField DataField="Seccion" HeaderText="Sec" />
                            <asp:BoundField DataField="Manzana" HeaderText="Man" />
                            <asp:BoundField DataField="Parcela" HeaderText="Par" />
                            <asp:BoundField DataField="P_h" HeaderText="P_h" />
                            <asp:BoundField DataField="Legajo" HeaderText="Legajo" />
                            <asp:BoundField DataField="Cuit" HeaderText="Cuit" />
                            <asp:BoundField DataField="Nro_Badec" HeaderText="Nro_Badec" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Cod_estado_cidi" HeaderText="Cod estado" />
                            <asp:BoundField DataField="Fecha_Inicio_Estado" HeaderText="Fecha Inicio" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Fecha_Fin_Estado" HeaderText="Fecha Fin" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Vencimiento" HeaderText="Vencimiento" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Nro_cedulon" HeaderText="Nro Cedulon" />
                            <asp:BoundField DataField="Debe" HeaderText="Debe" />
                            <asp:BoundField DataField="Monto_original" HeaderText="Monto" />
                            <asp:BoundField DataField="Interes" HeaderText="Interes" />
                            <asp:BoundField DataField="Descuento"  HeaderText="Desc" />
                            <asp:BoundField DataField="Importe_pagar"  HeaderText="Importe" />
                
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col-12"></div>
            </div>
            <div class="row">
                <div class="col-12"></div>
            </div>
    
        </div>
          
        
        
            </form>
</body>
<script src="../App_Themes/Main/js/jQuery-2.1.4.min.js"></script>
<script src="../App_Themes/Main/js/jquery-ui-1.10.3.min.js"></script>
<script src="../App_Themes/Main/js/bootstrap.min.js"></script>
<script src="../App_Themes/Main/js/bootstrap.bundle.min.js"></script>
<script src="../App_Themes/fontawesome/js/all.js"></script>
</html>
