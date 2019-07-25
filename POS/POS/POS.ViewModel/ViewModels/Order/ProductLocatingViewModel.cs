using POS.Repository.IServices;
using POS.ViewModel.Utils;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using P=POS.Model.Models;
using System.Text;
using System.Threading.Tasks;

namespace POS.ViewModel.ViewModels.Order
{
    public class ProductLocatingViewModel : BaseViewModel
    {
        public ProductLocatingViewModel(IProductRepository repository,IEventAggregator ea)
        {
            Repository = repository;
            EA = ea;
            SearchCommand = new DelegateCommand(Search, CaSearch);
        }

        bool isManually;
        string upc;


        public bool IsManually
        {
            get { return isManually; }
            set
            {
                SetProperty<bool>(ref isManually, value);
            }
        }
        public string UPC
        {
            get { return upc; }
            set
            { 
                    SetProperty<string>(ref upc, value, OnModelChanged); 
            }
        }

        public IProductRepository Repository { get; }

        public DelegateCommand SearchCommand { get; }

        protected virtual bool CaSearch()
        {
            return !string.IsNullOrWhiteSpace(UPC);
        }

        protected virtual void Search()
        {

            GetProduct();
        }

        protected override  void OnModelChanged()
        {
            if (IsManually)
                SearchCommand.RaiseCanExecuteChanged();
            else
                GetProduct();
        }

        protected virtual void GetProduct()
        {
            if (string.IsNullOrWhiteSpace(UPC) || UPC.Length < 7)
                return;

            var product = Repository.Get(p => p.UPC == UPC).FirstOrDefault(); 

            if (product == null)
            {
                Dialoger.ShowMessageAsync(this, "Product", $"No Product found for UPC:[{UPC}]", MahApps.Metro.Controls.Dialogs.MessageDialogStyle.Affirmative,OkCancelMessageSettings);
            }
            else
            {

                var productVM = new P.Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    PurchasePrice = product.PurchasePrice,
                    SalePrice = product.SalePrice,
                     Quantity=0, ///Becasue the ListableViewModel will take care of quantity
                      UPC=product.UPC 
                };

                EA.GetEvent<PurchaseAddEvent>().Publish(productVM);
            }
        }



    }
}
