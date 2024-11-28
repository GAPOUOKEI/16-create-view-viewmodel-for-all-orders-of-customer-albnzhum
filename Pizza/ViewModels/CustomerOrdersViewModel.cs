using System.Collections.ObjectModel;
using Pizza.Models;
using Pizza.Services;

namespace Pizza.ViewModels;

public class CustomerOrdersViewModel : BindableBase
{
    private readonly IOrderRepository _orderRepository;

    public CustomerOrdersViewModel(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        Orders = new ObservableCollection<Order>();
        LoadOrders();
       
        CloseCommand = new RelayCommand(Close);
    }

    private Customer _selectedCustomer;

    public Customer SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            SetProperty(ref _selectedCustomer, value);
        }
    }

    private ObservableCollection<Order> _orders;
    public ObservableCollection<Order> Orders
    {
        get => _orders;
        set => SetProperty(ref _orders, value);
    }
    public RelayCommand CloseCommand { get; }
    
    public event Action? Done;

    public void Close()
    {
        Done?.Invoke();
    }

    public async Task LoadOrders()
    {
        var orders = await _orderRepository.GetOrdersByCustomerAsync(SelectedCustomer.Id);
        Orders.Clear();
        foreach (var order in orders.OrderBy(o => o.OrderDate))
        {
            Orders.Add(order);
        }
    }
}