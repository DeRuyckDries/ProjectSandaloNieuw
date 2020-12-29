using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManager.Models;

namespace ShopManager.ViewModels
{
    /// <summary />
    public class ProductViewModel
    {
        private Product _product;

        public string Name
        {
            get { return _product.Name; }
            set { _product.Name = value; }
        }

        public string Beschrijving
        {
            get { return _product.Description; }
            set { _product.Description = value; }
        }

        public double Prijs
        {
            get { return _product.Price; }
            set { _product.Price = value; }
        }

        public Categories Category
        {
            get { return _product.Category; }
            set { _product.Category = value; }
        }

        public Product Model()
        {
            return _product;
        }

        public ProductViewModel()
        {
            _product = new Product();
        }

        public ProductViewModel(Product p)
        {
            _product = p;
        }
    }
}
