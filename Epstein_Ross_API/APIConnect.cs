using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Epstein_Ross_API
{

    
    class APIConnect
    {
                
        private static string apiUrl = "https://www.swapi.tech/api/starships";
        
        public APIConnect() 
        { 
        
        }

        



        public dynamic GetAllShips() 
        {
    

            using (WebClient wc = new WebClient())
            {
                string results = wc.DownloadString(apiUrl);
                dynamic allShips = JObject.Parse(results);
                return allShips.results;
            }

        }

        public static dynamic GetShip(dynamic chosenItem)
        {
            using (WebClient wc = new WebClient())
            {
            //https://www.swapi.tech/api/starships
                string results = wc.DownloadString(apiUrl + "/" + chosenItem.uid);
                dynamic ship = JObject.Parse(results);
                return ship.result.properties;
            }

        }
    }
}
