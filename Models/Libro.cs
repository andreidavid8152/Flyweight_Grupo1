namespace Flyweight_Grupo1.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int GeneroId { get; set; }

        public virtual Autor Autor { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
