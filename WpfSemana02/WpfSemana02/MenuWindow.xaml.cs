using System.Windows;
namespace WpfSemana02;
public partial class MenuWindow : Window
{
    public MenuWindow() => InitializeComponent();
    private void Ingresos_Click(object sender, RoutedEventArgs e) => new IngresoWindow { Owner = this }.ShowDialog();
    private void Salida_Click(object sender, RoutedEventArgs e) => new SalidaWindow { Owner = this }.ShowDialog();
    private void Conductores_Click(object sender, RoutedEventArgs e) => new ConductoresWindow { Owner = this }.ShowDialog();
    private void Reporte_Click(object sender, RoutedEventArgs e) => new ReporteIngresosWindow { Owner = this }.ShowDialog();
    private void Catalogo_Click(object sender, RoutedEventArgs e) { if (sender is FrameworkElement { Tag: string tipo }) new CatalogoWindow(tipo) { Owner = this }.ShowDialog(); }
}
