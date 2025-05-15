using System.Collections.Generic;

namespace InventoryManagement.Interfaces
{
  public interface IRepository<T> where T : class
  {
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
  }
}
