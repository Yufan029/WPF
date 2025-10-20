namespace WeatherApp.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(int id);
        void Save();
    }
}
