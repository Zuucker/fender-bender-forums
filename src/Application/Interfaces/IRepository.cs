namespace Application.Interfaces
{
    public interface IRepository<T,Tkey>
    {
        T Add(T entity);
        T? GetById(Tkey id);
        IEnumerable<T> GetAll();
        T Update(T entity);
        void Delete(T entity);
    }
}
