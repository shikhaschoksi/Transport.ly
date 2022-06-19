using System;
using System.Collections.Generic;
using System.Linq;

namespace Transportly.Class
{
    public class ScheduleDetails
    {
        private int NoOfDays { get; set; }
        private string strSchedule { get; set; }
        public List<Schedule> ProcessedSchedule { get; set; }
        public ScheduleDetails(string strschedule, int noOfDays)
        {
            this.NoOfDays = noOfDays;
            this.strSchedule = strschedule;
            ProcessedSchedule = new List<Schedule>();
            if (ProcessFlights())
                PrintFlights();
        }
        private Schedule ProcessFile()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Schedule>(strSchedule);
        }
        private bool ProcessFlights()
        {
            bool IsProcessed = false;
            try
            {
                var schedule = ProcessFile();//new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Schedule>(strSchedule);
                if (schedule.FlightDetail == null)
                    throw new Exception("No flight data available");
                var listOfSchedule = new List<Schedule>();
                int flightCount = 0;
                for (int i = 1; i <= NoOfDays; i++)
                {
                    var schdet = (Schedule)schedule.Clone();
                    listOfSchedule.Add(new Schedule { Day = i, FlightDetail = schdet.FlightDetail.Select((x, ixc) => new FlightDetails { Flight = ++flightCount, Departure = x.Departure, Arrival = x.Arrival, Capacity = x.Capacity }).ToList() });
                }
                ProcessedSchedule = listOfSchedule ?? throw new Exception("No flights processed");
                IsProcessed = true;
            }
            catch (Exception e)
            {
                ExceptionLogging.LogError(e);
            }
            return IsProcessed;
        }
        private void PrintFlights()
        {
            foreach (var schedule in ProcessedSchedule)
            {
                foreach (var flightDetail in schedule.FlightDetail)
                {
                    Console.WriteLine($"Flight: {flightDetail.Flight}, Departure: {flightDetail.Departure}, Arrival: {flightDetail.Arrival}, Day: {schedule.Day}");
                }
            }
        }
        public void PrintSchedule()
        {
            foreach (var schedule in ProcessedSchedule)
            {
                foreach (var flightDetail in schedule.FlightDetail)
                {
                    Console.WriteLine($"\nFlight: {flightDetail.Flight}, Capacity: {flightDetail.Capacity}, Occupied: {flightDetail.Assigned}, IsFull: {(flightDetail.isFull ? "Yes" : "No")} ");
                    foreach (var order in flightDetail.Orders)
                        Console.WriteLine($"Order: {order}, Flight: {flightDetail.Flight}, Departure: {flightDetail.Departure}, Arrival: {flightDetail.Arrival}, Day: {schedule.Day}");
                }
            }
        }

    }
}
