namespace Flyweight_Grupo1.Flyweight
{
    public class LibroFlyweightFactory
    {
        private Dictionary<string, ILibroFlyweight> flyweights = new Dictionary<string, ILibroFlyweight>();

        public ILibroFlyweight GetLibroFlyweight(string clave)
        {
            if (!flyweights.ContainsKey(clave))
            {
                string[] partes = clave.Split('-');
                flyweights.Add(clave, new LibroFlyweight(partes[0], partes[1]));
            }
            return flyweights[clave];
        }
    }
}
