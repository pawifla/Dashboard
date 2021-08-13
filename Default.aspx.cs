using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SouthForkDamnDashboard.App_Code;

namespace SouthForkDamnDashboard
{
    public partial class Default : System.Web.UI.Page
    {
        //3G Modem: '166.140.84.25' : '2332'
        //Sensor System: port'3389'
        //FTP System: port '2322'
        public static IPHostEntry host = Dns.GetHostEntry("166.140.84.25");
        public static IPAddress ipAd = host.AddressList[0];
        

        public static List<int> ports = new List<int>() {
            Convert.ToInt32(ConfigurationManager.AppSettings["sfModemPort"].ToString()),
            Convert.ToInt32(ConfigurationManager.AppSettings["sfSensorPort"].ToString()),
            Convert.ToInt32(ConfigurationManager.AppSettings["sfFTPPort"].ToString())
        };

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void checkModemConnection()
        {

        }
        protected static bool IsSocketConnected(Socket s, IPEndPoint i)
        {
            s.Connect(i);
            if (!s.Connected)
            {
                Console.WriteLine("NOT Connected" + i);

            }
            return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);


        }

    }
}