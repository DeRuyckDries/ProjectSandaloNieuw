﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ShopManager.Models;
using ShopManager.ViewModels.Common;
using ShopManager.Views;

namespace ShopManager.ViewModels
{

    public class CartViewModel : NotifyPropertyChanged
    {
        public double TotaalPrijs
        {
            get
            {
                double sum = 0;
                foreach (var cartItem in CartItems)
                {
                    sum += cartItem.Product.Prijs * cartItem.Aantal;
                }
                return sum;
            }
        }
        public ObservableCollection<ProductInCartViewModel> CartItems { get; set; } = new ObservableCollection<ProductInCartViewModel>();
        private ICommand _addProduct;
        private ICommand _checkout;
        private ICommand _clearCart;
        private ICommand _removeProduct;
        private ICommand _addQuantity;
        private ICommand _minusQuantity;

        public ICommand AddProduct
        {
            get
            {
                if (_addProduct == null)
                {
                    _addProduct = new RelayCommand(x =>
                    {
                        var product = (ProductViewModel) x;
                        int quantity = AddToCart.Show(product.Model());
                        if (quantity > 0)
                        {
                            bool found = false;
                            foreach (var productInCartViewModel in CartItems)
                            {
                                if (productInCartViewModel.Product == product)
                                {
                                    found = true;
                                    productInCartViewModel.Aantal += quantity;
                                    break;
                                }
                            }
                            if(!found)
                            {
                                CartItems.Add(new ProductInCartViewModel()
                                {
                                    Product = product,
                                    Aantal = quantity
                                });
                            }
                            OnPropertyChanged("TotaalPrijs");
                        }
                    });
                }
                return _addProduct;
            }
        }

        public ICommand RemoveProduct
        {
            get
            {
                if (_removeProduct == null)
                {
                    _removeProduct = new RelayCommand(x =>
                    {
                        CartItems.Remove((ProductInCartViewModel) x);
                        OnPropertyChanged("TotaalPrijs");

                    });
                }
                return _removeProduct;
            }
        }

        public ICommand ClearCart
        {
            get
            {
                if (_clearCart == null)
                {
                    _clearCart = new RelayCommand(x =>
                    {
                        CartItems.Clear();
                        OnPropertyChanged("TotaalPrijs");
                    });
                }
                
                return _clearCart;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand Checkout
        {
            get
            {
                if (_checkout == null)
                {
                    _checkout = new RelayCommand(x =>
                    {
                        MessageBox.Show($"Checkout completed. Totaal Prijs {TotaalPrijs}$", caption:"Checkout");
                    },x=>TotaalPrijs>0);
                }

                return _checkout;
            }
        }

        public ICommand AddQuantity
        {
            get
            {
                if (_addQuantity == null)
                {
                    _addQuantity = new RelayCommand(x=>
                    {
                        (x as ProductInCartViewModel).Aantal++;
                        OnPropertyChanged("TotaalPrijs");

                    });
                }
                return _addQuantity;
            }
        }

        public ICommand MinusQuantity
        {
            get
            {
                if (_minusQuantity == null)
                {
                    _minusQuantity = new RelayCommand(x =>
                    {
                        (x as ProductInCartViewModel).Aantal--;
                        OnPropertyChanged("TotaalPrijs");
                    }, x=>(x as ProductInCartViewModel).Aantal>0);
                }
                return _minusQuantity;
            }
        }
    }
}
