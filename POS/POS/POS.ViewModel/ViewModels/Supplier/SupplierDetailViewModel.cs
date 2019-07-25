using MahApps.Metro.Controls.Dialogs;
using POS.Repository.IServices;
using POS.ViewModel.Utils;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel.ViewModels.Supplier
{
    public  class SupplierDetailViewModel : DeletableViewModel<ISupplierRepository>
    {
        public SupplierDetailViewModel()
        {

        } 

        public SupplierDetailViewModel(ISupplierRepository repository,IEventAggregator ea):this()
        {
            Repository = repository;
            EA = ea;

        }

        string name, address, contact; 

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                    SetProperty<string>(ref name, value, OnModelChanged);
            }
        }


        public string Address
        {
            get { return address; }
            set
            {
                if (address != value)
                    SetProperty<string>(ref address, value, OnModelChanged);
            }
        }


        public string Contact
        {
            get { return contact; }
            set
            {
                if (contact != value)
                    SetProperty<string>(ref contact, value, OnModelChanged);
            }
        }

        protected override void OnModelChanged()
        {
            if (!IsViewModelAttached)
                return;

            var supplier = Repository.Get(Id);

            supplier.Name = Name;
            supplier.Contact = Contact;
            supplier.Address = Address;

            Repository.Save();
        }

        protected override  bool CanDelete()
        {
            return Id > 0;
        }

        protected override async void Delete()
        {
            var dailogResult = await Dialoger.ShowMessageAsync(this, "Supplier", $"Do you want to delete '{Name}' of {Address}",
                                                        MessageDialogStyle.AffirmativeAndNegative, YesNoMessageSettings);


            if (dailogResult != MessageDialogResult.Affirmative)
                return;

            var supplier = Repository.Get(Id);

            Repository.Remove(supplier);
            Repository.Save();

            EA.GetEvent<SupplierRemoveEvent>().Publish(supplier);
        }

    }
}
