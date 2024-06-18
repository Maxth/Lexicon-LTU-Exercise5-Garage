class GarageHandler : IHandler
{
    private Garage<IVehicle>? garage;
    private bool hasPopulated = false;

    public void Create(uint capacity)
    {
        garage = new Garage<IVehicle>(capacity);
        hasPopulated = false;
    }

    public void ListAllVehicles()
    {
        garage?.PerformOnAll((v) => Console.WriteLine(v));
    }

    public int AddVehicle(IVehicle vehicle)
    {
        if (garage != null)
        {
            var alreadyExistsCheck = garage.Find(vehicle.RegNr);
            if (alreadyExistsCheck == null)
            {
                return garage.Add(vehicle);
            }
            else
            {
                return -2;
            }
        }
        throw new ArgumentNullException();
    }

    public int RemoveVehicle(string regNr)
    {
        if (garage != null)
        {
            return garage.Remove(regNr);
        }
        throw new ArgumentNullException();
    }

    public void FindByRegNr(string regNr)
    {
        if (garage != null)
        {
            var vehicle = garage.Find(regNr);
            if (vehicle == null)
            {
                System.Console.WriteLine(
                    $"Could not find a vehicle with registration number {regNr}"
                );
            }
            else
            {
                System.Console.WriteLine(vehicle.ToString());
            }
        }
        else
        {
            throw new ArgumentNullException();
        }
    }

    public void ListVehiclesByCategory()
    {
        if (garage != null)
        {
            var enumerator = garage.GetEnumerator();
            int cars = 0;
            int boats = 0;
            int airplanes = 0;
            int motorcycles = 0;
            int buses = 0;
            while (enumerator.MoveNext())
            {
                switch (enumerator.Current.GetType().Name)
                {
                    case "Car":
                        cars++;
                        break;
                    case "Bus":
                        buses++;
                        break;
                    case "Airplane":
                        airplanes++;
                        break;
                    case "Motorcycle":
                        motorcycles++;
                        break;
                    case "Boat":
                        boats++;
                        break;

                    default:
                        break;
                }
            }
            System.Console.WriteLine(
                $"Cars: {cars}; Buses: {buses}; Motorcycles: {motorcycles}; Boats: {boats}; Airplanes: {airplanes}"
            );
        }
        else
        {
            throw new ArgumentNullException();
        }
    }

    public void PopulateGarage()
    {
        if (garage != null)
        {
            if (hasPopulated)
            {
                System.Console.WriteLine("The garage has already been populated");
                return;
            }
            IVehicle[] vehicles =
            {
                new Car("abc123", 4, "red", FuelType.Gasoline),
                new Car("edfd123", 4, "blue", FuelType.Gasoline),
                new Car("bhg123", 4, "green", FuelType.Diesel),
                new Bus("thr432", 8, "red", 40),
                new Motorcycle("lkj543", 2, "black", 200),
                new Boat("oky543", 0, "white", 20),
                new Airplane("lkg966", 3, "white", 30.5)
            };
            garage.Populate(vehicles);
            hasPopulated = true;
            System.Console.WriteLine("Garage successfully populated");
        }
        else
        {
            throw new ArgumentNullException();
        }
    }

    public void SearchByProps(string vehicleType, string color, uint? wheelCount)
    {
        if (garage != null)
        {
            var targetVehicles = garage.SearchByProps(vehicleType, color, wheelCount);

            if (!targetVehicles.Any())
            {
                System.Console.WriteLine("No vehicles found matching those descriptions");
            }
            else
            {
                foreach (var item in targetVehicles)
                {
                    if (item != null)
                    {
                        System.Console.WriteLine(item.ToString());
                    }
                }
            }
        }
        else
        {
            throw new ArgumentNullException();
        }
    }
}
