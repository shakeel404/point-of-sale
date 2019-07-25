using POS.Repository.IServices;
using POS.ViewModel.Utils;
using POS.ViewModel.ViewModels.Supplier;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel.ViewModels.PurchaseOrder
{
    public class SupplierSelectionViewModel : SupplierListViewModel
    {
        public SupplierSelectionViewModel(ISupplierRepository repository, IEventAggregator ea) :base(repository,ea)    
        {
            OrderDate = DateTime.Now;
        }

        private bool isOpen;
        private DateTime orderDate;
        private SupplierDetailViewModel selectedItem;

        public SupplierDetailViewModel SelectedItem 
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value,OnModelChanged);
            }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                SetProperty<bool>(ref isOpen, value);
            }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set
            {
                SetProperty<DateTime>(ref orderDate, value);
            }
        }

        protected override void OnModelChanged()
        { 
            IsOpen = true;
            base.OnModelChanged();  

            if(EA!=null)
            EA.GetEvent<CanSubmitEvent>().Publish();
        }
    }

    
}
