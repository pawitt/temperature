using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static string getTempData()
        {
            var client1 = new RestClient("http://localhost/mvc01/Home/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request1 = new RestRequest("GetTemp", Method.GET);
            RootObject oRootObject1 = new RootObject();
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;

            return "";
        }
        static void Main(string[] args)
        {
            Console.WriteLine("start consume data..");
            var x = getTempData();
            Console.WriteLine("press any key to continue..");
        }
    }
    #region model temperature
    //-- convert json to C# class http://json2csharp.com/#
    public class Header
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
        public string LastBuiltDate { get; set; }
        public string CopyRight { get; set; }
        public string Generator { get; set; }
    }
    public class Latitude
    {
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    public class Longitude
    {
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    public class MeanSeaLevelPressure
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class Temperature
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class MaxTemperature
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class DiffMaxTemperature
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class MinTemperature
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class DiffMinTemperature
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class RelativeHumidity
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class WindDirection
    {
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    public class WindSpeed
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class Rainfall
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }

    public class Observe
    {
        public string Time { get; set; }
        public MeanSeaLevelPressure MeanSeaLevelPressure { get; set; }
        public Temperature Temperature { get; set; }
        public MaxTemperature MaxTemperature { get; set; }
        public DiffMaxTemperature DiffMaxTemperature { get; set; }
        public MinTemperature MinTemperature { get; set; }
        public DiffMinTemperature DiffMinTemperature { get; set; }
        public RelativeHumidity RelativeHumidity { get; set; }
        public WindDirection WindDirection { get; set; }
        public WindSpeed WindSpeed { get; set; }
        public Rainfall Rainfall { get; set; }
    }

    public class Station
    {
        public string WmoNumber { get; set; }
        public string StationNameTh { get; set; }
        public string StationNameEng { get; set; }
        public string Province { get; set; }
        public Latitude Latitude { get; set; }
        public Longitude Longitude { get; set; }
        public Observe Observe { get; set; }
    }

    public class RootObject
    {
        public Header Header { get; set; }
        public List<Station> Stations { get; set; }
    }
    #endregion
}
