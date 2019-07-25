using POS.Model.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel.Utils
{
    public class ProductEvent : PubSubEvent<Product> { }

    public class ProductAddEvent : ProductEvent { }

    public class ProductRemoveEvent : ProductEvent { }

    public class ProductModelChange : PubSubEvent { }
}
