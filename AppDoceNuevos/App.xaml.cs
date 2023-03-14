using AppDoceNuevos.Views;
namespace AppDoceNuevos;


public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new Views.InicioSesion());
    }
}
