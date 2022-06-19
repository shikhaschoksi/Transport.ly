using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportly.Class
{
    class ProcessData
    {
        private ScheduleDetails scheduleDetails;
        private OrderDetails orderDetails;
        public ProcessData(string strschedule, int noOfDays, string jsonInputOrderFile)
        {
            scheduleDetails = new ScheduleDetails(strschedule, noOfDays);
            orderDetails = new OrderDetails(jsonInputOrderFile);
        }
        public void ProcessOrders()
        {
            try
            {
                foreach (var s in scheduleDetails.ProcessedSchedule)
                {
                    foreach (var f in s.FlightDetail)
                    {
                        if (!f.isFull)
                        {
                            var sum = 0;
                            var o = orderDetails.ProcessedOrders.Where(x => x.Destination == f.Arrival && !x.isProcessed).TakeWhile(x => { var temp = sum; sum += 1; if (temp < f.Capacity - f.Assigned) { x.isProcessed = true; return temp < f.Capacity - f.Assigned; } else { return false; } }).ToList();
                            //o.ForEach(x => x.isProcessed = true);
                            f.Assigned += o.Count();
                            f.Orders = o.Select(X => X.ID);
                            if (f.Assigned == f.Capacity)
                            {
                                f.isFull = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionLogging.LogError(e);
            }
        }
        public void Print()
        {
            scheduleDetails.PrintSchedule();
            orderDetails.PrintUnProcessedOrders();
        }

    }
}
