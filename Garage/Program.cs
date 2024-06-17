// See https://aka.ms/new-console-template for more information

var cui = new ConsoleUI();
var handler = new GarageHandler();
var manager = new Manager(cui, handler);
manager.Run();
