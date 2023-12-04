using ConsoleTools;
using DEAIC6_HFT_2023241.Models;
using DEAIC6_HFT_2023241.Client;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Linq;

namespace DEAIC6_HFT_2023241.Client
{
    public class Program
    {
        static RestService rest;
        static void Main(string[] args)
        {
            //http://localhost:27408/index.html
            //swagger
            rest = new RestService("http://localhost:27408/", "rover");

            var planetSubMenu = new ConsoleMenu(args, level: 1)
                 .Add("List", () => VisitedPlaceList("VisitedPlace"))
                 .Add("Create", () => VisitedPlaceCreate("VisitedPlace"))
                 .Add("Delete", () => VisitedPlaceDelete("VisitedPlace"))
                 .Add("Update", () => VisitedPlaceUpdate("VisitedPlace"))
                 .Add("Exit", ConsoleMenu.Close);

            var builderSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => RoverBuilderList("RoverBuilder"))
                .Add("Create", () => RoverBuilderCreate("RoverBuilder"))
                .Add("Delete", () => RoverBuilderDelete("RoverBuilder"))
                .Add("Update", () => RoverBuilderUpdate("RoverBuilder"))
                .Add("BuildersOrderedByDistance" , () => BuilderDistance("RoverBuilder"))
                .Add("Planet with most builders", () => BuilderWithMostVisitedPlaces("RoverBuilder"))
                .Add("Exit", ConsoleMenu.Close);


