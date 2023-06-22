using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOTC.Config
{
    public static class AppSettings
    {
        public static string REALM
        {
            get
            {
                return ConfigurationManager.AppSettings["REALM"];
            }
        }
        public static string SCENARIO
        {
            get
            {
                return ConfigurationManager.AppSettings["SCENARIO"];
            }
        }
        public static string URL_HOST
        {
            get
            {
                return ConfigurationManager.AppSettings["URL_HOST"];
            }
        }
        public static string PRIVATE_KEY_PATH
        {
            get
            {
                return ConfigurationManager.AppSettings["PRIVATE_KEY_PATH"];
            }
        }
        public static string USERNAME
        {
            get
            {
                return ConfigurationManager.AppSettings["USERNAME"];
            }
        }
    }
}
