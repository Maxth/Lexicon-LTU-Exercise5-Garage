using System.Threading.Channels;
using System.Transactions;

class ConsoleUI : IUI
{
    public UserAction ShowMainMenu(bool garageAlreadyExists = true)
    {
        Log("Welcome to the Garage Management" + "\n1. Create a new garage");
        if (garageAlreadyExists)
        {
            Log(
                "2. Populate garage"
                    + "\n3. Add vehicle"
                    + "\n4. Remove vehicle"
                    + "\n5. List vehicles"
                    + "\n6. List vehicles by category"
                    + "\n7. Search vehicles by property"
                    + "\n8. Find vehicle by registration number"
            );
        }
        Log("0. Exit");

        while (true)
        {
            var input = Console.ReadLine();

            if (!garageAlreadyExists && input != "1" && input != "0")
            {
                Log("Invalid input");
                continue;
            }

            switch (input)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    return UserAction.CreateNewGarage;
                case "2":
                    return UserAction.Populate;
                case "3":
                    return UserAction.Add;
                case "4":
                    return UserAction.Remove;
                case "5":
                    return UserAction.ListAll;
                case "6":
                    return UserAction.ListByCategory;
                case "7":
                    return UserAction.SearchByProp;
                case "8":
                    return UserAction.FindByRegNr;
                default:
                    Log("Invalid input");
                    break;
            }
        }
    }

    public uint AskForUint(string query = "", string successFeedback = "")
    {
        while (true)
        {
            Log(query);
            var input = Console.ReadLine();
            if (uint.TryParse(input, out uint result))
            {
                Log(successFeedback);
                return result;
            }
            else
            {
                Log("You need to enter a positive integer, please try again");
            }
        }
    }

    private void Log(string m)
    {
        System.Console.WriteLine(m);
    }

    public Tuple<string, string, uint, string> AskForVehicleDetails()
    {
        string vehicleType = AskForVehicleType();
        string regNr = AskForRegNr();
        uint wheelCount = AskForWheelCount();
        string color = AskForColor();

        return Tuple.Create(vehicleType, regNr, wheelCount, color);
    }

    private string AskForColor()
    {
        Log("What color is the vehicle?");
        while (true)
        {
            var input = Console.ReadLine();

            if (input != null && input.All(l => Char.IsLetter(l)))
            {
                return input;
            }
            else
            {
                Log("Invalid input");
            }
        }
    }

    private uint AskForWheelCount()
    {
        Log("How many wheels does the vehicle have?");

        while (true)
        {
            var input = Console.ReadLine();

            if (uint.TryParse(input, out uint result) && result < 11)
            {
                return result;
            }
            else
            {
                Log("Invalid input. The vehicle can only have 0 to 10 wheels.");
            }
        }
    }

    public string AskForVehicleType(bool permitAny = false)
    {
        Log(
            "Please choose the type of vehicle:"
                + "\n1. Car"
                + "\n2. Motorcycle"
                + "\n3. Bus"
                + "\n4. Airplane"
                + "\n5. Boat"
                + (permitAny ? "\n6. Any" : "")
        );

        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return "Car";
                case "2":
                    return "Motorcycle";
                case "3":
                    return "Bus";
                case "4":
                    return "Airplane";
                case "5":
                    return "Boat";
                case "6":
                    if (permitAny)
                    {
                        return "Any";
                    }
                    else
                    {
                        Log("Invalid input");
                    }
                    break;
                default:
                    Log("Invalid input");
                    break;
            }
        }
    }

    public string AskForRegNr()
    {
        Log("Please enter the vehicle's registration number");
        while (true)
        {
            var input = Console.ReadLine();

            if (
                input != null
                && input.Length == 6
                && input.Substring(0, 3).All(w => Char.IsLetter(w))
                && input.Substring(3).All(d => Char.IsDigit(d))
            )
            {
                return input;
            }
            else
            {
                Log("Invalid input. Registration number need be in the form ABC123");
            }
        }
    }

    public FuelType AskForFuelType()
    {
        Log("What fuel does the car use?" + "\n1. Gasoline" + "\n2. Diesel");

        while (true)
        {
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    return FuelType.Gasoline;
                case "2":
                    return FuelType.Diesel;
                default:
                    Log("Invalid input");
                    break;
            }
        }
    }

    public double AskForDouble(string query = "", string successFeedback = "")
    {
        while (true)
        {
            Log(query);
            var input = Console.ReadLine();
            if (double.TryParse(input, out double result) && result > 0)
            {
                Log(successFeedback);
                return result;
            }
            else
            {
                Log("You need to enter a number, please try again");
            }
        }
    }

    public string AskForColorToSearchFor()
    {
        Log("What color do you want to search for? Enter \"Any\" to search for any color.");

        while (true)
        {
            var input = Console.ReadLine();

            if (input != null && input.All(l => Char.IsLetter(l)))
            {
                return input;
            }
            else
            {
                Log("Invalid input");
            }
        }
    }

    public uint? AskForWheelCountToSearchFor()
    {
        Log(
            "What is the number of wheels you would like to search for? Enter \"Any\" to search for any number of wheels."
        );

        while (true)
        {
            var input = Console.ReadLine();

            if (uint.TryParse(input, out uint result))
            {
                return result;
            }
            else if (input != null && input.Equals("any", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else
            {
                Log("Invalid input");
            }
        }
    }
}
