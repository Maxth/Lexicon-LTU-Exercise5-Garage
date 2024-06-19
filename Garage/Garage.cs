using System.Collections;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Garage.Test")]

class Garage<T> : IEnumerable
    where T : IVehicle
{
    private T?[] _storage;

    public Garage(uint capacity)
    {
        _storage = new T[capacity];
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _storage)
        {
            if (item != null)
            {
                yield return item;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

    public T? Find(string regNr) =>
        _storage.FirstOrDefault(v =>
            v?.RegNr.Equals(regNr, StringComparison.OrdinalIgnoreCase) ?? false
        );

    internal void Populate(T[] vehicles)
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            Add(vehicles[i]);
        }
    }

    public IEnumerable<T?> SearchByProps(string vehicleType, string color, uint? wheelCount)
    {
        var firstSelection = vehicleType.Equals("any", StringComparison.OrdinalIgnoreCase)
            ? _storage
            : _storage.Where(v =>
                v != null
                && v.GetType().Name.Equals(vehicleType, StringComparison.OrdinalIgnoreCase)
            );
        var secondSelection = color.Equals("any", StringComparison.OrdinalIgnoreCase)
            ? firstSelection
            : firstSelection.Where(v =>
                v != null && v.Color.Equals(color, StringComparison.OrdinalIgnoreCase)
            );
        var thirdSelection =
            wheelCount == null
                ? secondSelection
                : secondSelection.Where(v => v != null && v.WheelCount == wheelCount);

        return thirdSelection;
    }
}
