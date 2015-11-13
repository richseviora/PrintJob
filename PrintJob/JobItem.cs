namespace PrintJob
{
    public class JobItem
    {        
        public string Name { get; set; }
        public decimal Price { get; set; }        
        public bool Exempt { get; set; }
        public decimal Tax { get; set; }
    }
}
