class Bus : Vehicle
{
    public uint SeatCount { get; set; }

    public Bus(string regNr, uint wheelCount, string color, uint seatCount)
        : base(regNr, wheelCount, color)
    {
        SeatCount = seatCount;
    }
}
