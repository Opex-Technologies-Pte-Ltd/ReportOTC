using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOTC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var otc = new OTC();

            Task.Run(async () => {
                var data = await otc.getOneTimeCode();
                Console.WriteLine(data);

                if (data != null)
                {
                    var reports = await otc.GetJsonReportList(data);
                    Console.WriteLine(reports);
                }
            });

            Console.ReadKey();
        }
    }
}
