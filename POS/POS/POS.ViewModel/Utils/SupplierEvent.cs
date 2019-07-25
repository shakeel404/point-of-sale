using Prism.Events;
using POS.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel.Utils
{
    public class SupplierEvent : PubSubEvent<Supplier> { }

    public class SupplierAddEvent : SupplierEvent { }
    public class SupplierRemoveEvent : SupplierEvent { }


    

}
