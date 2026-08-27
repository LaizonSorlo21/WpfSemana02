using System.Windows;

namespace WpfSemana02;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void Ingresar_Click(object sender, RoutedEventArgs e)
    {
        if (txtUsuario.Text == "kaft" && txtPassword.Password == "1234")
        {
            new MenuWindow().Show();
            Close();
        }
        else
        {
            MessageBox.Show("Usuario o contraseña incorrectos.", "Validación",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
