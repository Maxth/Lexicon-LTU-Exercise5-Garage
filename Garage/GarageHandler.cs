class GarageHandler : IHandler
{
    private Garage<IVehicle>? garage;

    public void Create(uint capacity)
    {
        garage = new Garage<IVehicle>(capacity);
    }

    public void ListAllVehicles()
    {
        garage?.PerformOnAll((v) => Console.WriteLine(v));
    }

    public int AddVehicle(IVehicle vehicle)
    {
        if (garage != null)
        {
            return garage.Add(vehicle);
        }
        throw new ArgumentNullException();
    }
}
