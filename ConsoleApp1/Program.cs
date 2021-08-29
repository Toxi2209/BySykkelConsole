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
            List<Station> stations = new List<Station>();
            List<Station> tempStations = new List<Station>();
            try
            {
                string response = await client.GetStringAsync("https://gbfs.urbansharing.com/oslobysykkel.no/station_status.json");               
                Root StationStatus = JsonConvert.DeserializeObject<Root>(response);
                stations = StationStatus.data.stations;

                response = await client.GetStringAsync("https://gbfs.urbansharing.com/oslobysykkel.no/station_information.json");
                Root StationData = JsonConvert.DeserializeObject<Root>(response);
                tempStations = StationData.data.stations;

                

                foreach (var i in stations)
                {
                    foreach (var t in tempStations)
                    {
                        if (t.station_id == i.station_id){
                            i.name = t.name;
                        }
                    }
                }


                stations.ForEach(result => Console.WriteLine(result.ToString()));


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
