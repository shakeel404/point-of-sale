using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Db.Db;
using POS.Repository.IServices;
using POS.Repository.Services;
using POS.ViewModel;
using POS.ViewModel.ViewModels;
using POS.ViewModel.ViewModels.Product;
using Prism.Events;

namespace POS.UI
{

    public partial class App : Application
    {  
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e); 

            var services = new ServiceCollection();

            services.AddSingleton<MainWindow>();  
            services.AddSingleton<DialogCoordinator>();  
            services.AddSingleton<PosContext>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ProductMainViewModel>(); 
            services.AddSingleton<IEventAggregator ,EventAggregator>();  

            ///Scope Repositories 
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderDetailRepository, PurchaseOrderDetailRepositoy>();
            services.AddScoped<IDialogCoordinator, DialogCoordinator>();

  

            var serviceProvider = services.BuildServiceProvider();

            var bootstrapper = new BootStrapper();
            bootstrapper.BootStrap(
                serviceProvider.GetService<IProductRepository>(),
                serviceProvider.GetService<ISupplierRepository>(), 
                serviceProvider.GetService<IEventAggregator>());


            var window = serviceProvider.GetService<MainWindow>();

            window.Show();

            
        }
    }
}
