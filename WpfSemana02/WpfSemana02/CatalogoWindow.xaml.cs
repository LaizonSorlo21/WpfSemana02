using System.Text.RegularExpressions;
using System.Windows;
namespace WpfSemana02;
public partial class CatalogoWindow : Window
{
    private readonly string tipo;
    public CatalogoWindow(string tipo)
    {
        InitializeComponent(); this.tipo = tipo; txtTitulo.Text = $"MANTENIMIENTO DE {tipo.ToUpper()}";
        if (tipo == "Transportistas") { lbl1.Text = "RUC"; lbl2.Text = "Razón social"; lbl3.Text = "Teléfono"; }
        else if (tipo == "Camiones") { lbl1.Text = "Placa"; lbl2.Text = "Marca"; lbl3.Text = "Capacidad kg y transportista"; lista3.Visibility = Visibility.Visible; lista3.ItemsSource = Datos.Transportistas; lista3.DisplayMemberPath = "RazonSocial"; }
        else { lbl1.Text = "Código"; lbl2.Text = "Nombre"; lbl3.Text = "Unidad"; campo3.Text = "kg"; }
        Actualizar();
    }
    private void Guardar_Click(object sender, RoutedEventArgs e)
    {
        var a = campo1.Text.Trim().ToUpper(); var b = campo2.Text.Trim(); var c = campo3.Text.Trim();
        if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b)) { Aviso("Complete los campos obligatorios."); return; }
        if (tipo == "Transportistas")
        {
            if (!Regex.IsMatch(a, @"^\d{11}$")) { Aviso("El RUC debe tener 11 dígitos."); return; }
            if (!Regex.IsMatch(c, @"^\d{7,9}$")) { Aviso("Ingrese un teléfono válido de 7 a 9 dígitos."); return; }
            if (Datos.Transportistas.Any(x => x.Ruc == a)) { Aviso("El RUC ya está registrado."); return; }
            Datos.Transportistas.Add(new() { Id = Datos.SiguienteId(Datos.Transportistas, x => x.Id), Ruc = a, RazonSocial = b, Telefono = c });
        }
        else if (tipo == "Camiones")
        {
            if (!Regex.IsMatch(a, @"^[A-Z]{3}-\d{3}$")) { Aviso("La placa debe tener el formato ABC-123."); return; }
            if (lista3.SelectedItem is not Transportista t) { Aviso("Seleccione un transportista."); return; }
            if (!double.TryParse(c, out var capacidad) || capacidad <= 0) { Aviso("Ingrese una capacidad mayor que cero."); return; }
            if (Datos.Camiones.Any(x => x.Placa == a)) { Aviso("La placa ya está registrada."); return; }
            Datos.Camiones.Add(new() { Id = Datos.SiguienteId(Datos.Camiones, x => x.Id), Placa = a, Marca = b, CapacidadKg = capacidad, TransportistaId = t.Id, Transportista = t.RazonSocial });
        }
        else
        {
            if (string.IsNullOrWhiteSpace(c)) { Aviso("Ingrese la unidad de medida."); return; }
            if (Datos.Productos.Any(x => x.Codigo.Equals(a, StringComparison.OrdinalIgnoreCase))) { Aviso("El código ya está registrado."); return; }
            Datos.Productos.Add(new() { Id = Datos.SiguienteId(Datos.Productos, x => x.Id), Codigo = a, Nombre = b, Unidad = c });
        }
        campo1.Clear(); campo2.Clear(); if (tipo != "Camiones" && tipo != "Productos") campo3.Clear(); Datos.Guardar(); Actualizar();
    }
    private void Eliminar_DobleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (grilla.SelectedItem is null || MessageBox.Show("¿Eliminar el registro seleccionado?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
        if (grilla.SelectedItem is Transportista t && Datos.Camiones.Any(x => x.TransportistaId == t.Id)) { Aviso("No se puede eliminar: tiene camiones asociados."); return; }
        if (grilla.SelectedItem is Transportista t1) Datos.Transportistas.Remove(t1); else if (grilla.SelectedItem is Camion c) Datos.Camiones.Remove(c); else if (grilla.SelectedItem is Producto p) Datos.Productos.Remove(p);
        Datos.Guardar(); Actualizar();
    }
    private void Actualizar() { grilla.ItemsSource = null; grilla.ItemsSource = tipo == "Transportistas" ? Datos.Transportistas : tipo == "Camiones" ? Datos.Camiones : Datos.Productos; }
    private static void Aviso(string m) => MessageBox.Show(m, "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
}
