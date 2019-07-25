using AutoMapper;
using POS.Model.Models;
using POS.Repository.IServices;
using POS.ViewModel.ViewModels.Product;
using POS.ViewModel.ViewModels.PurchaseOrder;
using POS.ViewModel.ViewModels.Supplier;
using Prism.Events;

namespace POS.ViewModel
{
    public class BootStrapper
    {
        public void BootStrap(
                                IProductRepository productRepository,
                                ISupplierRepository supplierRepository,
            
            IEventAggregator ea)
        {
          
              Mapper.Initialize(cfg =>  
              {
                  cfg.CreateMap<Product, ProductDetailViewModel>()
                      .ForMember(d => d.Repository, o => o.MapFrom(f => productRepository))
                      .ForMember(d => d.EA, o => o.MapFrom(f => ea));

                  cfg.CreateMap<Supplier, SupplierDetailViewModel>()
                      .ForMember(d => d.Repository, o => o.MapFrom(f => supplierRepository))
                      .ForMember(d => d.EA, o => o.MapFrom(f => ea));
                  cfg.CreateMap<Product, PurchaseOrderDetailViewModel>()
                     .ForMember(d => d.EA, o => o.MapFrom(f => ea))
                     .ForMember(d => d.ProductId, o => o.MapFrom(f => f.Id));
              });
            
        }
    }
}
