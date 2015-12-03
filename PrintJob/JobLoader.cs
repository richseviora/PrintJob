using System;
using System.Collections.Generic;
using System.Linq;

namespace PrintJob
{
    // Defines contract requirements for types that load data and convert it into a job.
    public interface IJobLoader
    {
        // Parses the lines provided and makes the data available.
        Job Parse(IEnumerable<string> lines);
    }

    // Loads a file and returns a Job object.
    public class JobLoader : IJobLoader
    {
        public decimal MarginRate { get; set; }
        public decimal ExtraMarginRate { get; set; }
        public decimal TaxRate { get; set; }

        public JobLoader(decimal marginRate, decimal extraMarginRate, decimal taxRate)
        {
            MarginRate = marginRate;
            ExtraMarginRate = extraMarginRate;
            TaxRate = taxRate;
        }

        public Job Parse(IEnumerable<string> lines)
        {
            var lineList = lines.ToList();
            var job = new Job {TaxRate = TaxRate};
            var skipIndex = 0;
            if (lineList[0] == "extra-margin")
            {
                job.MarginRate = ExtraMarginRate;
                skipIndex = 1;
            }
            else
            {
                job.MarginRate = MarginRate;
            }
            job.Items = lineList.Skip(skipIndex).Select(x => ParseItemLine(x, job)).ToList();
            return job;
        }

        private JobItem ParseItemLine(string line, Job job)
        {
            var item = line.Split(' ');
            var exemptValue = item.Length > 2 && item[2] == "exempt";
            return new JobItem { Name = item[0], Price = Convert.ToDecimal(item[1]), Exempt = exemptValue, Job =  job};
        }
    }
}