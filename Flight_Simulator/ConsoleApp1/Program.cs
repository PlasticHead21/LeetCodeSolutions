using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello. To start programm you need to add minimum two dispatchers.");
            FlightEventArgs parameters= new FlightEventArgs();
            Plane plane = new Plane();
            while(Plane.CountDispatchers!= 2) { CreateDispatcher(plane); }
            Console.Clear();
            while (!parameters.IsPlaneLanded)
            {
                try
                {
                    Menu();
                    var key = Console.ReadKey(true);
                    Console.Clear();
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (parameters.Speed == 0) { throw new Exception(message: "You cant rise up, without acceleration."); }
                        parameters.Higher(key.Modifiers.HasFlag(ConsoleModifiers.Shift));
                        plane.ReportToDispatcher(parameters);
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (parameters.Speed == 0 && parameters.Hight == 0) { throw new Exception(message: "You realy want to go underground?"); }
                        parameters.Lower(key.Modifiers.HasFlag(ConsoleModifiers.Shift));
                        plane.ReportToDispatcher(parameters);
                    }
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        parameters.Faster(key.Modifiers.HasFlag(ConsoleModifiers.Shift));
                        plane.ReportToDispatcher(parameters);
                    }
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        parameters.Slower(key.Modifiers.HasFlag(ConsoleModifiers.Shift));
                        plane.ReportToDispatcher(parameters);
                    }
                    else if (parameters.Speed == 1000)
                    {
                        parameters.Landing();
                    }
                    else if (key.Key == ConsoleKey.D) { CreateDispatcher(plane); }
                    else if (key.Key == ConsoleKey.Delete) { DeleteDispatcher(plane); }
                    if (parameters.Speed == 0 && parameters.Hight == 0 && !parameters.Start)
                    {
                        parameters.PlaneLanded();
                    }
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch(PlaneCrushException ex)
                {
                    Console.WriteLine($"{ex.Message}\tPress any key.");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                }
                catch(UnsuitableForFlightsException ex)
                {
                    Console.WriteLine($"{ex.Message}\tPress any key.");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"{ex.Message}\tPress any key.");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                }
            }            
            plane.CountAllPenaltiesPoints();
        }
        static void CreateDispatcher(Plane plane)
        {
            string name;
            Console.WriteLine("Enter the name of dispatcher.");
            name = Console.ReadLine();
            Dispatcher d1 = new Dispatcher(plane, name);
        }
        static void DeleteDispatcher(Plane plane)
        {
            string name;
            Console.WriteLine("Enter the name of dispatcher.");
            name = Console.ReadLine();
            plane.DeleteDispatcher(name);
        }
        static void Menu()
        {
            Console.WriteLine("Choose key:");
            Console.WriteLine("[D] - Add dispatcher.");
            Console.WriteLine("[Arrow right] or [Shift arrow right] - Acceleration.");
            Console.WriteLine("[Arrow left] or [Shift arrow left] - Bracking.");
            Console.WriteLine("[Arrow up] or [Shift arrow up] - Takeoff.");
            Console.WriteLine("[Arrow down] or [Shift arrow down] - Landing.");
        }

    }
}
