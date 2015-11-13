using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PrintJob
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please type data file name");
            var datafile = Console.ReadLine();

            const decimal TaxRate = 0.07m;
            const decimal MarginRate = 0.11m;
            const decimal ExtraMarginRate = 0.16m;

            var job = new Job();

            var lines = File.ReadAllLines(datafile);
            var linescoll = new Collection<string>(lines);
            var skipindex = 0;
            
            if (lines[0] == "extra-margin")
            {
                job.ExtraMargin = true;
                skipindex = 1;
            }
            else
            {
                job.ExtraMargin = false;
            }            

            foreach(var line in linescoll.Skip(skipindex))
            {
                var item = line.Split(' ');
                var jobitem = new JobItem { Name = item[0], Price = Convert.ToDecimal(item[1]), Exempt = item.Length>2&& item[2]== "exempt" };
                job.AddItem(jobitem);
            }           
            
            //job.Items = GetItemsList();

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

            //Console.ReadLine();
        }

        //private static IList<JobItem> GetItemsList()
        //{
        //    return new List<JobItem>
        //    {
        //        new JobItem {Name = "envelopes", Price = 520.00m, Exempt = false},
        //        new JobItem {Name = "letterhead ", Price = 1983.37m, Exempt = true}
        //    };
        //}
    }
}
