using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace NotificacionesCIDI.Secure
{
    public partial class Plantillas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillPlantillas();
            }
        }
        protected void btnGenerar_Click(object sender, EventArgs e)
        {

        }

        protected void fillPlantillas()
        {

            var listaNotas = BLL.NotasPlantillasBLL.read();
            GridPlantillas.DataSource = listaNotas;
            GridPlantillas.DataBind();
        }


        protected void GridPlantillas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void GridPlantillas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;
            int id = Convert.ToInt32(e.CommandArgument);

            if (commandName == "Editar")
            {
                var plantilla = BLL.NotasPlantillasBLL.getByPk(id);
                if (plantilla != null)
                {

                    Session["EditPlantillaId"] = id;

                    string contenidoEscapado = HttpUtility.JavaScriptStringEncode(plantilla.contenido);

                    // Registra el script para mostrar el modal y cargar el contenido
                    string script = $@"
                $(document).ready(function() {{
                    $('#plantillaModalEditar').modal('show');
                    setTimeout(function() {{
                        setQuillContent('{contenidoEscapado}');
                    }}, 500); 
                }});
            ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenEditModal", script, true);
                }
            }


            if (commandName == "Eliminar")
            {
                BLL.NotasPlantillasBLL.delete(id);

                fillPlantillas();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#exampleModalDelete').modal('show');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModalExito", @"
                var modal = new bootstrap.Modal(document.getElementById('exampleModalDelete'));
                modal.show();

                document.querySelector('#exampleModalDelete .btn-close').addEventListener('click', function() {
                    modal.hide();
                });
                
                document.querySelector('#exampleModalDelete .btn-secondary').addEventListener('click', function() {
                    modal.hide();
                });
            ", true);
            }
        }

        private void savePlantillas(NotasPlantillas notas)
        {
            int idMax = NotasPlantillasBLL.getMaxId();
            notas.id = idMax + 1;
            NotasPlantillasBLL.insert(notas);

        }

        protected void btnGuardarNota_Click(object sender, EventArgs e)
        {
            string contenido = MyHiddenControl2.Value;
            string nombreNota = MyHiddenControl.Value;
            string contenido2 = hiddenInput2.Text;


            if (string.IsNullOrWhiteSpace(nombreNota))
            {
                nombreNota = "Sin nombre";
            }

            NotasPlantillas notas = new NotasPlantillas();
            notas.nom_plantilla = nombreNota;
            notas.contenido = contenido;
            notas.id_subsistema = 8;

            savePlantillas(notas);
            fillPlantillas();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "VolverAModalPrincipal",
           @"
        $(document).ready(function() {
            $('#plantillaModalNombreNotas').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#plantillaModal').modal('show');
        });
        ", true);
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["EditPlantillaId"]);

            string contenido = hiddenInput3.Text;

            BLL.NotasPlantillasBLL.ActualizarContenido(id, contenido);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModal", "$('#plantillaModalEditar').modal('hide');", true);

                Response.Redirect(Request.RawUrl);

        }
    }
}
