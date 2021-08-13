using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Configuration;

namespace SouthForkDamnDashboard.App_Code
{
    /// <summary>
    /// Summary description for DataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DataService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        
        public string getRecent()
        {
            DataAccessLayer dal = new DataAccessLayer();
            
            var maxDate = dal.getRecent();
            

            Convert.ToDateTime(maxDate);

            return maxDate;
            
        }
        [WebMethod]
        
        public string getFiles()
        {
            DataAccessLayer dal = new DataAccessLayer();
            string maxDate = dal.getRecent();
            //Convert.ToDateTime(maxDate);



            return dal.getFiles(maxDate);
        }
        [WebMethod]
        public string getDates()
        {
            DataAccessLayer dal = new DataAccessLayer();
            string dates = dal.getDateRange();
            return dates;
        }
        [WebMethod]
        public string getAllYears(int month, int year)
        {
            DataAccessLayer dal = new DataAccessLayer();
            List<DataClass> alldates = dal.getAllYears();
            return JsonConvert.SerializeObject(alldates, Formatting.Indented);
            //return alldates;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string checkIPs()
        {
           
            DataAccessLayer dal = new DataAccessLayer();
         

            return dal.ipCheck();
        }


        //    public int daysInMonth(int month, int year)
        //    {
        //        DataAccessLayer dal = new DataAccessLayer();
        //        int daysInMonth = dal.daysInMonth();
        //        return daysInMonth;
        //    }
    }
}
