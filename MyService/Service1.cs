using MyService.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            this.OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            var x = getTempData();
            /*
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "Log.text";
            System.IO.File.AppendAllLines(strPath, new[] { "Starting time :" + DateTime.Now.ToString() });
            */
        }

        protected override void OnStop()
        {
            /*
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "Log.text";
            System.IO.File.AppendAllLines(strPath, new[] { "Starting time :" + DateTime.Now.ToString() });
            */
        }

        private string getTempData()
        {
            var client1 = new RestClient("http://localhost/mvc01/Home/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request1 = new RestRequest("GetTemp", Method.GET);
            RootObject oRootObject1 = new RootObject();
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
                        
            return "";
        }
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {


            var ret = getTempData();
            /*
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";            
            System.IO.File.AppendAllLines(strPath, new[] { "..calling time : " + DateTime.Now.ToString() });
            */
        }
    }
}
