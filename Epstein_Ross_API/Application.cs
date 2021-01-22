using System;
using System.Collections.Generic;
using System.Text;

namespace Epstein_Ross_API
{
    class Application
    {


        public static APIConnect _api;
        public static List<string> starWarsFilms = new List<string> { "Star Wars", "empire strikes back", "return of the jedi"};
        public static List<dynamic> filmList = new List<dynamic>();
        public static bool hasQuit = false;
        public Application() 
        {
            _api = new APIConnect();
            Startup();
            MainLoop();


        }

        public static void Startup() 
        {
            foreach (string film in starWarsFilms) 
            {
                var filmReturned = _api.GetFilm(film);
                filmList.Add(filmReturned);
                
                
            }
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
            DisplayHeader("Star Wars Databse");
           

            //menu functionality
            int menuLength = filmList.Count - 1;
            int i = 1;

            foreach (var item in filmList)
            {
                Console.WriteLine($"[{i}]:  {item.Title}");
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

                foreach (var item in filmList)
                {
                    Console.WriteLine($"[{i}]:  {item}");
                    i++;
                }

                //error if menu selection out of range    
                Console.Write($"Invalid entry!  Please enter a number between 1 and {menuLength} or 66 to exit! > ");
                _userChoice = Console.ReadLine();
                isInt = Validation.CheckInt(_userChoice);
                _userChoiceInt = isInt ? Int32.Parse(_userChoice) : 000; ;
                isInRange = Validation.CheckRange(_userChoiceInt, (menuLength + 1));
            }

            

            dynamic chosenItem = filmList[_userChoiceInt - 1];
            Console.WriteLine(chosenItem);
            Console.ReadKey();
        }

        //header method
        public static void DisplayHeader(string headerName)
        {
            Console.Clear();
            Console.WriteLine(headerName.ToUpper());
            Console.WriteLine("===================================================");
        }

    }


}
