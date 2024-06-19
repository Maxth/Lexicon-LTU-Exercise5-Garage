class Manager
{
    private IUI _cui;
    private IHandler _handler;
    private bool _garageCreated = false;

    public Manager(IUI cui, IHandler handler)
    {
        _cui = cui;
        _handler = handler;
    }

    public void Run()
    {
        while (true)
        {
            UserAction input = _cui.ShowMainMenu(_garageCreated);

            switch (input)
            {
                case UserAction.CreateNewGarage:
                    CreateNewGarage();
                    break;
                case UserAction.ListAll:
                    _handler.ListAllVehicles();
                    break;
                case UserAction.Add:
                    AddVehicle();
                    break;
                case UserAction.Remove:
                    RemoveVehicle();
                    break;
                case UserAction.FindByRegNr:
                    FindByRegNr();
                    break;
                case UserAction.ListByCategory:
                    _handler.ListVehiclesByCategory();
                    break;
                case UserAction.Populate:
                    _handler.PopulateGarage();
                    break;
                case UserAction.SearchByProp:
                    SearchByProp();
                    break;
            }

            //Execute
        }
    }

    private void SearchByProp()
    {
        System.Console.WriteLine(
            "You will now be presented with a number of options to search for.\n"
        );
        string vehicleType = _cui.AskForVehicleType(permitAny: true);
        string color = _cui.AskForColorToSearchFor();
        uint? wheelCount = _cui.AskForWheelCountToSearchFor();
        _handler.SearchByProps(vehicleType, color, wheelCount);
    }

    private void FindByRegNr()
    {
        string regNr = _cui.AskForRegNr();
        _handler.FindByRegNr(regNr);
    }

    private void RemoveVehicle()
    {
        string regNr = _cui.AskForRegNr();
        int slot = _handler.RemoveVehicle(regNr);
        if (slot == -1)
        {
            System.Console.WriteLine("That vehicle could not be found in the garage");
        }
        else
        {
            System.Console.WriteLine($"Successfully removed the vehicle {regNr} from slot {slot}");
        }
    }

    private void AddVehicle()
    {
        Tuple<string, string, uint, string> vehicleDetails = _cui.AskForVehicleDetails();
        int slot;
        switch (vehicleDetails.Item1)
        {
            case "Car":
                FuelType fuelType = _cui.AskForFuelType();
                slot = _handler.AddVehicle(
                    new Car(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        fuelType
                    )
                );
                GiveFeedBack(slot);
                break;
            case "Bus":
                uint seatCount = _cui.AskForUint(query: "How many seats does the bus have?");
                slot = _handler.AddVehicle(
                    new Bus(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        seatCount
                    )
                );
                GiveFeedBack(slot);
                break;
            case "Motorcycle":
                uint topSpeed = _cui.AskForUint(
                    query: "What is the motorcycle's top speed (km/h)?"
                );
                slot = _handler.AddVehicle(
                    new Motorcycle(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        topSpeed
                    )
                );
                GiveFeedBack(slot);
                break;
            case "Airplane":
                double wingSpan = _cui.AskForDouble(query: "What is the airplane's wingspan?");
                slot = _handler.AddVehicle(
                    new Airplane(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        wingSpan
                    )
                );
                GiveFeedBack(slot);
                break;
            case "Boat":
                uint length = _cui.AskForUint("What is boat's length (foot)?");
                slot = _handler.AddVehicle(
                    new Boat(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        length
                    )
                );
                GiveFeedBack(slot);
                break;
            default:
                throw new ArgumentException();
        }

        void GiveFeedBack(int slot)
        {
            if (slot == -2)
            {
                System.Console.WriteLine(
                    "A vehicle with that registration number is already parked in the garage"
                );
            }
            else if (slot == -1)
            {
                System.Console.WriteLine("Failed to park the vehicle because the garage is full.");
            }
            else
            {
                System.Console.WriteLine($"Successfully parked the vehicle in slot {slot}");
            }
        }
    }

    void CreateNewGarage()
    {
        uint capacity = _cui.AskForUint(
            query: "Enter capacity for the new garage:",
            successFeedback: $"A new garage was succesfully created!"
        );
        _handler.Create(capacity);
        _garageCreated = true;
    }
}
