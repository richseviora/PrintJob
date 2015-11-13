using System;
using System.Collections.Generic;
using System.IO;

namespace PrintJob
{
    class Program
    {
        static void Main(string[] args)
        {
            const decimal TaxRate = 0.07m;
            const decimal MarginRate = 0.11m;
            const decimal ExtraMarginRate = 0.16m;

            var job = new Job();

            using (StreamReader sr = File.OpenText("job.txt"))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if(s == "extra-margin")
                    {
                        job.ExtraMargin = true;
                    }
                    else
                    {
                        job.ExtraMargin = true;
                    }
                    Console.WriteLine(s);
                }
            }
            
            job.ExtraMargin = true;

            job.Items = GetItemsList();

            foreach(var item in job.Items)
            {
                if (item.Exempt)
                {
                    item.Tax = 0.0m;
                }
                else
                {
                    item.Tax = item.Price * TaxRate;
                }
                if (job.ExtraMargin)
                {
                    job.Margin += item.Price * ExtraMarginRate;
                }
                else
                {
                    job.Margin += item.Price * MarginRate;
                }
                job.TotalPrice += item.Price + item.Tax;                
            }
            job.TotalPrice += job.Margin;
            job.PrintReceipt(job);

            Console.ReadLine();
        }

        private static IList<JobItem> GetItemsList()
        {
            return new List<JobItem>
            {
                new JobItem {Name = "envelopes", Price = 520.00m, Exempt = false},
                new JobItem {Name = "letterhead ", Price = 1983.37m, Exempt = true}
            };
        }
    }
}
