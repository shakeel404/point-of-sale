using MahApps.Metro.Controls.Dialogs;
using POS.Repository.IServices;
using POS.ViewModel.Utils;
using Prism.Commands;
using Prism.Events;
using System; 

namespace POS.ViewModel.ViewModels.Product
{
    public class ProductDetailViewModel : ProductBaseViewModel<IProductRepository>
    {
        public ProductDetailViewModel(IProductRepository  repository,IEventAggregator ea)
        {
            EA = ea;
            Repository = repository; 
        }

        public ProductDetailViewModel()
        {

        }
       
        

        protected override void OnModelChanged()
        {
            if (!IsViewModelAttached)
                return;

            var product = Repository.Get(Id);
            product.Name = Name;
            product.Quantity = Quantity;
            product.PurchasePrice = PurchasePrice;
            product.SalePrice = SalePrice;

            Repository.Save();

            RaisePropertyChanged(nameof(UnitProfit));
            RaisePropertyChanged(nameof(TotalProfit));
        }
 

        protected override bool CanDelete()
        {
            return Id >0;
        }

        protected override async void Delete()
        {
            var dailogResult = await Dialoger.ShowMessageAsync(this, "Inventory", $"Do you want to delete '{Name}' with UPC: [{UPC}]", 
                                                        MessageDialogStyle.AffirmativeAndNegative,YesNoMessageSettings);

             
            if (dailogResult != MessageDialogResult.Affirmative)
                return;
           
            var product = Repository.Get(Id);

            Repository.Remove(product);
            Repository.Save(); 

            EA.GetEvent<ProductRemoveEvent>().Publish(product); 
        }

         
    }
}