            var roverSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => RoverList("Rover"))
                .Add("Create", () => RoverCreate("Rover"))
                .Add("Delete", () => RoverDelete("Rover"))
                .Add("Update", () => RoverUpdate("Rover"))
                .Add("Builder With Most Rovers", () => BuilderRovers("Rover"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
             .Add("Rovers", () => roverSubMenu.Show())
             .Add("RoverBuilders", () => builderSubMenu.Show())
             .Add("VisitedPlaces", () => planetSubMenu.Show())
             .Add("Exit", ConsoleMenu.Close);


            menu.Show();
        }

        static void RoverBuilderCreate(string entity)
        {
            try
            {
                if (entity == "RoverBuilder")
                {
                    Console.WriteLine("Enter Builder Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Builder Id: ");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter Planet Id: ");
                    string planetid = Console.ReadLine();
                    rest.Post(new RoverBuilder() { BuilderId = int.Parse(id), BuilderName = name, VisitedPlaceId = int.Parse(planetid) }, "roverbuilder");
                }
            }
            catch(Exception e)
            {
                ExceptionHandler();
            }
            Console.ReadLine();
        }

        static void RoverBuilderList(string entity)
        {
            try
            {
                if (entity == "RoverBuilder")
                {
                    List<RoverBuilder> builders = rest.Get<RoverBuilder>("roverbuilder");
                    foreach (var element in builders)
                    {
                        Console.WriteLine(element.BuilderName);
                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler();
            }
            Console.ReadLine();
        }

        static void RoverBuilderUpdate(string entity)
        {
            try
            {
                if (entity == "RoverBuilder")
                {
                    Console.Write("Enter Builders Id To Update: ");
                    int id = int.Parse(Console.ReadLine());
                    RoverBuilder one = rest.Get<RoverBuilder>(id, "roverbuilder");
                    Console.Write("New name [Old Name: " + one.BuilderName + "]: ");
                    string name = Console.ReadLine();
                    one.BuilderName = name;
                    rest.Put<RoverBuilder>(one, "roverbuilder");
                }
            }
            catch (Exception)
            {
                ExceptionHandler();
            }
            Console.ReadLine();
        }

        static void RoverBuilderDelete(string entity)
        {
            try
            {
                if (entity == "RoverBuilder")
                {
                    Console.Write("Enter Builders Id To Delete: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "roverbuilder");
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler();
            }
            Console.ReadLine();
        }

        static void RoverCreate(string entity)
        {
            try
            {
                if (entity == "Rover")
                {
                    Console.WriteLine("Enter Rover Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Rover Id: ");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter Launch Date Id: ");
                    string launchDate = Console.ReadLine();
                    Console.WriteLine("Enter Land Date Id: ");
                    string landDate = Console.ReadLine();
                    Console.WriteLine("Enter Builder Id: ");
                    string builderid = Console.ReadLine();
                    rest.Post(new Rover()
                    {
                        RoverId = int.Parse(id),
                        RoverName = name,
                        LaunchDate = DateTime.Parse(landDate),
                        LandDate = DateTime.Parse(landDate),
                        BuilderId = int.Parse(builderid)
                    }, "rover");
                }
            }
            catch (Exception)
            {
                ExceptionHandler();
            }
            Console.ReadLine();
        }

        static void RoverList(string entity)
        {
            if (entity == "Rover")
            {
                List<Rover> rovers = rest.Get<Rover>("rover");
                foreach (var element in rovers)
                {
                    Console.WriteLine(element.RoverId + ": " + element.RoverName);
                }
            }
            Console.ReadLine();
        }

        static void RoverUpdate(string entity)
        {
            try
            {
                if (entity == "Rover")
                {
                    Console.Write("Enter Rover Id To Update: ");
                    int id = int.Parse(Console.ReadLine());
                    Rover one = rest.Get<Rover>(id, "rover");
                    Console.Write("New name [Old Name: " + one.RoverName + "]: ");
                    string name = Console.ReadLine();
                    one.RoverName = name;
                    rest.Put<Rover>(one, "rover");
                }
            }
            catch (Exception)
            {
                ExceptionHandler();
            }
            Console.ReadLine();
        }

        static void RoverDelete(string entity)
        {
            try
            {
                if (entity == "Rover")
                {
                    Console.Write("Enter Rover Id To Delete: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "rover");
                }
            }
            catch(Exception) { ExceptionHandler(); }
            Console.ReadLine();
        }

        static void VisitedPlaceCreate(string entity)
        {
            try
            {
                if (entity == "VisitedPlace")
                {
                    Console.WriteLine("Enter Planet Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Planet Id: ");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter Planet Type: ");
                    string type = Console.ReadLine();
                    Console.WriteLine("Enter Distance of Planet: ");
                    string distance = Console.ReadLine();
                    rest.Post(new VisitedPlaces()
                    {
                        PlaceId = int.Parse(id),
                        PlanetName = name,
                        PlanetType = type,
                        Distance = int.Parse(distance)
                    }, "visitedplaces");
                }
            }
            catch (Exception) { ExceptionHandler(); }
            Console.ReadLine();
        }

        static void VisitedPlaceList(string entity)
        {
            if (entity == "VisitedPlace")
            {
                List<VisitedPlaces> planets = rest.Get<VisitedPlaces>("visitedplaces");
                foreach (var element in planets)
                {
                    Console.WriteLine(element.PlaceId + ": " + element.PlanetName + " - " + element.PlanetType + " : " + element.Distance);
                }
            }
            Console.ReadLine();
        }

        static void VisitedPlaceUpdate(string entity)
        {
            try
            {
                if (entity == "VisitedPlace")
                {
                    Console.Write("Enter VisitedPlace Id To Update: ");
                    int id = int.Parse(Console.ReadLine());
                    VisitedPlaces one = rest.Get<VisitedPlaces>(id, "visitedplaces");
                    Console.Write("New name [Old Name: " + one.PlanetName + "]: ");
                    string name = Console.ReadLine();
                    one.PlanetName = name;
                    rest.Put<VisitedPlaces>(one, "visitedplaces");
                }
            }
            catch(Exception) { ExceptionHandler(); }
            Console.ReadLine();
        }

        static void VisitedPlaceDelete(string entity)
        {
            try 
            {
                if (entity == "VisitedPlace")
                {
                    Console.Write("Enter Planet Id To Delete: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "VisitedPlaces");
                } 
            }
            catch(Exception) { ExceptionHandler(); }
            Console.ReadLine();
        }

        static void BuilderDistance(string entity)
        {
            //id == 0 distance == 0
            if(entity == "RoverBuilder")
            {
                var builderdistance = rest.Get<RoverBuilded>("roverbuilder");
                foreach(var builders in builderdistance)
                {
                    Console.WriteLine("Builder Id: " + builders.Id + " distance traveled to planets by builder: " + builders.Distance);
                }
            }
            Console.ReadLine();
        }

        static void BuilderWithMostVisitedPlaces(string entity)
        {
            //cannot deserialize json array
            if(entity == "RoverBuilder")
            {
                var builderDistant = rest.GetSingle<MaxBuilderNumber>("roverbuilder");
                Console.WriteLine("Builder with the most distant place visited: " + builderDistant.Name + " " + builderDistant.BuilderNumber);
            }
        }

        static void BuilderRovers(string entity)
        {
            if(entity == "Rover")
            {
                var builders = rest.Get<RoverNumber>("rover");
                foreach (var builder in builders)
                {
                    Console.WriteLine("Builder Id: " + builder.BuilderId + " rover count: " + builder.Number);
                }
            }
            Console.ReadLine();
        }

        static void ExceptionHandler()
        {
            Console.WriteLine("Something went wrong!");
        }
    }
}
