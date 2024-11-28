using System.Collections.ObjectModel;
using Pizza.Models;
using Pizza.Services;

namespace Pizza.ViewModels
{
    class AddEditOrderViewModel : BindableBase
    {
        private  IOrderRepository _orderRepository;
        public AddEditOrderViewModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            
            Order = new Order
            {
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now.AddDays(1)
            };
            
            SaveOrderCommand = new RelayCommand(OnSaveOrder);
            CancelCommand = new RelayCommand(OnCancel);
            
            LoadOrderStatuses();
        }

        private Order _order;

        public Order Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        private Customer? _selectedCustomer;

        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                SetProperty(ref _selectedCustomer, value);
                Order.CustomerId = value?.Id ?? Guid.Empty;
            }
        }

        public ObservableCollection<Customer> Customers { get; } = new();
        public ObservableCollection<OrderStatus> OrderStatuses { get; } = new();

        private OrderStatus? _selectedOrderStatus;

        public OrderStatus? SelectedOrderStatus
        {
            get => _selectedOrderStatus;
            set
            {
                SetProperty(ref _selectedOrderStatus, value);
                Order.OrderStatusId = value?.Id ?? 0;
            }
        }

        public async void LoadOrderStatuses()
        {
            var statuses = await _orderRepository.GetAllOrderStatusesAsync();
            OrderStatuses.Clear();
            foreach (var status in statuses)
            {
                OrderStatuses.Add(status);
            }
        }

        public RelayCommand SaveOrderCommand { get; }
        public RelayCommand CancelCommand { get; private set; }
        
        private async void OnSaveOrder()
        {
            await _orderRepository.AddOrderAsync(Order);
            Done?.Invoke();
        }
        
        internal void OnCancel()
        {
            Done?.Invoke();
        }

        public event Action? Done;
    }
}