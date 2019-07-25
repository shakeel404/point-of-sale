
using POS.Repository.IServices;
using Prism.Commands;
using Prism.Events;
using System;
using S = POS.Model.Models;
using System.Collections.Generic;
using System.Text;
using POS.ViewModel.Utils;

namespace POS.ViewModel.ViewModels.Supplier
{
    public class SupplierAddViewModel : SavableViewModel<ISupplierRepository>
    {
        
        public SupplierAddViewModel(ISupplierRepository repository, IEventAggregator ea) 
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

        protected override  bool CanSave()
        {
            return !(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(Contact));
        }

        protected override void Save()
        {  
            var supplier = new S.Supplier()
            {
                Name = Name,
                Address = Address,
                Contact = Contact 
            };

            Repository.Add(supplier);
            Repository.Save();
            Name =Contact= string.Empty;

            EA.GetEvent<SupplierAddEvent>().Publish(supplier);
        }

       
    }
}
