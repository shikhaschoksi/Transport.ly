using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportly.Class
{
    public class OrderDetails
    {
        public string jsonInputOrderFile { get; set; }
        public List<Orders> ProcessedOrders { get; set; }
        public OrderDetails(string jsonInputOrderFile)
        {
            this.jsonInputOrderFile = jsonInputOrderFile;
            ProcessedOrders = new List<Orders>();
            if (LoadJson())
                PrintTotalOrders();
        }
        private static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return dictionary;
        }

        private bool LoadJson()
        {
            bool IsProcessed = false;
            try
            {
                using (StreamReader r = new StreamReader(jsonInputOrderFile))
                {
                    var orders = new List<Orders>();
                    string json = r.ReadToEnd();
                    Dictionary<string, object> json_Dictionary = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                    foreach (var d in json_Dictionary.OrderBy(x => x.Key))
                    {
                        var t = ToDictionary<string>(d.Value);
                        orders.Add(new Orders { Destination = ToDictionary<string>(d.Value)["destination"].ToLower(), ID = d.Key });
                    }
                    ProcessedOrders = orders ?? throw new Exception("No orders processed");
                    IsProcessed = true;
                }
            }
            catch (Exception e)
            {
                ExceptionLogging.LogError(e);
            }
            return IsProcessed;
        }
        private void PrintTotalOrders()
        {
            Console.Write("\nTotal orders for Destination: ");
            foreach (var order in ProcessedOrders.GroupBy(x => x.Destination))
            {
                Console.Write($"{order.Key}: {order.Count()}\t"); ;
            }
            Console.WriteLine();
        }
        public void PrintUnProcessedOrders()
        {
            Console.WriteLine($"\nTotal Orders: {ProcessedOrders.Count()} Processed Orders: {ProcessedOrders.Where(x => x.isProcessed).Count()} Unprocessed Orders: {ProcessedOrders.Where(x => !x.isProcessed).Count()}");

            foreach (var order in ProcessedOrders.Where(x => !x.isProcessed))
            {
                Console.WriteLine($"Order: {order.ID}, Destination: {order.Destination}, Flight: Not Scheduled");
            }
        }

    }
}
