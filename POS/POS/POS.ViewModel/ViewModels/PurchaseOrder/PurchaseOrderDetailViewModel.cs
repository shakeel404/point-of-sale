using POS.Repository.IServices;
using POS.ViewModel.ViewModels.Product;
using Prism.Events;
using P = POS.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using POS.ViewModel.Utils;
using Prism.Commands;

namespace POS.ViewModel.ViewModels.PurchaseOrder
{
    public class PurchaseOrderDetailViewModel : ProductBaseViewModel<IPurchaseOrderDetailRepository>
    {
        public PurchaseOrderDetailViewModel(IEventAggregator ea):this()
        {  
            EA = ea;

        }

        public PurchaseOrderDetailViewModel()
        {
            IncreaseQuantityCommand = new DelegateCommand(IncreaseQuantity);

        }
        public int ProductId { get; set; }

        public int PurchaseOrderId { get; set; } 
        


        public DelegateCommand IncreaseQuantityCommand { get; } 
         
        private void IncreaseQuantity()
        {
            Quantity = Quantity + 1;
        }

        protected override void Delete()
        {
            Quantity = Quantity - 1;

            if (Quantity > 0)
                return;
             
            var product = new P.Product()
            {
                UPC = UPC,
            };

            EA.GetEvent<PurchaseRemoveEvent>().Publish(product);
        }

        protected override  void OnModelChanged()
        {
            RaisePropertyChanged(nameof(UnitProfit));
            RaisePropertyChanged(nameof(TotalProfit));

            if(EA!=null)
            EA.GetEvent<ProductModelChange>().Publish();
        }


    }
}
