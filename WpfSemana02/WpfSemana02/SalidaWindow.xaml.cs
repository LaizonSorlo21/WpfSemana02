using System.Windows;
namespace WpfSemana02;
public partial class SalidaWindow : Window
{
    public SalidaWindow() { InitializeComponent(); Actualizar(); }
    private void Actualizar() { var p = Datos.Ingresos.Where(x => !x.FechaSalida.HasValue).ToList(); cboIngreso.ItemsSource = p; cboIngreso.DisplayMemberPath = "Placa"; dgPendientes.ItemsSource = p; }
    private void Ingreso_Changed(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { if (cboIngreso.SelectedItem is Ingreso i) txtPesoSalida.Text = i.Peso.ToString("0.00"); }
    private void Registrar_Click(object sender, RoutedEventArgs e)
    {
        if (cboIngreso.SelectedItem is not Ingreso i) { MessageBox.Show("Seleccione un ingreso pendiente."); return; }
        if (!double.TryParse(txtPesoSalida.Text, out var peso) || peso < 0 || peso > i.Peso) { MessageBox.Show("El peso de salida debe estar entre 0 y el peso de ingreso."); return; }
        i.PesoSalida = peso; i.FechaSalida = DateTime.Now; Datos.Guardar(); MessageBox.Show($"Salida registrada. Peso neto: {i.PesoNeto:N2} kg", "Proceso completado"); Actualizar();
    }
}
