using ReportOTC.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReportOTC
{

    static class OTCHelper
    {
        public static UserAndSignature GetSign(String userId, List<string> groups, string realm, string scenario)
        {
            //Get UtcDate
            string creationDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ",
            CultureInfo.InvariantCulture);
            //Get Private key (filename <Realm>-<Scenario>-PrivateKey-<Date><Time>.xml)
            string privateKey = System.IO.File.ReadAllText(AppSettings.PRIVATE_KEY_PATH);
            // C:\Cert\ACME-production-PrivateKey-20230620-035819827.xml << VM
            //Create String To Sign <UserId><Group1,Group2,...><UTCDateTime>
            string strToSign = String.Format(
            "{0}|{1}|{2}|{3}|{4}",
            userId,
            String.Join(",", groups),
            creationDate,
            realm,
            scenario);
            //Get Signature from String
            string signature = Sign(strToSign, privateKey);
            UserAndSignature userToAuthenticate =
            new UserAndSignature
            {
                UserName = userId,
                Groups = groups,
                CreationTime = creationDate,
                Realm = realm,
                Scenario = scenario,
                Signature = signature
            };
            return userToAuthenticate;
        }
        //Signature of String SHA512 FIPS Compatible
        private static string Sign(string StringToSign, string PrivateKey)
        {
            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
            {
                rsaProvider.FromXmlString(PrivateKey);
                using (SHA512CryptoServiceProvider hashProvider = new
                SHA512CryptoServiceProvider())
                {
                    UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
                    byte[] SignedHashValue = rsaProvider.SignHash(
                    hashProvider.ComputeHash(unicodeEncoding.GetBytes(StringToSign)),
                    "System.Security.Cryptography.SHA512CryptoServiceProvider");
                    return System.Convert.ToBase64String(SignedHashValue);
                }
            }
        }
    }
}
