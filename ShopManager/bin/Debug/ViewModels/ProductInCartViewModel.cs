using ShopManager.ViewModels.Common;

namespace ShopManager.ViewModels
{
    /// <summary />
    public class ProductInCartViewModel : NotifyPropertyChanged
    {
        private int _quantity;
        public ProductViewModel Product { get; set; }

        public int Aantal
        {
            get { return _quantity; }
            set
            {
                _quantity = value; 
                OnPropertyChanged("Prijs");
            }
        }
    }
}
