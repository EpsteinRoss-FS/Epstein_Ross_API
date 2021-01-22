using System;
using System.Collections.Generic;
using System.Text;

namespace Epstein_Ross_API
{
    class Application
    {


        public static APIConnect _api;
        public static List<dynamic> shipList = new List<dynamic>();

        public static bool hasQuit = false;
        public Application()
        {
            _api = new APIConnect();
            Startup();
            while (!hasQuit)
            {
                if (!hasQuit)
                {
                    MainLoop();
                }
            }
            Console.WriteLine("You have executed Order 66.  Have a great day!");


        }

        public static void Startup()
        {
            var shipReturned = _api.GetAllShips();
            shipList.Add(shipReturned);
        }
        
        

        public static void MainLoop() 
        {
            DisplayHeader("Star Wars ship Databse");
           
            //menu functionality
            int menuLength = shipList[0].Count - 1;
            int i = 1;

            foreach (var item in shipList[0])
            {
                Console.WriteLine($"[{i}]:  {item.name}");
                i++;
            }

            Console.Write("Please make a selection, or press 66 to exit! >  ");
            string _userChoice = Console.ReadLine();

            //validate the choice is an integer
            bool isInt = Validation.CheckInt(_userChoice);
            int _userChoiceInt = isInt ? Int32.Parse(_userChoice) : 000;
            
            if(_userChoiceInt == 66) 
            {
                hasQuit = true;
                return;
            }

            //validate the choice is in range of the menu
            bool isInRange = Validation.CheckRange(_userChoiceInt, menuLength + 1);

            //ask again if the validation returns false
            while (!isInt || !isInRange)
            {
                if (_userChoiceInt == 66)
                {
                    hasQuit = true;
                    return;
                }
                i = 1;
                DisplayHeader("Star Wars Databse");
                
                
                foreach (var item in shipList[0])
                {
                    Console.WriteLine($"[{i}]:  {item.name}");
                    i++;
                }

                //error if menu selection out of range    
                Console.Write($"Invalid entry!  Please enter a number between 1 and {menuLength} or 66 to exit! > ");
                _userChoice = Console.ReadLine();
                isInt = Validation.CheckInt(_userChoice);
                _userChoiceInt = isInt ? Int32.Parse(_userChoice) : 000; ;
                isInRange = Validation.CheckRange(_userChoiceInt, (menuLength + 1));
            }

            

            dynamic chosenItem = shipList[0][_userChoiceInt - 1];
            DisplayShipInfo(chosenItem);
        }

        //header method
        public static void DisplayHeader(string headerName)
        {
            Console.Clear();
            Console.WriteLine(headerName.ToUpper());
            Console.WriteLine("===================================================");
        }

