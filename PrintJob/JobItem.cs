using System.ComponentModel.Design;

namespace PrintJob
{
    public class JobItem
    {
        // Object should know what hierarchy it's part of.
        public Job Job { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Exempt { get; set; }
        public decimal Tax { get; protected set; }
        // Stores the current taxation rate. Useful if there is a possible that the tax rate changes.
        public decimal TaxRate => Job.TaxRate;
        // Updates the tax value and stores the tax rate.
        public void UpdateTax()
        {
            var taxRate = Exempt ? 0 : TaxRate;
            Tax = Price * taxRate;
        }

        // Returns the current tax value. 
        public decimal TotalPrice => Price + TaxRate;
    }
}