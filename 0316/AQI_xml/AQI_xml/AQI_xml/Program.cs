using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AQI_xml
{
    class AQI_station {
        public string AQI{get; set;}
        public string County { get; set; }
        public string PM2_5 { get; set; }
        public string PublishTime { get; set; }
        public string SiteName { get; set; }
        public string Status { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var nodeList = new List<AQI_station>();

            XDocument docNew = XDocument.Load("/Users/Johnny/Desktop/C#/0316/AQI.xml");
            //Console.WriteLine(docNew.ToString());
            IEnumerable<XElement> nodes = docNew.Element("AQI").Elements("Data");

            nodeList = nodes
                .Select(node => {
                    var item = new AQI_station();
                    item.AQI = getValue(node, "AQI");
                    item.County = getValue(node, "County");
                    item.PM2_5 = getValue(node, "PM2.5");
                    item.PublishTime = getValue(node, "PublishTime");
                    item.SiteName = getValue(node, "SiteName");
                    item.Status = getValue(node, "Status");
                    return item;
                }).ToList();
            Show(nodeList);
            Console.ReadKey();
    }
        private static string getValue(XElement node, string v)
        {
            return node.Element(v)?.Value.Trim();
        }
        private static void Show(List<AQI_station> nodes)
        {

            nodes.GroupBy(node => node.County).ToList()
                .ForEach(group => {
                    var key = group.Key;
                    var groupData = group.ToList();
                    var message = $"County:{key},共有 {groupData.Count()}筆";

                    Console.WriteLine(message);
                    foreach (var item in groupData)
                    {
                        Console.WriteLine($"\tAQI: {item.AQI}");
                        //Console.WriteLine($"\tCounty: {item.County}");
                        Console.WriteLine($"\tPM2.5: {item.PM2_5}");
                        Console.WriteLine($"\tPublishTime: {item.PublishTime}");
                        Console.WriteLine($"\tSiteName: {item.SiteName}");
                        Console.WriteLine($"\t狀態: {item.Status}");
                        Console.WriteLine();
                    }
                });

        }
    }
}
