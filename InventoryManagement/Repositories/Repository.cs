using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repositories
{
  public class Repository<T> : IRepository<T> where T : class
  {
    protected readonly InventoryContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(InventoryContext context)
    {
      _context = context;
      _dbSet = _context.Set<T>();
    }

    // Синхронний метод для отримання всіх елементів
    public IEnumerable<T> GetAll() => _dbSet.ToList();  // Використовуємо ToList() для синхронного отримання всіх елементів

    // Синхронний метод для отримання елемента за ID
    public T GetById(int id) => _dbSet.Find(id);  // Використовуємо Find() для отримання елемента за ID

    // Синхронний метод для додавання елемента
    public void Add(T entity)
    {
      _dbSet.Add(entity);
      _context.SaveChanges();
    }

    // Синхронний метод для оновлення елемента
    public void Update(T entity)
    {
      _dbSet.Update(entity);
      _context.SaveChanges();
    }

    // Синхронний метод для видалення елемента
    public void Delete(T entity)
    {
      _dbSet.Remove(entity);
      _context.SaveChanges();
    }
  }
}
