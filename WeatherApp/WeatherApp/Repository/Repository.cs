using Microsoft.EntityFrameworkCore;
using WeatherApp.Interfaces;

namespace WeatherApp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly WeatherAppDbContext context;
        protected readonly DbSet<T> dbSet;

        public Repository(WeatherAppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
