using System;
using System.Collections.Generic;

namespace PrintJob
{
    public class Job
    {
        public IList<JobItem> Items { get; set; }
        public bool ExtraMargin { get; set; }
        public decimal Margin { get; set; }
        public decimal TotalPrice { get; set; }
       
        public void PrintReceipt(Job job)
        {
            Console.WriteLine("Job Receipt");
            foreach(var item in job.Items)
            {                   
                Console.WriteLine("{0}:  {1} ", item.Name, Math.Round((item.Price + item.Tax),2));
            }
            Console.WriteLine("Total: {0} ", Math.Round(job.TotalPrice,2));
        }
    }
}
