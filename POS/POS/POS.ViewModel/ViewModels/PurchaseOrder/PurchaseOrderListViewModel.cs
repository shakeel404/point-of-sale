
using POS.ViewModel.Utils;
using P = POS.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Prism.Events;
using System.Linq;
using AutoMapper;

namespace POS.ViewModel.ViewModels.PurchaseOrder
{
    public class PurchaseOrderListViewModel :ListableViewModel<PurchaseAddEvent,PurchaseRemoveEvent,P.Product,PurchaseOrderDetailViewModel>
    {
        public PurchaseOrderListViewModel(IEventAggregator ea) : base(null, ea,matchProperty:"UPC")
        {

            EA.GetEvent<ProductModelChange>().Subscribe(CalculateTotal);
        }

        protected override void OnAdded(P.Product item)
        {
            var productVM = Items.FirstOrDefault(p => p.UPC == item.UPC);  

            if (productVM==null)
            {
                productVM = Mapper.Map<PurchaseOrderDetailViewModel>(item); 
                Items.Add(productVM);
            }
            

            productVM.Quantity += 1; 
        }

         
        protected override void OnRemoved(P.Product item)
        {
            var productVM = Items.FirstOrDefault(p => p.UPC == item.UPC); 
            Items.Remove(productVM);

            CalculateTotal();
        }

        public int TotalProduct
        {
            get
            {
                return Items.Count;
            }
        }

        public int TotalQuantity
        {
            get
            {
                return Items.Sum(p => p.Quantity);
            }
        }

        public int GrandTotal
        {
            get
            {
                return (int)Items.Sum(p => p.Quantity * p.PurchasePrice);
            }
        } 

        public void CalculateTotal()
        {
            RaisePropertyChanged(nameof(TotalProduct));
            RaisePropertyChanged(nameof(TotalQuantity));
            RaisePropertyChanged(nameof(GrandTotal));

            EA.GetEvent<CanSubmitEvent>().Publish();
        }
    }
}
