abstract class Vehicle : IVehicle
{
    public string RegNr { get; set; }
    public uint WheelCount { get; set; }
    public string Color { get; set; }

    public Vehicle(string regNr, uint wheelCount, string color)
    {
        RegNr = regNr;
        WheelCount = wheelCount;
        Color = color;
    }

    public override string ToString()
    {
        return $"Type: {this.GetType().Name}; RegNr: {RegNr}; Number of wheels: {WheelCount}; Color: {Color}";
    }
}
