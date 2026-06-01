using JSuntaxi_Semana6.Modelos;
using System.Net;

namespace JSuntaxi_Semana6.Views;

public partial class VActualizarEliminar : ContentPage
{
	public VActualizarEliminar(Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
	}

    private async void BtnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Evitar Campos Nulos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtEdad.Text))
            {
                await DisplayAlert("Error", "Por favor, llene todos los campos antes de actualizar.", "OK");
                return;
            }

            // Confirmacion de actulizacion de datos
            bool confirmar = await DisplayAlert("Confirmar Actualización", "¿Está seguro de que desea guardar los cambios de este estudiante?", "Sí", "No");

            if (confirmar)
            {
                WebClient cliente = new WebClient();

                // campos para actulizar
                string codigo = txtCodigo.Text;
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string edad = txtEdad.Text;

                string url = $"http://192.168.56.1:8080/ws_estudiante/ws.php?codigo={codigo}&nombre={nombre}&apellido={apellido}&edad={edad}";

                cliente.UploadValues(url, "PUT", new System.Collections.Specialized.NameValueCollection());

                // Confirma de la actulizacion
                await DisplayAlert("Actualizado", "Los datos del estudiante han sido actualizados correctamente.", "OK");

                // Listamos los estudiantes
                await Navigation.PushAsync(new VEstudiante());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void BtnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            //Alerta de confirmación
            bool confirmar = await DisplayAlert("Confirmar", "¿Está seguro de que desea eliminar este estudiante?", "Sí", "No");

            if (confirmar)
            {
                WebClient cliente = new WebClient();

                // URL 
                string codigoEstudiante = txtCodigo.Text;
                string url = $"http://192.168.56.1:8080/ws_estudiante/ws.php?codigo={codigoEstudiante}";

  
                cliente.UploadValues(url, "DELETE", new System.Collections.Specialized.NameValueCollection());

                await DisplayAlert("Success", "Estudiante eliminado correctamente", "OK");

                // Volvemos al listado
                await Navigation.PushAsync(new VEstudiante());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}