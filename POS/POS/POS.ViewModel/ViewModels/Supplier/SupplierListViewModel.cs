using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using S = POS.Model.Models;
using System.Text;
using POS.Repository.IServices;
using System.Linq;
using AutoMapper;
using Prism.Events;
using POS.ViewModel.Utils;

namespace POS.ViewModel.ViewModels.Supplier
{
    public class SupplierListViewModel : ListableViewModel<SupplierAddEvent,SupplierRemoveEvent,S.Supplier,SupplierDetailViewModel>
    {
        

        public SupplierListViewModel(ISupplierRepository repository, IEventAggregator ea) 
            : base(repository,ea)
        {  
             
            Get();
        } 
    }
}
