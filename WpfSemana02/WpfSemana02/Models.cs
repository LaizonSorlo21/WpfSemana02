using System.Text.Json;
using System.IO;
namespace WpfSemana02;
public class Conductor { public int Id { get; set; } public string Nombre { get; set; } = ""; public string Licencia { get; set; } = ""; public string Transporte { get; set; } = ""; }
public class Transportista { public int Id { get; set; } public string Ruc { get; set; } = ""; public string RazonSocial { get; set; } = ""; public string Telefono { get; set; } = ""; }
public class Camion { public int Id { get; set; } public string Placa { get; set; } = ""; public string Marca { get; set; } = ""; public double CapacidadKg { get; set; } public int TransportistaId { get; set; } public string Transportista { get; set; } = ""; }
public class Producto { public int Id { get; set; } public string Codigo { get; set; } = ""; public string Nombre { get; set; } = ""; public string Unidad { get; set; } = "kg"; }
public class Ingreso { public int Id { get; set; } public string TipoDocumento { get; set; } = ""; public string NumeroDocumento { get; set; } = ""; public string Placa { get; set; } = ""; public string Turno { get; set; } = ""; public string Conductor { get; set; } = ""; public string Cliente { get; set; } = ""; public DateTime FechaHora { get; set; } public string Producto { get; set; } = ""; public double Peso { get; set; } public string Transporte { get; set; } = ""; public DateTime? FechaSalida { get; set; } public double? PesoSalida { get; set; } public double PesoNeto => PesoSalida.HasValue ? Math.Max(0, Peso - PesoSalida.Value) : 0; public string Estado => FechaSalida.HasValue ? "FINALIZADO" : "EN PATIO"; }
public static class Datos
{
 public static List<Conductor> Conductores { get; } = new(); public static List<Transportista> Transportistas { get; } = new(); public static List<Camion> Camiones { get; } = new(); public static List<Producto> Productos { get; } = new(); public static List<Ingreso> Ingresos { get; } = new();
 private static readonly string Archivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WpfSemana02", "datos.json");
 public static int SiguienteId<T>(IEnumerable<T> x, Func<T,int> id) => x.Any() ? x.Max(id) + 1 : 1;
 public static void Cargar() { try { if (!File.Exists(Archivo)) { Iniciales(); return; } var d=JsonSerializer.Deserialize<Almacen>(File.ReadAllText(Archivo)); if(d is null){Iniciales();return;} Conductores.AddRange(d.Conductores);Transportistas.AddRange(d.Transportistas);Camiones.AddRange(d.Camiones);Productos.AddRange(d.Productos);Ingresos.AddRange(d.Ingresos); } catch { Iniciales(); } }
 public static void Guardar() { Directory.CreateDirectory(Path.GetDirectoryName(Archivo)!); File.WriteAllText(Archivo,JsonSerializer.Serialize(new Almacen{Conductores=Conductores,Transportistas=Transportistas,Camiones=Camiones,Productos=Productos,Ingresos=Ingresos},new JsonSerializerOptions{WriteIndented=true})); }
 private static void Iniciales(){if(Transportistas.Count==0)Transportistas.Add(new(){Id=1,Ruc="20123456789",RazonSocial="Transportes Demo S.A.C.",Telefono="987654321"});if(Productos.Count==0)Productos.Add(new(){Id=1,Codigo="PRD-001",Nombre="Carga general",Unidad="kg"});}
 private sealed class Almacen { public List<Conductor> Conductores{get;set;}=new();public List<Transportista> Transportistas{get;set;}=new();public List<Camion> Camiones{get;set;}=new();public List<Producto> Productos{get;set;}=new();public List<Ingreso> Ingresos{get;set;}=new(); }
}
