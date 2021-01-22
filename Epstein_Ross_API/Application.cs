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
            MainLoop();


        }

        public static void Startup()
        {
            var filmReturned = _api.GetAllShips();
            shipList.Add(filmReturned);

            while (!hasQuit)
            {
                if (!hasQuit)
                {
                    MainLoop();
                }
            } 
        }
        
        

        public static void MainLoop() 
        {
            //_api.Connect("test");
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
                Program.Exit();
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
            dynamic shipInfo = APIConnect.GetShip(chosenItem);
            DisplayHeader(shipInfo.model.ToString());

            Console.WriteLine($"Ship Name: {shipInfo.name.ToString()}");
            Console.WriteLine($"Ship Model: {shipInfo.model.ToString()}");
            Console.WriteLine($"Ship Class: {shipInfo.starship_class.ToString()}");
            Console.WriteLine($"Ship Manufacturer: {shipInfo.manufacturer.ToString()}");
            Console.WriteLine($"Hyperdrive Rating: {shipInfo.hyperdrive_rating.ToString()}");
            Console.WriteLine($"Crew: {shipInfo.crew.ToString()}");
            Console.WriteLine($"Passengers: {shipInfo.passengers.ToString()}");


            Console.WriteLine("\nKNOWN PILOTS:");
            if (shipInfo.pilots != null)
                    {
                        List<dynamic> pilotsList = new List<dynamic>();

                        foreach (dynamic pilot in shipInfo.pilots)
                        {
                            pilotsList.Add(APIConnect.GetPilot(pilot.ToString()));
                        }

                int pilotListCount = pilotsList.Count - 1;
                int i = 1;
                foreach (var item in pilotsList)
                {
                    Console.WriteLine($"[{i}]:  {item.name}");
                    i++;
                }
                Console.ReadKey();



            }
        }
                
    }

}



