using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {

        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Data data = new Data();
            List<Station> stations = new List<Station>();
            List<Station> tempStations = new List<Station>();
            var STATION_INFO_URL = Environment.GetEnvironmentVariable("STATION_INFO_URL");
            var STATION_STATUS_URL = Environment.GetEnvironmentVariable("STATION_STATUS_URL");
            try
            {
                string response = await client.GetStringAsync(STATION_STATUS_URL);               
                Root StationStatus = JsonConvert.DeserializeObject<Root>(response);
                stations = StationStatus.data.stations;

                response = await client.GetStringAsync(STATION_INFO_URL);
                Root StationData = JsonConvert.DeserializeObject<Root>(response);
                tempStations = StationData.data.stations;

                data.stations = mergeLists(stations, tempStations);

                data.printOut();

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }


        public static List<Station> mergeLists(List<Station> list1, List<Station> list2)
        {

            foreach (var i in list1)
            {
                foreach (var t in list2)
                {
                    if (t.station_id == i.station_id)
                    {
                        i.name = t.name;
                    }
                }
            }
            return list1;
        }
    }
}
