/**
 * Ross Epstein
 * January 22nd, 2021
 * API 
 * **/

using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Epstein_Ross_API
{

    
    class APIConnect
    {
                
        //create base api url
        private static string apiUrl = "https://www.swapi.tech/api/starships";
        
        public APIConnect() 
        { 
        
        }

        
        public dynamic GetAllShips() 
        {
            using (WebClient wc = new WebClient())
            {
                //get all ships
                string results = wc.DownloadString(apiUrl);
                
                //parse to json object
                dynamic allShips = JObject.Parse(results);
                
                //return parsed results
                return allShips.results;
            }

        }

        public static dynamic GetShip(dynamic chosenItem)
        {
            using (WebClient wc = new WebClient())
            {
                //get starship based on UID of selected starship
                string results = wc.DownloadString(apiUrl + "/" + chosenItem.uid);
                
                //parse to JSON object
                dynamic ship = JObject.Parse(results);
                
                //return the properties of the ship
                return ship.result.properties;
            }

        }

        public static dynamic GetPilot(string pilot) 
        {
            using (WebClient wc = new WebClient())
            {
                //get pilot info
                string results = wc.DownloadString(pilot.ToString());
                
                //parse pilot JSON object
                dynamic pilotObj = JObject.Parse(results);
                
                //return pilot properties
                return pilotObj.result.properties;
            }
        }

        public static dynamic GetHomeworld(string homeworld)
        {
            using (WebClient wc = new WebClient())
            {
                //get homeworld
                string results = wc.DownloadString(homeworld.ToString());
                
                //parse JSON for Homeworld
                dynamic planetObj = JObject.Parse(results);
                
                //return properties of homeworld
                return planetObj.result.properties;
            }
        }
    }
}
