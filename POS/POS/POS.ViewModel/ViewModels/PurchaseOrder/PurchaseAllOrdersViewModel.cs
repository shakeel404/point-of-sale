using POS.Repository.IServices;
using System;
using System.Collections.Generic;
using M = POS.Model.Models;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using POS.ViewModel.Utils;
using Prism.Events;
using Prism.Commands;

namespace POS.ViewModel.ViewModels.PurchaseOrder
{
    public class PurchaseAllOrdersViewModel :  ListableBaseViewModel
    {
        private M.PurchaseOrder purchaseOrder; 
        private M.PurchaseOrderDetail purchaseOrderDetail; 

        public PurchaseAllOrdersViewModel(IPurchaseOrderRepository repository ,IEventAggregator ea)
        {
            Repository = repository; 
            EA = ea;
            Items = new ObservableCollection<M.PurchaseOrder>();

            GetItems(Page, Size);

            EA.GetEvent<SubmittedEvent>().Subscribe(Get);
            UpdatePurchaseDetailCommand = new DelegateCommand(UpdateDetail, CanUpdateDetail);
        }

        public int GrandTotal { get; set; }

        public int TotalQuantity { get; set; }

        public int TotalPayment { get; set; }

        public int TotalDues { get { return GrandTotal - TotalPayment; } }
        
        public IPurchaseOrderRepository Repository { get; }

        public ObservableCollection<M.PurchaseOrder> Items { get; private set; }

       
        public M.PurchaseOrder SelectedOrder
        {
            get { return purchaseOrder; }
            set
            {
                SetProperty(ref purchaseOrder, value, OnModelChanged);
            }
        } 

        public M.PurchaseOrderDetail SelectedOrderDetail
        {
            get { return purchaseOrderDetail; }
            set
            {
                SetProperty(ref purchaseOrderDetail, value, OnModelChanged);
            }
        }


        public DelegateCommand UpdatePurchaseDetailCommand { get; }


        private bool CanUpdateDetail()
        {
            return SelectedOrderDetail != null;
        }

        private void UpdateDetail()
        {

            EA.GetEvent<UpdatePurchaseDetails>().Publish(SelectedOrderDetail); 
        }

        protected virtual void Get()
        {
            GetItems(Page, Size);
        }

        protected override void GetItems(int page, int size)
        {
            Items.Clear();

            var items = Repository.Get(page, size)
                .Include(o => o.OrderDetails)
                .ThenInclude(p => p.Product)
                .Include(p => p.Supplier)
                .Include(p=>p.Payments)
                .OrderByDescending(o=>o.OrderDate).AsNoTracking();

            foreach (var item in items)
                Items.Add(item);
            
        }

        protected override void OnModelChanged()
        {
            if(SelectedOrder!=null)
            {
                GrandTotal = (int)SelectedOrder.OrderDetails.Sum(o => o.PurchasePrice * o.Quantity);
                TotalQuantity = (int)SelectedOrder.OrderDetails.Sum(o => o.Quantity);
                TotalPayment = (int)SelectedOrder.Payments.Sum(o => o.Amount);
            }
            else
            {
                GrandTotal = 0;
                TotalQuantity = 0;
                TotalPayment = 0;
            }

            RaisePropertyChanged(nameof(GrandTotal));
            RaisePropertyChanged(nameof(TotalQuantity));
            RaisePropertyChanged(nameof(TotalPayment));
            RaisePropertyChanged(nameof(TotalDues));
            UpdatePurchaseDetailCommand?.RaiseCanExecuteChanged();
        }

       
    }
}
