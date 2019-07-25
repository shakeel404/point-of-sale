
using POS.ViewModel.Utils;
using System;
using P = POS.Model.Models;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using POS.Repository.IServices;
using Prism.Events;
using Prism.Commands;

namespace POS.ViewModel.ViewModels.Product
{
    public class ProductAddViewModel : SavableViewModel<IProductRepository>
    { 
        public ProductAddViewModel(IProductRepository repository,IEventAggregator ea)
        {
            Repository = repository; 
            EA = ea;
        }

        string upc, name;
        int qty;
        double purchasePrice, salePrice; 


        public string UPC
        {
            get
            {
                return upc;
            }
            set
            {
                if (upc != value)
                    SetProperty<string>(ref upc, value);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                    SetProperty<string>(ref name, value, OnModelChanged);
            }
        }

        public int Quantity
        {
            get
            {
                return qty;
            }
            set
            {
                if (qty != value)
                    SetProperty<int>(ref qty, value, OnModelChanged);
            }
        }

        public double PurchasePrice
        {
            get
            {
                return purchasePrice;
            }
            set
            {
                if (purchasePrice != value)
                    SetProperty<double>(ref purchasePrice, value, OnModelChanged);
            }
        }

        public double SalePrice
        {
            get
            {
                return salePrice;
            }
            set
            {
                if (salePrice != value)
                    SetProperty<double>(ref salePrice, value, OnModelChanged);
            }
        } 
        
        protected override bool CanSave()
        {
            return !(string.IsNullOrWhiteSpace(UPC) || string.IsNullOrWhiteSpace(Name));
        }

        protected override async void Save()
        {
            var exist = Repository.Get(p => p.UPC == UPC).Count() > 0;

            if (exist)
            { 
                await  Dialoger.ShowMessageAsync(this, "Inventory", $"Product already exist with UPC:[{UPC}]",
                                                      MessageDialogStyle.Affirmative,OkCancelMessageSettings);
                return;
            }

              var product = new P.Product()
              {
                  UPC = UPC,
                  Name = Name,
                  Quantity = Quantity,
                  PurchasePrice = PurchasePrice,
                  SalePrice = SalePrice
              };

            Repository.Add(product);
            Repository.Save();  
            UPC = Name = string.Empty;

            EA.GetEvent<ProductAddEvent>().Publish(product);
        }

        
    }
}
