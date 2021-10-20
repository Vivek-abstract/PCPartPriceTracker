namespace ViewModels.Helpers
{
    public class PriceReachedTargetEventArgs
    {
        public string Products { get; set; }

        public PriceReachedTargetEventArgs(string products)
        {
            Products = products;
        }
    }
}