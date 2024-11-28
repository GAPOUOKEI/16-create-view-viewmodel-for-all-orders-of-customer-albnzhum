using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Pizza.Models;
using Pizza.Services;
using Pizza.ViewModels;
using Unity;
using Unity.Resolution;

namespace Pizza
{
    class MainWindowViewModel : BindableBase
    {
        private AddEditCustomerViewModel _addEditCustomerVewModel;
        private CustomerListViewModel _customerListViewModel;
        private AddEditOrderViewModel _addEditOrderPrepViewModel;
        private CustomerOrdersViewModel _orderViewModel;
        
        public MainWindowViewModel()
        {
            NavigationCommand = new RelayCommand<string>(OnNavigation);

            _customerListViewModel = RepoContainer.Container.Resolve<CustomerListViewModel>();
            _addEditCustomerVewModel = RepoContainer.Container.Resolve<AddEditCustomerViewModel>();
            _addEditOrderPrepViewModel = RepoContainer.Container.Resolve<AddEditOrderViewModel>();
            _orderViewModel = RepoContainer.Container.Resolve<CustomerOrdersViewModel>();

            _customerListViewModel.PlaceOrderRequested += NavigateToOrder;

            _customerListViewModel.AddCustomerRequested += NavigationToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavigationToEditCustomer;

            _customerListViewModel.GetAllOrdersRequested += NavigateToOrders;

            _addEditCustomerVewModel.Done += OnDone;
            _addEditOrderPrepViewModel.Done += ReturnToCustomerList;
            _orderViewModel.Done += ReturnToCustomerList;
        }

        private BindableBase _currentViewModel;

        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        //открывать список клиентов
        private void OnNavigation(string dest)
        {
            switch (dest)
            {
                case "orderPrep":
                    CurrentViewModel = _addEditOrderPrepViewModel;
                    break;
                case "customers":
                default:
                    CurrentViewModel = _customerListViewModel;
                    break;
            }
        }

        private void OnDone()
        {
            _ = _customerListViewModel.LoadCustomers();
            CurrentViewModel = _customerListViewModel;
        }

        //открывать окно для редактирования клиента
        private void NavigationToEditCustomer(Customer customer)
        {
            _addEditCustomerVewModel.IsEditeMode = true;
            _addEditCustomerVewModel.SetCustomer(customer);
            CurrentViewModel = _addEditCustomerVewModel;
        }

        private void NavigationToEditOrder(Customer customer)
        {
            _addEditCustomerVewModel.IsEditeMode = true;
            _addEditCustomerVewModel.SetCustomer(customer);
            CurrentViewModel = _addEditCustomerVewModel;
        }

        //открывать окно для добавления клиента
        //private void NavigationToAddCustomer(Customer cust)----------------------------
        private void NavigationToAddCustomer()
        {
            _addEditCustomerVewModel.IsEditeMode = false;
            _addEditCustomerVewModel.SetCustomer(new Customer
            {
                Id = Guid.NewGuid(),
            });
            CurrentViewModel = _addEditCustomerVewModel;
        }

        //окно для оформления заказа
        private void NavigateToOrder(Customer? customer)
        {
            if (customer == null)
            {
                MessageBox.Show("Customer is not selected. Please select a customer to proceed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _addEditOrderPrepViewModel.SelectedCustomer = customer;
            _addEditOrderPrepViewModel.Order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now.AddDays(1)
            };
            CurrentViewModel = _addEditOrderPrepViewModel;
        }

        private void NavigateToOrders(Customer? customer)
        {
            if (customer == null)
            {
                MessageBox.Show("Customer is not selected. Please select a customer to proceed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _orderViewModel.LoadOrders();
            _orderViewModel.SelectedCustomer = customer;
            CurrentViewModel = _orderViewModel;
        }
        
        private void ReturnToCustomerList()
        {
            CurrentViewModel = _customerListViewModel;
        }
    }
}