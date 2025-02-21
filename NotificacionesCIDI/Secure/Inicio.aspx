<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Inicio.aspx.cs" Inherits="NotificacionesCIDI.Secure.Inicio" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
            .li-menu {
                padding-top: 10px;
                padding-bottom: 10px;
                border-bottom: solid 3px lightgrey;
            }
    
            .a-menu {
                color: #343a40;
            }
        
            .title-h3 {
                font-size: 220px !important;
                text-align: center;
                margin-top: 25px;
                color: gray !important;
                font-weight: 600 !important;
            }
    
            .btn-add {
                font-size: 22px;
                border-radius: 50%;
                padding-top: 5px;
                position: absolute;
                padding-bottom: 5px;
                right: 25px;
                top: 115px;
            }
    
            td {
                font-size: 14px !important;
                vertical-align: middle !important;
            }
        
            .li-menu {
                padding-top: 10px;
                padding-bottom: 10px;
                border-bottom: solid 3px lightgrey;
            }
    
            .a-menu {
                color: #343a40;
            }
    
    
            .title-h3 {
                font-size: 22px !important;
                text-align: center;
                margin-top: 25px;
                color: gray !important;
                font-weight: 600 !important;
            }
    
            .btn-add {
                font-size: 22px;
                border-radius: 50%;
                padding-top: 5px;
                position: absolute;
                padding-bottom: 5px;
                right: 25px;
                top: 115px;
            }
    
            td {
                font-size: 14px !important;
                vertical-align: middle !important;
            }
        
            .form-control {
    
                font-size: 1.5rem !important;
            }
            label{
                font-size: 1.5rem !important;
            }
            .div-con-borde-gradient {
                color: #343a40;
            }
    
                .div-con-borde-gradient::before {
                    height: 5px; /* Tamaño del borde inferior */
                    background: linear-gradient(to right, red, yellow); /* Gradiente de rojo a amarillo */
                }
        </style>
    </asp:Content>
    
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>Inicio</h1>
        <asp:Button ID="btnMasivoDeuda" runat="server" Text="Masivo Deuda" PostBackUrl="~/Secure/MasivoDeuda.aspx" />
        <asp:Button ID="btnNotificacionesCIDI" runat="server" Text="Notificaciones CIDI"
            PostBackUrl="~/Secure/NotificacionesCIDI.aspx" />
        <asp:Button ID="btnPlantillas" runat="server" Text="Plantillas" PostBackUrl="~/Secure/Plantillas.aspx" />

        <div class="container-fluid ">
            <div class="row">
                <div class="col-2" style="padding-top: 10px; box-shadow: rgba(0, 0, 0, 0.5) 0px 2px 10px; min-height: 100vh; padding-left: 0; padding-right: 0;">
                    <ul style="list-style: none; padding-left: 0px; margin-top: 15px;">
                        <li>
                            <a href="#" class="div-con-borde-gradient" style="font-size: 18px; margin-left: 15px; font-weight: 700; text-decoration:none">
                                <i class="fa fa-car" style="margin-right: 8px;"></i>
                                <span>Automotores</span>
                            </a>
                            <div class="container-fluid"
                                style="background: linear-gradient(87deg, rgb(148 23 23) 0%, rgba(255,35,0,1) 41%,
                                    rgb(255 233 0) 79%); margin-top: 5px">
                                <div class="row">
                                    <div class="col-md-12" style="padding-top: 5px;">
                                    </div>
                                </div>
                            </div>
    
                        </li>
                        <li class="li-menu" style="margin-top: 10px;">
                            <a href="./NotificacionesGeneral.aspx?subsistema=4" class="a-menu" style="width: 100%; display: block; margin-left: 15px; text-decoration:none">
                                <span style="font-size: 16px !important;">Notificacion Auto</span>
                            </a>
                        </li>
                        <li class="li-menu">
                            <a href="./MasivoDeudaAuto.aspx" class="a-menu" style="width: 100%; display: block; 
    margin-left: 15px; text-decoration:none">
                                <span style="font-size: 16px !important;">Nueva notificacion</span>
                            </a>
                        </li>
                        <li style="margin-top: 20px;">
                            <a href="#" class="a-menu" style="font-size: 18px; margin-left: 15px; font-weight: 700; text-decoration:none">
                                <i class="fa fa-shopping-cart" style="margin-right: 8px;"></i>
                                <span>Ind. y Comercio</span>
                            </a>
                            <div class="container-fluid"
                                style="background: linear-gradient(87deg, rgb(148 23 23) 0%, rgba(255,35,0,1) 41%,
                                    rgb(255 233 0) 79%); margin-top: 5px">
                                <div class="row">
                                    <div class="col-md-12" style="padding-top: 5px;">
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li class="li-menu" style="margin-top: 10px;">
                            <a href="./NotificacionesGeneral.aspx?subsistema=3" class="a-menu" style="width: 100%; display: block;
    margin-left: 15px; text-decoration:none">
                                <span style="font-size: 16px !important;">Notificacion Ind. y Comercio</span>
                            </a>
                        </li>
                        <li class="li-menu">
                            <a href="./MasivoDeudaIyC.aspx" class="a-menu" style="width: 100%; display: block; 
    margin-left: 15px; text-decoration:none">
                                <span style="font-size: 16px !important;">Nueva notificacion</span>
                            </a>
                        </li>
                        <li style="margin-top: 20px;">
                            <a href="#" class="a-menu" style="font-size: 18px; margin-left: 15px; font-weight: 700; text-decoration:none">
                                <i class="fa fa-home" style="margin-right: 8px;"></i>
                                <span>Inmueble</span>
                            </a>
                            <div class="container-fluid"
                                style="background: linear-gradient(87deg, rgb(148 23 23) 0%, rgba(255,35,0,1) 41%,
                                    rgb(255 233 0) 79%); margin-top: 5px">
                                <div class="row">
                                    <div class="col-md-12" style="padding-top: 5px;">
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li class="li-menu" style="margin-top: 10px;">
                            <a href="./NotificacionesGeneral.aspx?subsistema=1" class="a-menu" style="width: 100%; display: block; 
     text-decoration:none; margin-left: 15px;">
                                <span style="font-size: 16px !important;">Notificacion Inmueble</span>
                            </a>
                        </li>
                        <li class="li-menu">
                            <a href="./MasivoDeuda.aspx" class="a-menu" style="width: 100%; display: block; 
    margin-left: 15px;  text-decoration:none">
                                <span style="font-size: 16px !important;">Nuevas</span>
                            </a>
                        </li>
                        <li style="margin-top: 20px;">
                            <a href="#" class="a-menu" style="font-size: 18px; margin-left: 15px; font-weight: 700; text-decoration:none">
                                <i class="fa fa-home" style="margin-right: 8px;"></i>
                                <span>General</span>
                            </a>
                            <div class="container-fluid"
                                style="background: linear-gradient(87deg, rgb(148 23 23) 0%, rgba(255,35,0,1) 41%,
                                    rgb(255 233 0) 79%); margin-top: 5px">
                                <div class="row">
                                    <div class="col-md-12" style="padding-top: 5px;">
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li class="li-menu" style="margin-top: 10px;">
                            <a href="./NotificacionesGeneral.aspx?subsistema=8" class="a-menu" style="width: 100%; display: block; 
     text-decoration:none; margin-left: 15px;">
                                <span style="font-size: 16px !important;">Notificaciones generales</span>
                            </a>
                        </li>
                        <li class="li-menu">
                            <a href="./MasivoDeudaGeneral.aspx" class="a-menu" style="width: 100%; display: block; 
    margin-left: 15px;  text-decoration:none">
                                <span style="font-size: 16px !important;">Nuevas Notificaciones</span>
                            </a>
                        </li>
                    </ul>
                </div>
              
                
                    <div class="col-8">
                        <div class="col-12" style="padding: 25px;">
                            <h1 style="font-size: 36px !important; font-weight: 600 !important; margin-bottom: 5px !important; display: flex !important; align-items: start !important;">
                                <span class="fa fa-car-side" style="color: #c09e76; border-right: solid 3px; padding-right: 10px;"></span>
                                <span style="font-size: 20px !important; padding-left: 8px !important; margin-top: 5px !important;  padding-bottom: 8px !important; ">Notificaciones</span>
                            </h1>
                            <h1 style="font-size: 16px !important; font-weight: 500 !important; color: gray !important; margin-left: 65px !important; margin-top: -24px !important;"></h1>
                        </div>
                        <hr style="margin-top: -10px; border: 2px solid #c09e76; margin-bottom: 20px; opacity: 1; width: 100%;" />
                        
                    
                   
                        <div class="">
                            <asp:GridView AutoGenerateColumns="false" CssClass="table" ID="gvMasivosAut" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="Nro_Emision" HeaderText="Emision" />
                                    <asp:BoundField DataField="Fecha_Emision" HeaderText="Fecha" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="Fecha_Vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="Cod_tipo_notificacion" HeaderText="Cod. Notificacion" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:BoundField DataField="Subsistema" HeaderText="Subsistema" />
                                    <asp:BoundField DataField="Cantidad_Reg" HeaderText="Registros" />
                                    <asp:BoundField DataField="Total"  HeaderText="Total" />
                                    <asp:BoundField DataField="Porcentaje"  HeaderText="Porcentaje" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href='DetNotificacionesGeneral.aspx?nro_emision=<%# Eval("Nro_Emision") %>'><span class="fa fa-envelope-circle-check" style="color: darkcyan"></span></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-7"></div>
                    </div>
                    <div class="row">
                        <div class="col-7"></div>
                    </div>
            



            </div>
            
        </div>
    </asp:Content>