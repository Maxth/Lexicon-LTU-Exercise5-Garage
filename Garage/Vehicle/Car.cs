public enum FuelType
{
    Diesel,
    Gasoline
}

class Car : Vehicle
{
    public FuelType FuelType { get; set; }

    public Car(string regNr, uint wheelCount, string color, FuelType fuelType)
        : base(regNr, wheelCount, color)
    {
        FuelType = fuelType;
    }
}
