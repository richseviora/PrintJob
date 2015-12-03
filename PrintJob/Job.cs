using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PrintJob
{
    public class Job
    {

        public Job()
        {
            Items = new List<JobItem>();
        }

        public IList<JobItem> Items { get; set; }
        public bool ExtraMargin { get; set; }
        // Stores the margin rate for this job.
        public decimal MarginRate { get; set; }
        // Returns the total margin for all items in the order.
        public decimal Margin => Items.Count() * MarginRate;
        // Returns the total price for all items, including tax and the margin.
        public decimal TotalPrice => Items.Sum(x => x.TotalPrice) + Margin;
        /// <summary>
        /// Stores the tax rate for this job.
        /// </summary>
        public decimal TaxRate { get; set; }

        public void PrintReceipt()
        {

            FileStream filestream = new FileStream("Receipt.txt", FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);

            Console.WriteLine("Job Receipt");
            foreach (var item in Items)
            {
                Console.WriteLine("{0}:  ${1} ", item.Name, Math.Round((item.Price + item.Tax), 2));
            }
            Console.WriteLine("Total: ${0} ", Math.Round(TotalPrice, 2));
        }
    }
}