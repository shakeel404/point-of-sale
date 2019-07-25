using POS.Model.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel.Utils
{
    public class PurchaseEvent:PubSubEvent<Product>
    {
    }

    public class PurchaseAddEvent : PurchaseEvent { }
    public class PurchaseRemoveEvent : PurchaseEvent { }

    public class CanSubmitEvent : PubSubEvent { }

    public class SubmittedEvent : PubSubEvent { }

    public class UpdatePurchaseDetails: PubSubEvent<PurchaseOrderDetail> { }

}
