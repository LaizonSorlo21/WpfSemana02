using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfSemana02
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) { Datos.Cargar(); base.OnStartup(e); }
        protected override void OnExit(ExitEventArgs e) { Datos.Guardar(); base.OnExit(e); }
    }

}
