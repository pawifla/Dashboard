using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Data;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.Threading.Tasks;



namespace SouthForkDamnDashboard.App_Code
{
    public class DataAccessLayer
    {
        string connStr = ConfigurationManager.ConnectionStrings["sForkDam"].ConnectionString;
        SqlConnection conn;
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        public static string database = "sforkdam"; //prod
        //public static string database = "sforkdam_dev"; //dev

        public DataAccessLayer()
        {
            conn = new SqlConnection(connStr);
        }
        public void getStatus()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
        } 
        public string getRecent()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
           
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT MAX(Measure_Dt) as Measure_Dt, Month(Max(Measure_Dt)) as MeasMonth, Year(MAX(Measure_Dt)) as MeasYear FROM[{database}].dbo.[res_elev]";

            SqlDataReader rdrr = cmd.ExecuteReader();
            DataClass data = new DataClass();
            while (rdrr.Read())
            {
                data.maxDate = rdrr["Measure_Dt"].ToString();
            }
            //Split the date and return both to pop both fields
            string mxDate = data.maxDate;
            Convert.ToDateTime(mxDate);

            rdrr.Close();
            conn.Close();

            return mxDate;
        }
        public string getFiles(string MaxDate)
        {
            DateTime maxDate = Convert.ToDateTime(MaxDate);
            string shortMDate = maxDate.ToShortDateString();

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT RES_ELEVX, Upstream, Downstream, YEAR(Measure_Dt) as MeasYear, Month(Measure_Dt) as MeasMonth, CONVERT(VARCHAR(10), Measure_Dt, 102) as Measure_Dt FROM[{database}].dbo.[res_elev] WHERE Measure_Dt ='{shortMDate}'";
            

            SqlDataReader rdr = cmd.ExecuteReader();
            DataClass data = new DataClass();
            while (rdr.Read())
            {
                data.elevation = Convert.ToDouble(rdr["RES_ELEVX"]);
                data.downstream = Convert.ToInt32(rdr["Downstream"]);
                data.upstream = Convert.ToInt32(rdr["Upstream"]);
                data.maxDate = shortMDate;
                data.year = Convert.ToInt32(rdr["MeasYear"]);
                data.measMonth = Convert.ToInt32(rdr["MeasMonth"]);
            }
            

            rdr.Close();
            conn.Close();

            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
        public string getDateRange()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"Select MAX(Measure_Dt) as MaxDt, MIN(Measure_Dt) as MinDt FROM [{database}].dbo.[res_elev]";
            SqlDataReader rdr = cmd.ExecuteReader();
            DataClass data = new DataClass();
            while (rdr.Read())
            {
                data.minDate = rdr["MinDt"].ToString();
                data.maxDate = rdr["MaxDt"].ToString();
            }
            string minDt = data.minDate;
            string maxDt = data.maxDate;
            conn.Close();
            return $"{minDt} {maxDt}";
        }
        public List<DataClass> getAllYears()
        {
            List<DataClass> datas = new List<DataClass>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT ID, Measure_Dt, RES_ELEVX, Upstream, Downstream FROM[{database}].dbo.[res_elev]";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                DataClass data = new DataClass();
                data.measDate = rdr["Measure_Dt"].ToString();
                data.id = Convert.ToInt32(rdr["ID"]);
                data.elevation = Convert.ToInt32(rdr["RES_ELEVX"]);
                data.upstream = Convert.ToInt32(rdr["Upstream"]);
                data.downstream = Convert.ToInt32(rdr["Downstream"]);
                datas.Add(data);
            }
            conn.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var jData = js.Serialize(datas);
            return datas;

        }
        public List<DataClass> getOneYear(int month, int year)
        {
            List<DataClass> datas = new List<DataClass>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT DISTINCT(ID), Measure_Dt, DAY(Measure_Dt) as measDay, MONTH(Measure_Dt) as measMonth, RES_ELEVX, Upstream, Downstream FROM[{database}].dbo.[res_elev] WHERE Month(Measure_Dt) = '{month}' AND Year(Measure_Dt) = '{year}' ORDER BY Measure_Dt" ;
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                DataClass data = new DataClass();
                data.measMonth = Convert.ToInt32(rdr["measMonth"]);
                data.measDate = rdr["measDay"].ToString();
                data.elevation = Convert.ToDouble(rdr["RES_ELEVX"]);
                data.upstream = Convert.ToDouble(rdr["Upstream"]);
                data.downstream = Convert.ToDouble(rdr["Downstream"]);
                datas.Add(data);
            }
            rdr.Close();
            conn.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var jData = js.Serialize(datas);
            return datas;
        }

        public static IPHostEntry host = Dns.GetHostEntry("166.140.84.25");
        public static IPAddress ipAd = host.AddressList[0];


        public static List<int> ports = new List<int>() {
            Convert.ToInt32(ConfigurationManager.AppSettings["sfModemPort"].ToString()),
            Convert.ToInt32(ConfigurationManager.AppSettings["sfSensorPort"].ToString()),
            Convert.ToInt32(ConfigurationManager.AppSettings["sfFTPPort"].ToString())
        };

        public string ipCheck()
        {
            List<int> ports = new List<int> (){ 2332,3389,2322};
            List<ipDataClass> ipDataList = new List<ipDataClass>();
            Parallel.ForEach(ports, (port) => {
                ipDataClass ipData = new ipDataClass();
                try
                {
                    IPHostEntry host = Dns.GetHostEntry("166.140.84.25");
                    IPAddress ipAd = host.AddressList[0];
                    IPEndPoint modemEndpoint = new IPEndPoint(ipAd, port);

                    Socket skt = new Socket(ipAd.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                    bool connectionEst = IsSocketConnected(skt, modemEndpoint);

                    ipData.ipAddress = modemEndpoint.ToString();
                    ipData.connStatus = connectionEst;
                    ipData.portNumber = port;
                    ipDataList.Add(ipData);

                }
                catch (Exception e)
                {
                    ipData.portNumber = port;
                    ipData.connStatus = false;
                    ipDataList.Add(ipData);
                    Console.WriteLine(e);
                }
            });
            return JsonConvert.SerializeObject(ipDataList, Formatting.Indented);
        }
        public static bool IsSocketConnected(Socket s, IPEndPoint i)
        {
            IAsyncResult result = s.BeginConnect(i, null, null);
            bool success = result.AsyncWaitHandle.WaitOne(3000, true);
            
            if (s.Connected)
            {
                s.EndConnect(result);
            }
            else
            {
                s.Close();
                throw new ApplicationException("Failed to connect to server");
            }
            s.Close();

            return success;
        }
      
    }
}