using POS.Repository.IServices;
using POS.ViewModel.Utils;
using Prism.Events;
using System.Linq;

namespace POS.ViewModel.ViewModels.Product
{
    public class ProductMainViewModel
    {
        public IProductRepository Repository { get; }

        public ProductMainViewModel(IProductRepository repository, IEventAggregator ea)
        {
            Repository = repository;

            ProductAddViewModel = new ProductAddViewModel(Repository,ea);
            ProductListViewModel = new ProductListViewModel(Repository,ea);
        } 

        public ProductAddViewModel ProductAddViewModel { get; }
        public ProductListViewModel ProductListViewModel { get; }
    }
}
