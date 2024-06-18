namespace Garage.Test;

public class GarageTest
{
    private Garage<IVehicle> garage;
    private Car testCar1 = new Car("lkj123", 4, "red", FuelType.Gasoline);
    private Car testCar2 = new Car("abc123", 4, "red", FuelType.Gasoline);

    public GarageTest()
    {
        //Arrange
        this.garage = new Garage<IVehicle>(10);
    }

    [Fact]
    public void Add_Method_Puts_Vehicle_In_Correct_Slot()
    {
        //Act
        int slot1 = garage.Add(testCar1);
        int slot2 = garage.Add(testCar2);
        var enumerator = garage.GetEnumerator();
        int carCount = 0;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current != null)
            {
                carCount++;
            }
        }
        //Assert
        Assert.Equal(0, slot1);
        Assert.Equal(1, slot2);
        Assert.Equal(2, carCount);
    }

    [Fact]
    public void Remove_Method_Returns_And_Removes_The_Correct_Slot_And_If_Not_Found_Returns_Neg1()
    {
        //Arrange
        garage.Add(testCar1);
        garage.Add(testCar2);

        //Act
        int slot0 = garage.Remove(testCar1.RegNr);
        int slot1 = garage.Remove(testCar2.RegNr);
        int slotempty = garage.Remove("ölk654");
        var enumerator = garage.GetEnumerator();

        int carCount = 0;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current != null)
            {
                carCount++;
            }
        }

        //Assert
        Assert.Equal(0, slot0);
        Assert.Equal(1, slot1);
        Assert.Equal(-1, slotempty);
        Assert.Equal(0, carCount);
    }

    [Fact]
    public void Find_Method_Returns_Correct_Vehicle_And_Null_If_Not_Found()
    {
        //Arrange
        garage.Add(testCar1);

        //Act
        var foundVehicle1 = garage.Find(testCar1.RegNr);
        var foundVehicle2 = garage.Find("aslkmlsad");

        //Assert
        Assert.Equal(foundVehicle1?.RegNr, testCar1.RegNr);
        Assert.Null(foundVehicle2);
    }
}
