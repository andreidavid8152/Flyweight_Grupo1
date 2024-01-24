using Flyweight_Grupo1.Data;
using Flyweight_Grupo1.Models;

namespace Flyweight_Grupo1.Flyweight
{
    public class GeneroFlyweightFactory
    {
        private Dictionary<int, Genero> _cache = new Dictionary<int, Genero>();

        public Genero GetGenero(int generoId, ApplicationDbContext context)
        {
            if (!_cache.ContainsKey(generoId))
            {
                var genero = context.Generos.Find(generoId);
                if (genero != null)
                {
                    _cache.Add(generoId, genero);
                }
            }

            return _cache.TryGetValue(generoId, out var cachedGenero) ? cachedGenero : null;
        }

        public bool WasUsedBefore(int id)
        {
            return _cache.ContainsKey(id);
        }

        public int CacheSize
        {
            get { return _cache.Count; }
        }
    }
}
