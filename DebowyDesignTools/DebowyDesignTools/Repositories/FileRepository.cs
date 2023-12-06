using System.Text.Json;
using DebowyDesignTools.Entities;

namespace DebowyDesignTools.Repositories;

public class FileRepository<T> : IRepository<T>
    where T : class, IEntity, new()
{
    private readonly List<T> _items = new();

    private string fileName;

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;

    public FileRepository(string day)
    {
        fileName = $"{day}.json";
        Load();
    }

    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }

    public T? GetById(int id)
    {
        return _items.SingleOrDefault(item => item.Id == id);
    }

    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
        ItemRemoved?.Invoke(this, item);
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize<IEnumerable<T>>(_items);
        File.WriteAllText(fileName, json);
    }

    public void Load()
    {
        if (File.Exists(fileName))
        {
            var json = File.ReadAllText(fileName);
            var items = JsonSerializer.Deserialize<IEnumerable<T>>(json);

            if (items != null)
            {
                _items.Clear();
                _items.AddRange(items);
            }
        }
    }
}