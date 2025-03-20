<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="NotificacionesCIDI.Secure.Error" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Error</title>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"
            integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
            crossorigin="anonymous" />
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="container my-5">
                <div class="alert alert-danger" role="alert">
                    Usted no tiene los permisos para acceder a esta sección.
                </div>
                <p>Comuníquese con el administrador para solicitar acceso.</p>
            </div>
        </form>
    </body>

    </html>