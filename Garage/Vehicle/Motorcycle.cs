class Motorcycle : Vehicle
{
    public uint TopSpeed { get; set; }

    public Motorcycle(string regNr, uint wheelCount, string color, uint topSpeed)
        : base(regNr, wheelCount, color)
    {
        TopSpeed = topSpeed;
    }
}
