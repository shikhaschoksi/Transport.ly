using System;
using System.IO;
using Transportly.Class;

namespace Transportly
{
    class Program
    {
        static void Main(string[] args)
        {

            //Input string with fixed capacity = 20
            var strSchedule = @"{""Day"":1,""FlightDetail"":[{""Flight"":1,""Departure"":""yul"",""Arrival"":""yyz""},{""Flight"":2,""Departure"":""yul"",""Arrival"":""yyc""},{""Flight"":3,""Departure"":""yul"",""Arrival"":""yvr""}]}";
            //Input string with variable capacity
            //var strSchedule = @"{""FlightDetail"":[{""Flight"":1,""Departure"":""yul"",""Arrival"":""yyz"",""Capacity"":10},{""Flight"":2,""Departure"":""yul"",""Arrival"":""yyc"",""Capacity"":20},{""Flight"":3,""Departure"":""yul"",""Arrival"":""yvr"",""Capacity"":30}]}";
            //var strSchedule = @"{}";
            var process = new ProcessData(strSchedule, 2, Directory.GetParent(Environment.CurrentDirectory).Parent.FullName+"\\coding-assigment-orders.json");
            process.ProcessOrders();
            process.Print();
            Console.ReadKey();
        }
    }
}
