/**
 * Ross Epstein
 * January 22nd, 2021
 * API 
 * **/

using System;
using System.Collections.Generic;
using System.Text;

namespace Epstein_Ross_API
{
    class Menu
    {
        //display menu items
        public static void DisplayMenu(dynamic menuItem) 
        {
            int i = 1;

            foreach (var item in menuItem)
            {
                Console.WriteLine($"[{i}]:  {item.name}");
                i++;
            }
            
        }

        //display pilots
        public static void PilotList(dynamic pilotsList) 
        {
            int i = 1;
            foreach (var item in pilotsList)
            {
                
                Console.WriteLine($"[{i}]:  {item.name}");
                i++;
            }
        }

    }
}
