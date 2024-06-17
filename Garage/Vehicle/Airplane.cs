class Airplane : Vehicle
{
    public double WingSpan { get; set; }

    public Airplane(string regNr, uint wheelCount, string color, double wingSpan)
        : base(regNr, wheelCount, color)
    {
        WingSpan = wingSpan;
    }
}
