using System.Threading.Tasks;
//using Flurl;
//using Flurl.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using System.Web.Script.Serialization;
using mvc01.EF;

namespace mvc01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        private RootObject getTempData()
        {
            string now = DateTime.Now.ToString("yyyyMMddHHmmss");
            using (var db = new forexEntities())
            {
                db.temperaturetbls.Add(new temperaturetbl { calltime = DateTime.Now, recid = now });
                db.SaveChanges();
            }
            var client1 = new RestClient("https://data.tmd.go.th/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request1 = new RestRequest("api/WeatherToday/V1/?type=json", Method.GET);
            RootObject oRootObject1 = new RootObject();
            IRestResponse response1 = client1.Execute(request1);
            if (response1.StatusDescription == "OK")
            {
                var content1 = response1.Content;
                JavaScriptSerializer oJS1 = new JavaScriptSerializer();
                oRootObject1 = oJS1.Deserialize<RootObject>(content1);
                using (var db = new forexEntities())
                {
                    var rec = db.temperaturetbls.Where(x => x.recid == now).SingleOrDefault();
                    rec.resptime = DateTime.Now;
                    rec.recdata = content1;
                    rec.status = "OK";                    
                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new forexEntities())
                {
                    var rec = db.temperaturetbls.Where(x => x.recid == now).SingleOrDefault();
                    rec.resptime = DateTime.Now;                    
                    rec.status = "FAIL";
                    db.SaveChanges();
                }
            }
            return oRootObject1;
        }

        Task<RootObject> GetTempAsync()
        {
            return Task.Run(() => getTempData());
        }
        public JsonResult GetTemp()
        {
            Task<RootObject> task = GetTempAsync();            
            return Json(task,  JsonRequestBehavior.AllowGet);

            //var data = getTempData();
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            // Flurl
            // dynamic json = await "https://data.tmd.go.th/api/WeatherToday/V1/?type=json".GetJsonAsync();
            //{ "Header":{ "Title":"WeatherToday","Description":"Today's Weather Observation","Uri":"https://data.tmd.go.th/api/WeatherToday/V1","LastBuiltDate":"19/2/2019 0:15:31","CopyRight":"Thai Meteorology Department 2015","Generator":"TMDData_API services"},"Stations":[]}
            //  var objs= JArray.Parse(json);
            //   var obj = objs[0];
            // var Title = obj.Header.Title;
            //  var rss = JObject.Parse(json);
            //var ss = from p in json["Hearder"] select (string)p["Title"];

            var datas = getTempData();
            
            
            

            return View(datas);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    // Model
    /*
    public class Header
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
        public string LastBuiltDate { get; set; }
        public string CopyRight { get; set; }
        public string Generator { get; set; }                
    }
    public class Station
    {
        public string StationId { get; set; }
        public string Result { get; set; }
    }

    
    //  "WmoNumber":"48300","StationNameTh":"แม่ฮ่องสอน","StationNameEng":"MAE HONG SON","Province":"แม่ฮ่องสอน","Latitude":{"Value":"19.298972222222222222222222222","Unit":"decimal degree"},"Longitude":{"Value":"97.97577777777777777777777778","Unit":"decimal degree"}
     //  ,"Observe":{"Time":"19/2/2019","MeanSeaLevelPressure":{"Value":1014.12,"Unit":"hPa"},"Temperature":{"Value":15.8,"Unit":"celcius"},"MaxTemperature":{"Value":32.7,"Unit":"celcius"},"DiffMaxTemperature":{"Value":-0.5,"Unit":"celcius"},"MinTemperature":{"Value":14.9,"Unit":"celcius"},"DiffMinTemperature":{"Value":-1.1,"Unit":"hPa"},"RelativeHumidity":{"Value":91.0,"Unit":"%"},"WindDirection":{"Value":"000","Unit":"degree"},"WindSpeed":{"Value":0.0,"Unit":"km/h"},"Rainfall":{"Value":0.00,"Unit":"mm"}}}


    public class MyData
    {
        //{ "Header":{ "Title":"WeatherToday","Description":"Today's Weather Observation","Uri":"https://data.tmd.go.th/api/WeatherToday/V1","LastBuiltDate":"19/2/2019 0:15:31","CopyRight":"Thai Meteorology Department 2015","Generator":"TMDData_API services"}
        //,"Stations":[]
        public Header header { get; set; }
        public Station station { get; set; }
    }*/

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
