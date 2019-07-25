using POS.Repository.IServices;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel.ViewModels.Supplier
{
    public class SupplierMainViewModel  
    {
        public SupplierMainViewModel(ISupplierRepository repository,IEventAggregator ea)
        {
            SupplierListViewModel = new SupplierListViewModel(repository,ea);
            SupplierAddViewModel = new SupplierAddViewModel(repository, ea);
        }

       public SupplierListViewModel SupplierListViewModel { get; }
        public SupplierAddViewModel SupplierAddViewModel { get; }
    }
}