        public static void DisplayShipInfo(dynamic chosenItem) 
        {
            List<dynamic> pilotsList = new List<dynamic>();
            dynamic shipInfo = APIConnect.GetShip(chosenItem);

            if (shipInfo.pilots.Count > 0)
            {
                

                foreach (dynamic pilot in shipInfo.pilots)
                {
                    pilotsList.Add(APIConnect.GetPilot(pilot.ToString()));
                }
            }
                DisplayHeader(shipInfo.model.ToString());

            Console.WriteLine($"Ship Name: {shipInfo.name.ToString()}");
            Console.WriteLine($"Ship Model: {shipInfo.model.ToString()}");
            Console.WriteLine($"Ship Class: {shipInfo.starship_class.ToString()}");
            Console.WriteLine($"Ship Manufacturer: {shipInfo.manufacturer.ToString()}");
            Console.WriteLine($"Hyperdrive Rating: {shipInfo.hyperdrive_rating.ToString()}");
            Console.WriteLine($"Crew: {shipInfo.crew.ToString()}");
            Console.WriteLine($"Passengers: {shipInfo.passengers.ToString()}");


            
            if (shipInfo.pilots.Count > 0)
            {
                Console.WriteLine("\nKNOWN PILOTS:");
                

                int pilotListCount = pilotsList.Count - 1;
                int i = 1;
                foreach (var item in pilotsList)
                {
                    Console.WriteLine($"[{i}]:  {item.name}");
                    i++;
                }
                Console.Write("Please make a selection, or press 66 to return to the menu. >  ");
                string _userChoice = Console.ReadLine();

                bool isInt = Validation.CheckInt(_userChoice);
                int _userChoiceInt = isInt ? Int32.Parse(_userChoice) : 000;

                if (_userChoiceInt == 66)
                {
                    MainLoop();
                }

                //validate the choice is in range of the menu
                bool isInRange = Validation.CheckRange(_userChoiceInt, pilotListCount + 1);

                while (!isInt || !isInRange)
                {
                    if (_userChoiceInt == 66)
                    {
                        MainLoop();
                    }

                    
                    i = 1;
                    foreach (var item in pilotsList)
                    {
                        Console.WriteLine($"[{i}]:  {item.name}");
                        i++;
                    }
                    Console.Write("Please make a selection, or press 66 to return to the menu. >  ");
                    _userChoice = Console.ReadLine();

                    isInt = Validation.CheckInt(_userChoice);
                    _userChoiceInt = isInt ? Int32.Parse(_userChoice) : 000;

                    if (_userChoiceInt == 66)
                    {
                        MainLoop();
                    }
                }
                dynamic chosenPilot = pilotsList[_userChoiceInt - 1];
                DisplayPilot(chosenPilot);
            }
            else 
            {
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
            }
        }

        public static void DisplayPilot(dynamic pilot)
        {
            dynamic homeworld = APIConnect.GetHomeworld(pilot.homeworld.ToString());
            Console.Clear();
            DisplayHeader($"{pilot.name}");


            Console.WriteLine($"Name: {pilot.name}");
            Console.WriteLine($"Gender: {pilot.gender}");
            Console.WriteLine($"Birth Year: {pilot.birth_year}");
            Console.WriteLine($"Hair Color: {pilot.hair_color}");
            Console.WriteLine($"Skin Color: {pilot.skin_color}");
            Console.WriteLine($"Eye Color: {pilot.eye_color}");
            Console.WriteLine($"Height: {pilot.height} Centimeters");
            Console.WriteLine($"Weight: {pilot.weight} Kilograms");
            Console.WriteLine($"\nHomeworld: {homeworld.name}");

            Console.Write($"Would you like to know more about {homeworld.name}?  yes/no >  ");
            string userChoice = Console.ReadLine();
            bool validEntry = Validation.ValidateString(userChoice);
            while (!validEntry || (userChoice.ToLower() != "yes" && userChoice.ToLower() != "no")) 
            {
                Console.WriteLine("Invalid entry!");
                Console.Write($"Would you like to know more about {homeworld.name}?  yes/no >  ");
                userChoice = Console.ReadLine();
                validEntry = Validation.ValidateString(userChoice);

            }

            if (userChoice.ToLower() == "no") 
            {
                MainLoop();
            }
            if (userChoice.ToLower() == "yes") 
            {
                DisplayPlanet(homeworld);
            }

            
        }

        public static void DisplayPlanet(dynamic planet) 
        {
            Console.Clear();
            DisplayHeader(planet.name.ToString());

            Console.WriteLine($"Planet Name: {planet.name}");
            Console.WriteLine($"Population: {planet.population} Sentient Lifeforms");
            Console.WriteLine($"Climate: {planet.climate}");
            Console.WriteLine($"Terrain: {planet.terrain}");
            Console.WriteLine($"Surface Water Coverage: {planet.surface_water}%");
            Console.WriteLine($"Gravity: {planet.gravity} Standard Unit");
            Console.WriteLine($"Day Length: {planet.rotation_period} Hours");
            Console.WriteLine($"Year Length: {planet.orbital_period} Days");
            Console.WriteLine($"Diameter: {planet.diameter} Kilometers");

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }
                
    }

}



