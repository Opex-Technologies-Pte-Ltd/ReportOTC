using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReportOTC.Config;

namespace ReportOTC
{
    public class OTC
    {
        public async Task<string> getOneTimeCode()
        {
            //this user should be a trusted user in the reporting api
            //get username and groups
            string userName = AppSettings.USERNAME;

            //Used public key REALM1-SCENARIO1-PrivateKey-20190912-121150201.xml
            //Used public key <realm>-<scenario>-PrivateKey-20190912-121150201.xml
            String realm = AppSettings.REALM;
            String scenario = AppSettings.SCENARIO;
            List<String> groups = new List<string>();

            //get Signature
            UserAndSignature signature = OTCHelper.GetSign(userName, groups, realm, scenario);

            //call Authenticate Method
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AppSettings.URL_HOST);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(signature), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Authenticate", contentPost);
            response.EnsureSuccessStatusCode();
            var otc = await response.Content.ReadAsStringAsync();
            return otc.Trim('"');
        }
    }

    public class UserAndSignature
    {
        public string UserName { get; set; }
        public string CreationTime { get; set; }
        public List<string> Groups { get; set; }
        public string Realm { get; set; }
        public string Scenario { get; set; }
        public string Signature { get; set; }
    }

}
