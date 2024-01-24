using Flyweight_Grupo1.Data;
using Flyweight_Grupo1.Models;

namespace Flyweight_Grupo1.Flyweight
{
    public class AutorFlyweightFactory
    {
        private Dictionary<int, Autor> _cache = new Dictionary<int, Autor>();

        public Autor GetAutor(int autorId, ApplicationDbContext context)
        {
            if (!_cache.ContainsKey(autorId))
            {
                var autor = context.Autores.Find(autorId);
                if (autor != null)
                {
                    _cache.Add(autorId, autor);
                }
            }

            return _cache.TryGetValue(autorId, out var cachedAutor) ? cachedAutor : null;
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
