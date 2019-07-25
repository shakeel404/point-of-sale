
using POS.Repository.IServices;
using POS.ViewModel.Utils;
using POS.ViewModel.ViewModels.Order;
using POS.ViewModel.ViewModels.Product;
using POS.ViewModel.ViewModels.PurchaseOrder;
using POS.ViewModel.ViewModels.Supplier;
using Prism.Events;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace POS.ViewModel.ViewModels
{
    public class MainViewModel : BindableBase
    {
        const string SaveGlyph = "\xE74E";
        const string TickGlyph = "\xE73E";
        public MainViewModel( 
                            IProductRepository productRepository, 
                            ISupplierRepository supplierRepository,
                            IPurchaseOrderRepository purchaseOrderRepository,
                            IPurchaseOrderDetailRepository purchaseOrderDetailRepository,
                            IEventAggregator ea)
        {
            ProductMainViewModel = new ProductMainViewModel(productRepository, ea);
            SupplierMainViewModel = new SupplierMainViewModel(supplierRepository,ea);
            PurchaseOrderMainViewModel = new PurchaseOrderMainViewModel(productRepository,supplierRepository, purchaseOrderRepository,purchaseOrderDetailRepository, ea);
            ea.GetEvent<Messanger>().Subscribe(OnMessageChanged);
            Glyph = TickGlyph;


        }

        public ProductMainViewModel ProductMainViewModel { get; }
        public SupplierMainViewModel SupplierMainViewModel { get; } 

        public PurchaseOrderMainViewModel PurchaseOrderMainViewModel { get; }

        string message,glyph;
        public string Message
        {
            get { return message; }
            set
            {
                SetProperty(ref message, value);
            }
        }

        public string Glyph
        {
            get { return glyph; }
            set
            {
                SetProperty(ref glyph, value);
            }
        }

        private async void OnMessageChanged(string message)
        {
            Glyph = SaveGlyph;
            Message = message;  
            await Task.Delay(5000);
            Glyph = TickGlyph;
            Message = string.Empty;


        }
    } 
    
}
