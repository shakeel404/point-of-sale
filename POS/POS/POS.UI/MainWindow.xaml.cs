using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs; 
using POS.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POS.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
         public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
          DataContext = viewModel;
          ProductMain.DataContext = viewModel.ProductMainViewModel; 
          SupplierMain.DataContext = viewModel.SupplierMainViewModel;
          PurchaseMain.DataContext = viewModel.PurchaseOrderMainViewModel;
        }

       

        private void MenuControl_ItemClick(object sender, MahApps.Metro.Controls.ItemClickEventArgs e)
        {
            var active = e.ClickedItem as HamburgerMenuGlyphItem;
            if (active != null)
            { 
                this.MenuControl.Content = active;
            }  
        }

         
    }
}
