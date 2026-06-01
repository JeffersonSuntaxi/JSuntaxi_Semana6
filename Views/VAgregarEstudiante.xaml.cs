using System.Net;

namespace JSuntaxi_Semana6.Views;

public partial class VAgregarEstudiante : ContentPage
{
	public VAgregarEstudiante()
	{
		InitializeComponent();
	}

    private void BtnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);
            cliente.UploadValues("http://192.168.56.1:8080/ws_estudiante/ws.php", "POST", parametros);
            DisplayAlert("Success", "Estudiante agregado correctamente", "OK");
            Navigation.PushAsync(new VEstudiante());


        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VEstudiante());
    }
}