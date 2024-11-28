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
using Pizza.Services;
using Pizza.ViewModels;

namespace Pizza.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderPrepView.xaml
    /// </summary>
    public partial class AddEditOrderView : UserControl
    {
        private readonly AddEditOrderViewModel _viewModel;

        public AddEditOrderView()
        {
            InitializeComponent();
           /* _viewModel = new AddEditOrderViewModel(orderRepository);
            DataContext = _viewModel;*/

            // Load customers
            //_viewModel.LoadCustomers(customerRepository);
        }
    }
}
