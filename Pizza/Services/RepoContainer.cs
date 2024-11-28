using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace Pizza.Services
{
    public static class RepoContainer
    {
        private static IUnityContainer _container;
        static RepoContainer()
        {
            _container = new UnityContainer();
            _container.RegisterType<ICustomerRepository, CustomerRepository>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IOrderRepository, OrderRepository>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container => _container;
    }
}
