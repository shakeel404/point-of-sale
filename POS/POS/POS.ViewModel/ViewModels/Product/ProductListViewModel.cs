using AutoMapper;
using POS.Repository.IServices;
using System.Linq;
using P = POS.Model.Models;
using System.Collections.ObjectModel;
using Prism.Events;
using POS.ViewModel.Utils;
using System;

namespace POS.ViewModel.ViewModels.Product
{
    public class ProductListViewModel : ListableViewModel<ProductAddEvent,ProductRemoveEvent,P.Product,ProductDetailViewModel>
    {
       
        public ProductListViewModel(IProductRepository repository,IEventAggregator ea):base(repository,ea)
        {

            Get();

            EA.GetEvent<SubmittedEvent>().Subscribe(Get);
        } 
    }
}
