interface IHandler
{
    int AddVehicle(IVehicle vehicle);
    void Create(uint capacity);

    void ListAllVehicles();
    int RemoveVehicle(string regNr);

    void FindByRegNr(string regNr);

    void ListVehiclesByCategory();
    void PopulateGarage();
    void SearchByProps(string vehicleType, string color, uint? wheelCount);
}
