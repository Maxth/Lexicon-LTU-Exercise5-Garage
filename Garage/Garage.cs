using System.Collections;

class Garage<T> : IEnumerable
    where T : IVehicle
{
    private T?[] _storage;

    public Garage(uint capacity)
    {
        _storage = new T[capacity];
    }

    public IEnumerator GetEnumerator()
    {
        foreach (var item in _storage)
        {
            yield return item;
        }
    }

    public void PerformOnAll(Action<T> action)
    {
        foreach (var item in _storage)
        {
            if (item != null)
            {
                action?.Invoke(item);
            }
        }
    }

    //Returns the slot where vehicle was parked, and -1 if garage is full
    public int Add(T vehicle) => _storage.Push(vehicle);

    public int Remove(string regNr)
    {
        int idxToRemove = Array.IndexOf(
            _storage.Select(v => v?.RegNr.ToUpper()).ToArray(),
            regNr.ToUpper()
        );

        if (idxToRemove != -1)
        {
            _storage[idxToRemove] = default(T);
        }

        return idxToRemove;
    }
}
