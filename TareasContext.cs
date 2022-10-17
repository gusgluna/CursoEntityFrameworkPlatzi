using Microsoft.EntityFrameworkCore;
using proyectoef.Models;
namespace proyectoef;
public class TareasContext : DbContext
{
  public DbSet<Categoria> Categorias { get; set; }
  public DbSet<Tarea> Tareas { get; set; }
  public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    List<Categoria> categoriasInit = new List<Categoria>();

    categoriasInit.Add(new Categoria()
    {
      CategoriaId = Guid.Parse("8132011f-01aa-401a-8ea6-c36e693c8977"),
      Nombre = "Actividades Pendientes",
      Peso = 20
    });

    categoriasInit.Add(new Categoria()
    {
      CategoriaId = Guid.Parse("8132011f-01aa-401a-8ea6-c36e693c8910"),
      Nombre = "Actividades Personales",
      Peso = 50
    });

    modelBuilder.Entity<Categoria>(catergoria =>
    {
      catergoria.ToTable("Categoria");
      catergoria.HasKey(p => p.CategoriaId);
      catergoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
      catergoria.Property(p => p.Description);
      catergoria.Property(p => p.Peso);
      catergoria.HasData(categoriasInit);
    });

    List<Tarea> tareasInit = new List<Tarea>();
    tareasInit.Add(new Tarea()
    {
      TareaId = Guid.Parse("8132011f-01aa-401a-8ea6-c36e693c8900"),
      CategoriaId = Guid.Parse("8132011f-01aa-401a-8ea6-c36e693c8977"),
      PrioridadTarea = Prioridad.Media,
      Titulo = "Pago de Servicios Publicos",
      FechaCreacion = DateTime.Now
    });

    tareasInit.Add(new Tarea()
    {
      TareaId = Guid.Parse("8132011f-01aa-401a-8ea6-c36e693c8901"),
      CategoriaId = Guid.Parse("8132011f-01aa-401a-8ea6-c36e693c8910"),
      PrioridadTarea = Prioridad.Baja,
      Titulo = "Terminar de ver Pelicula en Netflix",
      FechaCreacion = DateTime.Now
    });

    modelBuilder.Entity<Tarea>(tarea =>
    {
      tarea.ToTable("Tarea");
      tarea.HasKey(p => p.TareaId);
      tarea.HasOne(p => p.Categoria)
            .WithMany(p => p.Tareas)
            .HasForeignKey(p => p.CategoriaId);
      tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
      tarea.Property(p => p.Descripcion);
      tarea.Property(p => p.PrioridadTarea);
      tarea.Property(p => p.FechaCreacion);
      tarea.Ignore(p => p.Resumen);

      tarea.HasData(tareasInit);
    });
  }
}