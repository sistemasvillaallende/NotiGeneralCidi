<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plantillas.aspx.cs" 
    Inherits="NotificacionesCIDI.Secure.Plantillas"  Debug="true" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Editor de Texto con Variables</title>
    <link href="https://cdn.quilljs.com/1.3.7/quill.snow.css" rel="stylesheet" />
    <style>
        .ql-variable {
            background-color: #f0f0f0;
            border: 1px dashed #ccc;
            border-radius: 4px;
            color: #000;
            padding: 2px 4px;
            pointer-events: none; /* Evita clics */
            user-select: none; /* Evita selección */
        }

        #editor-container {
            height: 200px;
            border: 1px solid #ccc;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="editor-container" style="height: 200px; border: 1px solid #ccc;"></div>

        <!-- Botones para insertar variables -->
        <button type="button" onclick="insertVariable('{nombre}')">Insertar Nombre</button>
        <button type="button" onclick="insertVariable('{apellido}')">Insertar Apellido</button>
        <button type="button" onclick="insertVariable('{cuit}')">Insertar CUIT</button>

        <!-- Campo oculto para almacenar el contenido -->
        <asp:TextBox ID="hiddenInput2" runat="server" TextMode="MultiLine" Style="display: none;"></asp:TextBox>

        <!-- Botón ASP.NET para procesar el contenido -->
        <asp:Button ID="btnGenerate" runat="server" Text="Generar Notas" OnClick="btnGenerar_Click" />

        <!-- Contenedor para mostrar las notas generadas -->
        <asp:Literal ID="litNotasGeneradas" runat="server"></asp:Literal>
    </form>

    <script src="https://cdn.quilljs.com/1.3.7/quill.min.js"></script>
    <script>
        // Inicializa Quill
        const quill = new Quill('#editor-container', {
            theme: 'snow',
            modules: {
                toolbar: [['bold', 'italic', 'underline'], ['clean']]
            }
        });

        // Función para insertar variables en el editor
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

        // Copia el contenido del editor al campo oculto antes de ejecutar el evento del servidor
        document.getElementById('<%= btnGenerate.ClientID %>').addEventListener('click', function () {
            const content = quill.root.innerHTML; // Obtiene el contenido del editor
            document.getElementById('<%= hiddenInput2.ClientID %>').value = content; // Lo asigna al campo oculto
        });


    </script>
</body>
</html>
