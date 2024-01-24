namespace Flyweight_Grupo1.Flyweight
{
    public class LibroFlyweight : ILibroFlyweight
    {
        private string autor;
        private string genero; // Datos compartidos

        public LibroFlyweight(string autor, string genero)
        {
            this.autor = autor;
            this.genero = genero;
        }

        public void MostrarDatos(string titulo)
        {
            Console.WriteLine($"Libro: {titulo}, Autor: {autor}, Género: {genero}");
        }
    }
}
