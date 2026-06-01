using JSuntaxi_Semana6.Modelos;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace JSuntaxi_Semana6.Views;

public partial class VEstudiante : ContentPage
{
	private const string URL = "http://192.168.56.1:8080/ws_estudiante/ws.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> _estudiantes;


	public async void Get()
	{
		var content = await cliente.GetStringAsync(URL);
		//desempaquetamos el JSON
		List<Estudiante> objEstudiante = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		_estudiantes= new ObservableCollection<Estudiante>(objEstudiante);
		ListaEstudiantes.ItemsSource = _estudiantes;
    }
    public VEstudiante()
	{
		InitializeComponent();
		Get();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VAgregarEstudiante());
    }

    private void ListaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objetoestudiante = (Estudiante)e.SelectedItem;
        Navigation.PushAsync(new VActualizarEliminar(objetoestudiante));
    }
}