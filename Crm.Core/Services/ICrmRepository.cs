using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crm.Core.Models;

namespace Crm.Core.Services
{
    public interface ICrmRepository
    {
        IList<Customer> GetCustomers();
        IList<Permission> GetPermissions();
        IList<CustomerCall> GetCustomerCalls();
        IList<User> GetUsers();
        Customer GetCustomer(int customerNo);
        Permission GetPermission(int id);
        CustomerCall GetCustomerCall(Guid id);
        User GetUser(string userName);
        bool UpdateCustomer(Customer customer);
        bool UpdateCustomerCall(CustomerCall customerCall);
        CustomerCall InsertCustomerCall(CustomerCall customerCall);
        Customer InsertCustomer(Customer customer);

    }

}
