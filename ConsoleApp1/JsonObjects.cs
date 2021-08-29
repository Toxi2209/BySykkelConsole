using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
   // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Station
    {
        public string station_id { get; set; }
        public string name { get; set; }
        public int is_installed { get; set; }
        public int is_renting { get; set; }
        public int is_returning { get; set; }
        public int last_reported { get; set; }
        public int num_bikes_available { get; set; }
        public int num_docks_available { get; set; }
        override
        public string ToString()
        {
            return $"Id:{station_id} Bikes:{num_bikes_available} Docks:{num_docks_available} Name:{name}\n";
        }
    }

    public class Data
    {
        public List<Station> stations { get; set; }
    }

    public class Root
    {
        public int last_updated { get; set; }
        public int ttl { get; set; }
        public string version { get; set; }
        public Data data { get; set; }
    }
}
