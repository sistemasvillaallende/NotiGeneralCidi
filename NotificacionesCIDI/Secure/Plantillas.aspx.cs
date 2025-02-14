using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotificacionesCIDI.Secure
{
    public partial class Plantillas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            // Recupera el contenido desde el campo oculto
            string editorContent = hiddenInput2.Text;

            // Datos simulados
            var personas = new List<Persona>
    {
        new Persona { Nombre = "Juan", Apellido = "Pérez", Cuit = "20-12345678-9" },
        new Persona { Nombre = "Ana", Apellido = "Gómez", Cuit = "27-98765432-1" }
    };

            // Generar notas reemplazando las variables en el contenido
            var notasGeneradas = personas.Select(persona =>
                editorContent
                    .Replace("{nombre}", persona.Nombre)
                    .Replace("{apellido}", persona.Apellido)
                    .Replace("{cuit}", persona.Cuit)
            ).ToList();

            // Mostrar las notas generadas (puedes ajustar esto según tus necesidades)
            Response.Write("<h3>Notas Generadas:</h3>");
            foreach (var nota in notasGeneradas)
            {
                Response.Write($"<p>{nota}</p>");
            }
        }
    }
}
public class Persona
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Cuit { get; set; }
}