
using POS.Repository.IServices;
using POS.ViewModel.Utils;
using POS.ViewModel.ViewModels.Order;
using POS.ViewModel.ViewModels.Supplier;
using M=POS.Model.Models;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace POS.ViewModel.ViewModels.PurchaseOrder
{
    public class PurchaseOrderMainViewModel :BaseViewModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;

        public PurchaseOrderMainViewModel(
            IProductRepository productRepository,
            ISupplierRepository supplierRepository,
            IPurchaseOrderRepository purchaseOrderRepository,
            IPurchaseOrderDetailRepository purchaseOrderDetailRepository,
            IEventAggregator ea)
        {
            ProductLocatingViewModel = new ProductLocatingViewModel(productRepository, ea);
            PurchaseOrderListViewModel = new PurchaseOrderListViewModel(ea);
            SupplierSelectionViewModel = new SupplierSelectionViewModel(supplierRepository, ea);
            PurchaseAllOrdersViewModel = new PurchaseAllOrdersViewModel(purchaseOrderRepository,ea);

            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
            _productRepository = productRepository;
           _supplierRepository = supplierRepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
            EA = ea;
            EA.GetEvent<CanSubmitEvent>().Subscribe(RaiseCanSubmitEvent);
            EA.GetEvent<UpdatePurchaseDetails>().Subscribe(OnUpdatePurchaseDetails);
        }

        

        public SupplierSelectionViewModel SupplierSelectionViewModel { get; }
        public ProductLocatingViewModel ProductLocatingViewModel { get; }
        public PurchaseOrderListViewModel PurchaseOrderListViewModel { get; }
        public PurchaseAllOrdersViewModel PurchaseAllOrdersViewModel { get; }


        public DelegateCommand SubmitCommand { get; }

        private bool CanSubmit()
        {
            return SupplierSelectionViewModel.SelectedItem != null 
                && PurchaseOrderListViewModel.TotalProduct > 0;
        }

        private  async void Submit()
        { 
            var progress = await Dialoger.ShowProgressAsync(this, "Purchase Order", "Please wait. Order is in progress", settings:OkCancelMessageSettings) ;
            var maxValue = PurchaseOrderListViewModel.Items.Count;
            progress.Minimum = 0;
            progress.Maximum = maxValue;

            var progressValue = 0;
            await Task.Delay(100);
            var purchaseOrder = new M.PurchaseOrder
            {
                SupplierId = SupplierSelectionViewModel.SelectedItem.Id,
                OrderDate = SupplierSelectionViewModel.OrderDate
            };

            foreach (var item in PurchaseOrderListViewModel.Items)
            {
                var purchasedetail = new M.PurchaseOrderDetail()
                {
                    ProductId = item.ProductId,
                     PurchasePrice=item.PurchasePrice,
                     SalePrice=item.SalePrice,
                      Quantity=item.Quantity, 
                }; 
                purchaseOrder.OrderDetails.Add(purchasedetail); 

                var product = _productRepository.Get(purchasedetail.ProductId); 

                product.Quantity += purchasedetail.Quantity;

                if (purchasedetail.PurchasePrice > product.PurchasePrice)
                    product.PurchasePrice = purchasedetail.PurchasePrice;

                if (purchasedetail.SalePrice > product.SalePrice)
                    product.SalePrice = purchasedetail.SalePrice;

                _productRepository.Save();

                progressValue++;

                 progress.SetProgress(progressValue); 
                await Task.Delay(100);
            }

           var supplier= _supplierRepository.Get(purchaseOrder.SupplierId);

            supplier.PurchaseOrders.Add(purchaseOrder);
            _supplierRepository.Save(); 

            await Task.Delay(100);

            PurchaseOrderListViewModel.Items.Clear();
            PurchaseOrderListViewModel.CalculateTotal();

            EA.GetEvent<SubmittedEvent>().Publish();

            await progress.CloseAsync();
            await Dialoger.ShowMessageAsync(this, "Purchase Order", "Purchase order completed.\nInventory is being updated.", MessageDialogStyle.Affirmative,OkCancelMessageSettings);
        }

        private void RaiseCanSubmitEvent()
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

        protected override void OnModelChanged()
        {
             
        }


        private void OnUpdatePurchaseDetails(M.PurchaseOrderDetail detail)
        {

            var detilsInDb = _purchaseOrderDetailRepository.Get(detail.Id);

            var qty = detail.Quantity - detilsInDb.Quantity;  


            detilsInDb.Quantity = detail.Quantity;
            detilsInDb.PurchasePrice = detail.PurchasePrice;
            detilsInDb.SalePrice = detail.SalePrice;

            _purchaseOrderDetailRepository.Save();

            var productInDb = _productRepository.Get(detail.ProductId);

            productInDb.Quantity += qty;
            productInDb.SalePrice = detail.SalePrice > productInDb.SalePrice ? detail.SalePrice : productInDb.SalePrice;
            productInDb.PurchasePrice = detail.PurchasePrice > productInDb.PurchasePrice ? detail.PurchasePrice : productInDb.PurchasePrice;

            _productRepository.Save();


            ///this event is enough to call. Because it will will update the information where it is subscribed
            EA.GetEvent<SubmittedEvent>().Publish();
            EA.GetEvent<Messanger>().Publish("Inventory is being updated");
            //await Dialoger.ShowMessageAsync(this, "Inventory", "Inventory is being updated.", MessageDialogStyle.Affirmative, OkCancelMessageSettings);


        }
    }
}
